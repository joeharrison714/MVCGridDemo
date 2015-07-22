using MVCGridDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCGridDemo.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks(out int totalRecords, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
        IEnumerable<Book> GetBooks(out int totalRecords, string titleFilter, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
    }
}
