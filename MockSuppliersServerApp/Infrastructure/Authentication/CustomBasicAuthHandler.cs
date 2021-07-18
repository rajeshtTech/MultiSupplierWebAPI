using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MockSuppliersServerApp.Infrastructure.Authentication
{
    public class CustomBasicAuthOptions : AuthenticationSchemeOptions{
    }
    public class CustomBasicAuthHandler : AuthenticationHandler<CustomBasicAuthOptions>
    {
        private const string AuthorizationHeaderName = "Authorization";
        private string clientId = string.Empty;
        private string clientCredentials = string.Empty;
             
        public CustomBasicAuthHandler(IOptionsMonitor<CustomBasicAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock) {            
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthorizationHeaderName))
                return Task.FromResult(AuthenticateResult.NoResult());

            if (!AuthenticationHeaderValue.TryParse(Request.Headers[AuthorizationHeaderName], out AuthenticationHeaderValue headerValue))
                return Task.FromResult(AuthenticateResult.NoResult());

            SetClientIdAndCredentials(headerValue.Parameter);

            if (!IsClientValid())
                return Task.FromResult(AuthenticateResult.Fail("Invalid Client Id and Credentials"));

            return Task.FromResult(AuthenticateResult.Success(GenerateAuthTicket()));            
        }

        private void SetClientIdAndCredentials(string authHeaderValParameter)
        {
            byte[] headerValueBytes = Convert.FromBase64String(authHeaderValParameter);
            string userAndPassword = Encoding.UTF8.GetString(headerValueBytes);
            string[] parts = userAndPassword.Split(':');

            clientId = parts[0];
            clientCredentials = parts[1];
        }

        private bool IsClientValid()
        {
            return ((Request.Headers["Supplier"] == "SupplierX" && clientId == "ClientX" && clientCredentials == "PasswordX") ||
                    (Request.Headers["Supplier"] == "SupplierY" && clientId == "ClientY" && clientCredentials == "PasswordY") ||
                    (Request.Headers["Supplier"] == "SupplierZ" && clientId == "ClientZ" && clientCredentials == "PasswordZ"));
        }   

        private AuthenticationTicket GenerateAuthTicket()
        {
            var claims = new[] { new Claim(ClaimTypes.Name, clientId) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return ticket;  
        }
    }
}
