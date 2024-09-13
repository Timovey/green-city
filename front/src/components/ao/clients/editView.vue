<template>
  <div class="clientsEdit">
    <div class="clientsEdit__container" v-if="loadedAll">
      <div class="row rowObject">
        <vTitle class="pageTitle">Запись корректна</vTitle>
        <input type="checkbox" id="valid" @click="isValidated = !isValidated" :checked="isValidated"/>
        <label for="valid">
          <div class="validCheck"></div>
        </label>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Наименование</vTitle>
        <input type="text" name="" v-model="clientName" />
      </div>
      <div class="row">
        <vTitle class="pageTitle">ИНН</vTitle>
        <input type="text" name="" v-model="inn"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">КПП</vTitle>
        <input type="text" name="" v-model="kpp"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">ОГРН</vTitle>
        <input type="text" name="" v-model="ogrn"/>
      </div>
      <div class="row longRow">
        <div class="row">
          <vTitle class="pageTitle">Р/СЧ</vTitle>
          <input type="text" name="" v-model="rschet"/>
        </div>
        <div class="row">
          <vTitle class="pageTitle">Банк</vTitle>
          <input type="text" name="" v-model="bank"/>
        </div>
      </div>
      <div class="row">
        <vTitle class="pageTitle">БИК</vTitle>
        <input type="text" name="" v-model="bik"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">К/СЧ</vTitle>
        <input type="text" name="" v-model="kschet"/>
      </div>
      <div class="row greenRow">
        <vTitle class="pageTitleWhite">Адрес</vTitle>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Юридический адрес</vTitle>
        <addressInput class="addressInput" :defaultAddress="legalAddress" :param="'legal'"/>
        <!-- <input type="text" name="" v-model="legalAddress"/> -->
      </div>
      <div class="row">
        <vTitle class="pageTitle">Почтовый адрес</vTitle>
        <addressInput class="addressInput" :defaultAddress="postalAddress" :param="'postal'"/>
        <!-- <input type="text" name="" v-model="postalAddress"/> -->
      </div>
      <div class="row">
        <vTitle class="pageTitle">Телефон</vTitle>
        <input type="text" name="" v-model="responsibleTkoPersonPhone"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Электронная почта</vTitle>
        <input type="text" name="" v-model="email"/>
      </div>
      <div class="row greenRow">
        <vTitle class="pageTitleWhite">Контактные данные</vTitle>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Руководитель</vTitle>
        <input type="text" name="" v-model="supervizorName"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Должность руководителя</vTitle>
        <input type="text" name="" v-model="supervizorPosition"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Ответственное лицо по ТКО</vTitle>
      </div>
      <div class="row">
        <vTitle class="pageTitle">ФИО</vTitle>
        <input type="text" name="" v-model="responsibleTkoPersonName"/>
      </div>
      <div class="row">
        <vTitle class="pageTitle">Контактный телефон</vTitle>
        <input type="text" name="" v-model="clientContact"/>
      </div>
    </div>
    <div v-else>
      <vTitle>Загрузка</vTitle>
    </div>
    <createdStatusModal v-if="showCreatedStatus" :isError="errorStatus"/>
  </div>
</template>

<script>

import vTitle from "@/components/ui-kit/vTitle";
import createdStatusModal from "@/components/modals/createdStatusModal";
import addressInput from "@/components/ui-kit/addressInput";

import axios from 'axios';

