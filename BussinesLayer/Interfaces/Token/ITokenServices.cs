using System.Security.Claims;
namespace BussinesLayer.Interfaces.Token;

public interface ITokenService
{
    string Create(Dictionary<string, string> Claims, double ExpiryInMiuntes);
    ClaimsPrincipal Validate(string Token, out bool IsExpired);
}
