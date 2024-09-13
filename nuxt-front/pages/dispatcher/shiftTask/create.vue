<template>
  <div>
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="Сменное задание"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="form">
            <v-card-title class="text--secondary">
              <div>Создание сменного задания</div>
            </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text class="pb-2">
              <v-text-field
                v-model="shiftTask.date"
                filled
                type="date"
                label="Дата"
                :rules="requiredRules"
                required
                outlined
              ></v-text-field>

              <v-select
                v-model="shiftTask.workShiftId"
                :items="workshifts"
                item-text="workshiftData"
                item-value="id"
                label="Экипаж"
                :rules="requiredRules"
                required
                dense
                class="mt-4"
              >
              </v-select>

              <v-select
                v-model="shiftTask.orgId"
                :items="organizations"
                item-text="name"
                clearable
                item-value="id"
                label="Огранизация"
                dense
                required
                :rules="requiredRules"
                class="mt-4"
              >
              </v-select>

              <v-autocomplete
                :items="routes"
                label="Рейсы"
                item-value="id"
                outlined
                filled
                required
                :rules="requiredRules"
                clearable
                item-text="name"
                multiple
                v-model="shiftTask.routeIds"
              ></v-autocomplete>
            </v-card-text>

            <v-card-actions class="d-flex flex-column align-start">
              <a v-if="shiftTask.id" @click="printShiftTask">Печать СЗ</a>
              <a v-if="shiftTask.travelSheetId" @click="">Печать ПЛ</a>
            </v-card-actions>
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
import { dispatcherMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Сменное задание",
  },
  created() {
    this.shiftTask.date = this.$moment().format("YYYY-MM-DD");
    this.dateTime = this.shiftTask.date;
    this.fetchData();
  },
  components: {
    GlobalMenu,
  },
  data() {
    return {
      menu: dispatcherMenu,
      editStatus: false,
      id: false,
      drawer: false,
      backRouter: "/dispatcher/shiftTask",
      isValid: false,
      routes: [],
      workshifts: [],
      organizations: [],
      validation: {
        acceptValid: false,
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],

      shiftTask: {
        date: Date.UTC,
        workShiftId: -1,
        routeIds: [],
        orgId: -1,
      },
    };
  },
  methods: {
    fetchData() {
      if (!this.$moment(this.shiftTask.date).isValid()) {
        return;
      }
      //смотрим экипажи
      const getWorkshiftData = {
        dateTime: this.shiftTask.date,
      };
      this.$api.dropdownWorker
        .fetchWorkhifts(getWorkshiftData)
        .then((res) => {
          this.workshifts = res;
        })
        .catch((err) => {
          showErrorMessage(err);
        });

      //смотрим рейсы на основе даты и экипажей
      const getData = {
        date: this.shiftTask.date,
        workShiftId: this.shiftTask.workShiftId,
      };
      this.$api.shiftTask
        .fetchRoutes(getData)
        .then((res) => (this.routes = res))
        .catch((err) => {
          showErrorMessage(err);
        });

      //смотрим организации на основе экипажа
      this.$api.shiftTask
        .fetchOrganizations(getData)
        .then((res) => (this.organizations = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    printShiftTask() {},
    onBack() {
      this.$router.push(this.backRouter);
    },
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) {
        showErrorMessage("Проверьте данные");
        return;
      }

      this.$api.shiftTask
        .create(this.shiftTask)
        .then((res) => {
          this.shiftTask = res;
          showSuccess();

          this.$api.report
            .printShiftTask({
              id: res.id,
            })
            .catch((error) => {
              showErrorMessage(error);
            });
          this.$api.report
            .printTravelSheet({
              id: res.travelSheetId,
            })
            .catch((error) => {
              showErrorMessage(error);
            });

          this.$router.push(this.backRouter);
        })
        .catch((err) => {
          showErrorMessage(err);
        });
    },
  },
  computed: {},
  watch: {
    "shiftTask.date": {
      handler(oldVal, newVal) {
        this.shiftTask.workShiftId = -1;
        this.shiftTask.routeIds = [];
      },
      immediate: true,
    },

    "shiftTask.workShiftId": {
      handler(oldVal, newVal) {
        this.shiftTask.orgId = -1;
        this.fetchData();
      },
    },
    shiftTask: {
      handler(oldVal, newVal) {
        this.fetchData();
      },
      deep: true,
    },
  },
};
</script>

<style lang="scss" scoped></style>
