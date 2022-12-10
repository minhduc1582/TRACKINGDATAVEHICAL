using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop_api.Models.DTO.Images
{
    public class ImageDto
    {
        public string Name{get;set;}
        public IFormFile Image{get;set;}
        public int PositionID{get;set;}
    }
}