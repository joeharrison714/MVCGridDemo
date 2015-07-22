using MVCGridDemo.Interfaces;
using MVCGridDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCGridDemo.Services
{
    public class BookService : IBookService
    {
        public IEnumerable<Book> GetBooks(out int totalRecords, int? limitOffset, int? limitRowCount, string orderByColumn, bool desc)
        {
            return GetBooks(out totalRecords, null, limitOffset, limitRowCount, orderByColumn, desc);
        }

        public IEnumerable<Book> GetBooks(out int totalRecords, string titleFilter, int? limitOffset, int? limitRowCount, string orderByColumn, bool desc)
        {
            List<Book> results = new List<Book>();

            using (var db = new Data.MVCGridDemoEntities())
            {
                var query = db.Books.AsQueryable();

                if (!String.IsNullOrWhiteSpace(titleFilter))
                {
                    query = query.Where(p => p.Title.Contains(titleFilter));
                }
                //if (!String.IsNullOrWhiteSpace(filterLastName))
                //{
                //    query = query.Where(p => p.LastName.Contains(filterLastName));
                //}
                //if (filterActive.HasValue)
                //{
                //    query = query.Where(p => p.Active == filterActive.Value);
                //}

                //if (!String.IsNullOrWhiteSpace(globalSearch))
                //{
                //    query = query.Where(p => (p.FirstName + " " + p.LastName).Contains(globalSearch));
                //}

                totalRecords = query.Count();

                BookOrderBy? orderBy = null;

                BookOrderBy tempOrderBy;
                if (Enum.TryParse<BookOrderBy>(orderByColumn, true, out tempOrderBy))
                {
                    orderBy = tempOrderBy;
                }

                if (orderBy.HasValue)
                {
                    switch (orderBy.Value)
                    {
                        case BookOrderBy.Author:
                            if (!desc)
                                query = query.OrderBy(p => p.Author);
                            else
                                query = query.OrderByDescending(p => p.Author);
                            break;
                        case BookOrderBy.BindingTypeId:
                            if (!desc)
                                query = query.OrderBy(p => p.BindingTypeId);
                            else
                                query = query.OrderByDescending(p => p.BindingTypeId);
                            break;
                        case BookOrderBy.Imprint:
                            if (!desc)
                                query = query.OrderBy(p => p.Imprint);
                            else
                                query = query.OrderByDescending(p => p.Imprint);
                            break;
                        case BookOrderBy.Index:
                            if (!desc)
                                query = query.OrderBy(p => p.Index);
                            else
                                query = query.OrderByDescending(p => p.Index);
                            break;
                        case BookOrderBy.ISBN:
                            if (!desc)
                                query = query.OrderBy(p => p.ISBN);
                            else
                                query = query.OrderByDescending(p => p.ISBN);
                            break;
                        case BookOrderBy.Position:
                            if (!desc)
                                query = query.OrderBy(p => p.Position);
                            else
                                query = query.OrderByDescending(p => p.Position);
                            break;
                        case BookOrderBy.PublicationDate:
                            if (!desc)
                                query = query.OrderBy(p => p.PublDate);
                            else
                                query = query.OrderByDescending(p => p.PublDate);
                            break;
                        case BookOrderBy.PublisherGroup:
                            if (!desc)
                                query = query.OrderBy(p => p.PublisherGroup);
                            else
                                query = query.OrderByDescending(p => p.PublisherGroup);
                            break;
                        case BookOrderBy.Title:
                            if (!desc)
                                query = query.OrderBy(p => p.Title);
                            else
                                query = query.OrderByDescending(p => p.Title);
                            break;
                        case BookOrderBy.Volume:
                            if (!desc)
                                query = query.OrderBy(p => p.Volume);
                            else
                                query = query.OrderByDescending(p => p.Volume);
                            break;
                    }
                }


                if (limitOffset.HasValue)
                {
                    query = query.Skip(limitOffset.Value).Take(limitRowCount.Value);
                }

                foreach (var dbBook in query)
                {
                    Book book = new Book()
                    {
                        Author = dbBook.Author,
                        BindingTypeId = dbBook.BindingTypeId.Value,
                        Imprint = dbBook.Imprint,
                        Index = dbBook.Index,
                        ISBN = dbBook.ISBN,
                        Position = dbBook.Position.Value,
                        PublicationDate = dbBook.PublDate.Value,
                        PublisherGroup = dbBook.PublisherGroup,
                        Title = dbBook.Title,
                        Volume = dbBook.Volume.Value
                    };
                    results.Add(book);
                }
            }

            return results;
        }
    }
}
