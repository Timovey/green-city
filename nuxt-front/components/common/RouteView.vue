<template>
  <div>
    <GlobalMenu @onShowFilters="showFilters = true" @onPlus="onPlus()" title="Рейсы" :menu="menu" v-model="drawer"
      :edit="isCanAdd" />
    <!-- <v-dialog scrollable width="80%" v-model="showFilters"> -->
    <!-- <AuxiliaryWorkerFilter
            @onSave="applyFilters()"
            v-model="filters"
          /> -->
    <!-- </v-dialog> -->
    <DeleteItems v-model="showDeleteModal" @onDelete="onDelete()" :message="`Рейс ${selectedItem.routeName}`" />
    <PageLayout>
      <v-card-title>
        <v-text-field v-model="filters.date" filled type="date" label="Дата" :rules="requiredRules" required
          outlined></v-text-field>
      </v-card-title>
      <v-data-table multi-sort item-key="id" show-select single-select :headers="headers" :items="routes"
        :items-per-page="10" @item-selected="onItemSelect" ref="table">
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
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
  },
  created() {
    this.filters.date = this.$getTomorrowDate();
    this.fetchData();
  },
  data() {
    return {
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {
        date: "",
      },
      routes: [],
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      headers: [
        { text: "Название", value: "routeName" },
        { text: "Транспорт.", value: "carInfo" },
        { text: "Водитель", value: "driverInfo" },
      ],
    };
  },
  props: {
    menu: [],
    baseUrlPage: {
      type: String,
      required: true,
    },
    isCanAdd: {
      type: Boolean,
      required: false,
      default: false,
    },
  },
  methods: {
    onPlus() {
      this.$router.push(`${this.baseUrlPage}/edit/new`);
    },
    onEdit() {
      this.$router.push(`${this.baseUrlPage}/edit/${this.selectedItem.id}`);
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
        ...this.filters,
        sorting: {},
      };
      this.$api.route
        .fetchRoutes(data)
        .then((res) => (this.routes = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    onDelete() {
      let data = { id: this.selectedItem.id };
      this.$api.route
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
  watch: {
    "filters.date"(oldVal, newVal) {
      this.fetchData();
    },
  },
};
</script>

<style lang="scss" scoped></style>
