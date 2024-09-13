<template>
  <div class="main">
    <div class="main__table">
      <div class="main__table__header">
        <div class="table__cell header">
          <vTitle class="tableCellTitle">№</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Рег №</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">КП</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">КА</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Часы вывоза</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Ч...</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Объем</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Время забора</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">О...</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Конт-ы</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Период-ть</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Ко...</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Объем роста</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Время</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Масса</vTitle>
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
        <div class="table__regNumber table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__vehicleName table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__gibddNumber table__cell">
          <vText class="tableCell">-</vText>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
// import vButton from "@/components/ui-kit/vButton";

export default {
  name: "mainView",

  components: {
    vTitle,
    vText,
    // vButton,
  },
}
</script>

<style lang="scss" scoped>
.main {
  padding-top: 15px;

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
        width: calc(100% / 15);
      }

      .header {
        background: rgba(0, 0, 0, 0.05);
      }
    }
  }
}
</style>
