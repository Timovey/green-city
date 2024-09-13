<template>
  <div class="cronDateContainer">
    <div class="row">
      Выбор дат
      <date-picker 
        v-model="choosenDate" 
        :lang="lang" 
        :format="format"
        valueType="format"
        input-class="datePickerInput"
        class="datePickerCreate"
        @change="chooseDate"
      >
      </date-picker>
    </div>
    <div class="row">
      <ul>
        <li class="option" v-for="(date, index) in scheduleDatesCopy" :key="index">{{date}}
          <span class="cross" @click="deleteDate(date)">X</span>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';

export default {
  name: "cronDates",

  components: {
    DatePicker,
  },

  props: {
    scheduleDates: {
      type: Array,
      default() {
        return []
      }
    }
  },

  data() {
    return {
      choosenDate: '',
      scheduleDatesCopy: [],
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
    this.scheduleDatesCopy = this.scheduleDates
  },

  methods: {
    chooseDate() {
      let foundFlag = false
      for(let i = 0; i < this.scheduleDatesCopy.length; i++) {
        if (this.scheduleDatesCopy[i] == this.choosenDate) {
          this.scheduleDatesCopy.splice(i, 1)
          foundFlag = true
        }
      }
      if (!foundFlag) {
        this.scheduleDatesCopy.push(this.choosenDate)
      }
      this.choosenDate = ''
      this.sortArray()
      this.updateDates()
    },

    deleteDate(date) {
      for(let i = 0; i < this.scheduleDatesCopy.length; i++) {
        if (this.scheduleDatesCopy[i] == date) {
          this.scheduleDatesCopy.splice(i, 1)
        }
      }
      this.updateDates()
    },

    updateDates() {
      this.$emit('cronDates', this.scheduleDatesCopy)
    },

    sortArray() {
      return this.scheduleDatesCopy.sort((a, b) => {
        return a-b
      })
    },
  }
};
</script>

<style lang="scss">
  .cronDateContainer {
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
