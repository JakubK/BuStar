using System;

namespace BuStarAPI.Models
{
  public class WeatherInfo
  {
    public string ID {get;set;}
    public string Description {get;set;}

    public string Temperature {get;set;}
    public DateTime Time {get;set;} 
  }
}