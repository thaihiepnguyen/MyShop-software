using MyShop.DAO;
using MyShop.DTO;
using MyShop.UI.MainPage.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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

        public Tuple<List<ProductDTO>, int> findProductBySearch(int currentPage = 1, int rowsPerPage = 9,
                string keyword = "", Decimal? startPrice = null, Decimal? endPrice = null)
        {
            var origin = _productDAO.getAll();

            // TODO: nên handle việc ProName bị null ở đây .
            // 
            var list = origin
                .Where((item) =>
                    {
                        bool checkName = item.ProName.ToLower().Contains(keyword.ToLower());
                        bool checkBlock = item.Block == 0;

                        if (startPrice == null || endPrice == null) return checkName && checkBlock;

                        bool checkPrice = item.Price >= startPrice && item.Price <= endPrice;

                        return checkName && checkPrice && checkBlock;
                    });


            var items = list.Skip((currentPage - 1) * rowsPerPage)
                    .Take(rowsPerPage);

            var result = new Tuple<List<ProductDTO>, int>(
                   items.ToList(), list.Count()
               );

            
            return result;
        }

        public void delProduct(int id)
        {
            _productDAO.deleteProductById(id);
        }

        public int saveProduct(ProductDTO product)
        {
            int id = _productDAO.insertProduct(product);

            return id;
        }

        public void patchProduct(ProductDTO product)
        {
            _productDAO.updateProduct(product);
        }

        public void uploadImage(FileInfo selectedImage,int id, string key)
        {
            _productDAO.updateImage(id, key);

            var folder = AppDomain.CurrentDomain.BaseDirectory;

            var filePath = $"{folder}/Assets/Images/sp/{key}.png";

            File.Copy(selectedImage.FullName, filePath);
        }
    }
}
