using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    internal class WeatherYandex
    {
        public List<Forecast> forecasts { get; set; }

        public class Forecast
        {
            public string date { get; set; }
            public Part parts { get; set; }

            public class Part
            {
                public Detailed morning { get; set; }
                public Detailed day { get; set; }
                public Detailed evening { get; set; }
                public Detailed night { get; set; }

                public class Detailed
                {
                    public string temp_min { get; set; }
                    public string temp_max { get; set; }
                    public string temp_avg { get; set; }
                    public string feels_like { get; set; }
                }
            }
        }
    }
}
