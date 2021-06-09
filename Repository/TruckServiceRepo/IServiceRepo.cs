using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckAPI.Models;

namespace TruckAPI.Repository.TruckServiceRepo
{
    public interface IServiceRepo
    {
        string InsertServiceDetail(Service service);
        ActionResult<IEnumerable<Service>> GetServiceDetails();
        int UpdateServiceDetails(int id, Service service);
        bool ServiceExists(int id);
    }
}
