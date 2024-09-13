<template>
  <div class="test">
    <div>
      <topPanel :title="editTitle" :hiddenInput="true" :hiddenPlus="true"/>
      <editView />
    </div>
  </div>
</template>

<script>
import topPanel from "@/components/taks/test/topPanel";
import editView from "@/components/taks/test/editView";

export default {
  name: "test",

  components: {
    topPanel,
    editView,
  },

  data() {
    return {
      editTitle: 'Внос данных по больничному/отпуску',
    }
  },
}
</script>

<style lang="scss" scoped>

</style>
