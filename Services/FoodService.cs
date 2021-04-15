using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class FoodService : IFoodService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<Food> GetFood()
        {
            param = new SqlParameter[0];
            List<Food> lstFoods = new List<Food>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Foods", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Food food = new Food();
                food.ID = Convert.ToInt32(item["ID"]);
                food.Name = item["Name"].ToString();
                food.Description = item["Description"].ToString();
                food.Image = (byte[])item["Image"];
                food.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                food.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                food.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                food.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                food.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstFoods.Add(food);
            }

            return lstFoods;
        }

        public Food GetById(int id)
        {
            var lstFoods = GetFood();
            foreach (var u in lstFoods)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string AddFood(Food food)
        {
            try
            {

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@Name", food.Name);
                param[1] = new SqlParameter("@Description", food.Description);
                param[2] = new SqlParameter("@Image", (byte[])food.Image);
                param[3] = new SqlParameter("@IsAvailable", Convert.ToBoolean(food.IsAvailable));
                param[4] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@CreatedBy", Convert.ToInt32(food.CreatedBy));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(food.ModifiedBy));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Food", param);
                int i = Convert.ToInt32(param[8].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string UpdateFood(Food food)
        {
            try
            {
                var lst = GetFood();
                var item = lst.Any(x => x.ID == food.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[8];
                param[0] = new SqlParameter("@ID", food.ID);
                param[1] = new SqlParameter("@Name", food.Name);
                param[2] = new SqlParameter("@Description", food.Description);
                param[3] = new SqlParameter("@Image", food.Image == null ? null : (byte[])food.Image);
                param[4] = new SqlParameter("@IsAvailable", Convert.ToBoolean(food.IsAvailable));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedBy", Convert.ToInt32(food.ModifiedBy));
                param[7] = new SqlParameter("@ReturnId", "");

                param[7].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Food", param);
                int i = Convert.ToInt32(param[7].Value);
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
