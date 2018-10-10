using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BuStarAPI.Repository;
using BuStarAPI.Scheduling;
using BuStarAPI.Services;

namespace BuStarAPI.Tasks
{
  public class DownloadTristarDataTask : IScheduledTask
  {
    public string Schedule => "* */24 * * *";

    private IRepository repository;
    private IDataParseService dataParseService;

    public DownloadTristarDataTask(IRepository repositoryParam, IDataParseService dataParseServiceParam)
    {
      this.repository = repositoryParam;
      this.dataParseService = dataParseServiceParam;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      using (var client = new WebClient())
      {
        var stops = client.DownloadString(new Uri("http://91.244.248.19/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/cd4c08b5-460e-40db-b920-ab9fc93c1a92/download/stops.json"));
        var routes = client.DownloadString(new Uri("http://91.244.248.19/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/4128329f-5adb-4082-b326-6e1aea7caddf/download/routes.json"));

        //drop collections and send JSON to parsing service
        repository.Clear();
        var parsedBuses = dataParseService.ParseBuses(routes);
        var parsedStops = dataParseService.ParseStops(stops);
      

        repository.AddBuses(parsedBuses);
        repository.AddStops(parsedStops);

        await Task.FromResult(0);
      }
    }
  }
}