using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityRegister.Models
{
    public interface IEntity<T> where T: class

    {
        int Id { get; set; }
        string RequestType { get; set; }
        string RequestModel { get; set; }
        Entity<T> MyProperty { get; set; }
        string ComputerName { get; set; }
        string MachineId { get; set; }
        DateTime DateOfRequest { get; set; }
        string Error { get; set; }
        string UserName { get; set; }
    }
}
