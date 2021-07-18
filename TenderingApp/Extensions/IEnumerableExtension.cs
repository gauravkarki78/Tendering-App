using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using TenderingApp.Data;

namespace TenderingApp.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, string selectedValue)
        {

            //foreach (var a in items)
            //{
            //    Console.WriteLine(a.GetPropertyValue("CategoryName"));
            //}
            return from item in items
                   select new SelectListItem
                   {
                       Value = item.GetPropertyValue("CategoryName"),
                       Text = item.GetPropertyValue("CategoryName"),
                       Selected = item.GetPropertyValue("CategoryName").Equals(selectedValue)
                   };

        }

    }
}
