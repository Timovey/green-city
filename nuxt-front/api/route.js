export default ({ $post }) => ({
  fetchRoutes(filter = {}) {
    filter.isFilter = true;
    filter.isFilterByCreatedUser = false;
    if (!filter.date) {
      filter.date = new Date();
    }
    return new Promise((resolve, reject) => {
      $post("/api/Logist/GetFilteredRoutes", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchStands(filter = {}) {
    filter.isFilterByCreatedUser = false;
    return new Promise((resolve, reject) => {
      $post("/api/Sd/GetFilteredStandsForRoute", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  getRouteStands(data = {}) {
    return new Promise((resolve, reject) => {
      $post("/api/Sd/GetStandsByRouteId", data)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  delete(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Logist/DeleteRoute", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  get(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Logist/GetRouteById", data)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  create(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Logist/CreateRoute", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
  update(data) {
    return new Promise((resolve, reject) => {
      $post("/api/Logist/UpdateRoute", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
});
