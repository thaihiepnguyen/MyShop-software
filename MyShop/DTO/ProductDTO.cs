using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class ProductDTO : ICloneable, INotifyPropertyChanged
    {
        public ProductDTO() {
            Block = 0;
        }
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
        public int CatID { get; set; }
        public int Quantity { get; set; }
        public int? Block { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            // Create a new instance of the ProductDTO class
            ProductDTO clonedProduct = new ProductDTO();

            // Set the properties of the cloned object to the same values as the original object
            clonedProduct.ProId = this.ProId;
            clonedProduct.ProName = this.ProName;
            clonedProduct.Ram = this.Ram;
            clonedProduct.Rom = this.Rom;
            clonedProduct.ScreenSize = this.ScreenSize;
            clonedProduct.TinyDes = this.TinyDes;
            clonedProduct.FullDes = this.FullDes;
            clonedProduct.Price = this.Price;
            clonedProduct.ImagePath = this.ImagePath;
            clonedProduct.Trademark = this.Trademark;
            clonedProduct.BatteryCapacity = this.BatteryCapacity;
            clonedProduct.CatID = this.CatID;
            clonedProduct.Quantity = this.Quantity;
            clonedProduct.Block = this.Block;

            return clonedProduct;
        }

        public void copy(ProductDTO other)
        {

            // Set the properties of the cloned object to the same values as the original object
            this.ProId = other.ProId;
            this.ProName = other.ProName;
            this.Ram = other.Ram;
            this.Rom = other.Rom;
            this.ScreenSize = other.ScreenSize;
            this.TinyDes = other.TinyDes;
            this.FullDes = other.FullDes;
            this.Price = other.Price;
            this.ImagePath = other.ImagePath;
            this.Trademark = other.Trademark;
            this.BatteryCapacity = other.BatteryCapacity;
            this.CatID = other.CatID;
            this.Quantity = other.Quantity;
            this.Block = other.Block;
        }
    }
}
