using BussinesLayer.Interfaces.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedConfig;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceLayer
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private byte[] SignningKey(string SecretKey)
        {

            return Encoding.UTF8.GetBytes(SecretKey);
        }

        private SecurityKey SecurityKey(string SecretKey)
        {
            return new SymmetricSecurityKey(SignningKey(SecretKey));
        }
        private SigningCredentials SigningCredentials(string SecretKey)
        {
            return new SigningCredentials(SecurityKey(SecretKey), SecurityAlgorithms.HmacSha256);
        }
        public string Create(Dictionary<string, string> Claims, double ExpiryInMiuntes)
        {
            AppConfiguration jwtConfig = _configuration.Get<AppConfiguration>()!;

            var TokenDescription = new SecurityTokenDescriptor()
            {
                Issuer = jwtConfig!.Jwt!.Issuer,
                Audience = jwtConfig.Jwt.Audience,
                SigningCredentials = SigningCredentials(jwtConfig.Jwt.key ?? throw new ArgumentException("jwt:audience is not configured")),
                Expires = DateTime.UtcNow.AddMinutes(jwtConfig.Jwt.ExpirytimeinMinutes),
            };
            TokenDescription.Subject = new ClaimsIdentity();
            foreach (var Claim in Claims)
            {
                if (Claim.Key == ClaimTypes.Role)
                {
                    foreach (var Role in Claim.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        TokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, Role));
                    }
                }
                else
                {
                    TokenDescription.Subject.AddClaim(new Claim(Claim.Key, Claim.Value));
                }
            }



            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var token = tokenHandler.CreateToken(TokenDescription);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception )
            {
                throw;
            }

        }


        public ClaimsPrincipal Validate(string Token, out bool IsExpired)
        {
            AppConfiguration jwtConfig = _configuration.Get<AppConfiguration>()!;
            IsExpired = false;
            ClaimsPrincipal? ClaimsPrincipal = null;
            if (Token != string.Empty)
            {
                var now = DateTime.UtcNow;
                var valid = new TokenValidationParameters()
                {
                    ValidIssuer = jwtConfig!.Jwt!.Issuer,
                    ValidAudience = jwtConfig!.Jwt.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig!.Jwt.key ?? throw new ArgumentException("jwt:audience is not configured"))),
                    ValidateIssuerSigningKey = true,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal = tokenHandler.ValidateToken(Token, valid, out SecurityToken SecurityToken);
                IsExpired = now.CompareTo(SecurityToken.ValidTo) > 0;
                return ClaimsPrincipal;
            }
            return ClaimsPrincipal!;
        }
    }
}
