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
        public IEnumerable<Book> GetBooks(out int totalRecords, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {

            using (var db = new Data.MVCGridDemoEntities())
            {

            }

            totalRecords = 0;
            return new List<Book>();
        }
    }
}
