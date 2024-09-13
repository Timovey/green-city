<template>
  <div class="operatingSchedule">
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
import topPanel from "@/components/taks/operatingSchedule/topPanel";
import mainView from "@/components/taks/operatingSchedule/mainView";
import editView from "@/components/taks/operatingSchedule/editView";

export default {
  name: "operatingSchedule",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Транспортные средства',
      subTitle: 'Распределение по сменам',
      editTitle: 'Сменный график работы ТС',
      currentView: 'main',
    }
  },

  created() {
    this.$root.$refs.OperatingSchedule__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
