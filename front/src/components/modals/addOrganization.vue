<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <div class="cross" @click="close()"></div>
      </div>
      <div class="modal__container__content">
        <div class="row">
          <vTitle class="fieldTitle">Название организации</vTitle>
          <input type="text" name="" />
        </div>
        <div class="row">
          <vTitle class="fieldTitle">Адрес</vTitle>
          <input type="text" name="" />
        </div>
        <div class="modal__container__content__saveBtn">
          <div class="saveBtn">
            <vButton class="greenButton">Добавить</vButton>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import vTitle from "@/components/ui-kit/vTitle";
import vButton from "@/components/ui-kit/vButton";

export default {
  name: "addOrganization",

  components: {
    vTitle,
    vButton,
  },

  methods: {
    close() {
      this.$emit('close')
    }
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
    width: 600px;
    height: fit-content;
    background: #FFF;
    border: 1px solid #000;
    padding: 20px 10px 60px 10px;

    &__close {
      display: flex;
      justify-content: flex-end;
      align-items: center;

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

    &__content {

      .row {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin: 30px 0;

        input {
          border: 1px solid #000;
          width: 100%;
          height: 30px;
          margin-left: 20px;
          padding: 10px 5px;
          font-family: 'Lato', sans-serif;
          font-size: 18px;
          line-height: 22px;
          font-weight: 400;
          color: #000;
          border-radius: 4px;

          @media screen and (max-width: 1440px) {
            height: 20px;
            font-size: 12px;
            line-height: 16px;
          }
        }
      }

      &__saveBtn {  
        display: flex;
        justify-content: flex-end;
        align-items: center;

        .saveBtn {
          width: 90px;
          height: 30px;
        }
      }
    }
  }
}
</style>
