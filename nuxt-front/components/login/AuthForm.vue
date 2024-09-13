<template>
    <div class="form_container">
        <v-card width="500">
            <v-card-title>Авторизация</v-card-title>
            <v-card-text>
                <form @keyup.enter="submit">
                    <v-text-field
                        v-model="login"
                        :error-messages="nameErrors"
                        label="Имя пользователя"
                        required
                        @input="$v.login.$touch()"
                        @blur="$v.login.$touch()"
                    ></v-text-field>
                    <v-text-field
                        type="password"
                        v-model="password"
                        :error-messages="passErrors"
                        label="Пароль"
                        required
                        @input="$v.password.$touch()"
                        @blur="$v.password.$touch()"
                    ></v-text-field>
                    <v-btn
                        class="mr-4 primary"
                        @click="submit"
                    >
                        Войти
                    </v-btn>
                    <v-btn @click="clear">
                        Очистить
                    </v-btn>
                    <v-alert
                      border="bottom"
                      color="pink darken-1"
                      dark
                      v-if="errors"
                      class="mt-4"
                    >
                      {{errors}}
                    </v-alert>
                </form>
            </v-card-text>           
        </v-card>
    </div>
</template>


<script>
  import { validationMixin } from 'vuelidate'
  import { required, maxLength, minLength, email } from 'vuelidate/lib/validators'
  export default {
    mixins: [validationMixin],
    validations: {
      login: { required},
      password: { required, minLength: minLength(8)}
    },
    data: () => ({
      login: '',
      password: '',
    }),
    props:{
      errors:{
        type: String | Error,
        default: null
      }
    },
    computed: {
      nameErrors () {
        const errors = []
        if (!this.$v.login.$dirty) return errors
        !this.$v.login.required && errors.push('Введите имя пользователя')
        return errors
      },
      passErrors () {
        const errors = []
        if (!this.$v.password.$dirty) return errors
        !this.$v.password.minLength && errors.push('Неверная длина')
        !this.$v.password.required && errors.push('Введите пароль')
        return errors
      },
    },
    methods: {
      submit () {
        this.$v.$touch()
        if(this.$v.$anyError) return
        let user = {
          login: this.login,
          password: this.password
        }
        this.$emit('submit', user)
      },
      clear () {
        this.$v.$reset()
        this.login = ''
        this.password = ''
      },
    },
  }
</script>


<style>
    .form_container{
        display: flex;
        justify-content: center;
        align-items: center;
        height: 80vh;
    }
</style>