using BockingAppStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }

        [HttpGet]
        public ActionResult Buy( int id){
            ViewBag.BookId = id;
            return View();
        }

        //данный метод будет обрабатываться пост
        [HttpPost]
        public string Buy (Purchase purchase){

            purchase.Date = DateTime.Now;
            //сохраняем в бд
            db.Purchases.Add(purchase);
            //сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
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