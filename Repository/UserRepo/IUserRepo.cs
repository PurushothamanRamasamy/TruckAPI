using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckAPI.Models;

namespace TruckAPI.Repository.UserRepo
{
    public interface IUserRepo
    {
        string Encryptdata(string password);
        string Decryptdata(string encryptpwd);

        
        string InsertUserDetail(User user);
        ActionResult<IEnumerable<User>> GetUserDetails();
        int UpdateUserDetails(int id, User user);
        bool UserExists(int id);
        User GetUser(string mobile);
    }
}
