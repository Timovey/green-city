<template>
  <div v-if="(editStatus && route) || !editStatus">
    <GlobalMenu
      :edit="true"
      :backButton="true"
      @onBack="$router.push(backRouter)"
      v-model="drawer"
      :menu="menu"
      title="Рейс"
    />
    <v-row>
      <v-col class="col col-12">
        <v-card class="mt-5">
          <v-form v-model="isValid" ref="form">
            <v-card-title class="text--secondary">
              <div v-if="!editStatus">Добавление рейса</div>
              <div v-else>Редактирование рейса</div>
            </v-card-title>
            <v-divider class="mx-4" />
            <v-card-text class="pb-2">
              <v-text-field
                v-model="route.date"
                label="Дата"
                :rules="requiredRules"
                required
                outlined
                type="date"
                filled
                :disabled="editStatus"
              ></v-text-field>
              <v-autocomplete
                :rules="requiredRules"
                :items="workhifts"
                label="Экипажи"
                item-value="id"
                outlined
                filled
                required
                item-text="workshiftData"
                v-model="route.transportWorkShiftId"
                :disabled="editStatus"
              ></v-autocomplete>
              <v-autocomplete
                :items="bms"
                label="БМ"
                item-value="id"
                outlined
                filled
                required
                clearable
                item-text="name"
                v-model="route.baseRouteId"
              ></v-autocomplete>
              <v-autocomplete
                :items="dropdownStands"
                label="Адрес КП"
                item-value="address"
                outlined
                filled
                required
                clearable
                item-text="address"
                v-model="route.standAddress"
              ></v-autocomplete>
              <div class="d-flex flex-row mb-2">
                <v-btn @click="moveAll" class="primary"> Перенести </v-btn>
                <v-spacer />
                <v-btn
                  @click="selectAll"
                  class="warning lighten-2 text--secondary"
                >
                  Выбрать все
                </v-btn>
              </div>
              <v-card-subtitle class="text-h5 text--secondary"
                >Общий объем: {{ getPickedVolume }}</v-card-subtitle
              >
              <v-row>
                <v-col class="col col-5">
                  <div class="text-h5 d-flex flex-row justify-center">
                    Выбранные КП
                  </div>
                  <draggable
                    handle=".handle"
                    ghost-class="ghost"
                    v-model="pickedStands"
                    class="list-group"
                    :scroll-sensitivity="300"
                    :force-fallback="true"
                  >
                    <transition-group type="transition" name="flip-list">
                      <RoutePickedStandCard
                        class="item ma-2 mt-6 list-group-item handle"
                        :key="item.standId"
                        v-for="item in pickedStands"
                        :item="item"
                        @onRemove="removeStand(item)"
                        @onUp="upStand(item)"
                        @onDown="downStand(item)"
                      />
                    </transition-group>
                  </draggable>
                </v-col>
                <v-spacer></v-spacer>
                <v-col class="col col-5">
                  <div class="text-h5 d-flex flex-row justify-center">КП</div>
                  <transition-group type="transition" name="flip-list">
                    <RouteStandCard
                      class="item ma-2 mt-6 list-group-item"
                      :key="item.standId"
                      v-for="item in stands"
                      :item="item"
                      :checked="getChecked(item)"
                      @onclick="selectStand(item)"
                      @onMove="pickStand(item)"
                    />
                  </transition-group>
                </v-col>
              </v-row>
            </v-card-text>
            <v-card-actions>
              <v-checkbox
                v-model="route.routeReady"
                label="Готовность рейса"
              ></v-checkbox>
              <v-spacer />
              <v-btn @click="onSubmit" class="primary"> Сохранить </v-btn>
            </v-card-actions>
          </v-form>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import { logistMenu } from "@/helpers/menus";
