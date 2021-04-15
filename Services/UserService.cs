using NodeCMBAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NodeCMBAPI.Services
{
    public class UserService : IUserService
    {
        DbAccess access = new DbAccess();
        SqlParameter[] param;
        DataSet ds;

        public List<User> GetUsers()
        {
            param = new SqlParameter[0];
            List<User> lstUsers = new List<User>();
            ds = new DataSet();
            ds = access.spDataSet("spGet_User", param);

            DataTable dt = ds.Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                User user = new User();
                user.ID = Convert.ToInt32(item["ID"]);
                user.FirstName = item["FirstName"].ToString();
                user.Mobile = item["Mobile"].ToString();
                user.Password = item["Password"].ToString();
                user.RestaurentID = Convert.ToInt32(item["RestaurentID"]);
                user.HotelID = Convert.ToInt32(item["HotelID"]);                
                user.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                user.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"]);
                user.CreatedBy = Convert.ToInt32(item["CreatedBy"]);
                user.ModifiedBy = Convert.ToInt32(item["ModifiedBy"]);
                user.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                user.LastName = item["LastName"].ToString();
                user.UserName = item["UserName"].ToString();
                user.passwordHash = (byte[])item["passwordHash"];
                user.passwordSalt = (byte[])item["passwordSalt"];
                lstUsers.Add(user);
            }

            return lstUsers;
        }

        public User Authenticate(string username, string password)
        {
            var lstUsers = GetUsers();
            User user = new User(); 
            foreach (var u in lstUsers)
            {
                if (u.UserName.Equals(username))
                {
                    user = u;
                    break;
                }
            }

            if (user.UserName == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt))
                return null;

            //if (!user.Password.Equals(password))
            //    return null;

            return user;
        }

        public User GetById(int id)
        {
            var lstUsers = GetUsers();
            foreach (var u in lstUsers)
            {
                if (u.ID.Equals(id))
                {
                    return u;
                }
            }

            return null;
        }

        public string Register(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Password))
                    return "Password is required";

                var lstUsers = GetUsers();
                foreach (var item in lstUsers)
                {
                    if (item.UserName.Equals(user.UserName))
                        return "Username \"" + user.UserName + "\" is already taken";
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;

                param = new SqlParameter[16];
                param[0] = new SqlParameter("@FirstName", user.FirstName);
                param[1] = new SqlParameter("@Mobile", user.Mobile);
                param[2] = new SqlParameter("@Password", "");
                param[3] = new SqlParameter("@RestaurentID", Convert.ToInt32(user.RestaurentID));
                param[4] = new SqlParameter("@HotelID", Convert.ToInt32(user.HotelID));
                param[5] = new SqlParameter("@CreatedDate", Convert.ToDateTime(DateTime.Now));
                param[6] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[7] = new SqlParameter("@CreatedBy", Convert.ToInt32(user.CreatedBy));
                param[8] = new SqlParameter("@ModifiedBy", Convert.ToInt32(user.ModifiedBy));
                param[9] = new SqlParameter("@IsAvailable", user.IsAvailable);
                param[10] = new SqlParameter("@LastName", user.LastName);
                param[11] = new SqlParameter("@UserName", user.UserName);
                param[12] = new SqlParameter("@passwordHash", passwordHash);
                param[13] = new SqlParameter("@passwordSalt", passwordSalt);
                param[14] = new SqlParameter("@RoleID", user.RoleID);
                param[15] = new SqlParameter("@ReturnId", "");

                param[15].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spInsert_User", param);
                int i = Convert.ToInt32(param[15].Value);
                return i.ToString();
            }
            catch (Exception e)
            {
                return e.Message.ToString();
                //throw;
            }

        }

        public string ResetPassword(User user)
        {
            try
            {

                if (GetById(user.ID) == null)
                {
                    return "User Not Available";
                }

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;

                param = new SqlParameter[4];
                param[0] = new SqlParameter("@passwordHash", passwordHash);
                param[1] = new SqlParameter("@passwordSalt", passwordSalt);
                param[2] = new SqlParameter("@ID", user.ID);
                param[3] = new SqlParameter("@ReturnId", "");

                param[3].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spReset_Password", param);
                int i = Convert.ToInt32(param[3].Value);
                return i.ToString();
            }
            catch (Exception e)
            {

                return e.Message.ToString();
            }
        }

        public string UpdateUser(User user)
        {
            try
            {
                var lst = GetUsers();
                var item = lst.Any(x => x.ID == user.ID);

                if (item == false)
                {
                    return "Item is not available, please pass relevant item ID";
                }

                param = new SqlParameter[12];
                param[0] = new SqlParameter("@ID", user.ID);
                param[1] = new SqlParameter("@FirstName", user.FirstName);
                param[2] = new SqlParameter("@Mobile", user.Mobile);
                param[3] = new SqlParameter("@Password", "");
                param[4] = new SqlParameter("@RestaurentID", Convert.ToInt32(user.RestaurentID));
                param[5] = new SqlParameter("@HotelID", Convert.ToInt32(user.HotelID));
                param[6] = new SqlParameter("@ModifiedDate", Convert.ToDateTime(DateTime.Now));
                param[7] = new SqlParameter("@ModifiedBy", Convert.ToInt32(user.ModifiedBy));
                param[8] = new SqlParameter("@IsAvailable", user.IsAvailable);
                param[9] = new SqlParameter("@LastName", user.LastName);
                param[10] = new SqlParameter("@UserName", user.UserName);
                param[11] = new SqlParameter("@ReturnId", "");

                param[11].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = access.spDataSet("spUpdate_User", param);
                int i = Convert.ToInt32(param[11].Value);
                return i.ToString();
            }
            catch (Exception e)
            {

                return e.Message.ToString();
            }
        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            //if (password == null) return false;  //throw new ArgumentNullException("password");
            //if (string.IsNullOrWhiteSpace(password)) return false; //throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            //if (storedHash.Length != 64) return false; //throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            //if (storedSalt.Length != 128) return false; //throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}

