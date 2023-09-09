using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirWatch.Models
{
    public class ReadingData
    {
        public int humidity { get; set; }
        public int ammonia { get; set; }
        public int sulfurdioxide { get; set; }
        public int temperature { get; set; }
        public int carbonmonoxide { get; set; }
        public int nitrogenoxide { get; set; }
        public string apikey { get; set; }
    }
}