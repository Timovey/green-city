<template>
  <div class="cronsContainer">
    <div class="cronRow" v-for="(item, index) in cronsCopy" :key="item+index">
      <cron :cron="item" :id="index" @cron="setCron" />
      <button @click="deleteCron(index)">Удалить</button>
    </div>
    <div v-if="cronsCopy.length < 10">
      <button @click="addCron">Добавить новое выражение</button>
    </div>
  </div>
</template>

<script>
import cron from "@/components/cron/cron.vue";

export default {
  name: "crons",

  components: {
    cron,
  },

  props: {
    crons: {
      type: Array,
      default() {
        return []
      }
    }
  },

  data() {
    return {
      cronsCopy: [],
    }
  },

  created() {
    this.cronsCopy = this.crons
  },

  methods: {
    setCron(value ,index) {
      this.cronsCopy[index] = value
      this.$emit('cron', this.cronsCopy)
    },

    deleteCron(index) {
      this.cronsCopy.splice(index, 1)
      this.$emit('cron', this.cronsCopy)
    },

    addCron() {
      this.cronsCopy.push("0 0 0 * * ? *")
      this.$emit('cron', this.cronsCopy)
    }
  }
};
</script>

<style lang="scss" scoped>
  .cronsContainer {
    .cronRow {
      display: flex;
      justify-content: flex-start;
      align-items: center;
    }
  }
</style>
