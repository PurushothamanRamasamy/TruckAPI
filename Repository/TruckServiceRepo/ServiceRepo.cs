using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TruckAPI.Models;

namespace TruckAPI.Repository.TruckServiceRepo
{
    public class ServiceRepo:IServiceRepo
    {
        private readonly IConfiguration Configuration;

        public ServiceRepo(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IEnumerable<Request> BookingRequest(int id)
        {
            List<Request> SRList = new List<Request>();
            using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand sqlComm = new SqlCommand("S_bookingRequest_P", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Id", id);
                
                conn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                    Request request = new Request
                    {
                        ServiceId = Convert.ToInt32(dr["ServiceId"]),

                        TruckNumber = dr["TruckNumber"].ToString(),
                        BookingDate = Convert.ToDateTime(dr["BookingDate"]),
                        UserName = dr["UserName"].ToString(),
                        MobileNumber=dr["MobileNumber"].ToString(),
                        PickupCity = dr["PickupCity"].ToString(),
                        DropCity = dr["DropCity"].ToString(),
                        ServiceCost = Convert.ToInt32(dr["ServiceCost"])
                    };
                    SRList.Add(request);
                }

            }


            return SRList;
        }

        public IEnumerable<Service> GetServiceDetails()
        {
            List<Service> SRList = new List<Service>();
            using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand sqlComm = new SqlCommand("S_GetAllService_P", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader dr = sqlComm.ExecuteReader();
                while (dr.Read())
                {
                    Service service = new Service
                    {
                        ServiceId = Convert.ToInt32(dr["ServiceId"]),
                        CustomerId = Convert.ToInt32(dr["CustomerId"]),
                        ManagerId = Convert.ToInt32(dr["ManagerId"]),
                        TruckNumber = dr["TruckNumber"].ToString(),
                        BookingDate = Convert.ToDateTime(dr["BookingDate"]),
                        ServiceStatus = Convert.ToBoolean(dr["ServiceStatus"]),
                        BookingStatus = Convert.ToBoolean(dr["BookingStatus"]),
                        PickupCity = dr["PickupCity"].ToString(),
                        DropCity=dr["DropCity"].ToString(),
                        ServiceCost=Convert.ToInt32(dr["ServiceCost"])
                    };
                    SRList.Add(service);
                }

            }


            return SRList;
        }


        public string InsertServiceDetail(Service service)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand sqlComm = new SqlCommand("S_InsertService_P", conn);
                    sqlComm.Parameters.AddWithValue("@CustomerId", service.CustomerId);
                    sqlComm.Parameters.AddWithValue("@ManagerId", service.ManagerId);
                    sqlComm.Parameters.AddWithValue("@TruckNumber", service.TruckNumber);
                    sqlComm.Parameters.AddWithValue("@BookingDate", service.BookingDate);
                    sqlComm.Parameters.AddWithValue("@ServiceStatus", service.ServiceStatus);
                    sqlComm.Parameters.AddWithValue("@BookingStatus", service.BookingStatus);
                    sqlComm.Parameters.AddWithValue("@PickCity", service.PickupCity);
                    sqlComm.Parameters.AddWithValue("@DropCity", service.DropCity);
                    sqlComm.Parameters.AddWithValue("@ServiceCost", service.ServiceCost);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return "Data sucessfully added";
        }
        public bool ServiceExists(int id)
        {
            return GetServiceDetails().Any(e => e.ServiceId == id);

        }
        public int UpdateServiceDetails(int id, Service service)
        {
            if (id != service.ServiceId)
            {
                return 400;//bad request
            }
            

            try
            {
                using (SqlConnection conn = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")))
                {
                    SqlCommand sqlComm = new SqlCommand("S_UpdateService_P", conn);
                    sqlComm.Parameters.AddWithValue("@CustomerId", service.CustomerId);
                    sqlComm.Parameters.AddWithValue("@ManagerId", service.ManagerId);
                    sqlComm.Parameters.AddWithValue("@TruckNumber", service.TruckNumber);
                    sqlComm.Parameters.AddWithValue("@BookingDate", service.BookingDate);
                    sqlComm.Parameters.AddWithValue("@ServiceStatus", service.ServiceStatus);
                    sqlComm.Parameters.AddWithValue("@BookingStatus", service.BookingStatus);
                    sqlComm.Parameters.AddWithValue("@PickCity", service.PickupCity);
                    sqlComm.Parameters.AddWithValue("@DropCity", service.DropCity);
                    sqlComm.Parameters.AddWithValue("@ServiceCost", service.ServiceCost);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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
