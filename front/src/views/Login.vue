<template>
  <div class="login">
    <div class="login__container">
      <div class="title__container">
        <vTitle class="loginTitle">Вход в программу</vTitle>
      </div>
      <div class="input__container">
        <input 
          type="text"
          v-model="login"
          placeholder="Логин" 
        />
      </div>
      <div class="input__container">
        <input 
          type="password"
          v-model="password"
          placeholder="Пароль" 
        />
      </div>
      <div class="button__container">
        <vButton class="greenButton" @click.native="auth">Войти</vButton>
      </div>
      <div class="button__container" v-show="incorrectAuth">
        <p>Неверные логин или пароль</p>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import vTitle from "@/components/ui-kit/vTitle.vue";
import vButton from "@/components/ui-kit/vButton.vue";

import axios from 'axios';

export default {
  name: "Login",

  components: {
    vTitle,
    vButton,
  },

  data() {
    return {
      login: '',
      password: '',
      incorrectAuth: false,
    }
  },

  methods: {
    async auth() {
      let data = {
        login: this.login,
        password: this.password
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/auth/login`, data)
      .then(( response ) => {
        console.log(response.data.data);
        //Потом добавить доп.проверку
        if (response.data.data.role == 'admin') {
          localStorage.setItem('Role', response.data.data.role)
          localStorage.setItem('Token', response.data.data.token)
          localStorage.setItem('UserId', response.data.data.userId)
          localStorage.setItem('UserName', response.data.data.userName)
          this.$router.push('/admin')
        }
      })
      .catch((error) => {
        console.log(error);
        this.incorrectAuth = true
        setTimeout(function(){ this.incorrectAuth = false }, 3000);
      })
    }
  }
};
</script>

<style lang="scss" scoped>
.login {
  height: 100vh;
  width: 100vw;
  background: linear-gradient(180deg, rgba(66, 130, 25, 0.5) 66.23%, rgba(85, 204, 10, 0.196347) 99.38%, rgba(98, 253, 0, 0) 100%, rgba(90, 225, 6, 0.113131) 100%);
  display: flex;
  justify-self: center;
  align-items: center;

  &__container {
    width: fit-content;
    margin: 0 auto;

    .title__container {
      margin-bottom: 20px;
    }

    .button__container {
      width: 50%;
      margin: 0 auto;
      height: 60px;

      @media screen and (max-width: 1440px) {
        height: 40px;
      }
    }

    .input__container {
      width: 70%;
      margin: 50px auto;
      height: 60px;

      @media screen and (max-width: 1440px) {
        height: 40px;
        margin: 25px auto;
      }

      input {
        width: 100%;
        height: 100%;
        background: #708357;
        border-radius: 11px;
        color: #DADADA;
        font-weight: bold;
        font-size: 36px;
        line-height: 43px;
        padding: 10px 20px;
        font-family: 'Lato', sans-serif;

        @media screen and (max-width: 1440px) {
          font-size: 16px;
          line-height: 23px;
        }

        &::placeholder {
          color: #DADADA;
        }
      }
    }
  }
}
</style>
