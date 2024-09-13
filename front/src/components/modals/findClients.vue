<template>
  <div class="modal">
    <div class="modal__container">
      <div class="modal__container__close">
        <vTitle class="pageTitle">Поиск контрагентов</vTitle>
        <div class="cross" @click="reset()"></div>
      </div>
      <div class="clientsEdit__container">
        <div class="row">
          <vTitle class="pageTitle">Наименование</vTitle>
          <input type="text" name="" v-model="params.clientName"/>
        </div>
        <div class="row rowObject">
          <vTitle class="pageTitle">Запись корректна</vTitle>
          <input type="checkbox" id="valid" @click="params.isValidated = !params.isValidated" :checked="params.isValidated"/>
          <label for="valid">
            <div class="validCheck"></div>
          </label>
        </div>
        <div class="row">
          <vTitle class="pageTitle">ИНН</vTitle>
          <input type="text" name="" v-model="params.inn"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">КПП</vTitle>
          <input type="text" name="" v-model="params.kpp"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">ОГРН</vTitle>
          <input type="text" name="" v-model="params.ogrn"/>
        </div>
        <div class="row longRow">
          <div class="row">
            <vTitle class="pageTitle">Р/СЧ</vTitle>
            <input type="text" name="" v-model="params.rschet"/>
          </div>
          <div class="row">
            <vTitle class="pageTitle">Банк</vTitle>
            <input type="text" name="" v-model="params.bank"/>
          </div>
        </div>
        <div class="row">
          <vTitle class="pageTitle">БИК</vTitle>
          <input type="text" name="" v-model="params.bik"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">К/СЧ</vTitle>
          <input type="text" name="" v-model="params.kschet"/>
        </div>
        <div class="row greenRow">
          <vTitle class="pageTitleWhite">Адрес</vTitle>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Юридический адрес</vTitle>
          <input type="text" name="" v-model="params.legalAddress"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Почтовый адрес</vTitle>
          <input type="text" name="" v-model="params.postalAddress"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Электронная почта</vTitle>
          <input type="text" name="" v-model="params.email"/>
        </div>
        <div class="row greenRow">
          <vTitle class="pageTitleWhite">Контактные данные</vTitle>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Руководитель</vTitle>
          <input type="text" name="" v-model="params.supervizorName"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Должность руководителя</vTitle>
          <input type="text" name="" v-model="params.supervizorPosition"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Ответственное лицо по ТКО</vTitle>
        </div>
        <div class="row longRow">
          <div class="row">
            <vTitle class="pageTitle">ФИО</vTitle>
            <input type="text" name="" v-model="params.responsibleTkoPersonName"/>
          </div>
          <div class="row">
            <vTitle class="pageTitle">Телефон</vTitle>
            <input type="text" name="" v-model="params.responsibleTkoPersonPhone"/>
          </div>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Контактный телефон</vTitle>
          <input type="text" name="" v-model="params.clientContact"/>
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
  name: "findClients",

  components: {
    vTitle,
    vButton,
  },

  data() {
    return {
      params: {
        clientName: '',
        isValidated: false,
        inn: '',
        kpp: '',
        ogrn: '',
        rschet: '',
        bank: '',
        bik: '',
        kschet: '',
        legalAddress: '',
        postalAddress: '',
        email: '',
        supervizorName: '',
        supervizorPosition: '',
        responsibleTkoPersonName: '',
        responsibleTkoPersonPhone: '',
        clientContact: ''
      }
    }
  },

  methods: {
    close(bool = false, params) {
      this.$emit('close', bool, params)
    },

    reset() {
      this.params.clientName = ''
      this.params.isValidated = false
      this.params.inn = ''
      this.params.kpp = ''
      this.params.ogrn = ''
      this.params.rschet = ''
      this.params.bank = ''
      this.params.bik = ''
      this.params.kschet = ''
      this.params.legalAddress = ''
      this.params.postalAddress = ''
      this.params.email = ''
      this.params.supervizorName = ''
      this.params.supervizorPosition = ''
      this.params.responsibleTkoPersonName = ''
      this.params.responsibleTkoPersonPhone = ''
      this.params.clientContact = ''
      this.close(false, this.params)
    },

    filter() {
      this.close(true, this.params)
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
