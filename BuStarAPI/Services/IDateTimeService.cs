using System;

namespace BuStarAPI.Services
{
  public interface IDateTimeService
  {
      string Current();
      DateTime Now {get;}
  }
}