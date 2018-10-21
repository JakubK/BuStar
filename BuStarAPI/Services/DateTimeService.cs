using System;
using System.Globalization;

namespace BuStarAPI.Services
{
  public class DateTimeService : IDateTimeService
  {
    public string Current()
    {
        var cetZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
        var cetTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cetZone);
        return cetTime.ToString("HH:mm", CultureInfo.InvariantCulture);
    }
  }
}