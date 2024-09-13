import { correctDateRange } from "@/helpers/dateHelper";
import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";

export const state = () => ({
    rents: [],
});

export const actions = {
    fetchRents({ commit }, data = { sorting: {} }) {
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/GetFilteredRent", data)
                .then((res) => {
                    let result = deepCopy(res.data.data).map((el) =>
                    correctDateRange(this, el)
                  );
                    commit("setRents", result);
                    resolve(result)
                })
                .catch((err) => reject(err));
        });
    },
    addRent({ commit, state }, data) {
        data.dateRange.createDate = new Date();
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/CreateRent", data)
                .then((res) => {
                    let result = [...state.rents, correctDateRange(this,deepCopy(res.data.data))];
                    commit("setRents", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    deleteRent({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/DeleteRent", data)
                .then((res) => {
                    let result = state.rents.filter(
                        (item) => item.rentId != data.rentIds[0]
                    );
                    commit("setRents", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
};

export const mutations = {
    setRents(state, rents) {
        state.rents = rents;
    },
};