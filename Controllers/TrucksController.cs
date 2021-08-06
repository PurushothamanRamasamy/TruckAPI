using Microsoft.AspNetCore.Mvc;
using System;
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
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TrucksController));

        public TrucksController(ITruckRepo context)
        {
            _context = context;
        }

        // GET: api/Trucks
        [HttpGet]
        public  IEnumerable<Truck> GetTrucks()
        {
            
            try
            {
                _log4net.Info("Get request for all trucks");

                return _context.GetTruckDetails();
            }
            catch (System.Exception e)
            {
                _log4net.Error("Error Occured in getting all trucks"+ e.Message);
                throw;

            }
        }
        [HttpGet("pick:{Pickcity}/Drop:{Dropcity}/Type:{TruckType}/BDate:{Bookingdate}/approxDistance:{distance}")]
        public IEnumerable<STruck> SearchTrucks(string Pickcity, string Dropcity, string TruckType, DateTime Bookingdate,int distance)
        {

            try
            {
                _log4net.Info("Get request for Search trucks");

                return _context.SearchTruck(Pickcity, Dropcity, TruckType, Bookingdate,distance);
            }
            catch (System.Exception e)
            {
                _log4net.Error("Error Occured in Searching  trucks"+ e.Message);
                throw;

            }
        }


        // PUT: api/Trucks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutTruck(string id, Truck truck)
        {
            try
            {
                _log4net.Info("Requesting for edit truck having Truck Number " + id);

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
            catch (System.Exception e)
            {
                _log4net.Error("Error Occured in editing trucks" + e.Message);
                throw;
            }
        }

        // POST: api/Trucks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Message> PostTruck(Truck truck)
        {

            try
            {
                _log4net.Info("Request to Add truck with Truck number " + truck.TruckNumber);
                Message message = new();
                message.message = _context.InsertTruckDetail(truck);
                return message;
            }
            catch (System.Exception e)
            {
                _log4net.Error("Error Occured in adding truck" + e.Message);
                throw;
            }

        }
        [HttpDelete("{truckNumber}")]
        public ActionResult<Message> DeleteTruck(string truckNumber)
        {
            try
            {
                _log4net.Info("Request to Delete truck with Truck number " + truckNumber);
                Message message = new();
                message.message = _context.DeleteTruck(truckNumber);
                return message;
            }
            catch (System.Exception e)
            {
                _log4net.Error("Error Occured in Deleting truck" + e.Message);
                throw;
            }


        }
        [HttpGet("{truckNumber}")]
        public IActionResult GetTruckByTruckNumber(string truckNumber)
        {
            try
            {
                _log4net.Info("Request to get truck with Truck number " + truckNumber);
                Truck result = _context.GetTruck(truckNumber);
                if (result != null)
                {
                    return Ok(result);
                }
                result = new Truck();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                _log4net.Error("Error Occured in Deleting truck" + e.Message);
                throw;
            }
        }


    }
}
