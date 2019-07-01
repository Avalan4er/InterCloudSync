using InterCloudSync.Cloud;

namespace InterCloudSync.GoogleDrive.Models
{
    /// <summary>
    /// Файл
    /// </summary>
    internal class FileDto : IEntry
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
    }
}