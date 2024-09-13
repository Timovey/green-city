<template>
  <v-form class="mr-2" :style="{ width: '100%' }" v-model="isValid" ref="form">
    <v-text-field outlined filled v-model="inputDate" :label="label" prepend-icon="mdi-calendar" :rules="rules"
      type="date" v-bind="$attrs" v-on="$listeners" :disabled="disabled" :clearable="clearable" />
  </v-form>
</template>

<script>
export default {
  data() {
    return {
      menu: false,
      anotherDate: "",
    };
  },
  props: {
    date: null,
    label: "",
    value: false,
    validate: true,
    // rules: {
    //     type: Array,
    //     default: () => [v => !!v || 'Укажите дату']
    // },
    rules: [],
    clearable: false,
    disabled: false,
  },
  methods: {
    weekDay(date) {
      return new Date(date).toLocaleString("ru-RU", { weekday: "short" });
    },
  },
  computed: {
    isValid: {
      get() {
        return !!this.value;
      },
      set(newValue) {
        this.$emit("input", newValue);
      },
    },

    inputDate: {
      get() {
        return this.date;
      },
      set(newValue) {
        this.$emit("update:date", newValue);
      },
    },
  },
};
</script>
