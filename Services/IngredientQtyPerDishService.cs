using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class IngredientQtyPerDishService : IIngredientQtyPerDishService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddIngredientQtyPerDish(Ingredient_Qty_Per_Dish iqpd)
        {
            try
            {

                param = new SqlParameter[10];
                param[0] = new SqlParameter("@DishID", Convert.ToInt32(iqpd.DishID));
                param[1] = new SqlParameter("@PortionID", Convert.ToInt32(iqpd.PortionID));
                param[2] = new SqlParameter("@RawMaterialID", Convert.ToInt32(iqpd.RawMaterialID));
                param[3] = new SqlParameter("@Qty", Convert.ToDouble(iqpd.Qty));
                param[4] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@CreatedBy", Convert.ToInt32(iqpd.CreatedBy));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(iqpd.ModifiedBy));
                param[8] = new SqlParameter("@IsActive", Convert.ToBoolean(iqpd.IsActive));
                param[9] = new SqlParameter("@ReturnId", "");

                param[9].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_IngredientQtyPerDish", param);
                int i = Convert.ToInt32(param[9].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Ingredient_Qty_Per_Dish GetById(int id)
        {
            var lstIngredientQtyPerDish = GetIngredientQtyPerDish();
            foreach (var u in lstIngredientQtyPerDish)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Ingredient_Qty_Per_Dish> GetIngredientQtyPerDish()
        {
            param = new SqlParameter[0];
            List<Ingredient_Qty_Per_Dish> lstIngredientQtyPerDish = new List<Ingredient_Qty_Per_Dish>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_IngredientQtyPerDish", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Ingredient_Qty_Per_Dish igpd = new Ingredient_Qty_Per_Dish();
                igpd.ID = Convert.ToInt32(item["ID"]);
                igpd.DishID = Convert.ToInt32(item["DishID"]);
                igpd.PortionID = Convert.ToInt32(item["PortionID"]);
                igpd.RawMaterialID = Convert.ToInt32(item["RawMaterialID"]);
                igpd.Qty = Convert.ToDouble(item["Qty"]);
                igpd.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                igpd.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                igpd.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                igpd.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                igpd.IsActive = Convert.ToBoolean(item["IsActive"]);
                lstIngredientQtyPerDish.Add(igpd);
            }

            return lstIngredientQtyPerDish;
        }

        public string UpdateIngredientQtyPerDish(Ingredient_Qty_Per_Dish iqpd)
        {
            try
            {
                var lst = GetIngredientQtyPerDish();
                var item = lst.Any(x => x.ID == iqpd.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@ID", iqpd.ID);
                param[1] = new SqlParameter("@DishID", Convert.ToInt32(iqpd.DishID));
                param[2] = new SqlParameter("@PortionID", Convert.ToInt32(iqpd.PortionID));
                param[3] = new SqlParameter("@RawMaterialID", Convert.ToInt32(iqpd.RawMaterialID));
                param[4] = new SqlParameter("@Qty", Convert.ToDouble(iqpd.Qty));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedBy", Convert.ToInt32(iqpd.ModifiedBy));
                param[7] = new SqlParameter("@IsActive", Convert.ToBoolean(iqpd.IsActive));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_IngredientQtyPerDish", param);
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
