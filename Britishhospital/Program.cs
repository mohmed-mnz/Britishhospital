using APIs.Handlers;
using DataLayer;
using DbUp;
using Microsoft.OpenApi.Models;
using SharedConfig;
using BussinesLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Booking.MiddleWares;
using Booking.Middlewares;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        #region Configuration

        AppConfiguration appConfiguration;
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configBuilder = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{env}.json", true, reloadOnChange: true)
                             .Build();

        appConfiguration = configBuilder.Get<AppConfiguration>()!;
        builder.Services.AddSingleton(appConfiguration);


        #endregion       


        #region AddCors
        var cors = "Cors";
        builder.Services.AddCors(options =>
            options.AddPolicy(name: cors, p =>
           p.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

        #endregion


        #region TokenConfiguration
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              options.RequireHttpsMetadata = true;
              options.SaveToken = false;
              options.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidAudience = appConfiguration!.Jwt!.Audience,
                  ValidIssuer = appConfiguration.Jwt.Issuer,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfiguration.Jwt.key)),
                  ClockSkew = TimeSpan.Zero,
                  ValidateLifetime = true
              };
          });
        #endregion

        #region SignalR Configuration
        builder.Services.AddSignalR(options => options.EnableDetailedErrors = true)
         .AddJsonProtocol(options =>
         {
             options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
             options.PayloadSerializerOptions.MaxDepth = 64;
         });
        #endregion



        #region DbUpConfiguration
        DbUpRunner.Start(appConfiguration.DbConfig!.BritshHospitalConnctionString ?? throw new ArgumentException("GovlcConnctionString is not configured"));
        #endregion

        #region SwaggerConfiguration
        builder.Services.AddSwaggerGen(
        c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectStamp API Documentation ", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            c.OperationFilter<AuthOperationFilter>();
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                           {
                               new OpenApiSecurityScheme
                               {
                                   Reference= new OpenApiReference
                                   {
                                       Type=ReferenceType.SecurityScheme,
                                       Id="Bearer"
                                   }
                               },
                               new string[]{}
                           }
            });
        }
        );
        #endregion

    

        builder.Services.AddControllers();
        builder.Services.RegisterDataLayerDI(appConfiguration);
        builder.Services.RegisterBussinesLayerDi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHealthChecks();
        builder.Services.AddTransient<GlobalEaxceptionErrorHandlingMiddleware>();

        //builder.Services.AddSingleton<IDistributedLockProvider>(_ =>  new SqlDistributedLockProvider(appConfiguration.DbConfig.GovlcConnctionString));

        #region AddResponseCompression
        builder.Services.AddResponseCompression(builder =>
       {
           builder.EnableForHttps = true;
       });

        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        }); 
        #endregion

        builder.Services.AddSwaggerGen();

        builder.Services.AddMemoryCache();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpClient();






        var app = builder.Build();
        app.UseJwtTokenInterceptor();

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(cors);  
        
        app.UseHttpsRedirection();

        app.UseResponseCompression();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<GlobalEaxceptionErrorHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}