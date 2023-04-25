using Microsoft.Data.SqlClient;
using MyShop.DTO;
using MyShop.BUS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace MyShop.DAO
{
    internal class ProductDAO
    {
        DatabaseUtilitites db = DatabaseUtilitites.getInstance();
        private ObservableCollection<PromotionDTO> _promotion = (new PromotionDAO()).getAll();
        public ObservableCollection<ProductDTO> getAll()
        {
            ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();
            string sql = "select ProID, ProName, Ram, Rom, ScreenSize, TinyDes," +
                " FullDes, Price, ImagePath, Trademark," +
                "BatteryCapacity, CatID, Quantity,PromoCode, Block from product";
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
                product.ImagePath = reader["ImagePath"] == DBNull.Value ? "Assets/Images/sp/404.png" : (string?)reader["ImagePath"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.BatteryCapacity = (int)reader["BatteryCapacity"];
                product.CatID = (int)reader["CatID"];
                product.Quantity = (int)reader["Quantity"];
                product.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];
                product.Block = (int)reader["Block"];

                if (product.PromoCode != null)
                {
                    var item = _promotion.FirstOrDefault(p => p.PromoCode == product.PromoCode);
                    if (item != null)
                    {
                        product.OriginPrice = product.Price;
                        product.Price = (product.Price * (100 - item.DiscountPercent)) / 100;
                    }
                }
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
                "BatteryCapacity, CatID, PromoCode  from product";

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
                product.ImagePath = reader["ImagePath"] == DBNull.Value ? "Assets/Images/sp/404.png" : (string?)reader["ImagePath"];
                product.Trademark = reader["Trademark"] == DBNull.Value ? null : (string?)reader["Trademark"];
                product.BatteryCapacity = (int)reader["BatteryCapacity"];
                product.CatID = (int)reader["CatID"];
                product.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];

                list.Add(product);
            }

            reader.Close();

            return list;
        }


        //xóa nhưng thật sự là không có xóa :))) 
        public void deleteProductById(int ProId)
        {
            string sql = $"""
                update product 
                set Block = {1}

                where ProID = {ProId}
                """;

            var command = new SqlCommand(sql, db.connection);

            command.ExecuteNonQuery();
        }

        // insert và trả ra id
        public int insertProduct(ProductDTO productDTO)
        {
            // insert SQL
            string sql = "insert into product(ProName, Ram, Rom, ScreenSize, TinyDes, Price, Trademark, BatteryCapacity, CatID, Quantity,Block)" +
            "values(@ProName, @Ram, @Rom, @ScreenSize, @TinyDes, @Price, @Trademark, @BatteryCapacity, @CatID, @Quantity,@Block)";
            var command = new SqlCommand(sql, db.connection);

            command.Parameters.Add("@ProName", SqlDbType.NVarChar).Value = productDTO.ProName;
            command.Parameters.Add("@Ram", SqlDbType.Float).Value = productDTO.Ram;
            command.Parameters.Add("@Rom", SqlDbType.Int).Value = productDTO.Rom;
            command.Parameters.Add("@ScreenSize", SqlDbType.Float).Value = productDTO.ScreenSize;
            command.Parameters.Add("@TinyDes", SqlDbType.NVarChar).Value = productDTO.TinyDes;
            command.Parameters.Add("@Price", SqlDbType.Money).Value = productDTO.Price;
            command.Parameters.Add("@Trademark", SqlDbType.Text).Value = productDTO.Trademark;
            command.Parameters.Add("@BatteryCapacity", SqlDbType.Int).Value = productDTO.BatteryCapacity;
            command.Parameters.Add("@CatID", SqlDbType.Int).Value = productDTO.CatID;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = productDTO.Quantity;
            //command.Parameters.Add("@PromoCode", SqlDbType.NVarChar).Value = productDTO.PromoCode;
            command.Parameters.Add("@Block", SqlDbType.Int).Value = productDTO.Block;

            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 ProID FROM product ORDER BY ProID DESC ";

            var command1 = new SqlCommand(sql1, db.connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["ProID"];
            }

            reader.Close();

            return id;
        }

        public void updateProduct(ProductDTO productDTO)
        {
            string sql = "update product " +
                "set ProName =  @ProName, Ram = @Ram, Rom = @Rom, ScreenSize = @ScreenSize, TinyDes = @TinyDes," +
                "Price = @Price, Trademark = @Trademark, BatteryCapacity = @BatteryCapacity," +
                "CatID = @CatID, Quantity = @Quantity, Block = @Block " +
                "where ProID = @ProID";
            var command = new SqlCommand(sql, db.connection);

            command.Parameters.Add("@ProID", SqlDbType.Int).Value = productDTO.ProId;
            command.Parameters.Add("@ProName", SqlDbType.NVarChar).Value = productDTO.ProName;
            command.Parameters.Add("@Ram", SqlDbType.Float).Value = productDTO.Ram;
            command.Parameters.Add("@Rom", SqlDbType.Int).Value = productDTO.Rom;
            command.Parameters.Add("@ScreenSize", SqlDbType.Float).Value = productDTO.ScreenSize;
            command.Parameters.Add("@TinyDes", SqlDbType.NVarChar).Value = productDTO.TinyDes;
            command.Parameters.Add("@Price", SqlDbType.Money).Value = productDTO.Price;
            command.Parameters.Add("@Trademark", SqlDbType.Text).Value = productDTO.Trademark;
            command.Parameters.Add("@BatteryCapacity", SqlDbType.Int).Value = productDTO.BatteryCapacity;
            command.Parameters.Add("@CatID", SqlDbType.Int).Value = productDTO.CatID;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = productDTO.Quantity;
            //command.Parameters.Add("@PromoCode", SqlDbType.NVarChar).Value = productDTO.PromoCode;
            command.Parameters.Add("@Block", SqlDbType.Int).Value = productDTO.Block;

            command.ExecuteNonQuery();
        }
        public void updateImage(int id, string key)
        {
            // update SQL
            string sql = $"""
                update product 
                set ImagePath = 'Assets/Images/sp/{key}.png'
                where ProID = {id}
                """;

            var command = new SqlCommand(sql, db.connection);

            command.ExecuteNonQuery();
        }

        public int countTotalProduct()
        {
            int total = 0;
            string sql = $"""
                select sum(Quantity) as sum from product
                """;

            var command = new SqlCommand(sql, db.connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                total = (int)reader["sum"];
            }

            reader.Close();

            return total;
        }

        public ProductDTO getProductById(int id) 
        {
            List<ProductDTO> list = new List<ProductDTO>();
            ProductDTO result = new ProductDTO();

            string sql = "select ProID, ProName, Ram, Rom, ScreenSize, TinyDes," +
                " FullDes, Price, ImagePath, Trademark," +
                "BatteryCapacity, CatID, Quantity, PromoCode, Block from product where ProID = @id";

            var command = new SqlCommand(sql, db.connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

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
                product.CatID = (int)reader["CatID"];
                product.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];
                product.Quantity = (int)reader["Quantity"];

                list.Add(product);
            }

            reader.Close();
            result = list[0];

            return result;
        }

        public ObservableCollection<ProductDTO> getTop5Product()
        {
            ObservableCollection<ProductDTO> list = new ObservableCollection<ProductDTO>();
            string sql = "select top 5 ProID, ProName, Ram, Rom, ScreenSize, TinyDes," +
                " FullDes, Price, ImagePath, Trademark," +
                "BatteryCapacity, CatID, Quantity,  PromoCode, Block from product where Quantity <= 5 order by Quantity asc";
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
                product.CatID = (int)reader["CatID"];
                product.Quantity = (int)reader["Quantity"];
                product.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];
                product.Block = (int)reader["Block"];

                list.Add(product);
            }

            reader.Close();

            return list;
        }
    }
}
