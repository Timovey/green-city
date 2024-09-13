<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      title="Транспортные стредства"
      @onPlus="onPlus()"
      v-model="drawer"
      :menu="menu"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <TransportFilter
        :organizations="organizations"
        :transportTypes="getTransportTypes"
        @onSave="applyFilters()"
        v-model="filters"
      />
    </v-dialog>
    <DeleteItems
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Транспорт ${selectedItem.name} ${selectedItem.regNumber}`"
    />
    <PageLayout>
      <v-data-table
        multi-sort
        item-key="id"
        show-select
        single-select
        :headers="headers"
        :items="transports"
        :items-per-page="10"
        @item-selected="onItemSelect"
        ref="transportTable"
      >
        <template v-slot:item.rent="{ item }">
          <div class="d-flex justify-center" dark>
            <v-icon :color="getColor(item.rent)" v-if="item.rent">
              mdi-checkbox-marked-circle
            </v-icon>
            <v-icon :color="getColor(item.rent)" v-else>
              mdi-close-circle
            </v-icon>
          </div>
        </template>
        <template v-slot:item.organizationId="{ item }">
          {{ getNameOrganization(item.organizationId) }}
        </template>
        <template v-slot:item.tankTypes="{ item }">
          {{ getTankTypes(item.tankTypes) }}
        </template>
        <template v-slot:item.transportType="{ item }">
          {{ getTransportType(item.transportType) }}
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
import GlobalMenu from "@/components/UI/GlobalMenu";
import TransportFilter from "@/components/tax/TransportFilter";
import { taxerMenu } from "@/helpers/menus";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import { mapState } from "vuex";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import { toArray } from "@/helpers/objectHelper";
import { deepCopy } from "@/helpers/deepCopyHelper";
export default {
  head: {
    title: "Машины",
  },
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TransportFilter,
    TableActions,
  },
  created() {
    this.menu = taxerMenu;
    this.$store
      .dispatch("tax.transports/fetchTransports")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("organizations/fetchOrganizations")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("tax.enums/fetchEnums")
      .catch((err) => showErrorMessage(err));
    this.$store
      .dispatch("ao.capacities/getContainerTypes")
      .catch((err) => showErrorMessage(err));
  },
  data() {
    return {
      menu: [],
      showFilters: false,
      selectedItem: false,
      pageCount: 0,
      filters: {},
      drawer: false,
      showDeleteModal: false,
      itemsPerPage: 5,
      headers: [
        {
          text: "№",
          align: "start",
          value: "id",
        },
        { text: "Рег. №", value: "regNumber" },
        { text: "Название ТС", value: "name" },
        { text: "Тип ТС", value: "transportType" },
        { text: "Год выпуска", value: "productionYear" },
        { text: "Огранизация", value: "organizationId" },
        { text: "Забор", value: "tankFence" },
        { text: "Объем", value: "cargoVolume" },
        { text: "СТС", value: "sts" },
        { text: "Тип бака", value: "tankTypes" },
        { text: "Тип кузова", value: "bodyType" },
        { text: "Цвет", value: "bodyColor" },
        { text: "Грузопод.", value: "loadCapacity" },
      ],
    };
  },
  methods: {
    getColor(item) {
      if (item) return "primary";
      else return "warning";
    },
    onPlus() {
      this.$router.push("/tax/transports/edit/new");
    },
    onItemSelect(item) {
      if (item.value) this.selectedItem = item.item;
      else this.selectedItem = false;
    },
    onDelete() {
      this.$store
        .dispatch("tax.transports/deleteTransport", {
          transportIds: [this.selectedItem.id],
        })
        .then(() => {
          showSuccess();
        })
        .catch((err) => {
          showErrorMessage(err);
        });
      this.showDeleteModal = false;
      this.selectedItem = false;
    },
    onEdit() {
      this.$router.push(`/tax/transports/edit/${this.selectedItem.id}`);
    },
    applyFilters() {
      let data = {
        isFilter: true,
        sorting: {},
        ...this.filters,
      };
      this.$store
        .dispatch("tax.transports/fetchTransports", data)
        .then(() => (this.showFilters = false))
        .catch((err) => {
          showErrorMessage(err);
        });
      //убираем выделение элемента, т.к. его может и не быть
      this.selectedItem = false;
      this.$refs.transportTable.selection = [];
    },
    getNameOrganization(orgsId) {
      if (!orgsId) {
        return null;
      }
      const res = this.organizations.find((el) => el.id == orgsId);
      if (res) {
        return res.name;
      }
      return null;
    },
    getTankTypes(types) {
      if (this.containerTypes) {
        let filteredContainer = deepCopy(this.containerTypes)
          .filter((el) => {
            if (types.includes(el.id)) {
              return el;
            }
          })
          .map((el) => el.name)
          .join(" ");
        return filteredContainer;
      }
      return "Неизвестно";
    },
    getTransportType(transport) {
      if (this.enums.Transport && this.enums.Transport[transport]) {
        return this.enums.Transport[transport];
      }
      return "Неизвестно";
    },
  },
  computed: {
    ...mapState("tax.transports", ["transports"]),
    ...mapState("tax.enums", ["enums"]),
    ...mapState("organizations", ["organizations"]),
    ...mapState("ao.capacities", ["containerTypes"]),
    getTransportTypes() {
      return toArray(this.enums.Transport);
    },
  },
};
</script>
