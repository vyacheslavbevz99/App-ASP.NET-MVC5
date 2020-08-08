using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BockingAppStore.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>{
        protected override void Seed(BookContext db)
        {
            db.Books.Add( new Book { Name = "Война и мир", Author="Лев Толстой", Price= 300});
            db.Books.Add( new Book { Name = "Финансист", Author = "Теодор Драйзер", Price = 200 });
            db.Books.Add( new Book { Name = "Чайка", Author = "Чехов", Price = 250 });
            db.Books.Add(new Book { Name = "Нью-Йорк", Author = "Э. Резерфорд", Price = 400 });


        }

    }
}