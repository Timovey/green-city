<template>
  <div class="main">
    <div class="main__topBtn" v-show="vehicles.length">
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__btn" v-if="choosenVehicleId">
          <vButton class="greenButton">Ремонт/ТО</vButton>
        </div>
        <div class="main__topBtn__container__btn" v-if="choosenVehicleId">
          <vButton class="greenButton">Смена</vButton>
        </div>
      </div>
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__btn">
          <input type="number" v-model="itemsOnPage" @keyup.enter="changeItemsOnPage"/>
        </div>
      </div>
    </div>
    <div class="main__topBtn flex-end" v-show="vehicles.length">
      <div class="main__topBtn__container">
        <div class="main__topBtn__container__sortbtn">
          <vButton class="greenButton" @click.native.stop="sort">Сортировка</vButton>
        </div>
        <div class="main__topBtn__container__sortbtn">
          <vButton class="greenButton" @click.native.stop="resetSort">Сброс сортировки</vButton>
        </div>
      </div>
    </div>
    <div class="main__topBtn flex-end" v-show="vehicles.length">
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
      <table v-show="vehicles.length">
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
              <vTitle class="tableCellTitle" @click.native.stop="sortField('regNumber')">Рег №</vTitle>
              <div v-if="checkArrowDown('regNumber')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('regNumber')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('name')">Название ТС</vTitle>
              <div v-if="checkArrowDown('name')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('name')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('bodyColor')">Цвет</vTitle>
              <div v-if="checkArrowDown('bodyColor')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('bodyColor')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('transportType')">Тип ТС</vTitle>  
              <div v-if="checkArrowDown('transportType')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('transportType')" class="tableCellArrowDown"></div>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('tankFence')">Забор</vTitle>
              <div v-if="checkArrowDown('tankFence')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('tankFence')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('cargoVolume')">Объем</vTitle>
              <div v-if="checkArrowDown('cargoVolume')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('cargoVolume')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle" @click.native.stop="sortField('organizationId')">Организация</vTitle>
              <div v-if="checkArrowDown('organizationId')" class="tableCellArrowUp"></div>
              <div v-else-if="checkArrowUp('organizationId')" class="tableCellArrowDown"></div>  
              <span class="resize-handle"></span>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(vehicle, index) in vehicles" :key="index">
            <td class="table__number table__cell direction--row" @click.stop>
              <input type="radio" :id="'radio_' + index" name="radioSelect" class="radioBtn">
              <label :for="'radio_' + index" @click.stop="chooseVehicle(vehicle.id)">
              <div class="typeRadio">
                  <div class="typeRadio__back"></div>
                </div>
              </label>
              <font-awesome-icon icon="fa-solid fa-pen" class="deleteBtn" @click.stop="openContainerEdit(vehicle.contractClientId)"/>
              <font-awesome-icon @click.stop="openConfirmDelete(vehicle.contractClientId)" class="deleteBtn" :icon="['far', 'fa-trash-can']" />
            </td>
            <td><vText class="tableCell">{{vehicle.id}}</vText></td>
            <td><vText class="tableCell">{{vehicle.regNumber}}</vText></td>
            <td><vText class="tableCell">{{vehicle.name}}</vText></td>
            <td><vText class="tableCell">{{vehicle.bodyColor}}</vText></td>
            <td><vText class="tableCell">{{vehicle.transportType}}</vText></td>
            <td><vText class="tableCell">{{vehicle.tankFencetankFence}}</vText></td>
            <td><vText class="tableCell">{{vehicle.cargoVolume}}</vText></td>
            <td><vText class="tableCell">{{vehicle.organizationId}}</vText></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="main__load" v-if="vehicles.length && !loadedAll">
      <div class="main__load__btn">
        <vButton class="greenButton" @click.native="fetchMoreVehicles">Загрузить еще</vButton>
      </div>
    </div>
    <div v-else-if="!vehicles.length && !isEmpty">
      <vTitle>Загрузка...</vTitle>
    </div>
    <div v-else-if="!vehicles.length && isEmpty">
      <vTitle>Реестр пуст</vTitle>
    </div>
    <searchVehicles v-if="showModal" @close="closeModal"/>
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
import vButton from "@/components/ui-kit/vButton";
import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import searchVehicles from "@/components/modals/searchVehicles";
import toTopButton from "@/components/ui-kit/toTopButton";

//Подключение модулей
// import axios from 'axios';

export default {
  name: "mainView",

  components: {
    vButton,
    vTitle,
    vText,
    searchVehicles,
    toTopButton,
  },

  data() {
    return {
      showModal: false,
      showConfirmDelete: false,
      showFindVehicle: false,
      loadedAll: false,
      vehicles: [
        {
          id: 1,
          regNumber: '123',
          name: '123',
          bodyColor: '123',
          transportType: '123',
          tankFencetankFence: '123',
          cargoVolume: '123',
          organizationId: '123',
        }
      ],
      vehicleToDelete: null,
      priorityCount: 0,
      choosenVehicleId: null,

      //Данные для запросов
      isEmpty: false,
      itemsOnPage: 5,
      page: 0,
      isFilterByCreatedUser: true,
      filter: false,
      name: '',
      organizationId: null,
      regNumber: '',
      transportType: '',
      bodyColor: '',
      tankFence: '',
      cargoVolume: null,
      sorting: {
        id: {
          sortType: 0,
          sortPriority: 0
        },
        name: {
          sortType: 0,
          sortPriority: 0
        },
        organizationId: {
          sortType: 0,
          sortPriority: 0
        },
        regNumber: {
          sortType: 0,
          sortPriority: 0
        },
        transportType: {
          sortType: 0,
          sortPriority: 0
        },
        bodyColor: {
          sortType: 0,
          sortPriority: 0
        },
        tankFence: {
          sortType: 0,
          sortPriority: 0
        },
        cargoVolume: {
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
    this.$root.$refs.VehiclesMain_component = this

    // this.fetchVehicles()
  },

  methods: {
    closeModal() {
      this.showModal = false
    },

    chooseVehicle(id) {
      this.choosenVehicleId = id
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

    // async fetchVehicles() {
    //   return await axios.post('/api/Transports/GetFilteredTransport', { headers: {'Authorization': localStorage.getItem('Token')}})
    //   .then(( response ) => {
    //     console.log(response.data.data);
    //     this.vehicles = response.data.data
    //   })
    //   .catch((error) => {
    //     console.log(error);
    //   })
    // }
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

  table {
    min-width: 100vw;
    width: 2000px;
    flex: 1;
    display: grid;
    border-collapse: collapse;
    margin-top: 10px;
    box-shadow: 0px 4px 0px rgba(0, 0, 0, 0.25);

    /* These are just initial values which are overriden using JavaScript when a column is resized */
    grid-template-columns: 
      minmax(95px, 0.5fr)
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
