﻿using System;
using System.Threading.Tasks;

namespace Owin.Security.Providers.StackExchange
{
    /// <summary>
    /// Default <see cref="IStackExchangeAuthenticationProvider"/> implementation.
    /// </summary>
    public class StackExchangeAuthenticationProvider : IStackExchangeAuthenticationProvider
    {
        /// <summary>
        /// Initializes a <see cref="StackExchangeAuthenticationProvider"/>
        /// </summary>
        public StackExchangeAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
        }

        /// <summary>
        /// Gets or sets the function that is invoked when the Authenticated method is invoked.
        /// </summary>
        public Func<StackExchangeAuthenticatedContext, Task> OnAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets the function that is invoked when the ReturnEndpoint method is invoked.
        /// </summary>
        public Func<StackExchangeReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

        /// <summary>
        /// Invoked whenever StackExchange succesfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        public virtual Task Authenticated(StackExchangeAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        /// <summary>
        /// Invoked prior to the <see cref="System.Security.Claims.ClaimsIdentity"/> being saved in a local cookie and the browser being redirected to the originally requested URL.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        public virtual Task ReturnEndpoint(StackExchangeReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }
    }
}