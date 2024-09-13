<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Выбор контейнерной площадки</vTitle>
        <div class="cross" @click="close()"></div>
      </div>
      <div class="modal__container__content" v-if="containerYards.length">
        <div class="main__table">
          <div class="main__table__header">
            <div class="table__cell header small__cell"></div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">№</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Адрес</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Тип площадки</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Контакты</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Выбрано контейнеров</vTitle>
            </div>
          </div>
          <div class="main__table__header" v-for="(containerYard, index) in containerYards" :key="index">
            <div class="table__cell small__cell">
              <input type="checkbox" :id="'radioCheckedStand_' + index" :checked="containerYard.checked" name="radioSelect" class="radioBtn">
              <label :for="'radioCheckedStand_' + index" @click.prevent.stop="chooseContainerYard(containerYard.standId)">
                <div class="typeRadio">
                  <div class="typeRadio__back"></div>
                </div>
              </label>
            </div>
            <div class="table__number table__cell" @click="openContainersList(containerYard.standId, containerYard.address, containerYard.attached)">
              <vText class="tableCell">{{containerYard.standId}}</vText>
            </div>
            <div class="table__regNumber table__cell" @click="openContainersList(containerYard.standId, containerYard.address, containerYard.attached)">
              <vText class="tableCell">{{containerYard.address}}</vText>
            </div>
            <div class="table__regNumber table__cell" @click="openContainersList(containerYard.standId, containerYard.address, containerYard.attached)">
              <vText class="tableCell">{{containerYard.standTypeName}}</vText>
            </div>
            <div class="table__regNumber table__cell" @click="openContainersList(containerYard.standId, containerYard.address, containerYard.attached)">
              <vText class="tableCell">{{containerYard.contacts}}</vText>
            </div>
            <div class="table__regNumber table__cell" @click="openContainersList(containerYard.standId, containerYard.address, containerYard.attached)">
              <vText class="tableCell">{{containerYardsLength(containerYard.standId)}}</vText>
            </div>
          </div>
        </div>
        <div class="main__load" v-if="containerYards.length && !loadedAll">
          <div class="main__load__btn">
            <vButton class="greenButton" @click.native="fetchMoreContainerYards">Загрузить еще</vButton>
          </div>
        </div>
      </div>
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native.stop="saveChoose()">Выбрать</vButton>
        </div>
      </div>
    </div>
    <containersListForContract v-if="showContainersList" :attached="choosenStandContainers" :choosenStand="choosenStand" @close="closeContainersList"></containersListForContract>  
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'

import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import vButton from "@/components/ui-kit/vButton";

import containersListForContract from "@/components/modals/containersListForContract";

import axios from 'axios';

