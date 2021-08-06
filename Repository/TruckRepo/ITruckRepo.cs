using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using TruckAPI.Models;

namespace TruckAPI.Repository
{
    public interface ITruckRepo
    {
         

        string InsertTruckDetail(Truck truck);
        IEnumerable<Truck> GetTruckDetails();
        int UpdateTruckDetails(string id, Truck truck);
        bool TruckExists(string id);
        IEnumerable<STruck> SearchTruck(string Pickcity, string Dropcity, string TruckType, DateTime Rdate, int distance);
        Truck GetTruck(string truckNumber);

        string DeleteTruck(string truckNumber);
    }
}
