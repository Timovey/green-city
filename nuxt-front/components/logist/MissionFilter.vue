<template>
  <v-card>
    <v-card-title class="text--secondary"> Фильтр </v-card-title>
    <v-card-text>
      <v-autocomplete
        @change="changeContragent"
        :items="contractClients"
        label="Контрагент"
        item-value="id"
        clearable
        outlined
        filled
        multiple
        item-text="name"
        v-model="value.contractClientIds"
      ></v-autocomplete>
      <v-autocomplete
        @change="changeContract"
        :items="contracts"
        label="Договор"
        clearable
        item-value="id"
        outlined
        filled
        multiple
        item-text="contractNumber"
        v-model="value.contractIds"
      ></v-autocomplete>
      <v-autocomplete
        @change="changeStand"
        :items="stands"
        label="КП"
        clearable
        multiple
        item-value="id"
        outlined
        filled
        item-text="address"
        v-model="value.standIds"
      ></v-autocomplete>
      <v-autocomplete
        :items="containers"
        label="Контейнеры"
        clearable
        multiple
        item-value="id"
        outlined
        filled
        item-text="name"
        v-model="value.containerIds"
      ></v-autocomplete>
      <v-select
        v-model="value.statuses"
        :items="getMissionStatuses"
        item-text="name"
        clearable
        multiple
        outlined
        filled
        item-value="value"
        label="Статусы"
      >
      </v-select>
    </v-card-text>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn @click="onReset" color="warning"> Сбросить фильтры </v-btn>
      <v-btn @click.stop="$emit('onSave')" color="primary">
        <div>Применить</div>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapState } from "vuex";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import { toArray } from "@/helpers/objectHelper";
export default {
  created() {
    this.$store
      .dispatch("mission.enums/fetchEnums")
      .catch((err) => showErrorMessage(err));
    this.fetchContragents();
    this.fetchContracts();
    this.fetchStands();
    this.fetchContainers();
    this.value.date = this.value.date || this.$getTomorrowDate();
  },
  data() {
    return {
      value: {
        isFilterByCreatedUser: false,
        contractClientIds: [],
        contractIds: [],
        standIds: [],
        containerIds: [],
        statuses: [],
      },
      containers: [],
      contractClients: [],
      contracts: [],
      stands: [],
    };
  },
  methods: {
    onSave() {
      this.$emit("onSave");
    },
    onReset() {
      this.value = {
        date: this.$getTomorrowDate(),
      };
    },
    fetchContragents() {
      this.$api.dropdownWorker
        .fetchContragents()
        .then((res) => (this.contractClients = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchContracts() {
      this.$api.dropdownWorker
        .fetchContracts({
          contractClientIds: this.value.contractClientIds,
        })
        .then((res) => (this.contracts = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchStands() {
      this.$api.dropdownWorker
        .fetchStands({
          contractClientIds: this.value.contractClientIds,
          contractIds: this.value.contractIds,
        })
        .then((res) => (this.stands = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchContainers() {
      this.$api.dropdownWorker
        .fetchContainers({
          contractClientIds: this.value.contractClientIds,
          contractIds: this.value.contractIds,
          standIds: this.value.standIds,
        })
        .then((res) => (this.containers = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    changeContragent() {
      this.value.contractIds = [];
      this.value.standIds = [];
      this.value.containerIds = [];
      this.fetchContracts();
      this.fetchStands();
      this.fetchContainers();
    },
    changeContract() {
      this.value.standIds = [];
      this.value.containerIds = [];
      this.fetchStands();
      this.fetchContainers();
    },
    changeStand() {
      this.value.containerIds = [];
      this.fetchContainers();
    },
  },
  computed: {
    ...mapState("mission.enums", ["missionEnums"]),
    getMissionStatuses() {
      return toArray(this.missionEnums.MissionStatus);
    },
  },
  watch: {
    value: {
      handler: function (newValue) {
        this.$emit("input", newValue);
      },
      deep: true,
    },
  },
};
</script>
