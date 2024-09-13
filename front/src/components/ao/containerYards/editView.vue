<template>
  <div class="clientsEdit">
    <div class="clientsEdit__container" v-if="loadedAll">
      <div class="row rowObject">
        <vTitle class="pageTitle">Режимный объект</vTitle>
        <input type="checkbox" id="object" @click="isHighSecurityObject = !isHighSecurityObject" :checked="isHighSecurityObject"/>
        <label for="object">
          <div class="objectCheck"></div>
        </label>
      </div>
      <div class="row rowObject">
        <vTitle class="pageTitle">Запись корректна</vTitle>
        <input type="checkbox" id="valid" @click="isValidated = !isValidated" :checked="isValidated"/>
        <label for="valid">
          <div class="validCheck"></div>
        </label>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Адрес</vTitle>
        <!-- <input type="text" name="" v-model="address" /> -->
        <addressInput :defaultAddress="address" class="addressInput"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Широта</vTitle>
        <input type="text" v-model="coordinates.coordinates[0].longitude" v-mask="'99.9{1,10}'" />
        <vTitle class="pageTitle">Долгота</vTitle>
        <input type="text" name="" v-model="coordinates.coordinates[0].latitude" v-mask="'99.9{1,10}'" />
        <vTitle class="pageTitle">Комментарий</vTitle>
        <input type="text" name="" v-model="coordinates.coordinatesComment" />
      </div>
      <div class="row rowObject">
        <vTitle class="pageTitle">Наличие основания</vTitle>
        <input type="checkbox" id="base" @click="haveBase = !haveBase" :checked="haveBase"/>
        <label for="base">
          <div class="baseCheck"></div>
        </label>
      </div>
      <div class="row rowObject">
        <vTitle class="pageTitle">Наличие крыши</vTitle>
        <input type="checkbox" id="roof" @click="haveRoof = !haveRoof" :checked="haveRoof"/>
        <label for="roof">
          <div class="roofCheck"></div>
        </label>
      </div>
      <div class="row rowObject">
        <vTitle class="pageTitle">Наличие ограждения</vTitle>
        <input type="checkbox" id="fence" @click="haveFence = !haveFence" :checked="haveFence"/>
        <label for="fence">
          <div class="fenceCheck"></div>
        </label>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Путь подъезда</vTitle>
        <input type="text" name="" v-model="driveWay"/>
      </div>
      <!-- <div class="row">
        <vTitle class="pageTitle">Фото</vTitle>
        <div class="photo"></div>
      </div> -->
      <div class="row">
        <vTitle class="pageTitle">Контакты</vTitle>
        <input type="text" name="" v-model="contacts"/>
      </div>
      <div class="rowSelect">
        <vTitle class="pageTitle">Тип площадки</vTitle>
        <div class="select">
          <div @click.stop="showStandTypes = !showStandTypes">
            <input 
              type="text" 
              name="" 
              disabled
              v-model="choosenStandType"
            />
          </div>
          <div class="options" v-show="showStandTypes" v-click-outside="closeStandTypes">
            <div 
              class="option" 
              v-for="standType in standTypes" 
              :key="standType.id"
            >
              <vTitle class="pageTitle" @click.native.stop="chooseStandType(standType.id, standType.name)">{{ standType.name }}</vTitle>
            </div>
          </div>
        </div>
      </div>
      <div class="row align-start">
        <vTitle class="pageTitle">Контейнер</vTitle>
        <div class="btn__close">
          <div class="cross" @click.stop="openContainerSelectionMethod()"></div>
        </div>
        <div class="main__table" v-if="showContainersTable && attachedContainerIds.length">
          <div class="main__table__header">
            <div class="table__cell header small__cell"></div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">№</vTitle>
            </div>
            <div class="table__cell header">
              <vTitle class="tableCellTitle">Наименование</vTitle>
            </div>
          </div>
          <div class="main__table__header" v-for="(attachedContainer, index) in attachedContainerIds" :key="index">
            <div class="table__number table__cell small__cell">
              <font-awesome-icon class="deleteBtn" @click.stop="removeContainer(attachedContainer.containerId)" :icon="['far', 'fa-trash-can']" />
            </div>
            <div class="table__number table__cell">
              <vText class="tableCell">{{attachedContainer.containerId}}</vText>
            </div>
            <div class="table__regNumber table__cell">
              <vText class="tableCell">{{attachedContainer.containerName}}</vText>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      <vTitle>Загрузка</vTitle>
    </div>
    <createdStatusModal v-if="showCreatedStatus" :isError="errorStatus"/>
    <containerSelectionMethod 
      @close="closeContainerSelectionMethod" 
      @show="openShowContainers" 
      @create="openAddContainer" 
      v-if="showContainerSelectionMethod"
    >
    </containerSelectionMethod>
    <addContainer 
      @close="closeAddContainer" 
      v-else-if="showAddContainer"
    > 
    </addContainer>
    <showContainers 
      @close="closeShowContainers" 
      v-else-if="showContainers"
    >
    </showContainers>
  </div>
