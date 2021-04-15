using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class TaxService : ITaxService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddTax(Tax tax)
        {
            try
            {

                param = new SqlParameter[8];
                param[0] = new SqlParameter("@TaxName", tax.TaxName);
                param[1] = new SqlParameter("@Percentage", tax.Percentage);
                param[2] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@CreatedBy", Convert.ToInt32(tax.CreatedBy));
                param[5] = new SqlParameter("@ModifiedBy", Convert.ToInt32(tax.ModifiedBy));
                param[6] = new SqlParameter("@IsAvailable", tax.IsAvailable);
                param[7] = new SqlParameter("@ReturnId", "");

                param[7].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Tax", param);
                int i = Convert.ToInt32(param[7].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Tax GetById(int id)
        {
            var lstTax = GetTax();
            foreach (var u in lstTax)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Tax> GetTax()
        {
            param = new SqlParameter[0];
            List<Tax> lstTax = new List<Tax>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Tax", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Tax tax = new Tax();
                tax.ID = Convert.ToInt32(item["ID"]);
                tax.TaxName = item["TaxName"].ToString();
                tax.Percentage = Convert.ToDouble(item["Percentage"]);
                tax.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                tax.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                tax.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                tax.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                tax.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                lstTax.Add(tax);
            }

            return lstTax;
        }

        public string UpdateTax(Tax tax)
        {
            try
            {
                var lst = GetTax();
                var item = lst.Any(x => x.ID == tax.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@ID", tax.ID);
                param[1] = new SqlParameter("@TaxName", tax.TaxName);
                param[2] = new SqlParameter("@Percentage", tax.Percentage);
                param[3] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(tax.ModifiedBy));
                param[5] = new SqlParameter("@IsAvailable", tax.IsAvailable);
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Tax", param);
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
