<template>
  <v-card>
    <v-card-title class="text--secondary"> Фильтр </v-card-title>
    <v-divider class="mx-4"></v-divider>
    <v-card-text class="pb-2">
      <v-text-field v-model="value.date" filled type="date" label="Дата" :rules="requiredRules" required
        outlined></v-text-field>

      <v-select v-model="value.workShiftIds" :items="workshifts" item-text="workshiftData" clearable multiple
        item-value="id" label="Экипажи" rounded filled class="mt-4 mr-2">
      </v-select>
      <v-select v-model="value.routeIds" :items="routes" clearable multiple item-value="id" label="Рейсы" rounded filled
        class="mt-4 mr-2">
        <template v-slot:selection="{ item }">
          {{ item.routeName }}
        </template>
        <template v-slot:item="{ item }">
          {{ item.routeName }}
        </template>
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
  created() {
    this.value.date = this.$getTomorrowDate();
  },
  data() {
    return {
      value: {},
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
    };
  },
  props: {
    routes: [],
    workshifts: [],
  },
  methods: {
    onSave() {
      this.$emit("onSave");
    },
    onReset() {
      this.value = {
        date: this.$correctDate(new Date()),
      };
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
