using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Parse.Ps4Disk
{
    internal class PS4DiskParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("h4");
            var items2 = document.QuerySelectorAll("p").Where(item => item.ClassName != null && item.ClassName.Contains("price"));
            for(int i=0;i < items2.Count(); i++)
            {
                list.Add(items[i].TextContent);
            }

            foreach (var item in items2)
            {
                if(item.TextContent != "НАШ INSTAGRAM")
                list.Add(item.TextContent);
            }
           return list.ToArray();
        }
    }
}
