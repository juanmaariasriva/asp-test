using Microsoft.AspNetCore.Mvc;
using senddatatest.Data;
using senddatatest.Models;

namespace senddatatest.Controllers
{
    public class GraphsController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;

        public GraphsController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<Register> registrations = _db.Registers;
            return View(registrations);
        }
        public JsonResult LoadBrowser(String Id)
        {
            Console.Write("Hola Mundo Sobre Linea");
            List<ClickMonth> clickMonths = new List<ClickMonth>();
            Dictionary<string, int> aux = _db.Recorddatas.Where(l => l.Register == Id).GroupBy(x => x.Browser).Select(l => new { Link = l.Key, Count = l.Count()}).ToDictionary(k => k.Link, v => v.Count);
            foreach (var item in aux)
            {
                clickMonths.Add(new ClickMonth(item.Key, item.Value));
            }
            return Json(new { JSONList = clickMonths });
        }
        public JsonResult LoadPlatform(String Id)
        {
            List<ClickMonth> clickMonths = new List<ClickMonth>();
            Dictionary<string, int> aux = _db.Recorddatas.Where(l => l.Register == Id).GroupBy(x => x.Platform).Select(l => new { Link = l.Key, Count = l.Count() }).ToDictionary(k => k.Link, v => v.Count);
            foreach (var item in aux)
            {
                clickMonths.Add(new ClickMonth(item.Key, item.Value));
            }
            return Json(new { JSONList = clickMonths });
        }
        public JsonResult LoadMonth(String Id)
        {
            List<ClickMonth> clickMonths = new List<ClickMonth>();
            Dictionary<string, int> aux = _db.Recorddatas.Where(l => l.Register == Id && l.DateTime.Year == DateTime.Today.Year && l.DateTime.Month == DateTime.Today.Month)
                .Select(l => new { Link = l.DateTime.Date.ToString()}).GroupBy(x => x.Link).Select(l => new { Link = l.Key, Count = l.Count() }).ToDictionary(k => k.Link, v => v.Count);
            foreach (var item in aux)
            {
                clickMonths.Add(new ClickMonth(item.Key, item.Value));
            }
            return Json(new { JSONList = clickMonths });
        }
    }
}
