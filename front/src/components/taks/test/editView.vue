<template>
  <div class="edit">
    <div class="edit__container flex-start">
      <div class="edit__container__left">
        <div class="row">
          <vTitle class="pageTitle">Водитель</vTitle>
          <input type="text" name="">
        </div>
        <div class="rowSelect">
          <vTitle class="pageTitle">Организация</vTitle>
          <div class="select">
            <div @click="showOrganizations = !showOrganizations">
              <input 
                type="text" 
                name="" 
                disabled
              />
            </div>
            <div class="options" v-show="showOrganizations">
              <div 
                class="option" 
                v-for="organization in organizations" 
                :key="organization.id"
              >
                <vTitle class="pageTitle">{{ organization.name }}</vTitle>
              </div>
              <div 
                class="option"
                @click="showModal = true"
              >
                <vTitle class="pageTitle">+ Добавить организацию</vTitle>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="edit__container__right">
        <div class="row">
          <vTitle class="pageTitle">Сменный водитель</vTitle>
          <input type="text" name="">
        </div>
      </div>
    </div>
    <div class="edit__container">
      <div class="edit__container__left">
        <div class="row">
          <vTitle class="pageTitle">Событие</vTitle>
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
      <div class="edit__container__right">
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
    <addOrganization v-if="showModal" @close="closeModal"/>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import vTitle from "@/components/ui-kit/vTitle";
import addOrganization from "@/components/modals/addOrganization";

export default {
  name: "editView",

  components: {
    vTitle,
    DatePicker,
    addOrganization,
  },

  data() {
    return {
      organizations: [
        {
          id: 1,
          name: 'Зеленый город'
        },
        {
          id: 2,
          name: 'Красная стрела'
        }
      ],
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
      showModal: false,
      showOrganizations: false,
    }
  },

  methods: {
    closeModal() {
      this.showModal = false
    },
  }
}
</script>

<style lang="scss">
.edit {

  .flex-start {
    align-items: flex-start;
  }

  &__container {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
    width: 90%;

    .rowSelect {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin: 30px 0;

      .select {
        width: 50%;
        margin-left: 20px;
        position: relative;

        input {
          border: 1px solid #000;
          width: 100%;
          height: 30px;
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
