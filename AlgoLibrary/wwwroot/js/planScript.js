document.addEventListener("DOMContentLoaded", function () {
    var folderArticles = window.folderArticles;

    var groupedArticles = {};

    folderArticles.forEach(function (folderArticle) {
        var level = folderArticle.folder.name;
        if (!groupedArticles.hasOwnProperty(level)) {
            groupedArticles[level] = [];
        }
        groupedArticles[level].push(folderArticle.article);
    });

    console.log(groupedArticles);

    var container = document.getElementById("articlesList");
    Object.keys(groupedArticles).forEach(function (level) {
        var articles = groupedArticles[level];
        var levelItem = document.createElement("li");
        var levelHeader = document.createElement("h3");
        levelHeader.textContent = level;
        levelItem.appendChild(levelHeader);
        var articlesList = document.createElement("ul");

        articles.forEach(function (article) {
            var articleItem = document.createElement("li");
            var articleLink = document.createElement("a");
            articleLink.textContent = article.name;
            articleLink.href = "#";
            articleLink.dataset.articleId = article.articleId;
            articleLink.addEventListener("click", function (event) {
                event.preventDefault();
                loadArticle(article.articleId);
            });
            articleItem.appendChild(articleLink);
            articlesList.appendChild(articleItem);
        });
        levelItem.appendChild(articlesList);
        container.appendChild(levelItem);
    });
});


function loadArticle(articleId) {
    window.location.href = "/Plan/Plan?articleId=" + articleId;
}