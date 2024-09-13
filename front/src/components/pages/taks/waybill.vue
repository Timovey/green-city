<template>
  <div class="waybill">
    <topPanel :title="title"/>
    <mainView />
  </div>
</template>

<script>
import topPanel from "@/components/taks/waybill/topPanel";
import mainView from "@/components/taks/waybill/mainView";

export default {
  name: "waybill",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'Путевой лист',
    }
  },
}
</script>

<style lang="scss" scoped>

</style>
