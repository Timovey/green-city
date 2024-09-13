import { correctDateRange } from "@/helpers/dateHelper";
import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
import { addFio, addReplacementFio} from '@/helpers/driverHelper';
export const state = () => ({
  driverVacations: [],
});

function fillDataDriver(context, driverVacation) {
  correctDateRange(context, driverVacation)
  //console.log('driverVacation',driverVacation);
  let dv = addFio(driverVacation)
  dv = addReplacementFio(driverVacation)
  return dv
}
export const actions = {
  fetchDriverVacations({ commit }, data = { sorting: {} }) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/GetFilteredDriverVacation", data)
        .then((res) => {
          // console.log('res1',res);
          let result = deepCopy(res.data.data).map((el) =>
            fillDataDriver(this, el)
          );
          commit("setDriverVacations", result);
          resolve(result);
        })
        .catch((err) => {
          reject(err);
        });
    });
  },
  deleteDriverVacation({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/DeleteDriverVacation", data)
        .then((res) => {
          // console.log('res',res);
          let result = state.driverVacations.filter(
            (item) => !data.driverVacationIds.includes(item.driverVacationId)
          );
          commit("setDriverVacations", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  addDriverVacation({ commit, state }, data) {
    data.dateRange.createDate = new Date();
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/CreateDriverVacation", data)
        .then((res) => {
          let result = [...state.driverVacations, fillDataDriver(this,deepCopy(res.data.data))];
          commit("setDriverVacations", result);
          return resolve(result);
        })
        .catch((err) => {
          reject(err);
        });
    });
  },
  updateDriverVacation({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      data.dateRange.createdDate = Date.now();
      post(this, "/api/Transport/UpdateDriverVacation", data)
        .then((res) => {
          let result = deepCopy(state.driverVacations).map((elem) => {
            if (elem.driverVacationId == data.driverVacationId)
              return fillDataDriver(res.data.data);
            else return elem;
          });
          commit("setDriverVacations", result);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  getDiverVacation({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/GetDriverVacation", data)
        .then((res) => {
          let data = deepCopy(res.data.data[0]);
          fillDataDriver(this, data);
          return resolve(data);
        })
        .catch((err) => reject(err));
    });
  },
};

export const mutations = {
  setDriverVacations(state, driverVacations) {
    state.driverVacations = driverVacations;
  },
};
