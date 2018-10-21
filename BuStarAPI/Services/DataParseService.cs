using System;
using System.Collections.Generic;
using BuStarAPI.Models;
using Newtonsoft.Json.Linq;

namespace BuStarAPI.Services
{
  public class DataParseService : IDataParseService
  {
    public List<Bus> ParseBuses(string json)
    {
      JObject jObject = JObject.Parse(json);
      string date = DateTime.Now.ToString("yyyy-MM-dd");
      var routes = jObject[date]["routes"];

      Dictionary<JToken, Bus> buses = new Dictionary<JToken, Bus>();
      List<Bus> result = new List<Bus>();

      foreach(var route in routes)
      {
        buses[route["routeId"]] = new Bus { Id = route["routeId"].ToString().Trim(), Name = route["routeShortName"].ToString().Trim()};
      }

      foreach(var item in buses)
      {
        result.Add(new Bus { Id = item.Value.Id, Name = item.Value.Name});
      }

      return result;
    }

    public StopInfo ParseStopInfo(string json)
    {
      JObject jObject = JObject.Parse(json);
      StopInfo result = new StopInfo();

      foreach(var bus in jObject["delay"])
      {
        result.BusInfos.Add(new BusInfo
        {
          EstimatedTime = bus["estimatedTime"].ToString(),
          Headsign = bus["headsign"].ToString(),
          RouteID = bus["routeId"].ToString(),
          TheoreticalTime = bus["theoreticalTime"].ToString()
        });
      }

      return result;
    }

    public List<Stop> ParseStops(string json)
    {
      JObject jObject = JObject.Parse(json);
      string date = DateTime.Now.ToString("yyyy-MM-dd");
      var routes = jObject[date]["stops"];

      Dictionary<JToken, Stop> stops = new Dictionary<JToken, Stop>();
      List<Stop> result = new List<Stop>();

      foreach(var route in routes)
      {
        stops[route["stopId"]] = new Stop { Id = route["stopId"].ToString().Trim()
        , Name = route["stopDesc"].ToString().Trim(),
          Coordinates = new Coordinates
          {
            Latitude = route["stopLat"].ToString(), 
            Longitude = route["stopLon"].ToString()
          }
        };
      }

      foreach(var item in stops)
      {
        result.Add(new Stop { Id = item.Value.Id,
         Name = item.Value.Name,
         Coordinates = item.Value.Coordinates
        });
      }

      return result;    
    }

    public WeatherInfo ParseWeatherInfo(string json)
    {
      JObject jObject = JObject.Parse(json);

      return new WeatherInfo
      {
        ID = jObject["weather"][0]["id"].ToString(),
        Name = jObject["weather"][0]["main"].ToString(),
        Description = jObject["weather"][0]["description"].ToString()
      };
    }
  }
}