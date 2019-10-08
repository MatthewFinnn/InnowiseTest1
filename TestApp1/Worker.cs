using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp1
{
    class Worker
    {
        public void Work()
        {
            Console.WriteLine("\n\t\tMenu");
            while (true)
            {
                switch (Menu())
                {
                    case 'q': return;
                    case 'a': ShowAuthors(); break;
                    case 'b': ShowBooks(); break;
                    case 's': ShowBooksByAuthors(); break;
                    case 'c': Create(); break;
                    case 'd': Delete(); break;
                }
            }
        }
        public char Menu()
        {
            Console.Write("\n" +
                "\n\t\tq - Quit" +
                "\n\t\ta - Show authors" +
                "\n\t\tb - Show books" +
                "\n\t\tc - Create" +
                "\n\t\td - Delete book" +
                "\n\t\ts - Show all books by the author\n\n");
            return Console.ReadLine().ToLower()[0];
        }

        public void ShowAuthors()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Console.Write("\n\tAuthors\n");
                foreach (Author a in db.Author.ToList())
                    Console.WriteLine("Id: {0}  \tname: {1}", a.Id_Author, a.Author1);
            }
        }

        public void ShowBooks()
        {
            using (MyDBEntities db = new MyDBEntities())
            {
                Console.Write("\n\tBooks\n");

                foreach (Book b in db.Book.ToList())
                    Console.WriteLine("Id: {0} \tYear: {2} \tTitle: {1}", b.Id_Book, b.Title, b.Year.Year);
            }
        }

        public void ShowBooksByAuthors()
        {
            int id;
            Console.Write("Enter id_Author: ");
            if (int.TryParse(Console.ReadLine(), out id))
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    var result = from b in db.Book
                                 join ba in db.BookAuthor on b.Id_Book equals ba.Id_Book
                                 join a in db.Author on ba.Id_Author equals a.Id_Author
                                 where a.Id_Author == id
                                 select new
                                 {
                                     Id_Book = b.Id_Book,
                                     Title = b.Title,
                                     Year = b.Year
                                 };

                    foreach (var r in result)
                        Console.WriteLine("Id: {0} \tYear: {2}  \tTitle: {1}", r.Id_Book, r.Title, r.Year.Year);
                }
            }
            else
            {
                //Console.WriteLine("Enter number!!!\n");
            }

        }
        public char MenuCreate()
        {
            Console.Write("\n" +
                "\n\t\tq - Quit" +
                "\n\t\ta - Create authors" +
                "\n\t\tb - Create books\n\n");
            return Console.ReadLine().ToLower()[0];
        }
        public void Create()
        {
            Console.WriteLine("\n\t\tMenu");
            while (true)
            {
                switch (MenuCreate())
                {
                    case 'q': return;
                    case 'a': CreateAuthor(); break;
                    case 'b': CreateBook(); break;
                }
            }
        }
        public void CreateAuthor()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Author a1 = new Author { Author1 = name };
            using (MyDBEntities db = new MyDBEntities())
            {
                db.Author.Add(a1);
                db.SaveChanges();
            }
            ShowAuthors();
        }

        public void CreateBook()
        {
            int id;
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter year: ");
            DateTime year;
            if (DateTime.TryParse("01-01-" + Console.ReadLine(), out year))
            {

                Book b1 = new Book { Title = title, Year = year };
                using (MyDBEntities db = new MyDBEntities())
                {
                    db.Book.Add(b1);
                    db.SaveChanges();
                }

                while (true)
                {
                    Console.Write("Enter id_Book (or letter to exit): ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        BookAuthor ba = new BookAuthor { Id_Book = b1.Id_Book, Id_Author = id };
                        using (MyDBEntities db = new MyDBEntities())
                        {
                            db.BookAuthor.Add(ba);
                            Author a = db.Author.Find(id);
                            if (a != null)
                            {
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                        break;
                }
            }
            ShowBooks();
        }

        public void Delete()
        {
            int id;
            Console.Write("Enter id_Book: ");
            if (int.TryParse(Console.ReadLine(), out id))
            {
                using (MyDBEntities db = new MyDBEntities())
                {
                    Book b = db.Book.Find(id);
                    if (b != null)
                    {
                        db.Book.Remove(b);
                        db.SaveChanges();
                    }
                }
            }
            ShowBooks();
        }
    }
}
