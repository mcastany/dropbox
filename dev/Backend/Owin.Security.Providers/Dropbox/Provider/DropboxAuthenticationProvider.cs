namespace Owin.Security.Providers.Dropbox.Provider
{
    using System;
    using System.Threading.Tasks;

    public class DropboxAuthenticationProvider : IDropboxAuthenticationProvider
    {
        public DropboxAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
        }

        public Func<DropboxAuthenticatedContext, Task> OnAuthenticated { get; set; }

        public Func<DropboxReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

        public virtual Task Authenticated(DropboxAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        public virtual Task ReturnEndpoint(DropboxReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }
    }
}