<template>
  <div class="main">
    <div class="main__topBtn flex-end" v-show="containerYards.length">
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__btn">
          <input type="number" v-model="itemsOnPage" @keyup.enter="changeItemsOnPage"/>
        </div>
      </div>
    </div>
    <div class="main__topBtn flex-end" v-show="containerYards.length">
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
      <table v-show="containerYards.length">
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
              <vTitle class="tableCellTitle" @click.native.stop="sortField('address')">Адрес</vTitle>
              <div v-if="checkArrowDown('address')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('address')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('isHighSecurityObject')">Режимный объект</vTitle>
              <div v-if="checkArrowDown('isHighSecurityObject')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('isHighSecurityObject')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('isValidated')">Запись корректна</vTitle>
              <div v-if="checkArrowDown('isValidated')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('isValidated')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('haveBase')">Наличие основания</vTitle>
              <div v-if="checkArrowDown('haveBase')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('haveBase')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('haveRoof')">Наличие крыши</vTitle>
              <div v-if="checkArrowDown('haveRoof')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('haveRoof')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('haveFence')">Наличие ограждения</vTitle>
              <div v-if="checkArrowDown('haveFence')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('haveFence')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('driveWay')">Путь подъезда</vTitle>
              <div v-if="checkArrowDown('driveWay')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('driveWay')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('contacts')">Контакты</vTitle>
              <div v-if="checkArrowDown('contacts')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('contacts')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('standType')">Тип площадки</vTitle>
              <div v-if="checkArrowDown('standType')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('standType')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">Пользователь</vTitle>
              <span class="resize-handle"></span>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(containerYard, index) in containerYards" :key="index">
            <td class="table__number table__cell direction--row" @click.stop>
              <font-awesome-icon icon="fa-solid fa-pen" class="deleteBtn" @click.stop="openContainerYardEdit(containerYard.standId)"/>
              <font-awesome-icon @click.stop="openConfirmDelete(containerYard.standId)" class="deleteBtn" :icon="['far', 'fa-trash-can']" />
            </td>
            <td><vText class="tableCell">{{containerYard.standId}}</vText></td>
            <td><vText class="tableCell">{{containerYard.address}}</vText></td>
            <td><vText class="tableCell">{{containerYard.isHighSecurityObject}}</vText></td>
            <td><vText class="tableCell">{{containerYard.isValidated}}</vText></td>
            <td><vText class="tableCell">{{containerYard.haveBase}}</vText></td>
            <td><vText class="tableCell">{{containerYard.haveRoof}}</vText></td>
            <td><vText class="tableCell">{{containerYard.haveFence}}</vText></td>
            <td><vText class="tableCell">{{containerYard.driveWay}}</vText></td>
            <td><vText class="tableCell">{{containerYard.contacts}}</vText></td>
            <td><vText class="tableCell">{{containerYard.standTypeName}}</vText></td>
            <td><vText class="tableCell">{{containerYard.createdUserName}}</vText></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="main__load" v-if="containerYards.length && !loadedAll">
      <div class="main__load__btn">
        <vButton class="greenButton" @click.native="fetchMoreContainerYards">Загрузить еще</vButton>
      </div>
    </div>
    <div v-else-if="!containerYards.length && !isEmpty">
      <vTitle>Загрузка...</vTitle>
    </div>
    <div v-else-if="!containerYards.length && isEmpty">
      <vTitle>Реестр пуст</vTitle>
    </div>
    <confirmDelete v-if="showConfirmDelete" @delete="deleteContainerYardById" @close="closeConfirmDelete"></confirmDelete>
    <keepAlive>
      <findContainerYards v-if="showFindContainerYards" @close="closeFindContainerYards"></findContainerYards>
    </keepAlive>
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

import confirmDelete from "@/components/modals/confirmDelete";
import findContainerYards from "@/components/modals/findContainerYards"; 

