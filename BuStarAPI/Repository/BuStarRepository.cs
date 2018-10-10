using System.Collections.Generic;
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
    public void AddBus(Bus bus)
    {
      repository.Insert<Bus>(bus);
    }

    public void AddStop(Stop stop)
    {
      repository.Insert<Stop>(stop);
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
  }
}