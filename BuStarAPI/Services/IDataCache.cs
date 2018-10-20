using BuStarAPI.Models;

namespace BuStarAPI.Services
{
  public interface IDataCache
  {
    StopInfoResponse GetCachedResponse(string stop);
    void CacheResponse(string stop, StopInfoResponse response);
  }
}