using LiteDB;

namespace BuStarAPI.Models
{
  public class Stop
  {
    public string Id {get;set;}
    public string Name {get;set;}
    public Coordinates Coordinates {get;set;}
  }
}