using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    class ProductDTO
    {
        public int ProId { get; set; }
        public string? ProName { get; set; }
        public double Ram { get; set; }
        public int Rom { get; set; }
        public double Screen_size { get; set; }
        public string? Tiny_des { get; set; }
        public string? Full_des { get; set;}
        public decimal Price { get; set; }
        public string? Image_path { get; set; }
        public string? Trademark { get; set;}
        public int Battery_capacity { get; set; }
        public int? Cat_ID { get; set; }

    }
}
