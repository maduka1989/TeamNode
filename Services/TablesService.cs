using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class TablesService : ITablesService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddTables(Tables table)
        {
            try
            {

                param = new SqlParameter[8];
                param[0] = new SqlParameter("@TableNo", table.TableNo);
                param[1] = new SqlParameter("@UserID", Convert.ToInt32(table.UserID));
                param[2] = new SqlParameter("@IsInUse", Convert.ToBoolean(table.IsInUse));
                param[3] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@CreatedBy", Convert.ToInt32(table.CreatedBy));
                param[6] = new SqlParameter("@ModifiedBy", Convert.ToInt32(table.ModifiedBy));
                param[7] = new SqlParameter("@ReturnId", "");

                param[7].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Tables", param);
                int i = Convert.ToInt32(param[7].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Tables GetById(int id)
        {
            var lstTables = GetTables();
            foreach (var u in lstTables)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Tables> GetTables()
        {
            param = new SqlParameter[0];
            List<Tables> lstTables = new List<Tables>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Tables", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Tables table = new Tables();
                table.ID = Convert.ToInt32(item["ID"]);
                table.TableNo = item["TableNo"].ToString();
                table.UserID = Convert.ToInt32(item["UserID"]);
                table.IsInUse = Convert.ToBoolean(item["IsInUse"]);
                table.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                table.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                table.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                table.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstTables.Add(table);
            }

            return lstTables;
        }

        public string UpdateTables(Tables table)
        {
            try
            {
                var lst = GetTables();
                var item = lst.Any(x => x.ID == table.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@ID", table.ID);
                param[1] = new SqlParameter("@TableNo", table.TableNo);
                param[2] = new SqlParameter("@UserID", Convert.ToInt32(table.UserID));
                param[3] = new SqlParameter("@IsInUse", Convert.ToBoolean(table.IsInUse));
                param[4] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@ModifiedBy", Convert.ToInt32(table.ModifiedBy));
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Tables", param);
                int i = Convert.ToInt32(param[6].Value);
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
