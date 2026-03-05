using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantManagementSystem.DataLayer;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.BusinessLayer
{
    public class BLLogin
    {
        private readonly SqlServerDB _db;

        public BLLogin(SqlServerDB db)
        {
            _db = db;
        }

        public RegistrationClass ValidateLogin(LoginClass login)
        {
            try
            {
                string procedureName = "sp_ValidateLogin";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@EmailId", login.EmailId),
                    new SqlParameter("@Password", login.Password)
                };

              
               

                return null; // Login failed
            }
            catch (Exception ex)
            {
                throw new Exception("Login Error: " + ex.Message);
            }
        }
    }
}