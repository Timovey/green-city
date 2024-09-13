<template>
  <div class="clients">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView :clientId="clientId"/>
    </div>
    <div v-else-if="currentView === 'edit'">
      <topPanel :title="editTitle" :hiddenInput="true" :hiddenPlus="true"/>
      <editView :clientId="clientId" :contractId="contractId.toString()"/>
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/ao/contracts/topPanel";
import mainView from "@/components/ao/contracts/mainView";
import editView from "@/components/ao/contracts/editView";

export default {
  name: "contracts",

  components: {
    topPanel,
    mainView,
    editView,
  },

  props: {
    editView: {
      type: Boolean,
      default: false
    },

    clientId: {
      type: String,
      default: ''
    }
  },

  data() {
    return {
      title: 'Договор',
      editTitle: 'Добавление договора',
      currentView: 'main',
      contractId: '',
    }
  },

  created() {
    this.$root.$refs.Contracts__component = this
    if (this.editView) {
      this.currentView = 'edit'
    }
  },

  methods: {
    openContractEdit(id) {
      this.contractId = id
      this.currentView = 'edit'
    },

    openContractCreate() {
      this.contractId = '',
      this.currentView = 'edit'
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
