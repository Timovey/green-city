<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      :editStatus="true"
      title="Миссии"
      :menu="menu"
      v-model="drawer"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <MissionFilter @onSave="applyFilters()" v-model="filters" />
    </v-dialog>
    <PageLayout>
      <v-text-field
        class="ma-2"
        @change="applyFilters"
        v-model="filters.date"
        type="date"
        label="Дата"
        required
        outlined
        filled
      />
      <v-data-table
        multi-sort
        item-key="id"
        show-select
        single-select
        :headers="headers"
        :items="missions"
        :items-per-page="10"
        @item-selected="onItemSelect"
        ref="table"
      >
        <template v-slot:item.status="{ item }">
					{{ getMissionStatus(item.status) }}
				</template>
      </v-data-table>
      <TableActions
        :selectedItem="selectedItem"
        :showEdit="false"
        :showDelete="false"
      />
    </PageLayout>
  </div>
</template>

<script>
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import MissionFilter from "@/components/logist/MissionFilter.vue";
import { logistMenu } from "@/helpers/menus";
import { mapState } from "vuex"
import { showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Миссии",
  },
  name: "Missions",
  components: {
    GlobalMenu,
    DeleteItems,
    PageLayout,
    TableActions,
    MissionFilter,
  },
  created() {
    this.filters.date = this.$getTomorrowDate();
    this.fetchData();
    this.$store.dispatch('mission.enums/fetchEnums').catch(err => showErrorMessage(err))
  },
  data() {
    return {
      menu: logistMenu,
      baseUrlPage: "/dispatcher/mission",
      drawer: false,
      selectedItem: false,
      showDeleteModal: false,
      showFilters: false,
      filters: {
        date: this.$getTomorrowDate(),
      },
      missions: [],
      headers: [
        { text: "№", value: "id" },
        { text: "Статус", value: "status" },
        { text: "Комментарий к статусу", value: "statusComment" },
        { text: "КА", value: "contractClients" },
        { text: "Адрес", value: "address" },
        { text: "Контейнер", value: "containerName" },
        { text: "Тип мусора", value: "trashType" },
        { text: "Тип контейнера", value: "containerTypeId" },
        { text: "Имя типа контейнера", value: "containerTypeName" },
        { text: "Объем", value: "planVolume" },
        { text: "Факт объем", value: "factVolume" },
      ],
    };
  },
  methods: {
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
      this.$api.mission
        .fetch(data)
        .then((res) => (this.missions = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    getMissionStatus(status) {
			if (this.missionEnums.MissionStatus && this.missionEnums.MissionStatus[status]) {
				return this.missionEnums.MissionStatus[status]
			}
			return 'Неизвестно'
		},
  },
  computed: {
    ...mapState("mission.enums", ["missionEnums"]),
  },
  watch: {},
};
</script>

<style lang="scss" scoped></style>
