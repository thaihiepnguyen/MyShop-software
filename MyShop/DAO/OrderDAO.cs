using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyShop.DAO
{
    class OrderDAO
    {

        DatabaseUtilitites db = DatabaseUtilitites.getInstance();

        public ObservableCollection<OrderDTO> getOrders(int userID)
        {
            ObservableCollection<OrderDTO> list = new ObservableCollection<OrderDTO>();
            string query = "SELECT * FROM Orders WHERE UserID = @UserID";


            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@UserID", userID);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                OrderDTO order = new OrderDTO
                {
                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                    ProID = reader.GetInt32(reader.GetOrdinal("ProID")),
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    ProName = reader.GetString(reader.GetOrdinal("ProName")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    ImagePath = reader.GetString(reader.GetOrdinal("ImagePath")),
                    OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                    DeliveryDate = reader.GetDateTime(reader.GetOrdinal("DeliveryDate"))
                };
                list.Add(order);
            }

            reader.Close();

            return list;
        }

        public OrderDTO GetOne(int OrderID)
        {
            var query = "SELECT * FROM Orders WHERE OrderID = @OrderID";

            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@OrderID", OrderID);

            var reader = command.ExecuteReader();

            OrderDTO order = null;
            if (reader.Read())
            {
                order = new OrderDTO
                {
                    OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                    ProID = reader.GetInt32(reader.GetOrdinal("ProID")),
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    ProName = reader.GetString(reader.GetOrdinal("ProName")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    ImagePath = reader.GetString(reader.GetOrdinal("ImagePath")),
                    OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                    DeliveryDate = reader.GetDateTime(reader.GetOrdinal("DeliveryDate"))
                };
            }
            reader.Close();
            return order;
        }

        public bool DeleteOne(int OrderID)
        {
            Trace.WriteLine(OrderID);
            var query = "DELETE FROM Orders WHERE OrderID = @OrderIdToDelete";

            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@OrderIdToDelete", OrderID);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        public bool UpdateOne(int OrderID, String newAddress)
        {
            Trace.WriteLine(newAddress);
            Trace.WriteLine(OrderID);
            var query = "UPDATE Orders SET Address = @NewAddress WHERE OrderID = @OrderID";

            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@OrderID", OrderID);
            command.Parameters.AddWithValue("@NewAddress", newAddress);
            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }
        public bool CreateOrder(int proID, int userID, string address, DateTime orderDate, DateTime deliveryDate)
        {
            string query = "INSERT INTO Orders (ProID, UserID, Address, OrderDate, DeliveryDate) VALUES (@ProID, @UserID, @Address, @OrderDate, @DeliveryDate); SELECT SCOPE_IDENTITY()";

            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@ProID", proID);
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@OrderDate", orderDate);
            command.Parameters.AddWithValue("@DeliveryDate", deliveryDate);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

    }
}
