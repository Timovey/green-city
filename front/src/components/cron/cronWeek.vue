<template>
  <div class="cronWeekContainer">
    <div class="row">
      <input type="radio" name="cronWeekRadio" id="cronPerWeekRadio" :checked="checkedPosition == 1" class="hidden" @click="chooseCronWeek(0)">
      <label for="cronPerWeekRadio">Каждый день</label>
    </div>
    <div class="row">
      <input type="radio" name="cronWeekRadio" id="cronStepWeekRadio" :checked="checkedPosition == 2" class="hidden" @click="chooseCronWeek(1)">
      <label for="cronWeekRadio">
        <!-- <span class="mr-10">{{stepWeekFromIndex}}/{{stepWeekTo}}</span> -->
        <span>Начиная с</span>
        <div class="stepWeekContainer">
          <select v-model="stepWeekFrom" class="stepWeekInput" @change.prevent="setStepWeekFromIndex(stepWeekFrom)" @click="chooseCronWeek(1)">
            <option v-for="(weekDay, index) in weekDays" :key="index">{{weekDay}}</option>
          </select>
        </div>
        <span> через каждые</span>
        <div class="stepWeekContainer">
          <div class="counterBlock"  @click.stop="changeStepWeekValue('stepWeekTo', 'down')">-</div>
          <input type="text" name="stepWeekTo" disabled v-model="stepWeekTo" class="stepWeekInput">
          <div class="counterBlock"  @click.stop="changeStepWeekValue('stepWeekTo', 'up')">+</div>
        </div>
        <span>дней</span> 
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronWeekRadio" id="cronRangeRadio" :checked="checkedPosition == 3" class="hidden" @click="chooseCronWeek(2)">
      <label for="cronRangeRadio">
        <!-- <span class="mr-10">{{rangeFromIndex}}-{{rangeToIndex}}</span> -->
        <span>С</span>
        <div class="stepWeekContainer">
          <select v-model="rangeFrom" class="stepWeekInput" @change.prevent="setRangeFromIndex(rangeFrom)" @click="chooseCronWeek(2)">
            <option v-for="(weekDay, index) in weekDaysFrom" :key="index">{{weekDay}}</option>
          </select>
        </div>
        <span>по</span>
        <div class="stepWeekContainer">
          <select v-model="rangeTo" class="stepWeekInput" @change.prevent="setRangeToIndex(rangeTo)" @click="chooseCronWeek(2)">
            <option v-for="(weekDay, index) in weekDaysTo" :key="index">{{weekDay}}</option>
          </select>
        </div>
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronWeekRadio" id="cronSpecifiedRadio" :checked="checkedPosition == 4" class="hidden" @click="chooseCronWeek(3)">
      <label for="cronSpecifiedRadio">
        <span class="mr-10">Отдельные дни недели</span>
        <div class="stepWeekContainer stepWeekContainer_big">
          <input type="text" name="specified" v-model="specified" class="stepWeekInput stepWeekInput_big" @click.stop="showOptions = !showOptions">
          <div class="options" v-if="showOptions">
            <ul>
              <li class="option" v-for="(item, index) in weekDays" :key="index" @click.stop="chooseOption(index+1)">
                {{item}}
              </li>
            </ul>
          </div>
        </div>
      </label>
    </div>
    <!-- <div class="row">
      <input type="radio" name="cronWeekRadio" id="cronUnspecifiedRadio" :checked="checkedPosition == 5" class="hidden" @click="chooseCronWeek(4)">
      <label for="cronUnspecifiedRadio">Не задавать</label>
    </div> -->
    <div class="row">
      <input type="radio" name="cronWeekRadio" id="cronLastWeekDayRadio" :checked="checkedPosition == 6" class="hidden"  @click="chooseCronWeek(5)">
      <label for="cronLastWeekDayRadio">
        <!-- <span class="mr-10">{{lastWeekDayIndex}}L</span> -->
        <span>Последний</span>
        <div class="stepWeekContainer">
          <select v-model="lastWeekDay" class="stepWeekInput" @change.prevent="setLastWeekDayIndex(lastWeekDay)" @click="chooseCronWeek(5)">
            <option v-for="(weekDay, index) in weekDays" :key="index">{{weekDay}}</option>
          </select>
        </div>
        <span>текущего месяца</span>
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronWeekRadio" id="cronNumberWeekRadio" :checked="checkedPosition == 7" class="hidden"  @click="chooseCronWeek(6)">
      <label for="cronNumberWeekRadio">
        <!-- <span class="mr-10">{{weekDayIndex}}#{{numberWeek}}</span> -->
        <div class="stepWeekContainer">
          <div class="counterBlock" @click.stop="changeNumberWeekValue('numberWeek', 'down')">-</div>
          <input type="text" name="numberWeek" disabled v-model="numberWeek" class="stepWeekInput">
          <div class="counterBlock" @click.stop="changeNumberWeekValue('numberWeek', 'up')">+</div>
        </div>
        <span>-ый</span>
        <div class="stepWeekContainer">
          <select v-model="weekDay" class="stepWeekInput" @change="setweekDayIndex(weekDay)" @click="chooseCronWeek(6)">
            <option v-for="(weekDay, index) in weekDays" :key="index">{{weekDay}}</option>
          </select>
        </div>
      </label>
    </div>
  </div>
