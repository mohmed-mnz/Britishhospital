using BussinesLayer.Interfaces.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SharedConfig;
using System.Text;
namespace Booking.Middlewares
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ITokenService _tokenService;

        private readonly IPresistanceService _presistanceService;
        public JwtTokenMiddleware(RequestDelegate next, ITokenService tokenService, IPresistanceService presistanceService)
        {
            _next = next;
            _tokenService = tokenService;
            _presistanceService = presistanceService;
        }
        public Task Invoke(HttpContext httpContext)
        {
            string? authorization = httpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorization))
            {
                string token = authorization.Split(' ')[1];

                var claims = _tokenService.Validate(token, out bool isExpired);
                if (claims != null && !isExpired)
                {

                    var tokenId = claims.FindFirst("TokenId")?.Value;
                    if (tokenId != null)
                    {
                        string backToken = _presistanceService.Get<string>($"Token_{tokenId}");

                        if (!string.IsNullOrEmpty(backToken))
                        {
                            var backClaimes = _tokenService.Validate(backToken, out bool isBackTokenExpired);

                            if (!isBackTokenExpired)
                            {
                                Thread.CurrentPrincipal = backClaimes;
                                httpContext.User = backClaimes;
                                _presistanceService.Set<string>($"Token_{tokenId}", backToken, TimeSpan.FromMinutes(30));
                            }
                        }
                    }
                }
            }
            return _next(httpContext);
        }

    }

    public static class JwtTokenInterceptorExtensions
    {
        public static IApplicationBuilder UseJwtTokenInterceptor(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtTokenMiddleware>();
        }


        public static void UserAuthenticationService(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfiguration.Jwt!.key)),
                            ValidIssuer = appConfiguration.Jwt.Issuer,
                            ValidAudience = appConfiguration.Jwt.Audience
                        };
                    });
        }
    }

}
