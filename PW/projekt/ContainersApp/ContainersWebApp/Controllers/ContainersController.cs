using ContainersApp.BLC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace ContainersWebApp.Controllers
{
    public class ContainersController : Controller
    {
        private readonly BLC _blc;
        public ContainersController(BLC blc)
        {
            _blc = blc;
        }
        public IActionResult Index()
        {
            return View(_blc.GetAllContainers());
        }

        public IActionResult Details(int id)
        {
            return View(_blc.GetContainer(id));
        }

        public IActionResult Create()
        {
            ViewBag.Producers = new SelectList(_blc.GetAllProducers(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,ProductionYear,Producer,Type")] Models.Container container)
        {
            if (ModelState.IsValid)
            {
                _blc.AddContainer(container);
                return RedirectToAction(nameof(Index));
            }
            return View(container);
        }
    }
}
