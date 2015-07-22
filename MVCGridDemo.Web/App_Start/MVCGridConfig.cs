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
    using MVCGridDemo.Model;
    using MVCGridDemo.Interfaces;

    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {
            
            MVCGridDefinitionTable.Add("BookGrid", new MVCGridBuilder<Book>()
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .WithSorting(true, "Title")
                .WithPaging(true, 10, true, 100)
                .AddColumns(cols =>
                {
                    cols.Add("Title", "Title", b => b.Title).WithSorting(true);
                    cols.Add("Author", "Author", b => b.Author).WithSorting(true);

                    // Add your columns here
                    //cols.Add().WithColumnName("UniqueColumnName")
                    //    .WithHeaderText("Any Header")
                    //    .WithValueExpression(i => i.YourProperty); // use the Value Expression to return the cell text for this column
                    //cols.Add().WithColumnName("UrlExample")
                    //    .WithHeaderText("Edit")
                    //    .WithValueExpression((i, c) => c.UrlHelper.Action("detail", "demo", new { id = i.Id }));
                })
                .WithRetrieveDataMethod((context) =>
                {
                    // Query your data here. Obey Ordering, paging and filtering parameters given in the context.QueryOptions.
                    // Use Entity Framework, a module from your IoC Container, or any other method.
                    // Return QueryResult object containing IEnumerable<YouModelItem>

                    IBookService bookService = DependencyResolver.Current.GetService<IBookService>();

                    var options = context.QueryOptions;

                    int totalRecords;
                    var results = bookService.GetBooks(out totalRecords, options.GetLimitOffset(), options.GetLimitRowcount(), options.SortColumnName, options.SortDirection == SortDirection.Dsc);

                    return new QueryResult<Book>()
                    {
                        Items = results,
                        TotalRecords = totalRecords // if paging is enabled, return the total number of records of all pages
                    };

                })
            );
            
        }
    }
}