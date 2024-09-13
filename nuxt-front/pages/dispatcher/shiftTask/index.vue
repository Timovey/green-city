<template>
  <div>
    <GlobalMenu @onShowFilters="showFilters = true" @onPlus="onPlus()" title="Сменные задания" :menu="menu"
      v-model="drawer" />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <!-- <AuxiliaryWorkerFilter
        @onSave="applyFilters()"
        v-model="filters"
        :organizations="organizations"
      /> -->
    </v-dialog>
    <DeleteItems v-model="showDeleteModal" @onDelete="onDelete()"
      :message="`Сменное задание ${selectedItem.carWithDriverInfo}`" />
    <PageLayout>
      <v-card-title>
        <v-text-field v-model="filters.date" filled type="date" label="Дата" :rules="requiredRules" required
          outlined></v-text-field>
      </v-card-title>
      <v-data-table multi-sort item-key="id" show-select single-select :headers="headers" :items="shiftTasks"
        :items-per-page="10" @item-selected="onItemSelect" ref="table">
        <template v-slot:item.printInfo="{ item }">
          <div class="d-flex flex-column flex">
            <a @click="downloadShiftTask(item.id)">Сменное задание</a>
            <a @click="downloadTravelSheet(item.travelSheetInfo)">Путевой лист</a>
          </div>
        </template>
      </v-data-table>
      <TableActions :selectedItem="selectedItem" :showEdit="false" @onShowModal="showDeleteModal = true" />
    </PageLayout>
  </div>
</template>

<script>
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
// import AuxiliaryWorkerFilter from "@/components/dispatcher/AuxiliaryWorkerFilter";
import { dispatcherMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Сменные задания",
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
      baseUrlPage: "/dispatcher/shiftTask",
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {
        date: "",
      },
      shiftTasks: [],
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      headers: [
        { text: "Номер путевого листа", value: "travelSheetInfo" },
        { text: "Организация", value: "orgInfo" },
        { text: "Машина + водитель", value: "carWithDriverInfo" },
        { text: "Рейсовая информация", value: "routesInfo" },
        { text: "Печать", value: "printInfo" },
      ],
    };
  },
  methods: {
    downloadShiftTask(id) {
      this.$api.report
        .printShiftTask({
          id: id,
        })
        .catch((error) => {
          showErrorMessage(error);
        });
    },
    downloadTravelSheet(id) {
      console.log(id);
      this.$api.report
        .printTravelSheet({
          id: +id,
        })
        .catch((error) => {
          showErrorMessage(error);
        });
    },
    onPlus() {
      this.$router.push(`${this.baseUrlPage}/create`);
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
      let data = {
        isFilter: true,
        isFilterByCreatedUser: false,
        ...this.filters,
        sorting: {},
      };
      this.$api.shiftTask
        .fetchShiftTask(data)
        .then((res) => (this.shiftTasks = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    onDelete() {
      let data = { id: this.selectedItem.id };
      this.$api.shiftTask
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
  computed: {},
  watch: {
    "filters.date"(oldVal, newVal) {
      this.fetchData();
    },
  },
};
</script>

<style lang="scss" scoped></style>
