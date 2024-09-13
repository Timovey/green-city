import { correctDateRange } from "@/helpers/dateHelper";
import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";

export const state = () => ({
    repairs: [],
});

export const actions = {
    fetchRepairs({ commit }, data = { sorting: {} }) {
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/GetFilteredRepairCard", data)
                .then((res) => {
                    let result = deepCopy(res.data.data).map((el) =>
                    correctDateRange(this, el)
                  );
                    commit("setRepairs", result);
                    resolve(result)
                })
                .catch((err) => reject(err));
        });
    },
    addRepair({ commit, state }, data) {
        data.dateRange.createDate = new Date();
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/CreateRepairCard", data)
                .then((res) => {
                    let result = [...state.repairs, correctDateRange(this,deepCopy(res.data.data))];
                    commit("setRepairs", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    deleteRepair({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/DeleteRepairCard", data)
                .then((res) => {
                    let result = state.repairs.filter(
                        (item) => item.repairCardId != data.repairCardIds[0]
                    );
                    commit("setRepairs", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
};

export const mutations = {
    setRepairs(state, repairs) {
        state.repairs = repairs;
    },
};