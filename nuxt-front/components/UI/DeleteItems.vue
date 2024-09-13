<template>
  <v-dialog
    max-width="480"
    v-model="value"
    @click:outside="clickOutside()"
    @keydown="clickOutside()"
  >
    <v-card>
      <v-card-title class="text-wrap"
        >Вы действительно хотите удалить элемент?</v-card-title
      >
      <v-card-text>
        {{ message }}
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn class="error lighten-2" @click="$emit('onDelete')"> Да </v-btn>
        <v-btn @click="$emit('input', false)"> Отмена </v-btn>
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
      this.$emit("input", false);
    },
  },
};
</script>
