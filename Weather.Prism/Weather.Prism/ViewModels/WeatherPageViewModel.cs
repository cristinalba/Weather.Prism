using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Weather.Prism.Models;
using Weather.Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Weather.Prism.Models.WeatherClasses;

namespace Weather.Prism.ViewModels
{
    public class WeatherPageViewModel : ViewModelBase
    {
        #region atributos

        //private const string _useCityKey = "use_city";
        //private static readonly bool _useCityDefault = true;


        //private const string _cityKey = "city";
        //private static readonly string _cityDefault = "Madrid";

        private string _city;

        private readonly IApiService _apiService;
        private List<string> _cities = new List<string> { "Dublin", "Lisbon", "Madrid" };

        private string _temp = string.Empty;
        private string _condition = string.Empty;

        private bool _isBusy;
        

        #endregion


        WeatherClasses WeatherService { get; } = new WeatherClasses();
        WeatherForecastRoot forecast;

        public WeatherPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "WeatherPage";
            //LoadCitiesAsync();
        }

        #region comandos
        ICommand getWeather;
        public ICommand GetWeatherCommand =>
                getWeather ??
                (getWeather = new Command(async () => await ExecuteGetWeatherCommand()));
        #endregion

        #region propiedades
        //public static bool UseCity
        //{
        //    get => Preferences.Get(_useCityKey, _useCityDefault);
        //    set => Preferences.Set(_useCityKey, value);
        //}

        public string City //location
        {
            get { return _city; }
            set => SetProperty(ref _city, value);
        }
        public string Temp
        {
            get { return _temp; }
            set { SetProperty(ref _temp, value); }
        }

        
        public string Condition
        {
            get { return _condition; }
            set { SetProperty(ref _condition, value); ; }
        }

        public WeatherForecastRoot Forecast
        {
            get { return forecast; }
            set { forecast = value; } // OnPropertyChanged(); }
        }

        public bool IsBusy  //to read, write and clear
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        #endregion

        #region

        private async Task ExecuteGetWeatherCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                //WeatherRoot weatherRoot = null;

                ////Get weather by city
                //weatherRoot = await _apiService.GetWeather(City);           

                ////Get forecast based on cityId
                //Forecast = await _apiService.GetForecast(weatherRoot.CityId);

                //Temp = $"Temperature: {weatherRoot?.MainWeather?.Temperature ?? 0}°C";
                //Condition = $"{weatherRoot.Name}: {weatherRoot?.Weather?[0]?.Description ?? string.Empty}";

                string url = App.Current.Resources["UrlAPI"].ToString();

                Response response = await _apiService.GetWeatherAsync<WeatherRoot>(url, "/data/2.5/weather?", _city, "6101f97693f24c222e1d175691fda7bc");

                if(!response.IsSuccess)
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                    return;
                }

                var weatherRoot = (WeatherRoot)response.Result;

                Temp = $"Temperature: {weatherRoot?.MainWeather?.Temperature ?? 0}°C";
                Condition = $"{weatherRoot.Name}: {weatherRoot?.Weather?[0]?.Description ?? string.Empty}";


            }
            catch (Exception ex)
            {
                Temp = "Info not available. Try again later";
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

       
        #endregion

    }
}
