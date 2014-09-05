namespace DropboxMobileService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using DropboxMobileService.Models;
    using DropboxMobileService.Services;
    using Microsoft.WindowsAzure.Mobile.Service;

    public class MediaController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/Media

        [HttpGet]
        public async Task<DropboxMediaFileMetadata> Get(string path)
        {
            return await new DropboxService(string.Empty).GetMediaFileMetadata(path);
        }

    }
}
