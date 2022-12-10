using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using eshop_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshop_pbl6.Models.DTO.Positions
{
    public class CreateUpdatePosition
    {
        public double longitude {get; set;}
        public double latitude {get;set;}
        public double speed {get; set;}
        public DateTime datetime {get; set;}
        public string image1 {get; set;}
        public string image2 {get; set;}
    }
}