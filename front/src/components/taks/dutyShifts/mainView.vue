<template>
  <div class="main">
    <div class="main__topBtn">
      <div class="main__topBtn__left">
        <div class="main__topBtn__left__container">
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
        <div class="main__topBtn__left__container">
          <vButton class="greenButton">Сформировать</vButton>
        </div>
      </div>
      <div class="main__topBtn__right">
        <div class="main__topBtn__right__container">
          <vButton class="greenButton">Дежурная смена</vButton>
        </div>
        <div class="main__topBtn__right__container">
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
    <div class="main__table">
      <div class="main__table__header">
        <div class="table__cell header">
          <vTitle class="tableCellTitle">№</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">№ при...</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">ТС</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Рег ТС</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Организация</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Вид работ</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Начало</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Плановое окончание</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Фактическое окончание</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Архив</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Замена</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Период</vTitle>
        </div>
      </div>
      <div class="main__table__header" v-for="item in 3" :key="item">
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
.main {
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
        padding: 8px 10px;
        border: 1px solid #A5C081;
        width: calc(100% / 12);
      }

      .header {
        background: rgba(0, 0, 0, 0.05);
      }
    }
  }
}
</style>
