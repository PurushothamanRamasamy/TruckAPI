using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TruckAPI.Models;
using TruckAPI.Repository;

namespace TruckAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckRepo _context;


        public TrucksController(ITruckRepo context)
        {
            _context = context;
        }

        // GET: api/Trucks
        [HttpGet]
        public  ActionResult<IEnumerable<Truck>> GetTrucks()
        {
            return  _context.GetTruckDetails();
        }

        

        // PUT: api/Trucks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutTruck(string id, Truck truck)
        {
            int result = _context.UpdateTruckDetails(id, truck);
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

        // POST: api/Trucks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Message> PostTruck(Truck truck)
        {

            Message message = new();
            message.message = _context.InsertTruckDetail(truck);
            return message;

        }
        [HttpDelete("{truckNumber}")]
        public ActionResult<Message> DeleteTruck(string truckNumber)
        {

            Message message = new();
            message.message = _context.DeleteTruck(truckNumber);
            return message;

        }
        [HttpGet("{truckNumber}")]
        public IActionResult GetUserByMobile(string truckNumber)
        {
            Truck result = _context.GetTruck(truckNumber);
            if (result != null)
            {
                return Ok(result);
            }
            result = new Truck();
            return Ok(result);
        }


    }
}
