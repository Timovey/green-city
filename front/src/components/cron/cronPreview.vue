<template>
  <div class="cronPreviewContainer">
    <div class="row">
      Начиная с даты
      <date-picker 
        v-model="fromDate" 
        :lang="lang" 
        :format="format"
        valueType="format"
        input-class="datePickerInput"
        class="datePickerCreate"
      >
      </date-picker>
      Заканчивая датой
      <date-picker 
        v-model="toDate" 
        :lang="lang" 
        :format="format"
        valueType="format"
        input-class="datePickerInput"
        class="datePickerCreate"
      >
      </date-picker>
    </div>
    <div class="row">
      Количество дат
      <input type="number" name="number" v-model="examplesCount">
    </div>
    <div class="row">
      <button @click="fetchFireDates">Показать список</button>
    </div>
    <div class="row" v-if="examples && examples.length">
      <div>
        Результат:
        <ul>
          <li v-for="(item, index) in examples" :key="index">
            {{item}}
          </li>
        </ul>
      </div>
    </div>
    <div v-if="showLoader" class="loader"></div>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

import axios from 'axios';

export default {
  name: "cronPreview",

  components: {
    DatePicker,
  },

  data() {
    return {
      ContractShedule: {},
      fromDate: null,
      toDate: null,
      examplesCount: null,
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'YYYY-MM-DD',
      examples: [],
      showLoader: false,
    }
  },

  mounted() {
    this.ContractShedule = this.$root.$refs.CronModal_component.cronProp
    if (!Object.prototype.hasOwnProperty.call(this.ContractShedule, 'stepInitialDateSchedule')) {
      this.ContractShedule.stepInitialDateSchedule = null
    }
    if (!Object.prototype.hasOwnProperty.call(this.ContractShedule, 'crons')) {
      this.ContractShedule.crons = []
    }
    if (!Object.prototype.hasOwnProperty.call(this.ContractShedule, 'excludeCrons')) {
      this.ContractShedule.excludeCrons = []
    }
    if (!Object.prototype.hasOwnProperty.call(this.ContractShedule, 'contractScheduleSettings')) {
      this.ContractShedule.contractScheduleSettings = {
        enableWeekendWorkDates: true,
        redDateReactionType: 4,
        weekendReactionType: 4
      }
    }
    if (!Object.prototype.hasOwnProperty.call(this.ContractShedule, 'scheduleDates')) {
      this.ContractShedule.scheduleDates = []
    }
    if (!Object.prototype.hasOwnProperty.call(this.ContractShedule, 'excludeDates')) {
      this.ContractShedule.excludeDates = []
    }
    console.log(this.ContractShedule)
  },

  methods: {
    async fetchFireDates() {
      this.showLoader = true
      let data = {
        schedule: this.ContractShedule,
        examplesCount: this.examplesCount,
        fromDate: this.fromDate,
        toDate: this.toDate
      }
      if (data.schedule.stepInitialDateSchedule == null || data.schedule.stepInitialDateSchedule.initialDate == null) {
        data.schedule.stepInitialDateSchedule = null
      }
      return await axios.post(`${process.env.VUE_APP_API_IP}/api/Common/GetFireDatesByCron`, data, {headers: {
        'Authorization': localStorage.getItem('Token')
        }
      })
      .then(( response ) => {
        console.log(response)
        this.examples = response.data.data.dates
        this.showLoader = false
      })
      .catch((error) => {
        console.log(error);
      })
    }
  }
};
</script>

<style lang="scss">
  .cronPreviewContainer {
    padding: 15px;

    .loader {
      border: 4px solid #f3f3f3;
      border-top: 4px solid green;
      border-radius: 50%;
      width: 50px;
      height: 50px;
      animation: spin 2s linear infinite;
    }

    @keyframes spin {
      0% { transform: rotate(0deg); }
      100% { transform: rotate(360deg); }
    }

    .row {
      margin: 10px 0;
      display: flex;
      align-items: center;
      justify-content: flex-start;

      input, label {
        cursor: pointer;
      }

      label {
        margin-left: 10px;
        display: flex;
        width: 100%;
      }

      input[type="text"] {
        height: 28px;
        width: 200px;
        line-height: 28px;
        border-radius: 4px;
        border: 1px solid #DCDFE6;
        color: #606266;
        text-align: center;
      }

      input[type="number"] {
        height: 28px;
        width: 50px;
        line-height: 28px;
        border-radius: 4px;
        border: 1px solid #DCDFE6;
        color: #606266;
        text-align: center;
      }

      .option {

        &:hover {
          background: rgba(0, 0, 0, 0.2);
        }
      }

      .cross {
        margin-left: 10px;
        cursor: pointer;
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
  }
</style>
