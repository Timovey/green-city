export const state = () => ({
  enums: {},
});

export const actions = {
    fetchEnums({ commit }, data = {  }) {
        return new Promise((resolve, reject) => {
          this.$post("/api/Enum/GetSdEnums", data)
            .then((res) => {
              let result = this.$deepCopy(res.data.data.enumValues)
              // console.log(result);
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
    state.enums = enums;
  },
};
