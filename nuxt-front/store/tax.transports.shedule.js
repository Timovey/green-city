import { correctDateRange } from "@/helpers/dateHelper";
import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";

export const state = () => ({
  shedules: [],
});

export const actions = {
  fetchShedules({ commit }, data = { sorting: {} }) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/GetFilteredTransportShedule", data)
        .then((res) => {
          let result = deepCopy(res.data.data).map((el) =>
            correctDateRange(this, el)
          );
          commit("setShedules", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  addShedule({ commit, state }, data) {
    data.dateRange.createDate = new Date();
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/CreateTransportShedule", data)
        .then((res) => {
          let result = [
            ...state.shedules,
            correctDateRange(this, deepCopy(res.data.data)),
          ];
          commit("setShedules", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  deleteShedule({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/DeleteTransportShedule", data)
        .then((res) => {
          let result = state.shedules.filter(
            (item) => item.transportSheduleId != data.transportSheduleIds[0]
          );
          console.log('eds',res);
          commit("setShedules", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
};

export const mutations = {
  setShedules(state, shedules) {
    state.shedules = shedules;
  },
};
