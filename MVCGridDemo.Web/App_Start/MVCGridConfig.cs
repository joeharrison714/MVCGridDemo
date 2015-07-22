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
                .WithRenderingMode(RenderingMode.Controller)
                .WithViewPath("~/Views/MVCGrid/_Custom.cshtml")
                .WithSorting(true, "Title")
                .WithPaging(true, 10, true, 100)
                .WithFiltering(true)
                .AddColumns(cols =>
                {
                    cols.Add("Position", "Position", m => m.Position.ToString())
                        .WithSorting(true).WithVisibility(true, true);

                    cols.Add("Title", "Title", m => m.Title)
                        .WithSorting(true).WithFiltering(true).WithVisibility(true, true);

                    cols.Add("Author", "Author", m => m.Author)
                        .WithSorting(true).WithVisibility(true, true);

                    cols.Add("ISBN", "ISBN", m => m.ISBN)
                        .WithSorting(true).WithVisibility(false, true);

                    cols.Add("PublisherGroup", "Publisher Group", m => m.PublisherGroup)
                        .WithSorting(true).WithVisibility(false, true);

                    // Format Volume as Number
                    cols.Add("Volume", "Volume", m => m.Volume.ToString("N0"))
                        .WithSorting(true).WithVisibility(false, true);

                    cols.Add("PublicationDate", "Publ. Date", m => m.PublicationDate.ToShortDateString())
                        .WithSorting(true).WithVisibility(false, true);

                    // Use Value Template to format buttons
                    cols.Add("Edit", "Edit", m => m.Index.ToString()).WithSorting(false)
                        .WithValueTemplate("<a href='/edit/{Value}' class='btn btn-primary' role='button'>Edit</a>", false);

                    cols.Add("Delete", "Delete", m => m.Index.ToString()).WithSorting(false)
                        .WithValueTemplate("<a href='/edit/{Value}' class='btn btn-danger' role='button'>Delete</a>", false);


                })
                .WithRowCssClassExpression(b =>
                {
                    if (b.PublicationDate.Year >= (DateTime.Now.Year - 10))
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

                    IBookService bookService = DependencyResolver.Current.GetService<IBookService>();

                    var options = context.QueryOptions;

                    string titleFilter = options.GetFilterString("Title");

                    int totalRecords;
                    var results = bookService.GetBooks(out totalRecords, titleFilter, options.GetLimitOffset(), options.GetLimitRowcount(), options.SortColumnName, options.SortDirection == SortDirection.Dsc);

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