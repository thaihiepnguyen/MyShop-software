using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    internal class UserDTO
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        // 0 -> male, 1 -> female
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string AvatarPath { get; set; }
        public byte IsHide { get; set; }
        public int? RoleID { get; set; }
    }
}
