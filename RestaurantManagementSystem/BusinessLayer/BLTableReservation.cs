using System.Data;
using Microsoft.Data.SqlClient;
using RestaurantManagementSystem.DataLayer;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.BusinessLayer
{
    public class BLTableReservation
    {
        private readonly SqlServerDB _db;

        public BLTableReservation(SqlServerDB db)
        {
            _db = db;
        }

        public bool InsertReservation(TableResevationClass reservation)
        {
            string procedureName = "sp_InsertReservation";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", reservation.UserName),
                new SqlParameter("@UserEmail", reservation.UserEmail),
                new SqlParameter("@ReservationDateTime", reservation.ReservationDateTime),
                new SqlParameter("@NoOfPeople", reservation.NoOfPeople),
                new SqlParameter("@SpecialAttentions", reservation.SpecialAttentions ?? (object)DBNull.Value)
            };

            int result = _db.ExecuteNonQuery(
                procedureName,
                CommandType.StoredProcedure,
                parameters
            );

            return result > 0;
        }
    }
}
