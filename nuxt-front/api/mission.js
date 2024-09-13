export default ({ $post }) => ({
  fetch(filter) {
    return new Promise((resolve, reject) => {
      $post("/api/App/GetCommonFilteredMissions", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  get(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetMissionsForDispatcherByRouteId", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  update(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/UpdateMissionsForCloseRoute", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
});
