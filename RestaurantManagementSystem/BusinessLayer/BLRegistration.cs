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

        public bool RegisterValues(RegistrationClass registration)
        {
            if (registration == null)
                throw new ArgumentNullException(nameof(registration));

            try
            {
                string procedureName = "sp_InsertRegistration";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@FirstName", registration.FirstName ?? (object)DBNull.Value),
                    new SqlParameter("@LastName", registration.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@EmailId", registration.EmailId ?? (object)DBNull.Value),
                    new SqlParameter("@Password", registration.Password ?? (object)DBNull.Value),
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
                // ✅ Unique constraint violation (Duplicate Email)
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    throw new Exception("Email already exists.");
                }

                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Registration error: " + ex.Message);
            }
        }
    }
}