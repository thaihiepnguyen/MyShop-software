using MyShop.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class CategoryDTO: INotifyPropertyChanged
    {
        public int CatID { get; set; }
        public string CatName { get; set; }
        public string CatIcon { get; set; }
        public string CatDescription { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
