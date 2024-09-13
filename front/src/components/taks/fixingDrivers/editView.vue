<template>
  <div class="edit">
    <div class="edit__container">
      <div class="edit__container__left">
        <div class="row">
          <vTitle class="pageTitle">Транспортное средство</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Марка</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Организация</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Тип</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Количество персонала</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Закрепленный персонал:</vTitle>
        </div>
      </div>
      <div class="main__container__right"></div>
    </div>
    <div class="edit__person">
      <div 
        class="edit__person__container"
        v-for="item in personCount"
        :key="item"
      >
        <div class="row">
          <vTitle class="pageTitle">Дата начала учета</vTitle>
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
          <vTitle class="pageTitle">ФИО сотрудника</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Тип</vTitle>
          <input type="text" name="">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Организация</vTitle>
          <input type="text" name="">
        </div>
        <div class="row__check">
          <vTitle class="pageTitle">Доп.сотрудник:</vTitle>
          <div class="row__check__container">
            <input type="checkbox" :id="'check_' + item" :disabled="personCount > item + 1">
            <label :for="'check_' + item" @click="changePersonCount(item)">
              <div class="row__check__container__label"></div>
            </label>
          </div>
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
      personCount: 1,
    }
  },

  methods: {
    closeEditView() {
      this.$root.$refs.VehiclesRepair__component.currentView = 'main'
    },

    changePersonCount(count) {
      if (this.personCount === count) {
        this.personCount++
      }
      else if (this.personCount === count + 1) {
        this.personCount--
      }
    }
  }
}
</script>

<style lang="scss">
.edit {

  &__person {
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
    width: 90%;
    flex-wrap: wrap;

    &__container {
      width: 49%;
    }
  }

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

  .row__check {
    display: flex;
    justify-content: flex-start;
    align-items: center;
    margin: 30px 0;

    &__container {
      margin-left: 20px;
      width: 30px;
      height: 30px;

      &__label {
        width: 30px;
        height: 30px;
        border: 1px solid #000;
        border-radius: 15px;
      }

      input {
        display: none;
        width: 100%;
        height: 100%;

        &:checked + label .row__check__container__label {
          background: #000;
        }
      }
    }
  }

  &__saveBtn {
    display: flex;
    justify-content: flex-end;
    align-items: center;

    &__container {
      width: 90px;
      height: 30px;
    }
  }
}
</style>
