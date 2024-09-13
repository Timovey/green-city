<template>
  <div>
    <GlobalMenu
      :edit="true"
      title="План рабочих смен"
      v-model="drawer"
      :menu="menu"
      :backButton="false"
    />
    <v-row>
      <v-col class="col col-12">
        <v-card class="mt-5">
          <v-form ref="form" v-model="valid" lazy-validation>
            <v-card-text class="d-flex mt-2">
              <v-select
                :rules="requiredRules"
                required
                v-model="data.transportId"
                :items="transports"
                clearable
                filled
                rounded
                item-value="id"
                label="Транспорт"
              >
                <template v-slot:selection="{ item }">
                  {{ item.name + " " + item.regNumber }}
                </template>
                <template v-slot:item="{ item }">
                  {{ item.name + " " + item.regNumber }}
                </template>
              </v-select>
            </v-card-text>
            <v-card-text class="d-flex mt-2">
              <PickDateV2
                label="От"
                :date.sync="data.dateFrom"
                :rules="dateRules"
              />
              <PickDateV2
                label="До"
                :date.sync="data.dateTo"
                :rules="dateRules"
              />
            </v-card-text>
            <v-card-actions>
              <v-spacer />
              <v-btn @click.stop="onSubmit()" color="primary">
                Сформировать
              </v-btn>
            </v-card-actions>
          </v-form>
          <v-divider />
          <div v-if="Object.keys(workShift).length">
            <v-card-title class="text--secondary"> График </v-card-title>
            <v-card-text>
              <WorkShiftActions @onPrev="listCount--" @onNext="listCount++" />
              <v-row>
                <v-col class="col-6">
                  <WorkShiftCalendar
                    :enums="enums"
                    :workShift="workShift"
                    :listCount="listCount"
                  />
                </v-col>
                <v-col class="col-6">
                  <WorkShiftDataTable
                    class="elevation-1"
                    :enums="enums"
                    :workShift="workShift"
                    :listCount="listCount"
                    :drivers="drivers"
                    v-model="compute"
                  />
                </v-col>
              </v-row>
            </v-card-text>
          </div>
          <v-alert
            v-else
            border="bottom"
            colored-border
            type="info"
            elevation="2"
            color="primary"
          >
            Графики отобразятся после формирования
          </v-alert>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>
<script>
import PickDateV2 from "@/components/UI/PickDateV2.vue";
import GlobalMenu from "@/components/UI/GlobalMenu";
import { taxerMenu } from "@/helpers/menus";
import PageLayout from "@/components/UI/PageLayout";
import { mapState } from "vuex";
import { showErrorMessage } from "@/helpers/messages";
import WorkShiftDataTable from "@/components/tax/WorkShiftDataTable";
import WorkShiftCalendar from "@/components/tax/WorkShiftCalendar";
import WorkShiftActions from "@/components/tax/WorkShiftActions";
import { post } from "@/helpers/axiosHelper";
export default {
  head: {
    title: "Графики",
  },
  name: "WorkShifts",
  components: {
    GlobalMenu,
    PageLayout,
    WorkShiftCalendar,
    WorkShiftActions,
    WorkShiftDataTable,
    PickDateV2,
  },
  async created() {
    this.menu = taxerMenu;
    this.$store
      .dispatch("tax.transports/fetchTransports")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.drivers/fetchDrivers")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.enums/fetchEnums")
      .catch((err) => showErrorMessage(err));
  },
  data() {
    return {
      listCount: null,
      menu: [],
      baseUrlPage: "/tax/workshifts",
      errors: false,
      valid: false,
      drawer: false,
      data: {
        transportId: null,
        dateFrom: null,
        dateTo: null,
      },
      workShift: {},
      requiredRules: [(v) => !!v || "Необходимо заполнить поле"],
      dateRules: [(v) => !!v || "Необходимо заполнить поле"],
      compute: 0,
    };
  },
  methods: {
    onSubmit() {
      this.$refs.form.validate();
      if (!this.valid) {
        showErrorMessage("Невалидная форма");
        return;
      }
      post(this, "/api/Transport/GetTransportSheduleDateRange", this.data)
        .then((res) => {
          this.workShift = res.data.data;
          this.listCount = 0;
          this.compute++;
        })
        .catch((err) => {
          showErrorMessage(err);
        });
    },
  },
  computed: {
    ...mapState("tax.drivers", ["drivers"]),
    ...mapState("tax.transports", ["transports"]),
    ...mapState("tax.enums", ["enums"]),
  },
};
</script>
<style lang="scss"></style>
