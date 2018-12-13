using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCourse.Infrastructure
{
    [HtmlTargetElement(Attributes = "bs-for")]
    public class ButtonTagHelper : TagHelper
    {
        public string BsButtonColor { get; set; }
        public ModelExpression BsFor { get; set; }

        [HtmlAttributeNotBound]
        public ModelExpression BsFor2 { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //output.TagName = "p";
            //output.PreElement.SetContent("everything");
            // output.Attributes.SetAttribute("class", $"btn btn-{BsButtonColor}");
            output.Content.Append("default content");

            //output.Content.AppendHtml((output.GetChildContentAsync(false)).Result.GetContent());
        }
    }
}
