<template>
  <div class="waybill">
    <div v-if="currentView === 'main'">
      <topPanel :title="title"/>
      <mainView />
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/logist/waybill/topPanel";
import mainView from "@/components/logist/waybill/mainView";

export default {
  name: "waybill",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'Путевой лист',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.Waybill__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
