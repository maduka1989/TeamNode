using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class AddtionitemsmenuService :IAddtionitemsmenuService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<Addtion_items_menu> GetAddtionitemsmenu()
        {
            param = new SqlParameter[0];
            List<Addtion_items_menu> lstAddtionitemsmenu = new List<Addtion_items_menu>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Addtionitemsmenu", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Addtion_items_menu aim = new Addtion_items_menu();
                aim.ID = Convert.ToInt32(item["ID"]);
                aim.FoodID = Convert.ToInt32(item["FoodID"]);
                aim.RawMaterialID = Convert.ToInt32(item["RawMaterialID"]);
                aim.Description = item["Description"].ToString();
                aim.Price = Convert.ToDouble(item["Price"]);
                aim.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                aim.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                aim.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                aim.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstAddtionitemsmenu.Add(aim);
            }

            return lstAddtionitemsmenu;
        }

        public Addtion_items_menu GetById(int id)
        {
            var lstAddtionitemsmenu = GetAddtionitemsmenu();
            foreach (var u in lstAddtionitemsmenu)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string AddAddtionitemsmenu(Addtion_items_menu aim)
        {
            try
            {

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@FoodID", Convert.ToInt32(aim.FoodID));
                param[1] = new SqlParameter("@RawMaterialID", Convert.ToInt32(aim.RawMaterialID));
                param[2] = new SqlParameter("@Description", aim.Description);
                param[3] = new SqlParameter("@Price", Convert.ToDouble(aim.Price));
                param[4] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@CreatedBy", Convert.ToInt32(aim.CreatedBy));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(aim.ModifiedBy));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Addtionitemsmenu", param);
                int i = Convert.ToInt32(param[8].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string UpdateAddtionitemsmenu(Addtion_items_menu aim)
        {
            try
            {
                var lst = GetAddtionitemsmenu();
                var item = lst.Any(x => x.ID == aim.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[8];
                param[0] = new SqlParameter("@ID", aim.ID);
                param[1] = new SqlParameter("@FoodID", Convert.ToInt32(aim.FoodID));
                param[2] = new SqlParameter("@RawMaterialID", Convert.ToInt32(aim.RawMaterialID));
                param[3] = new SqlParameter("@Description", aim.Description);
                param[4] = new SqlParameter("@Price", Convert.ToDouble(aim.Price));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedBy", Convert.ToInt32(aim.ModifiedBy));
                param[7] = new SqlParameter("@ReturnId", "");

                param[7].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Addtionitemsmenu", param);
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
