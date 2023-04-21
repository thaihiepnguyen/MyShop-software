using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    internal class SaleDAO
    {
        DatabaseUtilitites db = DatabaseUtilitites.getInstance();

        public decimal getSaleByYear(int userID, int year)
        {
           decimal totalSales = 0;
            string query = "SELECT * FROM [Orders] WHERE [UserID] = @UserID AND YEAR([DeliveryDate]) = @Year";


            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@Year", year);
            var reader = command.ExecuteReader();

            while (reader.Read())
            { 
                totalSales = totalSales + reader.GetDecimal(reader.GetOrdinal("Price")); 
            }

            reader.Close(); 
            return totalSales;
        }
        public decimal getSaleByMonth(int userID, int month)
        {
            decimal totalSales = 0;
            string query = "SELECT * FROM [Orders] WHERE [UserID] = @UserId AND MONTH([DeliveryDate]) = @Month";


            var command = new SqlCommand(query, db.connection);

            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@Month", month);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                totalSales = totalSales + reader.GetDecimal(reader.GetOrdinal("Price"));
            }

            reader.Close();
            return totalSales;
        }
        public decimal getSaleByDate(int userID)
        {
            DateTime currentDate = DateTime.Today;

            decimal totalSales = 0;
            string query = "SELECT * FROM [Orders] WHERE [UserID] = @UserId AND [DeliveryDate] = @CurrentDate";


            var command = new SqlCommand(query, db.connection);
            command.Parameters.AddWithValue("@UserId", userID); // Replace 1 with the userId value you want to filter by
            command.Parameters.AddWithValue("@CurrentDate", currentDate);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                totalSales = totalSales + reader.GetDecimal(reader.GetOrdinal("Price"));
            }

            reader.Close();
            return totalSales;
        }
    }
}