export default {
  name: "mainView",

  components: {
    confirmDelete,
    vTitle,
    vText,
    vButton,
    toTopButton,
    findContainerYards,
  },

  data() {
    return {
      isEmpty: false,
      showConfirmDelete: false,
      showFindContainerYards: false,
      containerToDelete: null,
      itemsOnPage: 5,
      page: 0,
      isFilterByCreatedUser: true,
      filter: false,
      loadedAll: false,
      containerYards: [],
      address: '',
      isHighSecurityObject: null,
      isValidated: null,
      haveBase: null,
      haveRoof: null,
      haveFence: null,
      driveWay: '',
      contacts: '',
      standType: null,
      sorting: {
        id: {
          sortType: 0,
          sortPriority: 0
        },
        isHighSecurityObject: {
          sortType: 0,
          sortPriority: 0
        },
        isValidated: {
          sortType: 0,
          sortPriority: 0
        },
        haveFence: {
          sortType: 0,
          sortPriority: 0
        },
        haveBase: {
          sortType: 0,
          sortPriority: 0
        },
        haveRoof: {
          sortType: 0,
          sortPriority: 0
        },
        address: {
          sortType: 0,
          sortPriority: 0
        },
        driveWay: {
          sortType: 0,
          sortPriority: 0
        },
        contacts: {
          sortType: 0,
          sortPriority: 0
        },
        standType: {
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
    this.$root.$refs.ContainerYardsMainView__component = this
    this.fetchContainerYards()
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
      this.sorting.id.sortType = 0
      this.sorting.id.sortPriority = 0
      this.sorting.isHighSecurityObject.sortType = 0
      this.sorting.isHighSecurityObject.sortPriority = 0
      this.sorting.isValidated.sortType = 0
      this.sorting.isValidated.sortPriority = 0
      this.sorting.address.sortType = 0
      this.sorting.address.sortPriority = 0
      this.sorting.haveBase.sortType = 0
      this.sorting.haveBase.sortPriority = 0
      this.sorting.haveRoof.sortType = 0
      this.sorting.haveRoof.sortPriority = 0
      this.sorting.haveFence.sortType = 0
      this.sorting.haveFence.sortPriority = 0
      this.sorting.driveWay.sortType = 0
      this.sorting.driveWay.sortPriority = 0
      this.sorting.contacts.sortType = 0
      this.sorting.contacts.sortPriority = 0
      this.sorting.standType.sortType = 0
      this.sorting.standType.sortPriority = 0
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
      this.containerYards = []
      this.page = 0
      this.loadedAll = false
      this.fetchContainerYards()
    },

    sort() {
      this.containerYards = []
      this.page = 0
      this.loadedAll = false
      this.filter = true
      console.log(this.sorting)
      this.fetchContainerYards()
    },

    changeItemsOnPage() {
      this.containerYards = []
      this.page = 0
      this.loadedAll = false
      this.fetchContainerYards()
    },

    openContainerYardEdit(id) {
      this.$root.$refs.ContainerYards__component.openContainerYardEdit(id)
    },

    openFindContainerYards() {
      this.showFindContainerYards = true
      document.body.style.height = "100vh"
    },

    openConfirmDelete(id) {
      this.containerYardToDelete = id
      this.showConfirmDelete = true
      document.body.style.height = "100vh"
    },

    closeConfirmDelete() {
      this.containerYardToDelete = null
      this.showConfirmDelete = false
      document.body.style.height = "auto"
    },

    closeFindContainerYards(bool, params) {
      console.log(params)
      this.showFindContainerYards = false
      document.body.style.height = "auto"
      if (bool) {
        this.filter = true
        this.address = params.address
        this.isHighSecurityObject = params.isHighSecurityObject
        this.isValidated = params.isValidated
        this.haveBase = params.haveBase
        this.haveRoof = params.haveRoof
        this.haveFence = params.haveFence
        this.driveWay = params.driveWay
        this.contacts = params.contacts
        this.standType = params.choosenStandTypeId
        this.containerYards = []
        this.page = 0
        this.loadedAll = false
        this.fetchContainerYards()
      }
      else {
        if (this.filter) {
          this.filter = false
          this.address =''
          this.isHighSecurityObject = false
          this.isValidated = false
          this.haveBase = false
          this.haveRoof = false
          this.haveFence = false
          this.driveWay = ''
          this.contacts = ''
          this.standType = null
          this.containerYards = []
          this.page = 0
          this.loadedAll = false
          this.fetchContainerYards()
        }
      }
    },

    async fetchContainerYards() {
      this.isEmpty = false
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: this.filter,
        isFilterByCreatedUser: this.isFilterByCreatedUser,
        isHighSecurityObject: this.isHighSecurityObject,
        isValidated: this.isValidated,
        haveBase: this.haveBase,
        haveRoof: this.haveRoof,
        haveFence: this.haveFence,
        address: this.address,
        driveWay: this.driveWay,
        contacts: this.contacts,
        standTypes: this.standTypes,
        containerIds: this.containerIds,
        sorting: this.sorting
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetFilteredStand`, data, { headers: {
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
          this.containerYards = response.data.data
        }
        else {
          this.containerYards = this.containerYards.concat(response.data.data)
          this.loadedAll = true
        }
      })
      .catch((error) => {
        console.log(error);
        this.isEmpty = true
      })
    },

    async fetchMoreContainerYards() {
      let data = {
        userId: localStorage.getItem('UserId'),
        userName: localStorage.getItem('UserName'),
        itemsOnPage: this.itemsOnPage,
        page: this.page,
        isFilter: this.filter,
        isFilterByCreatedUser: this.isFilterByCreatedUser,
        isHighSecurityObject: this.isHighSecurityObject,
        isValidated: this.isValidated,
        haveBase: this.haveBase,
        haveRoof: this.haveRoof,
        haveFence: this.haveFence,
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
        console.log('second', response.data.data);
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

    async deleteContainerYardById() {
      this.showConfirmDelete = false
      if (this.containerYardToDelete != null) {
        let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          standIds: [this.containerYardToDelete]
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/DeleteStand`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response)
          this.containerYards = []
          this.page = 0
          this.loadedAll = false
          this.fetchContainerYards()
          this.containerYardToDelete = null
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
      minmax(75px, 0.3fr)
      minmax(75px, 0.3fr)
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

  tbody tr:hover > td { 
    background: rgba(0, 0, 0, 0.05);
  }
}
</style>
