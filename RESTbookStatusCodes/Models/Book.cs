using System;

namespace RESTbookStatusCodes.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public static void ValidateBook(Book book)
        {
            if (book.Title == null)
                throw new ArgumentNullException(nameof(book.Title), "Title is null");
            if (book.Title.Length < 2)
                throw new ArgumentException("Title must be at least 2 characters: " + book.Title);
            if (book.Price < 0)
                throw new ArgumentOutOfRangeException(nameof(book.Price), "Price must be non-negative: " + book.Price);
        }
    }
}
