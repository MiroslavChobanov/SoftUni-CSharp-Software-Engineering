namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            
        }
        //Task 01
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);
            var books = context.Books
                .Where(books => books.AgeRestriction == ageRestriction)
                .Select(book => book.Title)
                .OrderBy(title => title)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        //Task 02
        public static string GetGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            var result = new StringBuilder();

            foreach (var book in goldenBooks)
            {
                result.AppendLine(book);
            }

            return result.ToString().TrimEnd();

        }

        //Task 03
        public static string GetBooksByPrice(BookShopContext context)
        {
            var booksByPrice = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => new
                {
                    bookName = b.Title,
                    bookPrice = b.Price
                });

            var result = new StringBuilder();

            foreach (var book in booksByPrice)
            {
                result.AppendLine($"{book.bookName} - ${book.bookPrice:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 04
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var notReleased = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            var result = new StringBuilder();

            foreach (var book in notReleased)
            {
                result.AppendLine(book);
            }

            return result.ToString().TrimEnd();
        }

        //Task 05
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            var bookTitles = context.BooksCategories
                .Where(bc => categories.Contains(bc.Category.Name.ToLower()))
                .Select(bc => bc.Book.Title)
                .OrderBy(b => b)
                .ToList();

            var result = new StringBuilder();

            foreach (var book in bookTitles)
            {
                result.AppendLine(book);
            }

            return result.ToString().TrimEnd();
        }

        //Task 06
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateParts = date.Split('-');

            var day = int.Parse(dateParts[0]);
            var month = int.Parse(dateParts[1]);
            var year = int.Parse(dateParts[2]);

            var inputDate = new DateTime(year, month, day);

            var releasedBefore = context.Books
                .Where(b => b.ReleaseDate < inputDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    BookTitle = b.Title,
                    Edition = b.EditionType,
                    BookPrice = b.Price
                })

                .ToList();


            var result = new StringBuilder();

            foreach (var book in releasedBefore)
            {
                result.AppendLine($"{book.BookTitle} - {book.Edition} - ${book.BookPrice:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 07
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToList();

            var result = new StringBuilder();

            foreach (var author in authors)
            {
                result.AppendLine($"{author.FullName}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 08
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {

            var bookTitles = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();

            var result = new StringBuilder();

            foreach (var bookTitle in bookTitles)
            {
                result.AppendLine(bookTitle);
            }

            return result.ToString().TrimEnd();

        }

        //Task 09
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var bookTitles = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => new
                {
                    BookId = b.BookId,
                    BookTitle = b.Title,
                    AuthorFullName = b.Author.FirstName + " " + b.Author.LastName
                })
                .OrderBy(b => b.BookId)
                .ToList();

            var result = new StringBuilder();

            foreach (var bookTitle in bookTitles)
            {
                result.AppendLine($"{bookTitle.BookTitle} ({bookTitle.AuthorFullName})");
            }

            return result.ToString().TrimEnd();
        }

        //Task 10
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var bookTitles = context.Books
                .Where(b => b.Title.Length > lengthCheck);

            return bookTitles.Count();

        }

        //Task 11
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    BookCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.BookCopies)
                .ToList();


            var result = new StringBuilder();

            foreach (var author in authors)
            {
                result.AppendLine($"{author.FirstName} {author.LastName} - {author.BookCopies}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 12
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.CategoryBooks.Select(cb => cb.Book)
                    .Sum(b => b.Price * b.Copies)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name)
                .ToList();

            var result = new StringBuilder();

            foreach (var category in categories)
            {
                result.AppendLine($"{category.Name} ${category.TotalProfit:F2}");
            }

            return result.ToString().TrimEnd();
        }

        //Task 13
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks
                    .OrderByDescending(cb => cb.Book.ReleaseDate)
                    .Select(cb => new
                    {
                        BookTitle = cb.Book.Title,
                        ReleaseYear = cb.Book.ReleaseDate
                    })
                    .Take(3)
                })
                .OrderBy(c => c.Name)
                .ToList();
                




            var result = new StringBuilder();

            foreach (var category in categories)
            {
                result.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    result.AppendLine($"{book.BookTitle} ({book.ReleaseYear.Value.Year})");
                }
            }

            return result.ToString().TrimEnd();
        }

        //Task 14
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //Task 15
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            var removedBooksCount = books.Count();

            foreach (var book in books)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();

            return removedBooksCount;
        }
    }
}
