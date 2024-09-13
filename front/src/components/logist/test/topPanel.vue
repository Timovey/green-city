<template>
  <div class="panel">
    <div class="panel__left">
      <div class="panel__left__title">
        <vTitle class="pageTitle">{{ title }}</vTitle>
        <vTitle v-if="subTitle.length" class="pageTitle">{{ subTitle }}</vTitle>
      </div>
    </div>
    <div class="panel__right">
      <div class="panel__right__btns">
        <div class="panel__right__btns__saveBtn" @click="closeEditView()">
          <vButton class="greenButton">Сохранить</vButton>
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
  }

  &__right {

    &__btns {
      display: flex;
      align-items: center;
      justify-content: space-between;

      &__saveBtn {
        width: 90px;
        height: 30px;
        margin-right: 20px;
      }
    }
  }
}
</style>
