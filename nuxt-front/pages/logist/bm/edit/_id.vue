<template>
  <div v-if="(editStatus && bm) || !editStatus">
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="БМ"
    />
    <v-row>
      <v-col class="col col-2" />
      <v-col class="col col-8">
        <v-dialog v-model="dialog"></v-dialog>
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="form">
            <v-card-title class="text--secondary">
              <div v-if="!editStatus">Добавление базового маршрута</div>
              <div v-else>Редактирование базового маршрута</div>
            </v-card-title>
            <v-divider class="mx-4" />
            <v-card-text class="pb-2">
              <v-text-field
                :rules="requiredRules"
                v-model="bm.name"
                filled
                label="Название БМ"
                required
                outlined
                clearable
              ></v-text-field>
              <v-autocomplete
                :rules="requiredRules"
                :items="stands"
                label="Контейнерные площадки"
                multiple
                item-value="id"
                outlined
                filled
                required
                item-text="address"
                v-model="bm.stands"
              ></v-autocomplete>
              <div class="kp_containers">
                <div class="text-h5 d-flex flex-start">Выбранные КП</div>
                <draggable
                  v-if="bm && bm.stands && bm.stands.length > 0"
                  handle=".handle"
                  ghost-class="ghost"
                  v-model="bm.stands"
                  class="list-group"
                  :scroll-sensitivity="300"
                  :force-fallback="true"
                >
                  <transition-group type="transition" name="flip-list">
                    <StandCard
                      @onRemove="removeStand(id)"
                      :types="standTypes"
                      class="item ma-2 mt-6 list-group-item handle"
                      :key="id"
                      v-for="id in bm.stands"
                      :item="stands.find((item) => item.id == id)"
                    />
                  </transition-group>
                </draggable>
              </div>
            </v-card-text>
            <v-card-actions>
              <v-spacer />
              <v-btn @click="onSubmit" class="primary"> Сохранить </v-btn>
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
import draggable from "vuedraggable";
import StandCard from "@/components/logist/StandCard.vue";
import GlobalMenu from "@/components/UI/GlobalMenu";
import { mapState } from "vuex";
export default {
  components: {
    draggable,
    GlobalMenu,
    StandCard,
  },
  data() {
    return {
      isValid: false,
      dialog: false,
      backRouter: "/logist/bm",
      editStatus: false,
      drawer: false,
      menu: logistMenu,
      bm: {
        stands: [],
        name: "",
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
    };
  },
  created() {
    this.$store
      .dispatch("ao.enums/fetchEnums")
      .catch((err) => console.log(err));
    this.$store
      .dispatch("logist.bm/fetchStands", { isFilterByCreatedUser: false })
      .catch((err) => console.error(err));
    this.id = this.$route.params.id;
    if (this.id == "new" || this.id == null) {
      this.editStatus = false;
    } else {
      this.editStatus = true;
      this.$store
        .dispatch("logist.bm/getBm", { id: this.id })
        .then((res) => (this.bm = res));
    }
  },
  methods: {
    removeStand(id) {
      const idToRemove = this.bm.stands.findIndex((item) => item == id);
      this.bm.stands.splice(idToRemove, 1);
    },
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) {
        console.log("неВалидно");
        showErrorMessage("Проверьте данные");
        return;
      }

      if (this.editStatus) {
        this.$store
          .dispatch("logist.bm/updateBm", this.bm)
          .then((res) => {
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      } else {
        this.$store
          .dispatch("logist.bm/addBm", this.bm)
          .then((res) => {
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      }
    },
  },
  computed: {
    ...mapState("logist.bm", ["stands"]),
    ...mapState("ao.enums", ["enums"]),
    standTypes() {
      console.log(this.enums, "enums");
      return this.enums.Stand ?? {};
    },
  },
};
</script>

<style lang="scss">
.flip-list-move {
  transition: transform 0.5s;
}

.no-move {
  transition: transform 0s;
}

.handle .v-card {
  cursor: grab;
  transition: all ease-in 0.1s;
}

.ghost {
  .v-card {
    opacity: 0.9;

    background: rgb(208, 227, 231) !important;
  }
}
</style>
