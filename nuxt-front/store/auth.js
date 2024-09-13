import { getItem, setItem } from '@/helpers/persistanceStorage'
export const state = () => ({
    token: '',
    user:{
        userId: 0,
        userName: "",
        roles: "",
        token: ""
    }
})

export const actions = {
    login({ commit }, data) {
        return new Promise((resolve, reject) => {
            this.$axios.post('api/auth/loginV2', data).then(res => {    
                if(!res.data.success){
                    return reject(res.data.errors[0])
                }     
                let userData = res.data.data
                let errors = res.errors
                if (errors?.length) return reject(errors)
                const user = {
                    userId: userData.userId,
                    //login: data.login,
                    //password: data.password,
                    userName: userData.userName,
                    roles: userData.roles,
                    token: userData.token
                }
                commit("setUser", user)
                commit("setToken", userData.token)
                return resolve(user)
            }).catch((err) => {
                console.log(" Отрицательный Результат авторизации", err)
                return reject(err)
            })
        })
    },
    logout({ commit }, data) {
        commit("setToken", '')
        commit("setUser", '')
    },
}

export const mutations = {
    setUser(state, user) {
        setItem("user", user)
        state.user = user
    },
    setToken(state, token) {
        setItem("Token", token)
        state.token = token
    }
}
