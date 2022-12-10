using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop_api.Entities;
using eshop_api.Models.DTO.Images;
using eshop_pbl6.Entities;

namespace eshop_api.Services.Images
{
    public interface IImageService
    {
        Task<List<Image>> GetImagesByIdPosition(int code);
        Task<Image> AddImage(CreateUpdateImageDto image);
        Task<bool> DeleteImageById(int Id);
        Task<Image> UpdateImage(CreateUpdateImageDto image);
        Task<Image> AddImage(IFormFile image,int IdPosition);
    }
}