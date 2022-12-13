using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop_pbl6.Models.DTO.Positions
{
    public class DrivingStatistic
    {
        public double totalMile {get; set;}
        public double averageSpeed {get; set;}
        public double[][] road {get; set;}
        public string date {get; set;}
    }
}