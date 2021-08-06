﻿using System;
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
        public DateTime BookingDate { get; set; }
        public bool ServiceStatus { get; set; }
        public bool BookingStatus { get; set; }
        public string PickupCity { get; set; }
        public string DropCity { get; set; }
        public int ServiceCost { get; set; }






    }
}
