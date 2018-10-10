using System.Collections.Generic;
using System.Linq;
using BuStarAPI.Models;
using LiteDB;

namespace BuStarAPI.Repository
{
  public class BuStarRepository : IRepository
  {
    private LiteRepository repository;
    public BuStarRepository(string connectionString)
    {
      repository = new LiteRepository(connectionString);
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

    public IEnumerable<string> GetStopNames()
    {
      return repository.Query<Stop>().ToList().Select(x => x.Name).Distinct();
    }
  }
}