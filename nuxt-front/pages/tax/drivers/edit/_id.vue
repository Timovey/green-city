<template>
  <div>
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="Водитель"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="formDriver">
            <v-card-title class="text--secondary">
              <div v-if="!editStatus">Добавление водителя</div>
              <div v-else>Редактирование водителя</div>
            </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text class="pb-2">
              <v-text-field
                v-model="data.tableNumber"
                filled
                label="Таб. №"
                :rules="maxRules"
                required
                outlined
                clearable
              ></v-text-field>

              <v-text-field
                v-model="data.driverSurname"
                filled
                label="Фамилия водителя"
                :rules="maxRules"
                required
                outlined
                clearable
              ></v-text-field>

              <v-text-field
                v-model="data.driverName"
                filled
                label="Имя водителя"
                :rules="maxRules"
                required
                outlined
                clearable
              ></v-text-field>

              <v-text-field
                v-model="data.driverPatronymic"
                filled
                label="Отчество водителя"
                :rules="maxNotRequiredRules"
                required
                outlined
                clearable
              ></v-text-field>

              <v-text-field
                v-model="data.birthDate"
                type="date"
                label="Дата рождения"
                :rules="requiredRules"
                required
                outlined
                clearable
                filled
              ></v-text-field>

              <v-text-field
                v-model="data.birthPlace"
                :rules="maxNotRequiredRules"
                label="Место рождения"
                outlined
                clearable
                filled
              ></v-text-field>
            </v-card-text>
            <v-card-title class="text--secondary">
              Паспортные данные
            </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text>
              <v-text-field
                v-model="data.passportSerie"
                :rules="serieRules"
                label="Серия паспорта"
                outlined
                clearable
                filled
                type="number"
                hide-spin-buttons
              ></v-text-field>
              <v-text-field
                v-model="data.passportNumber"
                :rules="numberRules"
                label="Номер паспорта"
                outlined
                clearable
                filled
                type="number"
                hide-spin-buttons
              ></v-text-field>

              <v-text-field
                v-model="data.issueDate"
                type="date"
                label="Дата выдачи"
                :rules="requiredRules"
                required
                outlined
                clearable
                filled
              ></v-text-field>

              <v-text-field
                v-model="data.devisionCode"
                label="Код подразделения"
                :rules="devisionRules"
                required
                outlined
                clearable
                filled
              ></v-text-field>

              <v-text-field
                v-model="data.issuer"
                :rules="maxNotRequiredRules"
                label="Кем выдан"
                outlined
                clearable
                filled
              ></v-text-field>

              <v-text-field
                v-model="data.regAddress"
                :rules="maxNotRequiredRules"
                label="Адрес регистрации"
                outlined
                clearable
                filled
              ></v-text-field>

              <v-checkbox
                v-model="factEqualsReg"
                label="Совпадает с фактическим"
              ></v-checkbox>
              <v-text-field
                :disabled="factEqualsReg"
                v-model="data.factAddress"
                :rules="maxNotRequiredRules"
                label="Адрес фактический"
                outlined
                clearable
                filled
              ></v-text-field>
            </v-card-text>
            <v-divider class="mx-4"></v-divider>
            <v-card-text>
              <PickDateV2
                label="Дата принятия на работу"
                :date.sync="data.acceptWorkDate"
                :rules="requiredRules"
              />
            </v-card-text>
            <v-divider class="mx-4"></v-divider>
            <v-card-title class="pt-0 text--secondary">
              Информация об увольнении
            </v-card-title>
            <v-card-text>
              <v-checkbox
                v-model="data.isFired"
                label="Уволен"
                outlined
                clearable
                filled
              >
              </v-checkbox>
              <PickDateV2
                :disabled="!data.isFired"
                label="Дата увольнения"
                :date.sync="data.firedDate"
              />
            </v-card-text>
            <v-divider class="mx-4"></v-divider>
            <v-card-title class="pt-0 text--secondary">
              Должностная информация
            </v-card-title>
            <v-card-text class="pb-2">
              <v-select
                v-model="data.driverPositionType"
                :items="getDriverPositions"
                :rules="requiredRules"
                required
                item-text="name"
                clearable
                item-value="value"
                label="Должность"
                dense
                class="mt-4"
              >
              </v-select>

              <v-select
                v-model="data.organizationId"
                :items="organizations"
                item-text="name"
                clearable
                item-value="id"
                label="Огранизация"
                dense
                class="mt-4"
              >
              </v-select>

              <v-text-field
                v-model="data.skillClass"
                label="Квалиф. разряд"
                filled
                outlined
                :rules="requiredRules"
                required
                clearable
              ></v-text-field>

              <v-select
                v-model="data.drivingSkills"
                :items="getDrivingSkills"
                :rules="requiredRules"
                required
                item-text="name"
                clearable
                item-value="value"
                label="Чем может усправлять"
                dense
                class="mt-4"
                multiple
              >
              </v-select>

              <v-text-field
                type="tel"
                v-model="data.contactNumber"
                label="Телефон"
                filled
                outlined
                :rules="telRules"
                required
                clearable
              ></v-text-field>

              <v-text-field
                v-model="data.driversLicense"
                :rules="maxNotRequiredRules"
                label="Водительское удостоверение"
                outlined
                clearable
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
import PickDateV2 from "@/components/UI/PickDateV2";
import { taxerMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import { toArray } from "@/helpers/objectHelper";
export default {
  head: {
    title: "Водитель",
  },
  async created() {
    this.id = this.$route.params.id;
    if (this.id == "new" || this.id == null) {
      this.editStatus = false;
    } else {
      this.editStatus = true;
      this.$store
        .dispatch("tax.drivers/getDriver", { driverIds: [this.id] })
        .then((res) => (this.data = res));
    }
    this.menu = taxerMenu;
    this.$store
      .dispatch("organizations/fetchOrganizations")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.enums/fetchEnums")
      .catch((err) => showErrorMessage(err));
  },
  components: {
    GlobalMenu,
    PickDateV2,
  },
  data() {
    return {
      menu: [],
      editStatus: false,
      id: false,
      drawer: false,
      backRouter: "/tax/drivers",
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
      telRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        // v => /^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$/.test(v) || 'Неверный формат телефона',
        (v) =>
          /^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$/.test(v) ||
          "Неверный формат телефона",
      ],
      minMaxRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) =>
          (v && v.length <= 100) || "В поле не может быть больше 100 символов",
        (v) => (v && v.length >= 5) || "В поле не может быть меньше 5 символов",
      ],
      serieRules: [
        (v) => (v && v.length <= 4) || "Серия не может быть больше 4 символов",
        (v) => /^[0-9]*$/.test(v) || "Серия состоит только из цифр",
      ],
      numberRules: [
        (v) => (v && v.length <= 6) || "Номер не может быть больше 6 символов",
        (v) => /^[0-9]*$/.test(v) || "Номер состоит только из цифр",
      ],
      devisionRules: [
        (v) =>
          String(v).length <= 7 ||
          "Код подразделения не может быть больше 7 символов",
      ],

      factEqualsReg: false,
      data: {
        tableNumber: "",
        driverName: "",
        driverSurname: "",
        driverPatronymic: "",
        acceptWorkDate: "",
        organizationId: null,
        driverPositionType: "",
        skillClass: "",
        drivingSkills: [],
        contactNumber: "",
        driversLicense: "",
        birthDate: "",
        birthPlace: "",
        regAddress: "",
        factAddress: "",
        passportSerie: "",
        passportNumber: "",
        issueDate: "",
        issuer: "",
        devisionCode: "",
        isFired: false,
      },
    };
  },
  methods: {
    onBack() {
      this.$router.push(this.backRouter);
    },
    onSubmit() {
      this.$refs.formDriver.validate();
      if (!this.isValid) {
        console.log("неВалидно");
        showErrorMessage("Проверьте данные");
        return;
      }

      if (this.editStatus) {
        this.$store
          .dispatch("tax.drivers/updateDriver", this.data)
          .then((res) => {
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      } else {
        this.$store
          .dispatch("tax.drivers/addDriver", this.data)
          .then((res) => {
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      }
    },
    weekDay(date) {
      return new Date(date).toLocaleString("ru-RU", { weekday: "short" });
    },
  },
  computed: {
    ...mapState("organizations", ["organizations"]),
    ...mapState("tax.enums", ["enums"]),
    getDriverPositions() {
      return toArray(this.enums.DriverPosition);
    },
    getDrivingSkills() {
      return toArray(this.enums.DrivingSkills);
    },
  },
  watch: {
    "data.regAddress"(oldVal, newVal) {
      if (this.factEqualsReg) {
        this.data.factAddress = this.data.regAddress;
      }
    },
    factEqualsReg(oldVal, newVal) {
      if (!newVal) {
        this.data.factAddress = this.data.regAddress;
      }
    },
  },
};
</script>
<style lang="scss" scoped>
.error__messages {
  display: flex;
  flex-direction: column;
}

.error__message {
  color: #dd2c00;
  line-height: 12px;
  font-size: 12px;
}
</style>