</template>

<script>

export default {
  name: "cronWeek",

  components: {
  },

  props: {
    week: {
      type: String,
      default: '*'
    }
  },

  data() {
    return {
      checkedPosition: 0,
      showOptions: false,
      weekDays: ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'],
      stepWeekFrom: 'Воскресенье',
      stepWeekFromIndex: '1',
      stepWeekTo: '1',
      rangeFrom: 'Воскресенье',
      rangeFromIndex: '1',
      rangeTo: 'Воскресенье',
      rangeToIndex: '1',
      lastWeekDay: 'Воскресенье',
      lastWeekDayIndex: '1',
      specifiedArray: [1],
      numberWeek: '1',
      weekDay: 'Воскресенье',
      weekDayIndex: '1',
    }
  },

  mounted() {
    if (this.week == '*') {
      this.checkedPosition = 1
    }
    else if (this.week.includes('/')) {
      this.stepWeekFromIndex = this.week.slice(0, this.week.indexOf('/'))
      this.stepWeekFrom = this.weekDays[this.stepWeekFromIndex-1]
      this.stepWeekTo = this.week.slice(this.week.indexOf('/')+1)
      this.checkedPosition = 2
    }
    else if (this.week.includes('-')) {
      this.rangeFromIndex = this.week.slice(0, this.week.indexOf('-'))
      this.rangeFrom = this.weekDays[this.rangeFromIndex-1]
      this.rangeToIndex = this.week.slice(this.week.indexOf('-')+1)
      this.rangeTo = this.weekDays[this.rangeToIndex-1]
      this.checkedPosition = 3
    }
    else if (this.week.includes(',')) {
      this.specifiedArray = this.week.split(',')
      this.checkedPosition = 4
    }
    else if (this.week == '?') {
      this.checkedPosition = 5
    }
    else if (this.week.includes('L')) {
      this.lastWeekDayIndex = this.week.slice(0, this.week.indexOf('L'))
      this.lastWeekDay = this.weekDays[this.lastWeekDayIndex-1]
      this.checkedPosition = 6
    }
    else if (this.week.includes('#')) {
      this.numberWeek = this.week.slice(0, this.week.indexOf('#'))
      this.weekDayIndex = this.week.slice(this.week.indexOf('#')+1)
      this.weekDay = this.weekDays[this.weekDayIndex-1]
      this.checkedPosition = 7
    }
  },

  computed: {
    specified() {
      return this.specifiedArray.join()
    },

    weekDaysFrom() {
      let array = this.weekDays.slice()
      array = array.splice(0, this.rangeToIndex)
      return array
    },

    weekDaysTo() {
      let array = this.weekDays.slice()
      array = array.splice(this.rangeFromIndex-1)
      return array
    }
  },

  methods: {
    setStepWeekFromIndex(value) {
      for (let i = 0; i < this.weekDays.length; i++) {
        if (this.weekDays[i] == value) {
          this.stepWeekFromIndex = i+1
        }
      }
    },

    setweekDayIndex(value) {
      for (let i = 0; i < this.weekDays.length; i++) {
        if (this.weekDays[i] == value) {
          this.weekDayIndex = i+1
        }
      }
    },

    setLastWeekDayIndex(value) {
      for (let i = 0; i < this.weekDays.length; i++) {
        if (this.weekDays[i] == value) {
          this.lastWeekDayIndex = i+1
        }
      }
    },

    setRangeFromIndex(value) {
      for (let i = 0; i < this.weekDays.length; i++) {
        if (this.weekDays[i] == value) {
          this.rangeFromIndex = i+1
        }
      }
    },

    setRangeToIndex(value) {
      for (let i = 0; i < this.weekDays.length; i++) {
        if (this.weekDays[i] == value) {
          this.rangeToIndex = i+1
        }
      }
    },

    changeNumberWeekValue(model, action) {
      if (action === 'down') {
        if (this[model] > 1) {
          this[model]--
        }
      }
      else if (action === 'up') {
        if (this[model] < 5) {
          this[model]++
        }
      }
    },

    changeStepWeekValue(model, action) {
      if (action === 'down') {
        if (this[model] > 1) {
          this[model]--
        }
      }
      else if (action === 'up') {
        if (this[model] < 31) {
          this[model]++
        }
      }
    },

    chooseOption(item) {
      let foundFlag = false
      for (let i = 0; i < this.specifiedArray.length; i++) {
        if (this.specifiedArray[i] == item) {
          this.specifiedArray.splice(i ,1)
          foundFlag = true
        }
      }
      if (!foundFlag) {
        this.specifiedArray.push(item)
      }
      this.sortArray()
      console.log(this.specifiedArray)
    },

    sortArray() {
      return this.specifiedArray.sort((a, b) => {
        return a-b
      })
    },

    chooseCronWeek(index) {
      this.$root.$refs.Cron_component.setDay('?')
      switch(index) {
        case 0: 
          this.$emit('cronWeek', '*')
          break
        case 1:
          this.$emit('cronWeek', this.stepWeekFromIndex+'/'+this.stepWeekTo)
          break
        case 2:
          this.$emit('cronWeek', this.rangeFromIndex+'-'+this.rangeToIndex)
          break
        case 3:
          this.$emit('cronWeek', this.specified)
          break
        case 4:
          this.$emit('cronWeek', '?')
          break
        case 5:
          this.$emit('cronWeek', (this.lastWeekDayIndex).toString()+'L')
          break
        case 6:
          this.$emit('cronWeek', this.weekDayIndex+'#'+this.numberWeek)
          break
      }
    }
  }
};
</script>

<style lang="scss" scoped>
  .cronWeekContainer {
    padding: 15px;

    .mr-10 {
      margin-right: 10px;
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
        width: 60px;
        line-height: 28px;
        border-radius: 4px;
        border: 1px solid #DCDFE6;
        color: #606266;
        text-align: center;
      }

      span {
        display: flex;
        align-items: center;
        justify-content: center;
      }

      .stepWeekInput {

        &_big {
          width: 100% !important;
        }
      }

      .stepWeekContainer {
        position: relative;
        margin: 0 10px;
        display: flex;
        width: 130px;
        justify-content: center;
        align-items: center;

        &_big {
          width: 100%;
        }

        .counterBlock {
          width: 35px;
          display: flex;
          justify-content: center;
          align-items: center;
          height: 100%;
          background: #F5F7FA;
          color: #606266;
          cursor: pointer;
          border: 1px solid #DCDFE6;
        }

        .options {
          position: absolute;
          bottom: -300px;
          left: 0;
          width: 100%;
          height: 300px;
          content: '';
          overflow: auto;
          background: #FFF;
          border: 1px solid #000;
          z-index: 10;

          .option {

            &:hover {
              background: rgba(0, 0, 0, 0.2);
            }
          }
        }
      }
    }
  }
</style>
