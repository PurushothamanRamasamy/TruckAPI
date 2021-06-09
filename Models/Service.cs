using System;
using System.Collections.Generic;

#nullable disable

namespace TruckAPI.Models
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }
        public int ManagerId { get; set; }
        public string TruckNumber { get; set; }
        public string PickandDropCity { get; set; }
        public DateTime ServiceDate { get; set; }
        public bool ServiceStatus { get; set; }

       
    }
}
