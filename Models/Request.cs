using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruckAPI.Models
{
    public class Request
    {
        public int ServiceId { get; set; }

        public string TruckNumber { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public string PickupCity { get; set; }
        public string DropCity { get; set; }
        public int ServiceCost { get; set; }

    }
}
