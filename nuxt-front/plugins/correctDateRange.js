const createDateFormat = "YYYY-MM-DD, HH:mm";

export default ({app}, inject) => {
    inject('correctDateRange', function(object) {
        if (!object) {
          return;
        }
        object.dateRange.createDate = app.$correctDate(
          object.dateRange.createDate,
          createDateFormat
        );
        object.dateRange.startDate = app.$correctDate(object.dateRange.startDate);
        object.dateRange.endDate = app.$correctDate(object.dateRange.endDate);
        return object;
    })
}