import { showSuccess, showErrorMessage } from "@/helpers/messages";
import draggable from "vuedraggable";
import GlobalMenu from "@/components/UI/GlobalMenu";
import RouteStandCard from "@/components/logist/RouteStandCard.vue";
import RoutePickedStandCard from "@/components/logist/RoutePickedStandCard.vue";
import { deepCopy } from "@/helpers/deepCopyHelper";
export default {
  components: {
    draggable,
    GlobalMenu,
    RouteStandCard,
    RoutePickedStandCard,
  },
  data() {
    return {
      isValid: false,
      backRouter: "/logist/route",
      editStatus: false,
      drawer: false,
      menu: logistMenu,
      workhifts: [],
      bms: [],
      dropdownStands: [],
      stands: [],
      pickedStands: [],
      selected: [],
      route: {
        transportWorkShiftId: "",
        standIds: [],
        date: Date.now(),
        excludeStandIds: [],
        standAddress: "",
        routeReady: false,
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Необходимо заполнить поле",
        (v) => !!v || "Необходимо заполнить поле",
      ],
    };
  },
  created() {
    this.fetchDropdownBms();
    this.fetchDropdownStands();

    this.id = this.$route.params.id;
    if (this.id == "new" || this.id == null) {
      this.editStatus = false;
      this.route.date = this.$getTomorrowDate();
    } else {
      this.editStatus = true;
      this.route.routeId = this.id;
      const getData = {
        id: this.id,
      };
      this.$api.route
        .get(getData)
        .then((res) => {
          this.route = Object.assign(this.route, res);
          this.route.date = this.$correctDate(this.route.date);
        })
        .catch((err) => {
          showErrorMessage(err);
        });
    }
  },
  methods: {
    selectStand(item) {
      if (this.selected.includes(item.standId)) {
        const index = this.selected.findIndex((el) => el == item.standId);
        if (index >= 0) {
          this.selected.splice(index, 1);
        }
      } else {
        this.selected.push(item.standId);
      }
    },
    getChecked(item) {
      return this.selected.includes(item.standId);
    },
    selectAll() {
      this.stands.forEach((el) => this.selected.push(el.standId));
    },
    moveAll() {
      this.stands.forEach((stand) => {
        if (
          this.selected.includes(stand.standId) &&
          this.pickedStands.findIndex((el) => el.standId == stand.standId) < 0
        ) {
          this.pickStand(stand);
        }
      });
    },
    upStand(stand) {
      let pickIndex = this.pickedStands.findIndex(
        (el) => el.standId == stand.standId
      );
      if (pickIndex > 0) {
        let t = { ...this.pickedStands[pickIndex - 1] };
        this.pickedStands.splice(pickIndex - 1, 1, {
          ...this.pickedStands[pickIndex],
        });
        this.pickedStands.splice(pickIndex, 1, t);
      }
    },
    downStand(stand) {
      let pickIndex = this.pickedStands.findIndex(
        (el) => el.standId == stand.standId
      );
      if (pickIndex < this.pickedStands.length - 1) {
        let t = { ...this.pickedStands[pickIndex + 1] };
        this.pickedStands.splice(pickIndex + 1, 1, {
          ...this.pickedStands[pickIndex],
        });
        this.pickedStands.splice(pickIndex, 1, t);
      }
    },
    pickStand(item) {
      const existStand = this.pickedStands.find(
        (el) => el.standId == item.standId
      );
      if (!existStand) {
        this.pickedStands.push(item);
      }
      const index = this.selected.findIndex((el) => el == item.standId);
      if (index >= 0) {
        this.selected.splice(index, 1);
      }
    },
    removeStand(item) {
      const index = this.pickedStands.findIndex((el) => el === item);
      this.pickedStands.splice(index, 1);
    },
    fetchDropdownWorkshifts() {
      this.$api.dropdownWorker
        .fetchWorkhifts(this.route)
        .then((res) => (this.workhifts = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchDropdownBms() {
      this.$api.dropdownWorker
        .fetchBms()
        .then((res) => (this.bms = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchDropdownStands() {
      this.$api.dropdownWorker
        .fetchStands()
        .then((res) => (this.dropdownStands = res))
        .catch((err) => {
          showErrorMessage(err);
        });
    },
    fetchRouteStands() {
      this.$api.route
        .fetchStands(this.route)
        .then((res) => (this.stands = res))
        .then((res) => {
          if (this.editStatus) {
            this.route.standIds.forEach((element) => {
              const stand = this.stands.find((s) => s.standId === element);
              if (stand) {
                this.pickStand(stand);
              }
            });
          }
        })
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

      if (this.route.standIds.length === 0) {
        showErrorMessage("Выберете КП");
        return;
      }

      if (this.editStatus) {
        this.$api.route
          .update(this.route)
          .then((res) => {
            showSuccess();
            this.$router.push(this.backRouter);
          })
          .catch((err) => {
            showErrorMessage(err);
          });
      } else {
        this.$api.route
          .create(this.route)
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
    getPickedVolume() {
      return this.pickedStands.reduce(
        (accumulator, item) => accumulator + item.containersVolume,
        0
      );
    },
  },
  watch: {
    "route.date"(oldVal, newVal) {
      if (!this.$moment(this.route.date).isValid()) {
        return;
      }
      if (!this.editStatus) {
        this.route.transportWorkShiftId = "";
      }
      this.route.dateTime = this.route.date;
      this.fetchDropdownWorkshifts();
    },
    pickedStands: {
      handler: function (newValue) {
        this.route.standIds = deepCopy(
          this.pickedStands.map((el) => el.standId)
        );
        this.route.excludeStandIds = this.route.standIds;
        this.fetchRouteStands();
      },
      deep: true,
    },
    route: {
      handler: function (newValue) {
        this.fetchRouteStands();
      },
      deep: true,
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
  background: rgb(208, 227, 231) !important;

  .v-card {
    opacity: 0.9;
  }
}
</style>
