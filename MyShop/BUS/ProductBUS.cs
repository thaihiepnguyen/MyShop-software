using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    class ProductBUS
    {
        private ProductDAO _productDAO;

        public ProductBUS() {
            _productDAO = new ProductDAO();
        }

        public List<ProductDTO> GetProducts()
        {
            return _productDAO.getAll();
        }
    }
}
