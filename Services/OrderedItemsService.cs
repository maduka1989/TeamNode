using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class OrderedItemsService : IOrderedItemsService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddOrderedItems(OrderedItems ot)
        {
            try
            {

                param = new SqlParameter[10];
                param[0] = new SqlParameter("@OrderID", Convert.ToInt32(ot.OrderID));
                param[1] = new SqlParameter("@PriceID", Convert.ToInt32(ot.PriceID));
                param[2] = new SqlParameter("@Quantity", Convert.ToDouble(ot.Quantity));
                param[3] = new SqlParameter("@CustomerComment", ot.CustomerComment);
                param[4] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@CreatedBy", Convert.ToInt32(ot.CreatedBy));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ot.ModifiedBy));
                param[8] = new SqlParameter("@FoodID", Convert.ToInt32(ot.FoodID));
                param[9] = new SqlParameter("@ReturnId", "");

                param[9].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_OrderedItems", param);
                int i = Convert.ToInt32(param[9].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public OrderedItems GetById(int id)
        {
            var lstOrderedItems = GetOrderedItems();
            foreach (var u in lstOrderedItems)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<OrderedItems> GetOrderedItems()
        {
            param = new SqlParameter[0];
            List<OrderedItems> lstOrderedItems = new List<OrderedItems>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_OrderedItems", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                OrderedItems oi = new OrderedItems();
                oi.ID = Convert.ToInt32(item["ID"]);
                oi.OrderID = Convert.ToInt32(item["OrderID"]);
                oi.PriceID = Convert.ToInt32(item["PriceID"]);
                oi.Quantity = Convert.ToDouble(item["Quantity"]);
                oi.CustomerComment = item["CustomerComment"].ToString();
                oi.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                oi.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                oi.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                oi.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                oi.FoodID = Convert.ToInt32(item["FoodID"]);
                lstOrderedItems.Add(oi);
            }

            return lstOrderedItems;
        }

        public string UpdateOrderedItems(OrderedItems ot)
        {
            try
            {
                var lst = GetOrderedItems();
                var item = lst.Any(x => x.ID == ot.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@ID", ot.ID);
                param[1] = new SqlParameter("@OrderID", Convert.ToInt32(ot.OrderID));
                param[2] = new SqlParameter("@PriceID", Convert.ToInt32(ot.PriceID));
                param[3] = new SqlParameter("@Quantity", Convert.ToDouble(ot.Quantity));
                param[4] = new SqlParameter("@CustomerComment", ot.CustomerComment);
                param[5] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ot.ModifiedBy));
                param[7] = new SqlParameter("@FoodID", Convert.ToInt32(ot.FoodID));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_OrderedItems", param);
                int i = Convert.ToInt32(param[8].Value);
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
