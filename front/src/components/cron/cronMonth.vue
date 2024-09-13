<template>
  <div class="cronMonthContainer">
    <div class="row">
      <input type="radio" name="cronMonthRadio" id="cronPerMonthRadio" :checked="checkedPosition == 1" class="hidden" @click="chooseCronMonth(0)">
      <label for="cronPerMonthRadio">Каждый месяц</label>
    </div>
    <div class="row">
      <input type="radio" name="cronMonthRadio" id="cronStepMonthRadio" :checked="checkedPosition == 2" class="hidden"  @click="chooseCronMonth(1)">
      <label for="cronStepMonthRadio">
        <!-- <span class="mr-10">{{stepMonthFrom}}/{{stepMonthTo}}</span> -->
        <span>С</span>
        <div class="stepMonthContainer">
          <div class="counterBlock" @click.stop="changeStepMonthValue('stepMonthFrom', 'down')">-</div>
          <input type="text" name="stepMonthFrom" disabled v-model="stepMonthFrom" class="stepMonthInput">
          <div class="counterBlock" @click.stop="changeStepMonthValue('stepMonthFrom', 'up')">+</div>
        </div>
        <span>месяца через каждые</span>
        <div class="stepMonthContainer">
          <div class="counterBlock"  @click.stop="changeStepMonthValue('stepMonthTo', 'down')">-</div>
          <input type="text" name="stepMonthTo" disabled v-model="stepMonthTo" class="stepMonthInput">
          <div class="counterBlock"  @click.stop="changeStepMonthValue('stepMonthTo', 'up')">+</div>
        </div>
        <span>месяцев</span> 
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronMonthRadio" id="cronRangeRadio" :checked="checkedPosition == 3" class="hidden" @click="chooseCronMonth(2)">
      <label for="cronRangeRadio">
        <!-- <span class="mr-10">{{rangeFrom}}-{{rangeTo}}</span> -->
        <span>С</span>
        <div class="stepMonthContainer">
          <div class="counterBlock" @click.stop="changeRangeValue('rangeFrom', 'down')">-</div>
          <input type="text" name="rangeFrom" disabled v-model="rangeFrom" class="stepMonthInput">
          <div class="counterBlock" @click.stop="changeRangeValue('rangeFrom', 'up')">+</div>
        </div>
        <span>по</span>
        <div class="stepMonthContainer">
          <div class="counterBlock"  @click.stop="changeRangeValue('rangeTo', 'down')">-</div>
          <input type="text" name="rangeTo" disabled v-model="rangeTo" class="stepMonthInput">
          <div class="counterBlock"  @click.stop="changeRangeValue('rangeTo', 'up')">+</div>
        </div>
      </label>
    </div>
    <div class="row">
      <input type="radio" name="cronMonthRadio" id="cronSpecifiedRadio" :checked="checkedPosition == 4" class="hidden" @click="chooseCronMonth(3)">
      <label for="cronSpecifiedRadio">
        <span class="mr-10">Отдельные месяца</span>
        <div class="stepMonthContainer stepMonthContainer_big">
          <input type="text" name="specified" v-model="specified" class="stepMonthInput stepMonthInput_big" @click.stop="showOptions = !showOptions">
          <div class="options" v-if="showOptions">
            <ul>
              <li class="option" v-for="(item, index) in 12" :key="index" @click.stop="chooseOption(item)">
                {{item}}
              </li>
            </ul>
          </div>
        </div>
      </label>
    </div>
  </div>
</template>

<script>

export default {
  name: "cronMonth",

  components: {
  },

  props: {
    month: {
      type: String,
      default: '*'
    }
  },

  data() {
    return {
      checkedPosition: 0,
      showOptions: false,
      stepMonthFrom: '1',
      stepMonthTo: '1',
      rangeFrom: '1',
      rangeTo: '1',
      specifiedArray: [1],
    }
  },

  computed: {
    specified() {
      return this.specifiedArray.join()
    }
  },

  mounted() {
    if (this.month == '*') {
      this.checkedPosition = 1
    }
    else if (this.month.includes('/')) {
      this.stepMonthFrom = this.month.slice(0, this.month.indexOf('/'))
      this.stepMonthTo = this.month.slice(this.month.indexOf('/')+1)
      this.checkedPosition = 2
    }
    else if (this.month.includes('-')) {
      this.rangeFrom = this.month.slice(0, this.month.indexOf('-'))
      this.rangeTo = this.month.slice(this.month.indexOf('-')+1)
      this.checkedPosition = 3
    }
    else if (this.month.includes(',')) {
      this.specifiedArray = this.month.split(',')
      this.checkedPosition = 4
    }
  },

  methods: {
    changeStepMonthValue(model, action) {
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

    chooseCronMonth(index) {
      switch(index) {
        case 0: 
          this.$emit('cronMonth', '*')
          break
        case 1:
          this.$emit('cronMonth', this.stepMonthFrom+'/'+this.stepMonthTo)
          break
        case 2:
          this.$emit('cronMonth', this.rangeFrom+'-'+this.rangeTo)
          break
        case 3:
          this.$emit('cronMonth', this.specified)
          break
      }
    }
  }
};
</script>

<style lang="scss" scoped>
  .cronMonthContainer {
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

      .stepMonthInput {

        &_big {
          width: 100% !important;
        }
      }

      .stepMonthContainer {
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
