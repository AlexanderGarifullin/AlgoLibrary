using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace AlgoLibrary.Models
{
    public class ThemeModel
    {
        private int themeID;
        private string name;
        private int orderNumber;

        [Key] 
        public int ThemeId
        {
            get { return themeID; }
            set { themeID = value; }
        }

        [MaxLength(50)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }
    }
}
