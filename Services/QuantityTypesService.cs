using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class QuantityTypesService : IQuantityTypesService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<QuantityTypes> GetQuantityTypes()
        {
            param = new SqlParameter[0];
            List<QuantityTypes> lstQuantityTypes = new List<QuantityTypes>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_QuantityTypes", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                QuantityTypes qt = new QuantityTypes();
                qt.ID = Convert.ToInt32(item["ID"]);
                qt.Type = item["Type"].ToString();
                qt.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                qt.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                qt.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                qt.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                qt.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstQuantityTypes.Add(qt);
            }

            return lstQuantityTypes;
        }

        public QuantityTypes GetById(int id)
        {
            var lstQuantityTypes = GetQuantityTypes();
            foreach (var u in lstQuantityTypes)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string AddQuantityTypes(QuantityTypes qt)
        {
            try
            {

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@Type", qt.Type);
                param[1] = new SqlParameter("@IsAvailable", qt.IsAvailable);
                param[2] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@CreatedBy", Convert.ToInt32(qt.CreatedBy));
                param[5] = new SqlParameter("@ModifiedBy", Convert.ToInt32(qt.ModifiedBy));
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_QuantityTypes", param);
                int i = Convert.ToInt32(param[6].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string UpdateQuantityTypes(QuantityTypes qt)
        {
            try
            {
                var lst = GetQuantityTypes();
                var item = lst.Any(x => x.ID == qt.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[6];
                param[0] = new SqlParameter("@ID", qt.ID);
                param[1] = new SqlParameter("@Type", qt.Type);
                param[2] = new SqlParameter("@IsAvailable", qt.IsAvailable);               
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(qt.ModifiedBy));
                param[5] = new SqlParameter("@ReturnId", "");

                param[5].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_QuantityTypes", param);
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
