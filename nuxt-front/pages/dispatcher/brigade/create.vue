<template>
  <div>
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="Бригада"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="formAux">
            <v-card-title class="text--secondary">
              <div>Добавление бигады</div>
            </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text class="pb-2">
              <v-text-field
                v-model="data.date"
                type="date"
                label="Дата"
                :rules="requiredRules"
                required
                outlined
                filled
              ></v-text-field>

              <v-autocomplete
                :rules="requiredRules"
                :items="workhifts"
                label="Экипажи"
                item-value="id"
                outlined
                filled
                required
                item-text="workshiftData"
                v-model="data.workShiftId"
              ></v-autocomplete>

              <v-autocomplete
                :rules="requiredRules"
                :items="auxiliaryWorkers"
                label="Подсобные рабочие"
                item-value="id"
                outlined
                filled
                required
                item-text="fio"
                multiple
                v-model="data.auxWorkerIds"
              ></v-autocomplete>

              <v-autocomplete
                :rules="requiredRules"
                :items="routes"
                label="Рейсы"
                item-value="id"
                outlined
                filled
                required
                item-text="name"
                multiple
                v-model="data.routeIds"
              ></v-autocomplete>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="onBack" color="error lighten-2"> Отмена </v-btn>
              <v-btn @click="onSubmit" color="primary">
                <div>Сохранить</div>
              </v-btn>
            </v-card-actions>
          </v-form>
        </v-card>
      </v-col>
      <v-col class="col col-2" />
    </v-row>
  </div>
</template>

<script>
import GlobalMenu from "@/components/UI/GlobalMenu";
import PickDateV2 from "@/components/UI/PickDateV2";
import { dispatcherMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Бригада",
  },
  created() {
    this.data.date = this.$correctDate(new Date());
    this.fetchDropdownWorkshifts();
    this.fetchAuxiliaryWorkers();
    this.fetchRoutes();
  },
  components: {
    GlobalMenu,
    PickDateV2,
  },
  data() {
    return {
      menu: dispatcherMenu,
      drawer: false,
      backRouter: "/dispatcher/brigade",
      isValid: false,
      validation: {
        acceptValid: false,
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      workhifts: [],
      auxiliaryWorkers: [],
      routes: [],
      data: {
        date: new Date(),
        workShiftId: -1,
        auxWorkerIds: [],
        routeIds: [],
      },
    };
  },
  methods: {
    fetchDropdownWorkshifts() {
      this.$api.dropdownWorker
        .fetchWorkhifts(this.data)
        .then((res) => (this.workhifts = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchRoutes() {
      this.$api.brigade
        .fetchRoutes(this.data)
        .then((res) => (this.routes = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchAuxiliaryWorkers() {
      let data = {
        isFilter: true,
        ...this.filters,
        sorting: {},
      };
      this.$api.auxiliaryWorker
        .fetch(data)
        .then(
          (res) =>
            (this.auxiliaryWorkers = res.map((el) =>
              this.correctDataWorker(el)
            ))
        )
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    correctDataWorker(item) {
      const format = "DD-MM-YYYY";
      item.birthDate = this.$correctDate(item.birthDate, format);
      item.issueDate = this.$correctDate(item.issueDate, format);
      item.acceptWorkDate = this.$correctDate(item.acceptWorkDate, format);
      if (item.firedDate)
        item.firedDate = this.$correctDate(item.firedDate, format);

      return this.addFio(item);
    },

    addFio(item) {
      let patr = item.patronymic ? item.patronymic[0] + "." : "";
      item.fio = item.surname + " " + item.name[0] + ". " + patr;
      return item;
    },
    onBack() {
      this.$router.push(this.backRouter);
    },
    onSubmit() {
      this.$refs.formAux.validate();
      if (!this.isValid) {
        showErrorMessage("Проверьте данные");
        return;
      }

      this.$api.brigade
        .create(this.data)
        .then((res) => {
          this.data = res;
          showSuccess();
          this.$router.push(this.backRouter);
        })
        .catch((err) => {
          showErrorMessage(err);
        });
    },
  },
  computed: {},
  watch: {
    "data.date"(oldVal, newVal) {
      this.data.workShiftId = -1;
      this.data.dateTime = this.data.date;
      this.data.routeIds = [];
      this.fetchDropdownWorkshifts();
      this.fetchRoutes();
    },
    "data.workShiftId"(oldVal, newVal) {
      this.data.routeIds = [];
      this.fetchRoutes();
    },
  },
};
</script>

<style lang="scss" scoped></style>
