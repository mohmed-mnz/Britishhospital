using ApiContracts;
using ApiContracts.OrgSetteing;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Globalization;

namespace BussinesLayer.Services;

public class BookingSettingOrgServices(IBookingSettingOrgRepository bookingSettingOrgRepository, IMapper mapper) : IBookingSettingOrgServices
{
    public async Task<GResponse<BookingSettingOrgDto>> AddBookingSettingOrgAsync(BookingSettingOrgAddDto bookingSettingOrgAddDto)
    {
        var orgSetting = await bookingSettingOrgRepository
            .Where(x => x.OrgId == bookingSettingOrgAddDto.OrgId)!
            .FirstOrDefaultAsync();

        if (orgSetting != null)
        {
            orgSetting = mapper.Map(bookingSettingOrgAddDto, orgSetting);
            await bookingSettingOrgRepository.Commit();
            orgSetting = await bookingSettingOrgRepository
                .Where(x => x.Id == orgSetting.Id)!
                .Include(o => o.Org)
                .FirstOrDefaultAsync();
            var bookingSettingOrgDto = mapper.Map<BookingSettingOrgDto>(orgSetting);
            return GResponse<BookingSettingOrgDto>.CreateSuccess(bookingSettingOrgDto);
        }
        else
        {
            var bookingSettingOrg = new BookingSettingOrg
            {
                OrgId = bookingSettingOrgAddDto.OrgId ?? 0,
                UserlimitReservation = bookingSettingOrgAddDto.UserlimitReservation ?? 0,
                StartWorkingHour = !string.IsNullOrWhiteSpace(bookingSettingOrgAddDto.StartWorkingHour)
                    ? DateTime.ParseExact(bookingSettingOrgAddDto.StartWorkingHour, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : null,
                EndWorkingHour = !string.IsNullOrWhiteSpace(bookingSettingOrgAddDto.EndWorkingHour)
                    ? DateTime.ParseExact(bookingSettingOrgAddDto.EndWorkingHour, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : null,
                KioskClosingTime = !string.IsNullOrWhiteSpace(bookingSettingOrgAddDto.KioskClosingTime)
                    ? DateTime.ParseExact(bookingSettingOrgAddDto.KioskClosingTime, "hh:mmtt", CultureInfo.InvariantCulture).TimeOfDay
                    : null
            };

            await bookingSettingOrgRepository.InsertAsync(bookingSettingOrg);
            await bookingSettingOrgRepository.Commit();

            bookingSettingOrg = await bookingSettingOrgRepository
                .Where(x => x.Id == bookingSettingOrg.Id)!
                .Include(o => o.Org)
                .FirstOrDefaultAsync();

            var bookingSettingOrgDto = mapper.Map<BookingSettingOrgDto>(bookingSettingOrg);
            return GResponse<BookingSettingOrgDto>.CreateSuccess(bookingSettingOrgDto);
        }
    }



    public async Task<GResponse<bool>> DeleteBookingSettingOrgAsync(int id)
    {
        var bookingSettingOrg = await bookingSettingOrgRepository.Where(x => x.Id == id)!.FirstOrDefaultAsync();
        if (bookingSettingOrg == null)
        {
            throw new ApplicationException("BookingSettingOrg not found");
        }
        bookingSettingOrgRepository.Delete(bookingSettingOrg);
        await bookingSettingOrgRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);

    }

    public async Task<GResponse<BookingSettingOrgDto>> GetBookingSettingOrgAsync(int id)
    {
        var bookingSettingOrg = await bookingSettingOrgRepository.Where(x => x.Id == id)!.Include(o => o.Org).FirstOrDefaultAsync();
        if (bookingSettingOrg == null)
        {
            throw new ApplicationException("BookingSettingOrg not found");
        }
        var bookingSettingOrgDto = mapper.Map<BookingSettingOrgDto>(bookingSettingOrg);
        return GResponse<BookingSettingOrgDto>.CreateSuccess(bookingSettingOrgDto);
    }

    public async Task<GResponse<IEnumerable<BookingSettingOrgDto>>> GetBookingSettingOrgsAsync(int orgid)
    {
        var bookingSettingOrgs = await bookingSettingOrgRepository.Where(x => x.OrgId == orgid)!.Include(o => o.Org).ToListAsync();
        var bookingSettingOrgDtos = mapper.Map<IEnumerable<BookingSettingOrgDto>>(bookingSettingOrgs);
        return GResponse<IEnumerable<BookingSettingOrgDto>>.CreateSuccess(bookingSettingOrgDtos);
    }

    public async Task<GResponse<BookingSettingOrgDto>> UpdateBookingSettingOrgAsync(BookingSettingOrgUpdateDto bookingSettingOrgUpdateDto)
    {
        var bookingSettingOrg = await bookingSettingOrgRepository.Where(x => x.Id == bookingSettingOrgUpdateDto.Id)!.Include(o => o.Org).FirstOrDefaultAsync();
        if (bookingSettingOrg == null)
        {
            throw new ApplicationException("BookingSettingOrg not found");
        }
        bookingSettingOrg = mapper.Map(bookingSettingOrgUpdateDto, bookingSettingOrg);
        await bookingSettingOrgRepository.Commit();
        var bookingSettingOrgDto = mapper.Map<BookingSettingOrgDto>(bookingSettingOrg);
        return GResponse<BookingSettingOrgDto>.CreateSuccess(bookingSettingOrgDto);

    }
}
