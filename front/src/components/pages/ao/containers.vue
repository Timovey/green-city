<template>
  <div class="containers">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
    <div v-else-if="currentView === 'edit'">
      <topPanel :title="editTitle" :hiddenInput="true" :hiddenPlus="true"/>
      <editView :containerId="containerId.toString()"/>
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/ao/containers/topPanel";
import mainView from "@/components/ao/containers/mainView";
import editView from "@/components/ao/containers/editView";

export default {
  name: "containers",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Емкости',
      editTitle: 'Емкости',
      currentView: 'main',
      containerId: '',
    }
  },

  created() {
    this.$root.$refs.Containers__component = this
  },

  methods: {
    openContainerEdit(id) {
      this.containerId = id
      this.currentView = 'edit'
    },

    openContainerCreate() {
      this.containerId = ''
      this.currentView = 'edit'
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
