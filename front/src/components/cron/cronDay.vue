<template>
  <div class="cronDayContainer">
    <div class="row">
      <input type="radio" name="cronDayRadio" :checked="checkedPosition == 1" id="cronPerDayRadio" class="hidden" @click="chooseCronDay(0)">
      <label for="cronPerDayRadio">Каждый день</label>
    </div>
    <div class="row">
      <input type="radio" name="cronDayRadio" :checked="checkedPosition == 2" id="cronStepDayRadio" class="hidden"  @click="chooseCronDay(1)">
      <label for="cronStepDayRadio">
        <!-- <span class="mr-10">{{stepDayFrom}}/{{stepDayTo}}</span> -->
        <span>С</span>
        <div class="stepDayContainer">
          <div class="counterBlock" @click.stop="changeStepDayValue('stepDayFrom', 'down')">-</div>
          <input type="text" name="stepDayFrom" disabled v-model="stepDayFrom" class="stepDayInput">
          <div class="counterBlock" @click.stop="changeStepDayValue('stepDayFrom', 'up')">+</div>
        </div>
        <span>числа через каждые</span>
        <div class="stepDayContainer">
          <div class="counterBlock"  @click.stop="changeStepDayValue('stepDayTo', 'down')">-</div>
          <input type="text" name="stepDayTo" disabled v-model="stepDayTo" class="stepDayInput">
          <div class="counterBlock"  @click.stop="changeStepDayValue('stepDayTo', 'up')">+</div>
        </div>
        <span>дней</span> 
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronDayRadio" :checked="checkedPosition == 3" id="cronRangeRadio" class="hidden" @click="chooseCronDay(2)">
      <label for="cronRangeRadio">
        <!-- <span class="mr-10">{{rangeFrom}}-{{rangeTo}}</span> -->
        <span>С</span>
        <div class="stepDayContainer">
          <div class="counterBlock" @click.stop="changeRangeValue('rangeFrom', 'down')">-</div>
          <input type="text" name="rangeFrom" disabled v-model="rangeFrom" class="stepDayInput">
          <div class="counterBlock" @click.stop="changeRangeValue('rangeFrom', 'up')">+</div>
        </div>
        <span>по</span>
        <div class="stepDayContainer">
          <div class="counterBlock"  @click.stop="changeRangeValue('rangeTo', 'down')">-</div>
          <input type="text" name="rangeTo" disabled v-model="rangeTo" class="stepDayInput">
          <div class="counterBlock"  @click.stop="changeRangeValue('rangeTo', 'up')">+</div>
        </div>
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronDayRadio" id="cronSpecifiedRadio" :checked="checkedPosition == 4" class="hidden" @click="chooseCronDay(3)">
      <label for="cronSpecifiedRadio">
        <span class="mr-10">Отдельные даты</span>
        <div class="stepDayContainer stepDayContainer_big">
          <input type="text" name="specified" v-model="specified" class="stepDayInput stepDayInput_big" @click.stop="showOptions = !showOptions">
          <div class="options" v-if="showOptions">
            <ul>
              <li class="option" v-for="(item, index) in 31" :key="index" @click.stop="chooseOption(item)">
                {{item}}
              </li>
            </ul>
          </div>
        </div>
      </label>
    </div>
    <!-- <div class="row">
      <input type="radio" name="cronDayRadio" id="cronUnspecifiedRadio" :checked="checkedPosition == 5" class="hidden" @click="chooseCronDay(4)">
      <label for="cronUnspecifiedRadio">Не задавать</label>
    </div> -->
    <div class="row">
      <input type="radio" name="cronDayRadio" id="cronLastDayRadio" :checked="checkedPosition == 6" class="hidden"  @click="chooseCronDay(5)">
      <label for="cronLastDayRadio">
        <!-- <span v-if="lastDay == 1" class="mr-10">L</span> -->
        <!-- <span v-else class="mr-10">L-{{lastDay-1}}</span> -->
        <span>Последний</span>
        <div class="stepDayContainer">
          <div class="counterBlock" @click.stop="changeStepDayValue('lastDay', 'down')">-</div>
          <input type="text" name="lastDay" disabled v-model="lastDay" class="stepDayInput">
          <div class="counterBlock" @click.stop="changeStepDayValue('lastDay', 'up')">+</div>
        </div>
        <span>день текущего месяца</span>
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronDayRadio" id="cronNearestWorkDayRadio" :checked="checkedPosition == 7" class="hidden"  @click="chooseCronDay(6)">
      <label for="cronNearestWorkDayRadio">
        <!-- <span class="mr-10">{{nearestWorkDay}}W</span> -->
        <div class="stepDayContainer">
          <div class="counterBlock" @click.stop="changeStepDayValue('nearestWorkDay', 'down')">-</div>
          <input type="text" name="nearestWorkDay" disabled v-model="nearestWorkDay" class="stepDayInput">
          <div class="counterBlock" @click.stop="changeStepDayValue('nearestWorkDay', 'up')">+</div>
        </div>
        <span>число или ближайший рабочий день</span>
      </label>
    </div>
    <!-- <div class="row">
      <input type="radio" name="cronDayRadio" id="cronLastWorkDayRadio" :checked="checkedPosition == 8" class="hidden"  @click="chooseCronDay(7)">
      <label for="cronLastWorkDayRadio">LW Последний день месяца</label>
    </div> -->
  </div>
