<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Массовое обновление договоров</vTitle>
        <div class="cross" @click="close"></div>
      </div>
      <div class="clientsEdit__container">
        <div class="row">
          <vTitle class="pageTitle">Статус</vTitle>
          <input type="text" name="" v-model="params.status"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Дата начала действия</vTitle>
          <date-picker 
            v-model="params.startDate" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerCreate"
          >
          </date-picker>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Срок договора</vTitle>
          <date-picker 
            v-model="params.endDate" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerCreate"
          >
          </date-picker>
        </div>
      </div>
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native.stop="update">Обновить</vButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

import axios from 'axios';

export default {
  name: "bulkUpdateContracts",

  components: {
    vTitle,
    vButton,
    DatePicker,
  },

  props: {
    choosenContracts: {
      type: Array,
      default() {
        return []
      }
    },
  },

  data() {
    return {
      params: {
        status: '',
        startDate: null,
        endDate: null
      },
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'YYYY-MM-DD',
    }
  },

  methods: {
    close(flag = false) {
      this.$emit('close', flag)
    },

    async update() {
      let data = {
        contractIds: this.choosenContracts,
        status: this.params.status,
        startDate: this.params.startDate,
        endDate: this.params.endDate
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/BulkUpdateContract`, data, { headers: {
           'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response);
        this.close(true)
      })
      .catch((error) => {
        console.log(error);
      })
    }
  },

  directives: {
    ClickOutside
  }
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
    padding: 20px 10px 40px 10px;

    &__save {
      width: 100%;
      display: flex;
      justify-content: space-around;
      align-items: center;
      margin-top: 20px;

      .row {
        width: 100px;
      }
    }

    .clientsEdit {

      &__container {
        display: flex;
        flex-direction: column;
        width: 100%;
        margin-top: 20px;

        .rowSelect {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin: 30px 0;

          .select {
            width: 70%;
            margin-left: 20px;
            position: relative;

            input {
              border: 1px solid #000;
              width: 100%;
              height: 30px;
              border-radius: 10px;
            }

            .options {
              position: absolute;
              top: 35px;
              left: 0;
              width: 100%;
              padding: 5px 10px;
              border: 1px solid #000;
              background: #FFF;
            }
          }
        }

        .longRow {
          width: 100%;
        }

        .row {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin: 5px 0;

          input {
            border: 1px solid #000;
            width: 70%;
            height: 30px;
            margin-left: 20px;
          }
        }

        .flex-start {
          justify-content: flex-start !important;
        }

        .greenRow {
          background: rgba(37, 72, 15, 0.58);
          height: 40px;
          border-radius: 0 10px 10px 0 ;
          padding: 5px 20px;
          margin-left: -10px !important;
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

    &__content {

      .row {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin: 30px 0;

        input {
          border: 1px solid #000;
          width: 100%;
          height: 30px;
          margin-left: 20px;
          padding: 10px 5px;
          font-family: 'Lato', sans-serif;
          font-size: 18px;
          line-height: 22px;
          font-weight: 400;
          color: #000;
          border-radius: 4px;

          @media screen and (max-width: 1440px) {
            height: 20px;
            font-size: 12px;
            line-height: 16px;
          }
        }
      }

      &__saveBtn {  
        display: flex;
        justify-content: flex-end;
        align-items: center;

        .saveBtn {
          width: 90px;
          height: 30px;
        }
      }
    }
  }
}
</style>
