using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoLibrary.Models
{
    public class UserModel
    {
        private int userId;
        private string login;
        private string password;
        private string role;
        [Key]
        [HiddenInput(DisplayValue = false)]
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

        [Column(TypeName = "enum('Admin', 'Moderator', 'User')")]
        public string Role
        {
            get { return role; } 
            set { role = value; }
        }
    }
}
