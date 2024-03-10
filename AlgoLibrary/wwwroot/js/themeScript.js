﻿function swapRows(currentRow, targetRow) {
    var table = document.getElementById("themeTable");
    var rows = table.getElementsByTagName("tr");

    var currentThemeId = rows[currentRow].getAttribute("data-theme-id");
    var currentHTML = rows[currentRow].innerHTML;

    rows[currentRow].setAttribute("data-theme-id", rows[targetRow].getAttribute("data-theme-id"));
    rows[currentRow].innerHTML = rows[targetRow].innerHTML;

    rows[targetRow].setAttribute("data-theme-id", currentThemeId);
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
    var table = document.getElementById("themeTable");
    if (currentRow < table.rows.length - 1) {
        swapRows(currentRow, currentRow + 1);
    }
}

function saveOrder() {
    var table = document.getElementById("themeTable");
    var themeIds = [];
    for (var i = 1; i < table.rows.length; i++) {
        var themeId = table.rows[i].dataset.themeId;
        themeIds.push(themeId);
    }
    $.ajax({
        type: "POST",
        url: "/Theme/SaveOrder",
        data: { themeIds: themeIds },
        success: function (data) {
            var successMessage = document.getElementById("successMessage");
            successMessage.style.display = "block";
            console.log("Порядок сохранен успешно!");
        },
        error: function (xhr, status, error) {
            var successMessage = document.getElementById("successMessage");
            successMessage.textContent = "Порядок не сохранен, произошла ошибка!";
            successMessage.style.display = "block";
            successMessage.style.color = "#e74c3c"; 
            console.error("Ошибка сохранения порядка: " + error);
        }
    });
}
