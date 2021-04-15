using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class HappyHourService : IHappyHourService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public string AddHappyHour(Happy_Hour hh)
        {
            try
            {

                param = new SqlParameter[10];
                param[0] = new SqlParameter("@FoodID", Convert.ToInt32(hh.FoodID));
                param[1] = new SqlParameter("@PortionID", Convert.ToInt32(hh.PortionID));
                param[2] = new SqlParameter("@Day", Convert.ToInt32(hh.Day));
                param[3] = new SqlParameter("@StartTime", hh.StartTime);
                param[4] = new SqlParameter("@EndTime", hh.EndTime);         
                param[5] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[7] = new SqlParameter("@CreatedBy", Convert.ToInt32(hh.CreatedBy));
                param[8] = new SqlParameter("@ModifiedBy", Convert.ToInt32(hh.ModifiedBy));
                param[9] = new SqlParameter("@ReturnId", "");

                param[9].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_HappyHour", param);
                int i = Convert.ToInt32(param[9].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Happy_Hour GetById(int id)
        {
            var lstHappyHour = GetHappyHour();
            foreach (var u in lstHappyHour)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Happy_Hour> GetHappyHour()
        {
            param = new SqlParameter[0];
            List<Happy_Hour> lstHappyHour = new List<Happy_Hour>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_HappyHour", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Happy_Hour hh = new Happy_Hour();
                hh.ID = Convert.ToInt32(item["ID"]);
                hh.FoodID = Convert.ToInt32(item["FoodID"]);
                hh.PortionID = Convert.ToInt32(item["PortionID"]);
                hh.Day = Convert.ToInt32(item["Day"]);
                hh.StartTime = item["StartTime"].ToString();
                hh.EndTime = item["EndTime"].ToString();
                hh.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                hh.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                hh.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                hh.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstHappyHour.Add(hh);
            }

            return lstHappyHour;
        }

        public string UpdateHappyHour(Happy_Hour hh)
        {
            try
            {
                var lst = GetHappyHour();
                var item = lst.Any(x => x.ID == hh.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@ID", hh.ID);
                param[1] = new SqlParameter("@FoodID", hh.FoodID);
                param[2] = new SqlParameter("@PortionID", hh.PortionID);
                param[3] = new SqlParameter("@Day", hh.Day);
                param[4] = new SqlParameter("@StartTime", hh.StartTime);
                param[5] = new SqlParameter("@EndTime", hh.EndTime);
                param[6] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(hh.ModifiedBy));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_HappyHour", param);
                int i = Convert.ToInt32(param[8].Value);
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
