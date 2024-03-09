using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoLibrary.Models
{
    public class ArticleModel
    {
        private int articleId;
        private string name;
        private string text;
        private int themeId;
        private int orderNumber;
        private ThemeModel themeModel;

        [Key]
        public int ArticleId
        {
            get { return articleId; }
            set { articleId = value; }
        }

        [MaxLength(50)]
        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [Required]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        [ForeignKey("ThemeModel")]
        public int ThemeId
        {
            get { return themeId; }
            set { themeId = value; }
        }
        public ThemeModel Theme 
        {   get { return themeModel; }
            set { themeModel = value; } 
        }
        public int OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }
    }
}
