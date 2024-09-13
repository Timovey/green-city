<template>
  <div class="modal">
    <div class="modal__container" v-click-outside="close">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Добавление контейнера</vTitle>
        <div class="cross" @click="close()"></div>
      </div>
      <div class="clientsEdit__container">
        <div class="row">
          <vTitle class="pageTitle">Наименование</vTitle>
          <input type="text" name="" v-model="name"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Объем</vTitle>
          <input type="number" name="" v-model="volume"/>
        </div>
        <div class="row greenRow">
          <vTitle class="pageTitleWhite">Тара</vTitle>
        </div>
        <div class="row">
          <div class="radio__container" v-for="type in types.slice(0, 5)" :key="type.id">
            <input 
              type="radio" 
              :name="'type_' + type.id" 
              class="radioBtn" 
              :checked="type.checked"
              :id="'radio_' + type.id" 
              @click="setTankType(type.id)"
            />
            <label :for="'radio_' + type.id">
              <vTitle class="fieldTitle">{{ type.name }}</vTitle>
              <div class="typeRadio">
                <div class="typeRadio__back"></div>
              </div>
            </label>
          </div>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Пластик</vTitle>
        </div>
        <div class="row flex-start">
          <div class="radio__container" v-for="type in types.slice(5, 9)" :key="type.id">
            <input 
              type="radio" 
              :name="'type_' + type.id"
              class="radioBtn" 
              :id="'radio_' + type.id" 
              :checked="type.checked"
              @click="setTankType(type.id)"
            />
            <label :for="'radio_' + type.id">
              <vTitle class="fieldTitle">{{ type.name }}</vTitle>
              <div class="typeRadio">
                <div class="typeRadio__back"></div>
              </div>
            </label>
          </div>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Оцинковка</vTitle>
        </div>
        <div class="row flex-start">
          <div class="radio__container" v-for="type in types.slice(9, 11)" :key="type.id">
            <input 
              type="radio" 
              :name="'type_' + type.id"
              class="radioBtn" 
              :id="'radio_' + type.id" 
              :checked="type.checked"
              @click="setTankType(type.id)"
            />
            <label :for="'radio_' + type.id">
              <vTitle class="fieldTitle">{{ type.name }}</vTitle>
              <div class="typeRadio">
                <div class="typeRadio__back"></div>
              </div>
            </label>
          </div>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Нестандартный</vTitle>
        </div>
        <div class="row flex-start">
          <div class="radio__container" v-for="type in types.slice(11, 12)" :key="type.id">
            <input 
              type="radio" 
              :name="'type_' + type.id"
              class="radioBtn" 
              :id="'radio_' + type.id" 
              :checked="type.checked"
              @click="setTankType(type.id)"
            />
            <label :for="'radio_' + type.id">
              <vTitle class="fieldTitle">{{ type.name }}</vTitle>
              <div class="typeRadio">
                <div class="typeRadio__back"></div>
              </div>
            </label>
          </div>
        </div>
        <!-- <div class="rowSelect">
          <vTitle class="pageTitle">Контейнерная площадка</vTitle>
          <div class="select">
            <div @click.stop="showContainerYards = !showContainerYards">
              <input 
                type="text" 
                name="" 
                disabled
                v-model="choosenContainerYard"
              />
            </div>
            <div class="options" v-show="showContainerYards" v-click-outside="closeContainerYards">
              <div 
                class="option" 
                v-for="containerYard in containerYards" 
                :key="containerYard.id"
              >
                <vTitle class="pageTitle" @click.native.stop="chooseContainerYard(containerYard.id, containerYard.name)">{{ containerYard.name }}</vTitle>
              </div>
            </div>
          </div>
        </div> -->
      </div>
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native="createContainer">Сохранить</vButton>
        </div>
      </div>
    </div>
    <createdStatusModal v-if="showCreatedStatus" :isError="errorStatus"/>
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'

import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";
import createdStatusModal from "@/components/modals/createdStatusModal";

import axios from 'axios';

