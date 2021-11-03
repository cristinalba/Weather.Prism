using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Weather.Prism.Models;
using static Weather.Prism.Models.WeatherClasses;
using static Newtonsoft.Json.JsonConvert;

namespace Weather.Prism.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetWeatherAsync<T>(
            string urlBase,
            string servicePrefix,
            string city,
            string controller)
        {
            //const string WeatherCityUri = "http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=6101f97693f24c222e1d175691fda7bc";
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                var url = $"{servicePrefix}q={city}&units=metric&appid={controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        //const string WeatherCityUri = "http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=6101f97693f24c222e1d175691fda7bc";
        //public async Task<WeatherRoot> GetWeather(string city)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var url = string.Format(WeatherCityUri, city); 
        //        var json = await client.GetStringAsync(url);

        //        if (string.IsNullOrWhiteSpace(json))
        //            return null;

        //        return DeserializeObject<WeatherRoot>(json);
        //    }

        //}


        const string ForecastUri = "http://api.openweathermap.org/data/2.5/forecast?id={0}&units=metric&appid=6101f97693f24c222e1d175691fda7bc";
        public async Task<WeatherForecastRoot> GetForecast(int id)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(ForecastUri, id); 
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;
                
                return DeserializeObject<WeatherForecastRoot>(json);
            }

        }

    }
}
