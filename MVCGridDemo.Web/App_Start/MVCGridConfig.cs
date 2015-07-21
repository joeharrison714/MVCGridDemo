[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVCGridDemo.Web.MVCGridConfig), "RegisterGrids")]

namespace MVCGridDemo.Web
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using MVCGrid.Models;
    using MVCGrid.Web;
    using MVCGridDemo.Interfaces;
    using MVCGridDemo.Model;

    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {
            
            MVCGridDefinitionTable.Add("BookGrid", new MVCGridBuilder<Book>()
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .WithPaging(true, 10)
                .WithSorting(true, "Title")
                .WithFiltering(true)
                .AddColumns(cols =>
                {
                    cols.Add("Position", "Position", m => m.Position.ToString()).WithSorting(true);
                    cols.Add("Title", "Title", m => m.Title).WithSorting(true).WithFiltering(true);
                    cols.Add("Author", "Author", m => m.Author).WithSorting(true);
                    cols.Add("ISBN", "ISBN", m => m.ISBN).WithSorting(true);
                    cols.Add("PublisherGroup", "Publisher Group", m => m.PublisherGroup).WithSorting(true);
                    cols.Add("Volume", "Volume", m => m.Volume.ToString("N0")).WithSorting(true);
                    cols.Add("PublicationDate", "Publ. Date", m => m.PublicationDate.ToShortDateString()).WithSorting(true);

                    cols.Add("Edit", "Edit", m => m.Index.ToString()).WithSorting(false)
                        .WithValueTemplate("<a href='/edit/{Value}' class='btn btn-primary' role='button'>Edit</a>", false);

                    cols.Add("Delete", "Delete", m => m.Index.ToString()).WithSorting(false)
                        .WithValueTemplate("<a href='/edit/{Value}' class='btn btn-danger' role='button'>Delete</a>", false);
                })
                .WithRowCssClassExpression(b => {
                    if (b.PublicationDate.Year >= (DateTime.Now.Year - 10))
                    {
                        return "success";
                    }
                    return "";
                })
                .WithRetrieveDataMethod((context) =>
                {
                    var bookService = DependencyResolver.Current.GetService<IBookService>();

                    BookOrderBy orderBy;
                    Enum.TryParse<BookOrderBy>(context.QueryOptions.SortColumnName, true, out orderBy);
                    bool sortDescending = context.QueryOptions.SortDirection == SortDirection.Dsc;

                    var titleFilter = context.QueryOptions.GetFilterString("Title");

                    int totalRecords;
                    var books = bookService.GetBooks(out totalRecords, titleFilter, context.QueryOptions.GetLimitOffset(),
                        context.QueryOptions.GetLimitRowcount(), orderBy, sortDescending);

                    return new QueryResult<Book>()
                    {
                        Items = books,
                        TotalRecords = totalRecords // if paging is enabled, return the total number of records of all pages
                    };

                })
            );
            
        }
    }
}