</template>

<script>

export default {
  name: "cronDay",

  components: {
  },

  props: {
    day: {
      type: String,
      default: '*'
    }
  },

  data() {
    return {
      checkedPosition: 0,
      showOptions: false,
      stepDayFrom: '1',
      stepDayTo: '1',
      rangeFrom: '1',
      rangeTo: '1',
      specifiedArray: [1],
      lastDay: '1',
      nearestWorkDay: '1',
    }
  },

  mounted() {
    if (this.day == '*') {
      this.checkedPosition = 1
    }
    else if (this.day.includes('/')) {
      this.stepDayFrom = this.day.slice(0, this.day.indexOf('/'))
      this.stepDayTo = this.day.slice(this.day.indexOf('/')+1)
      this.checkedPosition = 2
    }
    else if (this.day.includes('-') && this.day.includes('L') == -1) {
      this.rangeFrom = this.day.slice(0, this.day.indexOf('-'))
      this.rangeTo = this.day.slice(this.day.indexOf('-')+1)
      this.checkedPosition = 3
    }
    else if (this.day.includes(',')) {
      this.specifiedArray = this.day.split(',')
      this.checkedPosition = 4
    }
    else if (this.day == '?') {
      this.checkedPosition = 5
    }
    else if (this.day.includes('L') && this.day != 'LW') {
      if (this.day == 'L') {
        this.lastDay = 1
      }
      else {
        this.lastDay = parseInt(this.day.slice(this.day.indexOf('L')+2))+1
      }
      this.checkedPosition = 6
    }
    else if (this.day.includes('W') && this.day != 'LW') {
      this.nearestWorkDay = this.day.slice(0, this.day.indexOf('W'))
      this.checkedPosition = 7
    }
    else if (this.day == 'LW') {
      this.checkedPosition = 8
    }
  },

  computed: {
    specified() {
      return this.specifiedArray.join()
    }
  },

  methods: {
    changeStepDayValue(model, action) {
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

    changeRangeValue(model, action) {
      if (action === 'down') {
        if (model === 'rangeFrom') {
          if (this[model] > 1) {
            this[model]--
          }
        }
        else if (model === 'rangeTo') {
          if (this.rangeFrom < this[model]) {
            this[model]--
          }
        }
      }
      else if (action === 'up') {
        if (model === 'rangeFrom') {
          if (this[model] < this.rangeTo) {
            this[model]++
          }
        }
        else if (model === 'rangeTo') {
          if (this[model] < 31) {
            this[model]++
          }
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

    chooseCronDay(index) {
      this.$root.$refs.Cron_component.setWeek('?')
      switch(index) {
        case 0: 
          this.$emit('cronDay', '*')
          break
        case 1:
          this.$emit('cronDay', this.stepDayFrom+'/'+this.stepDayTo)
          break
        case 2:
          this.$emit('cronDay', this.rangeFrom+'-'+this.rangeTo)
          break
        case 3:
          this.$emit('cronDay', this.specified)
          break
        case 4:
          this.$emit('cronDay', '?')
          break
        case 5:
          if (this.lastDay > 1) {
            this.$emit('cronDay', 'L-'+(this.lastDay-1).toString())
          }
          else {
            this.$emit('cronDay', 'L')
          }
          break
        case 6:
          this.$emit('cronDay', this.nearestWorkDay+'W')
          break
        case 7:
          this.$emit('cronDay', 'LW')
          break
      }
    }
  }
};
</script>

<style lang="scss" scoped>
  .cronDayContainer {
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

      .stepDayInput {

        &_big {
          width: 100% !important;
        }
      }

      .stepDayContainer {
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
