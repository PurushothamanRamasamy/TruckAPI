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

        public TruckServicesController(IServiceRepo context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            var result = _context.GetServiceDetails();
            return result;
        }

        

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public  IActionResult PutService(int id, Service service)
        {
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostService(Service service)
        {
            service.ServiceDate = DateTime.Today;
            return Ok(_context.InsertServiceDetail(service));
        }

       
    }
}
