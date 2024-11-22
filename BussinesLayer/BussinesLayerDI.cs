using BussinesLayer.Interface;
using BussinesLayer.Interfaces;
using BussinesLayer.Interfaces.Token;
using BussinesLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;

namespace BussinesLayer;

public  static  class BussinesLayerDI
{
    public static void RegisterBussinesLayerDi(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AdvertismentServices));
        services.AddScoped<IAdvertismentServices, AdvertismentServices>();
        services.AddScoped<IServicesServices, ServicesServices>();
        services.AddScoped<IArchiveServices, ArchiveService>();
        services.AddScoped<IAttachmetsService, AttachmetsService>();
        services.AddScoped<IPresistanceService, InMemoryPresistanceService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICountersServices, CountersServices>();
        services.AddScoped<IDisplayServices, DisplayServices>();
    }
}
