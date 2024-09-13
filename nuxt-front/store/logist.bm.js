import { deepCopy } from "@/helpers/deepCopyHelper";

export const state = () => ({
  bms: [],
  stands: [],
});

export const actions = {
  fetchBms({ commit }, data = { sorting: {} }) {
    return new Promise((resolve, reject) => {
      // console.log('data',data);
      this.$post("/api/Logist/getFilteredBaseRoutes", data)
        .then((res) => {
          let result = deepCopy(res.data.data);
          commit("setBms", result);
          return resolve(result);
        })
        .catch((err) => {
          return reject(err);
        });
    });
  },
  fetchStands({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      // console.log('data',data);
      this.$post("/api/Sd/getFilteredStandForBaseRoute ", data)
        .then((res) => {
          let result = deepCopy(res.data.data);
          commit("setStands", result);
          return resolve(result);
        })
        .catch((err) => {
          return reject(err);
        });
    });
  },
  deleteBm({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Logist/DeleteBaseRoute", data)
        .then((res) => {
          let result = state.bms.filter((item) => data.id != item.id);
          commit("setBms", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  addBm({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Logist/CreateBaseRoute", data)
        .then((res) => {
          let result = [...state.bms];
          result.push(deepCopy({ ...res.data.data }));
          commit("setBms", result);
          return resolve(result);
        })
        .catch((err) => {
          console.log(err);
          reject(err);
        });
    });
  },
  updateBm({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Logist/UpdateBaseRoute", data)
        .then((res) => {
          let result = [...state.bms].map((elem) => {
            if (elem.id == data.id) return deepCopy(res.data.data);
            else return elem;
          });
          commit("setBms", result);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  getBm({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Logist/GetBaseRouteById", data)
        .then((res) => {
          let result = deepCopy(res.data.data);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
};

export const mutations = {
  setBms(state, bms) {
    state.bms = bms;
  },
  setStands(state, stands) {
    state.stands = stands;
  },
};
