export const state = () => ({
  contragents: [],
});

export const actions = {
  fetchContragents({ commit }, data = {}) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Sd/getFilteredContractClient", data)
        .then((res) => {
          let result = res.data.data;
          commit("setContragents", result);
          return resolve(result);
        })
        .catch((err) => {
          return reject(err);
        });
    });
  },
  deleteContragent({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Sd/DeleteContractClient", data)
        .then((res) => {
          let result = state.contragents.filter(
            (item) => item.contractClientId != data.contractClientId
          );
          commit("setContragents", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  addContragent({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Sd/CreateContractClient",data)
        .then((res) => {
          let result = [...state.contragents];
          result.push(data);
          console.log(result);
          commit("setContragents", result);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  updateContragent({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Sd/UpdateContractClient", data)
        .then((res) => {
          let result = this.$deepCopy(state.contragents).map(
            (elem) => {
              if ((elem.contractClientId = data.contractClientId)) return data;
              else return elem;
            }
          );
          commit("setContragents", result);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  getContragent({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Sd/GetContractClient", data)
        .then((res) => {
          return resolve(res.data.data);
        })
        .catch((err) => reject(err));
    });
  },
};

export const mutations = {
  setContragents(state, contragents) {
    state.contragents = contragents;
  },
};
