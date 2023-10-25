using System;

namespace RESTbookStatusCodes.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public void Validate()
        {
            {
                if (Title == null)
                    throw new ArgumentNullException(nameof(Title), "Title is null");
                if (Title.Length < 2)
                    throw new ArgumentException("Title must be at least 2 characters: " + Title);
                if (Price < 0)
                    throw new ArgumentOutOfRangeException(nameof(Price), "Price must be non-negative: " + Price);
            }
        }
    }
}
