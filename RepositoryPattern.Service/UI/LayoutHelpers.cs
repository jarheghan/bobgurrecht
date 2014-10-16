using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace RepositoryPattern.Service.UI
{
    public delegate MvcHtmlString GetStringFromAction(string viewName, object model); 
    public static class LayoutHelpers
    {
        //public static void Repeater<T>(this HtmlHelper html
        //                      , IEnumerable<T> items
        //                      , string className
        //                      , string classNameAlt
        //                                , Action<T, string> render)
        //{
        //    if (items == null)
        //    return;

        //    int i = 0;
        //    //items.ForEach(item =>
        //    //{
        //    //    render(item, (i++ % 2 == 0) ? className : classNameAlt);
        //    //});
        //    foreach (var item in items)
        //    {
        //        render(item, (i++ % 2 == 0) ? className : classNameAlt);
        //    }
        //}

        //public static void Repeater<T>(this HtmlHelper html
        //  , string viewDataKey
        //  , string cssClass
        //  , string altCssClass
        //  , Action<T, string> render)
        //{
        //    var items = GetViewDataAsEnumerable<T>(html, viewDataKey);

        //    int i = 0;
        //    //items.ForEach(item =>
        //    //{
        //    //    render(item, (i++ % 2 == 0) ? cssClass : altCssClass);
        //    //});
        //    foreach (var item in items)
        //    {
        //        render(item, (i++ % 2 == 0) ? cssClass : altCssClass);
        //    }
        //}

        //static IEnumerable<T> GetViewDataAsEnumerable<T>(HtmlHelper html, string viewDataKey)
        //{
        //    var items = html.ViewContext.ViewData as IEnumerable<T>;
        //    var viewData = html.ViewContext.ViewData as IDictionary<string, object>;
        //    if (viewData != null)
        //    {
        //        items = viewData[viewDataKey] as IEnumerable<T>;
        //    }
        //    else
        //    {
        //        items = new ViewPage().ViewContext.ViewData[viewDataKey]
        //          as IEnumerable<T>;
        //    }
        //    return items;
        //}

        public static void Repeater<T>(this HtmlHelper html
                              , IEnumerable<T> items
                              , Action<T> render
                              , Action<T> renderAlt)
        {
            if (items == null)
                return;

            int i = 0;
            items.ForEach(item =>
            {
                if (i++ % 2 == 0)
                    render(item);
                else
                    renderAlt(item);
            });
        }

        public static void Repeater<T>(this HtmlHelper html
                                      , Action<T> render
                                      , Action<T> renderAlt)
        {
            var items = html.ViewContext.ViewData as IEnumerable<T>;
            html.Repeater(items, render, renderAlt);
        }

        public static void Repeater<T>(this HtmlHelper html
                                  , string viewDataKey
                                  , Action<T> render
                                  , Action<T> renderAlt)
        {
            var items = html.ViewContext.ViewData as IEnumerable<T>;
            var viewData = html.ViewContext.ViewData as IDictionary<string, object>;
            if (viewData != null)
            {
                items = viewData[viewDataKey] as IEnumerable<T>;
            }
            else
            {
                items = new ViewPage().ViewContext.ViewData[viewDataKey]as IEnumerable<T>;
            }
            html.Repeater(items, render, renderAlt);
        }

        public static void ForEach<T>(
                        this IEnumerable<T> source,
                        Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        public static MvcHtmlString RenderTemplateAsColumns(this HtmlHelper helper,
           ICollection items,
           string partialViewName,
           int numberOfColumns)
        {
            return RenderTemplateAsColumns(helper, items, partialViewName, numberOfColumns, helper.Partial);

        }
        public static MvcHtmlString RenderTemplateAsColumns(this HtmlHelper helper, ICollection items
            , string partialViewName
            , int numberOfColumns
            ,GetStringFromAction getStringMethod)
        {
            if (numberOfColumns < 1)
            {
                throw new ArgumentOutOfRangeException("numberOfColumns");
            }
            if (items == null)
            {
                throw new ArgumentOutOfRangeException("items");
            }
            StringBuilder builder = new StringBuilder();
            int columnsInRow = 0;
            int rowsDone = 0;
            int numberOfItemsDone = 0;
            int numberOfExtraColumnInLastRow;

            //calculate the needed table structure
            int numberOfRows = items.Count / numberOfColumns;

            //create the needed table tag
            builder.Append("<table>");

            //create the rows and columns
            foreach (var i in items)
            {
                if (columnsInRow == 1)
                {
                    builder.Append("<tr>");
                }
                builder.Append("<td>");
                builder.Append(getStringMethod(partialViewName, items));
                builder.Append("</td>");
                bool isLastItem = (items.Count == numberOfItemsDone + 1);

                if ((columnsInRow == numberOfColumns) || isLastItem)
                {
                    if (isLastItem)
                    {
                        numberOfExtraColumnInLastRow = numberOfColumns - columnsInRow;
                        builder.Append(RenderExtraColumns(numberOfExtraColumnInLastRow));
                    }
                    builder.Append("</tr>");
                    columnsInRow = 1;
                    rowsDone++;

                }
                else
                {
                    columnsInRow++;
                }
                builder.Append("</table>");
                return MvcHtmlString.Create(builder.ToString());
            }
            return MvcHtmlString.Empty;
        }

        private static string RenderExtraColumns(int numberOfExtraColumnsInLastRow)
        {
            if (numberOfExtraColumnsInLastRow > 0)
            { StringBuilder builder = new StringBuilder();
                for (int i = 0; i < numberOfExtraColumnsInLastRow; i++)
                { 
                    builder.Append("<td></td>"); 
                }
                return builder.ToString(); 
            } 
            return string.Empty;
        }

    }
}
