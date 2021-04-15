using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using NodeCMBAPI.Models;

namespace NodeCMBAPI.Services
{
    public class OrderTypeService : IOrderTypeService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;
        public string AddOrderType(OrderType ot)
        {
            try
            {

                param = new SqlParameter[7];
                param[0] = new SqlParameter("@Type", ot.Type);                
                param[1] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[2] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@CreatedBy", Convert.ToInt32(ot.CreatedBy));
                param[4] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ot.ModifiedBy));
                param[5] = new SqlParameter("@IsAvailable", ot.IsAvailable);
                param[6] = new SqlParameter("@ReturnId", "");

                param[6].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_OrderType", param);
                int i = Convert.ToInt32(param[6].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }
        }

        public OrderType GetById(int id)
        {
            var lstOrderType = GetOrderType();
            foreach (var u in lstOrderType)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public List<OrderType> GetOrderType()
        {
            param = new SqlParameter[0];
            List<OrderType> lstOrderType = new List<OrderType>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_OrderType", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                OrderType ot = new OrderType();
                ot.ID = Convert.ToInt32(item["ID"]);
                ot.Type = item["Type"].ToString();
                ot.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                ot.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                ot.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                ot.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                ot.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstOrderType.Add(ot);
            }

            return lstOrderType;
        }

        public string UpdateOrderType(OrderType ot)
        {
            try
            {
                var lst = GetOrderType();
                var item = lst.Any(x => x.ID == ot.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[6];
                param[0] = new SqlParameter("@ID", ot.ID);
                param[1] = new SqlParameter("@Type", ot.Type);
                param[2] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[3] = new SqlParameter("@ModifiedBy", Convert.ToInt32(ot.ModifiedBy));
                param[4] = new SqlParameter("@IsAvailable", ot.IsAvailable);
                param[5] = new SqlParameter("@ReturnId", "");

                param[5].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_OrderType", param);
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
