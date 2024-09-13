<template>
  <div class="vehiclesEdit">
    <div class="vehiclesEdit__container">
      <div class="vehiclesEdit__container__left">
        <div class="row">
          <vTitle class="fieldTitle">Название</vTitle>
          <input type="text" v-model.trim="name" name="name" />
        </div>
        <div class="rowSelect">
          <vTitle class="fieldTitle">Организация</vTitle>
          <div class="select">
            <div @click="showOrganizations = !showOrganizations">
              <input 
                type="text" 
                name="organization"
                v-model.trim="organization"
              />
            </div>
            <div class="options" v-show="showOrganizations">
              <div 
                class="option" 
                v-for="organization in organizations" 
                :key="organization.id"
                @click="chooseOrganization(organization.id, organization.name)"
              >
                <vTitle class="fieldTitle">{{ organization.name }}</vTitle>
              </div>
              <div 
                class="option"
                @click="showModal = true"
              >
                <vTitle class="fieldTitle">+ Добавить организацию</vTitle>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="vehiclesEdit__container__right">
        <div class="row rowCheck">
          <label for="rent">
            <vTitle class="fieldTitle">Аренда</vTitle>
          </label>
          <input type="checkbox" id="rent" v-model.trim="rent"/>
          <label for="rent">
            <div class="rentCheck">
              <div class="rentCheck__back"></div>
            </div>
          </label>
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Рег.номер</vTitle>
          <input type="text" name="regNumber" v-model.trim="regNumber"/>
        </div>
      </div>
    </div>
    <div class="vehiclesEdit__container">
      <div class="row">
        <vTitle class="pageTitle">Основные параметры:</vTitle>
      </div>
    </div>
    <div class="vehiclesEdit__generalContainer grey">
      <div class="vehiclesEdit__container">
        <div class="vehiclesEdit__container__left ">
          <div class="row">
            <vTitle class="fieldTitle">СТС</vTitle>
            <input type="text" name="sts" v-model.trim="sts" />
          </div>
          <div class="row">
            <vTitle class="fieldTitle">Тип кузова</vTitle>
            <input type="text" name="bodyType" v-model.trim="bodyType" />
          </div>
          <div class="row">
            <vTitle class="fieldTitle">Забор бака</vTitle>
            <input type="text" name="tankFence" v-model.trim="tankFence" />
          </div>
        </div>
        <div class="vehiclesEdit__container__right">
          <div class="row rowCheck">
            <vTitle class="fieldTitle">Тип ТС</vTitle>
            <input type="text" name="transportType" v-model.trim="transportType" />
          </div>
          <div class="row">
            <vTitle class="fieldTitle">Цвет кузова</vTitle>
            <input type="text" name="bodyColor" v-model.trim="bodyColor" />
          </div>
        </div>
      </div>
      <div class="vehiclesEdit__container">
        <div class="row">
          <div class="radio__container" v-for="type in types" :key="type.id">
            
            <input 
              type="radio" 
              name="type" 
              class="radioBtn" 
              :id="'radio_' + type.id" 
              @click="tankType = type.id"
            />
            <label :for="'radio_' + type.id">
              <vTitle class="fieldTitle">{{ type.name }}</vTitle>
              <div class="typeRadio">
                <div class="typeRadio__back"></div>
              </div>
            </label>
          </div>
        </div>
      </div>
      <div class="vehiclesEdit__container">
        <div class="vehiclesEdit__container__left ">
          <div class="row">
            <vTitle class="fieldTitle">Грузоподъемность</vTitle>
            <input type="number" name="loadCapacity" placeholder="в кг" v-model.trim="loadCapacity" />
          </div>
          <div class="row">
            <vTitle class="fieldTitle">Объем груза</vTitle>
            <input type="number" name="cargoVolume" placeholder="в куб.м" v-model.trim="cargoVolume" />
          </div>
          <div class="row">
            <vTitle class="fieldTitle">Пробег</vTitle>
            <input type="number" name="mileage" v-model.trim="mileage" />
          </div>
        </div>
        <div class="vehiclesEdit__container__right">
          <div class="row rowCheck">
            <vTitle class="fieldTitle">Объем кузова с коэф.</vTitle>
            <input type="text" name="cargoVolumeCoef" v-model.trim="cargoVolumeCoef" />
          </div>
          <div class="row">
            <vTitle class="fieldTitle">Год выпуска</vTitle>
            <date-picker 
              v-model.trim="productionYear" 
              :lang="lang" 
              :format="formatYear"
              type="year"
              valueType="format"
              input-class="datePickerInput"
              class="datePickerLastTO"
            >
            </date-picker>
          </div>
          <div class="row rowCheck">
            <vTitle class="fieldTitle">Предполагаемый СЭ</vTitle>
            <input type="text" name="se" v-model.trim="se" />
          </div>
        </div>
      </div>
    </div>
    <div class="vehiclesEdit__container">
      <div class="row">
        <vTitle class="pageTitle">Техническое состояние:</vTitle>
      </div>
    </div>
    <div class="vehiclesEdit__generalContainer grey">
      <div class="vehiclesEdit__container">
        <div class="vehiclesEdit__container__left ">
          <div class="row">
            <vTitle class="fieldTitle">Лимит затрат</vTitle>
            <input type="text" name="spendingLimit" v-model.trim="spendingLimit" />
          </div>
          <div class="row">
            <vTitle class="fieldTitle">Номер расхода ГСМ</vTitle>
            <input type="text" name="gsmNumber" v-model.trim="gsmNumber" />
          </div>
        </div>
        <div class="vehiclesEdit__container__right">
          <div class="row rowCheck">
            <vTitle class="fieldTitle">Последнее ТО</vTitle>
            <date-picker 
              v-model.trim="lastTO" 
              :lang="lang" 
              :format="format"
              valueType="format"
              input-class="datePickerInput"
              class="datePickerLastTO"
            >
            </date-picker>
          </div>
          <div class="row">
            <vTitle class="fieldTitle">ТО и ремонты</vTitle>
            <input type="text" name="repairsTO" v-model.trim="repairsTO" />
          </div>
        </div>
      </div>
    </div>
    <addOrganization v-if="showModal" @close="closeModal"/>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import axios from 'axios';

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
          name: 'ЕК(0,24;0,36)'
        },
        {
          id: 2,
          name: 'ЕК(0,66;0,77;1,1)'
        },
        {
          id: 3,
          name: '?(модернизированный)'
        },
        {
          id: 4,
          name: 'ПМК'
        },
        {
          id: 5,
          name: 'Бункер'
        },
      ],
      organization: null,
      name: null,
      organizationId: null,
      rent: false,
      regNumber: null,
      sts: null,
      transportType: null,
      bodyType: null,
      bodyColor: null,
      tankFence: null,
      tankType: null,
      loadCapacity: null,
      cargoVolume: null,
      cargoVolumeCoef: null,
      mileage: null,
      se: null,
      spendingLimit: null,
      gsmNumber: null,
      lastTO: null,
      productionYear: null,
      repairsTO: null,
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'DD-MM-YYYY',
      formatYear: 'YYYY',
      showModal: false,
      showOrganizations: false,
    }
  },

  created() {
    this.$root.$refs.VehiclesEdit__component = this
  },

  methods: {

    closeModal() {
      this.showModal = false
    },

    chooseOrganization(id, name) {
      this.organization = name
      this.organizationId = id
      this.showOrganizations = false
    },

    async createTransport() {
      let data = {
        name: this.name,
        organizationId: this.organizationId,
        rent: this.rent,
        regNumber: this.regNumber,
        sts: this.sts,
        transportType: this.transportType,
        bodyType: this.bodyType,
        bodyColor: this.bodyColor,
        tankFence: this.tankFence,
        tankType: this.tankType,
        loadCapacity: this.loadCapacity,
        cargoVolume: this.cargoVolume,
        cargoVolumeCoef: this.cargoVolumeCoef,
        mileage: this.mileage,
        productionYear: parseInt(this.productionYear),
        se: this.se,
        spendingLimit: this.spendingLimit,
        gsmNumber: this.gsmNumber,
        lastTO: this.lastTO,
        repairsTO: this.repairsTO
      }
      return await axios.post('/api/Transports/CreateTransport', data, { headers: {
          'Authorization': 'testText'
        } 
      })
      .then(( response ) => {
        console.log(response);
      })
      .catch((error) => {
        console.log(error);
      })
    }
  }
}
</script>

