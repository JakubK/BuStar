using BuStarAPI.Models;

namespace BuStarAPI.Services
{
  public interface IWeatherCache
  {
    WeatherInfo GetCachedWeather(Coordinates coord);
    void CacheWeather(Coordinates coord, WeatherInfo response);
  }
}