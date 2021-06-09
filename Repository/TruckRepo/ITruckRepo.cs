using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using TruckAPI.Models;

namespace TruckAPI.Repository
{
    public interface ITruckRepo
    {
         

        string InsertTruckDetail(Truck truck);
        ActionResult<IEnumerable<Truck>> GetTruckDetails();
        int UpdateTruckDetails(string id, Truck truck);
        bool TruckExists(string id);

        Truck GetTruck(string truckNumber);
    }
}
