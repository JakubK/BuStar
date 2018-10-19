using System;

namespace BuStarAPI.Services
{
  public class DateTimeService : IDateTimeService
  {
    public string Current()
    {
      return (DateTime.Now.Hour + ":" + DateTime.Now.Minute);
    }
  }
}