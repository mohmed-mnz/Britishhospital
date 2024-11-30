using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using SharedConfig;

namespace DataLayer;

public static class DataLayerDi
{
    public static void RegisterDataLayerDI(this IServiceCollection service, AppConfiguration appConfiguration)
    {
        service.AddDbContext<BritshHosbitalContext>(options => options.UseSqlServer(appConfiguration.DbConfig!.BritshHospitalConnctionString));
        service.AddScoped<IAdvertismentRepository, AdvertismentRepository>();
        service.AddScoped<IBookingSettingOrgRepository, BookingSettingOrgRepository>();
        service.AddScoped<ICitizenRepository, CitizenRepository>();
        service.AddScoped<ICounterRepository, CounterRepository>();
        service.AddScoped<ICounterServicesRepository, CounterServicesRepository>();
        service.AddScoped<ICustomerRepository, CustomerRepository>();
        service.AddScoped<IDisplayRepository, DisplayRepository>();
        service.AddScoped<IDisplayAdvertsRepository, DisplayAdvertsRepository>();
        service.AddScoped<IDisplayCountersRepository, DisplayCountersRepository>();
        service.AddScoped<IEmployeeRepository, EmployeeRepository>();
        service.AddScoped<IGroupRepository, GroupRepository>();
        service.AddScoped<IGroupUserRepository, GroupUserRepository>();
        service.AddScoped<IOrganizationRepository, OrganizationRepository>();
        service.AddScoped<IReservationsRepository, ReservationsRepository>();
        service.AddScoped<IServiceRepository, ServiceRepository>();

    }
}
