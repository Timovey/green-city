<template>
  <div class="main">
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
        <div class="table__pickup table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__pickup table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__pickup table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__pickup table__cell">
          <vText class="tableCell">-</vText>
        </div>
        <div class="table__pickup table__cell">
          <vText class="tableCell">-</vText>
        </div>
      </div>
    </div>
    <div class="main__bottomBtn">
      <div class="main__bottomBtn__container">
        <vButton class="greenButton">Редактировать</vButton>
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

  &__bottomBtn {
    display: flex;
    justify-content: flex-end;
    margin-top: 10px;

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
        width: calc(100% / 12);
      }

      .header {
        background: rgba(0, 0, 0, 0.05);
      }
    }
  }
}
</style>
