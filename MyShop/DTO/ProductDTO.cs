using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class ProductDTO
    {
        public int ProId { get; set; }
        public string? ProName { get; set; }
        public double Ram { get; set; }
        public int Rom { get; set; }
        public double ScreenSize { get; set; }
        public string? TinyDes { get; set; }
        public string? FullDes { get; set;}
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        public string? Trademark { get; set;}
        public int BatteryCapacity { get; set; }
        public int? CatID { get; set; }
        public int Quantity { get; set; }
        public int ? Block { get; set; }
    }
}
