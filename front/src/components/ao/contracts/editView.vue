<template>
  <div class="contractsEdit">
    <div class="contractsEdit__container" v-if="loadedAll">
      <div class="contractsEdit__container__medium">
        <div class="row">
          <vTitle class="pageTitle">Номер договора</vTitle>
          <input type="text" name="" v-model="contractNumber">
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Запись проверена</vTitle>
          <input type="checkbox" id="valid" @click="isValidated = !isValidated" :checked="isValidated"/>
          <label for="valid">
            <div class="validCheck"></div>
          </label>
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Автопродление договора</vTitle>
          <input type="checkbox" id="autorenew" @click="isAutorenewSchedules = !isAutorenewSchedules" :checked="isAutorenewSchedules"/>
          <label for="autorenew">
            <div class="autorenewCheck"></div>
          </label>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Статус подписания</vTitle>
          <!-- <input type="text" name="" v-model="status"> -->
          <input type="checkbox" id="object" @click="contractStatus = !contractStatus" :checked="contractStatus"/>
          <label for="object">
            <div class="objectCheck"></div>
          </label>
        </div>
      </div>
      <div class="contractsEdit__container__big">
        <div class="row">
          <vTitle class="pageTitle">Дата</vTitle>
          <date-picker 
            v-model="createDate" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerCreate"
          >
          </date-picker>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Дата начала действия</vTitle>
          <date-picker 
            v-model="startDate" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerCreate"
          >
          </date-picker>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Дата окончания действия</vTitle>
          <date-picker 
            v-model="endDate" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePickerCreate"
          >
          </date-picker>
        </div>
      </div>
      <div class="contractsEdit__container__small">
        <div class="row">
          <vTitle class="pageTitle">Контрагент</vTitle>
          <div class="select">
            <div @click.stop="showClients = !showClients">
              <input 
                type="text" 
                name="" 
                @keyup.enter="findClients"
                v-model="choosenClient"
              />
            </div>
            <div class="options" v-show="showClients" v-click-outside="closeClients">
              <div 
                class="option" 
                v-for="client in clients" 
                :key="client.contractClientId"
              >
                <vTitle class="pageTitle" @click.native.stop="chooseClient(client.contractClientId, client.clientName)">{{ client.clientName }}</vTitle>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="contractsEdit__container__small">
        <div class="contractsEdit__container__small__btn">
          <vButton class="greenButton rounded--10 size--32" @click.native="addContainerYards">Добавить контейнерные площадки</vButton>
        </div>
      </div>
      <div class="contractsEdit__container__small" v-if="standsForTable.length">
        <div class="row">
          <div class="main__table">
            <div class="main__table__header">
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
            <div class="main__table__header" v-for="(stand, index) in standsForTable" :key="index">
              <div class="table__number table__cell">
                <vText class="tableCell">{{stand.standId}}</vText>
              </div>
              <div class="table__regNumber table__cell">
                <vText class="tableCell">{{stand.address}}</vText>
              </div>
              <div class="table__vehicleName table__cell">
                <vText class="tableCell">{{stand.standType}}</vText>
              </div>
              <div class="table__vehicleName table__cell">
                <vText class="tableCell">{{stand.address}}</vText>
              </div>
              <div class="table__vehicleName table__cell">
                <vText class="tableCell">{{containerYardsLength(stand.standId)}}</vText>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="contractsEdit__container__small">
        <div class="row">
          <vTitle class="pageTitle">Способ отправки документов</vTitle>
          <input type="text" name="" v-model="sendDocsType">
        </div>
      </div>
      <div class="contractsEdit__container__small">
        <div class="row">
          <vTitle class="pageTitle">Комментарий</vTitle>
          <input type="text" name="" v-model="comment">
        </div>
      </div>
      <!-- <div class="contractsEdit__container__big">
        <div class="row">
          <vTitle class="pageTitle">Параметры расчета</vTitle>
          <input type="text" name="" v-model="calcParams">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Объем в год, м^3</vTitle>
          <input type="text" name="" v-model="volumePerYearM3">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Паспорт отходов</vTitle>
          <input type="text" name="" v-model="wastePassport">
        </div>
      </div>
      <div class="contractsEdit__container__medium">
        <div class="row">
          <vTitle class="pageTitle">Расчетная единица</vTitle>
          <input type="text" name="" v-model="calcUnit">
        </div>
        <div class="row">
          <vTitle class="pageTitle">Объем в месяц, м^3</vTitle>
          <input type="text" name="" v-model="volumePerMonthM3">
        </div>
      </div>
      <div class="contractsEdit__container__small">
        <div class="row">
          <vTitle class="pageTitle">Норматив</vTitle>
          <input type="text" name="" v-model="normativ">
        </div>
      </div> -->
    </div>
    <div v-else>
      <vTitle>Загрузка</vTitle>
    </div>
    <createdStatusModal v-if="showCreatedStatus" :isError="errorStatus"/>
    <!-- <keep-alive> -->
      <addContainerYards :attachedStands="standsForTable" v-if="showAddContainerYards" @close="closeAddContainerYards"></addContainerYards>
    <!-- </keep-alive> -->
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import axios from 'axios';

