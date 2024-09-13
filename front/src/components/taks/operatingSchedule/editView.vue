<template>
  <div class="operatingScheduleEdit">
    <div class="operatingScheduleEdit__container">
      <div class="operatingScheduleEdit__container__left">
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
        <div class="row">
          <vTitle class="pageTitle">ТС</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="pageTitle">Марка ТС</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="pageTitle">Типовой график</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="pageTitle">Дата начала цикла учета</vTitle>
          <date-picker 
              v-model="dateStart" 
              :lang="lang" 
              :format="format"
              valueType="format"
              input-class="datePickerInput"
              class="datePickerLastTO"
            >
            </date-picker>
        </div>
      </div>
      <div class="operatingScheduleEdit__container__right">
        <div class="row rowNotDriver">
          <vTitle class="pageTitle">Без водителя</vTitle>
          <input type="checkbox" id="notDriver" />
          <label for="notDriver">
            <div class="notDriverCheck"></div>
          </label>
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
.operatingScheduleEdit {

  &__container {
    display: flex;
    width: 100%;

    &__left {
      width: 40%;
      margin-right: 20px;
    }

    &__right {
      width: 40%;
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

      #notDriver {
        display: none;

        &:checked + label .firedCheck {
          background: #000;
        }
      }

      .notDriverCheck {
        margin-left: 20px;
        border: 1px solid #000;
        width: 30px;
        height: 30px;
        border-radius: 15px;
      }
    }

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

    .rowNotDriver {
      justify-content: flex-start;
    }
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
    width: 100% !important;
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

  .datePickerLastTO {
    width: 50%;
  }
}
</style>
