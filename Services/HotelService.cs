using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class HotelService : IHotelService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<Hotel> GetHotels()
        {
            param = new SqlParameter[0];
            List<Hotel> lstHotels = new List<Hotel>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Hotels", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Hotel hotel = new Hotel();
                hotel.ID = Convert.ToInt32(item["ID"]);
                hotel.Name = item["Name"].ToString();
                hotel.Address = item["Address"].ToString();
                hotel.Mobile = item["Mobile"].ToString();
                hotel.LandPhone = item["LandPhone"].ToString();
                hotel.country = item["country"].ToString();
                hotel.NodeCusID = Convert.ToInt32(item["NodeCusID"]);
                hotel.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                hotel.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                hotel.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                hotel.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstHotels.Add(hotel);
            }

            return lstHotels;
        }

        public Hotel GetById(int id)
        {
            var lstHotels = GetHotels();
            foreach (var u in lstHotels)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string AddHotel(Hotel hotel)
        {
            try
            {

                param = new SqlParameter[11];
                param[0] = new SqlParameter("@Name", hotel.Name);
                param[1] = new SqlParameter("@Address", hotel.Address);
                param[2] = new SqlParameter("@Mobile", hotel.Mobile);
                param[3] = new SqlParameter("@LandPhone", hotel.LandPhone);
                param[4] = new SqlParameter("@country", hotel.country);
                param[5] = new SqlParameter("@NodeCusID", Convert.ToInt32(hotel.NodeCusID));
                param[6] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[7] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[8] = new SqlParameter("@CreatedBy", Convert.ToInt32(hotel.CreatedBy));
                param[9] = new SqlParameter("@ModifiedBy", Convert.ToInt32(hotel.ModifiedBy));
                param[10] = new SqlParameter("@ReturnId", "");

                param[10].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Hotels", param);
                int i = Convert.ToInt32(param[10].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string UpdateHotel(Hotel hotel)
        {
            try
            {
                var lst = GetHotels();
                var item = lst.Any(x => x.ID == hotel.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[10];
                param[0] = new SqlParameter("@ID", hotel.ID);
                param[1] = new SqlParameter("@Name", hotel.Name);
                param[2] = new SqlParameter("@Address", hotel.Address);
                param[3] = new SqlParameter("@Mobile", hotel.Mobile);
                param[4] = new SqlParameter("@LandPhone", hotel.LandPhone);
                param[5] = new SqlParameter("@country", hotel.country);
                param[6] = new SqlParameter("@NodeCusID", Convert.ToInt32(hotel.NodeCusID));
                param[7] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[8] = new SqlParameter("@ModifiedBy", Convert.ToInt32(hotel.ModifiedBy));
                param[9] = new SqlParameter("@ReturnId", "");

                param[9].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Hotels", param);
                int i = Convert.ToInt32(param[9].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

    }
}
