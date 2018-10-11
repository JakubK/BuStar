using System.Collections.Generic;
using BuStarAPI.Models;

namespace BuStarAPI.Repository
{
  public interface IRepository
  {
      void Clear();
      void AddBuses(IEnumerable<Bus> buses);
      List<Bus> GetBuses();
      List<Stop> GetStops();
      void AddStops(IEnumerable<Stop> stops);

      StopInfoResponse GetStopData(string stop);
      IEnumerable<string> GetStopNames();
  }
}