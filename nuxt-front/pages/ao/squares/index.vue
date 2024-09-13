<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      :backButton="true"
      title="Контейнерные площадки"
      @onPlus="onPlus()"
      v-model="drawer"
      :menu="menu"
    />
    <v-dialog scrollable width="80%" v-model="showFilters">
      <SquaresFilter
        :squaresTypes="getSquares"
        @onSave="applyFilters()"
        v-model="filters"
      />
    </v-dialog>
    <DeleteItems
      v-if="selectedItem"
      v-model="showDeleteModal"
      @onDelete="onDelete()"
      :message="`Площадку  ${selectedItem.standId} | ${selectedItem.address} | ${selectedItem.driveWay}`"
    />

    <PageLayout>
      <v-text-field
        class="ma-4 mt-2"
        label="Поиск по адресу"
        v-model="query"
        filled
        outlined
        append-icon="mdi-magnify"
      ></v-text-field>
      <v-data-table
        show-expand
        :expanded.sync="expanded"
        multi-sort
        item-key="standId"
        show-select
        single-select
        :headers="headers"
        :items="filteredSquares"
        :items-per-page="20"
        @item-selected="onItemSelect"
      >
        <template v-slot:item.haveBase="{ item }">
          <div class="d-flex justify-center" dark>
            <v-icon :color="getColor(item.haveBase)" v-if="item.haveBase">
              mdi-checkbox-marked-circle
            </v-icon>
            <v-icon :color="getColor(item.haveBase)" v-else> mdi-minus-circle </v-icon>
          </div>
        </template>
        <template v-slot:item.haveFence="{ item }">
          <div class="d-flex justify-center" dark>
            <v-icon :color="getColor(item.haveFence)" v-if="item.haveFence">
              mdi-checkbox-marked-circle
            </v-icon>
            <v-icon :color="getColor(item.haveFence)" v-else> mdi-minus-circle </v-icon>
          </div>
        </template>
        <template v-slot:item.haveRoof="{ item }">
          <div class="d-flex justify-center" dark>
            <v-icon :color="getColor(item.haveRoof)" v-if="item.haveRoof">
              mdi-checkbox-marked-circle
            </v-icon>
            <v-icon :color="getColor(item.haveRoof)" v-else> mdi-minus-circle </v-icon>
          </div>
        </template>
        <template v-slot:expanded-item="{ headers, item }">
          <td :colspan="headers.length">
            <template v-if="item.attachedContainers.length">
              <div
                class="text-h6 align-center text--secondary d-flex pa-2"
                v-for="container in item.attachedContainers"
              >
                <div class="mr-4">Контейнер:</div>
                <div class="mr-4">
                  {{ container.containerName }}
                </div>
                <v-chip class="mr-4">
                  {{ getContainerType(container.trashType) }}
                </v-chip>
              </div>
            </template>

            <div v-else>Контейнеры ещё не добавлены</div>
          </td>
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
import SquaresFilter from "@/components/ao/SquaresFilter";
import GlobalMenu from "@/components/UI/GlobalMenu";
import DeleteItems from "@/components/UI/DeleteItems";
import PageLayout from "@/components/UI/PageLayout";
import TableActions from "@/components/UI/TableActions";
import { aoMenu } from "@/helpers/menus";
import { toArray } from "@/helpers/objectHelper";
import { showSuccess, showErrorMessage } from "@/helpers/messages";

export default {
  name: "AoLayout",
  components: {
    PageLayout,
    DeleteItems,
    TableActions,
    GlobalMenu,
    SquaresFilter,
  },
  created() {
    this.menu = aoMenu;
    this.$store.dispatch("ao.squares/fetchSquares").catch((err) => console.log(err));
    this.$store.dispatch("ao.enums/fetchEnums").catch((err) => showErrorMessage(err));
  },
  data() {
    return {
      expanded: [],
      drawer: false,
      query: "",
      showFilters: false,
      filters: {},
      menu: [],
      showDeleteModal: false,
      headers: [
        {
          text: "№",
          align: "start",
          sortable: false,
          value: "standId",
        },
        { text: "Адрес", value: "address" },
        { text: "Тип площадки", value: "standTypeName" },
        { text: "Путь проезда", value: "driveWay" },
        { text: "Основание", value: "haveBase" },
        { text: "Крыша", value: "haveRoof" },
        { text: "Ограждение", value: "haveFence" },
      ],
      selectedItem: false,
    };
  },
  methods: {
    getColor(item) {
      if (item) return "success";
      else return "warning";
    },
    onPlus() {
      this.$router.push("/ao/squares/edit/new");
    },
    onEdit() {
      this.$router.push(`/ao/squares/edit/${this.selectedItem.standId}`);
    },
    onDelete() {
      let data = { standIds: [this.selectedItem.standId] };
      this.$store
        .dispatch("ao.squares/deleteSquare", data)
        .then((res) => {
          this.showDeleteModal = false
          this.selectedItem = false
        });
    },
    onItemSelect(item) {
      if (item.value) this.selectedItem = item.item;
      else this.selectedItem = false;
      console.log(item);
    },
    applyFilters() {
      let data = {
        isFilter: true,
        clientName: this.filters.clientName,
        isFilterByCreatedUser: false,
        sorting: {},
        ...this.filters,
      };
      console.log(data);
      this.$store.dispatch("ao.squares/fetchSquares", data).catch((err) => {
        showErrorMessage(err);
      });
      this.showFilters = false;
    },
    getContainerType(type) {
      if (this.enums.Trash && this.enums.Trash[type]) {
        type = this.enums.Trash[type];
      }
      return type;
    },
  },
  computed: {
    ...mapState("ao.squares", ["squares"]),
    ...mapState("ao.enums", ["enums"]),
    filteredSquares() {
      return this.squares.filter((item) => {
        console.log(item);
        return item.address.trim().toLowerCase().indexOf(this.query.toLowerCase()) != -1;
      });
    },
    getSquares() {
      return toArray(this.enums.Stand);
    },
  },
};
</script>
