<template>
  <div class="card-container" v-if="item && Object.keys(item).length > 0">
    <div class="chip-container">
      <v-chip class="ma-2" color="primary" v-if="types && item.standType">
        {{ types[item.standType] }}
      </v-chip>
    </div>
    <v-card>
      <v-card-title class="pt-0 pb-2">{{ item.address }}</v-card-title>
      <v-card-subtitle class="d-flex flex-start pb-2">
        <v-icon>mdi-map-marker</v-icon>
        {{ item.longitude }}; {{ item.latitude }}
      </v-card-subtitle>
      <v-card-text class="d-flex align-start flex-column pt-0 pb-0">
        <div class="d-flex flex-start">
          <v-icon class="mr-2">mdi-comment</v-icon>
          {{ item.coordinatesComment }}
        </div>
        <div class="d-flex justify-space-between pb-2 w-100">
          <div class="d-flex flex-start align-center">
            <v-icon class="mr-2">mdi-card-account-phone</v-icon>
            {{ item.contacts }}
          </div>
          <v-spacer />
          <v-btn @click="$emit('onRemove')" class="error">Удалить</v-btn>
        </div>
      </v-card-text>
    </v-card>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  props: {
    item: {
      type: Object,
      required: false,
      default: () => ({}),
    },
    types: {
      type: Object,
      required: false,
      default: () => ({}),
    },
  },
};
</script>

<style>
.card-container {
  position: relative;
}

.chip-container {
  position: absolute;
  top: 0;
  right: 0;
  z-index: 1;
  transform: translate(0%, -50%);
}
.w-100 {
  width: 100%;
}
</style>
