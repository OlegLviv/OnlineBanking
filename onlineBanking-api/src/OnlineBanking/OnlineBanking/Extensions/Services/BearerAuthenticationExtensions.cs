using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace OnlineBanking.Extensions.Services
{
    public static class BearerAuthenticationExtensions
    {
        public static void AddBearerAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.RequireHttpsMetadata = bool.Parse(configuration["RequireHttpsMetadata"]);
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = bool.Parse(configuration["ValidateActor"]),
                        ValidateAudience = bool.Parse(configuration["ValidateAudience"]),
                        ValidateLifetime = bool.Parse(configuration["ValidateLifetime"]),
                        ValidateIssuerSigningKey = bool.Parse(configuration["ValidateIssuerSigningKey"]),
                        ValidIssuer = configuration["Issuer"],
                        ValidAudience = configuration["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                            (configuration["Key"]))
                    };
                });
        }
    }
}