export default {
  name: "editView",

  components: {
    vTitle,
    createdStatusModal,
    addressInput,
  },

  props: {
    clientId: {
      type: String,
      default: ''
    },
  },

  data() {
    return {
      isValidated: false,
      showCreatedStatus: false,
      errorStatus: false,
      clientName: '',
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
      clientContact: '',
      loadedAll: true,
    }
  },

  created() {
    this.$root.$refs.ClientsEditView_component = this
    if (this.clientId != '') {
      this.loadedAll = false
      this.getClient()
    }
  },

  methods: {

    saveClient() {
      if (this.clientId != '') {
        this.editClient()
      }
      else {
        this.createClient()
      }
    },

    async editClient() {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          contractClientId: this.clientId,
          clientName: this.clientName,
          inn: this.inn,
          kpp: this.kpp,
          ogrn: this.ogrn,
          rschet: this.rschet,
          bank: this.bank,
          bik: this.bik,
          kschet: this.kschet,
          legalAddress: this.legalAddress,
          isValidated: this.isValidated,
          postalAddress: this.postalAddress,
          email: this.email,
          supervizorName: this.supervizorName,
          supervizorPosition: this.supervizorPosition,
          responsibleTkoPersonName: this.responsibleTkoPersonName,
          responsibleTkoPersonPhone: this.responsibleTkoPersonPhone,
          clientContact: this.clientContact
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/UpdateContractClient`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response)
          this.errorStatus = false
          this.showCreatedStatus = true
          setTimeout(() => {
            this.showCreatedStatus = false
          }, 3000)
        })
        .catch((error) => {
          console.log(error);
          this.errorStatus = true
          this.showCreatedStatus = true
          setTimeout(() => {
            this.showCreatedStatus = false
            this.errorStatus = false
          }, 3000)
        })
    },

    async createClient() {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          clientName: this.clientName,
          inn: this.inn,
          kpp: this.kpp,
          ogrn: this.ogrn,
          rschet: this.rschet,
          bank: this.bank,
          bik: this.bik,
          kschet: this.kschet,
          isValidated: this.isValidated,
          legalAddress: this.legalAddress,
          postalAddress: this.postalAddress,
          email: this.email,
          supervizorName: this.supervizorName,
          supervizorPosition: this.supervizorPosition,
          responsibleTkoPersonName: this.responsibleTkoPersonName,
          responsibleTkoPersonPhone: this.responsibleTkoPersonPhone,
          clientContact: this.clientContact
        }
        console.log(data)
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/CreateContractClient`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response)
          this.errorStatus = false
          this.showCreatedStatus = true
          setTimeout(() => {
            this.showCreatedStatus = false
          }, 3000)
        })
        .catch((error) => {
          console.log(error);
          this.errorStatus = true
          this.showCreatedStatus = true
          setTimeout(() => {
            this.showCreatedStatus = false
            this.errorStatus = false
          }, 3000)
        })
    },

    async getClient() {
      let data = {
          userId: localStorage.getItem('UserId'),
          userName: localStorage.getItem('UserName'),
          contractClientId: this.clientId
        }
        return await axios.post(`${process.env.VUE_APP_API_IP}/api/Sd/GetContractClient`, data, {headers: {
          'Authorization': localStorage.getItem('Token')
          }
        })
        .then(( response ) => {
          console.log(response.data.data)
          this.clientName = response.data.data.clientName
          this.inn = response.data.data.inn
          this.kpp = response.data.data.kpp
          this.ogrn = response.data.data.ogrn
          this.rschet = response.data.data.rschet
          this.bank = response.data.data.bank
          this.bik = response.data.data.bik
          this.kschet = response.data.data.kschet
          this.isValidated = response.data.data.isValidated
          this.legalAddress = response.data.data.legalAddress
          this.postalAddress = response.data.data.postalAddress
          this.email = response.data.data.email
          this.supervizorName = response.data.data.supervizorName
          this.supervizorPosition = response.data.data.supervizorPosition
          this.responsibleTkoPersonName = response.data.data.responsibleTkoPersonName
          this.responsibleTkoPersonPhone = response.data.data.responsibleTkoPersonPhone
          this.clientContact = response.data.data.clientContact
          this.loadedAll = true
        })
        .catch((error) => {
          console.log(error);
        })
    }
  }
}
</script>

<style lang="scss">
.clientsEdit {

  &__container {
    display: flex;
    flex-direction: column;
    width: 60%;

    .row {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin: 10px 0;

      input {
        border: 1px solid #000;
        width: 70%;
        height: 30px;
        margin-left: 20px;
        border-radius: 10px;
      }
    }

    #valid {
      display: none;

      &:checked + label .validCheck {
        background: #000;
      }
    }

    .validCheck {
      margin-left: 20px;
      border: 1px solid #000;
      width: 30px;
      height: 30px;
      border-radius: 15px;
    }

    .longRow {
      width: 125%;

      .row {
        width: 49%;
      }
    }

    .greenRow {
      background: rgba(37, 72, 15, 0.58);
      height: 40px;
      border-radius: 0 10px 10px 0 ;
      padding: 5px 20px;
      margin-left: -20px;
    }
  }
}
</style>
