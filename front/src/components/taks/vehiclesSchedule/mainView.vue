<template>
  <div class="vehiclesSchedule">
    <div class="vehiclesSchedule__topBtn">
      <div class="vehiclesSchedule__topBtn__left">
        <div class="vehiclesSchedule__topBtn__left__container">
          <vText class="graphFormation">Сформировать график с </vText>
          <date-picker 
            v-model="graphFrom" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePicker"
          >
          </date-picker>
          <vText class="graphFormation">по </vText>
          <date-picker 
            v-model="graphTo" 
            :lang="lang" 
            :format="format"
            valueType="format"
            input-class="datePickerInput"
            class="datePicker"
          >
          </date-picker>
        </div>
        <div class="vehiclesSchedule__topBtn__left__container">
          <vButton class="greenButton">Сформировать</vButton>
        </div>
      </div>
      <div class="vehiclesSchedule__topBtn__right">
        <div class="vehiclesSchedule__topBtn__right__btn">
          <input type="number" v-model="itemsOnPage" @keyup.enter="changeItemsOnPage"/>
        </div>
        <!-- <div class="vehiclesSchedule__topBtn__right__container">
          <vButton class="greenButton">Дежурная смена</vButton>
        </div>
        <div class="vehiclesSchedule__topBtn__right__container">
          <vButton class="greenButton">Печать</vButton>
         </div>-->
      </div>
      <!-- <div class="main__topBtn__container">
        <vButton class="greenButton">Дежурная смена</vButton>
      </div>
      <div class="main__topBtn__container">
        <vButton class="greenButton">Печать</vButton>
      </div> -->
    </div>
    <div class="overflow--scroll">
      <table v-show="true">
        <thead>
          <tr>
            <th data-type="numeric">
              <vTitle class="tableCellTitle">№</vTitle>
              <span class="resize-handle"></span>  
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">ТС</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">Рег ТС</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">Смена</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">5</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">6</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">7</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">8</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">9</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">10</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">11</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">12</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">13</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">14</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">15</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">16</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">17</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">18</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">19</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">20</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">21</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">22</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">23</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">24</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">25</vTitle>
              <span class="resize-handle"></span>
            </th>
            <th data-type="text-short">
              <vTitle class="tableCellTitle">26</vTitle>
              <span class="resize-handle"></span>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
            <td><vText class="tableCell">-</vText></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="main__load" v-if="vehiclesSchedules.length && !loadedAll">
      <div class="main__load__btn">
        <vButton class="greenButton" @click.native="fetchMoreVehiclesShedules">Загрузить еще</vButton>
      </div>
    </div>
    <div v-else-if="!vehiclesSchedules.length && !isEmpty">
      <vTitle>Загрузка...</vTitle>
    </div>
    <div v-else-if="!vehiclesSchedules.length && isEmpty">
      <vTitle>Реестр пуст</vTitle>
    </div>
    <toTopButton></toTopButton>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import vButton from "@/components/ui-kit/vButton";
import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import toTopButton from "@/components/ui-kit/toTopButton";

export default {
  name: "mainView",

  components: {
    DatePicker,
    vButton,
    vTitle,
    vText,
    toTopButton,
  },

  data() {
    return {
      graphFrom: '',
      graphTo: '',
      loadedAll: false,
      itemsOnPage: 5,
      isEmpty: false,
      page: 0,
      vehiclesSchedules: [],
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'DD-MM-YYYY',
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
}
</script>

<style lang="scss">
.overflow--scroll {
  overflow-x: scroll;
}

.vehiclesSchedule {
  padding-top: 15px;

  &__topBtn {
    display: flex;
    justify-content: space-between;
    margin: 20px 0 30px 0;

    &__left {
      display: flex;
      align-items: center;

      &__container {
        height: 30px;
        display: flex;
        align-items: center;

        &:first-child {
          margin-right: 10px;
        }

        .datePicker {
          margin: 0 10px;
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

    &__right {
      display: flex;
      align-items: center;

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

      &__container {
        width: 90px;
        height: 30px;

        &:first-child {
          margin-right: 10px;
        }
      }
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
      minmax(95px, 0.5fr)
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
