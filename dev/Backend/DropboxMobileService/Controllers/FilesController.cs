namespace DropboxMobileService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using DropboxMobileService.Models;
    using DropboxMobileService.Services;
    using Microsoft.WindowsAzure.Mobile.Service;

    public class FilesController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/Files/{path}

        [HttpGet]
        public async Task<DropboxMetadata> Get(string path)
        {
            Services.Log.Info("Hello from custom controller!");

            return await new DropboxService(string.Empty).GetFolderContent(path);
        }
    }
}
