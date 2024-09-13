<template>
  <div class="schedulesContainer">
    <div class="schedulesContainer__modal">
      <div class="cross-container">
        <div @click="closeModal" class="cross">X</div>
      </div>
      <div >
        <input type="text" placeholder="График 1" @click="openCron"/>
        <span>от</span>
        <date-picker 
          v-model="scheduleDateFrom" 
          :lang="lang" 
          :format="format"
          valueType="format"
          input-class="datePickerInput"
          class="datePickerCreate"
        >
        </date-picker>
        <span>до</span>
        <date-picker 
          v-model="scheduleDateTo" 
          :lang="lang" 
          :format="format"
          valueType="format"
          input-class="datePickerInput"
          class="datePickerCreate"
        >
        </date-picker>
      </div>
      <button @click="addSchedule">Добавить график</button>
    </div>
    <cronModal v-if="showCron" :cronProp="cronProp" :common="common" :container="container" :containerYard="containerYard" @close="closeCronModal"/>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import cronModal from "@/components/cron/cronModal.vue";

export default {
  name: "schedules",

  components: {
    DatePicker,
    cronModal
  },

  props: {
    cronProp: {
      type: Array,
      default() {
        return [
          {
            stepInitialDateSchedule: null,
            scheduleDates: [],
            excludeDates: [],
            crons: [],
            excludeCrons: [],
            contractScheduleSettings: {
              enableWeekendWorkDates: true,
              redDateReactionType: 4,
              weekendReactionType: 4
            }
          }
        ]
      }
    },

    common: {
      type: Boolean,
      default: false,
    },

    container: {
      type: Object,
      default() {
        return {
          id: null,
          address: ""
        }
      }
    },

    containerYard: {
      type: Object,
      default() {
        return {
          id: null,
          address: ""
        }
      }
    },
  },

  data() {
    return {
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'YYYY-MM-DD',
      scheduleDateFrom: null,
      scheduleDateTo: null,
      showCron: false,
      cropPropCopy: '',
      containerIdCopy: null,
      schedules: []
    }
  },

  mounted() {
    console.log(this.cronProp)
  },

  methods: {
    closeModal() {
      if (this.cropPropCopy) {
        this.$emit('close', this.cropPropCopy, this.containerIdCopy)
      }
      else {
        this.$emit('close', this.cronProp, this.container.id)
      }
    },

    addSchedule() {

    },

    closeCronModal(cronProp, id) {
      this.cronPropCopy = cronProp
      this.containerIdCopy = id
      this.showCron = false
    },

    openCron() {
      this.showCron = true
    }
  }
};
</script>

<style lang="scss">
.schedulesContainer {
  height: 100%;
  width: 100%;
  position: absolute;
  top: 0;
  left: 0;
  background: #FFF;

  .cross-container {
    display: flex;
    justify-content: flex-end;
    align-items: center;

    .cross {
      cursor: pointer;
    }
  }
}

.datePickerInput {
  font-family: 'Roboto', sans-serif;
  font-weight: 700;
  font-size: 18px;
  line-height: 21px;
  color: #000000;
  display: inline-block;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  width: 100%;
  height: 34px;
  padding: 6px 30px;
  padding-left: 10px;
  background-color: #fff;
  border: 1px solid #000;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
  box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.08);
  cursor: pointer;
}
</style>
