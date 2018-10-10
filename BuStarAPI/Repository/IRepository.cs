using System.Collections.Generic;
using BuStarAPI.Models;

namespace BuStarAPI.Repository
{
  public interface IRepository
  {
      void Clear();
      void AddBuses(IEnumerable<Bus> buses);
      List<Bus> GetBuses();
      void AddStops(IEnumerable<Stop> stops);
      IEnumerable<string> GetStopNames();
  }
}