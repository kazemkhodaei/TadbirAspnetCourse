using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCourse.Infrastructure
{
    [ViewComponent(Name = "SimpleViewComponent2")]
    public class SimpleViewComponent2 : ViewComponent
    {
        public SimpleViewComponent2()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("MenuView", 2 * 2));
        }
    }
}
