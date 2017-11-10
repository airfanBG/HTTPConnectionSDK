using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityRegister.Models
{
    public class EntityStatistic : IEntity
    {
        public int Id { get; set; }
        public string RequestType { get; set; }
        public string RequestModel { get; set; }
        public string ComputerName { get; set; }
        public string MachineId { get; set; }
        public DateTime DateOfRequest { get; set; }
        public string Error { get; set; }
        public string UserName { get; set; }
    }
}
