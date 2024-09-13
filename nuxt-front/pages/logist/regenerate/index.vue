<template>
  <div>
    <GlobalMenu
      :backButton="false"
      :edit="true"
      v-model="drawer"
      :menu="menu"
      title="Перегенерация миссий"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-dialog v-model="dialog"></v-dialog>
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="form">
            <v-card-title class="text--secondary">
              <div>Перегенерация миссий</div>
            </v-card-title>
            <v-divider class="mx-4" />
            <v-card-text class="pb-2">
              <v-text-field
                v-model="filters.date"
                type="date"
                label="Дата"
                required
                :rules="requiredRules"
                outlined
                filled
              />
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
                v-model="filters.contractClientIds"
              ></v-autocomplete>
              <v-autocomplete
                :items="contracts"
                label="Договор"
                clearable
                item-value="id"
                outlined
                filled
                multiple
                item-text="contractNumber"
                v-model="filters.contractIds"
              ></v-autocomplete>
            </v-card-text>
            <v-card-actions>
              <v-spacer />
              <v-btn @click="onSubmit" class="primary">
                Перегенерировать
              </v-btn>
            </v-card-actions>
          </v-form>
        </v-card>
      </v-col>
      <v-col class="col col-2" />
    </v-row>
  </div>
</template>

<script>
import { logistMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import GlobalMenu from "@/components/UI/GlobalMenu";
export default {
  components: {
    GlobalMenu,
  },
  data() {
    return {
      isValid: false,
      dialog: false,
      drawer: false,
      menu: logistMenu,
      filters: {},
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      contractClients: [],
      contracts: [],
    };
  },
  created() {
    this.filters.date = this.$getTomorrowDate();
    this.fetchContragents();
    this.fetchContracts();
  },
  methods: {
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) {
        console.log("неВалидно");
        showErrorMessage("Проверьте данные");
        return;
      }

      this.$api.requestContractWorker
        .regenerate(this.filters)
        .then((res) => {
          showSuccess();
        })
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
    fetchContracts() {
      this.$api.dropdownWorker
        .fetchContracts({
          contractClientIds: this.filters.contractClientIds,
        })
        .then((res) => (this.contracts = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    changeContragent() {
      this.filters.contractIds = [];
      this.fetchContracts();
    },
  },
  computed: {},
};
</script>

<style lang="scss"></style>
