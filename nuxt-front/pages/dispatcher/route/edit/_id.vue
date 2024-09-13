<template>
  <div>
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="Закрытие рейса"
    />
    <PageLayout>
      <v-form v-model="isValid" ref="form">
        <v-card-title class="pb-2"> {{ route.routeName }} </v-card-title>
        <v-row>
          <v-col cols="12" sm="6" md="3">
            <v-card-text>
              Дата: {{ $moment(route.date).format("YYYY.MM.DD") }}
            </v-card-text>
          </v-col>
          <v-col cols="12" sm="6" md="3">
            <v-text-field
              v-model="route.routeFinishPlace"
              label="Место выгрузки"
              disabled
              required
              outlined
              filled
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="3">
            <v-text-field
              v-model="route.routeTotalVolumeOnFinish"
              label="Итоговый объем"
              :rules="requiredNumberRules"
              required
              outlined
              type="number"
              filled
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="3">
            <v-checkbox
              v-model="isOk"
              @change="changeIsOk"
              label="Все хорошо"
            />
          </v-col>
        </v-row>

        <v-data-table
          multi-sort
          item-key="id"
          :headers="headers"
          :items="route.missions"
          :items-per-page="10"
          ref="table"
        >
          <template v-slot:item.status="{ item }">
            <v-select
              v-model="item.status"
              :items="getStatuses"
              :rules="requiredRules"
              required
              item-text="name"
              item-value="value"
              dense
              class="mt-4"
            >
            </v-select>
          </template>
          <template v-slot:item.statusComment="{ item }">
            <v-text-field
              class="mt-2"
              outlined
              filled
              v-model="item.statusComment"
            />
          </template>
          <template v-slot:item.trashType="{ item }">
            {{ getTrashType(item.trashType) }}
          </template>
          <template v-slot:item.containerTypeId="{ item }">
            {{ getTankTypes(item.containerTypeId) }}
          </template>
          <template v-slot:item.factVolume="{ item }">
            <v-text-field
              class="mt-2"
              outlined
              filled
              @change="recalculateVolume"
              :rules="requiredRules"
              required
              type="number"
              v-model="item.factVolume"
            />
          </template>
        </v-data-table>
        <v-card-actions>
          <v-spacer />
          <v-btn @click="onSubmit" class="primary"> Сохранить </v-btn>
        </v-card-actions>
      </v-form>
    </PageLayout>
    <v-row>
      <v-col class="col col-12"> </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapState } from "vuex";
import { dispatcherMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import { toArray } from "@/helpers/objectHelper";
import GlobalMenu from "@/components/UI/GlobalMenu";
import PageLayout from "@/components/UI/PageLayout";
export default {
  components: {
    GlobalMenu,
    PageLayout,
  },
  data() {
    return {
      isValid: false,
      dialog: false,
      backRouter: "/dispatcher/route",
      editStatus: false,
      drawer: false,
      menu: dispatcherMenu,
      route: {},
      routeFinishPlace:
        'Ульяновская обл., Чердаклинский р-н, в границах СПК "Красноярский", 3,8 км юго-западнее п. Колхозный',
      isOk: false,
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
      requiredNumberRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => v >= 0 || "Необходимо заполнить поле",
      ],
      headers: [
        {
          text: "Ид миссии",
          align: "start",
          value: "id",
        },
        { text: "Комментарий к миссии", value: "missionComment" },
        { text: "Статус", value: "status" },
        { text: "Комментарий к статусу", value: "statusComment" },
        { text: "Контрагенты", value: "contractClients" },
        { text: "Ид КП", value: "standId" },
        { text: "Адресс", value: "address" },
        { text: "ИД контейнера", value: "containerId" },
        { text: "Название контейнера", value: "containerName" },
        { text: "Тип мусора", value: "trashType" },
        { text: "Тип ёмкости", value: "containerTypeId" },
        { text: "Название ёмкости", value: "containerTypeName" },
        { text: "Объем", value: "planVolume" },
        { text: "Факт объем", value: "factVolume" },
      ],
    };
  },
  created() {
    this.id = this.$route.params.id;
    if (this.id == "new" || this.id == null) {
      this.$router.push(this.backRouter);
      showErrorMessage("Нет id рейса");
    } else {
      const getData = {
        id: this.id,
      };
      this.$store
        .dispatch("mission.enums/fetchEnums")
        .catch((err) => showErrorMessage(err));
      this.$store
        .dispatch("ao.enums/fetchEnums")
        .catch((err) => showErrorMessage(err));
      this.$api.mission
        .get(getData)
        .then((res) => (this.route = res))
        .then((res) => (this.route.routeFinishPlace = this.routeFinishPlace))
        .catch((err) => {
          showErrorMessage(err);
        });
      this.$store
        .dispatch("ao.capacities/getContainerTypes")
        .catch((err) => showErrorMessage(err));
    }
  },
  methods: {
    recalculateVolume() {
      const sum = this.route.missions.reduce(
        (accumulator, currentValue) => accumulator + +currentValue.factVolume,
        0
      );
      this.route.routeTotalVolumeOnFinish = sum;
    },
    changeIsOk() {
      if (this.isOk) {
        this.route.missions.map((el) => {
          el.status = 4;
          el.statusComment = "Успех";
          el.factVolume = el.planVolume;
          return el;
        });
      }
      this.recalculateVolume();
    },
    getTrashType(trash) {
      if (this.enums.Trash && this.enums.Trash[trash]) {
        return this.enums.Trash[trash];
      }
      return "Неизвестно";
    },
    getTankTypes(typeId) {
      console.log(this.route);
      if (this.containerTypes) {
        const type = this.containerTypes.find((el) => el.id == typeId);
        return type?.name ?? "Неизвестно";
      }
      return "Неизвестно";
    },
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) {
        showErrorMessage("Проверьте данные");
        return;
      }

      this.$api.mission
        .update(this.route)
        .then((res) => {
          showSuccess();
          this.$router.push(this.backRouter);
        })
        .catch((err) => {
          showErrorMessage(err);
        });
    },
  },
  computed: {
    ...mapState("mission.enums", ["missionEnums"]),
    ...mapState("ao.enums", ["enums"]),
    ...mapState("ao.capacities", ["containerTypes"]),
    getStatuses() {
      return toArray(this.missionEnums.MissionStatus);
    },
  },
};
</script>

<style lang="scss"></style>
