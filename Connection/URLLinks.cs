using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class URLLinks<T> where T : class
    {
        private string GetName { get; set; }
        public HttpClient UriPath { get; set; }
        private string host;

        public URLLinks(string host)
        {
            UriPath = new HttpClient();
            GetName = GetClassName();
            this.host = host;
            UrlChoice();
        }
        private void UrlChoice()
        {
                string url = host+ "api/" + GetName;
                UriPath.BaseAddress = (new Uri(url));
            
        }
       
        private string GetClassName()
        {
            var r = typeof(T).Name;
            return r.ToString();
        }
    }
}
