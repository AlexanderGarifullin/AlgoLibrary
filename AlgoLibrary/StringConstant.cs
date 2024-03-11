
namespace AlgoLibrary
{
    public static class StringConstant
    {
        // DataBase
        public static string DbError = "Ошибка: проблемы с базой данных!";

        // Users
        public static string UsersInputError = "Ошибка: некорректные данные! Максимальная длина логина 40 символов! Максимальная длина пароля 256!";
        public static string DeleteHimselfError = "Ошибка: вы не можете удалить себя!";
        public static string UsersDuplicateNameError = "Ошибка: некорректные данные! Уже существует пользователь ";

        // Authorisation
        public static string AuthorisationError = "Ошибка: неверный логин или пароль!";

        //Theme
        public static string ThemeInputError = "Ошибка: некорректные данные! Максимальная длина темы 50 символов!";
        public static string ThemeDuplicateNameError = "Ошибка: некорректные данные! Уже существует тема ";

        // Article
        public static string ArticleInputError = "Ошибка: некорректные данные! Максимальная длина названия конспекта 50 символов!";
        public static string ArticleDuplicateNameError = "Ошибка: некорректные данные! Уже существует конспект ";

        // Folder
        public static string FolderInputError = "Ошибка: некорректные данные! Максимальная длина названия папки 50 символов!";
        public static string FolderDuplicateNameError = "Ошибка: некорректные данные! Уже существует папка ";
    }
}
