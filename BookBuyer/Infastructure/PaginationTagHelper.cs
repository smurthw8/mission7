using System;
using BookBuyer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookBuyer.Infastructure
{
    //inherit from tag helper class, add attribute and specify html element, and define attribute/class of that html element
    //so that it knows when to apply this tag helper
    [HtmlTargetElement("div", Attributes = "page-ref")]
    public class PaginationTagHelper : TagHelper
    {
        //dynamically create page links

        private IUrlHelperFactory uhf;

        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            uhf = temp;

        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        public PageInfo PageRef { get; set; }

        //will become a tag helper we can use to pass in page data from view
        public string PageAction { get; set; }

        //bootstrap adds
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        //override says instead of using parent class process, use this one
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");
            //loop through and create individual links

            for (int i = 1; i < PageRef.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                //bootstrap adds
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageRef.Currentpage
                        ? PageClassSelected : PageClassNormal);
                }

                tb.InnerHtml.Append(i.ToString());
                final.InnerHtml.AppendHtml(tb);
            }

            output.Content.AppendHtml(final.InnerHtml);
        }



    }
}
