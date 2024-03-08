using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoLibrary.Models
{
    public class Folder_ArticleModel
    {
        private int folderArticleId;
        private int folderId;
        private int articleId;
        private int orderNumber;

        [Key]
        public int FolderArticleId
        {
            get { return folderArticleId; }
            set { folderArticleId = value; }
        }
        [ForeignKey("FolderModel")]
        public int FolderId
        {
            get { return folderId; }
            set { folderId = value; }
        }
        [ForeignKey("ArticleId")]
        public int ArticleId
        {
            get { return articleId; }
            set { articleId = value; }
        }
        public int OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }
    }
}
