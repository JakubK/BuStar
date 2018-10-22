using System;
using System.Globalization;

namespace BuStarAPI.Services
{
  public class DateTimeService : IDateTimeService
  {
    public DateTime Now
    {
      get
      {
         return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time"));
      }
    }
    public string Current()
    {
        var cetZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
        var cetTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
        return cetTime.ToString("HH:mm", CultureInfo.InvariantCulture);
    }
  }
}