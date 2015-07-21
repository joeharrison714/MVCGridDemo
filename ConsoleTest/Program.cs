using MVCGridDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BookService bookService = new BookService();

            int totalRecords;
            var books = bookService.GetBooks(out totalRecords, null, null, MVCGridDemo.Model.BookOrderBy.Author, false);

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }
    }
}
