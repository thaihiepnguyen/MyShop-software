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

        public List<ProductDTO> getAll()
        {
            List<ProductDTO> list = new List<ProductDTO>();
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
                product.Screen_size = (double)reader["ScreenSize"];
                product.Tiny_des = reader["TinyDes"] == DBNull.Value ? null : (string?)reader["TinyDes"];
                product.Full_des = reader["FullDes"] == DBNull.Value ? null : (string?)reader["FullDes"];
                product.Price = (decimal)reader["Price"];
                product.Image_path = reader["ImagePath"] == DBNull.Value ? null : (string?)reader["ImagePath"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.Battery_capacity = (int)reader["BatteryCapacity"];
                product.Cat_ID = reader["CatID"] == DBNull.Value ? null : (int?)reader["CatID"];

                list.Add(product);
            }

            reader.Close();

            return list;
        }


        // TODO: code này là dựa trên mã nguồn của Thầy .
        public Tuple<List<ProductDTO>, int> getProducts(int currentPage = 1, int rowsPerPage = 10,
                string keyword = "")
        {
            var origin = new ObservableCollection<ProductDTO>();
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
                product.Screen_size = (double)reader["ScreenSize"];
                product.Tiny_des = reader["TinyDes"] == DBNull.Value ? null : (string?)reader["TinyDes"];
                product.Full_des = reader["FullDes"] == DBNull.Value ? null : (string?)reader["FullDes"];
                product.Price = (decimal)reader["Price"];
                product.Image_path = reader["ImagePath"] == DBNull.Value ? null : (string?)reader["ImagePath"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.Battery_capacity = (int)reader["BatteryCapacity"];
                product.Cat_ID = reader["CatID"] == DBNull.Value ? null : (int?)reader["CatID"];

                origin.Add(product);
            }

            // TODO: nên handle việc ProName bị null ở đây .
            var list = origin.Where(
                    item => item.ProName.ToLower().Contains(keyword.ToLower())
                );

            var items = list.Skip((currentPage - 1) * rowsPerPage)
                    .Take(rowsPerPage);

            var result = new Tuple<List<ProductDTO>, int>(
                   items.ToList(), list.Count()
               );
            reader.Close();

            return result;
        }

    }
}
