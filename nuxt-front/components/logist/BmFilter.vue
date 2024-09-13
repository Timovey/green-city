<template>
  <v-card>
    <v-card-title class="text--secondary"> Фильтр </v-card-title>
    <v-card-text>
      <v-checkbox
        label="Мои БМ"
        v-model="value.isFilterByCreatedUser"
        outlined
        filled
      />
      <v-text-field
        filled
        v-model="value.name"
        label="Название"
        outlined
        clearable
      />
    </v-card-text>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn @click="onReset" color="warning"> Сбросить фильтры </v-btn>
      <v-btn @click.stop="$emit('onSave')" color="primary">
        <div>Применить</div>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      value: {
        isFilterByCreatedUser: false,
        name: "",
      },
    };
  },
  methods: {
    onSave() {
      this.$emit("onSave");
    },
    onReset() {
      this.value = {};
    },
  },
  watch: {
    value: {
      handler: function (newValue) {
        this.$emit("input", newValue);
      },
      deep: true,
    },
  },
};
</script>
