<template>
  <div class="dutyShifts">
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
import topPanel from "@/components/taks/dutyShifts/topPanel";
import mainView from "@/components/taks/dutyShifts/mainView";
import editView from "@/components/taks/dutyShifts/editView";

export default {
  name: "dutyShifts",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Транспортные средства',
      subTitle: 'Дежурные смены',
      editTitle: 'Внос данных по дежурной смене',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.DutyShifts__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
