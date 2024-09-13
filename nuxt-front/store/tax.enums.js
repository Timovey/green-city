import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
export const state = () => ({
  enums: {},
});

export const actions = {
    fetchEnums({ commit }, data = {  }) {
        return new Promise((resolve, reject) => {
          post(this, "/api/Enum/GetTransportEnums", data)
            .then((res) => {
              let result = deepCopy(res.data.data.enumValues)
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
