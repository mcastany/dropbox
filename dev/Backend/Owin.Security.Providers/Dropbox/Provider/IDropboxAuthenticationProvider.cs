namespace Owin.Security.Providers.Dropbox.Provider
{
    using System.Threading.Tasks;

    public interface IDropboxAuthenticationProvider
    {
        Task Authenticated(DropboxAuthenticatedContext context);

        Task ReturnEndpoint(DropboxReturnEndpointContext context);
    }
}