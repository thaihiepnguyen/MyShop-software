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
    public class CustomerBUS
    {
        private CustomerDAO _customerDAO;

        public CustomerBUS()
        {
            _customerDAO = new CustomerDAO();
        }

        public ObservableCollection<CustomerDTO> getAll() { return _customerDAO.getAll(); }

        public int addCustomer(CustomerDTO customerDTO)
        {
            return _customerDAO.insertCustomer(customerDTO);
        }

        public string getNameById(int cusID)
        {
            return _customerDAO.getNameById(cusID);
        }

        public CustomerDTO findCustomerById(int cusID)
        {
            return _customerDAO.getCustomerById(cusID);
        }
    }
}
