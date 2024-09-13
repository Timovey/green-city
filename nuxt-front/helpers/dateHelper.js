const baseDateFormat = "YYYY-MM-DD";
const createDateFormat = "YYYY-MM-DD, HH:mm";

export function correctDate(context, field, dateFormat = baseDateFormat) {
  if (field > "9999-12-31")  {
    return null
  }
  if (field) {
    return context.$moment(field).format(dateFormat);
  }
}

export function correctDateRange(context, object) {
  if (!object) {
    return;
  }
  object.dateRange.createDate = correctDate(
    context,
    object.dateRange.createDate,
    createDateFormat
  );
  object.dateRange.startDate = correctDate(context, object.dateRange.startDate);
  object.dateRange.endDate = correctDate(context, object.dateRange.endDate);
  return object;
}
