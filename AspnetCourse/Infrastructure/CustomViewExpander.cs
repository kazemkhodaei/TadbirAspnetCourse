using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCourse.Infrastructure
{
    public class CustomViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // do nothing - not required
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            foreach (string location in viewLocations)
            {
                yield return location.Replace("Shared", "Common");
            }

            yield return "/Views/Legacy/{1}/{0}/View.cshtml";
        }
    }
}
