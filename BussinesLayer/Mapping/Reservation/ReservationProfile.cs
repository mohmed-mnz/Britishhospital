using ApiContracts.Reservation;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.Reservation;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Reservations, ReservationsDto>()
            .ReverseMap();
        CreateMap<Reservations, ReservationsAddDto>()
            .ReverseMap();
        CreateMap<Reservations, ReservationsUpdateStatusDto>()
            .ReverseMap();
        CreateMap<Reservations, CallNextInqueueReq>()
            .ReverseMap();
        CreateMap<Reservations, ReservationsDto>();

        CreateMap<Reservations, ReservationsCounterDetailsDto>()
            .ForMember(dest => dest.CounterName, opt => opt.MapFrom(src =>
                 src.Service != null && src.Service.CounterServices.Any()
                     ? src.Service.CounterServices.Select(cs => cs.Counter != null ? cs.Counter.CounterName : "No Counter").FirstOrDefault()
                     : "No Service"))
            .ForMember(dest => dest.ReservationData, opt => opt.MapFrom(src =>
                src.Service != null && src.Service.CounterServices.Any()
                    ? src.Service.CounterServices.Select(cs => new ReservationsDto
                    {
                        Id = src.Id,
                        Nid = src.Nid,
                        ReservationDate = src.ReservationDate,
                        Name = src.Citizen != null ? src.Citizen.Name : "Unknown Citizen",
                        QueueSerial = src.QueueSerial,
                        CitizenId = src.CitizenId.HasValue ? src.CitizenId.Value : 0,
                        CustomerId = src.CustomerId.HasValue ? src.CustomerId.Value : 0,
                        Status = src.Status,
                        CounterId = cs.CounterId,
                        MobileNumber = src.MobileNumber,
                        TicketNumber = src.TicketNumber,
                        ReqId = src.ServiceId,
                        CallAt = src.CallAt,
                        ReservationType = src.ReservationType,
                        Orgid = src.Orgid,
                        OrgName = src.Org != null ? src.Org.OrgName : "No Org"
                    }).ToList()
                : new List<ReservationsDto>()))
                .ReverseMap();




    }
}
