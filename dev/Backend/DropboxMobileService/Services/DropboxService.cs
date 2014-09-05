using System.Runtime.CompilerServices;

namespace DropboxMobileService.Services
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

    public class DropboxService
    {
        private const string BaseUri = "https://api.dropbox.com/1";
        private const string UserApi = "account/info";
        private const string FilesApi = "metadata/auto";
        private const string MediaApi = "media/auto";
        
        private string Token { get; set; }
        private WebClient WebClient { get; set; }

        public DropboxService(string userToken)
        {
            this.Token = userToken;
            this.Token = "WzudmobzDOkAAAAAAAAE0Goz7CGCF4uZ9j1p0xrNtM8bz8Fb5QyxoM-zdwAO8Sdu";
            this.WebClient = new WebClient();
        }

        public async Task<DropboxUserInfo> GetUserInformation()
        {
            var url = new Uri(string.Format("{0}/{1}?access_token={2}", BaseUri, UserApi, Token));
            var result = await WebClient.DownloadStringTaskAsync(url);
            return JsonConvert.DeserializeObject<DropboxUserInfo>(result);
        }

        public async Task<DropboxMetadata> GetFolderContent(string path)
        {
            var url = new Uri(string.Format("{0}/{1}/{2}?access_token={3}", BaseUri, FilesApi, path, Token));
            var result = await WebClient.DownloadStringTaskAsync(url);
            return JsonConvert.DeserializeObject<DropboxMetadata>(result);
        }

        public async Task<DropboxMediaFileMetadata> GetMediaFileMetadata(string path)
        {
            var url = new Uri(string.Format("{0}/{1}/{2}?access_token={3}", BaseUri, MediaApi, path, Token));
            var result = await WebClient.UploadStringTaskAsync(url, string.Empty);
            return JsonConvert.DeserializeObject<DropboxMediaFileMetadata>(result);
        }
    }
}