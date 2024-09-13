export default ({ $post }) => ({
  fetch(filter) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetCommonFilteredTravelSheets", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  delete(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/DeleteTravelSheets", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  get(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/GetTravelSheetById", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  create(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/CreateTravelSheet", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  update(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Dispatcher/FixTravelSheetDriveTimes", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
});
