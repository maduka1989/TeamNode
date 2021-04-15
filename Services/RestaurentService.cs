using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class RestaurentService : IRestaurentService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<Restaurent> GetRestaurent()
        {
            param = new SqlParameter[0];
            List<Restaurent> lstRestaurent = new List<Restaurent>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Restaurent", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Restaurent restaurent = new Restaurent();
                restaurent.ID = Convert.ToInt32(item["ID"]);
                restaurent.Name = item["Name"].ToString();
                restaurent.Address = item["Address"].ToString();
                restaurent.Mobile = item["Mobile"].ToString();
                restaurent.LandPhone = item["LandPhone"].ToString();
                restaurent.NoOfTables = Convert.ToInt32(item["NoOfTables"]);
                restaurent.HotelID = Convert.ToInt32(item["HotelID"].ToString());
                restaurent.NodeCusID = Convert.ToInt32(item["NodeCusID"]);
                restaurent.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                restaurent.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                restaurent.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                restaurent.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstRestaurent.Add(restaurent);
            }

            return lstRestaurent;
        }

        public Restaurent GetById(int id)
        {
            var lstRestaurent = GetRestaurent();
            foreach (var u in lstRestaurent)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string AddRestaurent(Restaurent restaurent)
        {
            try
            {

                param = new SqlParameter[12];
                param[0] = new SqlParameter("@Name", restaurent.Name);
                param[1] = new SqlParameter("@Address", restaurent.Address);
                param[2] = new SqlParameter("@Mobile", restaurent.Mobile);
                param[3] = new SqlParameter("@LandPhone", restaurent.LandPhone);
                param[4] = new SqlParameter("@NoOfTables", restaurent.NoOfTables);
                param[5] = new SqlParameter("@HotelID", Convert.ToInt32(restaurent.HotelID));
                param[6] = new SqlParameter("@NodeCusID", Convert.ToInt32(restaurent.NodeCusID));
                param[7] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[8] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[9] = new SqlParameter("@CreatedBy", Convert.ToInt32(restaurent.CreatedBy));
                param[10] = new SqlParameter("@ModifiedBy", Convert.ToInt32(restaurent.ModifiedBy));
                param[11] = new SqlParameter("@ReturnId", "");

                param[11].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Restaurent", param);
                int i = Convert.ToInt32(param[11].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string UpdateRestaurent(Restaurent restaurent)
        {
            try
            {
                var lst = GetRestaurent();
                var item = lst.Any(x => x.ID == restaurent.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[11];
                param[0] = new SqlParameter("@ID", restaurent.ID);
                param[1] = new SqlParameter("@Name", restaurent.Name);
                param[2] = new SqlParameter("@Address", restaurent.Address);
                param[3] = new SqlParameter("@Mobile", restaurent.Mobile);
                param[4] = new SqlParameter("@LandPhone", restaurent.LandPhone);
                param[5] = new SqlParameter("@NoOfTables", restaurent.NoOfTables);
                param[6] = new SqlParameter("@HotelID", Convert.ToInt32(restaurent.HotelID));
                param[7] = new SqlParameter("@NodeCusID", Convert.ToInt32(restaurent.NodeCusID));
                param[8] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[9] = new SqlParameter("@ModifiedBy", Convert.ToInt32(restaurent.ModifiedBy));
                param[10] = new SqlParameter("@ReturnId", "");

                param[10].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Restaurent", param);
                int i = Convert.ToInt32(param[10].Value);
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
