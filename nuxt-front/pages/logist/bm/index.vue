<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      @onPlus="onPlus()"
      title="БМ"
      :menu="menu"
      v-model="drawer"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <BmFilter @onSave="applyFilters()" v-model="filters" />
    </v-dialog>
    <DeleteItems
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Базовый маршрут ${selectedItem.name}`"
    />
    <PageLayout>
      <v-data-table
        multi-sort
        item-key="id"
        show-select
        single-select
        :headers="headers"
        :items="bms"
        :items-per-page="10"
        @item-selected="onItemSelect"
        ref="bmTable"
      >
      </v-data-table>
      <TableActions
        :selectedItem="selectedItem"
        @onShowModal="showDeleteModal = true"
        @onEdit="onEdit"
      />
    </PageLayout>
  </div>
</template>
<script>
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import BmFilter from "@/components/logist/BmFilter";
import { logistMenu } from "@/helpers/menus";
import { mapState } from "vuex";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Базовые маршруты",
  },
  name: "Bms",
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
    BmFilter,
  },
  created() {
    this.menu = logistMenu;
    this.$store
      .dispatch("logist.bm/fetchBms")
      .catch((err) => showErrorMessage(err));
  },
  data() {
    return {
      menu: [],
      baseUrlPage: "/logist/bm",
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {},
      headers: [
        {
          text: "№",
          align: "start",
          sortable: false,
          value: "id",
        },
        { text: "Название", value: "name" },
      ],
    };
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
      let data = {
        isFilter: true,
        ...this.filters,
        sorting: {},
      };
      this.$store.dispatch("logist.bm/fetchBms", data).catch((err) => {
        showErrorMessage(err);
      });
      this.showFilters = false;
      //убираем выделение элемента, т.к. его может и не быть
      this.selectedItem = false;
      this.$refs.bmTable.selection = [];
    },
    onDelete() {
      let data = { id: this.selectedItem.id };
      this.$store
        .dispatch("logist.bm/deleteBm", data)
        .then((res) => {
          showSuccess();
        })
        .catch((err) => {
          showErrorMessage(err);
        });
      this.showDeleteModal = false;
      this.selectedItem = false;
    },
  },
  computed: {
    ...mapState("logist.bm", ["bms"]),
  },
};
</script>
<style></style>
