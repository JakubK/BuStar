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
    public BuStarRepository(string connectionString, IDataParseService dataParseServiceParam, IDateTimeService dateTimeServiceParam)
    {
      repository = new LiteRepository(connectionString);

      this.dataParseService = dataParseServiceParam;
      this.dateTimeService = dateTimeServiceParam;
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
      var stops = GetStops().Where(x => x.Name.ToLower() == stop.ToLower());
      var buses = GetBuses();
      StopInfoResponse response = new StopInfoResponse();
      response.ResponseTime = dateTimeService.Current();
      System.Diagnostics.Debug.WriteLine(dateTimeService.Current());
      System.Diagnostics.Debug.WriteLine(DateTime.Now);
      using (var client = new WebClient())
      {
        foreach(var s in stops) //each stopInfo
        {
          var stopInfoJson = client.DownloadString(new Uri("http://87.98.237.99:88/delays?stopId=" + s.Id));
          response.StopInfos.Add(dataParseService.ParseStopInfo(stopInfoJson));
        }
      }

      foreach(var s in response.StopInfos)
      {
        foreach(var r in s.BusInfos)
        {
          r.RouteID = buses.Where(x => x.Id == r.RouteID).Select(y => y.Name).FirstOrDefault();
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