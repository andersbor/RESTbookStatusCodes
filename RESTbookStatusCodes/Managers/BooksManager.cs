using System.Collections.Generic;
using System.Linq;
using RESTbookStatusCodes.Models;
using static RESTbookStatusCodes.Models.Book;

namespace RESTbookStatusCodes.Managers
{
    public class BooksManager
    {
        private static int _nextId = 1;
        private static readonly List<Book> Data = new List<Book>
        {
            new Book {Id = _nextId++, Title = "C# is nice", Price = 12.34},
            new Book {Id=_nextId++, Title = "C# advanced", Price = 22.33},
            new Book {Id = _nextId++, Title = "ABC for beginners", Price= 19.95}
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers
        };

        public List<Book> GetAll(string title = null, string sortBy = null)
        // Optional parameters
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments
        {
            List<Book> books = new List<Book>(Data);
            // copy constructor
            // Callers should no get a reference to the Data object, but rather get a copy

            if (title != null)
            {
                books = books.FindAll(book => book.Title.StartsWith(title));
            }
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "id":
                        books = books.OrderBy(book => book.Id).ToList();
                        break;
                    case "title":
                        books = books.OrderBy(book => book.Title).ToList();
                        break;
                    case "priceasc":
                        books = books.OrderBy(book => book.Price).ToList();
                        break;
                    case "pricedesc":
                        books = books.OrderByDescending(book => book.Price).ToList();
                        break;
                }
            }
            return books;
        }

        public Book GetById(int id)
        {
            return Data.Find(book => book.Id == id);
        }

        public Book Add(Book newBook)
        {
            ValidateBook(newBook); // using static ...
            newBook.Id = _nextId++;
            Data.Add(newBook);
            return newBook;
        }

        public Book Delete(int id)
        {
            Book book = Data.Find(book1 => book1.Id == id);
            if (book == null) return null;
            Data.Remove(book);
            return book;
        }

        public Book Update(int id, Book updates)
        {
            ValidateBook(updates);
            Book book = Data.Find(book1 => book1.Id == id);
            if (book == null) return null;
            book.Title = updates.Title;
            book.Price = updates.Price;
            return book;
        }
    }
}
