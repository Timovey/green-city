<template>
  <div class="emloyees">
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
import topPanel from "@/components/taks/employees/topPanel";
import mainView from "@/components/taks/employees/mainView";
import editView from "@/components/taks/employees/editView";

export default {
  name: "employees",

  components: {
    topPanel,
    mainView,
    editView,
  },

  data() {
    return {
      title: 'Сотрудники',
      editTitle: 'Водитель',
      currentView: 'main'
    }
  },

  created() {
    this.$root.$refs.Employees__component = this
  },
}
</script>

<style lang="scss" scoped>

</style>
