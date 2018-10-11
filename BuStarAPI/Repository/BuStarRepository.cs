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
    public BuStarRepository(string connectionString, IDataParseService dataParseServiceParam)
    {
      repository = new LiteRepository(connectionString);
      this.dataParseService = dataParseServiceParam;
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
      StopInfoResponse response = new StopInfoResponse();
      using (var client = new WebClient())
      {
        foreach(var s in stops) //each stopInfo
        {
          var stopInfoJson = client.DownloadString(new Uri("http://87.98.237.99:88/delays?stopId=" + s.Id));
          response.StopInfos.Add(dataParseService.ParseStopInfo(stopInfoJson));
        }
      }

      return response;
    }

    public IEnumerable<string> GetStopNames()
    {
      return repository.Query<Stop>().ToList().Select(x => x.Name).Distinct();
    }
  }
}