<template>
  <div>
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="Путевой лист"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="formAux">
            <v-card-title class="text--secondary">
              <div v-if="!editStatus">Добавление путевого листа</div>
              <div v-else>Редактирование путевого листа</div>
            </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text class="pb-2">
              <v-text-field
                :disabled="editStatus"
                v-model="data.date"
                type="date"
                label="Дата"
                :rules="requiredRules"
                required
                outlined
                filled
              ></v-text-field>

              <v-select
                :disabled="editStatus"
                :rules="requiredRules"
                required
                v-model="data.driverId"
                item-value="driverId"
                :items="drivers"
                label="Водитель"
                filled
                rounded
              >
                <template v-slot:selection="{ item }">
                  {{ item.driverFio }}
                </template>
                <template v-slot:item="{ item }">
                  {{ item.driverFio }}
                </template>
              </v-select>

              <v-select
                :disabled="editStatus"
                :rules="requiredRules"
                required
                v-model="data.carId"
                item-value="id"
                :items="transports"
                label="Транспорт"
                filled
                rounded
              >
                <template v-slot:selection="{ item }">
                  {{ item.name + " " + item.regNumber }}
                </template>
                <template v-slot:item="{ item }">
                  {{ item.name + " " + item.regNumber }}
                </template>
              </v-select>

              <v-select
                :disabled="editStatus"
                v-model="data.orgId"
                :items="organizations"
                item-text="name"
                :rules="requiredRules"
                item-value="id"
                label="Огранизация"
                dense
                class="mt-4"
              >
              </v-select>

              <v-text-field
                v-if="editStatus"
                v-model="data.startDriveTime"
                label="Время выезда"
                outlined
                class="mt-2"
                filled
              ></v-text-field>

              <v-text-field
                v-if="editStatus"
                v-model="data.finishDriveTime"
                label="Время заезда"
                outlined
                filled
              ></v-text-field>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="onBack" color="error lighten-2"> Отмена </v-btn>
              <v-btn @click="onSubmit" color="primary">
                <div v-if="editStatus">Сохранить</div>
                <div v-else>Добавить</div>
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
import { mapState } from "vuex";
import GlobalMenu from "@/components/UI/GlobalMenu";
import { dispatcherMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Путевой лист",
  },
  async created() {
    this.data.date = this.$correctDate(new Date());

    this.id = this.$route.params.id;
    if (this.id == "new" || this.id == null) {
      this.editStatus = false;
    } else {
      this.editStatus = true;
      const getData = {
        id: this.id,
      };
      this.$api.travelSheet
        .get(getData)
        .then((res) => (this.data = res))
        .then((res) => (this.data.date = this.$correctDate(this.data.date)))
        .catch((err) => {
          showErrorMessage(err);
        });
    }
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
      backRouter: "/dispatcher/travelSheet",
      isValid: false,
      validation: {
        acceptValid: false,
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      maxRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) =>
          (v && v.length <= 100) || "В поле не может быть больше 100 символов",
      ],
      maxNotRequiredRules: [
        (v) =>
          String(v).length <= 100 || "В поле не может быть больше 100 символов",
      ],
      minMaxRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) =>
          (v && v.length <= 100) || "В поле не может быть больше 100 символов",
        (v) => (v && v.length >= 5) || "В поле не может быть меньше 5 символов",
      ],
      transports: [],
      drivers: [],
      data: {
        date: new Date(),
        carId: "",
        driverId: "",
        orgId: "",
        startDriveTime: null,
        finishDriveTime: null,
      },
    };
  },
  methods: {
    fetchData() {
      this.$store
        .dispatch("organizations/fetchOrganizations")
        .catch((err) => showErrorMessage(err));

      this.$api.transport
        .fetch({})
        .then((res) => (this.transports = res))
        .catch((err) => {
          showErrorMessage(err);
        });

      this.$api.driver
        .fetch({})
        .then((res) => (this.drivers = res))
        .catch((err) => {
          showErrorMessage(err);
        });
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

      if (this.editStatus) {
        this.$api.travelSheet
          .update(this.data)
          .then((res) => {
            this.data = res;
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      } else {
        this.$api.travelSheet
          .create(this.data)
          .then((res) => {
            this.data = res;
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      }
    },
  },
  computed: {
    ...mapState("organizations", ["organizations"]),
  },
  watch: {
    //   "data.regAddress"(oldVal, newVal) {
    //     if (this.factEqualsReg) {
    //       this.data.factAddress = this.data.regAddress;
    //     }
    //   },
    //   factEqualsReg(oldVal, newVal) {
    //     if (!newVal) {
    //       this.data.factAddress = this.data.regAddress;
    //     }
    //   },
  },
};
</script>

<style lang="scss" scoped></style>
