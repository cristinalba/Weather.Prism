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


        private string _city;
        private string _icon;
       

        private string _press;
        private string _hum;


        private readonly IApiService _apiService;
        private List<string> _cities = new List<string> { "Dublin", "Lisbon", "Madrid" };

        private string _temp = string.Empty;
        private string _condition = string.Empty;

        private bool _isBusy;
        

        #endregion


        //WeatherClasses WeatherService { get; } = new WeatherClasses();
        WeatherForecastRoot forecast;

        public WeatherPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Weather";
            //LoadCitiesAsync();
        }

        #region comandos
        ICommand getWeather;
        public ICommand GetWeatherCommand =>
                getWeather ??
                (getWeather = new Command(async () => await ExecuteGetWeatherCommand()));
        #endregion

        #region propiedades
 
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
            set { SetProperty(ref _condition, value); }
        }
        public string Hum
        {
            get { return _hum; }
            set { SetProperty(ref _hum, value); }
        }
        public string Press
        {
            get { return _press; }
            set { SetProperty(ref _press, value); }
        }
        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
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

            
            if (string.IsNullOrEmpty(City))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a city.", "Accept");
                return;
            }
            else
            {
                IsBusy = true;
                try
                {

                    string url = App.Current.Resources["UrlAPI"].ToString();

                    Response response = await _apiService.GetWeatherAsync<WeatherRoot>(url, "/data/2.5/weather?", _city, "6101f97693f24c222e1d175691fda7bc");

                    if(!response.IsSuccess)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                        return;
                       
                    }

                    var weatherRoot = (WeatherRoot)response.Result;

                    Temp = $"Temperature: {weatherRoot?.MainWeather?.Temperature ?? 0}°C in {weatherRoot.Name}";
                
                    Hum = $"Humidity: {weatherRoot?.MainWeather?.Humidity ?? 0}%"; 
                    Press = $"Pressure: {weatherRoot?.MainWeather?.Pressure ?? 0}hPa";

                    Condition = $"{weatherRoot.Name} : {weatherRoot?.Weather?[0]?.Description ?? string.Empty}";

                    if(weatherRoot.Weather[0].Description.Contains("cloud"))
                    Icon = "cloudy";
                    else if(weatherRoot.Weather[0].Description.Contains("clear"))
                    Icon = "sunny";

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

        }

       
        #endregion

    }
}
