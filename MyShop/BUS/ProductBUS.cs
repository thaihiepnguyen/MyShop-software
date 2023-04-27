using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;


// BUS : bussiness (login BA)
namespace MyShop.BUS
{
    class ProductBUS
    {
        private ProductDAO _productDAO;

        public ProductBUS() {
            _productDAO = new ProductDAO();
        }

        public ObservableCollection<ProductDTO> getAll() { return _productDAO.getAll(); }

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

        public string uploadImage(FileInfo selectedImage,int id, string key)
        {
            _productDAO.updateImage(id, key);

            var folder = AppDomain.CurrentDomain.BaseDirectory;

            var filePath = $"{folder}/Assets/Images/sp/{key}.png";
            var relativePath = $"Assets/Images/sp/{key}.png";

            File.Copy(selectedImage.FullName, filePath);

            return relativePath;
        }

        public ProductDTO findProductById(int id)
        {
            return _productDAO.getProductById(id);
        }

        public int countTotalProduct()
        {
            return _productDAO.countTotalProduct();
        }

        public ObservableCollection<ProductDTO> getTop5Product()
        {
            return _productDAO.getTop5Product();
        }
    }
}
