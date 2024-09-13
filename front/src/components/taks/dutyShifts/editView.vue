<template>
  <div class="edit">
    <div class="edit__container">
      <div class="edit__container__left">
        <div class="row">
          <vTitle class="pageTitle">Организация</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">ТС</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Событие</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Смена</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Дата начала</vTitle>
          <date-picker 
            v-model="dateStart" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerVehiclesRepair"
          >
          </date-picker>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Плановая дата окончания</vTitle>
          <date-picker 
            v-model="dateEndPlan" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerVehiclesRepair"
          >
          </date-picker>
        </div>
      </div>
      <div class="main__container__right">
        <div class="row">
          <vTitle class="pageTitle">Фактическая дата окончания</vTitle>
          <date-picker 
            v-model="dateEndFact" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerVehiclesRepair"
          >
          </date-picker>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import vTitle from "@/components/ui-kit/vTitle";

export default {
  name: "editView",

  components: {
    vTitle,
    DatePicker,
  },

  data() {
    return {
      dateStart: '',
      dateEndPlan: '',
      dateEndFact: '',
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'DD-MM-YYYY',
    }
  },
}
</script>

<style lang="scss">
.edit {

  &__container {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    width: 90%;

    &__left {
      width: 49%;
    }

    &__right {
      width: 49%;
    }

    .row {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin: 30px 0;

      input {
        border: 1px solid #000;
        width: 50%;
        height: 30px;
      }

      .datePickerInput {
        font-family: 'Roboto', sans-serif;
        font-weight: 700;
        font-size: 18px;
        line-height: 21px;
        color: #000000;
        display: inline-block;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        width: 100%;
        height: 34px;
        padding: 6px 30px;
        padding-left: 10px;
        background-color: #fff;
        border: 1px solid #000;
        border-radius: 4px;
        -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
        box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
        cursor: pointer;
      }

      .datePickerVehiclesRepair {
        width: 50%;
      }
    }
  }
}
</style>
