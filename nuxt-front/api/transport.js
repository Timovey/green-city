export default ({ $post }) => ({
  fetch(filter) {
    filter.isFilterByCreatedUser = false;
    filter.isFilter = false;
    filter.sorting = {};
    return new Promise((resolve, reject) => {
      $post("/api/Transport/GetFilteredTransport", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
});
