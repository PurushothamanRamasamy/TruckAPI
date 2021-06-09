using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckAPI.Models;

namespace TruckAPI.Repository.UserRepo
{
    public class UserRepo:IUserRepo
    {
        private readonly TruckAppDBContext _context;
        public UserRepo(TruckAppDBContext context)
        {
            _context = context;
        }
        public string Encryptdata(string password)
        {
            string strmsg;
            byte[] encode;
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public string Decryptdata(string encryptpwd)
        {
            string decryptpwd;
            UTF8Encoding encodepwd = new();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        public ActionResult<IEnumerable<User>> GetUserDetails()
        {
            return _context.Users.ToList<User>();
        }
        public string InsertUserDetail(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return user.UserName + " data sucessfully added";
        }
        public int UpdateUserDetails(int id, User user)
        {
            if (id != user.UserId)
            {
                return 400;//bad request
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        public User GetUser(string mobile)
        {
            User result= _context.Users.FirstOrDefault(e => e.MobileNumber == mobile);
            if (result!=null)
            {
                result.Password = Decryptdata(result.Password);
                return result;
            }
            return null;
        }
    }
}
