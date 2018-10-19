using System;
using System.Globalization;

namespace BuStarAPI.Services
{
  public class DateTimeService : IDateTimeService
  {
    public string Current()
    {
      return DateTime.Now.ToString("HH:mm",CultureInfo.InvariantCulture);
    }
  }
}