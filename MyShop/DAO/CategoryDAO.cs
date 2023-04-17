using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;

namespace MyShop.DAO
{
    class CategoryDAO
    {
        DatabaseUtilitites db = DatabaseUtilitites.getInstance();

        public List<CategoryDTO> getAll()
        {
            List<CategoryDTO> list = new List<CategoryDTO>();

            string sql = "select CatID, CatName, CatIcon from category";

            var command = new SqlCommand(sql, db.connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                CategoryDTO category = new CategoryDTO();
                category.CatID = (int)reader["CatID"];
                category.CatName = reader["CatName"] == DBNull.Value ? "Lỗi tên thể loại" : (string?)reader["CatName"];
                category.CatIcon = (string)reader["CatIcon"];

                list.Add(category);
            }

            reader.Close();

            return list;
        }
        public CategoryDTO getCategoryById(int id)
        {
            List<CategoryDTO> list = new List<CategoryDTO>();
            CategoryDTO result = new CategoryDTO();

            string sql = $"select CatID, CatName, CatIcon from category where CatID = @id";

            var command = new SqlCommand(sql, db.connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                CategoryDTO category = new CategoryDTO();

                category.CatID = (int)reader["CatID"];
                category.CatName = reader["CatName"] == DBNull.Value ? "Lỗi tên thể loại" : (string?)reader["CatName"];
                category.CatIcon = (string)reader["CatIcon"];

                list.Add(category);
            }

            reader.Close();
            result = list[0];

            return result;
        }
    }
}
