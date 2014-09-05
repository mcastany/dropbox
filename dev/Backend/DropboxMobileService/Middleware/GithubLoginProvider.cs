namespace DropboxMobileService.Middleware
{
    using Microsoft.WindowsAzure.Mobile.Service;
    using Microsoft.WindowsAzure.Mobile.Service.Security;
    using Owin.Security.Providers.GitHub;
    using Newtonsoft.Json.Linq;
    using Owin;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class GithubLoginProvider : LoginProvider
    {
        internal const string ProviderName = "Github";

        public GithubLoginProvider(IServiceTokenHandler tokenHandler)
            : base(tokenHandler)
        {
        }

        public override string Name
        {
            get { return ProviderName; }
        }

        public override void ConfigureMiddleware(IAppBuilder appBuilder, ServiceSettingsDictionary settings)
        {
            var options = new GitHubAuthenticationOptions
            {
                ClientId = settings["GithubClientId"],
                ClientSecret = settings["GithubClientPassword"],
                AuthenticationType = this.Name,
                Provider = new GitHubAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new Claim(ServiceClaimTypes.ProviderAccessToken, context.AccessToken));
                        return Task.FromResult(0);
                    }
                },
            };

            appBuilder.UseGitHubAuthentication(options);
        }

        public override ProviderCredentials CreateCredentials(ClaimsIdentity claimsIdentity)
        {
            var name = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var accessToken = claimsIdentity.FindFirst(ServiceClaimTypes.ProviderAccessToken);
            
            return new GithubCredentials
            {
                UserId = this.TokenHandler.CreateUserId(this.Name, name != null ? name.Value : null),
                AccessToken = accessToken != null ? accessToken.Value : null
            };
        }

        public override ProviderCredentials ParseCredentials(JObject serialized)
        {
            return serialized.ToObject<GithubCredentials>();
        }
    }
}