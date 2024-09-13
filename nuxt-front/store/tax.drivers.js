import { correctDate } from "@/helpers/dateHelper";
import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
import { addFio} from '@/helpers/driverHelper';
export const state = () => ({
  drivers: [],
});
function correctDateDriver(context, driver) {
  driver.birthDate = correctDate(context, driver.birthDate);
  driver.issueDate = correctDate(context, driver.issueDate);
  driver.acceptWorkDate = correctDate(context, driver.acceptWorkDate);
  if (driver.firedDate)
    driver.firedDate = correctDate(context, driver.firedDate);

  return addFio(driver);
}

export const actions = {
  fetchDrivers({ commit }, data = { sorting: {} }) {
    return new Promise((resolve, reject) => {
      // console.log('data',data);
      post(this, "/api/Transport/getFilteredDriver", data)
        .then((res) => {
          let result = deepCopy(res.data.data).map((el) =>
            correctDateDriver(this, el)
          );
          commit("setDrivers", result);
          return resolve(result);
        })
        .catch((err) => {
          return reject(err);
        });
    });
  },
  deleteDriver({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/DeleteDriver", data)
        .then((res) => {
          let result = state.drivers.filter(
            (item) => !data.driverIds.includes(item.driverId)
          );
          commit("setDrivers", result);
          resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  addDriver({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/CreateDriver", data)
        .then((res) => {
          let result = [...state.drivers];
          result.push(correctDateDriver(this,deepCopy(res.data.data)));
          // console.log(result);
          commit("setDrivers", result);
          return resolve(result);
        })
        .catch((err) => {
          console.log(err);
          reject(err);
        });
    });
  },
  updateDriver({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/UpdateDriver", data)
        .then((res) => {
          let result = deepCopy(state.drivers).map((elem) => {
            if ((elem.driverId == data.driverId)) return correctDateDriver(this,deepCopy(res.data.data))
            else return elem;
          });
          console.log(result);
          commit("setDrivers", result);
          return resolve(result);
        })
        .catch((err) => reject(err));
    });
  },
  getDriver({ commit, state }, data) {
    return new Promise((resolve, reject) => {
      post(this, "/api/Transport/GetDriver", data)
        .then((res) => {
          let data = deepCopy(res.data.data[0]);
          correctDateDriver(this, data);
          return resolve(data);
        })
        .catch((err) => reject(err));
    });
  },
};

export const mutations = {
  setDrivers(state, drivers) {
    state.drivers = drivers;
  },
};
