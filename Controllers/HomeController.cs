using System.Diagnostics;
using BookStoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using BookStoreApplication.ViewModels;

namespace BookStoreApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "Ali" },
            new Author { Id = 2, Name = "Ahmed" }
        };

        public static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Basics", Price = 1500, AuthorId = 1 },
            new Book { Id = 2, Title = "ASP.NET Core",Price = 2000, AuthorId = 2 },
            new Book { Id = 3, Title = "MVC Guide", Price = 2500, AuthorId = 1 }
        };

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            author.Id = Authors.Count == 0 ? 1 : Authors.Max(a => a.Id) + 1;

            Authors.Add(author);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAuthor(int id)
        {
            for (int i = 0; i < Authors.Count; i++)
            {
                if (Authors[i].Id == id)
                {
                    Authors.RemoveAt(i);
                    break;
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            book.Id = Books.Count == 0 ? 1 : Books.Max(b => b.Id) + 1;

            Books.Add(book);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].Id == id)
                {
                    Books.RemoveAt(i);
                    break;
                }
            }

            return RedirectToAction("Index");
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Authors = Authors;

            model.Books = new List<BookCardViewModel>();

            foreach (var book in Books)
            {
                BookCardViewModel card = new BookCardViewModel();

                card.Id = book.Id;
                card.Title = book.Title;
                card.Price = book.Price;

                foreach (var author in Authors)
                {
                    if (author.Id == book.AuthorId)
                    {
                        card.AuthorName = author.Name;
                        break;
                    }
                }

                model.Books.Add(card);
            }

            return View(model);
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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
