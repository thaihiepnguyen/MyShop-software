using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    internal class SaleBUS
    {
        public SaleBUS()
        {
           
        }
        private SaleDAO saleDAO = new SaleDAO();

        public decimal getSaleByYear(int userID, int year)
        {
            return saleDAO.getSaleByYear(userID, year);
        }
        public decimal getSaleByMonth(int userID, int month)
        {
            return saleDAO.getSaleByMonth(userID, month);
        }
        public decimal getSaleByDate(int userID)
        {
            return saleDAO.getSaleByDate(userID);
        }
    }
}
