using System.Collections.Generic;

namespace BuStarAPI.Models
{
  public class StopInfo
  {
    public List<BusInfo> BusInfos {get;set;}

    public string Longitude {get;set;}
    public string Latitude {get;set;}

    public StopInfo()
    {
      BusInfos = new List<BusInfo>();
    }
  }
}