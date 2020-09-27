using BockingAppStore.Models;
using BockingAppStore.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BockingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        public ActionResult SomeIndex(){
            return View("~/Views/Some/SomeView.cshtml");
        }
        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books.ToList();
            //ViewBag.Books = books;




            ViewData["Head"] = "Hello World!";
            ViewBag.Head1 = "Hey all";
            ViewBag.Fruit = new List<string>{
            "яблоки", "сливы", "груши"
            };
            return View(books);
            //сессии и куки
            /*Session["name"] = "Tom";
            HttpContext.Response.Cookies["id"].Value = "ca-1300w";*/
            //переопределение представления
            //return View("~/Views/Some/Index.cshtml");
        }

        public ActionResult BookIndex()
        {
            IEnumerable<Book> books = db.Books.ToList();
            //ViewBag.Books = books;
            return View(books);
        }

        //асинхронный метод
        public async Task<ActionResult> BookList()
        {
            IEnumerable<Book> books = await db.Books.ToListAsync();
            ViewBag.Books = books;
            return View("Index");
        }
        public string GetCookiesAndSession()
        {
            string id = HttpContext.Request.Cookies["id"].Value;
            var nameSession = Session["name"];
            return "<p> Id: " + id.ToString() + "</p><p> Session name: " + nameSession.ToString() + "</p>";
        }
        public void GetContext()
        {
            HttpContext.Response.Write("Привет мир!");
            string browser = HttpContext.Request.Browser.Browser;
            string userAgent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            //string cokies = HttpContext.Request.Cookies;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            HttpContext.Response.Write("<p>Browser: " + browser + "</p><p>User Agent: " + userAgent + "</p><p>Url: " + url
            + "</p><p>Ip-адрес: " + ip + "</p><p>Referrer: " + referrer + "</p>");
        }
        public FilePathResult GetFile()
        {
            //путь к файлу
            string filePath = Server.MapPath("~/Files/18.png");
            //Тип файла
            //string fileType = "application/png";
            //универсальный тип файла octet-stream
            string fileType = "application/octet-stream";
            // Имя файла
            string fileName = "18.png";
            return File(filePath, fileType, fileName);
        }

        public FileContentResult GetBytes()
        {
            string path = Server.MapPath("~/Files/18.png");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string fileType = "aplication/png";
            string fileName = "18.png";
            return File(mas, fileType, fileName);
        }

        public FileStreamResult GetStream()
        {
            string filePath = Server.MapPath("~/Files/18.png");
            // Обьект Stream
            FileStream fs = new FileStream(filePath, FileMode.Open);
            string fileType = "aplication/png";
            string fileName = "18.png";
            return File(fs, fileType, fileName);
        }

        public ActionResult GetImage()
        {
            string path = "../Content/Images/tattoo.png";
            return new ImageResult(path);

        }
        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2> Hey World!</h2>");
        }

        //http://localhost:60958/home/getvoid?id=7
        public ActionResult GetVoid(int id)
        {
            // тест redirect
            //return RedirectPermanent("~/Home/Contact");
            if (id > 3)
            {
                //return Redirect("~/Home/Contact");
                //return RedirectToRoute(new { controller = "Home", action = "Contact" } );
                //return RedirectToAction("Square", "Home", new { a = 10, h = 7 });
                //return new HttpStatusCodeResult(404);
                //return HttpNotFound();
                return new HttpUnauthorizedResult();
            }

            return View("About");
        }
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        //данный метод будет обрабатываться пост
        [HttpPost]
        public string Buy(Purchase purchase)
        {

            purchase.Date = DateTime.Now;
            //сохраняем в бд
            db.Purchases.Add(purchase);
            //сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }
        //http://localhost:60958/home/Square?a=10&h=30
        //public string Square(int a, int h)
        //{
        //    double s = a * h / 2;
        //    return "<h2> Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        //}

        //http://localhost:60958/home/Square?a=10&h=30
        public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2;
            return "<h2> Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }

        [HttpGet]
        public ActionResult GetBook()
        {
            return View();
        }

        //[HttpPost]
        //public string PostBook()
        //{
        //    string title = Request.Form["title"];
        //    string author = Request.Form["author"];
        //    return title +" "+ author;
        //}
        [HttpPost]
        public ContentResult PostBook()
        {
            string title = Request.Form["title"];
            string author = Request.Form["author"];
            return Content(title + " " + author);
        }


        public string GetId(int id)
        {
            return id.ToString();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}