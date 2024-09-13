<template>
  <v-card>
    <v-card-title> Добавить отпуск/больничный </v-card-title>
    <v-card-text>
      <v-form v-model="valid" ref="formVacation">
        <div class="d-flex">
          <v-select
            :rules="requiredRules"
            required
            v-model="data.driverId"
            item-value="driverId"
            item-text="driverName"
            :items="filteredDrivers"
            label="Водитель"
            class="mr-2"
            filled
            rounded
            clearable
          >
            <template v-slot:selection="{ item }">
              {{ item.driverFio }}
            </template>
            <template v-slot:item="{ item }">
              {{ item.driverFio }}
            </template>
          </v-select>
          <v-select
            v-model="data.replacementDriverId"
            item-value="driverId"
            item-text="driverName"
            :items="filteredReplacementDrivers"
            label="Сменный водитель"
            class="ml-2"
            filled
            rounded
            clearable
          >
            <template v-slot:selection="{ item }">
              {{ item.driverFio }}
            </template>
            <template v-slot:item="{ item }">
              {{ item.driverFio }}
            </template>
          </v-select>
          <v-select
            :rules="requiredRules"
            required
            v-model="data.vacationType"
            item-value="value"
            item-text="name"
            :items="vacationTypes"
            label="Событие"
            class="ml-2"
            filled
            rounded
            clearable
          />
        </div>
        <DateRange :dateRange.sync="data.dateRange" />
      </v-form>
    </v-card-text>
    <v-card-actions>
      <v-spacer />
      <v-btn @click="onSave()" color="primary"> Сохранить </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import DateRange from "../UI/DateRange";
import { showErrorMessage } from "@/helpers/messages";
export default {
  components: { DateRange },
  data() {
    return {
      valid: false,
      data: {
        dateRange: {
          createDate: null,
          startDate: null,
          endDate: null,
          factEndDate: null,
        },
        driverId: null,
        replacementDriverId: null,
        vacationType: "",
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      dateErrors: false,
    };
  },
  props: {
    drivers: [],
    vacationTypes: [],
    addingErrors: false,
  },
  methods: {
    onSave() {
      this.$refs.formVacation.validate();
      if (!this.valid) {
        showErrorMessage("Невалидная форма");
        return;
      }
      this.data.reset = this.$refs.formVacation.reset;
      this.$emit("onSave", this.data);
    },
  },
  computed: {
    filteredReplacementDrivers() {
      let filteredDrivers = this.drivers.filter((item) => {
        if (this.data.driverId != item.driverId) {
          return item;
        }
      });
      return filteredDrivers;
    },
    filteredDrivers() {
      let filteredDrivers = this.drivers.filter((item) => {
        if (this.data.replacementDriverId != item.driverId) {
          return item;
        }
      });
      return filteredDrivers;
    },
  },
};
</script>
