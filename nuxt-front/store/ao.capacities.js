export const state = () => ({
    containers: [],
    garbageTypes: [
        {
            id: 1,
            name: "ТКО",
        },
        {
            id: 2,
            name: "КГО",
        },
        {
            id: 3,
            name: "4 класс",
        },
        {
            id: 4,
            name: "5 класс",
        },
        {
            id: 5,
            name: "Строительный",
        },
    ],
    containerTypes: []
});

export const actions = {
    fetchContainers({ commit }, data = {}) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/getFilteredContainer", data)
                .then((res) => {
                    let result = res.data.data;
                    commit("setContainers", result);
                    return resolve(result);
                })
                .catch((err) => {
                    return reject(err);
                });
        });
    },
    deleteContainer({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/DeleteContainer", data)
                .then((res) => {
                    let result = state.containers.filter(
                        (item) => item.containerId != data.containerIds[0]
                    );
                    commit("setContainers", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    addContainer({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/CreateContainer", data)
                .then((res) => {
                    console.log(res);
                    let result = [...state.containers];
                    result.push(data);
                    commit("setContainers", result);
                    return resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    updateContainer({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/UpdateContainer", data)
                .then((res) => {
                    console.log(res);
                    let result = JSON.parse(
                        JSON.stringify(state.containers)
                    ).map((elem) => {
                        if ((elem.containerId = data.containerId)) return data;
                        else return elem;
                    });
                    commit("setContainers", result);
                    return resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    getContainer(_, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/GetContainer", data)
                .then((res) => {
                    return resolve(res.data.data);
                })
                .catch((err) => reject(err));
        });
    },
    getContainerTypes({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/GetAllActualContainerTypes")
                .then((res) => {
                    commit("setContainerTypes", res.data.data);
                    return resolve(res.data.data);
                })
                .catch((err) => reject(err));
        });
    },
};

export const mutations = {
    setContainers(state, containers) {
        state.containers = containers;
    },
    setContainerTypes(state, contTypes) {
        state.containerTypes = contTypes
    }
};
