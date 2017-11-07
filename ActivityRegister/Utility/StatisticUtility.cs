using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ActivityRegister.Utility
{
    public class StatisticUtility
    {
        public string MacAddress { get; set; }
        public string[] MachineAndUserName { get; set; }
        public string RequestType { get; set; }
        public string MethodName { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string Error { get; set; }

        public string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            { 
                if (sMacAddress == String.Empty)
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            MacAddress = sMacAddress;
            return sMacAddress;
        }
        public string[] GetMachineName()
        {
            var machineName=Environment.MachineName;
            var user = Environment.UserName;
            MachineAndUserName = new string[2] { machineName, user };

            return new string[2]{ "Machine Name "+machineName,"UserName "+ user };
        }
        public string GetRequestType(HttpMethod method)
        {
            var res = method.Method;
            RequestType = res;

            return res;
        }
        public string GetMethodName(string name)
        {
            MethodName = name;

            return name;
        }
        public DateTime GetDateOfRequest(DateTime date)
        {
            DateOfRequest = date;
            return date;
        }
        public string GetError(string err)
        {
            Error = err;
            return err;
        }
    }
}
