<template>
  <div class="test">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/logist/test/topPanel";
import mainView from "@/components/logist/test/mainView";

export default {
  name: "test",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'Паспорт базового рейса',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.LogistTest_component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
