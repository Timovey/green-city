<template>
  <div class="workPlan">
    <topPanel :title="title"/>
    <mainView />
  </div>
</template>

<script>
import topPanel from "@/components/taks/workPlan/topPanel";
import mainView from "@/components/taks/workPlan/mainView";

export default {
  name: "workPlan",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'План рабочих смен',
    }
  },
}
</script>

<style lang="scss" scoped>

</style>
