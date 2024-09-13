<template>
  <v-card>
    <div>
      <v-btn icon color="grey" @click="$emit('onUp')">
        <v-icon>mdi-arrow-up</v-icon>
      </v-btn>
      <v-btn icon color="grey" @click="$emit('onDown')">
        <v-icon>mdi-arrow-down</v-icon>
      </v-btn>
    </div>
    <v-card-title class="pt-0 pb-0">{{ item.standAddress }}</v-card-title>
    <v-card-text class="mb-0 pb-0">
      <div class="d-flex flex-column">
        <div class="">Инф.:{{ item.containersInfo }}</div>
        <div class="">Объем: {{ item.containersVolume }}</div>
      </div>
    </v-card-text>
    <v-card-actions class="pt-0">
      <v-btn icon color="red" @click="$emit('onRemove')">
        <v-icon>mdi-minus</v-icon>
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
  },
};
</script>

<style lang="scss" scoped></style>
