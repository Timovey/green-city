<template>
  <div>
    <GlobalMenu @onShowFilters="showFilters = true" :backButton="true" title="Заявки по контрактам" @onPlus="onPlus()"
      v-model="drawer" :menu="menu" />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <RequestFilter @onSave="applyFilters()" v-model="filters" />

    </v-dialog>
    <DeleteItems v-if="selectedItem" v-model="showDeleteModal" @onDelete="onDelete()"
      :message="`Заявку  ${selectedItem.id} | ${selectedItem.contractClientName} `" />
    <PageLayout>
      <v-text-field class="ma-2" @change="applyFilters" v-model="filters.appDate" type="date" label="Дата" required
        outlined filled />
      <v-data-table multi-sort item-key="id" show-select single-select :headers="headers" :items="requests"
        :items-per-page="20" @item-selected="onItemSelect">
        <template v-slot:item.appStatus="{ item }">
          <template v-if="missionEnums.AppComputedStatus">
            {{ missionEnums.AppComputedStatus[item.appStatus] }}

          </template>
        </template>
      </v-data-table>
      <TableActions :selectedItem="selectedItem" @onShowModal="showDeleteModal = true" @onEdit="onEdit" />
    </PageLayout>
  </div>
</template>

<script>
import { mapState } from "vuex";
import RequestFilter from "@/components/requests/RequestFilter";
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import { aoMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";

export default {
  name: "AoLayout",
  components: {
    PageLayout,
    DeleteItems,
    TableActions,
    GlobalMenu,
    RequestFilter,
  },
  created() {
    this.fetch();
    this.$store
      .dispatch("mission.enums/fetchEnums")
      .then(res => console.log(res))
  },
  data() {
    return {
      expanded: [],
      requests: [],
      drawer: false,
      showFilters: false,
      filters: {
        appDate: this.$getTomorrowDate()
      },
      menu: aoMenu,
      showDeleteModal: false,
      headers: [
        {
          text: "№",
          align: "start",
          sortable: false,
          value: "id",
        },
        { text: "Статус", value: "appStatus" },
        { text: "Комментарий", value: "comment" },
        { text: "Контрагент", value: "contractClientName" },
        { text: "Номер контракта", value: "contractNumber" },
      ],
      selectedItem: false,
    };
  },
  methods: {
    onPlus() {
      this.$router.push("/ao/requests/contract/edit/new");
    },
    onEdit() {
      this.$router.push(`/ao/requests/contract/edit/${this.selectedItem.id}`);
    },
    onDelete() {
      let data = { id: this.selectedItem.id };
      this.$api.requestContractWorker
        .delete(data)
        .then((res) => {
          this.requests = this.requests.filter(
            (item) => item.id != this.selectedItem.id
          );
          showSuccess("Удаление выполнено успешно!");
          this.showDeleteModal = false;
          this.selectedItem = false;
        })
        .catch(() => {
          showErrorMessage("Произошла ошибка при получении заявок");
        });
    },
    onItemSelect(item) {
      if (item.value) this.selectedItem = item.item;
      else this.selectedItem = false;
    },
    applyFilters() {
      let data = {
        isFilter: true,
        sorting: {},
        ...this.filters,
      };
      this.$api.requestContractWorker
        .fetch({
          appType: 1,
          ...data,
        })
        .then((res) => {
          this.requests = res;
        })
        .catch(() => {
          showErrorMessage("Произошла ошибка при получении заявок");
        });
      this.showFilters = false;
    },
    fetch() {
      this.$api.requestContractWorker
        .fetch({
          appType: 1,
          appDate: this.$getTomorrowDate(),
        })
        .then((res) => {
          this.requests = res;
        })
        .catch(() => {
          showErrorMessage("Произошла ошибка при получении заявок");
        });
    },
  },
  computed: {
    ...mapState('mission.enums', ['missionEnums'])
  },
};
</script>
