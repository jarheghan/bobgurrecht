using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RepositoryPattern.Service.UI
{
    public static class LayoutHelpers
    {
        public static void Repeater<T>(this HtmlHelper html
                              , IEnumerable<T> items
                              , string className
                              , string classNameAlt
                                        , Action<T, string> render)
        {
            if (items == null)
            return;

            int i = 0;
            items.ForEach(item =>
            {
                render(item, (i++ % 2 == 0) ? className : classNameAlt);
            });
        }

        public static void Repeater<T>(this HtmlHelper html
          , string viewDataKey
          , string cssClass
          , string altCssClass
          , Action<T, string> render)
        {
            var items = GetViewDataAsEnumerable<T>(html, viewDataKey);

            int i = 0;
            items.ForEach(item =>
            {
                render(item, (i++ % 2 == 0) ? cssClass : altCssClass);
            });
        }

        static IEnumerable<T> GetViewDataAsEnumerable<T>(HtmlHelper html, string viewDataKey)
        {
            var items = html.ViewContext.ViewData as IEnumerable<T>;
            var viewData = html.ViewContext.ViewData as IDictionary<string, object>;
            if (viewData != null)
            {
                items = viewData[viewDataKey] as IEnumerable<T>;
            }
            else
            {
                items = new ViewPage().ViewContext.ViewData[viewDataKey]
                  as IEnumerable<T>;
            }
            return items;
        }


        public static void ForEach<T>(
                        this IEnumerable<T> source,
                        Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

    }
}
