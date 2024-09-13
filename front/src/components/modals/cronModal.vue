<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Выбор графика</vTitle>
        <div class="cross" @click="close()"></div>
      </div>
      <div class="clientsEdit__container">
        <cron-input v-model="cron" @change="change" @reset="reset"/>
        <div class="results">
          <div v-for="(item, index) in result" :key="index">
            {{item}}
          </div>
        </div>
      </div>
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native="createCronExpression">Сохранить</vButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

import CronInput from 'vue-cron-generator/src/components/cron-input'

import axios from 'axios';

export default {
  name: "cronModal",

  components: {
    vTitle,
    vButton,
    CronInput
  },

  props: {
    cronProp: {
      type: String,
      default: '0 0 12 * * ? *'
    },
  },

  data() {
    return {
      result: [],
      cron: '0 0 12 * * ? *'
    }
  },

  created() {
    if (this.cronCommon) {
      this.cron = this.cronCommon
    }
    else if (this.cronProp) {
      this.cron = this.cronProp
    }
    this.fetchDates()
  },

  methods: {

    close(cron = '') {
      if (this.cronCommon) {
        this.$emit('closeCommon', cron)
      }
      else {
        this.$emit('close', cron)
      }
    },

    createCronExpression() {
      this.close(this.cron)
    },

    change(cron) {
      this.cron = cron
      this.fetchDates()
    },

    reset() {
      this.cron = '0 0 12 * * ? *'
    },

    async fetchDates() {
      let data = {
          cron: this.cron,
          examplesCount: 10
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Common/GetFireDatesByCron`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data)
        this.result = response.data.data.dates
      })
      .catch((error) => {
        console.log(error);
      })
    },
  },
}
</script>

<style lang="scss" scoped>
.modal {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.2);
  display: flex;
  align-items: center;
  justify-content: center;

  &__container {
    width: 800px;
    height: fit-content;
    background: #FFF;
    border: 1px solid #000;
    padding: 20px 10px 60px 10px;

    &__save {
      width: 100%;
      display: flex;
      justify-content: flex-end;
      align-items: center;

      .row {
        width: 100px;
      }
    }

    .clientsEdit {

      &__container {
        display: flex;
        flex-direction: row;
        width: 100%;
        margin-top: 20px;
        justify-content: space-around;

        .results {
          display: flex;
          flex-direction: column;
        }
      }
    }

    &__close {
      display: flex;
      justify-content: space-between;
      align-items: center;

      .cross {
        width: 30px;
        height: 30px;
        position: relative;
        border-radius: 15px;
        border: 1px solid #000;
        cursor: pointer;

        &:after, &:before {
          content: '';
          width: 2px;
          height: 20px;
          position: absolute;
          top: 4px;
          left: 13px;
          background: #000;
        }

        &:after {
          transform: rotate(45deg);
        }

        &:before {
          transform: rotate(-45deg);
        }
      }
    }
  }
}
</style>
