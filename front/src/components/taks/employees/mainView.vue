<template>
  <div class="main">
    <div class="main__topBtn">
      <div class="main__topBtn__container">
        <vButton class="greenButton">Объединить</vButton>
      </div>
    </div>
    <div class="main__table">
      <div class="main__table__header">
        <div class="table__cell header">
          <vTitle class="tableCellTitle">№</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Таб №</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">ФИО</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Должность</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Разряд</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Статус</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Организация</vTitle>
        </div>
      </div>
      <div class="main__table__header" v-for="item in 3" :key="item">
        <div class="table__number table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__regNumber table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__vehicleName table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__gibddNumber table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__color table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__type table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__pickup table__cell">
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
        width: calc(100% / 7);
      }

      .header {
        background: rgba(0, 0, 0, 0.05);
      }
    }
  }
}
</style>
