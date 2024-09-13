<template>
  <div>
    <GlobalMenu
      :edit="true"
      title="Экипажи"
      v-model="drawer"
      :menu="menu"
      :backButton="false"
    />
    <PageLayout>
      <v-card-title>
        <PickDateV2
          label="Посмотреть на дату"
          :date.sync="dateTimeView"
          :clearable="false"
        />
      </v-card-title>
      <v-data-table
        show-expand
        :expanded.sync="expanded"
        multi-sort
        item-key="transportId"
        :headers="headers"
        :items="reformedTeams"
        :items-per-page="10"
      >
        <template v-slot:expanded-item="{ headers, item }">
          <td :colspan="headers.length">
            <div
              class="text-h6 align-center text--secondary d-flex pa-2"
              v-for="driver in item.drivers"
            >
              <div class="mr-4">Водитель:</div>
              <div class="mr-4">
                {{ driver.driverFio }}
              </div>
              <v-spacer />
              <div class="mr-4">График:</div>
              <div class="mr-4">
                {{ driver.driverWorkDate }}
              </div>
            </div>
          </td>
        </template>
      </v-data-table>
      <v-card-actions>
        <v-btn @click.stop="onActualize()" color="primary">
          Актуализировать смены
        </v-btn>
        <v-spacer />
        <PickDateV2
          label="На дату"
          :date.sync="dateTimeActualize"
          :clearable="true"
        />
      </v-card-actions>
    </PageLayout>
  </div>
</template>
<script>
import GlobalMenu from "@/components/UI/GlobalMenu";
import { taxerMenu } from "@/helpers/menus";
import PageLayout from "@/components/UI/PageLayout";
import { mapState } from "vuex";
import { showErrorMessage, showSuccess } from "@/helpers/messages";
import { post } from "@/helpers/axiosHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
import PickDateV2 from "@/components/UI/PickDateV2";
export default {
  head: {
    title: "Дежурные смены",
  },
  name: "Team",
  components: {
    GlobalMenu,
    PageLayout,
    PickDateV2,
  },
  async created() {
    this.menu = taxerMenu;
    this.$store
      .dispatch("tax.drivers/fetchDrivers")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.enums/fetchEnums")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("organizations/fetchOrganizations")
      .catch((err) => showErrorMessage(err));
    this.dateTimeView = this.getNowCorrectDate();
    this.fetchTeam();
  },
  data() {
    return {
      expanded: [],
      menu: [],
      drawer: false,
      baseUrlPage: "/tax/team",
      teams: [],
      dateTimeActualize: null,
      dateTimeView: null,
      headers: [
        { text: "Модель транспорта", value: "transportName" },
        { text: "Тип транспорта", value: "transportType" },
        { text: "Номер", value: "regNumber" },
        { text: "Организация", value: "transportOrganization" },
      ],
    };
  },
  methods: {
    getNowCorrectDate() {
      return this.$correctDate(new Date());
    },
    onActualize() {
      let dateTime = this.dateTimeActualize;
      post(this, "/api/Transport/ActualizeWorkShits", { dateTime })
        .then((res) => {
          this.fetchTeam();
          showSuccess();
        })
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchTeam() {
      let dateTime = this.dateTimeView;
      post(this, "/api/Transport/GetActualWorkShits", {
        dateTime,
      })
        .then((res) => {
          this.teams = deepCopy(res.data.data);
        })
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fillTransportData(data, item) {
      data[item.transportId].transportId = item.transportId;
      data[item.transportId].transportName = item.transportName;

      if (this.enums.Transport && this.enums.Transport[item.transportType]) {
        data[item.transportId].transportType =
          this.enums.Transport[item.transportType];
      }
      data[item.transportId].regNumber = item.regNumber;

      let org = this.organizations.find(
        (el) => el.id == item.transportOrganizationId
      );

      data[item.transportId].transportOrganization = !!org ? org.name : "-";
    },
  },
  computed: {
    ...mapState("tax.drivers", ["drivers"]),
    ...mapState("organizations", ["organizations"]),
    ...mapState("tax.enums", ["enums"]),
    reformedTeams() {
      let result = deepCopy(this.teams);
      let data = {};
      for (let item of result) {
        const { driverFio, driverWorkDateType } = { ...item };

        let driverWorkDate = "";
        if (
          this.enums.DriverWorkDate &&
          this.enums.DriverWorkDate[driverWorkDateType]
        ) {
          driverWorkDate = this.enums.DriverWorkDate[driverWorkDateType];
        }
        const driverData = {
          driverFio: driverFio,
          driverWorkDate: driverWorkDate,
        };

        if (!data[item.transportId]) {
          data[item.transportId] = {};
          this.fillTransportData(data, item);
          data[item.transportId].drivers = [driverData];
        } else {
          data[item.transportId].drivers.push(driverData);
        }
      }

      return Object.values(data);
    },
  },
  watch: {
    dateTimeView(oldVal, newVal) {
      this.fetchTeam();
    },
  },
};
</script>
<style lang="scss"></style>
