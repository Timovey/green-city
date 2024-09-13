<template>
  <div class="containerYards">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
    <div v-else-if="currentView === 'edit'">
      <topPanel :title="editTitle" :hiddenInput="true" :hiddenPlus="true"/>
      <editView :containerYardId="containerYardId.toString()"/>
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/ao/containerYards/topPanel";
import mainView from "@/components/ao/containerYards/mainView";
import editView from "@/components/ao/containerYards/editView";

export default {
  name: "containerYards",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Контейнерные площадки',
      editTitle: 'Контейнерные площадки',
      currentView: 'main',
      containerYardId: '',
    }
  },

  created() {
    this.$root.$refs.ContainerYards__component = this
  },

  methods: {
    openContainerYardEdit(id) {
      this.containerYardId = id
      this.currentView = 'edit'
    },

    openContainerYardCreate() {
      this.containerYardId = ''
      this.currentView = 'edit'
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
