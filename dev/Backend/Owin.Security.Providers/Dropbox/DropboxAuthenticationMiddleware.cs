namespace Owin.Security.Providers.Dropbox
{
    using Owin.Security.Providers.Dropbox.Provider;
    using System;
    using System.Net.Http;    
    using Microsoft.Owin;
    using Microsoft.Owin.Logging;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.Owin.Security.Infrastructure;
    using Microsoft.Owin.Security.Jwt;
    using Microsoft.Owin.Security;
    using Owin;

    internal class DropboxAuthenticationMiddleware : AuthenticationMiddleware<DropboxAuthenticationOptions>
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public DropboxAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, DropboxAuthenticationOptions options)
            : base(next, options)
        {
            _logger = app.CreateLogger<DropboxAuthenticationMiddleware>();

            if (Options.Provider == null)
            {
                Options.Provider = new DropboxAuthenticationProvider();
            }

            if (Options.StateDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(DropboxAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }

            
            if (String.IsNullOrEmpty(Options.SignInAsAuthenticationType))
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();

            _httpClient = new HttpClient(ResolveHttpMessageHandler(Options))
            {
                Timeout = Options.BackchannelTimeout,
                MaxResponseContentBufferSize = 1024 * 1024 * 10
            };
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft Owin GitHub middleware");
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
        }

        protected override AuthenticationHandler<DropboxAuthenticationOptions> CreateHandler()
        {
            return new DropboxAuthenticationHandler(_httpClient, _logger);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(DropboxAuthenticationOptions options)
        {
            var handler = options.BackchannelHttpHandler ?? new WebRequestHandler();

            if (options.BackchannelCertificateValidator == null) return handler;

            var webRequestHandler = handler as WebRequestHandler;
            if (webRequestHandler == null)
            {
                throw new InvalidOperationException("Validator Handler Mismatch");
            }
            webRequestHandler.ServerCertificateValidationCallback = options.BackchannelCertificateValidator.Validate;

            return handler;
        }
    }
}