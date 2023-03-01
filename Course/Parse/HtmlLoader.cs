using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Course.Parse
{
    internal class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}{settings.Prefix}";
        }

        public async Task<string> GetSourseByPageId(int id)
        {
            var currentUrl = url.Replace("{CurrentId}", id.ToString());
            var responce = await client.GetAsync(currentUrl);

            string source = null;

            if(responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync();
            }
            return source;

        }

    }
}
