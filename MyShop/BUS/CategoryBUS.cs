using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<CategoryDTO> getAll() 
        {
            return _categoryDAO.getAll();
        }

        public int addCategory(CategoryDTO category)
        {
            return _categoryDAO.insertCategory(category);
        }

        public void delCategoryById(int catID)
        {
            _categoryDAO.delCategoryById(catID);
        }

        public void updateCategory(CategoryDTO category)
        {
            _categoryDAO.updateCategory(category);
        }
    }
}
