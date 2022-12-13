using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingDataVehical.Models.DTO.Positions
{
    public class CreateUpdatePositionByFileDto
    {
        public double longitude {get; set;}
        public double latitude {get;set;}
        public double speed {get; set;}
        public DateTime datetime {get; set;} = DateTime.Now;
        public IFormFile image1 {get; set;}
        public IFormFile image2 {get; set;}
    }
}