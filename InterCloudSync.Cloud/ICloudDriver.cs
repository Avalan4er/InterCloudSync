using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace InterCloudSync.Cloud
{
    /// <summary>
    /// Драйвер облачного хранилища
    /// </summary>
    public interface ICloudDriver
    {
        /// <summary>
        /// Авторизация выполнена
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Выполняет авторизацию на сервере
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        Task Authenticate(string userName, CancellationToken ct = default);

        /// <summary>
        /// Запрашивает список всех файлов в облаке
        /// </summary>
        Task<ICollection<IEntry>> GetFiles(CancellationToken ct = default);

        /// <summary>
        /// Скачивает файл
        /// </summary>
        /// <param name="entry">Данные файла</param>
        Task<Stream> DownloadFile(IEntry entry, CancellationToken ct = default);
    }

    /// <summary>
    /// Данные файла
    /// </summary>
    public interface IEntry
    {
        /// <summary>
        /// Название файла
        /// </summary>
        string Name { get; set; }
    }
}