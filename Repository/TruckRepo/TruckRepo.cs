using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TruckAPI.Models;
using static System.Formats.Asn1.AsnWriter;

namespace TruckAPI.Repository
{
    public class TruckRepo : ITruckRepo
    {
        private readonly TruckAppDBContext _context;
        public TruckRepo(TruckAppDBContext context)
        {
            _context = context;
        }

        public string DeleteTruck(string truckNumber)
        {
            var truckdelete = _context.Trucks.Find(truckNumber);
            if (truckdelete!=null)
            {
                _context.Trucks.Remove(truckdelete);
                _context.SaveChanges();
                return truckdelete.TruckNumber + " successfully deleted";
            }
            return truckNumber + " Not found";
        }

        public Truck GetTruck(string truckNumber)
        {
            Truck result = _context.Trucks.FirstOrDefault(e => e.TruckNumber == truckNumber);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public ActionResult<IEnumerable<Truck>> GetTruckDetails()
        {
            return _context.Trucks.ToList<Truck>();
        }
        public string InsertTruckDetail(Truck truck)
        {
            try
            {
                _context.Trucks.Add(truck);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return truck.TruckNumber + " data sucessfully added";
        } 
        public bool TruckExists(string id)
        {
            return _context.Trucks.Any(e => e.TruckNumber == id);

        }

        public int UpdateTruckDetails(string id, Truck truck)
        {
            if (id != truck.TruckNumber)
            {
                return 400;//bad request
            }
            _context.Entry(truck).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
                {
                    return 404;//not found
                }
                else
                {
                    throw;
                }
            }

            return 204;//success
        }

        

        
    }
}
