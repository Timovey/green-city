<template>
  <div>
    <GlobalMenu @onShowFilters="showFilters = true" @onPlus="onPlus()" title="Путевые листы" :menu="menu"
      v-model="drawer" />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <!-- <AuxiliaryWorkerFilter
          @onSave="applyFilters()"
          v-model="filters"
          :organizations="organizations"
        /> -->
    </v-dialog>
    <DeleteItems v-model="showDeleteModal" @onDelete="onDelete()"
      :message="`Путевой лист ${selectedItem.numberToShow}`" />
    <PageLayout>
      <v-card-title>
        <v-text-field v-model="filters.date" filled type="date" label="Дата" :rules="requiredRules" required
          outlined></v-text-field>
      </v-card-title>
      <v-data-table multi-sort item-key="id" show-select single-select :headers="headers" :items="travelSheets"
        :items-per-page="10" @item-selected="onItemSelect" ref="table">
        <template v-slot:item.printInfo="{ item }">
          <a @click="downloadTravelSheet(item.id)">Путевой лист</a>
        </template>
      </v-data-table>
      <TableActions :selectedItem="selectedItem" @onShowModal="showDeleteModal = true" @onEdit="onEdit" />
    </PageLayout>
  </div>
</template>

<script>
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import { mapState } from "vuex";
// import AuxiliaryWorkerFilter from "@/components/dispatcher/AuxiliaryWorkerFilter";
import { dispatcherMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Путевые листы",
  },
  name: "ShiftTask",
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
  },
  created() {
    this.menu = dispatcherMenu;
    this.filters.date = this.$getTomorrowDate();
    this.fetchData();
  },
  data() {
    return {
      menu: [],
      baseUrlPage: "/dispatcher/travelSheet",
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {
        date: "",
      },
      travelSheets: [],
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      headers: [
        { text: "Номер путевого листа", value: "numberToShow" },
        { text: "Организация", value: "orgInfo" },
        { text: "Машина водитель", value: "carInfo" },
        { text: "Водитель", value: "driverInfo" },
        { text: "Время выезда", value: "startDriveTime" },
        { text: "Время заезда", value: "finishDriveTime" },
        { text: "Печать", value: "printInfo" },
      ],
    };
  },
  methods: {
    async downloadTravelSheet(id) {
      this.$api.report
        .printTravelSheet({
          id: id,
        })
        .catch((error) => {
          showErrorMessage(error);
        });
    },
    onEdit() {
      this.$router.push(`${this.baseUrlPage}/edit/${this.selectedItem.id}`);
    },
    onPlus() {
      this.$router.push(`${this.baseUrlPage}/edit/new`);
    },
    onItemSelect(item) {
      if (item.value) this.selectedItem = item.item;
      else this.selectedItem = false;
    },
    applyFilters() {
      this.fetchData();
      this.showFilters = false;
      //убираем выделение элемента, т.к. его может и не быть
      this.selectedItem = false;
      this.$refs.table.selection = [];
    },
    fetchData() {
      if (!this.$moment(this.filters.date).isValid()) {
        return;
      }
      let data = {
        isFilter: true,
        isFilterByCreatedUser: false,
        ...this.filters,
        sorting: {},
      };

      this.$api.travelSheet
        .fetch(data)
        .then((res) => (this.travelSheets = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    onDelete() {
      let data = { id: this.selectedItem.id };
      this.$api.travelSheet
        .delete(data)
        .then((res) => {
          showSuccess();
          this.fetchData();
        })
        .catch((err) => {
          showErrorMessage(err);
        });

      this.showDeleteModal = false;
      this.selectedItem = false;
    },
  },
  computed: {
    ...mapState("organizations", ["organizations"]),
  },
  watch: {
    "filters.date"(oldVal, newVal) {
      this.fetchData();
    },
  },
};
</script>

<style lang="scss" scoped></style>
