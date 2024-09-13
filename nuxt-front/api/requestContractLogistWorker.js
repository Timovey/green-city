export default ({ $post }) => ({
    fetch(filter = {}) {
      return new Promise((resolve, reject) => {
        $post("/api/App/GetCommonFilteredApps", filter)
          .then((res) => resolve(res.data.data))
          .catch((err) => reject(err));
      });
    },
    add(data) {
      return new Promise((resolve, reject) => {
        $post("api/App/CreateAppByContractClientByLogist", data)
          .then((res) => {
            resolve(res.data.data);
          })
          .catch((err) => reject(err));
      });
    },
    delete(data) {
      return new Promise((resolve, reject) => {
        $post("/api/App/DeleteAppById", data)
          .then((res) => {
            resolve(res.data.data);
          })
          .catch((err) => reject(err));
      });
    },
    get(data) {
      return new Promise((resolve, reject) => {
        $post("/api/App/GetRealAppById", data)
          .then((res) => {
            console.log(res)
            resolve(res.data.data);
          })
          .catch((err) => reject(err));
      });
    },
    update(data) {
      return new Promise((resolve, reject) => {
        $post("/api/App/UpdateAppByContractClient", data)
        .then((res) => {
          resolve(res.data.data);
        })
        .catch((err) => reject(err));
      });
    },
  });
  