<template>
  <v-card :style="getSelectedStyle" @click.stop="$emit('onclick')">
    <v-card-title class="pt-0 pb-0">
      {{ item.standAddress }}
    </v-card-title>
    <v-card-text class="mb-0 pb-0">
      <div class="d-flex flex-column">
        <div class="">Инф.:{{ item.containersInfo }}</div>
        <div class="">Объем: {{ item.containersVolume }}</div>
      </div>
    </v-card-text>
    <v-card-actions class="pt-0">
      <v-btn icon color="primary" @click.stop="$emit('onMove')">
        <v-icon>mdi-arrow-left</v-icon>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  data() {
    return {};
  },
  props: {
    item: {
      type: Object,
      required: true,
    },
    checked: {
      type: Boolean,
      required: true,
    },
  },
  computed: {
    getSelectedStyle() {
      return this.checked
        ? { backgroundColor: "#C5E1A5", cursor: "pointer" }
        : { cursor: "pointer" };
    },
  },
};
</script>

<style lang="scss" scoped></style>
