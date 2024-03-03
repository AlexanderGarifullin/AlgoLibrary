using System.ComponentModel.DataAnnotations;

namespace AlgoLibrary.Models
{
    public class UserModel
    {
        private int userId;
        private string login;
        private int password;
        private string role;
        [Key]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public int Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Role
        {
            get { return role; } 
            set { role = value; }
        }
    }
}
