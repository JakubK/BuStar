using System;
using BuStarAPI.Scheduling.Cron;

namespace BuStarAPI.Scheduling
{
  public class SchedulerTaskWrapper
  {
      public CrontabSchedule Schedule { get; set; }
      public IScheduledTask Task { get; set; }

      public DateTime LastRunTime { get; set; }
      public DateTime NextRunTime { get; set; }

      public void Increment()
      {
          LastRunTime = NextRunTime;
          NextRunTime = Schedule.GetNextOccurrence(NextRunTime);
      }

      public bool ShouldRun(DateTime currentTime)
      {
          return NextRunTime < currentTime && LastRunTime != NextRunTime;
      }
  }
}