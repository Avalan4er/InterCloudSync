using Google.Apis.Drive.v3.Data;
using InterCloudSync.Cloud;
using InterCloudSync.GoogleDrive.Models;

namespace InterCloudSync.GoogleDrive
{
    internal static class Mapper
    {
        public static IEntry Map(this File file)
        {
            return new FileDto()
            {
                Id = file.Id,
                Name = file.Name,
            };
        }
    }
}