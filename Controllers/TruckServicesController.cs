using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckAPI.Models;
using TruckAPI.Repository;
using TruckAPI.Repository.TruckServiceRepo;

namespace TruckAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckServicesController : ControllerBase
    {
        private readonly IServiceRepo _context;
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TruckServicesController));

        public TruckServicesController(IServiceRepo context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public IEnumerable<Service> GetServices()
        {
            try
            {
                _log4net.Info("Get request for all Services");
                var result = _context.GetServiceDetails();
                return result;
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured in getting all services " + e.Message);

                throw;
            }
        }

        

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  IActionResult PutService(int id, Service service)
        {
            try
            {
                _log4net.Info("Get request for edit Service with id " + id);
                int result = _context.UpdateServiceDetails(id, service);
                if (result == 400)
                {
                    return BadRequest();
                }
                if (result == 404)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured in editing service " + e.Message);

                throw;
            }
        }

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostService(Service service)
        {
            try
            {
                _log4net.Info("Request to Add Service");

                return Ok(_context.InsertServiceDetail(service));
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured in adding service " + e.Message);

                throw;
            }
        }
        [HttpGet("{Id}")]
        public IActionResult GetUserByMobile(int Id)
        {
            try
            {
                _log4net.Info("Request to get request with service Id " + Id);

                List<Request> result = _context.BookingRequest(Id).ToList();
                if (result != null)
                {
                    return Ok(result);
                }
                result = new List<Request>();
                return Ok(result);
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured in getting service request " + e.Message);

                throw;
            }
        }

    }
}
