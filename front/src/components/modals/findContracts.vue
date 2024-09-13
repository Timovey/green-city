<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Поиск договоров</vTitle>
        <div class="cross" @click="reset()"></div>
      </div>
      <div class="clientsEdit__container">
        <div class="row">
          <vTitle class="pageTitle">Номер договора</vTitle>
          <input type="text" name="" v-model="params.contractNumber">
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Запись проверена</vTitle>
          <input type="checkbox" id="valid" @click="params.isValidated = !params.isValidated" :checked="params.isValidated"/>
          <label for="valid">
            <div class="validCheck"></div>
          </label>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Статус подписания</vTitle>
          <!-- <input type="text" name="" v-model="params.status"> -->
          <input type="checkbox" id="object" @click="params.status = !params.status" :checked="params.status"/>
          <label for="object">
            <div class="objectCheck"></div>
          </label>
        </div>
        <div class="rowSelect">
          <vTitle class="pageTitle">Контрагент</vTitle>
          <div class="select">
            <div @click.stop="showClients = !showClients">
              <input 
                type="text" 
                name="" 
                disabled
                :placeholder="'Выбрано ' + params.contractClientIds.length"
              />
            </div>
            <div class="options" v-show="showClients" v-click-outside="closeClients">
              <div 
                class="option" 
                v-for="(client, index) in clients" 
                :key="index"
              >
                <input type="checkbox" :id="'checkClient_' + index" name="checkClientSelect" class="radioBtn">
                <label :for="'checkClient_' + index" @click.stop="chooseClient(client.contractClientId)">
                  <div class="typeRadio">
                    <div class="typeRadio__back"></div>
                  </div>
                  <vTitle class="pageTitle" @click.native.stop="chooseClient(client.contractClientId)">{{ client.clientName }}</vTitle>
                </label>
              </div>
            </div>
          </div>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Способ отправки документов</vTitle>
          <input type="text" name="" v-model="params.sendDocsType">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Параметры расчета</vTitle>
          <input type="text" name="" v-model="params.calcParams">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Объем в год от, м^3</vTitle>
          <input type="text" name="" v-model="params.volumePerYearM3From">
          <vTitle class="pageTitle">Объем в год до, м^3</vTitle>
          <input type="text" name="" v-model="params.volumePerYearM3To">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Паспорт отходов</vTitle>
          <input type="text" name="" v-model="params.wastePassport">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Расчетная единица</vTitle>
          <input type="text" name="" v-model="params.calcUnit">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Объем в месяц от, м^3</vTitle>
          <input type="text" name="" v-model="params.volumePerMonthM3From">
          <vTitle class="pageTitle">Объем в месяц до, м^3</vTitle>
          <input type="text" name="" v-model="params.volumePerMonthM3To">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Норматив</vTitle>
          <input type="text" name="" v-model="params.normativ">
        </div>
        <div class="rowSelect">
          <vTitle class="pageTitle">Прикрепленные площадки</vTitle>
          <div class="select">
            <div @click.stop="showContainerYards = !showContainerYards">
              <input 
                type="text" 
                name="" 
                disabled
                :placeholder="'Выбрано ' + params.attachedStands.length"
              />
            </div>
            <div class="options" v-show="showContainerYards" v-click-outside="closeContainerYards">
              <div 
                class="option" 
                v-for="(containerYard, index) in containerYards" 
                :key="index"
              >
                <input type="checkbox" :id="'checkContainerYard_' + index" name="checkContainerYardSelect" class="radioBtn">
                <label :for="'checkContainerYard_' + index" @click.stop="chooseContainerYard(containerYard.standId)">
                  <div class="typeRadio">
                    <div class="typeRadio__back"></div>
                  </div>
                  <vTitle class="pageTitle" @click.native.stop="chooseContainerYard(containerYard.standId)">{{ containerYard.address }}</vTitle>
                </label>
              </div>
            </div>
          </div>
        </div>
        <div class="rowSelect">
          <vTitle class="pageTitle">Прикрепленные контейнеры</vTitle>
          <div class="select">
            <div @click.stop="showContainers = !showContainers">
              <input 
                type="text" 
                name="" 
                disabled
                :placeholder="'Выбрано ' + params.attachedContainers.length"
              />
            </div>
            <div class="options optionsTop" v-show="showContainers" v-click-outside="closeContainers">
              <div 
                class="option" 
                v-for="(container, index) in containers" 
                :key="index"
              >
                <input type="checkbox" :id="'checkContainer_' + index" name="checkContainerSelect" class="radioBtn">
                <label :for="'checkContainer_' + index" @click.stop="chooseContainer(container.containerId)">
                  <div class="typeRadio">
                    <div class="typeRadio__back"></div>
                  </div>
                  <vTitle class="pageTitle" @click.native.stop="chooseContainer(container.containerId)">{{ container.name }}</vTitle>
                </label>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native.stop="reset">Сброс</vButton>
        </div>
        <div class="row">
          <vButton class="greenButton" @click.native.stop="filter">Поиск</vButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'

