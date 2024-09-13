<template>
  <v-dialog
    max-width="480"
    v-model="value"
    @click:outside="clickOutside()"
    @keydown="clickOutside()"
  >
    <v-card>
      <v-card-title class="text-wrap"
        >Вы действительно хотите выйти? Изменения не сохранятся!</v-card-title
      >
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="error lighten-2" @click="$emit('ok')"> Да </v-btn>
        <v-btn @click="$emit('not', false)"> Отмена </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  data() {
    return {};
  },
  props: {
    message: "",
    value: false,
  },
  methods: {
    clickOutside() {
      this.$emit("not", false);
    },
  },
};
</script>
