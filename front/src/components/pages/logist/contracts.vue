<template>
  <div class="contracts">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/logist/contracts/topPanel";
import mainView from "@/components/logist/contracts/mainView";

export default {
  name: "contracts",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'Договор',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.LogistContracts__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
