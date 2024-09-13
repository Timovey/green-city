<template>
  <div class="main">
    <div class="main__topBtn" v-show="clients.length">
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__btn" v-if="choosenClientId">
          <vButton class="greenButton" @click.native="createContract">Создать договор</vButton>
        </div>
        <div class="main__topBtn__container__btn" v-if="choosenClientId">
          <vButton class="greenButton" @click.native="showCurrentContracts">Текущий договор</vButton>
        </div>
        <!-- <div class="main__topBtn__container__btn">
          <vButton class="greenButton" @click.native.stop="openModal">Печатать договор</vButton>
        </div> -->
      </div>
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__btn">
          <input type="number" v-model="itemsOnPage" @keyup.enter="changeItemsOnPage"/>
        </div>
      </div>
    </div>
    <div class="main__topBtn flex-end" v-show="clients.length">
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
      <table v-show="clients.length">
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
              <vTitle class="tableCellTitle" @click.native.stop="sortField('clientName')">Наименование</vTitle>
              <div v-if="checkArrowDown('clientName')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('clientName')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('isValidated')">Запись корректна</vTitle>
              <div v-if="checkArrowDown('isValidated')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('isValidated')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('inn')">ИНН</vTitle>
              <div v-if="checkArrowDown('inn')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('inn')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('kpp')">КПП</vTitle>  
              <div v-if="checkArrowDown('kpp')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('kpp')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('ogrn')">ОГРН</vTitle>
              <div v-if="checkArrowDown('ogrn')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('ogrn')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('rschet')">Р/СЧ</vTitle>
              <div v-if="checkArrowDown('rschet')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('rschet')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('bank')">Банк</vTitle>
              <div v-if="checkArrowDown('bank')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('bank')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('bik')">БИК</vTitle>
              <div v-if="checkArrowDown('bik')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('bik')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('kschet')">К/СЧ</vTitle>
              <div v-if="checkArrowDown('kschet')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('kschet')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('legalAddress')">Юридический адрес</vTitle>
              <div v-if="checkArrowDown('legalAddress')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('legalAddress')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('postalAddress')">Почтовый адрес</vTitle>
              <div v-if="checkArrowDown('postalAddress')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('postalAddress')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('email')">Электронная почта</vTitle>
              <div v-if="checkArrowDown('email')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('email')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('supervizorName')">Руководитель</vTitle>
              <div v-if="checkArrowDown('supervizorName')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('supervizorName')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('supervizorPosition')">Должность руководителя</vTitle>
              <div v-if="checkArrowDown('supervizorPosition')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('supervizorPosition')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('responsibleTkoPersonName')">ФИО</vTitle>
              <div v-if="checkArrowDown('responsibleTkoPersonName')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('responsibleTkoPersonName')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle"  @click.native.stop="sortField('responsibleTkoPersonPhone')">Телефон</vTitle>
              <div v-if="checkArrowDown('responsibleTkoPersonPhone')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('responsibleTkoPersonPhone')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('clientContact')">Контактный телефон</vTitle>
              <div v-if="checkArrowDown('clientContact')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('clientContact')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">Пользователь</vTitle>
              <span class="resize-handle"></span>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(client, index) in clients" :key="index">
            <td class="table__number table__cell direction--row" @click.stop>
              <input type="radio" :id="'radio_' + index" name="radioSelect" class="radioBtn">
              <label :for="'radio_' + index" @click.stop="chooseClient(client.contractClientId)">
              <div class="typeRadio">
                  <div class="typeRadio__back"></div>
                </div>
              </label>
              <font-awesome-icon icon="fa-solid fa-pen" class="deleteBtn" @click.stop="openContainerEdit(client.contractClientId)"/>
              <font-awesome-icon @click.stop="openConfirmDelete(client.contractClientId)" class="deleteBtn" :icon="['far', 'fa-trash-can']" />
            </td>
            <td><vText class="tableCell">{{client.contractClientId}}</vText></td>
            <td><vText class="tableCell">{{client.clientName}}</vText></td>
            <td><vText class="tableCell">{{client.isValidated}}</vText></td>
            <td><vText class="tableCell">{{client.inn}}</vText></td>
            <td><vText class="tableCell">{{client.kpp}}</vText></td>
            <td><vText class="tableCell">{{client.ogrn}}</vText></td>
            <td><vText class="tableCell">{{client.rschet}}</vText></td>
            <td><vText class="tableCell">{{client.bank}}</vText></td>
            <td><vText class="tableCell">{{client.bik}}</vText></td>
            <td><vText class="tableCell">{{client.kschet}}</vText></td>
            <td><vText class="tableCell">{{client.legalAddress}}</vText></td>
            <td><vText class="tableCell">{{client.postalAddress}}</vText></td>
            <td><vText class="tableCell">{{client.email}}</vText></td>
            <td><vText class="tableCell">{{client.supervizorName}}</vText></td>
            <td><vText class="tableCell">{{client.supervizorPosition}}</vText></td>
            <td><vText class="tableCell">{{client.responsibleTkoPersonName}}</vText></td>
            <td><vText class="tableCell">{{client.responsibleTkoPersonPhone}}</vText></td>
            <td><vText class="tableCell">{{client.clientContact}}</vText></td>
            <td><vText class="tableCell">{{client.createdUserName}}</vText></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="main__load" v-if="clients.length && !loadedAll">
      <div class="main__load__btn">
        <vButton class="greenButton" @click.native="fetchMoreClients">Загрузить еще</vButton>
      </div>
    </div>
    <div v-else-if="!clients.length && !isEmpty">
      <vTitle>Загрузка...</vTitle>
    </div>
    <div v-else-if="!clients.length && isEmpty">
      <vTitle>Реестр пуст</vTitle>
    </div>
    <printContract v-if="showModal" @close="closeModal"/>
    <confirmDelete v-if="showConfirmDelete" @delete="deleteClientById" @close="closeConfirmDelete"></confirmDelete>
    <keepAlive>
      <findClients v-if="showFindClient" @close="closeFindClients"></findClients>
    </keepAlive>
    <toTopButton></toTopButton>
  </div>
