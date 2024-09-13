<template>
  <div>
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="ТС"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-card class="mt-5">
          <v-card-title class="text--secondary">
            <div v-if="!editStatus">Добавление ТС</div>
            <div v-else>Редактирование ТС</div>
          </v-card-title>
          <v-divider class="mx-4" />
          <v-form ref="form" v-model="isValid">
            <v-card-text>
              <v-text-field
                :rules="requiredRules"
                required
                filled
                v-model="transport.name"
                label="Название"
                outlined
                clearable
              />
              <v-select
                v-model="transport.organizationId"
                :items="organizations"
                item-text="name"
                clearable
                dense
                item-value="id"
                label="Огранизация"
                class="mt-4"
              >
              </v-select>
              <v-text-field
                required
                :rules="numberRules"
                filled
                v-model="transport.regNumber"
                label="Регистрационный номер"
                outlined
                clearable
              />
            </v-card-text>
            <v-card-title class="text--secondary">
              Основые параметры
            </v-card-title>
            <v-card-text>
              <v-text-field
                :rules="numberRules"
                required
                filled
                v-model="transport.sts"
                label="СТС"
                outlined
                clearable
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.bodyType"
                label="Тип кузова"
                outlined
                clearable
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.tankFence"
                label="Забор бака"
                outlined
                clearable
              />
              <!-- <v-text-field :rules="requiredRules" required filled v-model="transport.transportType"
                                label="Тип ТС" outlined clearable/> -->
              <v-select
                v-model="transport.transportType"
                :rules="requiredRules"
                required
                :items="getTransportTypes"
                item-text="name"
                clearable
                dense
                item-value="value"
                label="Тип транспорта"
                class="mt-4"
              >
              </v-select>
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.bodyColor"
                label="Цвет кузова"
                outlined
                clearable
              />
              <v-select
                multiple
                v-model="transport.tankTypes"
                :items="containerTypes"
                item-text="name"
                item-value="id"
                label="Тип бака"
                dense
                class="mt-4"
              >
              </v-select>
              <v-text-field
                :rules="maxNotRequiredNumberRules"
                filled
                v-model="transport.loadCapacity"
                label="Грузоподъемность"
                outlined
                clearable
              />
              <v-text-field
                :rules="requiredRules"
                filled
                v-model="transport.cargoVolume"
                label="Объем кузова"
                type="number"
                hide-spin-buttons
                outlined
                clearable
              />
              <v-text-field
                :rules="requiredRules"
                filled
                v-model="transport.cargoVolumeCoef"
                label="Объем кузова коэффицент"
                type="number"
                outlined
                clearable
              />
              <v-text-field
                filled
                disabled
                :value="getComputeCargo"
                label="Объем кузова c коэффицентом"
                outlined
              >
              </v-text-field>

              <v-text-field
                :rules="requiredRules"
                required
                filled
                v-model="transport.productionYear"
                label="Год выпуска"
                type="number"
                hide-spin-buttons
                outlined
                clearable
              />
            </v-card-text>
            <v-card-title class="text--secondary">
              Техническое состояние
            </v-card-title>
            <v-card-text>
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.spendingLimit"
                label="Лимит затрат"
                outlined
                clearable
                type="number"
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.gsmNumberWinterCity"
                label="Норма расхода ГСМ зима город"
                outlined
                clearable
                type="number"
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.gsmNumberWinterRoad"
                label="Номер расхода ГСМ зима трасса"
                outlined
                clearable
                type="number"
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.gsmNumberSummerCity"
                label="Номер расхода ГСМ лето город"
                outlined
                clearable
                type="number"
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.gsmNumberSummerRoad"
                label="Номер расхода ГСМ лето трасса"
                outlined
                clearable
                type="number"
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.normOneMotoHour"
                label="Норма на 1 моточас"
                outlined
                clearable
                type="number"
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.normOneAcademyHour"
                label="Норма на 1 академический час "
                outlined
                clearable
                type="number"
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.normOneMinute"
                label="Норма на 1 минуту "
                outlined
                clearable
                type="number"
              />
              <v-text-field
                filled
                v-model="transport.lastTO"
                label="Последнее ТО"
                type="date"
                outlined
                clearable
              />
              <v-text-field
                :rules="maxNotRequiredRules"
                filled
                v-model="transport.repairsTO"
                label="ТО и ремонты"
                outlined
                clearable
              />
            </v-card-text>

            <v-card-actions>
              <v-spacer />
              <v-btn color="error lighten-2" @click="onBack()">
                <div>Отмена</div>
              </v-btn>
              <v-btn @click.stop="onSubmit()" color="primary">
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
import { validationMixin } from "vuelidate";
import { mapState } from "vuex";
import GlobalMenu from "@/components/UI/GlobalMenu";
import { taxerMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import { toArray } from "@/helpers/objectHelper";
export default {
  head: {
    title: "ТС",
  },
  components: {
    GlobalMenu,
  },
  async created() {
    this.id = this.$route.params.id;
    if (this.id == "new") {
      this.editStatus = false;
    } else {
      this.editStatus = true;
    }
    this.menu = taxerMenu;
    if (this.editStatus) {
      this.$store
        .dispatch("tax.transports/getTransport", { transportIds: [this.id] })
        .then((res) => {
          this.transport = res[0];
        })
        .catch((err) => showErrorMessage(err));
    }
    this.$store
      .dispatch("organizations/fetchOrganizations")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.enums/fetchEnums")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("ao.capacities/getContainerTypes")
      .catch((err) => showErrorMessage(err));
  },
  mixins: [validationMixin],
  data: () => ({
    backRouter: "/tax/transports",
    errors: false,
    isValid: false,
    drawer: false,
    editStatus: false,
    menu: [],
    id: false,
    transport: {
      name: "",
      organizationId: null,
      regNumber: "",
      sts: "",
      transportType: "",
      bodyType: "",
      bodyColor: "",
      tankFence: "",
      tankTypes: [],
      loadCapacity: "",
      cargoVolume: null,
      cargoVolumeCoef: null,
      productionYear: "",
      spendingLimit: "",
      gsmNumberWinterCity: null,
      gsmNumberWinterRoad: null,
      gsmNumberSummerCity: null,
      gsmNumberSummerRoad: null,
      normOneMotoHour: null,
      normOneAcademyHour: null,
      normOneMinute: null,
      lastTO: null,
      repairsTO: "",
    },
    requiredRules: [
      (v) => String(v).length > 0 || "Необходимо заполнить поле",
      (v) => !!v || "Необходимо заполнить поле",
    ],
    numberRules: [
      (v) => String(v).length > 0 || "Необходимо заполнить поле",
      (v) => v.length < 32 || "В поле должно быть меньше 32 символов",
    ],
    maxNotRequiredNumberRules: [
      (v) => String(v).length < 32 || "В поле должно быть меньше 32 символов",
    ],
    maxNotRequiredRules: [
      (v) => String(v).length < 100 || "В поле должно быть меньше 100 символов",
    ],
  }),
  methods: {
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) {
        showErrorMessage("Проверьте данные");
        return;
      }

      if (!this.editStatus) {
        this.$store
          .dispatch(`tax.transports/addTransport`, this.transport)
          .then((res) => {
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      } else {
        this.$store
          .dispatch(`tax.transports/updateTransport`, this.transport)
          .then((res) => {
            showSuccess();
            this.$router.push("/tax/transports");
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      }
    },
    onBack() {
      this.$router.push(this.backRouter);
    },
  },
  computed: {
    ...mapState("tax.transports", ["transports"]),
    ...mapState("tax.enums", ["enums"]),
    ...mapState("organizations", ["organizations"]),
    ...mapState("ao.capacities", ["containerTypes"]),
    getTransportTypes() {
      return toArray(this.enums.Transport);
    },
    getComputeCargo() {
      if (!this.transport.cargoVolume && !this.transport.cargoVolumeCoef) {
        return "";
      }
      return this.transport.cargoVolume * this.transport.cargoVolumeCoef;
    },
  },
};
</script>
<style lang="scss" scoped></style>
