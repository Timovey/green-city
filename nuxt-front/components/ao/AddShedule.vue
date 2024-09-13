<template>
  <v-dialog scrollable v-model="value" max-width="1000px">
    <v-dialog width="600px" v-model="showPreview">
      <v-card>
        <v-card-title>Предпросмотр графика</v-card-title>
        <v-card-text>
          <v-date-picker
            readonly
            width="580px"
            v-model="previewDates"
            multiple
          ></v-date-picker>
          <v-combobox
            v-model="previewDates"
            multiple
            chips
            small-chips
            label="Выбранные даты"
            prepend-icon="mdi-calendar"
            readonly
          ></v-combobox>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn @click="showPreview = false">Закрыть</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-card>
      <v-card-title>
        <span v-if="!editable">Добавление периода </span>
        <span v-else> Редактирование периода</span>
      </v-card-title>
      <v-card-text>
        <v-form
          v-if="contractSchedule.stepInitialDateSchedule"
          ref="form"
          v-model="isValid"
        >
          <v-checkbox
            v-model="isStepSchedule"
            filled
            label="Использовать шаговый график"
            outlined
          />
          <template v-if="isStepSchedule">
            <PickDate
              v-model="initialDateValid"
              label="Начало шагового графика"
              :date.sync="contractSchedule.stepInitialDateSchedule.initialDate"
            />
            <v-text-field
              type="number"
              v-model="contractSchedule.stepInitialDateSchedule.stepValue"
              :max="100"
              outlined
              clearable
              filled
              label="Шаг графика"
            />
          </template>

          <div class="text-h6 text--secondary">Включающие гибкие графики</div>
          <div class="my-2" v-for="(value, index) in contractSchedule.crons">
            <div class="d-flex justify-center">
              <CronInput
                style="
                   {
                    width: 100%;
                  }
                "
                v-model="contractSchedule.crons[index]"
                size="large"
              />
              <v-btn
                @click="removeCron(contractSchedule.crons, index)"
                class="ml-2 mt-2"
                fab
                dark
                small
              >
                <v-icon dark> mdi-minus-circle </v-icon>
              </v-btn>
            </div>
          </div>
          <v-btn @click="addCron()" class="ma-2 warning" fab>
            <v-icon>mdi-shape-circle-plus</v-icon>
          </v-btn>
          <v-divider class="mb-2" />
          <div class="text-h6 text--secondary">Исключающие гибкие графики</div>
          <div
            class="my-2"
            v-for="(value, index) in contractSchedule.excludeCrons"
          >
            <div class="d-flex justify-center">
              <CronInput
                v-model="contractSchedule.excludeCrons[index]"
                size="large"
              />
              <v-btn
                @click="removeCron(contractSchedule.excludeCrons, index)"
                class="ml-2 mt-2"
                fab
                dark
                small
              >
                <v-icon dark> mdi-minus-circle </v-icon>
              </v-btn>
            </div>
          </div>

          <v-btn @click="addExcludeCron()" class="ma-2 warning" fab>
            <v-icon>mdi-shape-circle-plus</v-icon>
          </v-btn>
          <div class="d-flex justify-space-between">
            <div>
              <div class="text-h6 text--secondary">Включаемые даты</div>
              <v-date-picker
                v-model="contractSchedule.scheduleDates"
                multiple
              />
            </div>
            <div>
              <div class="text-h6 text--secondary">Исключаемые даты</div>
              <v-date-picker v-model="contractSchedule.excludeDates" multiple />
            </div>
          </div>
          <v-divider />
          <div class="text-h6 text--secondary">Настройки периода</div>
          <div v-if="contractSchedule.contractScheduleSettings">
            <v-select
              :rules="requiredRules"
              required
              v-model="
                contractSchedule.contractScheduleSettings.weekendReactionType
              "
              :items="reactionTypes"
              item-value="id"
              item-text="name"
              filled
              label="Реакция на праздники"
              outlined
              clearable
            />
            <v-select
              :rules="requiredRules"
              required
              v-model="
                contractSchedule.contractScheduleSettings.redDateReactionType
              "
              :items="reactionTypes"
              item-value="id"
              item-text="name"
              filled
              label="Реакция на выходные"
              outlined
              clearable
            />
            <v-checkbox
              v-model="
                contractSchedule.contractScheduleSettings.enableWeekendWorkDates
              "
              filled
              label="Учитывать рабочие выходные дни"
              outlined
              clearable
            />
          </div>

          <div class="d-flex">
            <PickDate
              :rules="requiredRules"
              v-model="startValid"
              label="Действует с "
              :date.sync="scheduleDateFrom"
            />
            <v-spacer />
            <PickDate
              v-model="endValid"
              label="Действует по"
              :date.sync="scheduleDateTo"
              :clearable="true"
            />
          </div>
          <v-alert
            v-if="!dateValid"
            text
            prominent
            class="mb-4"
            type="error"
            icon="mdi-clock-fast"
          >
            Обязательно укажите даты
          </v-alert>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-slider
          style="width: 200px"
          step="1"
          label="Число дат"
          v-model="examplesCount"
          max="100"
          min="1"
          hide-details
          class="align-center"
        >
        </v-slider>
        <v-text-field
          v-model="examplesCount"
          class="mt-0 pt-0"
          hide-details
          single-line
          type="number"
          style="max-width: 100px"
          counter="100"
        ></v-text-field>
        <v-spacer />
        <v-btn @click="preview">Предпросмотр</v-btn>
        <v-btn
          v-if="!editable"
          @click="addSchedule(contractSchedule)"
          class="primary"
          >Добавить</v-btn
        >
        <v-btn v-else @click="saveSchedule(contractSchedule)" class="primary"
          >Сохранить</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { mapState } from "vuex";