<style lang="scss">
.vehiclesEdit {

  &__generalContainer {
    display: flex;
    flex-direction: column;
    width: 100%;
  }

  .grey {
    background: rgba(37, 72, 15, 0.19);
    padding: 0 20px;
    border-radius: 32px;
  }

  .flex-end {
    align-items: flex-end;
  }

  &__container {
    display: flex;
    width: 100%;

    &__left {
      width: calc(50% - 10px);
      margin-right: 20px;
    }

    &__right {
      width: calc(50% - 10px);
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

      #rent {
        display: none;

        &:checked + label .rentCheck__back {
          background: rgba(37, 72, 15, 0.58);
        }
      }

      .rentCheck {
        margin-left: 20px;
        border: 1px solid #000;
        width: 30px;
        height: 30px;
        border-radius: 15px;
        display: flex;
        align-items: center;
        justify-content: center;

        @media screen and (max-width: 1440px) {
          height: 20px;
          width: 20px;
        }

        &__back {
          width: 20px;
          height: 20px;
          background: #FFF;
          border-radius: 15px;

          @media screen and (max-width: 1440px) {
            height: 14px;
            width: 14px;
          }
        }
      }

      .radio__container {
        display: flex;

        label {
          display: flex;
          align-items: center;
        }

        .radioBtn {
          display: none;

          &:checked + label .typeRadio__back {
            background: rgba(37, 72, 15, 0.58);
          }
        }
      }

      .typeRadio {
        margin: 0 20px 0 5px;
        border: 1px solid #000;
        width: 30px;
        height: 30px;
        border-radius: 15px;
        display: flex;
        justify-content: center;
        align-items: center;

        @media screen and (max-width: 1440px) {
          height: 20px;
          width: 20px;
        }

        &__back {
          width: 20px;
          height: 20px;
          border-radius: 15px; 

          @media screen and (max-width: 1440px) {
            height: 14px;
            width: 14px;
          }       
        }
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
        cursor: pointer;

        input {
          border: 1px solid #000;
          width: 100%;
          height: 30px;
          padding: 10px 5px;
          font-family: 'Lato', sans-serif;
          font-size: 18px;
          line-height: 22px;
          font-weight: 400;
          color: #000;
          border-radius: 4px;
          cursor: pointer;

          @media screen and (max-width: 1440px) {
            height: 20px;
            font-size: 12px;
            line-height: 16px;
          }
        }

        .options {
          position: absolute;
          top: 35px;
          left: 0;
          width: 100%;
          padding: 5px 10px;
          border: 1px solid #000;
          background: #FFF;

          @media screen and (max-width: 1440px) {
            top: 25px;
          }
        }
      }
    }

    .rowCheck {
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