</template>

<script>
//Подключение иконок
import { library } from '@fortawesome/fontawesome-svg-core';
import { faTrashCan } from "@fortawesome/free-regular-svg-icons";
import { faPen } from "@fortawesome/free-solid-svg-icons";
library.add(faTrashCan, faPen)

//Подключение самописных компонент
import printContract from "@/components/modals/printContract"; 
import confirmDelete from "@/components/modals/confirmDelete"; 
import findClients from "@/components/modals/findClients"; 
import vButton from "@/components/ui-kit/vButton";
import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import toTopButton from "@/components/ui-kit/toTopButton";

//Подключение модулей
import axios from 'axios';

export default {
  name: "mainView",

  components: {
    printContract,
    confirmDelete,
    findClients,
    vButton,
    vTitle,
    vText,
    toTopButton,
  },

  data() {
    return {
      showModal: false,
      showConfirmDelete: false,
      showFindClient: false,
      loadedAll: false,
      clients: [],
      clientToDelete: null,
      priorityCount: 0,
      choosenClientId: null,

      //Данные для запросов
      isEmpty: false,
      itemsOnPage: 5,
      page: 0,
      isFilterByCreatedUser: true,
      filter: false,
      clientName: '',
      isValidated: null,
      inn: '',
      kpp: '',
      ogrn: '',
      rschet: '',
      bank: '',
      bik: '',
      kschet: '',
      legalAddress: '',
      postalAddress: '',
      email: '',
      supervizorName: '',
      supervizorPosition: '',
      responsibleTkoPersonName: '',
      responsibleTkoPersonPhone: '',
      clientContact: '',
      sorting: {
        id: {
          sortType: 0,
          sortPriority: 0
        },
        clientName: {
          sortType: 0,
          sortPriority: 0
        },
        clientContact: {
          sortType: 0,
          sortPriority: 0
        },
        isValidated: {
          sortType: 0,
          sortPriority: 0
        },
        email: {
          sortType: 0,
          sortPriority: 0
        },
        inn: {
          sortType: 0,
          sortPriority: 0
        },
        kpp: {
          sortType: 0,
          sortPriority: 0
        },
        ogrn: {
          sortType: 0,
          sortPriority: 0
        },
        rschet: {
          sortType: 0,
          sortPriority: 0
        },
        bank: {
          sortType: 0,
          sortPriority: 0
        },
        bik: {
          sortType: 0,
          sortPriority: 0
        },
        kschet: {
          sortType: 0,
          sortPriority: 0
        },
        legalAddress: {
          sortType: 0,
          sortPriority: 0
        },
        postalAddress: {
          sortType: 0,
          sortPriority: 0
        },
        supervizorName: {
          sortType: 0,
          sortPriority: 0
        },
        supervizorPosition: {
          sortType: 0,
          sortPriority: 0
        },
        responsibleTkoPersonName: {
          sortType: 0,
          sortPriority: 0
        },
        responsibleTkoPersonPhone: {
          sortType: 0,
          sortPriority: 0
        }
      },
    }
  },

  mounted() {
    // Our real web app uses Vue.js but I'll stick to plain JavaScript here
    /* eslint-disable */
    const min = 100;
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
    this.$root.$refs.ClientsMainView_component = this
    this.fetchClients()
  },

  methods: {
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
      this.sorting.clientName.sortType = 0
      this.sorting.clientName.sortPriority = 0
      this.sorting.clientContact.sortType = 0
      this.sorting.clientContact.sortPriority = 0
      this.sorting.isValidated.sortType = 0
      this.sorting.isValidated.sortPriority = 0
      this.sorting.email.sortType = 0
      this.sorting.email.sortPriority = 0
      this.sorting.inn.sortType = 0
      this.sorting.inn.sortPriority = 0
      this.sorting.kpp.sortType = 0
      this.sorting.kpp.sortPriority = 0
      this.sorting.ogrn.sortType = 0
      this.sorting.ogrn.sortPriority = 0
      this.sorting.rschet.sortType = 0
      this.sorting.rschet.sortPriority = 0
      this.sorting.bank.sortType = 0
      this.sorting.bank.sortPriority = 0
      this.sorting.bik.sortType = 0
      this.sorting.bik.sortPriority = 0
      this.sorting.kschet.sortType = 0
      this.sorting.kschet.sortPriority = 0
      this.sorting.legalAddress.sortType = 0
      this.sorting.legalAddresssortPriority = 0
      this.sorting.postalAddress.sortType = 0
      this.sorting.postalAddress.sortPriority = 0
      this.sorting.supervizorName.sortType = 0
      this.sorting.supervizorName.sortPriority = 0
      this.sorting.supervizorPosition.sortType = 0
      this.sorting.supervizorPosition.sortPriority = 0
      this.sorting.responsibleTkoPersonName.sortType = 0
      this.sorting.responsibleTkoPersonName.sortPriority = 0
      this.sorting.responsibleTkoPersonPhone.sortType = 0
      this.sorting.responsibleTkoPersonPhone.sortPriority = 0
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
      this.clients = []
      this.page = 0
      this.loadedAll = false
      this.fetchClients()
    },

    closeModal() {
      this.showModal = false
    },

    openModal() {
      this.showModal = true
    },

    openContainerEdit(id) {
      this.$root.$refs.Clients__component.openClientEdit(id)
    },

    chooseClient(id) {
      this.choosenClientId = id
    },

    createContract() {
      this.$root.$refs.AO__component.changeCurrentView({link: 'contracts'}, true, this.choosenClientId)
    },

    showCurrentContracts() {
      this.$root.$refs.AO__component.changeCurrentView({link: 'contracts'}, false, this.choosenClientId)
    },

    openConfirmDelete(id) {
      this.clientToDelete = id
      this.showConfirmDelete = true
      document.body.style.height = "100vh"
    },

    closeConfirmDelete() {
      this.clientToDelete = null
      this.showConfirmDelete = false
      document.body.style.height = "auto"
    },

    openFindClients() {
      this.showFindClient = true
      document.body.style.height = "100vh"
    },

    closeFindClients(bool, params) {
      this.showFindClient = false
      document.body.style.height = "auto"
      if (bool) {
        this.filter = true
        this.clientName = params.clientName
        this.isValidated = params.isValidated
        this.inn = params.inn
        this.kpp = params.kpp
        this.ogrn = params.ogrn
        this.rschet = params.rschet
        this.bank = params.bank
        this.bik = params.bik
        this.kschet = params.kschet
        this.legalAddress = params.legalAddress
        this.postalAddress = params.postalAddress
        this.email = params.email
        this.supervizorName = params.supervizorName
        this.supervizorPosition = params.supervizorPosition
        this.responsibleTkoPersonName = params.responsibleTkoPersonName
        this.responsibleTkoPersonPhone = params.responsibleTkoPersonPhone
        this.clientContact = params.clientContact
        this.clients = []
        this.page = 0
        this.loadedAll = false
        this.fetchClients()
      }
      else {
        if (this.filter) {
          this.filter = false
          this.clientName = params.clientName
          this.isValidated = params.isValidated
          this.inn = params.inn
          this.kpp = params.kpp
          this.ogrn = params.ogrn
          this.rschet = params.rschet
          this.bank = params.bank
          this.bik = params.bik
          this.kschet = params.kschet
          this.legalAddress = params.legalAddress
          this.postalAddress = params.postalAddress
          this.email = params.email
          this.supervizorName = params.supervizorName
          this.supervizorPosition = params.supervizorPosition
          this.responsibleTkoPersonName = params.responsibleTkoPersonName
          this.responsibleTkoPersonPhone = params.responsibleTkoPersonPhone
          this.clientContact = params.clientContact
          this.clients = []
          this.page = 0
          this.loadedAll = false
          this.fetchClients()
        } 
      }
    },

    sort() {
      this.clients = []
      this.page = 0
      this.filter = true
      this.loadedAll = false
      this.fetchClients()
    },

    changeItemsOnPage() {
      this.clients = []
      this.page = 0
      this.loadedAll = false
      this.fetchClients()
    },

    async fetchClients() {
      this.isEmpty = false
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        isFilterByCreatedUser: this.isFilterByCreatedUser,
        page: this.page,
        isFilter: this.filter,
        clientName: this.clientName,
        isValidated: this.isValidated,
        inn: this.inn,
        kpp: this.kpp,
        ogrn: this.ogrn,
        rschet: this.rschet,
        bank: this.bank,
        bik: this.bik,
        kschet: this.kschet,
        legalAddress: this.legalAddress,
        postalAddress: this.postalAddress,
        email: this.email,
        supervizorName: this.supervizorName,
        supervizorPosition: this.supervizorPosition,
        responsibleTkoPersonName: this.responsibleTkoPersonName,
        responsibleTkoPersonPhone: this.responsibleTkoPersonPhone,
        clientContact: this.clientContact,
        sorting: this.sorting
      }
      console.log(data)
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContractClient`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log('first', response.data.data);
        this.page ++
        if (response.data.data.length == 0) {
          this.isEmpty = true
        }
        if (response.data.data.length == this.itemsOnPage) {
          this.clients = response.data.data
        }
        else {
          this.clients = this.clients.concat(response.data.data)
          this.loadedAll = true
        }
      })
      .catch((error) => {
        console.log(error);
        this.isEmpty = true
      })
    },

    async fetchMoreClients() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        isFilterByCreatedUser: this.isFilterByCreatedUser,
        page: this.page,
        isFilter: this.filter,
        clientName: this.clientName,
        isValidated: this.isValidated,
        inn: this.inn,
        kpp: this.kpp,
        ogrn: this.ogrn,
        rschet: this.rschet,
        bank: this.bank,
        bik: this.bik,
        kschet: this.kschet,
        legalAddress: this.legalAddress,
        postalAddress: this.postalAddress,
        email: this.email,
        supervizorName: this.supervizorName,
        supervizorPosition: this.supervizorPosition,
        responsibleTkoPersonName: this.responsibleTkoPersonName,
        responsibleTkoPersonPhone: this.responsibleTkoPersonPhone,
        clientContact: this.clientContact,
        sorting: this.sorting
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredContractClient`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log('second', response.data.data);
        this.page ++
        if (response.data.data.length == this.itemsOnPage) {
          this.clients = this.clients.concat(response.data.data)
        }
        else {
          this.clients = this.clients.concat(response.data.data)
          this.loadedAll = true
        }
      })
      .catch((error) => {
        console.log(error);
      })
    },

    async deleteClientById() {
      this.showConfirmDelete = false
      if (this.clientToDelete != null) {
        let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          contractClientId: this.clientToDelete
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/DeleteContractClient`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response)
          this.clients = []
          this.page = 0
          this.loadedAll = false
          this.fetchClients()
          this.clientToDelete = null
          this.showConfirmDelete = false
          document.body.style.height = "auto"
        })
        .catch((error) => {
          console.log(error);
        })
      }
    },
  },
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

  table {
    min-width: 100vw;
    width: 3000px;
    flex: 1;
    display: grid;
    border-collapse: collapse;
    margin-top: 10px;
    box-shadow: 0px 4px 0px rgba(0, 0, 0, 0.25);

    /* These are just initial values which are overriden using JavaScript when a column is resized */
    grid-template-columns: 
      minmax(100px, 1fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
      minmax(150px, 1.67fr)
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

  tbody tr:hover > td{ 
    background: rgba(0, 0, 0, 0.05);
  }
}
</style>
