using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ActivityRegister.Utility
{
    public class StatisticUtility
    {

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
            return sMacAddress;
        }
        public string[] GetMachineName()
        {
            var machineName=Environment.MachineName;
            var user = Environment.UserName;

            return new string[2]{ "Machine Name "+machineName,"UserName "+ user };
        }
    }
}
