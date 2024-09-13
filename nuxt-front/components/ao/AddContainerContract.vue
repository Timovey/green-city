<template>
  <v-card>
    <v-form v-model="isValid" ref="form">
      <AddShedule
        v-model="showAddPeriod"
        :current="currentSchedule"
        @onScheduleSave="saveSchedule($event)"
        @onScheduleAdd="addPeriod($event)"
      />
      <ConfirmClose
        v-model="showConfirmClose"
        @ok="goBackConfirm"
        @not="showConfirmClose = false"
      />
      <v-toolbar v-if="!byKP" dark color="primary">
        <v-btn @click="goBack" icon dark>
          <v-icon>mdi-close</v-icon>
        </v-btn>
        <v-toolbar-title>
          <span v-if="!editStatus">Добавление</span>
          <span v-else>Редактирование</span> контейнера
        </v-toolbar-title>
        <v-spacer />
        <v-toolbar-items>
          <v-btn v-if="!editStatus" @click="onAdd()" dark text>
            Добавить
          </v-btn>
          <v-btn v-else="editStatus" @click="onSave()" dark text>
            Сохранить
          </v-btn>
        </v-toolbar-items>
      </v-toolbar>
      <v-card-text class="pt-4">
        <template v-if="!editStatus">
          <v-autocomplete
            v-if="!byKP"
            v-model="address"
            :items="containers"
            item-value="containerStandAddress"
            item-text="containerStandAddress"
            filled
            label="Поиск по адресу"
            outlined
            clearable
          />
          <v-autocomplete
            v-if="!byKP"
            :rules="requiredContainerRules"
            v-model="container.container"
            :items="containers"
            return-object
            item-text="name"
            filled
            label="Контейнер"
            outlined
            clearable
          />
        </template>
        <v-text-field
          v-model="container.name"
          label="Контейнер"
          outlined
          readonly
          v-else
        ></v-text-field>

        <v-textarea
          v-model="container.scheduleComment"
          filled
          type="text"
          label="Комментарий к расписанию"
          outlined
          clearable
        />
        <div class="d-flex">
          <PickTime
            label="Время работы с"
            :time.sync="container.workTimeFrom"
            :clearable="true"
          />
          <v-spacer />
          <PickTime
            label="Время работы по"
            :time.sync="container.workTimeTo"
            :clearable="true"
          />
        </div>
        <div class="d-flex pt-4">
          <v-checkbox
            class="mr-4"
            v-model="container.isInRegistry"
            filled
            label="Находится в реестре"
            outlined
            clearable
          />
          <v-spacer />
          <PickDate
            :validate="false"
            label="В реестре с"
            :date.sync="container.inRegistryFromDate"
            :clearable="true"
          />
        </div>

        <v-card class="ma-4">
          <v-card-title>Периоды</v-card-title>
          <v-card-text>
            <Periods
              :key="periodKey"
              @edit="editSchedule"
              @remove="deleteSchedule($event)"
              :periods="container.contractScheduleCollection"
            />
            <!-- <v-alert v-else border="bottom" colored-border type="warning" elevation="2">
                            Добавьте периоды
                        </v-alert> -->
          </v-card-text>
          <v-card-actions>
            <v-spacer />
            <v-btn @click="onPeriodAdd()" class="primary"
              >Добавить период</v-btn
            >
            <v-spacer />
          </v-card-actions>
        </v-card>
      </v-card-text>
    </v-form>
  </v-card>
</template>

<script>
import AddShedule from "@/components/ao/AddShedule";
import PickDate from "@/components/UI/PickDate";
import PickTime from "@/components/UI/PickTime";
import Periods from "./Periods.vue";
import ConfirmClose from "@/components/ao/ConfirmClose.vue";
export default {
  components: {
    AddShedule,
    Periods,
    PickTime,
    PickDate,
    ConfirmClose,
  },
  created() {
    if (this.byKP && this.container) this.container.containerId = 1;
  },
  data() {
    return {
      address: "",
      search: "",
      editStatus: false,
      isValid: false,
      showAddPeriod: false,
      showConfirmClose: false,
      periodKey: 0,
      currentSchedule: false,
      container: {
        container: null,
        containerId: "",
        contractScheduleCollection: [],
        scheduleComment: "",
        workTimeFrom: null,
        workTimeTo: null,
        isInRegistry: false,
        inRegistryFromDate: "",
      },
      requiredRules: [
        (v) => String(v).length > 0 || "Поле не долно быть пустым",
      ],
      requiredContainerRules: [(v) => !!v || "Необходимо заполнить поле"],
    };
  },
  props: {
    value: false,
    current: false,
    byKP: false,
    containers: [],
  },
  methods: {
    addPeriod(period) {
      this.container.contractScheduleCollection.push({
        ...period,
        idx: this.container.contractScheduleCollection.length,
      });
      this.showAddPeriod = false;
      this.currentSchedule = false;
    },
    goBack() {
      this.showConfirmClose = true;
      //   this.$emit("onClose");
    },
    goBackConfirm() {
      this.showConfirmClose = false;
      this.$emit("onClose");
    },
    onAdd() {
      this.container = {
        ...this.container,
        ...this.container.container,
      };
      this.$refs.form.validate();
      if (this.isValid) {
        this.$store.dispatch("ao.capacities/fetchContainers");
        this.$emit("onContainerAdd", this.container);
        this.clear();
        this.currentSchedule = false;
      }
    },
    onSave() {
      this.container = {
        ...this.container,
        ...this.container.container,
      };
      this.$refs.form.validate();
      if (this.isValid) {
        this.$store.dispatch("ao.capacities/fetchContainers");
        this.$emit("onContainerSave", this.container);
        this.clear();
      }
    },
    deleteSchedule(idx) {
      this.container.contractScheduleCollection.splice(idx, 1);
    },
    editSchedule(idx) {
      const schdl = this.container.contractScheduleCollection[idx];
      this.currentSchedule = {
        idx,
        ...schdl,
        scheduleDateFrom: this.$correctDate(schdl.scheduleDateFrom),
        scheduleDateTo: this.$correctDate(schdl.scheduleDateTo),
      };
      this.showAddPeriod = true;
    },
    saveSchedule(schedule) {
      this.container.contractScheduleCollection[schedule.idx] = schedule;
      this.currentSchedule = false;
      this.showAddPeriod = false;
      this.periodKey++;
    },

    clear() {
      this.container = {
        containerId: "",
        containerStandAddress: "",
        contractScheduleCollection: [],
        scheduleComment: "",
        workTimeFrom: "",
        workTimeTo: "",
        isInRegistry: false,
        inRegistryFromDate: "",
      };
    },
    onPeriodAdd() {
      this.currentSchedule = false;
      this.showAddPeriod = true;
    },
  },
  watch: {
    current: {
      handler(val) {
        if (val) {
          this.container = JSON.parse(JSON.stringify(val));
          this.editStatus = true;
        } else {
          this.clear();
          this.editStatus = false;
        }
      },
      deep: true,
      immediate: true,
    },
    address(val) {
      this.container.containerStandAddress = val;
      this.$emit("onAddressSearch", val);
    },
    container: {
      deep: true,
      handler(val) {
        let valid = this.isValid;
        this.$emit("onValidate", valid);
        this.$emit("onDataChange", val);
      },
    },
  },
};
</script>
