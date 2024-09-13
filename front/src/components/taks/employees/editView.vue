<template>
  <div class="employeesEdit">
    <div class="employeesEdit__container">
      <div class="employeesEdit__container__left">
        <div class="row">
          <vTitle class="pageTitle">Табельный номер</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="pageTitle">ФИО</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="pageTitle">Дата приема</vTitle>
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
        <div class="row">
          <vTitle class="pageTitle">Должность</vTitle>
          <input type="text" name="" />
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
        <div class="row">
          <vTitle class="pageTitle">Квалиф.разряд</vTitle>
          <input type="text" name="" />
        </div>
      </div>
      <div class="employeesEdit__container__right">
        <div class="row rowFired">
          <vTitle class="pageTitle">Уволен</vTitle>
          <input type="checkbox" id="fired" />
          <label for="fired">
            <div class="firedCheck"></div>
          </label>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Инициалы</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="pageTitle">Дата увольнения</vTitle>
          <date-picker 
              v-model="dateEnd" 
              :lang="lang" 
              :format="format"
              valueType="format"
              input-class="datePickerInput"
              class="datePickerLastTO"
            >
            </date-picker>
        </div>
      </div>
    </div>
    <div class="employeesEdit__container">
      <div class="row">
        <div class="radio__container" v-for="type in types" :key="type.id">
          
          <input type="radio" name="type" class="radioBtn" :id="'radio_' + type.id" />
          <label :for="'radio_' + type.id">
            <vTitle class="pageTitle">{{ type.name }}</vTitle>
            <div class="typeRadio"></div>
          </label>
        </div>
      </div>
    </div>
    <div class="employeesEdit__container">
      <div class="employeesEdit__container__left ">
        <div class="row">
          <vTitle class="pageTitle">Место рождения</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="pageTitle">Адрес</vTitle>
          <input type="text" name="" />
        </div>
      </div>
      <div class="employeesEdit__container__right"></div>
    </div>
    <div class="employeesEdit__container">
      <div class="row">
        <vTitle class="pageTitle">Паспортные данные:</vTitle>
      </div>
    </div>
    <div class="employeesEdit__container">
      <div class="employeesEdit__container__passport">
        <div class="row">
          <vTitle class="pageTitle">Серия</vTitle>
          <input type="text" name="" />
        </div>
      </div>
      <div class="employeesEdit__container__passport">
        <div class="row">
          <vTitle class="pageTitle">Номер</vTitle>
          <input type="text" name="" />
        </div>
      </div>
      <div class="employeesEdit__container__passport">
        <div class="row">
          <vTitle class="pageTitle">Дата выдачи</vTitle>
          <date-picker 
              v-model="issueDate" 
              :lang="lang" 
              :format="format"
              valueType="format"
              input-class="datePickerInput"
              class="datePickerLastTO"
            >
            </date-picker>
        </div>
      </div>
      <div class="employeesEdit__container__passport">
        <div class="row">
          <vTitle class="pageTitle">Кем выдан</vTitle>
          <input type="text" name="" />
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
      types: [
        {
          id: 1,
          name: 'Мусоровоз'
        },
        {
          id: 2,
          name: 'Бункеровоз'
        },
        {
          id: 3,
          name: 'Самосвал'
        },
      ],
      dateStart: '',
      dateEnd: '',
      issueDate: '',
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
.employeesEdit {

  &__container {
    display: flex;
    width: 100%;

    &__passport {
      width: 24%;
    }

    &__left {
      width: 33%;
      margin-right: 20px;
    }

    &__right {
      width: 33%;
    }

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
      }

      #fired {
        display: none;

        &:checked + label .firedCheck {
          background: #000;
        }
      }

      .firedCheck {
        margin-left: 20px;
        border: 1px solid #000;
        width: 30px;
        height: 30px;
        border-radius: 15px;
      }

      .radio__container {
        display: flex;

        label {
          display: flex;
          align-items: center;
        }

        .radioBtn {
          display: none;

          &:checked + label .typeRadio {
            background: #000;
          }
        }
      }

      .typeRadio {
        margin: 0 20px 0 5px;
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
        width: 100%;
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

    .rowFired {
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
    width: calc(100% - 20px) !important;
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
    width: 100%;
  }
}
</style>
