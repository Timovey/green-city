export const state = () => ({
  missionEnums: {},
});

export const actions = {
  fetchEnums({ commit }, data = {}) {
    return new Promise((resolve, reject) => {
      this.$post("/api/Enum/GetMissionEnums", data)
        .then((res) => {
          let result = this.$deepCopy(res.data.data.enumValues);
          commit("setEnums", result);
          return resolve(result);
        })
        .catch((err) => {
          return reject(err);
        });
    });
  },
};

export const mutations = {
  setEnums(state, enums) {
    state.missionEnums = enums;
  },
};
