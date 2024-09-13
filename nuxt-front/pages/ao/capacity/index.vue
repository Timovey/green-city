<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      :backButton="true"
      title="Ёмкости"
      @onPlus="onPlus()"
      v-model="drawer"
      :menu="menu"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <ContainersFilter
        :containerTypes="containerTypes"
        :garbageTypes="getTrash"
        @onSave="applyFilters()"
        v-model="filters"
      />
    </v-dialog>
    <DeleteItems
      v-if="selectedItem"
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Контейнер  ${selectedItem.containerId} | ${selectedItem.name} | ${selectedItem.trashTypeName}`"
    />

    <PageLayout>
      <v-text-field
        class="ma-4 mt-2"
        label="Поиск по наименованию"
        v-model="query"
        filled
        outlined
        append-icon="mdi-magnify"
      ></v-text-field>
      <v-data-table
        multi-sort
        item-key="containerId"
        show-select
        single-select
        :headers="headers"
        :items="filteredContainers"
        :items-per-page="20"
        @item-selected="onItemSelect"
      >
        <template v-slot:item.containerTypeId="{ item }">
          {{ getContainerType(item.containerTypeId) }}
        </template>
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
import ContainersFilter from "@/components/ao/ContainersFilter";
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import { mapState } from "vuex";
import { aoMenu } from "@/helpers/menus";
import { toArray } from "@/helpers/objectHelper";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  components: {
    ContainersFilter,
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
  },
  created() {
    this.$store
      .dispatch("ao.capacities/fetchContainers")
      .then((res) => console.error(this.containers));
    this.menu = aoMenu;
    this.$store.dispatch("ao.capacities/getContainerTypes").then((res) => {
      this.containerTypes = res;
      console.log(this.containerTypes);
    });
    this.$store.dispatch("ao.enums/fetchEnums").catch((err) => showErrorMessage(err));
  },
  data() {
    return {
      query: "",
      drawer: false,
      selectedItem: false,
      showFilters: false,
      showDeleteModal: false,
      containerTypes: [],
      filters: {},
      headers: [
        {
          text: "#",
          align: "start",
          sortable: false,
          value: "containerId",
        },
        { text: "Наименование", value: "name" },
        { text: "Тип мусора", value: "trashTypeName" },
        { text: "Вид контейнера", value: "containerTypeId" },
      ],
    };
  },
  methods: {
    getContainerType(id) {
      return this.containerTypes.find((item) => item.id == id)?.name;
    },
    onPlus() {
      this.$router.push("/ao/capacity/edit/new");
    },
    onEdit() {
      this.$router.push(`/ao/capacity/edit/${this.selectedItem.containerId}`);
    },
    onDelete() {
      let data = { containerIds: [this.selectedItem.containerId] };
      this.$store
        .dispatch("ao.capacities/deleteContainer", data)
        .then((res) => {
          this.showDeleteModal = false
          this.selectedItem = false
        });
    },
    onItemSelect(item) {
      if (item.value) this.selectedItem = item.item;
      else this.selectedItem = false;
    },
    applyFilters() {
      let data = {
        isFilter: true,
        ...this.filters,
        isFilterByCreatedUser: true,
        sorting: {},
      };
      this.$store.dispatch("ao.capacities/fetchContainers", data).catch((err) => {
        showErrorMessage(err);
      });
      this.showFilters = false;
    },
  },
  computed: {
    ...mapState("ao.capacities", ["containers"]),
    ...mapState("ao.enums", ["enums"]),
    getTrash() {
      return toArray(this.enums.Trash);
    },
    filteredContainers() {
      return this.containers.filter((item) => {
        return (
          item.name.trim().toLowerCase().indexOf(this.query.toLowerCase()) != -1
        );
      });
    },
  },
};
</script>
