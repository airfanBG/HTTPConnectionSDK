using ActivityRegister.Utility;
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
           
            BaseConnection<Clients> conn = new BaseConnection<Clients>(cl, "dd", "123456");


            Clients client = new Clients();
            client.Name = "Airfan1234";
            ClientEquipments ew = new ClientEquipments();
            ew.Id = 1;
            ew.Model = "inte";
            ew.SerialModel = "2322";
            ew.TradeMark = "zzzz";

            client.Address = "mizia";

            client.DateOfLastCheck = DateTime.Now;
            client.Town = "veliko tarnovo";
            client.ClientEquipments = new List<ClientEquipments>() { ew };
            client.isChecked = false;
            client.DateForCheck = DateTime.Now;
            client.CreatedOn = DateTime.Now;

            client.ApplicationUserID = "aea8cc7e-95e9-4049-ab59-189f9c522758";
            client.ClientToken = Guid.NewGuid().ToString();
            //var t = conn.GetAll("Clients").Result;
            //var r = JsonConvert.DeserializeObject<IEnumerable<Clients>>(t);
            //conn.PostClient(client);
            //conn.Delete("f2f4f43a-1e0e-461a-9707-30ea818187e9");
            conn.PutClient(18, client);
            //StatisticUtility st = new StatisticUtility();
            //var t = st.GetMachineName();
            //var mac = st.GetMacAddress();
            //conn.Delete(18);
        }
    }
}
