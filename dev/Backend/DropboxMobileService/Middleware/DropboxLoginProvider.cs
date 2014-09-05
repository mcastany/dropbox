using System;

namespace DropboxMobileService.Middleware
{
    using Microsoft.WindowsAzure.Mobile.Service;
    using Microsoft.WindowsAzure.Mobile.Service.Security;
    using Owin.Security.Providers.Dropbox;
    using Owin.Security.Providers.Dropbox.Provider;
    using Newtonsoft.Json.Linq;
    using Owin;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class DropboxLoginProvider : LoginProvider
    {
        internal const string ProviderName = "Dropbox";

        public DropboxLoginProvider(IServiceTokenHandler tokenHandler)
            : base(tokenHandler)
        {
            this.TokenLifetime = new TimeSpan(30, 0, 0, 0);
        }

        public override string Name
        {
            get { return ProviderName; }
        }

        public override void ConfigureMiddleware(IAppBuilder appBuilder, ServiceSettingsDictionary settings)
        {
            var options = new DropboxAuthenticationOptions()
            {
                ClientId = settings["DropboxClientId"],
                ClientSecret = settings["DropboxClientPassword"],
                AuthenticationType = this.Name,
                Provider = new DropboxLoginAuthenticationProvider()
            };

            appBuilder.UseDropboxAuthentication(options);
        }

        public override ProviderCredentials CreateCredentials(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException("claimsIdentity");
            }

            var name = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var accessToken = claimsIdentity.FindFirst(ServiceClaimTypes.ProviderAccessToken);

            return new DropboxCredentials
            {
                UserId = this.TokenHandler.CreateUserId(this.Name, name != null ? name.Value : null),
                AccessToken = accessToken != null ? accessToken.Value : null
            };
        }

        public override ProviderCredentials ParseCredentials(JObject serialized)
        {
            return serialized.ToObject<DropboxCredentials>();
        }
    }

    public class DropboxLoginAuthenticationProvider : DropboxAuthenticationProvider
    {
        public override Task Authenticated(DropboxAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(ServiceClaimTypes.ProviderAccessToken, context.AccessToken));
            return base.Authenticated(context);
        }
    }
}