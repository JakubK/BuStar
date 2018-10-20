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
    public BuStarRepository(string connectionString,
     IDataParseService dataParseServiceParam,
      IDateTimeService dateTimeServiceParam,
      IDataCache dataCacheParam)
    {
      repository = new LiteRepository(connectionString);

      this.dataParseService = dataParseServiceParam;
      this.dateTimeService = dateTimeServiceParam;
      this.dataCache = dataCacheParam;
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

      if(dataCache.GetCachedResponse(stop) != null)
      {
        if(dataCache.GetCachedResponse(stop).ResponseTime == response.ResponseTime)
        {
          return dataCache.GetCachedResponse(stop);
        }
      }
      var stops = GetStops().Where(x => x.Name.ToLower() == stop.ToLower());
      var buses = GetBuses();

      using (var client = new WebClient())
      {
        foreach(var s in stops) //each stopInfo
        {
          var stopInfoJson = client.DownloadString(new Uri("http://87.98.237.99:88/delays?stopId=" + s.Id));
          StopInfo info = dataParseService.ParseStopInfo(stopInfoJson);
          info.Longitude = s.Longitude;
          info.Latitude = s.Latitude;
          response.StopInfos.Add(info);
        }
      }

      foreach(var s in response.StopInfos)
      {
        foreach(var r in s.BusInfos)
        {
          r.RouteID = buses.Where(x => x.Id == r.RouteID).Select(y => y.Name).FirstOrDefault();
        }
      }

      dataCache.CacheResponse(stop, response);

      return response;
    }

    public IEnumerable<string> GetStopNames()
    {
      return repository.Query<Stop>().ToList().Select(x => x.Name).Distinct().OrderBy(x => x);
    }
  }
}