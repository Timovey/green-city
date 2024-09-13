<template>
  <v-card-actions>
    <v-spacer></v-spacer>
    <v-btn
      v-if="showEdit"
      @click="$emit('onEdit')"
      :disabled="!selectedItem"
      class="warning text--primary lighten-2"
    >
      {{ editName }}
    </v-btn>
    <v-btn
      v-if="showDelete"
      @click="$emit('onShowModal')"
      :disabled="!selectedItem"
      class="error lighten-2"
    >
      Удалить
    </v-btn>
  </v-card-actions>
</template>

<script>
export default {
  props: {
    selectedItem: {},
    editName: {
      type: String,
      required: false,
      default: "Изменить",
    },
    showDelete: {
      type: Boolean,
      required: false,
      default: true,
    },
    showEdit: {
      type: Boolean,
      required: false,
      default: true,
    },
  },
};
</script>
