<template>
  <div class="panel">
    <div class="panel__left">
      <div class="panel__left__title">
        <vTitle class="pageTitle">{{ title }}</vTitle>
        <vTitle v-if="subTitle.length" class="pageTitle">{{ subTitle }}</vTitle>
      </div>
      <div 
        class="panel__left__add"
        v-if="!hiddenPlus"
      >
      </div>
    </div>
    <div class="panel__right">
      <div class="panel__right__btns" v-if="!hiddenInput">
        <div class="panel__right__btns__saveBtn">
          <vButton class="greenButton">Печать</vButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

export default {
  name: "topPanel",

  components: {
    vTitle, 
    vButton,
  },

  props: {
    title: {
      type: String,
      default: ''
    },
    subTitle: {
      type: String,
      default: '',
    },
    hiddenInput: {
      type: Boolean,
      default: false,
    },
    hiddenPlus: {
      type: Boolean,
      default: false,
    }
  },
}
</script>

<style lang="scss" scoped>
.panel {
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-bottom: 1px solid rgba(0, 0, 0, 0.24);
  padding-bottom: 20px;

  &__left {
    display: flex;
    align-items: center;

    &__add {
      margin-left: 10px;
      width: 30px;
      height: 30px;
      background: rgba(37, 72, 15, 0.58);
      border-radius: 15px;
      position: relative;
      cursor: pointer;

      &::after {
        content: '+';
        position: absolute;
        top: 0;
        left: 1.4px;
        color: #FFF;
        font-family: 'Lato', sans-serif;
        font-weight: 700;
        font-size: 48px;
        line-height: 28px;
      }
    }
  }

  &__right {

    &__btns {
      display: flex;
      align-items: center;
      justify-content: space-between;

      &__saveBtn {
        width: 90px;
        height: 30px;
      }
    }
  }
}
</style>
