<template>
  <div class="fixingDrivers">
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
import topPanel from "@/components/taks/fixingDrivers/topPanel";
import mainView from "@/components/taks/fixingDrivers/mainView";
import editView from "@/components/taks/fixingDrivers/editView";

export default {
  name: "fixingDrivers",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Закрепление водителей',
      editTitle: 'Водитель',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.FixingDrivers__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
