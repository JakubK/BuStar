using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BuStarAPI.Scheduling;

namespace BuStarAPI.Tasks
{
  public class DownloadTristarDataTask : IScheduledTask
  {
    public string Schedule => "* */24 * * *";

    public DownloadTristarDataTask()
    {
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      using (var client = new WebClient())
      {
        var stops = client.DownloadString(new Uri("http://91.244.248.19/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/cd4c08b5-460e-40db-b920-ab9fc93c1a92/download/stops.json"));
        var routes = client.DownloadString(new Uri("http://91.244.248.19/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/4128329f-5adb-4082-b326-6e1aea7caddf/download/routes.json"));

        await Task.FromResult(0);
      }
    }
  }
}