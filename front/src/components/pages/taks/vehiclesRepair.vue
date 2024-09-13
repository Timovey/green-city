<template>
  <div class="vehiclesRepair">
    <div v-if="currentView === 'main'">
      <topPanel :title="title" :subTitle="subTitle"/>
      <mainView />
    </div>
    <div v-else-if="currentView === 'edit'">
      <topPanel :title="editTitle" :hiddenInput="true" :hiddenPlus="true"/>
      <editView />
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/taks/vehiclesRepair/topPanel";
import mainView from "@/components/taks/vehiclesRepair/mainView";
import editView from "@/components/taks/vehiclesRepair/editView";

export default {
  name: "vehiclesRepair",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Транспортные средства',
      subTitle: 'ТО/Ремонт',
      editTitle: 'Внос данных по ремонту/ТО',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.VehiclesRepair__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
