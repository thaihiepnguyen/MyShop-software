using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    internal class UserBUS
    {
        private UserDAO _userDAO;

        public UserBUS()
        {
            _userDAO = new UserDAO();
        }

        public UserDTO getOne(string username, string password)
        {
            return _userDAO.GetOne(username, password);
        }
        public bool createUser(UserDTO user)
        {
            return _userDAO.CreateUser(user);
        }

    }
}
