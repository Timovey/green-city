<template>
  <div class="baseTrips">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/logist/baseTrips/topPanel";
import mainView from "@/components/logist/baseTrips/mainView";

export default {
  name: "baseTrips",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'Базовый рейс',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.BaseTrips = this
  },
}
</script>

<style lang="scss" scoped>

</style>
