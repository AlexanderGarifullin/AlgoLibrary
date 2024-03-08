using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace AlgoLibrary.Models
{
    public class FolderModel
    {
        private int folderId;
        private string name;
        private int orderNumber;

        [Key]
        public int FolderId
        {
            get { return folderId; }
            set { folderId = value; }
        }

        [MaxLength(50)]
        [Required]
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
