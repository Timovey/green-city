export default ({ $post }) => ({
  fetchShiftTask(filter = {}) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetCommonFilteredShiftTasks", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchRoutes(filter = {}) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetAvailableRouteForCreateShiftTask", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchOrganizations(filter = {}) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetOrgsDropDownForCreateShiftTask", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  delete(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/DeleteShiftTasks", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },

  create(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/CreateShiftTask", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
});
