<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      :backButton="true"
      title="Договоры"
      @onPlus="onPlus()"
      v-model="drawer"
      :menu="menu"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <ContractFilter
        :containers="containers"
        :contragents="contragents"
        @onSave="applyFilters()"
        v-model="filters"
      />
    </v-dialog>
    <DeleteItems
      v-if="selectedItem"
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Договор  ${selectedItem.contractId} | ${selectedItem.sendDocsType} | ${selectedItem.comment}`"
    />

    <PageLayout>
      <v-text-field
        class="ma-4 mt-2"
        label="Поиск по номеру договора"
        v-model="query"
        filled
        outlined
        append-icon="mdi-magnify"
      ></v-text-field>
      <v-data-table
        multi-sort
        item-key="contractId"
        show-select
        single-select
        :headers="headers"
        :items="filteredContracts"
        :items-per-page="20"
        @item-selected="onItemSelect"
      >
        <template v-slot:item.startDate="{ item }">
          <div class="text-h6 align-center text--secondary d-flex pa-2">
            <v-spacer />
            <v-chip>
              {{ $correctDate(item.startDate) }}
            </v-chip>
            <div class="pa-2">—</div>
            <v-chip v-if="item.endDate">
              {{ $correctDate(item.endDate) }}
            </v-chip>
          </div>
        </template>
        <template v-slot:item.contractClient="{ item }">
          <div class="align-center text--secondary flex-column d-flex pa-2">
            {{ item.contractClient && item.contractClient.clientName ? item.contractClient.clientName : "" }}
            <div>ИНН: {{ item.contractClient && item.contractClient.inn ? item.contractClient.inn : "" }}</div>
          </div>
        </template>
        <template v-slot:item.contractStatus="{ item }">
          <div class="d-flex justify-center" dark>
            <v-icon :color="getColor(item.contractStatus)" v-if="item.contractStatus">
              mdi-checkbox-marked-circle
            </v-icon>
            <v-icon :color="getColor(item.contractStatus)" v-else>
              mdi-minus-circle
            </v-icon>
          </div>
        </template>
        <template v-slot:item.isAutorenewSchedules="{ item }">
          <div class="d-flex justify-center" dark>
            <v-icon
              :color="getColor(item.isAutorenewSchedules)"
              v-if="item.isAutorenewSchedules"
            >
              mdi-checkbox-marked-circle
            </v-icon>
            <v-icon :color="getColor(item.isAutorenewSchedules)" v-else>
              mdi-minus-circle
            </v-icon>
          </div>
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
import { mapState } from "vuex";
import ContractFilter from "@/components/ao/ContractFilter";
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import { aoMenu } from "@/helpers/menus";
export default {
  name: "AoLayout",
  components: {
    PageLayout,
    DeleteItems,
    TableActions,
    GlobalMenu,
    ContractFilter,
  },
  created() {
    this.menu = aoMenu;
    this.$store.dispatch("ao.capacities/fetchContainers");
    this.$store.dispatch("ao.contragents/fetchContragents").then(res=>console.log(res));
    this.$store.dispatch("ao.contracts/fetchContracts").catch((err) => console.log(err));
  },
  data() {
    return {
      drawer: false,
      query: "",
      showFilters: false,
      filters: {},
      menu: [],
      showDeleteModal: false,
      headers: [
        {
          text: "№",
          sortable: false,
          value: "contractId",
        },
        { text: "Номер договора", value: "contractNumber" },
        { text: "Контрагент", align: "center", value: "contractClient" },
        { text: "Способ отправки", value: "sendDocsType" },
        { text: "Комментарий", value: "comment" },
        { text: "Даты", align: "center", value: "startDate" },
        { text: "Статус подписания", value: "contractStatus" },
        { text: "Автопродление", value: "isAutorenewSchedules" },
      ],
      selectedItem: false,
    };
  },
  methods: {
    getColor(item) {
      if (item) return "success";
      else return "warning";
    },
    getColor(item) {
      if (item) return "success";
      else return "warning";
    },
    onPlus() {
      this.$router.push("/ao/contracts/edit/new");
    },
    onEdit() {
      this.$router.push(`/ao/contracts/edit/${this.selectedItem.contractId}`);
    },
    onDelete() {
      let data = { contractIds: [this.selectedItem.contractId] };
      this.$store
        .dispatch("ao.contracts/deleteContract", data)
        .then((res) => {
          this.showDeleteModal = false
          this.selectedItem = false
        })
        .catch((err) => console.log(err));
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
      console.log("data", data);
      this.$store.dispatch("ao.contracts/fetchContracts", data);
      this.showFilters = false;
      this.selectedItem = false
    },
  },
  computed: {
    ...mapState("ao.contracts", ["contracts"]),
    ...mapState("ao.contragents", ["contragents"]),
    ...mapState("ao.capacities", ["containers"]),
    filteredContracts(){
        return this.contracts.filter((item) => {
        return item.contractNumber.trim().toLowerCase().indexOf(this.query.toLowerCase()) != -1;
      });
    }
  },
};
</script>
