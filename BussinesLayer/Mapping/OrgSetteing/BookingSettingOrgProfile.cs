using AutoMapper;
using System.Globalization;

namespace BussinesLayer.Mapping.OrgSetteing;

public class BookingSettingOrgProfile : Profile
{
    public BookingSettingOrgProfile()
    {
        CreateMap<Models.Models.BookingSettingOrg, ApiContracts.OrgSetteing.BookingSettingOrgDto>()
            .ForMember(dest => dest.OrgName, opt => opt.MapFrom(src => src.Org!.OrgName))
            .ReverseMap();

        CreateMap<ApiContracts.OrgSetteing.BookingSettingOrgAddDto, Models.Models.BookingSettingOrg>()
            .ForMember(dest => dest.StartWorkingHour, opt => opt.MapFrom(src =>
                !string.IsNullOrWhiteSpace(src.StartWorkingHour)
                    ? DateTime.ParseExact(src.StartWorkingHour, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : (TimeSpan?)null))
            .ForMember(dest => dest.EndWorkingHour, opt => opt.MapFrom(src =>
                !string.IsNullOrWhiteSpace(src.EndWorkingHour)
                    ? DateTime.ParseExact(src.EndWorkingHour, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : (TimeSpan?)null))
            .ForMember(dest => dest.KioskClosingTime, opt => opt.MapFrom(src =>
                !string.IsNullOrWhiteSpace(src.KioskClosingTime)
                    ? DateTime.ParseExact(src.KioskClosingTime, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : (TimeSpan?)null))
            .ReverseMap();

        CreateMap<ApiContracts.OrgSetteing.BookingSettingOrgUpdateDto, Models.Models.BookingSettingOrg>()
            .ForMember(dest => dest.StartWorkingHour, opt => opt.MapFrom(src =>
                !string.IsNullOrWhiteSpace(src.StartWorkingHour)
                    ? DateTime.ParseExact(src.StartWorkingHour, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : (TimeSpan?)null))
            .ForMember(dest => dest.EndWorkingHour, opt => opt.MapFrom(src =>
                !string.IsNullOrWhiteSpace(src.EndWorkingHour)
                    ? DateTime.ParseExact(src.EndWorkingHour, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : (TimeSpan?)null))
            .ForMember(dest => dest.KioskClosingTime, opt => opt.MapFrom(src =>
                !string.IsNullOrWhiteSpace(src.KioskClosingTime)
                    ? DateTime.ParseExact(src.KioskClosingTime, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : (TimeSpan?)null))
            .ReverseMap();
    }
}
