import { correctDateRange } from "@/helpers/dateHelper";
import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
import { addFio, addReplacementFio} from '@/helpers/driverHelper';
export const state = () => ({
  fixes: [],
});

function fillDataDriver(context, driverVacation) {
  correctDateRange(context, driverVacation)
  //console.log('driverVacation',driverVacation);
  return addFio(driverVacation)
}

export const actions = {
  fetchFixing({ commit }, data = { sorting: {} }) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/GetFilteredDrivers2Transport", data)
        .then((res) => {
          let result = deepCopy(res.data.data).map((el) =>
          fillDataDriver(this, el)
          );
          commit("setFixes", result);
          resolve(result);
        })
        .catch((err) => {
          reject(err);
        });
    });
  },
  addFix({ commit, state }, data) {
    data.dateRange.createDate = new Date();
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/CreateDriver2Transport", data)
        .then((res) => {
          let result = [...state.fixes, fillDataDriver(this,deepCopy(res.data.data))];
          commit("setFixes", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  deleteFix({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/DeleteDriver2Transport", data)
        .then((res) => {
          // console.log(data)
          let result = state.fixes.filter(
            (item) => item.driver2TransportId != data.driver2TransportIds[0]
          );
          commit("setFixes", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
};

export const mutations = {
  setFixes(state, fixes) {
    state.fixes = fixes;
  },
};
