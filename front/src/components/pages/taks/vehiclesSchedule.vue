<template>
  <div class="vehiclesSchedule">
    <topPanel :title="title" :subTitle="subTitle"/>
    <mainView />
  </div>
</template>

<script>
import topPanel from "@/components/taks/vehiclesSchedule/topPanel";
import mainView from "@/components/taks/vehiclesSchedule/mainView";

export default {
  name: "vehiclesSchedule",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'Транспортные средства',
      subTitle: 'График работы',
    }
  },
}
</script>

<style lang="scss" scoped>

</style>
