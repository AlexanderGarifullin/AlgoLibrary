using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AlgoLibrary.Models
{
    public class UserModel
    {
        private int userId;
        private string login;
        private string password;
        private string role;
        [Key]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [MaxLength(40)]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [EnumDataType(typeof(UserRole))]
        public string Role
        {
            get { return role; } 
            set { role = value; }
        }
    }
}
