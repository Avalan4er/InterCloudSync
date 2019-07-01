using InterCloudSync.Cloud;

namespace InterCloudSync.Business
{
    public class DriverManager
    {
        public ICloudDriver GoogleDrive { get; } = new GoogleDrive.Driver();
    }
}