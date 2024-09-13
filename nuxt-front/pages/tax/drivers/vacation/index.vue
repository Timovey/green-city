<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      title="Отпуска/больничные"
      @onPlus="onPlus()"
      v-model="drawer"
      :menu="menu"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <VacationFilter
        @onSave="applyFilters()"
        v-model="filters"
        :drivers="drivers"
        :vacationTypes="getDriverVacations"
      />
    </v-dialog>
    <v-dialog width="70%" v-model="addModal">
      <AddVacation
        @onSave="onAdd"
        :addingErrors="serverErrors"
        :drivers="drivers"
        :vacationTypes="getDriverVacations"
      />
    </v-dialog>
    <DeleteItems
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Отпуск/больничный водителя ${selectedItem.driverFio}`"
    />
    <PageLayout>
      <v-data-table
        show-expand
        :expanded.sync="expanded"
        multi-sort
        item-key="driverId"
        :headers="headers"
        :items="reformedVacations"
        :items-per-page="10"
        @item-selected="onItemSelect"
      >
        <template v-slot:expanded-item="{ headers, item }">
          <td :colspan="headers.length">
            <div
              class="text-h6 align-center text--secondary d-flex pa-2"
              v-for="vacation in item.vacations"
            >
              <div class="mr-4">Сменный водитель:</div>
              <div class="mr-4">
                {{ vacation.replacementDriverFio }}
              </div>
              <v-spacer />
              <div class="mr-4">Дата создания:</div>
              <v-chip class="mr-4">
                {{ vacation.dateRange.createDate }}
              </v-chip>
              <div class="mr-4">
                {{ vacation.vacationType }}
              </div>
              <v-chip>
                {{ vacation.dateRange.startDate }}
              </v-chip>
              <div class="pa-2">—</div>
              <v-chip>
                {{ vacation.dateRange.endDate }}
              </v-chip>
              <v-btn
                @click="onDeleteModal(vacation)"
                color="error"
                class="ml-2"
                fab
                x-small
              >
                <v-icon> mdi-delete </v-icon>
              </v-btn>
            </div>
          </td>
        </template>
      </v-data-table>
      <!-- <TableActions :selectedItem="selectedItem" @onShowModal="showDeleteModal = true" @onEdit="onEdit" /> -->
    </PageLayout>
  </div>
</template>
<script>
import GlobalMenu from "@/components/UI/GlobalMenu";
import { taxerMenu } from "@/helpers/menus";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import VacationFilter from "@/components/tax/VacationFilter.vue";
import AddVacation from "@/components/tax/AddVacation.vue";
import { mapState } from "vuex";
import { showErrorMessage, showSuccess } from "@/helpers/messages";
import { deepCopy } from "@/helpers/deepCopyHelper";
import { toArray } from "@/helpers/objectHelper";
export default {
  head: {
    title: "Отпуска/Больничные",
  },
  name: "DriverVacations",
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
    AddVacation,
    VacationFilter,
  },
  created() {
    this.menu = taxerMenu;
    this.$store
      .dispatch("tax.vacations/fetchDriverVacations")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.drivers/fetchDrivers")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("organizations/fetchOrganizations")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.enums/fetchEnums")
      .catch((err) => showErrorMessage(err));
  },
  data() {
    return {
      expanded: [],
      menu: [],
      serverErrors: false,
      addModal: false,
      baseUrlPage: "/tax/drivers/vacation",
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {},
      headers: [
        { text: "Водитель", value: "driverFio" },
        { text: "Должность", value: "driverPosition" },
        { text: "Организация", value: "organization" },
      ],
    };
  },
  methods: {
    onPlus() {
      this.addModal = true;
    },
    onEdit() {
      this.$router
        .push(`${this.baseUrlPage}/edit/${this.selectedItem.driverVacationId}`)
        .then((res) => {
          showSuccess();
        })
        .catch((err) => showErrorMessage(err));
    },
    onDelete() {
      let data = { driverVacationIds: [this.selectedItem.driverVacationId] };
      this.$store
        .dispatch("tax.vacations/deleteDriverVacation", data)
        .then((res) => {
          showSuccess();
        })
        .catch((err) => showErrorMessage(err));
      this.showDeleteModal = false;
    },
    onItemSelect(item) {
      if (item.value) this.selectedItem = item.item;
      else this.selectedItem = false;
    },
    applyFilters() {
      let data = {
        isFilter: true,
        isFilterByCreatedUser: true,
        ...this.filters,
        sorting: {},
      };
      this.$store
        .dispatch("tax.vacations/fetchDriverVacations", data)
        .catch((err) => showErrorMessage(err));
      this.showFilters = false;
    },
    onAdd(data) {
      this.$store
        .dispatch("tax.vacations/addDriverVacation", data)
        .then((res) => {
          showSuccess();
          this.serverErrors = false;
          this.addModal = false;
          data.reset();
        })
        .catch((err) => {
          this.serverErrors = err;
          showErrorMessage(err);
        });
    },
    onDeleteModal(item) {
      this.selectedItem = item;
      this.showDeleteModal = true;
    },
    getVacationData(item) {
      let vacation = { ...deepCopy(item) };
      if (item.replacementDriverId && item.replacementDriverName) {
        vacation.replaceDriver = item.replacementDriverName;
      } else {
        vacation.replaceDriver = "-";
      }

      if (
        this.enums.DriverVacation &&
        this.enums.DriverVacation[item.vacationType]
      ) {
        vacation.vacationType = this.enums.DriverVacation[item.vacationType];
      } else {
        vacation.vacationType = "Неизвестно";
      }
      return vacation;
    },
    fillDriverData(data, item) {
      data[item.driverId].driverId = item.driverId;
      data[item.driverId].driverFio = item.driverFio;
      if (
        this.enums.DriverPosition &&
        this.enums.DriverPosition[item.driverPositionType]
      ) {
        data[item.driverId].driverPosition =
          this.enums.DriverPosition[item.driverPositionType];
      }
      let org = this.organizations.find(
        (el) => el.id == item.driverOrganizationId
      );

      data[item.driverId].organization = org == null ? "-" : org.name;
    },
  },
  computed: {
    ...mapState("tax.drivers", ["drivers"]),
    ...mapState("tax.vacations", ["driverVacations"]),
    ...mapState("organizations", ["organizations"]),
    ...mapState("tax.enums", ["enums"]),
    reformedVacations() {
      let result = deepCopy(this.driverVacations);
      let data = {};
      for (let item of result) {
        if (!data[item.driverId]) {
          data[item.driverId] = {};

          this.fillDriverData(data, item);

          data[item.driverId].vacations = [
            {
              ...this.getVacationData(item),
            },
          ];
        } else {
          // console.log(data[item.driverId])
          data[item.driverId].vacations.push({
            ...this.getVacationData(item),
          });
        }
      }
      let resultData = [];
      for (let item of Object.keys(data)) {
        resultData.push(data[item]);
      }
      return resultData;
    },
    getDriverVacations() {
      return toArray(this.enums.DriverVacation);
    },
  },
};
</script>
<style lang="scss"></style>
