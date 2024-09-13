<template>
  <div class="modal">
    <div class="modal__container" v-click-outside="close">
      <div class="modal__container__header">
        <vTitle class="printFormTitle">Способы добавления контейнера</vTitle>
      </div>
      <div class="modal__container__content">
        <div class="modal__container__content__btn">
          <vButton class="whiteToGreenButton" @click.native.stop="show">Из реестра</vButton>
        </div>
        <div class="modal__container__content__btn">
          <vButton class="whiteToGreenButton" @click.native.stop="create">Новый</vButton>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'
import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

export default {
  name: "containerSelectionMethod",

  components: {
    vTitle,
    vButton,
  },

  methods: {
    close() {
      this.$emit('close')
    },

    create() {
      this.$emit('create')
    },

    show() {
      this.$emit('show')
    }
  },

  directives: {
    ClickOutside
  }
}
</script>

<style lang="scss" scoped>
.modal {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.2);
  display: flex;
  align-items: center;
  justify-content: center;

  &__container {
    width: 800px;
    height: fit-content;
    background: #FFF;
    border: 1px solid #000;
    display: flex;
    flex-direction: column;

    &__header {
      background: rgba(37, 72, 15, 0.58);
    }

    &__content {
      display: flex;
      justify-content: space-around;
      align-items: center;
      padding: 50px 40px;

      &__btn {
        width: 30%;
        display: flex;
        justify-content: center;
        align-items: center;
      }
    }
  }
}
</style>
