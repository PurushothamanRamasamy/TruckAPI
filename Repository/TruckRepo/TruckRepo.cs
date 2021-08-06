using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TruckAPI.Models;
using static System.Formats.Asn1.AsnWriter;

namespace TruckAPI.Repository
{
    public class TruckRepo : ITruckRepo
    {
        private readonly IConfiguration Configuration;

        
        public TruckRepo(IConfiguration _configuration)
        {
            Configuration = _configuration;
       

        }

        public string DeleteTruck(string truckNumber)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand sqlComm = new SqlCommand("S_DeleteTruck_P", conn);
                    sqlComm.Parameters.AddWithValue("@id", truckNumber);
                    


                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                    return truckNumber + " successfully deleted";
                }
            }
            catch (Exception e)
            {
                return e.Message;
                throw;
            }
            
        }

        public Truck GetTruck(string truckNumber)
        {
            
            
            
            Truck trk = new Truck();
            using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand sqlComm = new SqlCommand("S_GetTruckByTruckNumber_P", conn);
                sqlComm.Parameters.AddWithValue("@TruckNumber", truckNumber);


                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                       trk.TruckNumber = dr["TruckNumber"].ToString();
                       trk.TruckType = dr["TruckType"].ToString();
                       trk.ManagerId = Convert.ToInt32(dr["ManagerId"]);
                       trk.DriverName = dr["DriverName"].ToString();
                       trk.DriverLicenceNumber = dr["DriverLicenceNumber"].ToString();
                       trk.PickCity = dr["PickCity"].ToString();
                       trk.DropCity = dr["DropCity"].ToString();
                       trk.TruckStatus = Convert.ToBoolean(dr["truckStatus"]);
                }

            }
            

            return trk;
            
            
        }

        public IEnumerable<Truck> GetTruckDetails()
        {
            List<Truck> TkList = new List<Truck>();
            using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand sqlComm = new SqlCommand("S_GetAllTrucks_P", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                    Truck trk = new Truck
                    {
                        TruckNumber = dr["TruckNumber"].ToString(),
                        TruckType = dr["TruckType"].ToString(),
                        ManagerId = Convert.ToInt32(dr["ManagerId"]),
                        DriverName = dr["DriverName"].ToString(),
                        DriverLicenceNumber = dr["DriverLicenceNumber"].ToString(),
                        PickCity = dr["PickCity"].ToString(),
                        DropCity = dr["DropCity"].ToString(),
                        TruckStatus = Convert.ToBoolean(dr["truckStatus"])
                    };
                    TkList.Add(trk);
                }

            }
            
            
            return TkList;
            
        }
        public string InsertTruckDetail(Truck truck)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand sqlComm = new SqlCommand("S_InsertTruck_P", conn);
                    sqlComm.Parameters.AddWithValue("@TruckNumber", truck.TruckNumber);
                    sqlComm.Parameters.AddWithValue("@TruckType", truck.TruckType);
                    sqlComm.Parameters.AddWithValue("@ManagerId", truck.ManagerId);
                    sqlComm.Parameters.AddWithValue("@DriverName", truck.DriverName);
                    sqlComm.Parameters.AddWithValue("@DriverLicenceNumber", truck.DriverLicenceNumber);
                    sqlComm.Parameters.AddWithValue("@PickCity", truck.PickCity);
                    sqlComm.Parameters.AddWithValue("@DropCity", truck.DropCity);


                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return truck.TruckNumber + " data sucessfully added";
        }

        public IEnumerable<STruck> SearchTruck(string Pickcity, string Dropcity, string TruckType,DateTime Rdate,int distance)
        {
            List<STruck> TkList = new List<STruck>();
            using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand sqlComm = new SqlCommand("S_SearchTruck_P", conn);
                
                sqlComm.Parameters.AddWithValue("@PickCity", Pickcity);
                sqlComm.Parameters.AddWithValue("@DropCity", Dropcity);
                sqlComm.Parameters.AddWithValue("@TruckType", TruckType);
                sqlComm.Parameters.AddWithValue("@ServiceDate",Rdate);
                sqlComm.Parameters.AddWithValue("@Aproxdistance", distance);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                    STruck trk = new STruck
                    {
                        TruckNumber = dr["TruckNumber"].ToString(),
                        TruckType = dr["TruckType"].ToString(),
                        ManagerId = Convert.ToInt32(dr["ManagerId"]),
                        DriverName = dr["DriverName"].ToString(),
                        DriverLicenceNumber = dr["DriverLicenceNumber"].ToString(),
                        PickCity = dr["PickCity"].ToString(),
                        DropCity = dr["DropCity"].ToString(),
                        TruckStatus = Convert.ToBoolean(dr["truckStatus"]),
                        Cost=Convert.ToInt32(dr["cost"])
                    };
                    TkList.Add(trk);
                }

            }


            return TkList;
        }

        

        public bool TruckExists(string id)
        {
            return GetTruckDetails().Any(e => e.TruckNumber == id);

        }

        public int UpdateTruckDetails(string id, Truck truck)
        {
            if (id != truck.TruckNumber)
            {
                return 400;//bad request
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand sqlComm = new SqlCommand("S_UpdateTruck_P", conn);
                    sqlComm.Parameters.AddWithValue("@TruckNumber", truck.TruckNumber);
                    sqlComm.Parameters.AddWithValue("@TruckType", truck.TruckType);
                    sqlComm.Parameters.AddWithValue("@DriverName", truck.DriverName);
                    sqlComm.Parameters.AddWithValue("@DriverLicenceNumber", truck.DriverLicenceNumber);
                    sqlComm.Parameters.AddWithValue("@PickCity", truck.PickCity);
                    sqlComm.Parameters.AddWithValue("@DropCity", truck.DropCity);
                    sqlComm.Parameters.AddWithValue("@TruckStatus", truck.TruckStatus);



                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TruckExists(id))
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