export default {
  name: "addContainer",

  components: {
    vTitle,
    vButton,
    createdStatusModal,
  },

  data() {
    return {
      showCreatedStatus: false,
      errorStatus: false,
      types: [
        {
          id: 1,
          name: 'PMK075',
          checked: false
        },
        {
          id: 2,
          name: 'ЕК036',
          checked: false
        },
        {
          id: 3,
          name: 'ЕК024',
          checked: false
        },
        {
          id: 4,
          name: '_4M3',
          checked: false
        },
        {
          id: 5,
          name: '_8M3',
          checked: false
        },
        {
          id: 6,
          name: 'Plastic_ЕК077',
          checked: false
        },
        {
          id: 7,
          name: 'Plastic_ЕК11',
          checked: false
        },
        {
          id: 8,
          name: 'Plastic_ЕК12',
          checked: false
        },
        {
          id: 9,
          name: 'Plastic_ЕК066',
          checked: false
        },
        {
          id: 10,
          name: 'Zink_ЕК077',
          checked: false
        },
        {
          id: 11,
          name: 'Zink_ЕК11',
          checked: false
        },
        {
          id: 12,
          name: 'NonStandart',
          checked: false
        }
      ],
      name: '',
      volume: null,
      // containerYards: [],
      // showContainerYards: false,
      // choosenContainerYard: '',
      // choosenContainerYardId: null,
      tankType: '',
      itemsOnPage: 0,
      page: 0,
      isFilter: false,
      isHighSecurityObject: null,
      address: '',
      trashtypes: [],
      driveWay: '',
      contacts: '',
      standTypes: [],
      containerIds: [],
      sorting: {},
    }
  },

  // created() {
    // this.fetchContainerYards()
  // },

  methods: {
    close(id = null) {
      this.$emit('close', id)
    },

    // chooseContainerYard(id, name) {
    //   this.choosenContainerYard = name
    //   this.choosenContainerYardId = id
    //   this.closeContainerYards()
    // },

    setTankType(param) {
      for (let i = 0; i < this.types.length; i++) {
        if (this.types[i].id == param) {
          this.types[i].checked = true
          this.tankType = param
        }
        else {
          this.types[i].checked = false
        }
      }
    },

    // closeContainerYards() {
    //   this.showContainerYards = false
    // },

    // async fetchContainerYards() {
    //   let data = {
    //     userId: localStorage.getItem('UserId'),
    //     userName: localStorage.getItem('UserName'),
    //     itemsOnPage: this.itemsOnPage,
    //     page: this.page,
    //     isFilter: this.isFilter,
    //     isHighSecurityObject: this.isHighSecurityObject,
    //     address: this.address,
    //     trashtypes: this.trashTypes,
    //     driveWay: this.driveWay,
    //     contacts: this.contacts,
    //     standTypes: this.standTypes,
    //     containerIds: this.containerIds,
    //     sorting: this.sorting
    //   }
    //   return await axios.post('http://192.168.31.112:7003/api/Sd/GetFilteredStand', data, {headers: {
    //     'Authorization': localStorage.getItem('Token')
    //     }
    //   })
    //   .then(( response ) => {
    //     console.log(response.data.data)
    //     for (let i = 0; i < response.data.data.length; i++) {

    async fetchContainerYards() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: this.isFilter,
        isHighSecurityObject: this.isHighSecurityObject,
        address: this.address,
        trashtypes: this.trashTypes,
        driveWay: this.driveWay,
        contacts: this.contacts,
        standTypes: this.standTypes,
        containerIds: this.containerIds,
        sorting: this.sorting
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredStand`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data)
        for (let i = 0; i < response.data.data.length; i++) {

          this.containerYards[i] = {
            id: response.data.data[i].standId,
            name: response.data.data[i].address
          }
        }
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async createContainer() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        name: this.name,
        volume: this.volume,
        containerType: this.tankType,
        containerStandId: this.choosenContainerYardId
      }
    console.log(data)
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/CreateContainer`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response)
        this.errorStatus = false
        this.showCreatedStatus = true
        setTimeout(() => {
          this.showCreatedStatus = false
          this.close(response.data.data)
        }, 3000)
      })
      .catch((error) => {
        console.log(error);
        this.errorStatus = true
        this.showCreatedStatus = true
        setTimeout(() => {
          this.showCreatedStatus = false
          this.errorStatus = false
        }, 3000)
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
    width: 600px;
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

        .row {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin: 15px 0;

          input {
            border: 1px solid #000;
            width: 70%;
            height: 30px;
            margin-left: 20px;
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
