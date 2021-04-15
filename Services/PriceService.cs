using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class PriceService : IPriceService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddPrice(Price price)
        {
            try
            {

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@PortionID", Convert.ToDouble(price.PortionID));
                param[1] = new SqlParameter("@FoodID", Convert.ToInt32(price.FoodID));
                param[2] = new SqlParameter("@ActualPrice", Convert.ToDouble(price.ActualPrice));
                param[3] = new SqlParameter("@SellingPrice", Convert.ToDouble(price.SellingPrice));
                param[4] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@CreatedBy", Convert.ToInt32(price.CreatedBy));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(price.ModifiedBy));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Price", param);
                int i = Convert.ToInt32(param[8].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Price GetById(int id)
        {
            var lstPrices = GetPrice();
            foreach (var u in lstPrices)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Price> GetPrice()
        {
            param = new SqlParameter[0];
            List<Price> lstPrice = new List<Price>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Price", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Price price = new Price();
                price.ID = Convert.ToInt32(item["ID"]);
                price.PortionID = Convert.ToInt32(item["PortionID"]);
                price.FoodID = Convert.ToInt32(item["FoodID"]);
                price.ActualPrice = Convert.ToDouble(item["ActualPrice"]);
                price.SellingPrice = Convert.ToDouble(item["SellingPrice"]);
                price.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                price.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                price.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                price.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstPrice.Add(price);
            }

            return lstPrice;
        }

        public string UpdatePrice(Price price)
        {
            try
            {
                var lst = GetPrice();
                var item = lst.Any(x => x.ID == price.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[8];
                param[0] = new SqlParameter("@ID", Convert.ToInt32(price.ID));
                param[1] = new SqlParameter("@PortionID", Convert.ToInt32(price.PortionID));
                param[2] = new SqlParameter("@FoodID", Convert.ToInt32(price.FoodID));
                param[3] = new SqlParameter("@ActualPrice", Convert.ToDouble(price.ActualPrice));
                param[4] = new SqlParameter("@SellingPrice", Convert.ToDouble(price.SellingPrice));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedBy", Convert.ToInt32(price.ModifiedBy));
                param[7] = new SqlParameter("@ReturnId", "");

                param[7].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Price", param);
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