export default {
  name: "addContainerYards",

  components: {
    vTitle,
    vText,
    vButton,
    containersListForContract,
  },

  data() {
    return {
      containerYards: [],
      choosenContainerYards: [],
      showContainersList: false,
      choosenStand: {
        id: null,
        address: ""
      },
      attachedContainers: [],
      choosenStandContainers: [],
      params: [],
      itemsOnPage: 10,
      page: 0,
      loadedAll: false,
    }
  },

  props: {
    attachedStands: {
      type: Array,
      default() {
        return []
      }
    }
  },

  created() {
    this.fetchContainerYards()
    console.log(this.attachedStands)
  },

  methods: {

    containerYardsLength(id) {
      let count = 0
      this.containerYards.forEach((stand) => {
        if (stand.standId == id) {
          stand.attached.forEach((container) => {
            if (container.checked) {
              count ++
            }
          })
        }
      })
      return count
    },

    close(params = []) {
      this.$emit('close', params)
    },

    saveChoose() {
      this.containerYards.forEach((stand) => {
        if (stand.checked) {
          this.choosenContainerYards.push(stand)
        }
      })
      this.close(this.choosenContainerYards)
    },

    openContainersList(id, address, arr) {
      this.choosenStand.id = id
      this.choosenStand.address = address
      this.choosenStandContainers = arr
      this.showContainersList = true
    },

    closeContainersList(param) {
      console.log(123, param)
      this.containerYards.forEach((stand) => {
        if (stand.standId == this.choosenStand.id) {
          stand.attached = param
        }
      })
      this.showContainersList = false
      this.choosenStandContainers = []
      this.choosenStand.id = null
      this.choosenStand.address = ""
      console.log(this.containerYards)
    },

    chooseContainerYard(id) {
      for (let i = 0; i < this.containerYards.length; i++) {
        if (this.containerYards[i].standId == id) {
          this.containerYards[i].checked = !this.containerYards[i].checked
        }
      }
      console.log(this.containerYards)
    },

    async fetchContainerYards() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: false,
        sorting: {}
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredStand`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        response.data.data.forEach((stand) => {
          stand.attached = []
          stand.checked = false
        })
        if (this.attachedStands.length) {
          this.attachedStands.forEach((s) => {
            response.data.data.forEach((stand) => {
              if (s.standId == stand.standId) {
                stand.checked = s.checked
                stand.attached = s.attached
              }
            })
          })
        }
        console.log(response.data.data);
        this.page ++
        if (response.data.data.length == this.itemsOnPage) {
          this.containerYards = response.data.data
        }
        else {
          this.containerYards = this.containerYards.concat(response.data.data)
          this.loadedAll = true
        }
        console.log(this.containerYards)
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async fetchMoreContainerYards() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: false,
        sorting: {}
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        response.data.data.forEach((stand) => {
          stand.attached = []
          stand.checked = false
        })
        if (this.attachedStands.length) {
          this.attachedStands.forEach((s) => {
            response.data.data.forEach((stand) => {
              if (s.standId == stand.standId) {
                stand.checked = s.checked
                stand.attached = s.attached
              }
            })
          })
        }
        console.log(response.data.data);
        this.page ++
        if (response.data.data.length == this.itemsOnPage) {
          this.containerYards = this.containerYards.concat(response.data.data)
        }
        else {
          this.containerYards = this.containerYards.concat(response.data.data)
          this.loadedAll = true
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

  &__container {
    width: 800px;
    height: fit-content;
    background: #FFF;
    border: 1px solid #000;
    padding: 20px 10px 40px 10px;

    &__save {
      margin-top: 20px;
      width: 100%;
      display: flex;
      justify-content: flex-end;
      align-items: center;

      .row {
        width: 100px;
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

        &:hover {
          animation-name: rotate;
          animation-duration: 0.3s;

          @keyframes rotate {
            0% {
              transform: rotate(0deg);
            }

            50% {
              transform: rotate(45deg) scale(1.05);
            }

            100% {
              transform: rotate(90deg) scale(1.0);
            }
          }
        }

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
      .main {

        &__table {
          margin-top: 10px;
          box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
          max-height: 500px;
          overflow-y: scroll;
          height: fit-content;

          &__header {
            display: flex;
            width: 100%;
            justify-content: space-between;
            align-items: stretch;

            .small__cell {
              width: 40px !important;
              text-align: center;
            }

            .table__cell {
              padding: 8px 10px;
              border: 1px solid #A5C081;
              width: calc((100% / 5) - 40px/5);
            }

            .table__shedule {
              display: flex;
              align-items: center;
              justify-content: flex-start;

              input {
                border: 1px solid #000;
                width: 50%;
                border-radius: 5px;
                height: 30px;
                padding: 5px;
                margin-right: 20px;
              }
            }

            .header {
              background: rgba(0, 0, 0, 0.05);
            }
          }
        }
        
        &__load {
          margin-top: 40px;
          display: flex;
          justify-content: center;
          align-items: center;

          &__btn {
            width: fit-content;
          }
        }
      }
    }
  }
}
</style>
