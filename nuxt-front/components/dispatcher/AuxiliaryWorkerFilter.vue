<template>
  <v-card>
    <v-card-title class="text--secondary"> Фильтр </v-card-title>
    <v-divider class="mx-4"></v-divider>
    <v-card-text class="pb-2">
      <v-text-field
        v-model="value.surname"
        filled
        label="Фамилия"
        class="mr-2"
        outlined
        clearable
      >
      </v-text-field>

      <v-text-field
        v-model="value.name"
        filled
        label="Имя"
        class="mr-2"
        outlined
        clearable
      ></v-text-field>

      <v-select
        v-model="value.organizationIds"
        :items="organizations"
        item-text="name"
        clearable
        multiple
        item-value="id"
        label="Огранизации"
        rounded
        filled
        class="mt-4 mr-2"
      >
      </v-select>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn @click="onReset" color="warning"> Сбросить фильтры </v-btn>
      <v-btn @click="onSave" color="primary"> Сохранить </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      value: {},
    };
  },
  props: {
    organizations: [],
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
