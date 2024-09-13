<template>
  <div class="main">
    <div class="main__topBtn" v-show="contracts.length">
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__btn">
          <vButton class="greenButton" @click.native="openBulkUpdate" v-if="choosenContracts.length">Массовое обновление</vButton>
        </div>
      </div>
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__btn">
          <input type="number" v-model="itemsOnPage" @keyup.enter="changeItemsOnPage"/>
        </div>
      </div>
    </div>
    <div class="main__topBtn flex-end" v-show="contracts.length">
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__sortbtn">
          <vButton class="greenButton" @click.native.stop="sort">Сортировка</vButton>
        </div>
        <div class="main__topBtn__container__sortbtn">
          <vButton class="greenButton" @click.native.stop="resetSort">Сброс сортировки</vButton>
        </div>
      </div>
    </div>
    <div class="main__topBtn flex-end">
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__sortbtn">
          <input type="checkbox" :checked="!isFilterByCreatedUser" id="createdUserCheck" name="createdUserCheck" class="radioBtn">
          <label for="createdUserCheck" @click.prevent.stop="chooseUser">
            <div class="typeRadio">
              <div class="typeRadio__back"></div>
            </div>
            <div class="textForInput">
              <vText>Показать все записи</vText>
            </div>
          </label>
        </div>
      </div>
    </div>
    <div class="overflow--scroll">
      <table v-show="contracts.length">
        <thead>
          <tr>
            <th data-type="numeric"> 
              <span id="span1" class="resize-handle"></span>
            </th>
            <th data-type="numeric">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('id')">№</vTitle>
              <div v-if="checkArrowDown('id')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('id')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('isValidated')">Запись проверена</vTitle>
              <div v-if="checkArrowDown('isValidated')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('isValidated')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="numeric">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('contractNumber')">Номер договора</vTitle>
              <div v-if="checkArrowDown('contractNumber')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('contractNumber')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('status')">Статус подписания</vTitle>
              <div v-if="checkArrowDown('status')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('status')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('createDate')">Дата</vTitle>
              <div v-if="checkArrowDown('createDate')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('createDate')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('startDate')">Дата начала</vTitle>
              <div v-if="checkArrowDown('startDate')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('startDate')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('endDate')">Дата окончания</vTitle>
              <div v-if="checkArrowDown('endDate')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('endDate')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('sendDocsType')">Способ отправки</vTitle>
              <div v-if="checkArrowDown('sendDocsType')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('sendDocsType')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">Пользователь</vTitle>
              <span class="resize-handle"></span>
            </th>
            <!-- <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('calcParams')">Параметры расчета</vTitle>
              <div v-if="checkArrowDown('calcParams')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('calcParams')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('volumePerMonthM3')">Объем в месяц</vTitle>
              <div v-if="checkArrowDown('volumePerMonthM3')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('volumePerMonthM3')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('volumePerYearM3')">Объем в год</vTitle>
              <div v-if="checkArrowDown('volumePerYearM3')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('volumePerYearM3')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('calcUnit')">Расчетная единица</vTitle>
              <div v-if="checkArrowDown('calcUnit')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('calcUnit')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('wastePassport')">Паспорт отходов</vTitle>
              <div v-if="checkArrowDown('wastePassport')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('wastePassport')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('normativ')">Норматив</vTitle>
              <div v-if="checkArrowDown('normativ')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('normativ')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th> -->
          </tr>
        </thead>
        <tbody>
          <tr v-for="(contract, index) in contracts" :key="index">
            <td class="table__number table__cell direction--row" @click.stop>
              <input type="checkbox" :id="'radio_' + index" name="radioSelect" class="radioBtn">
              <label :for="'radio_' + index" @click.stop="chooseContract(contract.contractId)">
              <div class="typeRadio">
                  <div class="typeRadio__back"></div>
                </div>
              </label>
              <font-awesome-icon icon="fa-solid fa-pen" class="deleteBtn" @click.stop="openContractEdit(contract.contractId)"/>
              <font-awesome-icon @click.stop="openConfirmDelete(contract.contractId)" class="deleteBtn" :icon="['far', 'fa-trash-can']" />
            </td>
            <td><vText class="tableCell">{{contract.contractId}}</vText></td>
            <td><vText class="tableCell">{{contract.isValidated}}</vText></td>
            <td><vText class="tableCell">{{contract.contractNumber}}</vText></td>
            <td><vText class="tableCell">{{contract.contractStatus ? 'Подписан' : 'Не подписан'}}</vText></td>
            <td><vText class="tableCell">{{contract.createDate}}</vText></td>
            <td><vText class="tableCell">{{contract.startDate}}</vText></td>
            <td><vText class="tableCell">{{contract.endDate}}</vText></td>
            <td><vText class="tableCell">{{contract.sendDocsType}}</vText></td>
            <td><vText class="tableCell">{{contract.createdUserName}}</vText></td>
            <!-- <td><vText class="tableCell">{{contract.calcParams}}</vText></td>
            <td><vText class="tableCell">{{contract.volumePerMonthM3}}</vText></td>
            <td><vText class="tableCell">{{contract.volumePerYearM3}}</vText></td>
            <td><vText class="tableCell">{{contract.calcUnit}}</vText></td>
            <td><vText class="tableCell">{{contract.wastePassport}}</vText></td>
            <td><vText class="tableCell">{{contract.normativ}}</vText></td> -->
          </tr>
        </tbody>
      </table>
    </div>
    <div class="main__load" v-if="contracts.length && !loadedAll">
      <div class="main__load__btn">
        <vButton class="greenButton" @click.native="fetchMoreContracts">Загрузить еще</vButton>
      </div>
    </div>
    <div v-else-if="!contracts.length && !isEmpty">
      <vTitle>Загрузка...</vTitle>
    </div>
    <div v-else-if="!contracts.length && isEmpty">
      <vTitle>Реестр пуст</vTitle>
    </div>
    <confirmDelete v-if="showConfirmDelete" @delete="deleteContractById" @close="closeConfirmDelete"></confirmDelete>
    <keepAlive>
      <findContracts v-if="showFindContracts" @close="closeFindContracts"></findContracts>
    </keepAlive>
    <bulkUpdateContracts :choosenContracts="choosenContracts" v-if="showBulkUpdate" @close="closeBulkUpdate"></bulkUpdateContracts>
    <toTopButton></toTopButton>
  </div>
