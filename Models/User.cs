using System;
using System.Collections.Generic;

#nullable disable

namespace TruckAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public bool? UserStatus { get; set; }
        public string Password { get; set; }

    }
}
