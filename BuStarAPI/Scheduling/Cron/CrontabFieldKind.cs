using System;

namespace BuStarAPI.Scheduling.Cron
{
  [Serializable]
    public enum CrontabFieldKind
    {
        Minute,
        Hour,
        Day,
        Month,
        DayOfWeek
    }
}