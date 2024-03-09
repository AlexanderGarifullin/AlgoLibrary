document.addEventListener("DOMContentLoaded", function () {
    // Получить список всех статей вместе с подключенными темами
    var articlesWithThemes = window.articlesWithThemes;

    // Создать объект для хранения статей, сгруппированных по темам
    var groupedArticles = {};

    // Группировать статьи по темам
    articlesWithThemes.forEach(function (article) {
        if (!groupedArticles.hasOwnProperty(article.theme.name)) {
            groupedArticles[article.theme.name] = [];
        }
        groupedArticles[article.theme.name].push(article);
    });

    // Отобразить статьи, сгруппированные по темам
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
            articleLink.href = "#"; // Здесь можно указать ссылку на действительную страницу статьи
            articleLink.dataset.articleId = article.articleId; // Запоминаем идентификатор статьи
            articleLink.addEventListener("click", function (event) {
                event.preventDefault(); // Отменяем действие по умолчанию (переход по ссылке)
                loadArticle(article.articleId); // Загружаем текст статьи по идентификатору
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
