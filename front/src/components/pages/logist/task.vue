<template>
  <div class="task">
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
import topPanel from "@/components/logist/task/topPanel";
import mainView from "@/components/logist/task/mainView";
import editView from "@/components/logist/task/editView";

export default {
  name: "task",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Сменное задание',
      editTitle: 'Сменное задание',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.Task__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
