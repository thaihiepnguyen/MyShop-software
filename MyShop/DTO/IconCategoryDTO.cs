using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class IconCategoryDTO
    {
        public string CatIcon { get; set; }

        public IconCategoryDTO(string catIcon)
        {
            CatIcon = catIcon;
        }
    }
}
