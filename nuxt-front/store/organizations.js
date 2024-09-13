import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";

export const state = () => ({
  organizations: [],
});

export const actions = {
  fetchOrganizations({ commit }) {
    return new Promise((resolve, reject) => {
      post(this, "api/Auth/GetOrganizations", {isFilterByUserId: false})
        .then((res) => {
          let result = deepCopy(res.data.data)
          commit("setOrganizations", result);
          return resolve(result);
        })
        .catch((err) => {
          return reject(err);
        });
    });
  },
};

export const mutations = {
  setOrganizations(state, organizations) {
    state.organizations = organizations;
  },
};