import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import vButton from "@/components/ui-kit/vButton";

import addContainerYards from "@/components/modals/addContainerYards";
import createdStatusModal from "@/components/modals/createdStatusModal";

export default {
  name: "editView",

  components: {
    vTitle,
    vText,
    vButton,
    DatePicker,
    addContainerYards,
    createdStatusModal,
  },

  props: {
    contractId: {
      type: String,
      default: ''
    },

    clientId: {
      type: String,
      default: ''
    }
  },

  data() {
    return {
      showCreatedStatus: false,
      errorStatus: false,
      loadedAll: true,
      createDate: '',
      startDate: '',
      endDate: null,
      comment: '',
      contractNumber: '',
      status: '',
      contractStatus: false,
      isValidated: false,
      isAutorenewSchedules: false,
      sendDocsType: '',
      calcParams: '',
      volumePerYearM3: null,
      volumePerMonthM3: null,
      calcUnit: '',
      wastePassport: '',
      normativ: '',
      attachedContainers: [],
      standsForTable: [],
      attachedStands: [],
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'YYYY-MM-DD',
      clients: [],
      showClients: false,
      choosenClient: '',
      choosenClientId: null,
      showAddContainerYards: false,
    }
  },

  created() {
    this.$root.$refs.ContractsEdit__component = this
    if (this.contractId != '') {
      this.loadedAll = false
      this.getContract()
    }
    if (this.clientId != '') {
      this.choosenClientId = this.clientId
    }
    this.fetchClients()
  },

  methods: {

    containerYardsLength(id) {
      let count = 0
      this.standsForTable.forEach((stand) => {
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

    addContainerYards() {
      this.showAddContainerYards = true
    },

    closeAddContainerYards(param) {
      console.log(param)
    // if (param.length) {
      this.standsForTable = param
    // }
      this.showAddContainerYards = false
    },

    chooseClient(id, name) {
      this.choosenClient = name
      this.choosenClientId = id
      this.closeClients()
    },

    closeClients() {
      this.showClients = false
    },

    saveContract() {
      if (this.contractId != '') {
        this.editContract()
      }
      else {
        this.createContract()
      }
    },

    async findClients() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: 0,
        page: 0,
        isFilter: true,
        clientName: this.choosenClient,
        clientContact: '',
        email: '',
        inn: '',
        kpp: '',
        ogrn: '',
        rschet: '',
        bank: '',
        bik: '',
        kschet: '',
        legalAddress: '',
        postalAddress: '',
        supervizorName: '',
        supervizorPosition: '',
        responsibleTkoPersonName: '',
        responsibleTkoPersonPhone: '',
        sorting: {}
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContractClient`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data);
        this.clients = response.data.data
        if (this.choosenClientId) {
          this.clients.forEach((client) => {
            if (client.contractClientId == this.choosenClientId) {
              this.choosenClient = client.clientName
            }
          })
        }
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async fetchClients() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: 10,
        page: 0,
        isFilter: false
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContractClient`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data);
        this.clients = response.data.data
        if (this.choosenClientId) {
          this.clients.forEach((client) => {
            if (client.contractClientId == this.choosenClientId) {
              this.choosenClient = client.clientName
            }
          })
        }
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async getContract() {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          contractId: this.contractId
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetContract`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response.data.data)
          this.contractNumber = response.data.data.contractNumber
          this.choosenClient = response.data.data.contractClient.clientName
          this.choosenClientId = response.data.data.contractClient.contractClientId
          this.status = response.data.data.status
          this.isAutorenewSchedules = response.data.data.isAutorenewSchedules
          this.contractStatus = response.data.data.contractStatus
          this.createDate = response.data.data.createDate
          this.comment = response.data.data.comment
          this.startDate = response.data.data.startDate
          this.endDate = response.data.data.endDate
          this.sendDocsType = response.data.data.sendDocsType
          this.calcParams = response.data.data.calcParams
          this.volumePerMonthM3 = response.data.data.volumePerMonthM3
          this.volumePerYearM3 = response.data.data.volumePerYearM3
          this.calcUnit = response.data.data.calcUnit
          this.wastePassport = response.data.data.wastePassport
          this.normativ = response.data.data.normativ
          this.attachedStands = response.data.data.attachedStands
          for (let i = 0; i < this.attachedStands.length; i++) {
            let obj = {}
            obj.address = this.attachedStands[i].address
            obj.attachedContainers = []
            obj.checked = true
            obj.contacts = this.attachedStands[i].contacts
            obj.coordinates = this.attachedStands[i].coordinates
            obj.driveWay = this.attachedStands[i].driveWay
            obj.isHighSecurityObject = this.attachedStands[i].isHighSecurityObject
            obj.photo = this.attachedStands[i].photo
            obj.standId = this.attachedStands[i].standId
            obj.standType = this.attachedStands[i].standType
            obj.trashType = this.attachedStands[i].trashType
            obj.attached = this.attachedStands[i].attachedContainers
            obj.workTimeFrom = this.attachedStands[i].workTimeFrom
            obj.workTimeTo = this.attachedStands[i].workTimeTo
            obj.isInRegistry = this.attachedStands[i].isInRegistry
            obj.inRegistryFromDate = this.attachedStands[i].inRegistryFromDate
            this.standsForTable.push(obj)
            this.standsForTable[i].attached.forEach((container) => {
              container.checked = true
              container.isInContract = false
            })
          }
          console.log(this.standsForTable)
          this.loadedAll = true
        })
        .catch((error) => {
          console.log(error);
        })
    },

    async editContract() {
      this.attachedContainers = []
      this.standsForTable.forEach((stand) => {
        stand.attached.forEach((container) => {
          if (container.checked) {
            let obj = {
              containerId: container.containerId,
              contractSchedule: container.cronShedule,
              sheduleComment: container.sheduleComment,
              workTimeFrom: new Date(`2022-02-02T${container.workTimeFrom}`),
              workTimeTo: new Date(`2022-02-02T${container.workTimeTo}`),
              isInRegistry: container.isInRegistry,
              inRegistryFromDate: container.inRegistryFromDate
            }
            console.log(obj)
            if (obj.contractSchedule.stepInitialDateSchedule == null || obj.contractSchedule.stepInitialDateSchedule.initialDate == null) {
              obj.contractSchedule.stepInitialDateSchedule = null
            }
            this.attachedContainers.push(obj)
          }
        })
      })
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          contractId: Number(this.contractId),
          contractNumber: this.contractNumber,
          contractClientId: this.choosenClientId,
          contractStatus: this.contractStatus,
          createDate: this.createDate,
          comment: this.comment,
          startDate: this.startDate,
          isValidated: this.isValidated,
          isAutorenewSchedules: this.isAutorenewSchedules,
          endDate: this.endDate,
          sendDocsType: this.sendDocsType,
          calcParams: null,
          volumePerMonthM3: null,
          volumePerYearM3: null,
          calcUnit: null,
          wastePassport: null,
          normativ: null,
          attachedContainers: this.attachedContainers
        }
        console.log(data)
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/UpdateContract`, data, {headers: {
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

    async createContract() {
      this.attachedContainers = []
      this.standsForTable.forEach((stand) => {
        stand.attached.forEach((container) => {
          if (container.checked) {
            let obj = {
              containerId: container.containerId,
              contractSchedule: container.cronShedule,
              sheduleComment: container.sheduleComment,
              workTimeFrom: new Date(`2022-02-02T${container.workTimeFrom}`),
              workTimeTo: new Date(`2022-02-02T${container.workTimeTo}`),
              isInRegistry: container.isInRegistry,
              inRegistryFromDate: container.inRegistryFromDate
            }
            if (obj.contractSchedule.stepInitialDateSchedule == null || obj.contractSchedule.stepInitialDateSchedule.initialDate == null) {
              obj.contractSchedule.stepInitialDateSchedule = null
            }
            this.attachedContainers.push(obj)
          }
        })
      })
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        contractNumber: this.contractNumber,
        contractClientId: Number(this.choosenClientId),
        isValidated: this.isValidated,
        isAutorenewSchedules: this.isAutorenewSchedules,
        contractStatus: this.contractStatus,
        createDate: this.createDate,
        startDate: this.startDate,
        endDate: this.endDate,
        comment: this.comment,
        sendDocsType: this.sendDocsType,
        // calcParams: this.calcParams,
        // calcUnit: this.calcUnit,
        // volumePerMonthM3: Number(this.volumePerMonthM3),
        // volumePerYearM3: Number(this.volumePerYearM3),
        // wastePassport: this.wastePassport,
        // normativ: this.normativ,
        attachedContainers: this.attachedContainers
      }
      console.log(data)
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/CreateContract`, data, { headers: {
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
.contractsEdit {
  input {
    font-family: 'Roboto', sans-serif;
    font-weight: 700;
    font-size: 14px;
    line-height: 17px;
  }

  &__container {
    width: 100%;

    &__small {
      display: flex;
      justify-content: flex-start;
      align-items: center;
      width: 50%;

      &__btn {
        display: flex;
        justify-content: flex-start;
        align-items: center;
        margin: 15px 0;
        width: 80%;
      }

      .row {
        display: flex;
        justify-content: flex-start;
        align-items: center;
        margin: 15px 0;
        width: 100%;

        input {
          border: 1px solid #000;
          width: calc(100% - 20px) !important;
          height: 30px;
          border-radius: 10px;
          margin-left: 20px;
        }

        .select {
          width: calc(100% - 20px);
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
            max-height: 250px;
            height: fit-content;
            overflow-y: scroll;

            h1 {
              font-family: 'Roboto', sans-serif;
              font-weight: 700;
              font-size: 18px;
              line-height: 21px;
            }
          }
        }

        .main {

          &__table {
            width: 100%;
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
                width: calc(100% / 5);
              }

              .header {
                background: rgba(0, 0, 0, 0.05);
              }
            }
          }
        }
      }
    }

    &__medium {
      display: flex;
      justify-content: flex-start;
      align-items: center;
      width: calc(((100%/3)*2) - 40px);

      .row {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin: 15px 0;
        width: 50%;

        &:nth-child(2) {
          margin: 15px 20px;
        }

        #object {
          display: none;

          &:checked + label .objectCheck {
            background: #000;
          }
        }

        .objectCheck, .validCheck, .autorenewCheck {
          margin-left: 20px;
          border: 1px solid #000;
          width: 30px;
          height: 30px;
          border-radius: 15px;
        }

        #valid {
          display: none;

          &:checked + label .validCheck {
            background: #000;
          }
        }

        #autorenew {
          display: none;

          &:checked + label .autorenewCheck {
            background: #000;
          }
        }

        input {
          border: 1px solid #000;
          width: 50%;
          height: 30px;
          border-radius: 10px;
          margin-left: 20px;
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
          width: 100%;
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
      }
    }

    &__big {
      display: flex;
      justify-content: flex-start;
      align-items: center;
      width: 100%;

      .row {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin: 15px 0;
        width: calc((100%/3) - 40px);

        &:nth-child(2) {
          margin: 15px 20px;
        }

        input {
          border: 1px solid #000;
          width: 50%;
          height: 30px;
          border-radius: 10px;
          margin-left: 20px;
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
          width: 100%;
          margin: 0;
          height: 34px;
          padding: 6px 30px;
          padding-left: 10px;
          background-color: #fff;
          border: 1px solid #000;
          border-radius: 10px;
          -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
          box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
          cursor: pointer;
        }
      }
    }
  }
}
</style>
