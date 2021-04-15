using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class OrderService : IOrderService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddOrder(Order ot)
        {
            try
            {

                param = new SqlParameter[14];
                param[0] = new SqlParameter("@TableID", Convert.ToInt32(ot.TableID));
                param[1] = new SqlParameter("@TaxID", Convert.ToInt32(ot.TaxID));
                param[2] = new SqlParameter("@GrossPrice", Convert.ToDouble(ot.GrossPrice));
                param[3] = new SqlParameter("@NetPrice", Convert.ToDouble(ot.NetPrice));
                param[4] = new SqlParameter("@TaxAmount", Convert.ToDouble(ot.TaxAmount));
                param[5] = new SqlParameter("@OrderTypeID", Convert.ToInt32(ot.OrderTypeID));
                param[6] = new SqlParameter("@OrderPrepTimer", ot.OrderPrepTimer);
                param[7] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[8] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[9] = new SqlParameter("@CreatedBy", Convert.ToInt32(ot.CreatedBy));
                param[10] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ot.ModifiedBy));
                param[11] = new SqlParameter("@StartTime", Convert.ToDateTime(ot.StartTime));
                param[12] = new SqlParameter("@EndTime", Convert.ToDateTime(ot.EndTime));
                param[13] = new SqlParameter("@ReturnId", "");

                param[13].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_Order", param);
                int i = Convert.ToInt32(param[13].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public Order GetById(int id)
        {
            var lstOrder = GetOrder();
            foreach (var u in lstOrder)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<Order> GetOrder()
        {
            param = new SqlParameter[0];
            List<Order> lstOrder = new List<Order>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_Order", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Order order = new Order();
                order.ID = Convert.ToInt32(item["ID"]);
                order.TableID = Convert.ToInt32(item["TableID"]);
                order.TaxID = Convert.ToInt32(item["TaxID"]);
                order.GrossPrice = Convert.ToDouble(item["GrossPrice"]);
                order.NetPrice = Convert.ToDouble(item["NetPrice"]);
                order.TaxAmount = Convert.ToDouble(item["TaxAmount"]);
                order.OrderTypeID = Convert.ToInt32(item["OrderTypeID"]);
                order.OrderPrepTimer = item["OrderPrepTimer"].ToString();
                order.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                order.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                order.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                order.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                order.StartTime = Convert.ToDateTime(item["StartTime"]);
                order.EndTime = Convert.ToDateTime(item["EndTime"]);
                lstOrder.Add(order);
            }

            return lstOrder;
        }

        public string UpdateOrder(Order ot)
        {
            try
            {
                var lst = GetOrder();
                var item = lst.Any(x => x.ID == ot.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[13];
                param[0] = new SqlParameter("@ID", Convert.ToInt32(ot.ID));
                param[1] = new SqlParameter("@TableID", Convert.ToInt32(ot.TableID));
                param[2] = new SqlParameter("@TaxID", Convert.ToInt32(ot.TaxID));
                param[3] = new SqlParameter("@GrossPrice", Convert.ToDouble(ot.GrossPrice));
                param[4] = new SqlParameter("@NetPrice", Convert.ToDouble(ot.NetPrice));
                param[5] = new SqlParameter("@TaxAmount", Convert.ToDouble(ot.TaxAmount));
                param[6] = new SqlParameter("@OrderTypeID", Convert.ToInt32(ot.OrderTypeID));
                param[7] = new SqlParameter("@OrderPrepTimer", ot.OrderPrepTimer);
                param[8] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[9] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ot.ModifiedBy));
                param[10] = new SqlParameter("@StartTime", Convert.ToDateTime(ot.StartTime));
                param[11] = new SqlParameter("@EndTime", Convert.ToDateTime(ot.EndTime));
                param[12] = new SqlParameter("@ReturnId", "");

                param[12].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_Order", param);
                int i = Convert.ToInt32(param[12].Value);
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
