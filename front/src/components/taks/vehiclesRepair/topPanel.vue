<template>
  <div class="panel">
    <div class="panel__left">
      <div class="panel__left__title">
        <vTitle class="pageTitle">{{ title }}</vTitle>
        <vTitle v-if="subTitle.length" class="pageTitle">{{ subTitle }}</vTitle>
      </div>
      <div 
        class="panel__left__add" 
        @click="changeCurrentView()"
        v-if="!hiddenPlus"
      >
      </div>
    </div>
    <div class="panel__right">
      <div class="panel__right__searchInput" v-if="!hiddenInput">
        <div class="panel__right__btns__saveBtn" @click="openSearch()">
          <vButton class="greenButton">Поиск</vButton>
        </div>
      </div>
      <div class="panel__right__btns" v-else>
        <div class="panel__right__btns__saveBtn" @click="closeEditView()">
          <vButton class="greenButton">Сохранить</vButton>
        </div>
        <div class="panel__right__btns__close">
          <div class="cross" @click="closeEditView()"></div>
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
      default: '',
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

  methods: {
    changeCurrentView() {
      this.$root.$refs.VehiclesRepair__component.currentView = 'edit'
    },

    closeEditView() {
      this.$root.$refs.VehiclesRepair__component.currentView = 'main'
    }
  }
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
      width: 150px;
      justify-content: space-between;

      &__saveBtn {
        width: 90px;
        height: 30px;
      }

      &__close {
        
        .cross {
          width: 30px;
          height: 30px;
          position: relative;
          border-radius: 15px;
          border: 1px solid #000;
          cursor: pointer;

          &:after, &:before {
            content: '';
            width: 2px;
            height: 20px;
            position: absolute;
            top: 4px;
            left: 13px;
            background: #000;
          }

          &:after {
            transform: rotate(45deg);
          }

          &:before {
            transform: rotate(-45deg);
          }
        }
      }
    }

    &__searchBtn {
      display: flex;
      justify-content: flex-end;
      align-items: center;
      width: 200px;
      height: 32px;
    }
  }
}
</style>
