import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
import { correctDate } from "@/helpers/dateHelper";

export const state = () => ({
    dutyshifts: [],
});
function correctDutyshift(context, duty) {
    duty.date = correctDate(context, duty.date);
    return duty;
  }

export const actions = {
    fetchDutyshifts({ commit }, data = { sorting: {} }) {
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/GetFilteredTransportDutyShift", data)
                .then((res) => {
                    let result = deepCopy(res.data.data).map((el) =>
                    correctDutyshift(this, el)
                    );
                    commit("setDutyshifts", result);
                    resolve(result)
                })
                .catch((err) => reject(err));
        });
    },
    addDutyshift({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/CreateTransportDutyShift", data)
                .then((res) => {
                    let result = [...state.dutyshifts];
                    result.push(correctDutyshift(this,deepCopy(res.data.data)));
                    commit("setDutyshifts", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    deleteDutyshift({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            post(this, "/api/Transport/DeleteTransportDutyShift", data)
                .then((res) => {
                    let result = state.dutyshifts.filter(
                        (item) => item.transportDutyShiftId != data.transportDutyShiftIds[0]
                    );
                    commit("setDutyshifts", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
};

export const mutations = {
    setDutyshifts(state, dutyshifts) {
        state.dutyshifts = dutyshifts;
    },
};