using System;
using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantManagementSystem.DataLayer;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.BusinessLayer
{
    public class BLRegistration
    {
        private readonly SqlServerDB _db;

        public BLRegistration(SqlServerDB db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // =========================
        // USER REGISTRATION
        // =========================
        public bool RegisterValues(RegistrationClass registration)
        {
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));

            try
            {
                string procedureName = "sp_InsertRegistration";

                SqlParameter[] sqlParameters =
                {
                    new SqlParameter("@FirstName", registration.FirstName ?? (object)DBNull.Value),
                    new SqlParameter("@LastName", registration.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@EmailId", registration.EmailId ?? (object)DBNull.Value),
                    new SqlParameter("@Password", registration.Password ?? (object)DBNull.Value)
                };

                int result = _db.ExecuteNonQuery(
                    procedureName,
                    CommandType.StoredProcedure,
                    sqlParameters
                );

                return result > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    throw new Exception("Email already exists.");
                }

                throw;
            }
        }

        // =========================
        // USER LOGIN
        // =========================
        public RegistrationClass ValidateLogin(LoginClass login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));

            try
            {
                string procedureName = "sp_ValidateLogin";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@EmailId", login.EmailId ?? (object)DBNull.Value),
                    new SqlParameter("@Password", login.Password ?? (object)DBNull.Value)
                };

                DataTable dt = _db.GetDataTable(
                    procedureName,
                    CommandType.StoredProcedure,
                    parameters
                );

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    return new RegistrationClass
                    {
                        FirstName = row["FirstName"] != DBNull.Value ? row["FirstName"].ToString() : "",
                        LastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "",
                        EmailId = row["EmailId"] != DBNull.Value ? row["EmailId"].ToString() : "",

                    };
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}