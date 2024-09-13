using TransportContracts.DateRange;

namespace TransportService.Core.Helpers
{
    internal static class DateRangeHelper
    {
        internal static void Normalize<T>(this T model) where T : DateRangeBaseModel, new()
        {
            model.CreateDate = DateTime.Now;
            model.StartDate = new DateTime(model.StartDate.Year, model.StartDate.Month, model.StartDate.Day);
            if(model.EndDate != null)
            {
                model.EndDate = new DateTime(
                    model.EndDate.Value.Year, 
                    model.EndDate.Value.Month, 
                    model.EndDate.Value.Day
                    );
            }
            else
            {
                model.EndDate = DateTime.MaxValue;
            }
        }

        internal static DateTime Normalize(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
