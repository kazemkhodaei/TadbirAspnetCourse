using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCourse.Infrastructure
{
    [ViewComponent(Name = "SimpleViewComponent")]
    public class SimpleViewComponent : ViewComponent
    {
        public SimpleViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("MenuView", 2 * 2));
        }
    }
}
