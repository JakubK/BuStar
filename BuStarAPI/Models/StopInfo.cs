using System.Collections.Generic;

namespace BuStarAPI.Models
{
  public class StopInfo
  {
    public List<BusInfo> BusInfos {get;set;}

    public WeatherInfo WeatherInfo {get;set;}

    public Coordinates Coordinates {get;set;}

    public StopInfo()
    {
      BusInfos = new List<BusInfo>();
    }
  }
}