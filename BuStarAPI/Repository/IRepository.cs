using System.Collections.Generic;
using BuStarAPI.Models;

namespace BuStarAPI.Repository
{
  public interface IRepository
  {
      void Clear();
      void AddBus(Bus bus);
      List<Bus> GetBuses();
      void AddStop(Stop stop);
      List<Stop> GetStops();
  }
}