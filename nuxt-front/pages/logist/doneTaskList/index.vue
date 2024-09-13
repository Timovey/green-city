<template>
  <div>
    <GlobalMenu
      :edit="true"
      :backButton="false"
      v-model="drawer"
      :menu="menu"
      title="Движение транспортных средств"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="form">
            <v-card-title class="text--secondary">
              <div>Движение транспортных средств</div>
            </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text class="pb-2">
              <v-text-field
                v-model="doneTaskList.dateFrom"
                filled
                type="date"
                label="Дата от"
                :rules="requiredRules"
                required
                outlined
              ></v-text-field>

              <v-text-field
                v-model="doneTaskList.dateTo"
                filled
                type="date"
                label="Дата до"
                :rules="requiredRules"
                required
                outlined
              ></v-text-field>

              <v-select
                v-model="doneTaskList.carIds"
                item-value="id"
                :items="transports"
                label="Транспорт"
                outlined
                filled
                multiple
                clearable
                
              >
                <template v-slot:selection="{ item }">
                  {{ item.name + " " + item.regNumber + ', ' }}
                </template>
                <template v-slot:item="{ item }">
                  {{ item.name + " " + item.regNumber }}
                </template>
              </v-select>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="onSubmit" color="primary">
                <div>Отправить</div>
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
import GlobalMenu from "@/components/UI/GlobalMenu";
import { logistMenu } from "@/helpers/menus";
import { showErrorMessage } from "@/helpers/messages";
export default {
  head: {
    title: "Движение транспортных средств",
  },
  created() {
    this.fetchData();
  },
  components: {
    GlobalMenu,
  },
  data() {
    return {
      menu: logistMenu,
      editStatus: false,
      id: false,
      drawer: false,
      backRouter: "/dispatcher/doneTaskList",
      isValid: false,
      transports: [],
      validation: {
        acceptValid: false,
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],

      doneTaskList: {
        dateTo: null,
        dateFrom: null,
        carIds: [],
      },
    };
  },
  methods: {
    fetchData() {
      this.$api.transport
        .fetch({})
        .then((res) => (this.transports = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) {
        showErrorMessage("Проверьте данные");
        return;
      }

      this.$api.report.printDoneTaskList(this.doneTaskList).catch((error) => {
        showErrorMessage(error);
      });
    },
  },
  computed: {},
  watch: {},
};
</script>

<style lang="scss" scoped></style>
