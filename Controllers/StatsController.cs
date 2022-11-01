using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senddatatest.Data;
using senddatatest.Models;

namespace senddatatest.Controllers
{
    public class StatsController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;

        public StatsController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        // GET: StatsController
        public ActionResult Index()
        {
            Dictionary<string, int> clickMonths = _db.Recorddatas.Where(x => x.DateTime.Month == DateTime.Now.Month).GroupBy(g=>g.Register).Select(s => new {Short = s.Key, Count = s.Count()}).ToDictionary(k=>k.Short,v=>v.Count);
            //JsonResult aux = GetLinksCount(clickMonths);
            return View(clickMonths);
        }

        // GET: StatsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StatsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StatsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StatsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StatsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StatsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetLinksCount()
        {
            List<ClickMonth> clickMonths = new List<ClickMonth>();
            Dictionary<string, int> aux = _db.Recorddatas.Where(x => x.DateTime.Month == DateTime.Now.Month).GroupBy(g => g.Register).Select(s => new { Short = s.Key, Count = s.Count() }).ToDictionary(k => k.Short, v => v.Count);
            foreach (var item in aux) {
                clickMonths.Add(new ClickMonth (item.Key, item.Value ));
            }
            return Json(new { JSONList = clickMonths});
        }
    }
}
