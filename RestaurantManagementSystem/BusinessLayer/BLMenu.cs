using Microsoft.Data.SqlClient;
using RestaurantManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace RestaurantManagementSystem.BusinessLayer
{
    public class BLMenu
    {
        private readonly string connectionString;

        public BLMenu(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ======================================
        // GET ALL MENU ITEMS
        // ======================================
        public List<object> GetMenu()
        {
            List<object> menuList = new List<object>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM MenuNew";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    byte[] imageBytes = reader["image"] as byte[];

                    string base64Image = null;

                    if (imageBytes != null)
                    {
                        base64Image = Convert.ToBase64String(imageBytes);
                    }

                    menuList.Add(new
                    {
                        menu_id = Convert.ToInt32(reader["menu_id"]),
                        category_id = Convert.ToInt32(reader["category_id"]),
                        item_name = reader["item_name"].ToString(),
                        Description = reader["Description"].ToString(),
                        price = Convert.ToInt32(reader["price"]),
                        image = base64Image,
                        is_available = Convert.ToBoolean(reader["is_available"])
                    });
                }
            }

            return menuList;
        }

        // ======================================
        // ADD MENU ITEM
        // ======================================
        public string AddMenuItem(MenuClass MenuNew)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertMenuItem", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@category_id", MenuNew.category_id);
                cmd.Parameters.AddWithValue("@item_name", MenuNew.item_name);
                cmd.Parameters.AddWithValue("@Description", MenuNew.Description);
                cmd.Parameters.AddWithValue("@price", MenuNew.price);
                cmd.Parameters.AddWithValue("@image", MenuNew.image);
                cmd.Parameters.AddWithValue("@is_available", MenuNew.is_available);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            return "Menu item added successfully!";
        }

        // ======================================
        // UPDATE MENU ITEM
        // ======================================
        public bool UpdateMenuItem(int menuId, MenuClass menu)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateMenuItem", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@menu_id", menuId);
                cmd.Parameters.AddWithValue("@category_id", menu.category_id);
                cmd.Parameters.AddWithValue("@item_name", menu.item_name);
                cmd.Parameters.AddWithValue("@Description", menu.Description);
                cmd.Parameters.AddWithValue("@price", menu.price);
                cmd.Parameters.AddWithValue("@image", menu.image);
                cmd.Parameters.AddWithValue("@is_available", menu.is_available);

                con.Open();

                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }

        // ======================================
        // DELETE MENU ITEM
        // ======================================
        public bool DeleteMenuItem(int menuId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteMenuItem", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@menu_id", menuId);

                con.Open();

                int rows = cmd.ExecuteNonQuery();

                return rows > 0;
            }
        }
    }
}