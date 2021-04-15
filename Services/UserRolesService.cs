using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class UserRolesService : IUserRolesService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddUserRoles(UserRoles ur)
        {
            try
            {

                param = new SqlParameter[6];
                param[0] = new SqlParameter("@Role", ur.Role);
                param[1] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[2] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@CreatedBy", Convert.ToInt32(ur.CreatedBy));
                param[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ur.ModifiedBy));
                param[5] = new SqlParameter("@ReturnId", "");

                param[5].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_UserRoles", param);
                int i = Convert.ToInt32(param[5].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public UserRoles GetById(int id)
        {
            var lstUserRoles = GetUserRoles();
            foreach (var u in lstUserRoles)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<UserRoles> GetUserRoles()
        {
            param = new SqlParameter[0];
            List<UserRoles> lstUserRoles = new List<UserRoles>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_UserRoles", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                UserRoles ur = new UserRoles();
                ur.ID = Convert.ToInt32(item["ID"]);
                ur.Role = item["Role"].ToString();
                ur.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                ur.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                ur.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                ur.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstUserRoles.Add(ur);
            }

            return lstUserRoles;
        }

        public string UpdateUserRoles(UserRoles ur)
        {
            try
            {
                var lst = GetUserRoles();
                var item = lst.Any(x => x.ID == ur.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[5];
                param[0] = new SqlParameter("@ID", ur.ID);
                param[1] = new SqlParameter("@Role", ur.Role);
                param[2] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ur.ModifiedBy));
                param[4] = new SqlParameter("@ReturnId", "");

                param[4].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_UserRoles", param);
                int i = Convert.ToInt32(param[4].Value);
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
