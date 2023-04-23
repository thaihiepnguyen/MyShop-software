using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class PurchaseDTO
    {
        public int PurchaseID { get; set; }
        public int OrderID { get; set; }
        public int ProID { get; set; }
        public int Quantity { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}
