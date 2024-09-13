import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
import { correctDate } from "@/helpers/dateHelper";

export const state = () => ({
  transports: [],
});

export const actions = {
  fetchTransports({ commit }, data = { sorting: {} }) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/getFilteredTransport", data)
        .then((res) => {
          let result = deepCopy(res.data.data)
          result.forEach(element => {
            element.lastTO = correctDate(this, element.lastTO);
          });
          commit("setTransports", result);
          resolve(result);
        })
        .catch((err) => {
          reject(err);
        });
    });
  },
  addTransport({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/CreateTransport", data)
        .then((res) => {
          if (!res.data.errors) {
            let result = [...state.transports];
            let transport = deepCopy(res.data.data);
            transport.lastTO = correctDate(this, transport.lastTO);
            result.push(transport);
            commit("setTransports", result);
            resolve(res.data.data);
          } else reject(res.data.errors);

          console.log(res);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  getTransport(_, data = { sorting: {} }) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/getTransport", data)
        .then((res) => {
          resolve(deepCopy(res.data.data));
        })
        .catch((err) => {
          reject("Ошибка получения транспорта", err);
        });
    });
  },
  deleteTransport({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/DeleteTransport", data)
        .then((res) => {
          let result = state.transports.filter(
            (item) => item.id != data.transportIds[0]
          );
          commit("setTransports", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  updateTransport({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this,
        "/api/Transport/UpdateTransport",
        (data = { ...data, transportIds: [data.id] })
      )
        .then((res) => {
          let result = deepCopy(state.transports).map((elem) => {
            if (elem.id == data.id) {
              console.log(res.data.data);
              let transport = deepCopy(res.data.data);
              transport.lastTO = correctDate(this, transport.lastTO);
              console.log(transport);
              return transport;
            } else return elem;
          });
          commit("setTransports", result);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
};

export const mutations = {
  setTransports(state, transports) {
    state.transports = transports;
  }
};
