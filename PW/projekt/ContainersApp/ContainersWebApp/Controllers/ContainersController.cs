using ContainersApp.BLC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContainersWebApp.Controllers
{
    public class ContainersController : Controller
    {
        private readonly BLC _blc;
        public ContainersController(BLC blc)
        {
            _blc = blc;
        }
        public IActionResult Index(string? searchString)
        {
            if (searchString != null)
            {
                return View(_blc.GetContainersByName(searchString));
            }
            return View(_blc.GetAllContainers());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var container = _blc.GetContainer((int)id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        public IActionResult Create()
        {
            ViewBag.Producers = new SelectList(_blc.GetAllProducers(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,ProductionYear,ProducerId,Type")] Models.Container container)
        {
            if (ModelState.IsValid)
            {
                container.Producer = _blc.GetProducer(container.ProducerId);
                _blc.AddContainer(container);
                return RedirectToAction(nameof(Index));
            }
            return View(container);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var container = _blc.GetContainer((int)id);
            if (container == null)
            {
                return NotFound();
            }

            ViewBag.Producers = new SelectList(_blc.GetAllProducers(), "Id", "Name");

            Models.Container containerModel = new Models.Container()
            {
                Id = container.Id,
                Name = container.Name,
                Producer = container.Producer,
                ProducerId = container.Producer.Id,
                ProductionYear = container.ProductionYear,
                Type = container.Type
            };

            return View(containerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,ProductionYear,ProducerId,Type")] Models.Container container)
        {
            if (id != container.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                container.Producer = _blc.GetProducer(container.ProducerId);
                _blc.UpdateContainer(container);
                return RedirectToAction(nameof(Index));
            }

            return View(container);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var container = _blc.GetContainer((int)id);
            if (container == null)
            {
                return NotFound();
            }

            return View(container);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _blc.DeleteContainer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
