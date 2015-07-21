using MVCGridDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCGridDemo.Services
{
    public class BookService
    {
        public IEnumerable<Book> GetBooks(out int totalRecords, int? limitOffset, int? limitRowCount, BookOrderBy orderBy, bool desc)
        {

            using (var db = new Data.MVCGridDemoEntities())
            {
                foreach (var b in db.Books)
                {
                    Console.WriteLine(b.Title);
                }
            }

            totalRecords = 0;
            return new List<Book>();
        }
    }
}
