using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class NodeCustomersService : INodeCustomersService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<Node_Customers> GetNodeCustomers()
        {
            param = new SqlParameter[0];
            List<Node_Customers> lstNodeCustomers = new List<Node_Customers>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_NodeCustomers", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                Node_Customers nodeCustomers = new Node_Customers();
                nodeCustomers.ID = Convert.ToInt32(item["ID"]);
                nodeCustomers.Name = item["Name"].ToString();
                nodeCustomers.Address = item["Address"].ToString();
                nodeCustomers.Mobile = item["Mobile"].ToString();
                nodeCustomers.LandPhone = item["LandPhone"].ToString();
                nodeCustomers.Country = item["Country"].ToString();
                nodeCustomers.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                nodeCustomers.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                nodeCustomers.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                nodeCustomers.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                lstNodeCustomers.Add(nodeCustomers);
            }

            return lstNodeCustomers;
        }

        public Node_Customers GetById(int id)
        {
            var lstNodeCustomers = GetNodeCustomers();
            foreach (var u in lstNodeCustomers)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string AddNodeCustomers(Node_Customers nodeCustomers)
        {
            try
            {

                param = new SqlParameter[10];
                param[0] = new SqlParameter("@Name", nodeCustomers.Name);
                param[1] = new SqlParameter("@Address", nodeCustomers.Address);
                param[2] = new SqlParameter("@Mobile", nodeCustomers.Mobile);
                param[3] = new SqlParameter("@LandPhone", nodeCustomers.LandPhone);
                param[4] = new SqlParameter("@Country", nodeCustomers.Country);
                param[5] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[7] = new SqlParameter("@CreatedBy", Convert.ToInt32(nodeCustomers.CreatedBy));
                param[8] = new SqlParameter("@ModifiedBy", Convert.ToInt32(nodeCustomers.ModifiedBy));
                param[9] = new SqlParameter("@ReturnId", "");

                param[9].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_NodeCustomers", param);
                int i = Convert.ToInt32(param[9].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string UpdateNodeCustomers(Node_Customers nodeCustomers)
        {
            try
            {
                var lst = GetNodeCustomers();
                var item = lst.Any(x => x.ID == nodeCustomers.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[9];
                param[0] = new SqlParameter("@ID", nodeCustomers.ID);
                param[1] = new SqlParameter("@Name", nodeCustomers.Name);
                param[2] = new SqlParameter("@Address", nodeCustomers.Address);
                param[3] = new SqlParameter("@Mobile", nodeCustomers.Mobile);
                param[4] = new SqlParameter("@LandPhone", nodeCustomers.LandPhone);
                param[5] = new SqlParameter("@Country", nodeCustomers.Country);
                param[6] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(nodeCustomers.ModifiedBy));
                param[8] = new SqlParameter("@ReturnId", "");

                param[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_NodeCustomers", param);
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
