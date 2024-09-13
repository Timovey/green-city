<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      @onPlus="onPlus()"
      title="Подсобные рабочие"
      :menu="menu"
      v-model="drawer"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <AuxiliaryWorkerFilter
        @onSave="applyFilters()"
        v-model="filters"
        :organizations="organizations"
      />
    </v-dialog>
    <DeleteItems
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Подсобный рабочий ${selectedItem.name}`"
    />
    <PageLayout>
      <v-data-table
        multi-sort
        item-key="id"
        show-select
        single-select
        :headers="headers"
        :items="auxiliaryWorkers"
        :items-per-page="10"
        @item-selected="onItemSelect"
        ref="table"
      >
        <template v-slot:item.organizationId="{ item }">
          {{ getNameOrganization(item.organizationId) }}
        </template>
        <template v-slot:item.isFired="{ item }">
          <div class="d-flex justify-center" dark>
            <v-icon :color="getColor(item.isFired)" v-if="item.isFired">
              mdi-checkbox-marked-circle
            </v-icon>
            <v-icon :color="getColor(item.isFired)" v-else>
              mdi-close-circle
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
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import AuxiliaryWorkerFilter from "@/components/dispatcher/AuxiliaryWorkerFilter";
import { dispatcherMenu } from "@/helpers/menus";
import { mapState } from "vuex";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Подсобные рабочие",
  },
  name: "AuxiliaryWorkers",
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
    AuxiliaryWorkerFilter,
  },
  created() {
    this.menu = dispatcherMenu;
    this.$store
      .dispatch("organizations/fetchOrganizations")
      .catch((err) => showErrorMessage(err));
    this.fetchData();
  },
  data() {
    return {
      menu: [],
      baseUrlPage: "/dispatcher/auxiliaryWorker",
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {},
      auxiliaryWorkers: [],
      headers: [
        {
          text: "Таб. №",
          align: "start",
          sortable: false,
          value: "tableNumber",
        },
        { text: "ФИО", value: "fio" },
        { text: "Адрес регистр.", value: "regAddress" },
        { text: "Факт. адрес", value: "factAddress" },
        { text: "Дата принятия на работу", value: "acceptWorkDate" },
        { text: "Дата рождения", value: "birthDate" },
        { text: "Место рождения", value: "birthPlace" },
        { text: "Телефон", value: "contactNumber" },
        { text: "Серия паспорта", value: "passportSerie" },
        { text: "Номер паспорта", value: "passportNumber" },
        { text: "Дата выдачи", value: "issueDate" },
        { text: "Кем выдан", value: "issuer" },
        { text: "Код подразделения", value: "devisionCode" },
        { text: "Уволен", value: "isFired" },
        { text: "Дата увольнения", value: "firedDate" },
        { text: "Организация", value: "organizationId" },
      ],
    };
  },
  methods: {
    getColor(item) {
      if (item) return "red";
      else return "warning";
    },
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
      this.$api.auxiliaryWorker
        .fetch(data)
        .then(
          (res) =>
            (this.auxiliaryWorkers = res.map((el) =>
              this.correctDataWorker(el)
            ))
        )
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    onDelete() {
      let data = { id: this.selectedItem.id };
      this.$api.auxiliaryWorker
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
    correctDataWorker(item) {
      const format = "DD-MM-YYYY";
      item.birthDate = this.$correctDate(item.birthDate, format);
      item.issueDate = this.$correctDate(item.issueDate, format);
      item.acceptWorkDate = this.$correctDate(item.acceptWorkDate, format);
      if (item.firedDate)
        item.firedDate = this.$correctDate(item.firedDate, format);

      return this.addFio(item);
    },

    addFio(item) {
      let patr = item.patronymic ? item.patronymic[0] + "." : "";
      item.fio = item.surname + " " + item.name[0] + ". " + patr;
      return item;
    },
  },
  computed: {
    ...mapState("organizations", ["organizations"]),
  },
};
</script>

<style lang="scss" scoped></style>
