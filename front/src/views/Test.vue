<template>
  <div class="test">
    <cronModal />
  </div>
</template>

<script>
import cronModal from "@/components/cron/cronModal.vue";

export default {
  name: "Test",

  components: {
    cronModal,
  },

  data() {
    return {
    }
  },

   methods: {
  
  }
};
</script>

<style lang="scss" scoped>
  .test {
    height: 100vh;
  }
</style>
