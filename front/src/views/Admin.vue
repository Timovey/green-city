<template>
  <div class="adminView">
    <div class="button__container">
      <vButton class="greenButton" @click.native="goto('ao')">Абонентский отдел</vButton>
    </div>
    <div class="button__container">
      <vButton class="greenButton" @click.native="goto('taks')">Таксировщик</vButton>
    </div>
    <div class="button__container">
      <vButton class="greenButton" @click.native="goto('logist')">Логист</vButton>
    </div>
  </div>
</template>

<script>
import vButton from "@/components/ui-kit/vButton.vue";

export default {
  name: "Admin",

  components: {
    vButton
  },

  data() {
    return {
      
    }
  },

  methods: {
    goto(value) {
      this.$router.push(`/${value}`)
    },
  },
};
</script>

<style lang="scss" scoped>
.adminView {
  display: flex;
}
</style>