import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

import axios from 'axios';

export default {
  name: "findContracts",

  components: {
    vTitle,
    vButton,
  },

  data() {
    return {
      params: {
        contractNumber: '',
        contractClientIds: [],
        status: false,
        isValidated: false,
        sendDocsType: '',
        calcParams: '',
        volumePerMonthM3From: null,
        volumePerMonthM3To: null,
        volumePerYearM3From: null,
        volumePerYearM3To: null,
        calcUnit: '',
        wastePassport: '',
        normativ: '',
        attachedStands: [],
        attachedContainers: [],
      },
      clients: [],
      containerYards: [],
      containers: [],
      showContainerYards: false,
      showClients: false,
      showContainers: false,
    }
  },

  created() {
    this.fetchClients()
    this.fetchContainerYards()
    this.fetchContainers()
  },

  methods: {
    close(bool = false, params) {
      this.$emit('close', bool, params)
    },

    reset() {
      this.params.contractNumber = ''
      this.params.contractClientIds = []
      this.params.isValidated = false
      this.params.status = false
      this.params.sendDocsType = ''
      this.params.calcParams = ''
      this.params.volumePerMonthM3From = null
      this.params.volumePerMonthM3To = null
      this.params.volumePerYearM3From = null
      this.params.volumePerYearM3To = null
      this.params.calcUnit = ''
      this.params.wastePassport = ''
      this.params.normativ = ''
      this.params.attachedStands = []
      this.params.attachedContainers = []
      this.close(false, this.params)
    },

    filter() {
      this.close(true, this.params)
    },

    chooseClient(id) {
      let foundFlag = false
      for (let i = 0; i < this.params.contractClientIds.length; i++) {
        if (this.params.contractClientIds[i] == id) {
          this.params.contractClientIds.splice(i, 1)
          foundFlag = true
        }
      }
      if (!foundFlag) {
        this.params.contractClientIds.push(id)
      }
      console.log(this.params.contractClientIds)
    },

    closeClients() {
      this.showClients = false
    },

    chooseContainerYard(id) {
      let foundFlag = false
      for (let i = 0; i < this.params.attachedStands.length; i++) {
        if (this.params.attachedStands[i] == id) {
          this.params.attachedStands.splice(i, 1)
          foundFlag = true
        }
      }
      if (!foundFlag) {
        this.params.attachedStands.push(id)
      }
      console.log(this.params.attachedStands)
    },

    closeContainerYards() {
      this.showContainerYards = false
    },

    chooseContainer(id) {
      let foundFlag = false
      for (let i = 0; i < this.params.attachedContainers.length; i++) {
        if (this.params.attachedContainers[i] == id) {
          this.params.attachedContainers.splice(i, 1)
          foundFlag = true
        }
      }
      if (!foundFlag) {
        this.params.attachedContainers.push(id)
      }
      console.log(this.params.attachedContainers)
    },

    closeContainers() {
      this.showContainers = false
    },

    async fetchClients() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: 0,
        page: 0,
        isFilter: false,
        sorting: {}
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContractClient`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log('first', response.data.data);
        this.clients = response.data.data
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async fetchContainerYards() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: 0,
        page: 0,
        isFilter: false,
        sorting: {}
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredStand`, data, { headers: {
          'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data);
        this.containerYards = response.data.data
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async fetchContainers() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: 0,
        page: 0,
        isFilter: false,
        sorting: {}
      }
      console.log(data)
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContainer`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data);
        this.containers = response.data.data
      })
      .catch((error) => {
        console.log(error);
      })
    },
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

            .optionsTop {
              top: -200px !important;
            }

            .options {
              max-height: 200px;
              height: fit-content;
              overflow-y: scroll;
              position: absolute;
              top: 35px;
              left: 0;
              width: 100%;
              padding: 5px 10px;
              border: 1px solid #000;
              background: #FFF;

              .option {
                display: flex;
                justify-content: flex-start;
                align-items: center;

                label {
                  display: flex;
                  align-items: center;
                  justify-content: flex-start;
                }

                .radioBtn {
                  display: none;

                  &:checked + label .typeRadio__back {
                    background: rgba(37, 72, 15, 0.58);
                  }
                }

                .typeRadio {
                  cursor: pointer;
                  border: 1px solid #000;
                  width: 16px;
                  height: 16px;
                  border-radius: 15px;
                  display: flex;
                  justify-content: center;
                  align-items: center;

                  &__back {
                    width: 10px;
                    height: 10px;
                    border-radius: 15px;     
                  }
                }
              }
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

          #object {
            display: none;

            &:checked + label .objectCheck {
              background: #000;
            }
          }

          .objectCheck {
            margin-left: 20px;
            border: 1px solid #000;
            width: 30px;
            height: 30px;
            border-radius: 15px;
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
