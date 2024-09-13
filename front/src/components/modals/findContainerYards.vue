<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Поиск контейнерных площадок</vTitle>
        <div class="cross" @click="reset()"></div>
      </div>
      <div class="clientsEdit__container">
        <div class="row rowObject">
          <vTitle class="pageTitle">Режимный объект</vTitle>
          <input type="checkbox" id="object" @click="params.isHighSecurityObject = !params.isHighSecurityObject" :checked="params.isHighSecurityObject"/>
          <label for="object">
            <div class="objectCheck"></div>
          </label>
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Запись корректна</vTitle>
          <input type="checkbox" id="valid" @click="params.isValidated = !params.isValidated" :checked="params.isValidated"/>
          <label for="valid">
            <div class="validCheck"></div>
          </label>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Адрес</vTitle>
          <input type="text" name="" v-model="params.address" />
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Наличие основания</vTitle>
          <input type="checkbox" id="base" @click="params.haveBase = !params.haveBase" :checked="params.haveBase"/>
          <label for="base">
            <div class="baseCheck"></div>
          </label>
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Наличие крыши</vTitle>
          <input type="checkbox" id="roof" @click="params.haveRoof = !params.haveRoof" :checked="params.haveRoof"/>
          <label for="roof">
            <div class="roofCheck"></div>
          </label>
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Наличие ограждения</vTitle>
          <input type="checkbox" id="fence" @click="params.haveFence = !params.haveFence" :checked="params.haveFence"/>
          <label for="fence">
            <div class="fenceCheck"></div>
          </label>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Путь подъезда</vTitle>
          <input type="text" name="" v-model="params.driveWay"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Контакты</vTitle>
          <input type="text" name="" v-model="params.contacts"/>
        </div>
        <div class="rowSelect">
          <vTitle class="pageTitle">Тип площадки</vTitle>
          <div class="select">
            <div @click.stop="showStandTypes = !showStandTypes">
              <input 
                type="text" 
                name="" 
                disabled
                v-model="choosenStandType"
              />
            </div>
            <div class="options" v-show="showStandTypes" v-click-outside="closeStandTypes">
              <div 
                class="option" 
                v-for="standType in standTypes" 
                :key="standType.id"
              >
                <vTitle class="pageTitle" @click.native.stop="chooseStandType(standType.id, standType.name)">{{ standType.name }}</vTitle>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="modal__container__save">
        <div class="row">
          <vButton class="greenButton" @click.native.stop="reset">Сброс</vButton>
        </div>
        <div class="row">
          <vButton class="greenButton" @click.native.stop="filter">Поиск</vButton>
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
  name: "findContainerYards",

  components: {
    vTitle,
    vButton,
  },

  data() {
    return {
      params: {
        isHighSecurityObject: false,
        isValidated: false,
        address: '',
        haveBase: false,
        haveRoof: false,
        haveFence: false,
        driveWay: '',
        contacts: '',
        choosenStandTypeId: null,
      },
      choosenStandType: '',
      showStandTypes: false,
      standTypes: [
        {
          id: 1,
          name: 'Общий'
        },
        {
          id: 2,
          name: 'Жилой фонд'
        },
      ],
    }
  },

  methods: {
    close(bool = false, params) {
      this.$emit('close', bool, params)
    },

    reset() {
      this.params.isHighSecurityObject = false
      this.params.isValidated = false
      this.params.haveBase = false
      this.params.haveRoof = false
      this.params.haveFence = false
      this.params.address = ''
      this.params.driveWay = ''
      this.params.contacts = ''
      this.params.choosenStandTypeId = null
      this.choosenStandType = ''
      this.close(false, this.params)
    },

    closeStandTypes() {
      this.showStandTypes = false
    },

    chooseStandType(id, name) {
      this.choosenStandType = name
      this.choosenStandTypeId = id
      this.closeStandTypes()
    },

    filter() {
      this.close(true, this.params)
    },
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
    padding: 20px 10px 40px 10px;

    &__save {
      width: 100%;
      display: flex;
      justify-content: space-around;
      align-items: center;
      margin-top: 20px;

      .row {
        width: 100px;
      }
    }

    .clientsEdit {

      &__container {
        display: flex;
        flex-direction: column;
        width: 100%;
        margin-top: 20px;

        .rowSelect {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin: 30px 0;

          .select {
            width: 70%;
            margin-left: 20px;
            position: relative;

            input {
              border: 1px solid #000;
              width: 100%;
              height: 30px;
              border-radius: 10px;
            }

            .options {
              position: absolute;
              top: 35px;
              left: 0;
              width: 100%;
              padding: 5px 10px;
              border: 1px solid #000;
              background: #FFF;
            }
          }
        }

        .longRow {
          width: 100%;
        }

        .row {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin: 5px 0;

          input {
            border: 1px solid #000;
            width: 70%;
            height: 30px;
            margin-left: 20px;
          }
        }

        .flex-start {
          justify-content: flex-start !important;
        }

        .greenRow {
          background: rgba(37, 72, 15, 0.58);
          height: 40px;
          border-radius: 0 10px 10px 0 ;
          padding: 5px 20px;
          margin-left: -10px !important;
        }
      }
      }

    &__close {
      display: flex;
      justify-content: space-between;
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
