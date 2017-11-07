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
            // conn.GetAccess();

            Clients client = new Clients();
            // //ClientEquipments ew = new ClientEquipments();
            // //ew.Id = 1;
            // //ew.Model = "inte";
            // //ew.SerialModel = "2322";
            // //ew.TradeMark = "zzzz";

            // client.Address = "ВТ";
            // client.Name = "dddfff";
            // client.DateOfLastCheck = DateTime.Now;

            // //client.ClientEquipments = new List<ClientEquipments>() {ew };
            // client.isChecked = false;
            // client.DateForCheck = DateTime.Now;
            // client.CreatedOn = DateTime.Now;
            // client.ApplicationUserID = "aea8cc7e-95e9-4049-ab59-189f9c522758";
            // client.ClientToken = Guid.NewGuid().ToString();
            //var t = conn.GetAll("Clients").Result;
            //var r = JsonConvert.DeserializeObject<IEnumerable<Clients>>(t);
            // conn.PostClient(client);
            var res=conn.GetByParam("1");

            //StatisticUtility st = new StatisticUtility();
            //var t = st.GetMachineName();
            //var mac = st.GetMacAddress();

        }
    }
}
