using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckAPI.Models;

namespace TruckAPI.Repository.TruckServiceRepo
{
    public class ServiceRepo:IServiceRepo
    {
        private readonly TruckAppDBContext _context;

        public ServiceRepo(TruckAppDBContext context)
        {
            _context = context;
        }
        public ActionResult<IEnumerable<Service>> GetServiceDetails()
        {
            return _context.Services.ToList<Service>();
        }


        public string InsertServiceDetail(Service service)
        {
            try
            {
                _context.Services.Add(service);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return "Data sucessfully added";
        }
        public bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceId == id);

        }
        public int UpdateServiceDetails(int id, Service service)
        {
            if (id != service.ServiceId)
            {
                return 400;//bad request
            }
            _context.Entry(service).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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
