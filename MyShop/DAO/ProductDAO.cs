using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace MyShop.DAO
{
    internal class ProductDAO
    {
        DatabaseUtilitites db = DatabaseUtilitites.getInstance();

        public ObservableCollection<ProductDTO> getAll()
        {
            ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();
            string sql = "select ProID, ProName, Ram, Rom, ScreenSize, TinyDes," +
                " FullDes, Price, ImagePath, Trademark," +
                "BatteryCapacity, CatID, Quantity from product";
            var command = new SqlCommand(sql, db.connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.ProId = (int)reader["ProID"];
                product.ProName = reader["ProName"] == DBNull.Value ? "Lỗi tên sản phẩm" : (string?)reader["ProName"];
                product.Ram = (double)reader["Ram"];
                product.Rom = (int)reader["Rom"];
                product.ScreenSize = (double)reader["ScreenSize"];
                product.TinyDes = reader["TinyDes"] == DBNull.Value ? null : (string?)reader["TinyDes"];
                product.FullDes = reader["FullDes"] == DBNull.Value ? null : (string?)reader["FullDes"];
                product.Price = (decimal)reader["Price"];
                product.ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string?)reader["ImagePath"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.BatteryCapacity = (int)reader["BatteryCapacity"];
                product.CatID = reader["CatID"] == DBNull.Value ? null : (int?)reader["CatID"];
                product.Quantity = (int)reader["Quantity"];

                list.Add(product);
            }

            reader.Close();

            return list;
        }


        // TODO: code này là dựa trên mã nguồn của Thầy .
        public ObservableCollection<ProductDTO> getProducts(int currentPage = 1, int rowsPerPage = 10,
                string keyword = "")
        {
            var list = new ObservableCollection<ProductDTO>();
            string sql = "select ProID, ProName, Ram, Rom, ScreenSize, TinyDes," +
                " FullDes, Price, ImagePath, Trademark," +
                "BatteryCapacity, CatID  from product";

            var command = new SqlCommand(sql, db.connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.ProId = (int)reader["ProID"];
                product.ProName = reader["ProName"] == DBNull.Value ? "Lỗi tên sản phẩm" : (string?)reader["ProName"];
                product.Ram = (double)reader["Ram"];
                product.Rom = (int)reader["Rom"];
                product.ScreenSize = (double)reader["ScreenSize"];
                product.TinyDes = reader["TinyDes"] == DBNull.Value ? null : (string?)reader["TinyDes"];
                product.FullDes = reader["FullDes"] == DBNull.Value ? null : (string?)reader["FullDes"];
                product.Price = (decimal)reader["Price"];
                product.ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string?)reader["ImagePath"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.BatteryCapacity = (int)reader["BatteryCapacity"];
                product.CatID = reader["CatID"] == DBNull.Value ? null : (int?)reader["CatID"];

                list.Add(product);
            }

            reader.Close();

            return list;
        }

    }
}
