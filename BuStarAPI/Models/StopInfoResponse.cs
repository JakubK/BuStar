using System.Collections.Generic;

namespace BuStarAPI.Models
{
  public class StopInfoResponse
  {
    public List<StopInfo> StopInfos {get;set;}
    public string ResponseTime {get;set;}

    public StopInfoResponse()
    {
      StopInfos = new List<StopInfo>();
    }
  }
}