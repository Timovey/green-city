<template>
  <div class="main">
    <div class="main__topBtn">
      <div class="main__topBtn__container">
        <vButton class="greenButton">Изменить</vButton>
      </div>
    </div>
    <div class="main__table">
      <div class="main__table__header">
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
          <vTitle class="tableCellTitle">Тип ТС</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">График</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Организация</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Количество</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Сотрудник 1</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Сотрудник 2</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Сотрудник 3</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Сотрудник 4</vTitle>
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
      </div>
    </div>
  </div>
</template>

<script>
import vButton from "@/components/ui-kit/vButton";
import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";

export default {
  name: "mainView",

  components: {
    vButton,
    vTitle,
    vText,
  },

  data() {
    return {
      
    }
  },
}
</script>

<style lang="scss" scoped>
.main {
  padding-top: 15px;

  &__topBtn {
    display: flex;
    justify-content: flex-end;

    &__container {
      width: 90px;
      height: 30px;

      &:first-child {
        margin-right: 10px;
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
        width: calc(100% / 11);
      }

      .header {
        background: rgba(0, 0, 0, 0.05);
      }
    }
  }
}
</style>
