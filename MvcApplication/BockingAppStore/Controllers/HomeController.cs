using BockingAppStore.Models;
using BockingAppStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace BockingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        public ActionResult Index()
        {
            IEnumerable<Book> books = db.Books;
            ViewBag.Books = books;

            ViewData["Head"] = "Hello World!";
            ViewBag.Head1 = "Hey all";
            ViewBag.Fruit = new List<string>{
            "яблоки", "сливы", "груши"
            };

            return View();

            //переопределение представления
            //return View("~/Views/Some/Index.cshtml");
        }

        public ActionResult GetImage(){
            string path = "../Content/Images/tattoo.png";
            return new ImageResult(path);

        }
        public ActionResult GetHtml(){
            return new HtmlResult("<h2> Hey World!</h2>");
        }

        //http://localhost:60958/home/getvoid?id=7
        public ActionResult GetVoid(int id){
            // тест redirect
            //return RedirectPermanent("~/Home/Contact");
            if( id >3){
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