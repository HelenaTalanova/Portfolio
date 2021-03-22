using MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Weather.Models;
using Weather.Services;

namespace Weather.ViewModels
{
    class WeatherCityViewModel : NotifyPropertyChanged
    {
        private readonly CollectionViewSource citiesSource;
        public ICollectionView Cities => citiesSource.View;

        public CityModel SelectedCity { get; set; }

        private readonly CollectionViewSource weatherSource;
        private readonly List<DetaliedViewModel> weather;

        public ICollectionView Weather => weatherSource.View;

        public Command Read => new Command(UpDate);

        public WeatherCityViewModel()
        {
            citiesSource = new CollectionViewSource() { Source = CityCollection.Cities };
            SelectedCity = CityCollection.Cities.FirstOrDefault();
                        
            weather = new List<DetaliedViewModel>()
            {
                new DetaliedViewModel("Утро"),
                new DetaliedViewModel("День"),
                new DetaliedViewModel("Вечер"),
                new DetaliedViewModel("Ночь"),
            };
            weatherSource = new CollectionViewSource() { Source = weather };
        }

        private async void UpDate()
        {
            string response = null;

            await Task.Run(() =>
                response = WeatherReader.Read(SelectedCity)
                );

            var weatherResponse = JsonConvert.DeserializeObject<WeatherYandex>(response);

            App.Current.Dispatcher?.Invoke(() =>
            {
                weather.Clear();
                weather.Add(new DetaliedViewModel(weatherResponse.forecasts[0].parts.morning, "Утро"));
                weather.Add(new DetaliedViewModel(weatherResponse.forecasts[0].parts.day, "День"));
                weather.Add(new DetaliedViewModel(weatherResponse.forecasts[0].parts.evening, "Вечер"));
                weather.Add(new DetaliedViewModel(weatherResponse.forecasts[0].parts.night, "Ночь"));
                Weather.Refresh();
            });
        }
    }

    class DetaliedViewModel
    {
        public string Name { get; }
        public string TMin { get; }
        public string T { get; }
        public string TMax { get; }
        public string TLike { get; }

        public DetaliedViewModel(string name)
        {
            Name = name;
            TMin = T = TMax = TLike = "-";
        }

        public DetaliedViewModel(WeatherYandex.Forecast.Part.Detailed detailed, string name)
        {
            Name = name;

            TMin = detailed.temp_min;
            T = detailed.temp_avg;
            TMax = detailed.temp_max;
            TLike = detailed.feels_like;
        }
    }
}
