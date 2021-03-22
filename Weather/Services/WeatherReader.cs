using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services
{
    internal static class WeatherReader
    {
        private const string KeyAPI = "698ab646-98c2-4590-bd18-022c6e702d99";

        public static string Read(CityModel city)
        {
            string url =
                "http://api.weather.yandex.ru/v2/forecast?" +
                $"lat={city.Lat}" +
                $"&lon={city.Lon}" +
                $"&limit=1" +
                $"&hours=false" +
                $"&extra=false";

            var Request = WebRequest.Create(url);
            Request.Headers.Add("X-Yandex-API-Key", KeyAPI);

            var Response = Request.GetResponse();

            string json;

            using (StreamReader r = new StreamReader(Response.GetResponseStream()))
            {
                json = r.ReadToEnd();
            }

            return json;
        }
    }
}
