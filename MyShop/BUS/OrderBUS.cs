using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    internal class OrderBUS
    {
        private OrderDAO orderDAO;

        public OrderBUS()
        {
            orderDAO = new OrderDAO();
        }
        public Tuple<List<OrderDTO>, int> findOderBySearch(int userID,int currentPage = 1, int rowsPerPage = 6,
             string keyword = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            var origin = orderDAO.getOrders(userID);

            // TODO: nên handle việc ProName bị null ở đây .
            // 
            var list = origin
       .Where((item) =>
       {
           bool checkName = item.ProName.ToLower().Contains(keyword.ToLower());

           bool checkStartDate = startDate == null || item.OrderDate >= startDate;
           bool checkEndDate = endDate == null || item.OrderDate <= endDate;

           return checkName && checkStartDate && checkEndDate;
       });


            var items = list.Skip((currentPage - 1) * rowsPerPage)
                    .Take(rowsPerPage);

            var result = new Tuple<List<OrderDTO>, int>(
                   items.ToList(), list.Count()
               );


            return result;
        }


        public bool createUser(int proID, int userID, string address, DateTime orderDate, DateTime deliveryDate)
        {
            return orderDAO.CreateOrder( proID,  userID,  address,  orderDate,  deliveryDate);
        }
/*        public List<OrderDTO>(int userID)
        {
            return OrderDAO.GetOrders(userID);
        }*/
        public OrderDTO GetOne(int data)
        {
            return orderDAO.GetOne(data);
        }
        public bool DeleteOne(int orderID)
        {
            return orderDAO.DeleteOne(orderID);
        }
        public bool UpdateOne(int orderID, String newAdress)
        {
            return orderDAO.UpdateOne(orderID, newAdress);
        }
    }
}
