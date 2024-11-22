using ApiContracts;
using ApiContracts.Advertisment;
using AutoMapper;
using BussinesLayer.Interface;
using DataLayer.Interfaces;
using Models.Models;

namespace BussinesLayer.Services;

public class AdvertismentServices(IAdvertismentRepository _advertismentRepository, IMapper _mapper ,  IAttachmetsService _attachmentServices) : IAdvertismentServices
{
    public async Task<GResponse<AdvertismentDto>> AddAdvertisment(AdvertismentAddDto addDto)
    {
        var advertisment = _mapper.Map<Advertisment>(addDto);
        var date = DateTime.Now.ToString("yyyy/MM/ddd").Replace('/', '-');
       
        if (advertisment.Mediatype == "1")
        {
            var extension = addDto.MediaFile!.Split(';')[0].Split('/')[1];
            var response = _attachmentServices.UploadFileAsBase64(new SharedConfig.AssetInfo
            {
                Name = $"{addDto.AdvertName}".Trim(),
                FeatureDirectory = $"Advertisment/Images/{date}/",
                type = extension,
                buffer = addDto.MediaFile
            });
            if (response != null && response.success)
            {
                advertisment.MediaFile = $"Advertisment/Images/{date}/{response.dbPath ?? ""}";
            }
            else
            {
                advertisment.MediaFile = null;
            }
        }
        else
        {
            var extension = addDto.MediaFile!.Split(';')[0].Split('/')[1];
            var response = _attachmentServices.UploadFileAsBase64(new SharedConfig.AssetInfo
            {
                Name = $"{advertisment.AdvertName}".Trim(),
                FeatureDirectory = $"Advertisment/Videos/{date}/",
                type = extension,
                buffer = addDto.MediaFile
            });
            if (response != null && response.success)
            {
                advertisment.MediaFile = $"Advertisment/Videos/{date}/{response.dbPath ?? ""}";
            }
            else
            {
                advertisment.MediaFile = null;
            }
        }

        await _advertismentRepository.InsertAsync(advertisment);
        await _advertismentRepository.Commit();
        var result = _mapper.Map<AdvertismentDto>(advertisment);
        return GResponse<AdvertismentDto>.CreateSuccess(result);
    }

    public async Task<GResponse<bool>> DeleteAdvertisment(int id)
    {
        var advert = await _advertismentRepository.FindAsync(id)!;
        if (advert == null)
        {
          throw new ApplicationException("Advertisment not found");
        }
        _attachmentServices.DeleteFile(advert.MediaFile!, "");

        _advertismentRepository.Delete(advert);
        await _advertismentRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);

    }

    public async Task<GResponse<AdvertismentDto>> GetAdvertismentById(int id)
    {
        var advert = await _advertismentRepository.FindAsync(id)!;
        if (advert == null)
        {
            throw new ApplicationException("Advertisment not found");
        }

        var result = _mapper.Map<AdvertismentDto>(advert);
        result.Mediatype = advert.Mediatype == "1" ? "image" : "Vedio";
        result.MediaFile = await _attachmentServices.RetrieveFileAsBase64("", result.MediaFile!);
        return GResponse<AdvertismentDto>.CreateSuccess(result);
    }

    public async Task<GResponse<List<AdvertismentDto>>> GetAllAdvertisments()
    {
        var adverts =await _advertismentRepository.GetAllAsync();
        var result = _mapper.Map<List<AdvertismentDto>>(adverts);
        foreach (var item in result)
        {
            item.Mediatype = item.Mediatype == "1" ? "image" : "Vedio";
            item.MediaFile = await _attachmentServices.RetrieveFileAsBase64("", item.MediaFile!);
        }
        return GResponse<List<AdvertismentDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<AdvertismentDto>> UpdateAdvertisment(AdvertismentUpdateDto model)
    {
        var advert = await _advertismentRepository.FindAsync(model.Id)!;
        if (advert == null)
        {
            throw new ApplicationException("Advertisment not found");
        }
        advert= _mapper.Map(model, advert);
        if (model.MediaFile != null)
        {
            var date = DateTime.Now.ToString("yyyy/MM/ddd").Replace('/', '-');
            if (advert.Mediatype == "1")
            {
                var extension = model.MediaFile!.Split(';')[0].Split('/')[1];
                var response = _attachmentServices.UploadFileAsBase64(new SharedConfig.AssetInfo
                {
                    Name = $"{model.AdvertName}".Trim(),
                    FeatureDirectory = $"Advertisment/Images/{date}/",
                    type = extension,
                    buffer = model.MediaFile
                });
                if (response != null && response.success)
                {
                    advert.MediaFile = $"Advertisment/Images/{date}/{response.dbPath ?? ""}";
                }
                else
                {
                    advert.MediaFile = null;
                }
            }
            else
            {
                var extension = model.MediaFile!.Split(';')[0].Split('/')[1];
                var response = _attachmentServices.UploadFileAsBase64(new SharedConfig.AssetInfo
                {
                    Name = $"{model.AdvertName}".Trim(),
                    FeatureDirectory = $"Advertisment/Videos/{date}/",
                    type = extension,
                    buffer = model.MediaFile
                });
                if (response != null && response.success)
                {
                    advert.MediaFile = $"Advertisment/Videos/{date}/{response.dbPath ?? ""}";
                }
                else
                {
                    advert.MediaFile = null;
                }
            }
        }
        else
        {
            advert.MediaFile = model.MediaFile;
        }
        await _advertismentRepository.Commit();
        var result = _mapper.Map<AdvertismentDto>(advert);
        return GResponse<AdvertismentDto>.CreateSuccess(result);
    }
}
