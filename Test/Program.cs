using Connection;
using Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient cl = new HttpClient();
            cl.BaseAddress = new Uri("http://localhost:52281");

            BaseConnection<Clients> conn = new BaseConnection<Clients>(cl, "dd", "123456oo");

            var t = conn.GetAll().Result;
            var r = JsonConvert.DeserializeObject<IEnumerable<Clients>>(t);
            var tgfgd = conn.GetAll().Result;
            char a = 'd';
        }
    }
}
