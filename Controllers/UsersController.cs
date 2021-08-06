using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckAPI.Models;
using TruckAPI.Repository.UserRepo;

namespace TruckAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _context;
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UsersController));

        public UsersController(IUserRepo context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public  IEnumerable<User> GetUsers()
        {
            try
            {
                _log4net.Info("Get request for all Services");
                var result = _context.GetUserDetails();
                return result;
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured in getting all users list" + e.Message);

                throw;
            }
            
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            try
            {
                _log4net.Info("Request to edit user with id "+id);

                string encryptdata = _context.Encryptdata(user.Password);
                user.Password = encryptdata;
                int result = _context.UpdateUserDetails(id, user);
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
                _log4net.Info("Error Occured in editing  user" + e.Message);

                throw;
            }
            
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public string PostUser(User user)
        {
            try
            {
                _log4net.Info("Request to add user with name " + user.UserName);

                string encryptdata = _context.Encryptdata(user.Password);
                user.Password = encryptdata;
                return _context.InsertUserDetail(user);
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured in editing  user" + e.Message);

                throw;
            }
                    
        }
        
        [HttpGet("{Mobile}")]
        public IActionResult GetUserByMobile(string Mobile)
        {
            try
            {
                _log4net.Info("Request to edit user with mobile " +Mobile);

                User result = _context.GetUser(Mobile);
                if (result != null)
                {
                    return Ok(result);
                }
                result = new User();
                return Ok(result);
            }
            catch (Exception e)
            {
                _log4net.Info("Error Occured in get user" + e.Message);

                throw;
            }
        }

    }
}
