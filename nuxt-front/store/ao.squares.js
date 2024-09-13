export const state = () => ({
    squares: [],
    squareTypes: [
        {
            id: 1,
            name: "Общий",
        },
        {
            id: 2,
            name: "Жилой фонд",
        },
    ],
});

export const actions = {
    fetchSquares({ commit },  data = { sorting: {} }) {
        return new Promise((resolve, reject) => {
            data.isFilterByCreatedUser = false
            this.$post("/api/Sd/getFilteredStand", data)
                .then((res) => {
                    let result = res.data.data;
                    commit("setSquares", result);
                    return resolve(result);
                })
                .catch((err) => {
                    return reject(err);
                });
        });
    },
    updateSquare({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/SD/UpdateStand", data)
                .then((res) => {
                    let result = this.$deepCopy(state.squares).map(elem=>{
                        if (elem.standId = data.standId)
                            return data
                        else return elem
                    })
                    console.log(result)
                    commit("setSquares", result)
                    return resolve(result)
                })
                .catch((err) => reject(err));
        });
    },
    deleteSquare({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/DeleteStand", data)
                .then((res) => {
                    let result = state.squares.filter(
                        (item) => item.standId != data.standIds[0]
                    );
                    commit("setSquares", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    addSquare({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/CreateStand", data)
                .then((res) => {
                    let result = [...state.squares];
                    // console.log(res);
                    data.standId = res.data.data
                    result.push(this.$deepCopy(data));
                    commit("setSquares", result);
                    return resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    getSquare(_, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/GetStand", data)
                .then((res) => {
                    return resolve(res.data.data[0]);
                })
                .catch((err) => reject(err));
        });
    },
    getContainerTypes() {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/GetAllActualContainerTypes")
                .then((res) => {
                    return resolve(res.data.data);
                })
                .catch((err) => reject(err));
        });
    },
};

export const mutations = {
    setSquares(state, squares) {
        state.squares = squares;
    },
};
