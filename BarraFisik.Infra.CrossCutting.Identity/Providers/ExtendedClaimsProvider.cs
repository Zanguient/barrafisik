using System.Security.Claims;

namespace BarraFisik.Infra.CrossCutting.Identity.Providers
{
    public class ExtendedClaimsProvider
    {
        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }
    }
}