using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class DbAccess : DBConnection
    {
        public DbAccess()
        {

        }


        public DataSet spDataSet(string sp, SqlParameter[] param)
        {
            SqlTransaction transaction;
            transaction = connection.BeginTransaction();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sp, connection);
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {
                    foreach (SqlParameter p in param)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                transaction.Commit();                
                return ds;
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    string ex = ex2.Message.ToString();
                    throw;
                }
                throw;
            }
        }
    }
}
