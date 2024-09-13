<template>
  <div class="driversSchedule">
    <topPanel :title="title" :subTitle="subTitle"/>
    <mainView />
  </div>
</template>

<script>
import topPanel from "@/components/taks/driversSchedule/topPanel";
import mainView from "@/components/taks/driversSchedule/mainView";

export default {
  name: "driversSchedule",

  components: {
    topPanel,
    mainView,
  },

  data() {
    return {
      title: 'Транспортные средства',
      subTitle: 'График работы водителей',
    }
  },
}
</script>

<style lang="scss" scoped>

</style>
