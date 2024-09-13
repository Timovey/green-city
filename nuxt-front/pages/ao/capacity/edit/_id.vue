<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      edit="true"
      :backButton="true"
      title="Ёмкости"
      @onPlus="onPlus()"
      v-model="drawer"
      :menu="menu"
      @onBack="onBack"
    />
    <v-row>
      <v-col class="col col-1" />
      <v-col class="col col-10">
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="form">
            <v-card-title class="text--secondary">
              <div v-if="!editStatus">Добавление ёмкости</div>
              <div v-else>Редактирование ёмкости</div>
            </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text class="pb-2">
              <v-text-field
                :rules="requiredRules"
                v-model="container.name"
                filled
                label="Наименование"
                outlined
                clearable
              />
              <v-select
                :rules="requiredRules"
                v-model="container.trashType"
                :items="garbageTypes"
                item-value="id"
                item-text="name"
                label="Тип мусора"
                required
                outlined
                filled
              />
            </v-card-text>
            <v-card-title class="pt-0 text--secondary"> Тара </v-card-title>
            <v-divider class="mx-4"></v-divider>
            <v-card-text>
              <v-select
                :rules="requiredRules"
                v-model="container.containerTypeId"
                :items="containerTypes"
                label="Вид тары"
                required
                outlined
                filled
                item-value="id"
                item-text="name"
              />
              <v-autocomplete
                :items="squares"
                item-value="standId"
                item-text="address"
                :rules="requiredRules"
                required
                v-model="container.containerStandId"
                label="Контейнерная площадка"
                outlined
                filled
              />
            </v-card-text>
          </v-form>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="onBack" color="error lighten-2"> Отмена </v-btn>
            <v-btn @click="onSubmit" color="primary">
              <div v-if="editStatus">Сохранить</div>
              <div v-else>Добавить</div>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
      <v-col class="col col-1" />
    </v-row>
  </div>
</template>

<script>
import { mapState } from "vuex";
import { getItem } from "@/helpers/persistanceStorage";
import GlobalMenu from "@/components/UI/GlobalMenu";
import { aoMenu } from "@/helpers/menus";
export default {
  components: {
    GlobalMenu,
  },
  created() {
    this.menu = aoMenu;
    this.id = this.$route.params.id;
    if (this.id == "new") {
      this.editStatus = false;
    } else {
      this.editStatus = true;
      this.$store
        .dispatch("ao.capacities/getContainer", { containerIds: [this.id] })
        .then((res) => {
          this.container = res[0];
          console.log(this.container);
        });
    }
    this.$store
      .dispatch("ao.squares/fetchSquares")
      .then(() => console.log(this.squares));
    this.$store.dispatch("ao.capacities/getContainerTypes").then((res) => {
      this.containerTypes = res;
    });
  },
  data() {
    return {
      id: 0,
      isValid: false,
      editStatus: false,
      drawer: false,
      container: {
        name: "",
        trashType: "",
        containerTypeId: "",
        containerStandId: 0,
      },
      containerTypes: [],
      requiredRules: [
        (v) => String(v).length > 0 || "Поле не долно быть пустым",
      ],
    };
  },

  methods: {
    onBack() {
      this.$router.push("/ao/capacity");
    },
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) return;
      let cntnr = {
        ...this.container,
        containerIds: [this.container.containerId],
      };
      let data = {
        ...cntnr,
        user: getItem("user"),
      };
      if (this.editStatus) {
        this.$store
          .dispatch("ao.capacities/updateContainer", data)
          .then((res) => this.$router.push("/ao/capacity"));
      } else {
        this.$store
          .dispatch("ao.capacities/addContainer", data)
          .then((res) => this.$router.push("/ao/capacity"));
      }
    },
  },
  computed: {
    ...mapState("ao.squares", ["squares"]),
    ...mapState("ao.capacities", ["garbageTypes"]),
  },
};
</script>
