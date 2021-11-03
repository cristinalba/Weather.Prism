using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weather.Prism.Models;
using static Weather.Prism.Models.WeatherClasses;

namespace Weather.Prism.Services
{
    public interface IApiService
    {
        

        Task<WeatherForecastRoot> GetForecast(int id);

        //Task<WeatherRoot> GetWeather(string city);

        Task<Response> GetWeatherAsync<T>(
           string urlBase,
           string servicePrefix,
           string city,
           string controller);
    }
}
