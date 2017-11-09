using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ActivityRegister.Utility
{
    public interface IStatistic<T> where T:class
    {
        string MacAddress { get; set; }
        string[] MachineAndUserName { get; set; }
        string RequestType { get; set; }
        string MethodName { get; set; }
        DateTime DateOfRequest { get; set; }
        string Error { get; set; }

        string GetMacAddress();
        string[] GetMachineName();
        string GetRequestType(HttpMethod method);
        string GetMethodName(string name);
        DateTime GetDateOfRequest(DateTime date);
        string GetError(string err);
    }
}
