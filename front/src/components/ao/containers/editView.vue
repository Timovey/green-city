<template>
  <div class="clientsEdit">
    <div class="clientsEdit__container" v-if="loadedAll">
      <div class="row">
        <vTitle class="pageTitle">Наименование</vTitle>
        <input type="text" name="name" v-model="name"/>
      </div>
      <div class="row rowObject">
        <vTitle class="pageTitle">Запись корректна</vTitle>
        <input type="checkbox" id="valid" @click="isValidated = !isValidated" :checked="isValidated"/>
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
      <div class="row greenRow">
        <vTitle class="pageTitleWhite">Тара</vTitle>
      </div>
      <div class="rowSelect">
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
              v-for="containerType in types" 
              :key="containerType.id"
            >
              <vTitle class="pageTitle" @click.native.stop="chooseContainerType(containerType.id, containerType.name)">{{ containerType.name }}</vTitle>
            </div>
          </div>
        </div>
      </div>
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
    <div v-else>
      <vTitle>Загрузка</vTitle>
    </div>
    <createdStatusModal v-if="showCreatedStatus" :isError="errorStatus"/>
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside';
import vTitle from "@/components/ui-kit/vTitle";
import createdStatusModal from "@/components/modals/createdStatusModal";

import axios from 'axios';

export default {
  name: "editView",

  components: {
    vTitle,
    createdStatusModal,
  },

  props: {
    containerId: {
      type: String,
      default: ''
    },
  },

  data() {
    return {
      showCreatedStatus: false,
      errorStatus: false,
      name: '',
      volume: null,
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
      showGarbageTypes: false,
      showContainerTypes: false,
      choosenContainerType: '',
      choosenContainerTypeId: null,
      choosenGarbageType: '',
      choosenGarbageTypeId: null,
      trashType: 0,
      containerYards: [],
      isValidated: false,
      showContainerYards: false,
      choosenContainerYard: '',
      choosenContainerYardId: null,
      tankType: '',
      isHighSecurityObject: null,
      address: '',
      trashtypes: [],
      driveWay: '',
      contacts: '',
      standTypes: [],
      containerIds: [],
      sorting: {},
      loadedAll: true,
    }
  },

  created() {
    //Пофиксить баг с прогрузкой контейнеров
    this.$root.$refs.ContainersEditView_component = this
    this.getContainerTypes()
    if (this.containerId != '') {
      this.loadedAll = false
      this.getContainer() //Вот этот
    }
    this.fetchContainerYards()
  },

  methods: {

    saveContainer() {
      if (this.containerId != '') {
        this.editContainer()
      }
      else {
        this.createContainer()
      }
    },

    chooseGarbageType(id, name) {
      this.choosenGarbageType = name
      this.choosenGarbageTypeId = id
      this.closeGarbageTypes()
    },

    chooseContainerType(id, name) {
      this.choosenContainerType = name
      this.choosenContainerTypeId = id
      this.closeContainerTypes()
    },

    closeContainerTypes() {
      this.showContainerTypes = false
    },

    closeGarbageTypes() {
      this.showGarbageTypes = false
    },

    chooseContainerYard(id, name) {
      this.choosenContainerYard = name
      this.choosenContainerYardId = id
      this.closeContainerYards()
    },

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

    closeContainerYards() {
      this.showContainerYards = false
    },

    async editContainer() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        name: this.name,
        trashType: this.choosenGarbageTypeId,
        isValidated: this.isValidated,
        containerTypeId: this.choosenContainerTypeId,
        containerStandId: this.choosenContainerYardId,
        containerIds: [this.containerId]
      }
      console.log(data)
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/UpdateContainer`, data, {headers: {
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

    async findContainerYards() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: 0,
        page: 0,
        isFilter: true,
        isHighSecurityObject: null,
        address: this.choosenContainerYard,
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
        itemsOnPage: 10,
        page: 0,
        isFilter: false,
        isHighSecurityObject: this.isHighSecurityObject,
        address: this.address,
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
          trashType: this.choosenGarbageTypeId,
          isValidated: this.isValidated,
          containerTypeId: this.choosenContainerTypeId,
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

    async getContainer() {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          containerIds: [this.containerId]
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetContainer`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response.data.data)
          this.name = response.data.data[0].name
          this.choosenContainerTypeId = response.data.data[0].containerTypeId
          this.types.forEach((type) => {
            if (type.id == response.data.data[0].containerTypeId) {
              this.choosenContainerType = type.name
            }
          })
          this.choosenGarbageTypeId = response.data.data[0].trashType
          this.garbageTypes.forEach((garbageType) => {
            if (garbageType.id == response.data.data[0].trashType) {
              this.choosenGarbageType = garbageType.name
              this.choosenGarbageTypeId = garbageType.id
            }
          })
          this.isValidated = response.data.data[0].isValidated
          this.choosenContainerYardId = response.data.data[0].containerType
          for (let i = 0; i < this.containerYards.length; i++) {
            if (this.containerYards[i].id == response.data.data[0].containerType) {
              this.choosenContainerYard = this.containerYards[i].name
            }
          }
          this.setTankType(response.data.data[0].containerType)
          this.loadedAll = true
        })
        .catch((error) => {
          console.log(error);
        })
    },

    async getContainerTypes() {
      let data = {
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetAllActualContainerTypes`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response.data.data)
          this.types = response.data.data
          if (this.choosenContainerTypeId) {
            this.types.forEach((type) => {
            if (type.id == this.choosenContainerTypeId) {
              this.choosenContainerType = type.name
            }
          })
          }
        })
        .catch((error) => {
          console.log(error);
        })
    }
  },

  directives: {
    ClickOutside
  }
}
</script>

<style lang="scss">
.clientsEdit {

  &__container {
    display: flex;
    flex-direction: column;
    width: 60%;

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
          overflow-y: scroll;
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
      margin-left: -20px !important;
    }
  }
}
</style>
