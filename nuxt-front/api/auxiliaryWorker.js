export default ({ $post }) => ({
  fetch(filter) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetFilteredAuxiliaryWorkers", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  delete(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/DeleteAuxiliaryWorker", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  get(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/GetAuxiliaryWorker", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  create(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/CreateAuxiliaryWorker", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  update(data) {
    return new Promise((resolve, reject) => {
      $post("api/Dispatcher/UpdateAuxiliaryWorker", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
});
