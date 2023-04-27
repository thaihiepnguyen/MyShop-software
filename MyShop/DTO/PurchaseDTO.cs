using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class PurchaseDTO : INotifyPropertyChanged
    {
        public int PurchaseID { get; set; }
        public int OrderID { get; set; }
        public int ProID { get; set; }
        public int Quantity { get; set; }

        // tổng tiền ở đây là đã có chiết xuất lợi nhuận rồi 
        public Decimal TotalPrice { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
