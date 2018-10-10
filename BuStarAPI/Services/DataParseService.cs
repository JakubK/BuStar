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
        buses[route["routeId"]] = new Bus { Id = route["routeId"].ToString(), Name = route["routeShortName"].ToString()};
      }

      foreach(var item in buses)
      {
        result.Add(new Bus { Id = item.Value.Id, Name = item.Value.Name});
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
        stops[route["stopId"]] = new Stop { Id = route["stopId"].ToString(), Name = route["stopDesc"].ToString()};
      }

      foreach(var item in stops)
      {
        result.Add(new Stop { Id = item.Value.Id, Name = item.Value.Name});
      }

      return result;    
    }
  }
}