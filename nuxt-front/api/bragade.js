export default ({ $post }) => ({
  fetch(filter) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetFilteredBrigades", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchRoutes(filter) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetAvailableRoutesForCreateBrigade", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  delete(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/DeleteBrigades", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  create(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/CreateBrigade", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
});
