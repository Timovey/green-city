<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Поиск контейнеров</vTitle>
        <div class="cross" @click="reset()"></div>
      </div>
      <div class="clientsEdit__container">
        <div class="row">
          <vTitle class="pageTitle">Наименование</vTitle>
          <input type="text" name="" v-model="params.name"/>
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Запись корректна</vTitle>
          <input type="checkbox" id="valid" @click="params.isValidated = !params.isValidated" :checked="params.isValidated"/>
          <label for="valid">
            <div class="validCheck"></div>
          </label>
        </div>
        <div class="rowSelect">
          <vTitle class="pageTitle">Тип мусора</vTitle>
          <div class="select">
            <div @click.stop="showGarbageTypes = !showGarbageTypes">
              <input 
                type="text" 
                name="" 
                disabled
                v-model="choosenGarbageType"
              />
            </div>
            <div class="options" v-show="showGarbageTypes" v-click-outside="closeGarbageTypes">
              <div 
                class="option" 
                v-for="garbageType in garbageTypes" 
                :key="garbageType.id"
              >
                <vTitle class="pageTitle" @click.native.stop="chooseGarbageType(garbageType.id, garbageType.name)">{{ garbageType.name }}</vTitle>
              </div>
            </div>
          </div>
        </div>
        <!-- <div class="row greenRow">
          <vTitle class="pageTitleWhite">Тара</vTitle>
        </div>
        <div class="rowSelect">
          <vTitle class="pageTitle">Тип мусора</vTitle>
          <div class="select">
            <div @click.stop="showContainerTypes = !showContainerTypes">
              <input 
                type="text" 
                name="" 
                disabled
                v-model="choosenContainerType"
              />
            </div>
            <div class="options" v-show="showContainerTypes" v-click-outside="closeContainerTypes">
              <div 
                class="option" 
                v-for="type in containerTypes" 
                :key="type.id"
              >
                <vTitle class="pageTitle" @click.native.stop="chooseContainerType(type.id, type.name)">{{ type.name }}</vTitle>
              </div>
            </div>
          </div>
        </div> -->
        <div class="rowSelect">
          <vTitle class="pageTitle">Контейнерная площадка</vTitle>
          <div class="select">
            <div @click.stop="showContainerYards = !showContainerYards">
              <input 
                type="text" 
                name=""
                @keyup.enter="findContainerYards" 
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

import axios from 'axios';

import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

export default {
  name: "findContainers",

  components: {
    vTitle,
    vButton,
  },

  data() {
    return {
      showGarbageTypes: false,
      garbageTypes: [
        {
          id: 1,
          name: 'ТКО'
        },
        {
          id: 2,
          name: 'КГО'
        },
        {
          id: 3,
          name: '4 класс'
        },
        {
          id: 4,
          name: '5 класс'
        },
        {
          id: 5,
          name: 'Строительный'
        },
      ],
      types: [],
      containerYards: [],
      params: {
        name: '',
        choosenGarbageTypeId: null,
        choosenContainerTypeId: null,
        isValidated: false,
        choosenContainerYardId: null,
        tankType: ''
      },
      choosenGarbageType: '',
      choosenContainerType: '',
      showContainerYards: false,
      choosenContainerYard: '',
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

  created() {
    this.fetchContainerYards()
  },

  methods: {
    close(bool = false, params) {
      this.$emit('close', bool, params)
    },

    reset() {
      this.params.name = ''
      this.params.isValidated = false
      this.choosenContainerYard = ''
      this.params.choosenGarbageTypeId = null
      this.params.choosenContainerYardId = null
      this.params.tankType = ''
      this.choosenGarbageType = ''
      this.types.forEach((type) => {
        type.checked = false
      })
      this.close(false, this.params)
    },

    closeGarbageTypes() {
      this.showGarbageTypes = false
    },

    closeContainerTypes() {
      this.showContainerTypes = false
    },

    chooseGarbageType(id, name) {
      this.choosenGarbageType = name
      this.params.choosenGarbageTypeId = id
      this.closeGarbageTypes()
    },

    chooseContainerType(id, name) {
      this.choosenContainerType = name
      this.params.choosenContainerTypeId = id
      this.closeGarbageTypes()
    },

    filter() {
      this.close(true, this.params)
    },

    chooseContainerYard(id, name) {
      this.choosenContainerYard = name
      this.params.choosenContainerYardId = id
      this.closeContainerYards()
    },

    setTankType(param) {
      for (let i = 0; i < this.types.length; i++) {
        if (this.types[i].id == param) {
          this.types[i].checked = true
          this.params.tankType = param
        }
        else {
          this.types[i].checked = false
        }
      }
    },

    closeContainerYards() {
      this.showContainerYards = false
    },

    async findContainerYards() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: 0,
        page: 0,
        isFilter: true,
        isHighSecurityObject: null,
        address: this.choosenContainerYard,
        trashtypes: [],
        driveWay: '',
        contacts: '',
        standTypes: [],
        containerIds: [],
        sorting: {}
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredStand`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data)
        this.containerYards = []
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
              max-height: 250px;
              height: fit-content;
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
