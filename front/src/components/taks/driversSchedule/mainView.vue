<template>
  <div class="driversSchedule">
    <div class="driversSchedule__topBtn">
      <div class="driversSchedule__topBtn__left">
        <div class="driversSchedule__topBtn__left__container">
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
        <div class="driversSchedule__topBtn__left__container">
          <vButton class="greenButton">Сформировать</vButton>
        </div>
      </div>
      <div class="driversSchedule__topBtn__right">
        <div class="driversSchedule__topBtn__right__container">
          <vButton class="greenButton">Печать</vButton>
        </div>
      </div>
      <!-- <div class="main__topBtn__container">
        <vButton class="greenButton">Дежурная смена</vButton>
      </div>
      <div class="main__topBtn__container">
        <vButton class="greenButton">Печать</vButton>
      </div> -->
    </div>
    <div class="driversSchedule__table">
      <div class="driversSchedule__table__header">
        <div class="table__cell header">
          <vTitle class="tableCellTitle">№</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">ТС</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Рег ТС</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Водитель</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">5</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">6</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">7</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">8</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">9</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">10</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">11</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">12</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">13</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">14</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">15</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">16</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">17</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">18</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">19</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">20</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">21</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">22</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">23</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">24</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">25</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">26</vTitle>
        </div>
      </div>
      <div class="driversSchedule__table__header" v-for="item in 3" :key="item">
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__cell">
          <vText class="tableCell">-</vText>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import vButton from "@/components/ui-kit/vButton";
import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";

export default {
  name: "mainView",

  components: {
    DatePicker,
    vButton,
    vTitle,
    vText,
  },

  data() {
    return {
      graphFrom: '',
      graphTo: '',
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'DD-MM-YYYY',
    }
  },
}
</script>

<style lang="scss">
.driversSchedule {
  padding-top: 15px;

  &__topBtn {
    display: flex;
    justify-content: space-between;

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

      &__container {
        width: 90px;
        height: 30px;

        &:first-child {
          margin-right: 10px;
        }
      }
    }
  }

  &__table {
    margin-top: 10px;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);

    &__header {
      display: flex;
      width: 100%;
      justify-content: space-between;
      align-items: stretch;

      .table__cell {
        word-wrap: break-word;
        padding: 8px 10px;
        border: 1px solid #A5C081;
        width: calc(100% / 26);
      }

      .header {
        background: rgba(0, 0, 0, 0.05);
      }
    }
  }
}
</style>
