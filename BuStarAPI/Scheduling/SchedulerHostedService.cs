using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BuStarAPI.Scheduling.Base;
using BuStarAPI.Scheduling;
using System;
using System.Linq;
using BuStarAPI.Scheduling.Cron;

namespace BuStarAPI.Scheduling
{
  public class SchedulerHostedService : HostedService
  {
    public event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;
    private readonly List<SchedulerTaskWrapper> ScheduledTasks = new List<SchedulerTaskWrapper>();
    public SchedulerHostedService(IEnumerable<IScheduledTask> scheduledTasks)
    {
      var referenceTime = DateTime.UtcNow;

      foreach(var scheduledTask in scheduledTasks)
      {
        ScheduledTasks.Add( new SchedulerTaskWrapper
        {
          Schedule = CrontabSchedule.Parse(scheduledTask.Schedule),
          Task = scheduledTask,
          NextRunTime = referenceTime
        });
      }
    }
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      while(!cancellationToken.IsCancellationRequested)
      {
        await ExecuteOnceAsync(cancellationToken);
        await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
      }
    }

    private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
    {
      var taskFactory = new TaskFactory(TaskScheduler.Current);
      var referenceTime = DateTime.UtcNow;

      var tasksThatShouldRun = ScheduledTasks.Where(t => t.ShouldRun(referenceTime)).ToList();

      foreach(var taskThatShouldRun in tasksThatShouldRun)
      {
        taskThatShouldRun.Increment();

        await taskFactory.StartNew(
          async () =>
          {
            try
            {
              await taskThatShouldRun.Task.ExecuteAsync(cancellationToken);
            }
            catch(Exception ex)
            {
              var args = new UnobservedTaskExceptionEventArgs(
                ex as AggregateException ?? new AggregateException(ex));

                UnobservedTaskException?.Invoke(this, args);

                if(!args.Observed)
                {
                  throw;
                }
              
            }
          }, cancellationToken
        );
      }
    }
  }
}