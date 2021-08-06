using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckAPI.Models
{
    public class STruck
    {
        public string TruckNumber { get; set; }
        public string TruckType { get; set; }
        public int ManagerId { get; set; }
        public string DriverName { get; set; }
        public string DriverLicenceNumber { get; set; }
        public string PickCity { get; set; }
        public string DropCity { get; set; }
        public bool TruckStatus { get; set; }
        public int Cost { get; set; }
    }
}
