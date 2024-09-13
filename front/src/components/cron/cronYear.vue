<template>
  <div class="cronYearContainer">
    <div class="row">
      <input type="radio" name="cronYearRadio" id="cronPerYearRadio" :checked="checkedPosition == 1" class="hidden" @click="chooseCronYear(0)">
      <label for="cronPerYearRadio">Каждый год</label>
    </div>
    <div class="row">
      <input type="radio" name="cronYearRadio" id="cronStepYearRadio" :checked="checkedPosition == 2" class="hidden"  @click="chooseCronYear(1)">
      <label for="cronStepYearRadio">
        <!-- <span class="mr-10">{{stepYearFrom}}/{{stepYearTo}}</span> -->
        <span>С</span>
        <div class="stepYearContainer">
          <div class="counterBlock" @click.stop="changeStepYearValue('stepYearFrom', 'down')">-</div>
          <input type="text" name="stepYearFrom" disabled v-model="stepYearFrom" class="stepYearInput">
          <div class="counterBlock" @click.stop="changeStepYearValue('stepYearFrom', 'up')">+</div>
        </div>
        <span>года через каждые</span>
        <div class="stepYearContainer">
          <div class="counterBlock"  @click.stop="changeStepYearValue('stepYearTo', 'down')">-</div>
          <input type="text" name="stepYearTo" disabled v-model="stepYearTo" class="stepYearInput">
          <div class="counterBlock"  @click.stop="changeStepYearValue('stepYearTo', 'up')">+</div>
        </div>
        <span>лет</span> 
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronYearRadio" id="cronRangeRadio" :checked="checkedPosition == 3" class="hidden" @click="chooseCronYear(2)">
      <label for="cronRangeRadio">
        <!-- <span class="mr-10">{{rangeFrom}}-{{rangeTo}}</span> -->
        <span>С</span>
        <div class="stepYearContainer">
          <div class="counterBlock" @click.stop="changeRangeValue('rangeFrom', 'down')">-</div>
          <input type="text" name="rangeFrom" disabled v-model="rangeFrom" class="stepYearInput">
          <div class="counterBlock" @click.stop="changeRangeValue('rangeFrom', 'up')">+</div>
        </div>
        <span>по</span>
        <div class="stepYearContainer">
          <div class="counterBlock"  @click.stop="changeRangeValue('rangeTo', 'down')">-</div>
          <input type="text" name="rangeTo" disabled v-model="rangeTo" class="stepYearInput">
          <div class="counterBlock"  @click.stop="changeRangeValue('rangeTo', 'up')">+</div>
        </div>
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronYearRadio" id="cronSpecifiedRadio" :checked="checkedPosition == 4" class="hidden" @click="chooseCronYear(3)">
      <label for="cronSpecifiedRadio">
        <span class="mr-10">Отдельные года</span>
        <div class="stepYearContainer stepYearContainer_big">
          <input type="text" name="specified" v-model="specified" class="stepYearInput stepYearInput_big" @click.stop="showOptions = !showOptions">
          <div class="options" v-if="showOptions">
            <ul>
              <li class="option" v-for="(item, index) in yearsList" :key="index" @click.stop="chooseOption(item)">
                {{item}}
              </li>
            </ul>
          </div>
        </div>
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronYearRadio" id="cronEmptyRadio" :checked="checkedPosition == 5" class="hidden" @click="chooseCronYear(4)">
      <label for="cronEmptyRadio">Пустой</label>
    </div>
  </div>
</template>

<script>

export default {
  name: "cronYear",

  components: {
  },

  props: {
    year: {
      type: String,
      default: '*'
    }
  },

  data() {
    return {
      checkedPosition: 0,
      showOptions: false,
      stepYearFrom: '2022',
      stepYearTo: '2022',
      rangeFrom: '2022',
      rangeTo: '2022',
      specifiedArray: [2022],
    }
  },

  mounted() {
    if (this.year == '*') {
      this.checkedPosition = 1
    }
    else if (this.year.includes('/')) {
      this.stepYearFrom = this.year.slice(0, this.year.indexOf('/'))
      this.stepYearTo = this.year.slice(this.year.indexOf('/')+1)
      this.checkedPosition = 2
    }
    else if (this.year.includes('-')) {
      this.rangeFrom = this.year.slice(0, this.year.indexOf('-'))
      this.rangeTo = this.year.slice(this.year.indexOf('-')+1)
      this.checkedPosition = 3
    }
    else if (this.year.includes(',')) {
      this.specifiedArray = this.year.split(',')
      this.checkedPosition = 4
    }
    else if (this.day == ' ') {
      this.checkedPosition = 5
    }
  },

  computed: {
    specified() {
      return this.specifiedArray.join()
    },

    yearsList() {
      let yearArray = []
      for (let i = 2022; i < 2100; i++) {
        yearArray.push(i)
      }
      return yearArray
    }
  },

  methods: {
    changeStepYearValue(model, action) {
      if (action === 'down') {
        if (model === 'stepYearFrom') {
          if (this[model] > 2022) {
            this[model]--
          }
        }
        else if (model === 'stepYearTo') {
          if (this.stepYearFrom < this[model]) {
            this[model]--
          }
        }
      }
      else if (action === 'up') {
        if (model === 'stepYearFrom') {
          if (this[model] < this.stepYearTo) {
            this[model]++
          }
        }
        else if (model === 'stepYearTo') {
          this[model]++
        }
      }
    },

    changeRangeValue(model, action) {
      if (action === 'down') {
        if (model === 'rangeFrom') {
          if (this[model] > 2022) {
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

    chooseCronYear(index) {
      switch(index) {
        case 0: 
          this.$emit('cronYear', '*')
          break
        case 1:
          this.$emit('cronYear', this.stepYearFrom+'/'+this.stepYearTo)
          break
        case 2:
          this.$emit('cronYear', this.rangeFrom+'-'+this.rangeTo)
          break
        case 3:
          this.$emit('cronYear', this.specified)
          break
        case 4:
          this.$emit('cronYear', '')
          break
      }
    }
  }
};
</script>

<style lang="scss" scoped>
  .cronYearContainer {
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

      .stepYearInput {

        &_big {
          width: 100% !important;
        }
      }

      .stepYearContainer {
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
