using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class MenuService : IMenuService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddMenu(Menu menu)
        {
            try
            {

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@Name", menu.Name);         
                param[1] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[2] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@CreatedBy", Convert.ToInt32(menu.CreatedBy));
                param[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(menu.ModifiedBy));
                param[5] = new SqlParameter("@IsAvailable", Convert.ToInt32(menu.IsAvailable));
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Menu", param);
                int i = Convert.ToInt32(param[6].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Menu GetById(int id)
        {
            var lstMenu = GetMenu();
            foreach (var u in lstMenu)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Menu> GetMenu()
        {
            param = new SqlParameter[0];
            List<Menu> lstMenus = new List<Menu>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Menu", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Menu menu = new Menu();
                menu.ID = Convert.ToInt32(item["ID"]);
                menu.Name = item["Name"].ToString();
                menu.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                menu.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                menu.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                menu.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                menu.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                lstMenus.Add(menu);
            }

            return lstMenus;
        }

        public string UpdateMenu(Menu menu)
        {
            try
            {
                var lst = GetMenu();
                var item = lst.Any(x => x.ID == menu.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }               


                param = new SqlParameter[6];
                param[0] = new SqlParameter("@ID", menu.ID);
                param[1] = new SqlParameter("@Name", menu.Name);
                param[2] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedBy", Convert.ToInt32(menu.ModifiedBy));
                param[4] = new SqlParameter("@IsAvailable", Convert.ToInt32(menu.IsAvailable));
                param[5] = new SqlParameter("@ReturnId", "");

                param[5].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Menu", param);
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
