<template>
  <div>
    <GlobalMenu
      @onShowFilters="showFilters = true"
      @onBack="onBack"
      :backButton="true"
      title="Контейнерные площадки"
      @onPlus="onPlus()"
      :edit="true"
      v-model="drawer"
      :menu="menu"
    />
    <v-row>
      <v-col class="col col-1" />
      <v-col class="col col-10">
        <v-card class="mt-5">
          <v-card-title class="text--secondary">
            <div v-if="editStatus">Редактирование площадки</div>
            <div v-else>Добавление площадки</div>
          </v-card-title>
          <v-form ref="form" v-model="isValid">
            <v-card-text class="pb-2">
              <v-autocomplete
                :rules="requiredRules"
                prepend-icon="mdi-database-search"
                :search-input.sync="search"
                v-model="square.address"
                :items="addresses"
                item-value="value"
                item-text="value"
                filled
                label="Адрес"
                outlined
                clearable
                @keyup.enter="setAddress"
              >
              </v-autocomplete>
              <div class="d-flex align-center justify-center mb-4">
                <div>Текущий адрес:</div>

                <v-chip dark v-if="square.address" class="green">{{
                  square.address
                }}</v-chip>
              </div>

              <div class="d-flex">
                <v-text-field
                  v-model="square.coordinates.coordinates[0].longitude"
                  filled
                  type="number"
                  class="mr-2"
                  label="Долгота"
                  outlined
                  clearable
                />
                <v-text-field
                  v-model="square.coordinates.coordinates[0].latitude"
                  filled
                  class="ml-2"
                  type="number"
                  label="Широта"
                  outlined
                  clearable
                />
              </div>
              <v-textarea
                :rules="textareaRule"
                required
                v-model="square.coordinates.coordinatesComment"
                filled
                type="number"
                label="Комментарий"
                outlined
                clearable
              />
            </v-card-text>
            <v-divider class="mx-4" />
            <v-card-text>
              <div class="d-flex justify-space-between">
                <v-checkbox
                  v-model="square.isHighSecurityObject"
                  label="Режимный объект"
                  color="primary"
                />
                <v-checkbox
                  v-model="square.haveRoof"
                  label="Наличие крыши"
                  color="primary"
                />
                <v-checkbox
                  v-model="square.haveBase"
                  label="Наличие основания"
                  color="primary"
                />
                <v-checkbox
                  v-model="square.haveFence"
                  label="Наличие ограждения"
                  color="primary"
                />
              </div>
              <v-textarea
                :rules="textareaRule"
                required
                v-model="square.driveWay"
                filled
                label="Путь проезда"
                outlined
                clearable
              />
              <v-text-field
                v-model="square.contacts"
                filled
                label="Контакты"
                outlined
                clearable
              />
              <v-select
                v-model="square.standType"
                :items="squareTypes"
                item-value="id"
                item-text="name"
                label="Тип площадки"
                :rules="requiredRules"
                required
                outlined
                filled
              />
              <v-autocomplete
                v-model="square.attachedContainerIds"
                multiple
                :items="containers"
                item-value="containerId"
                item-text="name"
                label="Связанные контейнеры"
                outlined
                filled
              />
            </v-card-text>
            <v-spacer />
            <v-card-actions>
              <v-spacer />
              <v-btn @click="onBack" color="error lighten-2"> Отмена </v-btn>
              <v-btn @click="onSubmit" color="primary">
                <div v-if="editStatus">Сохранить</div>
                <div v-else>Добавить</div>
              </v-btn>
            </v-card-actions>
          </v-form>
        </v-card>
      </v-col>
      <v-col class="col col-1" />
    </v-row>
  </div>
</template>

<script>
import { mapState } from "vuex";
import { aoMenu } from "@/helpers/menus";
import GlobalMenu from "@/components/UI/GlobalMenu";
export default {
  components: {
    GlobalMenu,
  },
  created() {
    this.menu = aoMenu;
    this.id = this.$route.params.id;
    this.$store.dispatch("ao.capacities/fetchContainers");
    if (this.id == "new") {
      this.editStatus = false;
    } else {
      this.editStatus = true;
      this.$store
        .dispatch("ao.squares/getSquare", { standIds: [this.id] })
        .then((res) => {
          this.square = res;
          this.search = square.address;
          this.square.attachedContainerIds = this.square.attachedContainers.map(
            (item) => item.containerId
          );
        });
    }
  },
  data() {
    return {
      addresses: [],
      id: 0,
      editStatus: false,
      drawer: false,
      menu: [],
      search: "",
      square: {
        address: "",
        isHighSecurityObject: true,
        coordinates: {
          coordinates: [
            {
              longitude: 0,
              latitude: 0,
            },
          ],
          coordinatesComment: "",
        },
        driveWay: "",
        haveBase: true,
        standType: "",
        contacts: "",
        haveRoof: true,
        haveFence: true,
        containerTypeId: 0,
        containerStandId: 0,
        attachedContainerIds: [],
        attachedContainers: [],
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Поле не долно быть пустым",
      ],
      textareaRule: [
        (v) => (v || "aaaa").length >= 4 || "Поле должно быть содержательным",
      ],
      isValid: false,
    };
  },

  methods: {
    onBack() {
      this.$router.push("/ao/squares");
    },
    onSubmit() {
      this.$refs.form.validate();
      if (!this.isValid) return;
      if (!this.editStatus) {
        this.$store
          .dispatch("ao.squares/addSquare", this.square)
          .then((res) => {
            this.$router.push("/ao/squares");
          });
      } else {
        this.$store
          .dispatch("ao.squares/updateSquare", this.square)
          .then((res) => {
            this.$router.push("/ao/squares");
          });
      }
    },
    setAddress() {
      this.square.address = this.search;
    },
  },
  computed: {
    ...mapState("ao.squares", ["squareTypes"]),
    ...mapState("ao.capacities", ["containers"]),
  },
  watch: {
    search() {
      this.$axios
        .post(
          `https://data.pbprog.ru/api/address/full-address/parse`,
          JSON.stringify({ query: this.search, hierarchyMode: "adm" }),
          {
            headers: {
              Authorization: "10f6359a7b36477ab282b1b0c7caad6daefef54a",
              "Content-Type": "application/json",
            },
          }
        )
        .then((response) => {
          this.addresses = [this.search, ...response.data.suggestions];
        });
    },
  },
};
</script>
