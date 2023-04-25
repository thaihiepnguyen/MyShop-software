using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    public class ReportBUS
    {
        // Nói chung là sẽ get all còn phân tích thế nào thì chưa biết :)
        private List<ShopOrderDTO> _orders;
        public ReportBUS()
        {
            var orderDAO = new OrderDAO();
            
            var ob = orderDAO.getAll();

            _orders = ob.ToList();
        }

        // Đã có dữ liệu rồi phân tích dữ liệu nào  :) 

        public Decimal mean(List<Decimal> prices)
        {
            return prices.Sum() / prices.Count;
        }

        public List<List<ShopOrderDTO>> groupOrdersByWeek() {
            var ordersByWeek = _orders.GroupBy(o => CultureInfo.InvariantCulture.Calendar
            // trả ra tuần thứ bao nhiêu ứng với o.CreateAt
            .GetWeekOfYear(o.CreateAt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
            .Select(g => g.ToList())
            .ToList();

            return ordersByWeek;
        }

        public List<List<ShopOrderDTO>> groupOrdersByMonth()
        {
            var ordersByMonth = _orders.GroupBy(o => o.CreateAt.Month)
                                       .Select(g => g.ToList())
                                       .ToList();

            return ordersByMonth;
        }

        public List<List<ShopOrderDTO>> groupOrdersByYear()
        {
            var ordersByYear = _orders.GroupBy(o => o.CreateAt.Year)
                                      .Select(g => g.ToList())
                                      .ToList();

            return ordersByYear;
        }
    }
}
