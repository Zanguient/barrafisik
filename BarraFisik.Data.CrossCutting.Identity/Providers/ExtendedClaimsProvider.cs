using System.Security.Claims;

namespace BarraFisik.Data.CrossCutting.Identity.Providers
{
    public class ExtendedClaimsProvider
    {
        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }
    }
}