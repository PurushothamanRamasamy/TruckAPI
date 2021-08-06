using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckAPI.Models;

namespace TruckAPI.Repository.UserRepo
{
    public class UserRepo:IUserRepo
    {
        private readonly IConfiguration Configuration;
        public UserRepo(TruckAppDBContext context, IConfiguration _configuration)
        {
            Configuration = _configuration;
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
        public IEnumerable<User> GetUserDetails()
        {
            List<User> URList = new List<User>();
            using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand sqlComm = new SqlCommand("S_AllUsers_P", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                    User USR = new User
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        MobileNumber = dr["MobileNumber"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        UserRole = dr["UserRole"].ToString(),
                        UserStatus = Convert.ToBoolean(dr["UserStatus"]),

                        Password = dr["Password"].ToString()
                        
                    };
                    URList.Add(USR);
                }

            }


            return URList;
        }
        public string InsertUserDetail(User user)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand sqlComm = new SqlCommand("S_InsertUser_P", conn);
                    sqlComm.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                    sqlComm.Parameters.AddWithValue("@UserName", user.UserName);
                    sqlComm.Parameters.AddWithValue("@UserRole", user.UserRole);
                    sqlComm.Parameters.AddWithValue("@UserStatus", user.UserStatus);
                    sqlComm.Parameters.AddWithValue("@Password", user.Password); ;



                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                }
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
           

            try
            {
                using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand sqlComm = new SqlCommand("S_UpdateUser_P", conn);
                    sqlComm.Parameters.AddWithValue("@id", id);
                    sqlComm.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                    sqlComm.Parameters.AddWithValue("@UserName", user.UserName);
                    sqlComm.Parameters.AddWithValue("@UserRole", user.UserRole);
                    sqlComm.Parameters.AddWithValue("@UserStatus", user.UserStatus);
                    sqlComm.Parameters.AddWithValue("@Password", user.Password); ;



                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                }
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
            return GetUserDetails().Any(e => e.UserId == id);
        }

        public User GetUser(string mobile)
        {
            User result=new User();
            using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand sqlComm = new SqlCommand("S_IsRegisteredUser_P", conn);
                sqlComm.Parameters.AddWithValue("@MobileNumber", mobile);
                

                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                while (reader.Read())
                {
                    result.UserId = Convert.ToInt32(reader["UserId"]);
                    result.MobileNumber = reader["MobileNumber"].ToString();
                    result.UserName = reader["UserName"].ToString();
                    result.UserRole = reader["UserRole"].ToString();
                    result.UserStatus = Convert.ToBoolean(reader["UserStatus"]);
                    result.Password = reader["Password"].ToString();
                }

            }
            
           
            if (result!=null)
            {
                result.Password = Decryptdata(result.Password);
                return result;
            }
            return null;
        }
    }
}
