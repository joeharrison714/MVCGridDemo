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

                    cols.Add("Edit", "Edit", m => m.Index.ToString()).WithSorting(true)
                        .WithValueTemplate("<a href='/edit/{Value}' class='btn btn-primary' role='button'>{Model.Author}</a>", false);

                    // Add your columns here
                    //cols.Add().WithColumnName("UniqueColumnName")
                    //    .WithHeaderText("Any Header")
                    //    .WithValueExpression(i => i.YourProperty); // use the Value Expression to return the cell text for this column
                    //cols.Add().WithColumnName("UrlExample")
                    //    .WithHeaderText("Edit")
                    //    .WithValueExpression((i, c) => c.UrlHelper.Action("detail", "demo", new { id = i.Id }));
                })
                .WithRowCssClassExpression(b => {
                    if (b.Volume > 3000000)
                    {
                        return "success";
                    }
                    return "";
                })
                .WithRetrieveDataMethod((context) =>
                {
                    // Query your data here. Obey Ordering, paging and filtering parameters given in the context.QueryOptions.
                    // Use Entity Framework, a module from your IoC Container, or any other method.
                    // Return QueryResult object containing IEnumerable<YouModelItem>

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