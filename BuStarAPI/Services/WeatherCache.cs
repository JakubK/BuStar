using System.Collections.Generic;
using BuStarAPI.Models;

namespace BuStarAPI.Services
{
  public class WeatherCache : IWeatherCache
  {
    private Dictionary<Coordinates, WeatherInfo> Cache;
    public WeatherCache()
    {
      Cache = new Dictionary<Coordinates, WeatherInfo>();
    }
    public void CacheWeather(Coordinates coord, WeatherInfo weather)
    {
      Cache[coord] = weather;
    }

    public WeatherInfo GetCachedWeather(Coordinates coord)
    {
      if(Cache.ContainsKey(coord))
        return Cache[coord];
      else
        return null;
    }
  }
}