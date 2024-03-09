document.addEventListener("DOMContentLoaded", function () {
    var articlesWithThemes = window.articlesWithThemes;

    var groupedArticles = {};

    articlesWithThemes.sort(function (a, b) {
        return a.theme.orderNumber - b.theme.orderNumber;
    });

    console.log(articlesWithThemes);

    articlesWithThemes.forEach(function (article) {
        if (!groupedArticles.hasOwnProperty(article.theme.name)) {
            groupedArticles[article.theme.name] = [];
        }
        groupedArticles[article.theme.name].push(article);
    });

    Object.keys(groupedArticles).forEach(function (themeName) {
        groupedArticles[themeName].sort(function (a, b) {
            return a.orderNumber - b.orderNumber;
        });
    });

    console.log(groupedArticles);

    var container = document.getElementById("articlesList");
    Object.keys(groupedArticles).forEach(function (themeName) {
        var themeArticles = groupedArticles[themeName];
        var themeItem = document.createElement("li");
        var themeHeader = document.createElement("h3");
        themeHeader.textContent = themeName;
        themeItem.appendChild(themeHeader);
        var articlesList = document.createElement("ul");

        themeArticles.forEach(function (article) {
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
        themeItem.appendChild(articlesList);
        container.appendChild(themeItem);
    });
});


function loadArticle(articleId) {
    window.location.href = "/Home/Index?articleId=" + articleId;
}
