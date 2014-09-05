namespace DropboxMobileService.Middleware
{
    using Microsoft.WindowsAzure.Mobile.Service.Security;

    public class GithubCredentials : ProviderCredentials
    {
        public GithubCredentials()
            : base(GithubLoginProvider.ProviderName)
        {
        }

        public string AccessToken { get; set; }
    }
}