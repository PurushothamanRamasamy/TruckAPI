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

        public UsersController(IUserRepo context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public  ActionResult<IEnumerable<User>> GetUsers()
        {
            var result =  _context.GetUserDetails();
            return  result;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            string encryptdata =_context.Encryptdata(user.Password);
            user.Password = encryptdata;
            int result = _context.UpdateUserDetails(id, user);
            if(result==400)
            {
                return BadRequest();
            }
            if(result==404)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public string PostUser(User user)
        {
            string encryptdata = _context.Encryptdata(user.Password);
            user.Password = encryptdata;
            return _context.InsertUserDetail(user);        
        }
        
        [HttpGet("{Mobile}")]
        public IActionResult GetUserByMobile(string Mobile)
        {
            User result = _context.GetUser(Mobile);
            if (result!=null)
            {
                return Ok(result);
            }
            result = new User();
            return Ok(result);
        }

    }
}
