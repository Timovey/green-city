<template>
  <v-form :disabled="edit && logist" ref="form" v-model="isValid">
    <v-card-title class="text--secondary"> Информация о заявке </v-card-title>
    <v-card-text>
      <v-textarea :disabled="readonly" v-model="request.comment" filled label="Комментарий" class="mr-2"
        outlined />
      <v-text-field :disabled="readonly" v-model="request.date" type="date" label="Дата вывоза" :rules="requiredRules"
        required outlined filled />
      <v-autocomplete @change="changeContragent" :disabled="readonly || edit" :rules="map ? [] : requiredRules"
        :items="contractClients" :clearable="map" label="Контрагент" item-value="id" outlined filled :required="!map"
        item-text="name" v-model="request.contractClientId"></v-autocomplete>
      <v-autocomplete @change="changeContract" :disabled="readonly || edit" :rules="map ? [] : requiredRules"
        :items="contracts" :clearable="map" label="Договор" item-value="id" outlined filled :required="!map"
        item-text="contractNumber" v-model="request.contractId"></v-autocomplete>
      <v-autocomplete v-if="!map" :disabled="readonly" @focus="fetchContainers" :rules="requiredRules" :items="containers"
        return-object label="Контейнеры" multiple outlined filled item-text="name" item-value="id"
        v-model="request.containers"></v-autocomplete>
      <template v-if="map">
        <v-text-field :disabled="readonly" v-model="request.longitude" type="number" label="Долгота"
          :rules="requiredRules" required outlined filled />
        <v-text-field :disabled="readonly" v-model="request.latitude" type="number" label="Широта" :rules="requiredRules"
          required outlined filled />
        <v-text-field :disabled="readonly" v-model="request.customVolume" type="number" label="Предполагаемый объём"
          :rules="requiredRules" required outlined filled />
      </template>

      <div class="d-flex flex-column">
        <ContainerCard :key="container.id" :enums="missionEnums" class="mb-2" :edit="!readonly"
          @remove="removeContainer(container.id)" v-for="container in request.containers" :container="container" />
      </div>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn v-if="!edit" @click="submit" color="success">Добавить</v-btn>
      <v-btn :disabled="readonly" v-else @click="submit" color="warning">Изменить</v-btn>
    </v-card-actions>
  </v-form>
</template>

<script>
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import { mapState } from "vuex";
import ContainerCard from "@/components/common/ContainerCard";
export default {
  components: {
    ContainerCard,
  },
  created() {
    this.$store
      .dispatch("mission.enums/fetchEnums")
      .then((res) => console.log(res));
    this.fetchContragents()
    this.fetchContracts()
    this.request.date = this.request.date || this.$getTomorrowDate()

  },
  data: () => ({
    request: {
      comment: "",
      date: "",
      containers: [],
      contractId: 0,
      contractClientId: 0,
    },
    containers: [],
    isValid: false,
    contractClients: [],
    contracts: [],
    requiredRules: [
      (v) => String(v).length > 0 || "Необходимо заполнить поле",
      (v) => !!v || "Необходимо заполнить поле",
    ],
  }),
  props: {
    edit: false,
    map: false,
    readonly: false,
    logist: false,
    data: {},
  },
  methods: {
    submit() {
      this.$refs.form.validate();
      if (!this.isValid) {
        showErrorMessage("Проверьте данные");
        return;
      }
      this.$emit("submit", this.request);
    },
    fetchContracts() {
      this.$api.dropdownWorker
        .fetchContracts({
          contractClientIds: this.request.contractClientId !== null ? [this.request.contractClientId] : [],
        })
        .then((res) => (this.contracts = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchContragents() {
      this.$api.dropdownWorker
        .fetchContragents()
        .then((res) => (this.contractClients = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchContainers() {
      this.$api.dropdownWorker
        .fetchContainers({
          contractClientIds: this.request.contractClientId !== null ? [this.request.contractClientId] : [],
          contractIds: this.request.contractId !== null ? [this.request.contractId] : [],
        })
        .then((res) => (this.containers = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    removeContainer(id) {
      this.request.containers = this.request.containers.filter(
        (item) => item.id != id
      );
    },
    changeContragent() {
      this.request.contractId = undefined
      this.request.containers = []
      this.fetchContracts()
    },
    changeContract() {
      this.request.containers = []
      this.fetchContainers()
    }
  },
  computed: {
    ...mapState("mission.enums", ["missionEnums"]),
  },
  watch: {
    data(val) {
      if (val) {
        this.request = { ...val, date: this.$correctDate(val.date) };
      }
    },
    request(val) {
      if (val.contractClientId && val.contractId) {
        this.fetchContainers();
      }
      if (val.contractClientId) {
        this.fetchContracts()
      }
    },
  },
};
</script>
