using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BuStarAPI.Models;
using BuStarAPI.Services;
using LiteDB;

namespace BuStarAPI.Repository
{
  public class BuStarRepository : IRepository
  {
    private LiteRepository repository;
    private IDataParseService dataParseService;
    private IDateTimeService dateTimeService;
    private IDataCache dataCache;
    private IWeatherCache weatherCache;

    private string weatherApiKey;
    public BuStarRepository(string connectionString,
      string weatherApiKey,
      IDataParseService dataParseServiceParam,
      IDateTimeService dateTimeServiceParam,
      IDataCache dataCacheParam,
      IWeatherCache weatherCacheParam)
    {
      repository = new LiteRepository(connectionString);
      this.weatherApiKey = weatherApiKey;

      this.dataParseService = dataParseServiceParam;
      this.dateTimeService = dateTimeServiceParam;
      this.dataCache = dataCacheParam;
      this.weatherCache = weatherCacheParam;
    }
    public void AddBuses(IEnumerable<Bus> buses)
    {
      repository.Insert<Bus>(buses);
    }

    public void AddStops(IEnumerable<Stop> stops)
    {
      repository.Insert<Stop>(stops);
    }

    public void Clear()
    {
      repository.Database.DropCollection("stop");
      repository.Database.DropCollection("bus");
    }

    public List<Bus> GetBuses()
    {
      return repository.Query<Bus>().ToList();
    }
    public List<Stop> GetStops()
    {
      return repository.Query<Stop>().ToList();
    }

    public StopInfoResponse GetStopData(string stop)
    {
      StopInfoResponse response = new StopInfoResponse();
      response.ResponseTime = dateTimeService.Current();

      if(dataCache.GetCachedResponse(stop) != null && dataCache.GetCachedResponse(stop).ResponseTime == response.ResponseTime)
      {
          response.StopInfos = dataCache.GetCachedResponse(stop).StopInfos;
      }
      else
      {
          var stops = GetStops().Where(x => x.Name.ToLower() == stop.ToLower());
          var buses = GetBuses();

          using (var client = new WebClient())
          {
            foreach(var s in stops) //each stopInfo
            {
              var stopInfoJson = client.DownloadString(new Uri("http://87.98.237.99:88/delays?stopId=" + s.Id));
              StopInfo info = dataParseService.ParseStopInfo(stopInfoJson);

              info.Coordinates = s.Coordinates;
              response.StopInfos.Add(info);
            }
          }

          
        foreach(var s in response.StopInfos)
        {
          foreach(var r in s.BusInfos) // assign Bus names instead of ID's
          {
            r.RouteID = buses.Where(x => x.Id == r.RouteID).Select(y => y.Name).FirstOrDefault();
          }
        }

        dataCache.CacheResponse(stop, response);
      }

      foreach (var s in response.StopInfos)
      {
         if(s.BusInfos.Count > 0) //ask for a weather for active stops
          {
            //ask Cache first
            using (var client = new WebClient())
            {
              if(weatherCache.GetCachedWeather(s.Coordinates) != null 
              && weatherCache.GetCachedWeather(s.Coordinates).Time < dateTimeService.Now.AddMinutes(15))
              {
                s.WeatherInfo = weatherCache.GetCachedWeather(s.Coordinates);
              }
              else
              {
                //get data and cache them
                  var weatherInfoJson = client.DownloadString(new Uri("http://api.openweathermap.org/data/2.5/weather?lat=" + s.Coordinates.Latitude + "&lon=" + s.Coordinates.Longitude + "&appid=" + weatherApiKey));
                  WeatherInfo weatherInfo = dataParseService.ParseWeatherInfo(weatherInfoJson);
                  s.WeatherInfo = weatherInfo;
                  s.WeatherInfo.Time = dateTimeService.Now;
                  weatherCache.CacheWeather(s.Coordinates, weatherInfo);
              }
            }
          }
      }

      return response;
    }

    public IEnumerable<string> GetStopNames()
    {
      return repository.Query<Stop>().ToList().Select(x => x.Name).Distinct().OrderBy(x => x);
    }
  }
}