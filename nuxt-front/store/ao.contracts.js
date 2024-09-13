export const state = () => ({
    contracts: [],
    reactionTypes: [
        {
            id: 1,
            name: "Ближайший рабочий день вперёд",
        },
        {
            id: 2,
            name: "Ближайший рабочий день назад",
        },
        {
            id: 3,
            name: "Ближайший рабочий",
        },
        {
            id: 4,
            name: "Работаем в праздник/выходной",
        },
        {
            id: 5,
            name: "Не работаем",
        },
    ],
});

function correctDatesContract(context, contract) {
    if(contract.endDate) {
        contract.endDate = context.$correctDate(contract.endDate)
    }
    if(contract.startDate) {
        contract.startDate = context.$correctDate(contract.startDate)
    }
    if(contract.attachedContainers) {
        contract.attachedContainers = contract.attachedContainers.map(el => {
            if(el.inRegistryFromDate) {
                el.inRegistryFromDate = context.$correctDate(el.inRegistryFromDate)
            }
            el.contractScheduleCollection = el.contractScheduleCollection.map(shed => {
                if(shed.contractSchedule.excludeDates) {
                    shed.contractSchedule.excludeDates = shed.contractSchedule.excludeDates.map(ex => {
                        ex = context.$correctDate(ex)
                        return ex
                    })
                }
                if(shed.contractSchedule.scheduleDates) {
                    shed.contractSchedule.scheduleDates = shed.contractSchedule.scheduleDates.map(date => {
                        date = context.$correctDate(date)
                        return date
                    })
                }
                return shed
            })
            return el
        })
    }
    return contract
}
export const actions = {
    fetchContracts({ commit }, data = {}) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/GetFilteredContract", data)
                .then((res) => {
                    let result = res.data.data;
                    commit("setContracts", result);
                    return resolve(result);
                })
                .catch((err) => {
                    return reject(err);
                });
        });
    },
    deleteContract({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/DeleteContract", data)
                .then((res) => {
                    let result = state.contracts.filter(
                        (item) => item.contractId != data.contractIds[0]
                    );
                    commit("setContracts", result);
                    resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    addContract({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            data.attachedContainers = data.attachedContainers.map(el => {
                if(el.inRegistryFromDate === "") {
                    el.inRegistryFromDate = null
                } 
                if(el.workTimeFrom === "") {
                    el.workTimeFrom = null
                } 
                if(el.workTimeTo === "") {
                    el.workTimeTo = null
                } 
                return el
             })
             this.$post("/api/Sd/CreateContract", data)
                .then((res) => {
                    let result = [...state.contracts];
                    data.contractId = res.data.data
                    //console.log('add Contract',data);
                    result.push(data);
                    commit("setContracts", result);
                    return resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    updateContract({ commit, state }, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/UpdateContract", data)
                .then((res) => {
                    console.log(res);
                    let result = this.$deepCopy(state.contracts).map((elem) => {
                        if ((elem.contractClientId == data.contractClientId)) return data;
                        else return elem;
                    });
                    commit("setContracts", result);
                    return resolve(result);
                })
                .catch((err) => reject(err));
        });
    },
    getContract(_, data) {
        return new Promise((resolve, reject) => {
            this.$post("/api/Sd/GetContract", data)
                .then((res) => {
                    let source = this.$deepCopy(res.data.data)
                    let attachedContainers = []
                    for (let stand of source.attachedStands){
                        for (let container of stand.attachedContainers){
                            let containerToAdd = {
                                containerStandAddress: stand.address,
                                containerId: container.containerId,
                                
                                ...container.container2Schedule,
                                ...container
                            }
                            attachedContainers.push(containerToAdd)
                        }
                    }
                    
                    source.contractClientId = source.contractClient.contractClientId
                    source.attachedContainers = attachedContainers
                    source = correctDatesContract(this, source)
                    return resolve(source);
                })
                .catch((err) => reject(err))
        });
    }
};

export const mutations = {
    setContracts(state, contracts) {
        state.contracts = contracts;
    }
};
