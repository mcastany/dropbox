namespace Owin.Security.Providers.Dropbox
{
    using System;
    using Microsoft.Owin.Security;

    public static class DropboxAuthenticationExtensions
    {
        public static IAppBuilder UseDropboxAuthentication(this IAppBuilder app, DropboxAuthenticationOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            app.Use(typeof (DropboxAuthenticationMiddleware), app, options);
            return app;
        }

        public static IAppBuilder UseDropboxAuthentication(this IAppBuilder app, string clientId, string clientSecret)
        {
            return UseDropboxAuthentication(app, new DropboxAuthenticationOptions
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType(),
                });
        }
    }
}