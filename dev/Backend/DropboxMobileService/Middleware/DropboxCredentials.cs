namespace DropboxMobileService.Middleware
{
    using Microsoft.WindowsAzure.Mobile.Service.Security;

    public class DropboxCredentials : ProviderCredentials
    {
        public DropboxCredentials()
            : base(DropboxLoginProvider.ProviderName)
        {
        }

        public string AccessToken { get; set; }
    }
}