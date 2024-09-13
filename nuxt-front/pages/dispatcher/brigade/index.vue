<template>
  <div>
    <GlobalMenu @onShowFilters="showFilters = true" @onPlus="onPlus()" title="Бригады" :menu="menu" v-model="drawer" />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <BrigadeFilter @onSave="applyFilters()" v-model="filters" :routes="routes" :workshifts="workshifts" />
    </v-dialog>
    <PageLayout>
      <v-text-field class="ma-2" @change="applyFilters" v-model="filters.date" type="date" label="Дата" required outlined
        filled />
      <v-data-table multi-sort item-key="id" show-select single-select :headers="headers" :items="brigades"
        :items-per-page="10" @item-selected="onItemSelect" ref="table">
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
import BrigadeFilter from "@/components/dispatcher/BrigadeFilter.vue";
import { dispatcherMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Бригады",
  },
  name: "Brigades",
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
    BrigadeFilter,
  },
  created() {
    this.menu = dispatcherMenu;
    this.filters.date = this.$getTomorrowDate();
    this.fetchData();
  },
  data() {
    return {
      menu: [],
      baseUrlPage: "/dispatcher/brigade",
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {
        date: this.$getTomorrowDate(),
      },
      brigades: [],
      workshifts: [],
      routes: [],
      headers: [
        { text: "Машина + водитель", value: "workShiftInfo" },
        { text: "Рейсовая информация", value: "routeInfo" },
        { text: "Подсобные рабочие", value: "auxWorkersInfo" },
      ],
    };
  },
  methods: {
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
      console.log(data);
      this.$api.brigade
        .fetch(data)
        .then((res) => (this.brigades = res))
        .catch((err) => {
          showErrorMessage(err);
        });

      const getWorkshiftData = {
        dateTime: this.filters.date,
      };
      this.$api.dropdownWorker
        .fetchWorkhifts(getWorkshiftData)
        .then((res) => {
          this.workshifts = res;
        })
        .catch((err) => {
          showErrorMessage(err);
        });
      this.$api.route
        .fetchRoutes(this.filters)
        .then((res) => (this.routes = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    onDelete() {
      let data = { id: this.selectedItem.id };
      this.$api.brigade
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
    // "filters.date"(oldVal, newVal) {
    //   this.fetchData();
    // },
  },
};
</script>

<style lang="scss" scoped></style>
