using Microsoft.AspNetCore.Mvc;
using senddatatest.Data;
using senddatatest.Models;
using System.Diagnostics;
using System.Text;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;
using Wangkanai.Extensions;

namespace senddatatest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;
        private readonly IDetectionService _detectionService;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger, IDetectionService detectionService)
        {
            _db = db;
            _logger = logger;
            _detectionService = detectionService;
        }


        [Route("")]
        [Route("{Id?}")]
        public IActionResult Index(String? Id)
        {
            if (!Id.IsNullOrEmpty())
            {
                Register aux = _db.Registers.Find(Id);
                if(aux != null)
                {
                    return new RedirectResult(aux.url);
                }
                else
                {
                    return new StatusCodeResult(404);
                }
            }
            IEnumerable<Register> registrations = _db.Registers;
            return View(registrations);
        }

        [HttpPost]
        public IActionResult Add(String Url)
        {
            Uri uriResult;
            Boolean isValid = Uri.TryCreate(Url, UriKind.Absolute, out uriResult);
            if (!isValid)
            {
                TempData["alert"] = "The URL is invalid";
            }
            else
            {
                Register reg = new Register();
                String code = "";
                for (int i = 0; i < 10; i++)
                {
                    code = getCode(5);
                    Register aux = _db.Registers.Find(code);
                    if (aux != null)
                    {
                        code = "";
                    }
                    else
                    {
                        break;
                    }
                }
                if (code.Length > 0)
                {
                    Register aux = _db.Registers.Where(x => x.url == Url).FirstOrDefault();
                    if (aux == null)
                    {
                        reg.id = code;
                        reg.url = Url;
                        reg.count = 0;
                        _db.Registers.Add(reg);
                        _db.SaveChanges();
                        TempData["success"] = "Was successfully created!!!";
                    }
                    else
                    {
                        TempData["alert"] = "The URL Exist!!! this one is " + aux.id;
                    }
                }
                else
                {
                    TempData["alert"] = "Error with the short url";
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Redirect(String? Id)
        {

            Register aux = _db.Registers.Find(Id);
            if(aux != null)
            {
                Recorddata record = new Recorddata();
                //record.Id = Guid.NewGuid().GetHashCode();
                record.Register = Id;
                record.Click = 1;
                record.Platform = _detectionService.Platform.Name.ToString();
                record.Browser = _detectionService.Browser.Name.ToString();
                record.DateTime = DateTime.Now;
                _db.Recorddatas.Add(record);
                aux.count++;
                _db.Registers.Update(aux);
                _db.SaveChanges();
                return new RedirectResult(aux.url); ;
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public String getCode(int length)
        {
            const string src = "ABCDEFGHIJKLMNOPQRESTUVWXYZ";
            var sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(src[random.Next(0,src.Length)]);
            }
            return sb.ToString();
        }
    }
}