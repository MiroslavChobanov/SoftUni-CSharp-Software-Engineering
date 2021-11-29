namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    Books = a.AuthorsBooks
                        .Select(ab => ab.Book)
                        .OrderByDescending(b => b.Price)
                        .Select(b => new
                        {
                            BookName = b.Name,
                            BookPrice = string.Format("f2", b.Price)
                        })
                        .ToArray()
                })
                .ToArray()
                .OrderByDescending(a => a.Books.Length)
                .ThenBy(a => a.AuthorName)
                .ToArray();


            var authorsJson = JsonConvert.SerializeObject(authors, Formatting.Indented);

            return authorsJson;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        { 

            var books = context.Books
                .Where(b => b.PublishedOn < date && b.Genre == Genre.Science)
                .ToArray()
                .OrderByDescending(b => b.Pages)
                .ThenByDescending(b => b.PublishedOn)
                .Take(10)
                .Select(b => new ExportBookDto
                {
                    Name = b.Name,
                    Pages = b.Pages,
                    PublishedOn = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .ToArray();

            var writer = new StringWriter();

            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var serializer = new XmlSerializer(typeof(ExportBookDto[]), new XmlRootAttribute("Books"));
            serializer.Serialize(writer, books, ns);

            var booksXml = writer.GetStringBuilder();

            return booksXml.ToString().TrimEnd();
        }
    }
}