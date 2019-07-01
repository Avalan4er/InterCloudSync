using Google.Apis.Drive.v3;
using System;
using System.IO;

namespace InterCloudSync.GoogleDrive
{
    internal static class Configuration
    {
        /// <summary>
        /// Файл с данными для получения токена Google API
        /// </summary>
        public static string CredentialsPath = Path.Combine(AppContext.BaseDirectory, "credentials.json");

        /// <summary>
        /// Области доступа
        /// </summary>
        public static string[] Scopes = { DriveService.Scope.DriveReadonly };

        /// <summary>
        /// Название приложения
        /// </summary>
        public const string ApplicationName = "Inter Cloud sync";

        /// <summary>
        /// Путь до папки Temp
        /// </summary>
        public const string FileDataStorePath = "InterCloudSync";

        /// <summary>
        /// Поля файлов для запроса
        /// </summary>
        public const string Fields = "nextPageToken, files(id, name)";
    }
}