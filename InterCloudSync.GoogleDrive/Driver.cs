using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using InterCloudSync.Cloud;
using InterCloudSync.GoogleDrive.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InterCloudSync.GoogleDrive
{
    public class Driver : ICloudDriver
    {
        private readonly ILogger _logger = new LogFactory().GetCurrentClassLogger();
        private UserCredential _userCredential;
        private DriveService _driveService;

        #region ICloudDriver implementation

        public bool IsAuthenticated { get; private set; }

        public async Task Authenticate(string userName, CancellationToken ct = default)
        {
            if (IsAuthenticated)
            {
                return;
            }

            var secrets = GetClientSecrets();
            _userCredential = await AuthorizeAsync(secrets, userName, ct).ConfigureAwait(false);
            _driveService = CreateDriveService(_userCredential);

            IsAuthenticated = true;
        }

        public async Task<Stream> DownloadFile(IEntry entry, CancellationToken ct = default)
        {
            if (!(entry is FileDto fileDto))
            {
                throw new ArgumentException($"{nameof(entry)} не задано, либо не может быть приведено к {nameof(FileDto)}");
            }

            var request = _driveService.Files.Get(fileDto.Id);
            var ms = new MemoryStream();

            try
            {
                await request.DownloadAsync(ms, ct).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка при загрузке файла {fileDto.Name}");
                throw;
            }

            return ms;
        }

        public async Task<ICollection<IEntry>> GetFiles(CancellationToken ct = default)
        {
            var files = new List<Google.Apis.Drive.v3.Data.File>();
            string pageToken = null;
            FileList response = null;

            try
            {
                do
                {
                    var request = CreateGetPageRequest(pageToken);
                    response = await request.ExecuteAsync(ct).ConfigureAwait(false);
                    files.AddRange(response.Files.Where(x => x.Trashed != true));
                    pageToken = response.NextPageToken;
                } while (!string.IsNullOrEmpty(pageToken));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при запросе списка файлов");
                throw;
            }

            return files.Select(x => x.Map()).ToList();
        }

        #endregion ICloudDriver implementation

        #region Utils

        private FilesResource.ListRequest CreateGetPageRequest(string pageToken)
        {
            var request = _driveService.Files.List();
            request.PageSize = 1000;
            request.Fields = Configuration.Fields;
            request.Corpora = "user";
            request.OrderBy = "folder,name";
            request.PageToken = pageToken;

#if DEBUG
            request.PrettyPrint = true;
#endif

            return request;
        }

        /// <summary>
        /// Создает сервис Drive API
        /// </summary>
        /// <param name="userCredential"></param>
        private DriveService CreateDriveService(UserCredential userCredential)
        {
            return new DriveService(new BaseClientService.Initializer()
            {
                ApplicationName = Configuration.ApplicationName,
                HttpClientInitializer = userCredential,
            });
        }

        /// <summary>
        /// Производит авторизацию пользователя в Google Disk Api
        /// </summary>
        /// <param name="clientSecrets">Секрет клиента</param>
        /// <param name="userName">Имя пользователя</param>
        private async Task<UserCredential> AuthorizeAsync(ClientSecrets clientSecrets, string userName, CancellationToken ct = default)
        {
            if (clientSecrets == null)
            {
                throw new ArgumentNullException(nameof(clientSecrets));
            }

            try
            {
                return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                       clientSecrets,
                       Configuration.Scopes,
                       userName,
                       ct,
                       new FileDataStore(Configuration.FileDataStorePath, true)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при получении токена авторизации");
                throw;
            }
        }

        /// <summary>
        /// Получает секрет клиента из файла
        /// </summary>
        private ClientSecrets GetClientSecrets()
        {
            Stream credentialsStream;
            try
            {
                credentialsStream = new FileStream(Configuration.CredentialsPath, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Ошибка при открытии файла {Configuration.CredentialsPath}");
                throw;
            }

            try
            {
                var credentials = GoogleClientSecrets.Load(credentialsStream);
                return credentials.Secrets;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при чтении секрета из файла");
                throw;
            }
        }

        #endregion Utils
    }
}