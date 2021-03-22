using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Models
{
    internal class CityCollection
    {
        public static List<CityModel> Cities { get; } = new List<CityModel>()
        {
            new CityModel() { Name = "Орел", Lat = "52.970756", Lon ="36.064349" },
            new CityModel() { Name = "Москва", Lat = "55.755814", Lon ="37.617635" },
            new CityModel() { Name = "Санкт-Петербург", Lat = "59.939095", Lon ="30.315868" },
            new CityModel() { Name = "Нижний Новгород", Lat = "58.010450", Lon ="44.023225" },
            new CityModel() { Name = "Пермь", Lat = "58.010450", Lon ="56.229434" },
        };
    }
}
