using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class RawMaterialsService :IRawMaterialsService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<Raw_Materials> GetRawMaterials()
        {
            param = new SqlParameter[0];
            List<Raw_Materials> lstRawMaterials = new List<Raw_Materials>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_RawMaterials", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Raw_Materials rm = new Raw_Materials();
                rm.ID = Convert.ToInt32(item["ID"]);
                rm.Name = item["Name"].ToString();
                rm.Description = item["Description"].ToString();
                rm.Quantity = Convert.ToDouble(item["Quantity"]);
                rm.QuantityTypeID = Convert.ToInt32(item["QuantityTypeID"]);
                rm.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                rm.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                rm.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                rm.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstRawMaterials.Add(rm);
            }

            return lstRawMaterials;
        }

        public Raw_Materials GetById(int id)
        {
            var lstRawMaterials = GetRawMaterials();
            foreach (var u in lstRawMaterials)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string AddRawMaterials(Raw_Materials rm)
        {
            try
            {

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@Name", rm.Name);
                param[1] = new SqlParameter("@Description", rm.Description);
                param[2] = new SqlParameter("@Quantity", rm.Quantity);
                param[3] = new SqlParameter("@QuantityTypeID", Convert.ToInt32(rm.QuantityTypeID));
                param[4] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@CreatedBy", Convert.ToInt32(rm.CreatedBy));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(rm.ModifiedBy));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_RawMaterials", param);
                int i = Convert.ToInt32(param[8].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string UpdateRawMaterials(Raw_Materials rm)
        {
            try
            {
                var lst = GetRawMaterials();
                var item = lst.Any(x => x.ID == rm.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[8];
                param[0] = new SqlParameter("@ID", rm.ID);
                param[1] = new SqlParameter("@Name", rm.Name);
                param[2] = new SqlParameter("@Description", rm.Description);
                param[3] = new SqlParameter("@Quantity", rm.Quantity);
                param[4] = new SqlParameter("@QuantityTypeID", Convert.ToInt32(rm.QuantityTypeID));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedBy", Convert.ToInt32(rm.ModifiedBy));
                param[7] = new SqlParameter("@ReturnId", "");

                param[7].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_RawMaterials", param);
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
