<template>
  <div class="cronStepContainer">
    <div class="row">
      <input type="checkbox" name="stepRadio" :checked="cronStepChecked" id="stepRadio" class="hidden" @click="changeCronStep">
      <label for="stepRadio">Использовать шаговый график</label>
    </div>
    <div class="row">
      Начальная дата
      <date-picker 
        v-model="stepInitialDateScheduleCopy.initialDate" 
        :lang="lang" 
        :format="format"
        valueType="format"
        input-class="datePickerInput"
        class="datePickerCreate"
        @change="chooseCronStep"
      >
      </date-picker>
    </div>
    <div class="row">
      <input type="number" v-model="stepInitialDateScheduleCopy.stepValue" name="cronStep" id="cronStep" @change="chooseCronStep">
      <label for="cronStep">Шаг</label>
    </div>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

export default {
  name: "cronStep",

  components: {
    DatePicker,
  },

  props: {
    stepInitialDateSchedule: {
      type: Object,
      default() {
        return null
      }
    }
  },

  data() {
    return {
      cronStepChecked: false,
      stepInitialDateScheduleCopy: {
        initialDate: '',
        stepValue: 1
      },
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'YYYY-MM-DD',
    }
  },

  mounted() {
    if (this.stepInitialDateSchedule) {
      this.stepInitialDateScheduleCopy = this.stepInitialDateSchedule
      this.cronStepChecked = true
    }
  },

  methods: {
    changeCronStep() {
      this.cronStepChecked = !this.cronStepChecked
      this.chooseCronStep()
    },

    chooseCronStep() {
      if (this.cronStepChecked) {
        this.$emit('cronStep', this.stepInitialDateScheduleCopy)
      }
      else {
        this.$emit('cronStep', null)
      }
    }
  }
};
</script>

<style lang="scss">
  .cronStepContainer {
    padding: 15px;

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
