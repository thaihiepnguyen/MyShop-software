using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;


// BUS : bussiness (login BA)
namespace MyShop.BUS
{
    class ProductBUS
    {
        private ProductDAO _productDAO;

        public ProductBUS() {
            _productDAO = new ProductDAO();
        }

        public Tuple<List<ProductDTO>, int> findProductBySearch(int currentPage = 1, int rowsPerPage = 10,
                string keyword = "", Decimal? startPrice = null, Decimal? endPrice = null)
        {
            var origin = _productDAO.getAll();

            // TODO: nên handle việc ProName bị null ở đây .
            // 
            var list = origin
                .Where((item) =>
                    {
                        bool checkName = item.ProName.ToLower().Contains(keyword.ToLower());

                        if (startPrice == null || endPrice == null) return checkName;

                        bool checkPrice = item.Price >= startPrice && item.Price <= endPrice;

                        return checkName && checkPrice;
                    });


            var items = list.Skip((currentPage - 1) * rowsPerPage)
                    .Take(rowsPerPage);

            var result = new Tuple<List<ProductDTO>, int>(
                   items.ToList(), list.Count()
               );

            
            return result;
        }
    }
}
