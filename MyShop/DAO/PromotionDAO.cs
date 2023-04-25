﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyShop.DTO;
using System.Collections.ObjectModel;
using System.Data;

namespace MyShop.DAO
{
    internal class PromotionDAO
    {
        DatabaseUtilitites db = DatabaseUtilitites.getInstance();


        public ObservableCollection<PromotionDTO> getAll()
        {
            ObservableCollection<PromotionDTO> list = new ObservableCollection<PromotionDTO>();

            string sql = "select IdPromo, PromoCode, DiscountPercent from promotions";

            var command = new SqlCommand(sql, db.connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                PromotionDTO category = new PromotionDTO();
                category.IdPromo = (int)reader["IdPromo"];
                category.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];
                category.DiscountPercent = (int)reader["DiscountPercent"];

                list.Add(category);
            }

            reader.Close();

            return list;
        }
        public PromotionDTO getPromoById(int id)
        {
            List<PromotionDTO> list = new List<PromotionDTO>();
            PromotionDTO result = new PromotionDTO();

            string sql = $"select IdPromo, PromoCode, DiscountPercent from promotions where IdPromo = @id";

            var command = new SqlCommand(sql, db.connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                PromotionDTO category = new PromotionDTO();
                category.IdPromo = (int)reader["IdPromo"];
                category.PromoCode = reader["PromoCode"] == DBNull.Value ? null : (string?)reader["PromoCode"];
                category.DiscountPercent = (int)reader["DiscountPercent"];

                list.Add(category);
            }

            reader.Close();
            result = list[0];

            return result;
        }

        public int insertPromo(PromotionDTO category)
        {
            string sql = "insert into promotions(PromoCode, DiscountPercent)" +
                "values(@PromoCode, @DiscountPercent)";
            var command = new SqlCommand(sql, db.connection);

            command.Parameters.Add("@PromoCode", SqlDbType.NVarChar).Value = category.PromoCode;
            command.Parameters.Add("@DiscountPercent", SqlDbType.Int).Value = category.DiscountPercent;

            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 IdPromo FROM promotions ORDER BY IdPromo DESC ";

            var command1 = new SqlCommand(sql1, db.connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["IdPromo"];
            }

            reader.Close();

            return id;
        }

        public void delPromoById(int idPromo)
        {
            string sql = $"""
                delete promotions 
                where IdPromo = {idPromo}
                """;

            var command = new SqlCommand(sql, db.connection);

            command.ExecuteNonQuery();
        }

        public void updatePromo(PromotionDTO category)
        {
            string sql = "update promotions " +
                "set PromoCode =  @PromoCode, DiscountPercent = @DiscountPercent " +
                "where IdPromo = @IdPromo";
            var command = new SqlCommand(sql, db.connection);

            command.Parameters.Add("@PromoCode", SqlDbType.NVarChar).Value = category.PromoCode;
            command.Parameters.Add("@DiscountPercent", SqlDbType.Int).Value = category.DiscountPercent;
            command.Parameters.Add("@IdPromo", SqlDbType.Int).Value = category.IdPromo;

            command.ExecuteNonQuery();
        }
    }
}
