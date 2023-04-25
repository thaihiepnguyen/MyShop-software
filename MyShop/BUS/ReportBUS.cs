using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
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

        public List<Decimal> groupPriceTotalByMonth(int year)
        {
            List<Decimal> result = new List<Decimal>();

            for (int month = 1; month <= 12; month++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Month == month && order.CreateAt.Year == year)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.FinalTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<Decimal> groupProfitTotalByMonth(int year)
        {
            List<Decimal> result = new List<Decimal>();

            for (int month = 1; month <= 12; month++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Month == month && order.CreateAt.Year == year)
                    {
                        prices.Add((decimal)order.ProfitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<decimal> groupProfitTotalByWeek(int month, int year)
        {
            List<decimal> result = new List<decimal>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++) // loop over 5 weeks only
            {
                decimal totalPrice = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.CreateAt >= startDate && order.CreateAt <= endDate)
                    {
                        totalPrice += (decimal)order.ProfitTotal;
                    }
                }

                result.Add(totalPrice);

                if (weekCount < 5 && week == weekCount) // if there are less than 5 weeks in the month
                {
                    for (int i = week + 1; i <= 5; i++) // add 0 to the remaining weeks
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }

        public List<decimal> groupPriceTotalByWeek(int month, int year)
        {
            List<decimal> result = new List<decimal>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++) // loop over 5 weeks only
            {
                decimal totalPrice = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.CreateAt >= startDate && order.CreateAt <= endDate)
                    {
                        totalPrice += (decimal)order.FinalTotal;
                    }
                }

                result.Add(totalPrice);

                if (weekCount < 5 && week == weekCount) // if there are less than 5 weeks in the month
                {
                    for (int i = week + 1; i <= 5; i++) // add 0 to the remaining weeks
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }

        public List<Decimal> groupPriceTotalByYear()
        {
            List<Decimal> result = new List<Decimal>();

            for (int year = 2021; year <= 2023; year++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Year == year)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.FinalTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<Decimal> groupProfitTotalByYear()
        {
            List<Decimal> result = new List<Decimal>();

            for (int year = 2021; year <= 2023; year++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Year == year)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.ProfitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public List<Decimal> groupPriceTotalByDate(DateTime startDate, DateTime endDate)
        {
            List<Decimal> result = new List<Decimal>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Date == day)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.FinalTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<Decimal> groupProfitTotalByDate(DateTime startDate, DateTime endDate)
        {
            List<Decimal> result = new List<Decimal>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.CreateAt.Date == day)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.ProfitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<String> EachDayConverter(DateTime startDate, DateTime endDate)
        {
            List<string> result = new List<string>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                result.Add(day.Date.ToString());
            }

            return result;
        }
    }
}