</template>

<script>
import { library } from '@fortawesome/fontawesome-svg-core';
import { faTrashCan } from "@fortawesome/free-regular-svg-icons";
import { faPen } from "@fortawesome/free-solid-svg-icons";
library.add(faTrashCan, faPen)

import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import vButton from "@/components/ui-kit/vButton";
import toTopButton from "@/components/ui-kit/toTopButton";

import axios from 'axios';

import findContracts from "@/components/modals/findContracts";
import confirmDelete from "@/components/modals/confirmDelete";
import bulkUpdateContracts from "@/components/modals/bulkUpdateContracts";

export default {
  name: "mainView",

  components: {
    confirmDelete,
    bulkUpdateContracts,
    findContracts,
    vTitle,
    vText,
    vButton,
    toTopButton,
  },

  props: {
    clientId: {
      type: String,
      default: ''
    }
  },

  data() {
    return {
      isEmpty: false,
      choosenContracts: [],
      showBulkUpdate: false,
      showConfirmDelete: false,
      showFindContracts: false,
      contractToDelete: null,
      isValidated: null,
      contracts: [],
      loadedAll: false,
      itemsOnPage: 5,
      page: 0,
      filter: false,
      isFilterByCreatedUser: true,
      contractNumber: '',
      contractClientIds: [],
      status: '',
      contractStatus: false,
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
      sorting: {
        id: {
          sortType: 0,
          sortPriority: 0
        },
        contractNumber: {
          sortType: 0,
          sortPriority: 0
        },
        isValidated: {
          sortType: 0,
          sortPriority: 0
        },
        status: {
          sortType: 0,
          sortPriority: 0
        },
        createDate: {
          sortType: 0,
          sortPriority: 0
        },
        startDate: {
          sortType: 0,
          sortPriority: 0
        },
        endDate: {
          sortType: 0,
          sortPriority: 0
        },
        sendDocsType: {
          sortType: 0,
          sortPriority: 0
        },
        calcParams: {
          sortType: 0,
          sortPriority: 0
        },
        volumePerMonthM3: {
          sortType: 0,
          sortPriority: 0
        },
        volumePerYearM3: {
          sortType: 0,
          sortPriority: 0
        },
        calcUnit: {
          sortType: 0,
          sortPriority: 0
        },
        wastePassport: {
          sortType: 0,
          sortPriority: 0
        },
        normativ: {
          sortType: 0,
          sortPriority: 0
        },
        contractStatus: {
          sortType: 0,
          sortPriority: 0
        }
      },
      priorityCount: 0,
    }
  },

  mounted() {
    // Our real web app uses Vue.js but I'll stick to plain JavaScript here
    /* eslint-disable */
    const min = 75;
    // The max (fr) values for grid-template-columns
    const columnTypeToRatioMap = {
      numeric: 1,
      'text-short': 1.67,
      'text-long': 3.33,
    };

    const table = document.querySelector('table');
    /*
      The following will soon be filled with column objects containing
      the header element and their size value for grid-template-columns 
    */
    const columns = [];
    let headerBeingResized;

    // The next three functions are mouse event callbacks

    // Where the magic happens. I.e. when they're actually resizing
    const onMouseMove = (e) => requestAnimationFrame(() => {
      console.log('onMouseMove');
      
      // Calculate the desired width
      let horizontalScrollOffset = document.documentElement.scrollLeft;
      let box = headerBeingResized.getBoundingClientRect()
      const width = (horizontalScrollOffset + e.clientX) - box.left;
      
      // Update the column object with the new size value
      const column = columns.find(({ header }) => header === headerBeingResized);
      if (Math.max(min, width) <= 500) {
        column.size = Math.max(min, width) + 'px'; // Enforce our minimum
      }
      else column.size = 500 + 'px'
      
      // For the other headers which don't have a set width, fix it to their computed width
      columns.forEach((column) => {
        if(column.size.startsWith('minmax')){ // isn't fixed yet (it would be a pixel value otherwise)
          column.size = parseInt(column.header.clientWidth, 10) + 'px';
        }
      });
      
      /* 
        Update the column sizes
        Reminder: grid-template-columns sets the width for all columns in one value
      */
      table.style.gridTemplateColumns = columns
        .map(({ header, size }) => size)
        .join(' ');
    });

    // Clean up event listeners, classes, etc.
    const onMouseUp = () => {
      console.log('onMouseUp');
      
      window.removeEventListener('mousemove', onMouseMove);
      window.removeEventListener('mouseup', onMouseUp);
      headerBeingResized.classList.remove('header--being-resized');
      headerBeingResized = null;
    };

    // Get ready, they're about to resize
    const initResize = ({ target }) => {
      console.log('initResize');
      
      headerBeingResized = target.parentNode;
      window.addEventListener('mousemove', onMouseMove);
      window.addEventListener('mouseup', onMouseUp);
      headerBeingResized.classList.add('header--being-resized');
    };

    // Let's populate that columns array and add listeners to the resize handles
    document.querySelectorAll('th').forEach((header) => {
      const max = columnTypeToRatioMap[header.dataset.type] + 'fr';
      columns.push({ 
        header, 
        // The initial size value for grid-template-columns:
        size: `minmax(${min}px, ${max})`,
      });
      header.querySelector('.resize-handle').addEventListener('mousedown', initResize);
    });
    /* eslint-enable */
  },

  created() {
    this.$root.$refs.ContractsMainView__component = this
    if (this.clientId) {
      this.contractClientIds.push(this.clientId)
      this.filter = true
    }
    this.fetchContracts()
  },

  methods: {

    chooseContract(id) {
      let foundFlag = false
      for (let i = 0; i < this.choosenContracts.length; i++) {
        if (this.choosenContracts[i] == id) {
          this.choosenContracts.splice(i, 1)
          foundFlag = true
        }
      }
      if (!foundFlag) {
        this.choosenContracts.push(id)
      }
      console.log(this.choosenContracts)
    },

    sortField(value) {
      if (this.sorting[value].sortType == 0) {
        this.priorityCount ++
        this.sorting[value].sortType = 1
        this.sorting[value].sortPriority = this.priorityCount
      }
      else if (this.sorting[value].sortType == 1) {
        this.sorting[value].sortType = 2
      }
      else {
        this.sorting[value].sortType = 0
        this.sorting[value].sortPriority = 0
      }
    },

    resetSort() {
      this.sorting.id.sortType = 0
      this.sorting.id.sortPriority = 0
      this.sorting.isValidated.sortType = 0
      this.sorting.isValidated.sortPriority = 0
      this.sorting.contractNumber.sortType = 0
      this.sorting.contractNumber.sortPriority = 0
      this.sorting.status.sortType = 0
      this.sorting.status.sortPriority = 0
      this.sorting.createDate.sortType = 0
      this.sorting.createDate.sortPriority = 0
      this.sorting.startDate.sortType = 0
      this.sorting.startDate.sortPriority = 0
      this.sorting.endDate.sortType = 0
      this.sorting.endDate.sortPriority = 0
      this.sorting.sendDocsType.sortType = 0
      this.sorting.sendDocsType.sortPriority = 0
      this.sorting.calcParams.sortType = 0
      this.sorting.calcParams.sortPriority = 0
      this.sorting.volumePerMonthM3.sortType = 0
      this.sorting.volumePerMonthM3.sortPriority = 0
      this.sorting.volumePerYearM3.sortPriority = 0
      this.sorting.volumePerYearM3.sortType = 0
      this.sorting.calcUnit.sortPriority = 0
      this.sorting.calcUnit.sortPriority = 0
      this.sorting.wastePassport.sortType = 0
      this.sorting.wastePassport.sortPriority = 0
      this.sorting.normativ.sortPriority = 0
      this.sorting.normativ.sortType = 0
      this.sorting.contractStatus.sortPriority = 0
      this.sorting.contractStatus.sortType = 0
      this.priorityCount = 0
      this.filter = false
      this.sort()
    },

    checkArrowDown(value) {
      if (this.sorting[value].sortType == 1) {
        return true
      }
      else return false
    },

    checkArrowUp(value) {
      if (this.sorting[value].sortType == 2) {
        return true
      }
      else return false
    },

    chooseUser() {
      this.isFilterByCreatedUser = !this.isFilterByCreatedUser
      this.contracts = []
      this.page = 0
      this.loadedAll = false
      this.fetchContracts()
    },

    sort() {
      this.contracts = []
      this.page = 0
      this.loadedAll = false
      this.filter = true
      console.log(this.sorting)
      this.fetchContracts()
    },

    changeItemsOnPage() {
      this.contracts = []
      this.page = 0
      this.loadedAll = false
      this.fetchContracts()
    },

    openContractEdit(id) {
      this.$root.$refs.Contracts__component.openContractEdit(id)
    },

    openFindContracts() {
      this.showFindContracts = true
      document.body.style.height = "100vh"
    },

    openConfirmDelete(id) {
      this.contractToDelete = id
      this.showConfirmDelete = true
      document.body.style.height = "100vh"
    },

    openBulkUpdate() {
      this.showBulkUpdate = true
      document.body.style.height = "100vh"
    },

    closeBulkUpdate(flag) {
      this.showBulkUpdate = false
      document.body.style.height = "auto"
      if (flag) {
        this.page = 0
        this.contracts = []
        this.fetchContracts()
      }
    },

    closeConfirmDelete() {
      this.contaractToDelete = null
      this.showConfirmDelete = false
      document.body.style.height = "auto"
    },

    closeFindContracts(bool, params) {
      console.log(params)
      this.showFindContracts = false
      document.body.style.height = "auto"
      if (bool) {
        this.filter = true
        this.contractNumber = params.contractNumber
        this.isValidated = params.isValidated
        this.contractClientIds = params.contractClientIds
        this.contractStatus = params.status
        this.sendDocsType = params.sendDocsType
        this.calcParams = params.calcParams
        this.volumePerMonthM3From = params.volumePerMonthM3From
        this.volumePerMonthM3To = params.volumePerMonthM3To
        this.volumePerYearM3From = params.volumePerYearM3From
        this.volumePerYearM3To = params.volumePerYearM3To
        this.calcUnit = params.calcUnit
        this.wastePassport = params.wastePassport
        this.normativ = params.normativ
        this.attachedStands = params.attachedStands
        this.attachedContainers = params.attachedContainers
        this.contracts = []
        this.page = 0
        this.loadedAll = false
        this.fetchContracts()
      }
      else {
        if (this.filter) {
          this.filter = false
          this.contractNumber =''
          this.isValidated = false
          this.contractClientIds = []
          this.contractStatus = false
          this.sendDocsType = ''
          this.calcParams = ''
          this.volumePerMonthM3From = null
          this.volumePerMonthM3To = null
          this.volumePerYearM3From = null
          this.volumePerYearM3To = null
          this.calcUnit = ''
          this.wastePassport = ''
          this.normativ = ''
          this.calcUnit = ''
          this.attachedStands = []
          this.attachedContainers = []
          this.contracts = []
          this.page = 0
          this.loadedAll = false
          this.fetchContracts()
        }
      }
    },

    async fetchContracts() {
      this.isEmpty = false
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: this.filter,
        isFilterByCreatedUser: this.isFilterByCreatedUser,
        contractNumber: this.contractNumber,
        isValidated: this.isValidated,
        contractClientIds: this.contractClientIds,
        status: this.status,
        contractStatus: this.contractStatus,
        sendDocsType: this.sendDocsType,
        calcParams: this.calcParams,
        volumePerMonthM3From: this.volumePerMonthM3From,
        volumePerMonthM3To: this.volumePerMonthM3To,
        volumePerYearM3From: this.volumePerYearM3From,
        volumePerYearM3To: this.volumePerYearM3To,
        calcUnit: this.calcUnit,
        wastePassport: this.wastePassport,
        normativ: this.normativ,
        attachedStands: this.attachedStands,
        attachedContainers: this.attachedContainers,
        sorting: this.sorting
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContract`, data, { headers: {
          'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response.data.data);
        this.page ++
        if (response.data.data.length == 0) {
          this.isEmpty = true
        }
        if (response.data.data.length == this.itemsOnPage) {
          this.contracts = response.data.data
        }
        else {
          this.contracts = this.contracts.concat(response.data.data)
          this.loadedAll = true
        }
      })
      .catch((error) => {
        console.log(error);
        this.isEmpty = true
      })
    },

    async fetchMoreContracts() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: this.filter,
        isFilterByCreatedUser: this.isFilterByCreatedUser,
        contractNumber: this.contractNumber,
        isValidated: this.isValidated,
        contractClientIds: this.contractClientIds,
        status: this.status,
        contractStatus: this.contractStatus,
        sendDocsType: this.sendDocsType,
        calcParams: this.calcParams,
        volumePerMonthM3From: this.volumePerMonthM3From,
        volumePerMonthM3To: this.volumePerMonthM3To,
        volumePerYearM3From: this.volumePerYearM3From,
        volumePerYearM3To: this.volumePerYearM3To,
        calcUnit: this.calcUnit,
        wastePassport: this.wastePassport,
        normativ: this.normativ,
        attachedStands: this.attachedStands,
        attachedContainers: this.attachedContainers,
        sorting: this.sorting
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContract`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log('second', response.data.data);
        this.page ++
        if (response.data.data.length == this.itemsOnPage) {
          this.contracts = this.contracts.concat(response.data.data)
        }
        else {
          this.contracts = this.contracts.concat(response.data.data)
          this.loadedAll = true
        }
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async deleteContractById() {
      this.showConfirmDelete = false
      if (this.contractToDelete != null) {
        let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          contractIds: [this.contractToDelete]
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/DeleteContract`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response)
          this.contracts = []
          this.page = 0
          this.loadedAll = false
          this.fetchContracts()
          this.contractToDelete = null
          this.showConfirmDelete = false
          document.body.style.height = "auto"
        })
        .catch((error) => {
          console.log(error);
        })
      }
    },
  }
}
</script>

<style lang="scss" scoped>
.overflow--scroll {
  overflow-x: scroll;
}

.flex-end {
  justify-content: flex-end !important;
}

.main {
  padding-top: 15px;

  &__topBtn {
    display: flex;
    justify-content: space-between;

    &__container {
      display: flex;
      justify-content: space-between;
      align-items: center;

      &__sortbtn {
        margin-top: 10px;

        &:first-child {
          margin-right: 10px;
        }

        label {
          display: flex;

          .textForInput {
            margin-left: 10px;
          }
        }
      }

      &__btn {
        width: fit-content;
        height: 30px;

        &:nth-child(2) {
          margin: 0 10px 0 10px;
        }

        input {
          border: 1px solid #000;
          border-radius: 10px;
          padding: 5px 10px;
        }
      }
    }
  }

  .direction--row {
    flex-direction: row;
    display: flex;
    justify-content: space-between;
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

  .deleteBtn {
    cursor: pointer;
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

  table {
    min-width: 100%;
    width: 100%;
    flex: 1;
    display: grid;
    border-collapse: collapse;
    margin-top: 10px;
    box-shadow: 0px 4px 0px rgba(0, 0, 0, 0.25);

    /* These are just initial values which are overriden using JavaScript when a column is resized */
    grid-template-columns: 
      minmax(100px, 0.3fr)
      minmax(75px, 0.3fr)
      minmax(75px, 0.3fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr);
  }

  thead,
  tbody,
  tr {
    display: contents;
  }

  th,
  td {
    padding: 15px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }

  th {
    position: sticky;
    top: 0;
    background: rgba(0, 0, 0, 0.05);
    border: 1px solid #A5C081;
    position: relative;
    display: flex;
    justify-content: center;
    align-items: center;

    .tableCellTitle {
      width: fit-content;
    }

    .tableCellArrowDown {
      position: relative;
      margin-left: 10px;
      height: 10px;
      width: 10px;

      &::after {
        content: '';
        width: 2px;
        height: 10px;
        background: black;
        transform: rotate(45deg);
        position: absolute;
        top: 0;
        bottom: 0;
        right: 0;
      }

      &::before {
        content: '';
        width: 2px;
        height: 10px;
        background: black;
        transform: rotate(-45deg);
        position: absolute;
        top: 0;
        bottom: 0;
        right: 6px;
      }
    }

    .tableCellArrowUp {
      position: relative;
      margin-left: 10px;
      height: 10px;
      width: 10px;

      &::after {
        content: '';
        width: 2px;
        height: 10px;
        background: black;
        transform: rotate(-45deg);
        position: absolute;
        top: 0;
        bottom: 0;
        right: 0;
      }

      &::before {
        content: '';
        width: 2px;
        height: 10px;
        background: black;
        transform: rotate(45deg);
        position: absolute;
        top: 0;
        bottom: 0;
        right: 6px;
      }
    }
  }

  .resize-handle {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    background: black;
    opacity: 0;
    width: 3px;
    cursor: col-resize;
    user-select: none;
  }

  .resize-handle:hover,
  /* The following selector is needed so the handle is visible during resize even if the mouse isn't over the handle anymore */
  .header--being-resized .resize-handle {
    opacity: 0.5;
  }

  th:hover .resize-handle {
    opacity: 0.3;
  }

  td {
    padding: 8px 10px;
    border: 1px solid #A5C081;
  }

  tbody tr:hover > td { 
    background: rgba(0, 0, 0, 0.05);
  }
}
</style>
