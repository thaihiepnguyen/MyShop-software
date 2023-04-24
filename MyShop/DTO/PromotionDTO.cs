using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class PromotionDTO : INotifyPropertyChanged
    {
        public int IdPromo { get; set; }
        public string PromoCode { get; set; }
        public int DiscountPercent { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}