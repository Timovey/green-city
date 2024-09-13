<template>
  <div class="modal">
    <div class="modal__container" v-click-outside="close">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Выбор контейнера</vTitle>
        <div class="cross" @click="close()"></div>
      </div>
      <div class="modal__container__content">
        <div class="main__table">
          <div class="main__table__header">
            <div class="table__cell header small__cell"></div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">№</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Наименование</vTitle>
            </div>
          </div>
          <div class="main__table__header" v-for="(container, index) in containersCopy" :key="index">
            <div class="table__number table__cell small__cell">
              <input type="checkbox" :id="'radio_' + index" name="radioSelect" class="radioBtn" :checked="container.checked">
              <label :for="'radio_' + index" @click.prevent.stop="chooseContainer(container.containerId)">
                <div class="typeRadio">
                  <div class="typeRadio__back"></div>
                </div>
              </label>
            </div>
            <div class="table__number table__cell" @click="chooseContainer(container.containerId)">
              <vText class="tableCell">{{container.containerId}}</vText>
            </div>
            <div class="table__regNumber table__cell" @click="chooseContainer(container.containerId)">
              <vText class="tableCell">{{container.name}}</vText>
            </div>
          </div>
        </div>
        <div class="main__load" v-if="containers.length && !loadedAll">
          <div class="main__load__btn">
            <vButton class="greenButton" @click.native="fetchMoreContainers">Загрузить еще</vButton>
          </div>
        </div>
      </div>
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native.stop="saveChoose()">Выбрать</vButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'

import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import vButton from "@/components/ui-kit/vButton";

import axios from 'axios';

export default {
  name: "showContainers",

  components: {
    vTitle,
    vText,
    vButton,
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
      showOrganizations: false,
      organization: '',
      containers: [],
      containersCopy: [],
      itemsOnPage: 10,
      page: 0,
      loadedAll: false,
      filter: true,
      name: '',
      containerTypes: [],
      containerStandIds: [],
      sorting: {}
    }
  },

  created() {
    this.fetchContainers()
  },

  methods: {
    close(data = []) {
      this.$emit('close', data)
    },

    saveChoose() {
      let containersArray = []
      this.containersCopy.forEach((container) => {
        if (container.checked) {
          let obj = {
            containerId: container.containerId,
            containerName: container.name
          }
          containersArray.push(obj)
        }
      })
      this.close(containersArray)
    },

    chooseOrganization(id, name) {
      this.organization = name
      this.organizationId = id
      this.showOrganizations = false
    },

    chooseContainer(id) {
      console.log(id)
      this.containersCopy.forEach((container) => {
        if (container.containerId == id) {
          container.checked = !container.checked
        }
      })
    },  

    createCopyContainers() {
      for (let i = 0; i < this.containers.length; i++) {
        let obj = {
          containerId: this.containers[i].containerId,
          name: this.containers[i].name,
          checked: false
        }
        this.containersCopy.push(obj)
      }
    },

    async fetchContainers() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: this.filter,
        name: this.name,
        containerTypes: this.containerTypes,
        containerStandIds: this.containerStandIds,
        sorting: this.sorting
      }
      console.log(data)
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContainer`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data);
        this.page ++
        if (response.data.data.length == this.itemsOnPage) {
          this.containers = response.data.data
        }
        else {
          this.containers = this.containers.concat(response.data.data)
          this.loadedAll = true
        }
        this.createCopyContainers()
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async fetchMoreContainers() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: this.filter,
        name: this.name,
        containerTypes: this.containerTypes,
        containerStandIds: this.containerStandIds,
        sorting: this.sorting
      }
      return await axios.post('http://192.168.31.112:7003/api/Sd/GetFilteredContainer', data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log('second', response.data.data);
        this.page ++
        if (response.data.data.length == this.itemsOnPage) {
          this.containers = this.containers.concat(response.data.data)
        }
        else {
          this.containers = this.containers.concat(response.data.data)
          this.loadedAll = true
        }
        this.containersCopy = []
        this.createCopyContainers()
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
    width: 600px;
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
              width: calc((100% / 2) - 20px);
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
