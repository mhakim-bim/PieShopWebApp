﻿
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace PieShop.TagHelpers
{
    [HtmlTargetElement("ul", Attributes = nameof(Condition))]
    public class ConditionTagHelper : TagHelper
    {
        public bool Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            
            if (!Condition)
            {
                output.SuppressOutput();
            }

        }
    }
}
