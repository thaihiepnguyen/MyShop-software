using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    class CategoryBUS
    {
        private CategoryDAO _categoryDAO;

        public CategoryBUS()
        {
            _categoryDAO= new CategoryDAO();
        }

        public CategoryDTO getCategoryById(int id)
        {
            return _categoryDAO.getCategoryById(id);
        }

        public List<CategoryDTO> getAll() 
        {
            return _categoryDAO.getAll();
        }
    }
}
