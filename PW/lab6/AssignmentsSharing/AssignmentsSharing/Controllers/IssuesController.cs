using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssignmentsSharing;
using AssignmentsSharing.Models;
using AssignmentsSharing.Algorithms.Exceptions;
using AssignmentsSharing.Algorithms;

namespace AssignmentsSharing.Controllers
{
    public class IssuesController : Controller
    {
        private readonly DataContext _context;

        public IssuesController(DataContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Issues.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            ViewBag.ListOfPseudonyms = _context.Developers.Select(u => u.Pseudonym).ToList();
            ViewBag.ListOfLabels = _context.Assignments.Select(a => a.Label).ToList();
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Label,Description")] Issue issue,
            [Bind("Developers")] string Developers, [Bind("Assignments")] string Assignments)
        {
            ModelState.Remove("Developers");
            ModelState.Remove("Assignments");
            if (ModelState.IsValid)
            {
                issue.Id = Guid.NewGuid();
                _context.Add(issue);
                await _context.SaveChangesAsync();

                issue = _context.Issues.Include("Developers")
                    .Include("Assignments").FirstOrDefault(m => m.Id == issue.Id);

                var listOfDevelopers = (Developers ?? "")
                    .Split(",")
                    .Select(pseudo => pseudo.Trim())
                    .Join(_context.Developers,
                        pseudo => pseudo,
                        d => d.Pseudonym,
                        (pseudo, d) => d)
                    .ToList();

                var listOfAssignments = (Assignments ?? "")
                    .Split(",")
                    .Select(desc => desc.Trim())
                    .Join(_context.Assignments,
                        desc => desc,
                        a => a.Label,
                        (desc, a) => a)
                    .ToList();

                issue.Developers = listOfDevelopers;
                issue.Assignments = listOfAssignments;
                _context.Issues.Update(issue);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Label,Description")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue != null)
            {
                _context.Issues.Remove(issue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(Guid id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Suggest(Guid? id)
        {
            if (id == null || _context.Issues == null)
            {
                return NotFound();
            }
            var issue = await _context.Issues
            .Include("Developers")
            .Include("Assignments")
            .FirstOrDefaultAsync(i => i.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            try
            {
                var algorithm = new BinaryPartitionAlgorithm<Assignment>();
                algorithm.SubsetsCount = issue.Developers.Count();
                algorithm.InterpretAsWeight = i => i.TimeCost;
                ViewBag.Result = algorithm.Run(issue.Assignments);
            }
            catch (InappropriateWeightException e)
            {
                ViewBag.Error = "Invalid time cost of one of the assignments";
            }
            catch (InputSetUnsplittableException e)
            {
                ViewBag.Error = "Cannot split the set of assignments";
            }
            catch (UnsupportedSubsetsCountException e)
            {
                ViewBag.Error = "Cannot split the set of assignments into " +
                issue.Developers.Count() + " subsets";
            }
            return View(issue);
        }
    }
}
