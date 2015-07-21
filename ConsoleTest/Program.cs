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
            bookService.GetBooks(out totalRecords, null, null, MVCGridDemo.Model.BookOrderBy.Author, false);
        }
    }
}