import { showError, showSuccess } from "@/helpers/messages";
import PickDate from "@/components/UI/PickDate";
import CronInput from "@/components/UI/cron/components/cron-input";
import { correctDate } from "@/helpers/dateHelper";
import { DEFAULT_CRON_EXPRESSION } from "@/components/UI/cron/constant/filed";
import { post } from "@/helpers/axiosHelper";

export default {
  components: {
    PickDate,
    CronInput,
  },
  props: {
    value: false,
    current: false,
  },
  data() {
    return {
      isPreviewed: false,
      isStepSchedule: false,
      showPreview: false,
      previewDates: [],
      examplesCount: 1,
      isValid: false,
      idx: 0,
      editable: false,
      contractSchedule: {
        stepInitialDateSchedule: {
          initialDate: correctDate(this, Date.now()),
          stepValue: "",
        },
        crons: [],
        excludeCrons: [],
        scheduleDates: [],
        excludeDates: [],
        contractScheduleSettings: {
          enableWeekendWorkDates: true,
          redDateReactionType: 4,
          weekendReactionType: 4,
        },
      },
      scheduleDateFrom: "",
      scheduleDateTo: null,
      requiredRules: [
        (v) => (String(v).length > 0 && !!v) || "Поле не долно быть пустым",
      ],
      startValid: false,
      endValid: false,
      initialDateValid: false,
    };
  },
  methods: {
    removeCron(crons, idx) {
      crons = crons.splice(idx, 1);
    },
    preview() {
      post(this, "/api/Common/GetFireDatesByCron", {
        schedule: this.contractSchedule,
        examplesCount: this.examplesCount,
        fromDate: this.scheduleDateFrom,
        toDate: this.scheduleDateTo,
      })
        .then((res) => {
          this.showPreview = true;
          this.isPreviewed = true;
          this.previewDates = res.data.data.dates.map(
            function (item) {
              return this.$correctDate(item);
            }.bind(this)
          );
        })
        .catch((err) => {
          console.log("err", err);
          showError("Проверьте правильность введённых данных");
        });
    },
    addCron() {
      this.contractSchedule.crons.push(DEFAULT_CRON_EXPRESSION);
    },
    addExcludeCron() {
      this.contractSchedule.excludeCrons.push(DEFAULT_CRON_EXPRESSION);
    },
    addSchedule() {
      this.$refs.form.validate();
      if (!this.isPreviewed) {
        return showError("Необходимо выполнить предпросмотр");
      }
      if (this.isValid && this.dateValid) {
        this.$emit("onScheduleAdd", {
          contractSchedule: this.contractSchedule,
          scheduleDateFrom: this.scheduleDateFrom,
          scheduleDateTo: this.scheduleDateTo,
        });
        this.clear();
      }
    },
    validateCrons(source) {
      for (let i in source) {
        let res = source[i].split(" ");
        if (res[5] != "?") {
          res[3] = "?";
        } else if (res[3] != "?") {
          res[5] = "?";
        }
        source[i] = res.join(" ");
      }
      return source;
    },
    saveSchedule() {
      this.$refs.form.validate();
      this.contractSchedule.crons = this.validateCrons(
        this.contractSchedule.crons
      );
      this.contractSchedule.excludeCrons = this.validateCrons(
        this.contractSchedule.excludeCrons
      );
      if (this.isValid && this.dateValid) {
        this.$emit("onScheduleSave", {
          idx: this.idx,
          contractSchedule: this.contractSchedule,
          scheduleDateFrom: this.$correctDate(this.scheduleDateFrom),
          scheduleDateTo: this.$correctDate(this.scheduleDateTo),
        });
        this.clear();
      }
    },
    clear() {
      this.contractSchedule = {
        stepInitialDateSchedule: {
          initialDate: correctDate(this, Date.now()),
          stepValue: "",
        },
        crons: [],
        excludeCrons: [],
        scheduleDates: [],
        excludeDates: [],
        scheduleDateFrom: "",
        scheduleDateTo: "",
        contractScheduleSettings: {
          enableWeekendWorkDates: true,
          redDateReactionType: 4,
          weekendReactionType: 4,
        },
      };
    },
  },
  computed: {
    ...mapState("ao.contracts", ["reactionTypes"]),
    dateValid() {
      return this.startValid && this.endValid;
    },
  },

  watch: {
    current: {
      immediate: true,
      handler(val) {
        if (val) {
          //   console.log(val);
          this.contractSchedule = val.contractSchedule;
          //проверяем шаговый график, есть ли он
          if (this.contractSchedule.stepInitialDateSchedule) {
            //проверяем дату начала шагового графика
            if (this.contractSchedule.stepInitialDateSchedule.initialDate) {
              //дата приходит в общем формате - приводим к нормальному виду
              this.contractSchedule.stepInitialDateSchedule.initialDate =
                this.$moment(
                  this.contractSchedule.stepInitialDateSchedule.initialDate
                ).format("YYYY-MM-DD");
              if (this.contractSchedule.stepInitialDateSchedule.stepValue) {
                //ставим чекбок, открывающий шаговый график
                this.isStepSchedule = true;
              }
            }
          }
          this.scheduleDateFrom = val.scheduleDateFrom;
          this.scheduleDateTo = val.scheduleDateTo;
          this.editable = true;
        } else {
          this.editable = false;
          this.clear();
        }
      },
      deep: true,
    },
    value(val) {
      this.$emit("input", val);
    },
  },
};
</script>
