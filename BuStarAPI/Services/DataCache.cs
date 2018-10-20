using System.Collections.Generic;
using BuStarAPI.Models;

namespace BuStarAPI.Services
{
  public class DataCache : IDataCache
  {
    private Dictionary<string, StopInfoResponse> Cache;

    public DataCache()
    {
      Cache = new Dictionary<string, StopInfoResponse>();
    }
    public void CacheResponse(string stop, StopInfoResponse response)
    {
      Cache[stop] = response;
    }

    public StopInfoResponse GetCachedResponse(string stop)
    {
      if(Cache.ContainsKey(stop))
        return Cache[stop];
      else
        return null;
    }
  }
}