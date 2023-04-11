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
            string sql = "select ProID, ProName, Ram, Rom, Screen_size, Tiny_des," +
                " Full_des, Price, Image_path, Trademark," +
                "Battery_capacity, CatID  from product";
            var command = new SqlCommand(sql, db.connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.ProId = (int)reader["ProID"];
                product.ProName = reader["ProName"] == DBNull.Value ? "Lỗi tên sản phẩm" : (string?)reader["ProName"];
                product.Ram = (double)reader["Ram"];
                product.Rom = (int)reader["Rom"];
                product.Screen_size = (double)reader["Screen_size"];
                product.Tiny_des = reader["Tiny_des"] == DBNull.Value ? null : (string?)reader["Tiny_des"];
                product.Full_des = reader["Full_des"] == DBNull.Value ? null : (string?)reader["Full_des"];
                product.Price = (decimal)reader["Price"];
                product.Image_path = reader["Image_path"] == DBNull.Value ? null : (string?)reader["Image_path"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.Battery_capacity = (int)reader["Battery_capacity"];
                product.Cat_ID = reader["CatID"] == DBNull.Value ? null : (int?)reader["CatID"];

                list.Add(product);
            }

            reader.Close();

            return list;
        }

        public Tuple<List<ProductDTO>, int> getProducts(int currentPage = 1, int rowsPerPage = 10,
                string keyword = "")
        {
            var origin = new ObservableCollection<ProductDTO>();
            string sql = "select ProID, ProName, Ram, Rom, Screen_size, Tiny_des," +
                " Full_des, Price, Image_path, Trademark," +
                "Battery_capacity, CatID  from product";

            var command = new SqlCommand(sql, db.connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.ProId = (int)reader["ProID"];
                product.ProName = reader["ProName"] == DBNull.Value ? "Lỗi tên sản phẩm" : (string?)reader["ProName"];
                product.Ram = (double)reader["Ram"];
                product.Rom = (int)reader["Rom"];
                product.Screen_size = (double)reader["Screen_size"];
                product.Tiny_des = reader["Tiny_des"] == DBNull.Value ? null : (string?)reader["Tiny_des"];
                product.Full_des = reader["Full_des"] == DBNull.Value ? null : (string?)reader["Full_des"];
                product.Price = (decimal)reader["Price"];
                product.Image_path = reader["Image_path"] == DBNull.Value ? null : (string?)reader["Image_path"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.Battery_capacity = (int)reader["Battery_capacity"];
                product.Cat_ID = reader["CatID"] == DBNull.Value ? null : (int?)reader["CatID"];

                origin.Add(product);
            }

            var list = origin.Where(
                    item => item.ProName.Contains(keyword)
                );

            var items = list.Skip((currentPage - 1) * rowsPerPage)
                    .Take(rowsPerPage);

            var result = new Tuple<List<ProductDTO>, int>(
                   items.ToList(), list.Count()
               );

            return result;
        }

    }
}
