namespace DropboxMobileService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using DropboxMobileService.Models;
    using DropboxMobileService.Services;
    using Microsoft.WindowsAzure.Mobile.Service;

    public class UserInfoController : ApiController
    {
        public ApiServices Services { get; set; }

        // GET api/UserInfo
        [HttpGet]
        public async Task<DropboxUserInfo> Get()
        {
            Services.Log.Info("Hello from custom controller!");

            
            return await new DropboxService(string.Empty).GetUserInformation();
            //var serviceUser = User as ServiceUser;
            //if (serviceUser != null)
            //{
            //    return await serviceUser.GetIdentitiesAsync().ContinueWith(async t =>
            //    {
            //        return new DropboxService(string.Empty).GetUserInformation();
            //    });
            //}
            //else
            //{
            //}
        }
    }
}
