using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace e_BApi.Validating
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (ExporterRepository _repo = new ExporterRepository())
            {
                var exp = _repo.ValidateExp(context.UserName, context.Password);
                if (exp == null)
                {
                    context.SetError("invalid_grant", "Korisnicko ime i šifra su pogrešni.");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, exp.Authorization.Name));
                identity.AddClaim(new Claim(ClaimTypes.Name, exp.Name));
                identity.AddClaim(new Claim(ClaimTypes.Surname, exp.Surname));
                identity.AddClaim(new Claim("ExpID", exp.ExpID.ToString()));
                
                context.Validated(identity);
            }
        }


    }
}