using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class MenuItemsService : IMenuItemsService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddMenuItems(Menu_Items mi)
        {
            try
            {

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@PriceID", Convert.ToInt32(mi.PriceID));
                param[1] = new SqlParameter("@MenuTypeID", Convert.ToInt32(mi.MenuTypeID));
                param[2] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@CreatedBy", Convert.ToInt32(mi.CreatedBy));
                param[5] = new SqlParameter("@ModifiedBy", Convert.ToInt32(mi.ModifiedBy));
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_MenuItems", param);
                int i = Convert.ToInt32(param[6].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Menu_Items GetById(int id)
        {
            var lstMenuItemss = GetMenuItems();
            foreach (var u in lstMenuItemss)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Menu_Items> GetMenuItems()
        {
            param = new SqlParameter[0];
            List<Menu_Items> lstMenuItems = new List<Menu_Items>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_MenuItems", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Menu_Items mi = new Menu_Items();
                mi.ID = Convert.ToInt32(item["ID"]);
                mi.PriceID = Convert.ToInt32(item["PriceID"]);
                mi.MenuTypeID = Convert.ToInt32(item["MenuTypeID"]);
                mi.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                mi.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                mi.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                mi.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstMenuItems.Add(mi);
            }

            return lstMenuItems;
        }

        public string UpdateMenuItems(Menu_Items mi)
        {
            try
            {
                var lst = GetMenuItems();
                var item = lst.Any(x => x.ID == mi.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[6];
                param[0] = new SqlParameter("@ID", Convert.ToInt32(mi.ID));
                param[1] = new SqlParameter("@PriceID", Convert.ToInt32(mi.PriceID));
                param[2] = new SqlParameter("@MenuTypeID", Convert.ToInt32(mi.MenuTypeID));
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(mi.ModifiedBy));
                param[5] = new SqlParameter("@ReturnId", "");

                param[5].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_MenuItems", param);
                int i = Convert.ToInt32(param[5].Value);
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