</template>

<script>
import { library } from '@fortawesome/fontawesome-svg-core';
import { faTrashCan } from "@fortawesome/free-regular-svg-icons";
library.add(faTrashCan)

import ClickOutside from 'vue-click-outside'

import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import addressInput from "@/components/ui-kit/addressInput";
import containerSelectionMethod from "@/components/modals/containerSelectionMethod";
import addContainer from "@/components/modals/addContainer";
import showContainers from "@/components/modals/showContainers";
import createdStatusModal from "@/components/modals/createdStatusModal";

import axios from 'axios';

export default {
  name: "editView",

  components: {
    vTitle,
    vText,
    addressInput,
    containerSelectionMethod,
    addContainer,
    showContainers,
    createdStatusModal,
  },

  props: {
    containerYardId: {
      type: String,
      default: ''
    },
  },

  data() {
    return {
      showCreatedStatus: false,
      errorStatus: false,
      standTypes: [
        {
          id: 1,
          name: 'Общий'
        },
        {
          id: 2,
          name: 'Жилой фонд'
        },
      ],
      showStandTypes: false,
      showContainerSelectionMethod: false,
      showAddContainer: false,
      showContainers: false,
      choosenStandType: 'Общий',
      choosenStandTypeId: 1,
      isHighSecurityObject: false,
      haveBase: false,
      haveRoof: false,
      haveFence: false,
      isValidated: false,
      address: '',
      coordinates: {
        coordinates: [
          {
            longitude: null,
            latitude: null,
          }
        ],
        coordinatesComment: ''
      },
      driveWay: '',
      photo: '',
      contacts: '',
      standType: 0,
      attachedContainerIds: [],
      sorting: {},
      loadedAll: true,
      showContainersTable: false,
    }
  },

  created() {
    this.$root.$refs.ContainerYardsEdit__component = this
    if (this.containerYardId != '') {
      this.loadedAll = false
      this.getContainerYard()
    }
  },

  methods: {

    removeContainer(id) {
      for (let i = 0; i < this.attachedContainerIds.length; i++) {
        if (this.attachedContainerIds[i].containerId == id) {
          this.attachedContainerIds.splice(i, 1)
        }
      }
    },

    saveContainerYard() {
      if (this.containerYardId != '') {
        this.editContainerYard()
      }
      else {
        this.createContainerYard()
      }
    },

    chooseStandType(id, name) {
      this.choosenStandType = name
      this.choosenStandTypeId = id
      this.closeStandTypes()
    },
    
    closeStandTypes() {
      this.showStandTypes = false
    },

    openShowContainers() {
      this.closeContainerSelectionMethod()
      this.showContainers = true
      document.body.style.height = "100vh"
    },

    openAddContainer() {
      this.closeContainerSelectionMethod()
      this.showAddContainer = true
      document.body.style.height = "100vh"
    },

    openContainerSelectionMethod() {
      this.showContainerSelectionMethod = true
      document.body.style.height = "100vh"
    },

    closeContainerSelectionMethod() {
      this.showContainerSelectionMethod = false
      document.body.style.height = "auto"
    },

    closeAddContainer(id) {
      this.getContainer(id)
      this.showAddContainer = false
      document.body.style.height = "auto"
    },

    closeShowContainers(data) {
      if (data.length) {
        data.forEach((element) => {
          let flag = false
          for (let i = 0; i < this.attachedContainerIds.length; i++) {
            if (element.containerId == this.attachedContainerIds[i].containerId) {
              flag = true
              console.log(123)
            }
          }
          if (!flag) {
            this.attachedContainerIds.push(element)
          }
        })
        this.showContainersTable = true
      }
      this.showContainers = false
      document.body.style.height = "auto"
    },

    async getContainer(id) {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          containerIds: [id]
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetContainer`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response.data.data)
          let container = {
            containerId: response.data.data[0].containerId,
            containerName: response.data.data[0].name
          }
          this.attachedContainerIds.push(container)
          this.showContainersTable = true
          console.log(this.attachedContainerIds)
        })
        .catch((error) => {
          console.log(error);
        })
    },

    async editContainerYard() {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          standId: this.containerYardId,
          isHighSecurityObject: this.isHighSecurityObject,
          haveBase: this.haveBase,
          haveRoof: this.haveRoof,
          haveFence: this.haveFence,
          isValidated: this.isValidated,
          address: this.$root.$refs.AddressInput__component.address,
          coordinates: this.coordinates,
          driveWay: this.driveWay,
          photo: this.photo,
          contacts: this.contacts,
          standType: this.choosenStandTypeId,
        }
        let attachedContainers = []
        this.attachedContainerIds.forEach((attachedContainerId) => {
          attachedContainers.push(attachedContainerId.containerId)
        })
        data.attachedContainerIds = attachedContainers
        console.log(data)
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/UpdateStand`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response)
          this.errorStatus = false
          this.showCreatedStatus = true
          setTimeout(() => {
            this.showCreatedStatus = false
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

    async getContainerYard() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        standIds: [this.containerYardId]
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetStand`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data)
        this.isHighSecurityObject = response.data.data[0].isHighSecurityObject
        this.haveBase = response.data.data[0].haveBase
        this.haveRoof = response.data.data[0].haveRoof
        this.haveFence = response.data.data[0].haveFence
        this.isValidated = response.data.data[0].isValidated
        this.address = response.data.data[0].address
        this.coordinates = response.data.data[0].coordinates
        this.driveWay = response.data.data[0].driveWay
        this.photo = response.data.data[0].photo
        this.contacts = response.data.data[0].contacts
        this.standTypes.forEach((standType) => {
          if (standType.id == response.data.data[0].standType) {
            this.choosenStandType = standType.name
            this.choosenStandTypeId = standType.id
          }
        })
        this.attachedContainerIds = response.data.data[0].attachedContainers
        if (this.attachedContainerIds.length) {
          this.showContainersTable = true
        }
        this.loadedAll = true
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async createContainerYard() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        isHighSecurityObject: this.isHighSecurityObject,
        haveBase: this.haveBase,
        haveRoof: this.haveRoof,
        haveFence: this.haveFence,
        isValidated: this.isValidated,
        address: this.$root.$refs.AddressInput__component.address,
        coordinates: this.coordinates,
        driveWay: this.driveWay,
        photo: this.photo,
        contacts: this.contacts,
        standType: this.choosenStandTypeId,
      }
      let attachedContainers = []
      this.attachedContainerIds.forEach((attachedContainerId) => {
        attachedContainers.push(attachedContainerId.containerId)
      })
      data.attachedContainerIds = attachedContainers
      console.log(data)
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/CreateStand`, data, { headers: {
          'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response);
        this.errorStatus = false
        this.showCreatedStatus = true
        setTimeout(() => {
          this.showCreatedStatus = false
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
    }
  },

  directives: {
    ClickOutside
  }
}
</script>

<style lang="scss">
.deleteBtn {
  cursor: pointer;
}

.clientsEdit {

  &__container {
    display: flex;
    flex-direction: column;
    width: 60%;

    .row {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin: 15px 0;

      .btn {
        display: flex;
        align-items: center;
        justify-content: space-between;

        &__close {

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
              transform: rotate(90deg);
            }

            &:before {
              transform: rotate(0deg);
            }
          }
        }
      }

      input {
        border: 1px solid #000;
        width: 70%;
        height: 30px;
        margin-left: 20px;
      }

      .addressInput {
        width: 100%;
        position: relative;

        input {
          width: 100%;
        }
      }

      #object {
        display: none;

        &:checked + label .objectCheck {
          background: #000;
        }
      }

      #roof {
        display: none;

        &:checked + label .roofCheck {
          background: #000;
        }
      }

      #valid {
        display: none;

        &:checked + label .validCheck {
          background: #000;
        }
      }

      #fence {
        display: none;

        &:checked + label .fenceCheck {
          background: #000;
        }
      }

      #base {
        display: none;

        &:checked + label .baseCheck {
          background: #000;
        }
      }

      .objectCheck, .baseCheck, .fenceCheck, .roofCheck, .validCheck {
        margin-left: 20px;
        border: 1px solid #000;
        width: 30px;
        height: 30px;
        border-radius: 15px;
      }

      .photo {
        background: #C4C4C4;
        width: 70%;
        height: 200px;
      }

      .main {

        &__table {
          width: 70%;
          margin-top: 10px;
          box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);

          &__header {
            display: flex;
            width: 100%;
            justify-content: space-between;
            align-items: stretch;

            .table__cell {
              padding: 8px 10px;
              border: 1px solid #A5C081;
              width: calc((100% / 2) - 20px);
            }

            .small__cell {
              width: 40px !important;
              text-align: center;
            }

            .header {
              background: rgba(0, 0, 0, 0.05);
            }
          }
        }

        &__btn {
          display: flex;
          margin-top: 20px;
          justify-content: flex-end;
          align-items: center;

          &__container {
            width: 90px;
            height: 30px;
          }
        }
      }
    }

    .align-start {
      align-items: flex-start;
    }

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
        }

        .options {
          position: absolute;
          top: 35px;
          left: 0;
          width: 100%;
          padding: 5px 0;
          border: 1px solid #000;
          background: #FFF;
          z-index: 10;

          .option {
            padding: 0 10px;
            cursor: pointer;
            transition: all .2s ease;

            &:hover {
              background: rgba(0, 0, 0, 0.2);
            }
          }
        }
      }
    }

    .rowObject {
      justify-content: flex-start;
    }
  }
}
</style>
