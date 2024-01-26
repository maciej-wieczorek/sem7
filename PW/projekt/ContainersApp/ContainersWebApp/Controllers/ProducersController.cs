using ContainersApp.BLC;
using Microsoft.AspNetCore.Mvc;

namespace ContainersWebApp.Controllers
{
    public class ProducersController : Controller
    {
        private readonly BLC _blc;
        public ProducersController(BLC blc)
        {
            _blc = blc;
        }
        public IActionResult Index()
        {
            return View(_blc.GetAllProducers());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducer((int)id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address")] Models.Producer producer)
        {
            if (ModelState.IsValid)
            {
                _blc.AddProducer(producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducer((int)id);
            if (producer == null)
            {
                return NotFound();
            }

            Models.Producer producerModel = new Models.Producer()
            {
                Id = producer.Id,
                Name = producer.Name,
                Address = producer.Address
            };

            return View(producerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address")] Models.Producer producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _blc.UpdateProducer(producer);
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducer((int)id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _blc.DeleteProducer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
