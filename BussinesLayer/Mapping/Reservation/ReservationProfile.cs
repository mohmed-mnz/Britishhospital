using ApiContracts.Reservation;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.Reservation;

public class ReservationProfile: Profile
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
    }
}
