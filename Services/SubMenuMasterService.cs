using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class SubMenuMasterService : ISubMenuMasterService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddSubMenuMaster(SubMenu_Master smm)
        {
            try
            {

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@Name", smm.Name);
                param[1] = new SqlParameter("@MenuID", Convert.ToInt32(smm.MenuID));
                param[2] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@CreatedBy", Convert.ToInt32(smm.CreatedBy));
                param[5] = new SqlParameter("@ModifiedBy", Convert.ToInt32(smm.ModifiedBy));
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_SubMenuMaster", param);
                int i = Convert.ToInt32(param[6].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public SubMenu_Master GetById(int id)
        {
            var lstsmm = GetSubMenuMaster();
            foreach (var u in lstsmm)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<SubMenu_Master> GetSubMenuMaster()
        {
            param = new SqlParameter[0];
            List<SubMenu_Master> lstSubMenuMasters = new List<SubMenu_Master>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_SubMenuMaster", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                SubMenu_Master smm = new SubMenu_Master();
                smm.ID = Convert.ToInt32(item["ID"]);
                smm.Name = item["Name"].ToString();
                smm.MenuID = Convert.ToInt32(item["MenuID"]);
                smm.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                smm.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                smm.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                smm.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstSubMenuMasters.Add(smm);
            }

            return lstSubMenuMasters;
        }

        public string UpdateSubMenuMaster(SubMenu_Master smm)
        {
            try
            {
                var lst = GetSubMenuMaster();
                var item = lst.Any(x => x.ID == smm.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[6];
                param[0] = new SqlParameter("@ID", smm.ID);
                param[1] = new SqlParameter("@Name", smm.Name);
                param[2] = new SqlParameter("@MenuID", Convert.ToInt32(smm.MenuID));
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(smm.ModifiedBy));
                param[5] = new SqlParameter("@ReturnId", "");

                param[5].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_SubMenuMaster", param);
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
