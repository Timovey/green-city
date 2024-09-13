<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      :backButton="true"
      title="Контрагенты"
      @onPlus="onPlus()"
      v-model="drawer"
      :menu="menu"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <ContragentsFilter @onSave="applyFilters()" v-model="filters" />
    </v-dialog>
    <DeleteItems
      v-if="selectedItem"
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Контрагента  ${selectedItem.contractClientId} | ${selectedItem.clientName} | ${selectedItem.inn}`"
    />
    <PageLayout>
      <v-text-field
        class="ma-4 mt-2"
        v-model="query"
        filled
        label="Поиск по наименованию"
        persistence-hint
        outlined
        append-icon="mdi-magnify"
      ></v-text-field>
      <v-data-table
        multi-sort
        item-key="contractClientId"
        show-select
        single-select
        :headers="headers"
        :items="filteredContragents"
        :items-per-page="20"
        @item-selected="onItemSelect"
      />
      <TableActions
        :selectedItem="selectedItem"
        @onShowModal="showDeleteModal = true"
        @onEdit="onEdit"
      />
    </PageLayout>
  </div>
</template>

<script>
import { mapState } from "vuex";
import ContragentsFilter from "@/components/ao/ContragentsFilter";
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import { aoMenu } from "@/helpers/menus";
export default {
  name: "AoLayout",
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
    ContragentsFilter,
  },
  created() {
    this.menu = aoMenu;
    this.$store
      .dispatch("ao.contragents/fetchContragents")
      .catch((err) => console.log(err));
  },
  data() {
    return {
      query: "",
      drawer: false,
      showFilters: false,
      filters: {},
      menu: [],
      showDeleteModal: false,
      headers: [
        {
          text: "№",
          align: "start",
          sortable: false,
          value: "contractClientId",
        },
        { text: "Наименование", value: "clientName" },
        { text: "ИНН", value: "inn" },
        { text: "КПП", value: "kpp" },
        { text: "ОГРН", value: "ogrn" },
        { text: "Р/СЧ", value: "rschet" },
        { text: "Банк", value: "bank" },
        { text: "БИК", value: "bik" },
        { text: "К/СЧ", value: "kschet" },
        { text: "Юридический адрес", value: "legalAddress" },
        { text: "Почтовый адрес", value: "postalAddress" },
        { text: "Электронная почта", value: "email" },
        { text: "Руководитель", value: "supervizorName" },
        { text: "Должность", value: "supervizorPosition" },
        { text: "ФИО", value: "responsibleTkoPersonName" },
        { text: "Телефон", value: "responsibleTkoPersonPhone" },
        { text: "Контактный телефон", value: "clientContact" },
      ],
      selectedItem: false,
    };
  },
  methods: {
    onPlus() {
      this.$router.push("/ao/contragents/edit/new");
    },
    onEdit() {
      this.$router.push(`/ao/contragents/edit/${this.selectedItem.contractClientId}`);
    },
    onDelete() {
      let data = { contractClientId: this.selectedItem.contractClientId };
      this.$store
        .dispatch("ao.contragents/deleteContragent", data)
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
      this.$store.dispatch("ao.contragents/fetchContragents", data);
      this.showFilters = false;
    },
  },
  computed: {
    ...mapState("ao.contragents", ["contragents"]),
    filteredContragents() {
      return this.contragents.filter((item) => {
        return item.clientName.trim().toLowerCase().indexOf(this.query.toLowerCase()) != -1;
      });
    },
  },
};
</script>
