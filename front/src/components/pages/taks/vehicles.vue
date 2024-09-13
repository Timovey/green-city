<template>
  <div class="vehicles">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
    <div v-else-if="currentView === 'edit'">
      <topPanel :title="editTitle" :hiddenInput="true" :hiddenPlus="true"/>
      <editView />
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/taks/vehicles/topPanel";
import mainView from "@/components/taks/vehicles/mainView";
import editView from "@/components/taks/vehicles/editView";

export default {
  name: "vehicles",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Транспортные средства',
      editTitle: 'Паспорт ТС',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.Vehicles__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
