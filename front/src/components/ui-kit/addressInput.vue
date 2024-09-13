<template>
  <div v-click-outside="hideSuggestions">
    <input id="address" @focus="showSuggestions" @input.prevent="changeInput()" v-model="address" type="text" name="search" class="fias-search" placeholder="Начните вводить адрес ...">
    <div class="fias-dropdown-wrapper" v-if="suggestions.length && suggestionsVisible">
      <ul class="fias-dropdown" v-if="suggestionsVisible" >
        <li @click="changeInput(suggestion.value)" v-for="(suggestion, index) in suggestions" :key="index">{{suggestion.value}}</li>
      </ul>
    </div>
    <link href="https://data.pbprog.ru/fiasSuggestions.min.css" rel="stylesheet"/>
  </div>
</template>

<script>
import ClickOutside from 'vue-click-outside'

import axios from 'axios'

export default {
  name: 'addressInput',

  props: {
    param: {
      type: String,
      default: ''
    },

    defaultAddress: {
      type: String,
      default: ''
    }
  },
  
  data() {
    return {
      address: '',
      suggestions: [],
      suggestionsVisible: false,
    }
  },

  created() {
    this.$root.$refs.AddressInput__component = this
    if (this.defaultAddress) {
      this.address = this.defaultAddress
    }
  },

  methods: {
    changeInput(value = '') {
      console.log(value)
      if (value !== '') {
        this.address = value
        this.suggestionsVisible = false
      }
      this.sendQueryToApi()
    },

    async sendQueryToApi() {
      return await axios.post(`https://data.pbprog.ru/api/address/full-address/parse`, JSON.stringify({query: this.address, hierarchyMode:'adm'}), { headers: {
          'Authorization': "10f6359a7b36477ab282b1b0c7caad6daefef54a",
          'Content-Type':'application/json'
        }
      })
      .then(( response ) => {
        console.log(response.data.suggestions)
        this.suggestions = response.data.suggestions
        if (this.param == 'legal') {
          this.$root.$refs.ClientsEditView_component.legalAddress = this.suggestions[0].value
        }

        else if (this.param == 'postal') {
          this.$root.$refs.ClientsEditView_component.postalAddress = this.suggestions[0].value
        }
      })
      .catch((error) => {
        console.log(error)
      })
    },

    showSuggestions() {
      this.suggestionsVisible = true
    },

    hideSuggestions() {
      this.suggestionsVisible = false
    }
  },

  directives: {
    ClickOutside
  }
}
</script>

<style lang="scss">
  .fias-dropdown-wrapper {
    max-height: 500px !important;
    height: 500px !important;
    overflow: auto !important;
    position: absolute !important;
    top: 40px;
    left: 0;
    width: 100%;
    z-index: 100;
 }

 .fias-dropdown {
    position: initial !important;
    opacity: 1 !important;
    visibility: visible !important;
    max-height: 200px;
    height: fit-content !important;
    overflow: auto;
 }
</style>