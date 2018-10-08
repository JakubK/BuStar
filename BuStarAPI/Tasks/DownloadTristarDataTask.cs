using System.Threading;
using System.Threading.Tasks;
using BuStarAPI.Scheduling;

namespace BuStarAPI.Tasks
{
  public class DownloadTristarDataTask : IScheduledTask
  {
    public string Schedule => "* */6 * * *";

    public DownloadTristarDataTask()
    {
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      await Task.Delay(0);
      System.Diagnostics.Debug.WriteLine("CRON TASK EXECUTED");
    }
  }
}