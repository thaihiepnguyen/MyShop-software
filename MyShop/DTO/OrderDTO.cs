using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
     public class OrderDTO : INotifyPropertyChanged, ICloneable
    {
        public int OrderID { get; set; }
        public int ProID { get; set; }
        public string ProName { get; set; }
        public decimal? Price { get; set; }
        public string ImagePath { get; set; }
        public int UserID { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
