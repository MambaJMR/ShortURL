using Microsoft.AspNetCore.Mvc;
using ShortURL.Data;
using ShortURL.Models.DB;
using ShortURL.Models.Models;
using ShortURL.Serveces;

namespace ShortURL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var urlList = _context.Urls.ToList();
            return View(urlList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] UrlModel model)
        {

            if (Uri.IsWellFormedUriString(model.LongUrl, UriKind.Absolute))
            {
                string guidStr = UrlService.GuidCreate();
                var shortUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{guidStr}";

                var obj = new Url
                {
                    LongUrl = model.LongUrl,
                    GuidString = guidStr,
                    ShortUrl = shortUrl,
                    CreationDate = DateTime.Now,
                    TransitionСount = 0
                };

                _context.Urls.Add(obj);
                await _context.SaveChangesAsync();
                model.ShortUrl = obj.ShortUrl;
                return Ok(model);
            }
            else
            {
                return BadRequest(model);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Url url = _context.Urls.FirstOrDefault(x => x.Id == id);


            if (url == null)
            {
                return NotFound();
            }
            return View(url);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteUrl(int? id)
        {
            var obj = _context.Urls.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Urls.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Url url = _context.Urls.FirstOrDefault(x => x.Id == id);


            if (url == null)
            {
                return NotFound();
            }
            return View(url);
        }

        [HttpPost]
        public IActionResult Update(int? id)
        {
            string guidStr = UrlService.GuidCreate();
            var obj = _context.Urls.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.GuidString = guidStr;
            obj.ShortUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{guidStr}";

            _context.Urls.Update(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}