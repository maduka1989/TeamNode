using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace NodeCMBAPI.Services
{
    public class FoodPortionService : IDataService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<Food_Portion> GetFoodPortion()
        {
            param = new SqlParameter[0];
            List<Food_Portion> lstFoodPortion = new List<Food_Portion>();
            ds = new DataSet();
            ds = access.spDataSet("get_Food_Portion", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Food_Portion foodPortion = new Food_Portion();
                foodPortion.ID = Convert.ToInt32(item["ID"]);
                foodPortion.Portion = item["Portion"].ToString();
                foodPortion.ShortDescription = item["ShortDescription"].ToString();
                foodPortion.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                foodPortion.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                foodPortion.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                foodPortion.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                foodPortion.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                lstFoodPortion.Add(foodPortion);
            }

            return lstFoodPortion;
        }

        public Food_Portion GetFoodPortionById(int id)
        {
            var lstFoodPortion = GetFoodPortion();
            foreach (var fp in lstFoodPortion)
            {
                if (fp.ID == id)
                    return fp;
            }
            return null;
        }


        public string AddFoodPortion(Food_Portion foodPortion)
        {
            try
            {
                param = new SqlParameter[8];
                param[0] = new SqlParameter("@Portion", foodPortion.Portion);
                param[1] = new SqlParameter("@ShortDescription", foodPortion.ShortDescription);
                param[2] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@CreatedBy", foodPortion.CreatedBy);
                param[5] = new SqlParameter("@ModifiedBy", foodPortion.ModifiedBy);
                param[6] = new SqlParameter("@IsAvailable", foodPortion.IsAvailable);
                param[7] = new SqlParameter("@ReturnId", foodPortion.IsAvailable);

                param[7].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Food_Portion", param);
                int i = Convert.ToInt32(param[7].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
            
        }

        public string UpdateFoodPortion(Food_Portion foodPortion)
        {
            try
            {
                var lst = GetFoodPortion();
                var item = lst.Any(x => x.ID == foodPortion.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@ID", foodPortion.ID);
                param[1] = new SqlParameter("@Portion", foodPortion.Portion);
                param[2] = new SqlParameter("@ShortDescription", foodPortion.ShortDescription);
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@ModifiedBy", foodPortion.ModifiedBy);
                param[5] = new SqlParameter("@IsAvailable", foodPortion.IsAvailable);
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_FoodPortion", param);
                int i = Convert.ToInt32(param[6].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }

        }
    }
}
