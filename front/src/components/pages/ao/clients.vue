<template>
  <div class="clients">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
    <div v-else-if="currentView === 'edit'">
      <topPanel :title="editTitle" :hiddenInput="true" :hiddenPlus="true"/>
      <editView :clientId="clientId"/>
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/ao/clients/topPanel";
import mainView from "@/components/ao/clients/mainView";
import editView from "@/components/ao/clients/editView";

export default {
  name: "clients",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Контрагенты',
      editTitle: 'Контрагенты',
      currentView: 'main',
      clientId: '',
    }
  },

  created() {
    this.$root.$refs.Clients__component = this
  },

  methods: {
    openClientEdit(id) {
      this.clientId = id
      this.currentView = 'edit'
    },

    openClientCreate() {
      this.clientId = ''
      this.currentView = 'edit'
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
