<template>
  <div class="main">
    <div class="main__table">
      <div class="main__table__header">
        <div class="table__cell header">
          <vTitle class="tableCellTitle">№</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Договор</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">Реквизиты договора</vTitle>
        </div>
        <div class="table__cell header">
          <vTitle class="tableCellTitle">...</vTitle>
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
      </div>
    </div>
    <div class="main__btn">
      <div class="main__btn__container">
        <vButton class="greenButton">Согласовать</vButton>
      </div>
    </div>
  </div>
</template>

<script>
import vTitle from "@/components/ui-kit/vTitle";
import vText from "@/components/ui-kit/vText";
import vButton from "@/components/ui-kit/vButton";

export default {
  name: "mainView",

  components: {
    vTitle,
    vText,
    vButton,
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
        width: calc(100% / 4);
      }

      .header {
        background: rgba(0, 0, 0, 0.05);
      }
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
}
</style>
