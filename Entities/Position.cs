using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using eshop_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace eshop_pbl6.Entities
{
    public class Position
    {
        [Key]
        public int id {get; set;}
        public double longitude {get; set;}
        public double latitude {get;set;}
        public double speed {get; set;}
        public DateTime datetime {get; set;}
        [ForeignKey("User")]
        public int idUser {get; set;}
        public string url1 {get; set;}
        public string url2{get; set;}
        [JsonIgnore]
        public virtual User user {get; set;}
        
    }
}