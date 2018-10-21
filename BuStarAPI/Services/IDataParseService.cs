using System.Collections.Generic;
using BuStarAPI.Models;

namespace BuStarAPI.Services
{
  public interface IDataParseService
  {
    List<Bus> ParseBuses(string json);
    List<Stop> ParseStops(string json);
    StopInfo ParseStopInfo(string json);
    WeatherInfo ParseWeatherInfo(string json);
  }
}