function swapRows(currentRow, targetRow) {
    var table = document.getElementById("addedArticlesTable");
    var rows = table.getElementsByTagName("tr");

    var currentThemeId = rows[currentRow].getAttribute("data-article-id");
    var currentHTML = rows[currentRow].innerHTML;

    rows[currentRow].setAttribute("data-article-id", rows[targetRow].getAttribute("data-article-id"));
    rows[currentRow].innerHTML = rows[targetRow].innerHTML;

    rows[targetRow].setAttribute("data-article-id", currentThemeId);
    rows[targetRow].innerHTML = currentHTML;
}

function moveRowUp(button) {
    var currentRow = button.parentNode.parentNode.rowIndex;
    if (currentRow > 1) {
        swapRows(currentRow, currentRow - 1);
    }
}

function moveRowDown(button) {
    var currentRow = button.parentNode.parentNode.rowIndex;
    var table = document.getElementById("addedArticlesTable");
    if (currentRow < table.rows.length - 1) {
        swapRows(currentRow, currentRow + 1);
    }
}

function saveOrder() {
    var table = document.getElementById("addedArticlesTable");
    var articlesIds = [];
    var folderId = document.getElementById("folderId").value;
    for (var i = 1; i < table.rows.length; i++) {
        var articleId = table.rows[i].dataset.articleId;
        articlesIds.push(articleId);
    }
    console.log(articlesIds);
    $.ajax({
        type: "POST",
        url: "/FolderArticle/SaveOrder",
        data: { articlesIds: articlesIds, currentFolderId: folderId },
        success: function (data) {
            var successMessage = document.getElementById("successMessage");
            successMessage.style.display = "block";
            console.log("Порядок сохранен успешно!");
        },
        error: function (xhr, status, error) {
            console.error("Ошибка сохранения порядка: " + error);
        }
    });
}

