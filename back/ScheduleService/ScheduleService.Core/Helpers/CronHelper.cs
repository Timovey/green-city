using Quartz;
using System;

namespace ScheduleService.Core.Helpers
{
    internal class CronHelper
    {
        public static (DateTime NextFireDate, string[] Errors) GetNextFireDateByCron(string cron, DateTime initialDate)
        {
            CronExpression cronExpression;
            try
            {
                cronExpression = new CronExpression(cron);
            }
            catch
            {
                return (DateTime.MaxValue, new string[] { $"Невалидный крон! {cron}" });
            }

            //!_! Если переданная дата уже подходит под крон то ее и вернем
            if (cronExpression.IsSatisfiedBy(initialDate.Date))
            {
                return (initialDate.Date, default);
            }

            var result = cronExpression.GetNextValidTimeAfter(initialDate) ?? DateTimeOffset.MaxValue;

            result = result.ToUniversalTime().AddHours(4);
            return (result.DateTime.Date, default);
        }

        public static (bool IsFire, string[] Errors) IsDateFireByCron(DateTime date, string cron)
        {
            CronExpression cronExpression;
            try
            {
                cronExpression = new CronExpression(cron);
            }
            catch
            {
                return (false, new string[] { $"Невалидный крон! {cron}" });
            }

            var result = cronExpression.IsSatisfiedBy(date);

            return (result, default);
        }
    }
}
