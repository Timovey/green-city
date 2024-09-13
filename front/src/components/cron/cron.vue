<template>
  <div class="cronContainer">
    <input type="text" class="cronExpression__input" v-model="cronCopy" readonly @click="showModal = !showModal">
    <div class="cronExpression__modal" v-if="showModal">
      <div class="modal__header">
        <ul>
          <li v-for="(tab, index) in headerTabs" :key="index" :class="{active: tab.isActive}" @click="changeActive(tab)">
            {{tab.name}}
          </li>
        </ul>
      </div>
      <div class="modal__content">
        <cronDay v-if="headerTabs[0].isActive" :day="days" @cronDay="setDay" />
        <cronMonth v-if="headerTabs[1].isActive" :month="months" @cronMonth="setMonth" />
        <cronWeek v-if="headerTabs[2].isActive" :week="weeks" @cronWeek="setWeek" />
        <cronYear v-if="headerTabs[3].isActive" :year="years" @cronYear="setYear" />
      </div>
    </div>
  </div>
</template>

<script>
import cronDay from "@/components/cron/cronDay.vue";
import cronMonth from "@/components/cron/cronMonth.vue";
import cronWeek from "@/components/cron/cronWeek.vue";
import cronYear from "@/components/cron/cronYear.vue";

export default {
  name: "cron",

  components: {
    cronDay,
    cronMonth,
    cronWeek,
    cronYear
  },

  props: {
    cron: {
      type: String,
      default: ''
    },

    id: {
      type: Number,
      default: null
    }
  },

  data() {
    return {
      showModal: false,
      cronCopy: '',
      seconds: '',
      minutes: '',
      hours: '',
      days: '',
      months: '',
      weeks: '',
      years: '',
      headerTabs: [
        {
          name: 'Дни',
          isActive: true
        },
        {
          name: 'Месяца',
          isActive: false
        },
        {
          name: 'Недели',
          isActive: false
        },
        {
          name: 'Года',
          isActive: false
        }
      ]
    }
  },

  mounted() {
    this.$root.$refs.Cron_component = this
    this.cronCopy = this.cron
    console.log(this.cron)
    this.parseCronExpression(this.cronCopy)
  },

  methods: {

    parseCronExpression(cronExpression) {
      let cronExpressionArray = cronExpression.split(' ')
      this.seconds = cronExpressionArray[0]
      this.minutes = cronExpressionArray[1]
      this.hours = cronExpressionArray[2]
      this.days = cronExpressionArray[3]
      this.months = cronExpressionArray[4]
      this.weeks = cronExpressionArray[5]
      this.years = cronExpressionArray[6]
    },

    changeActive(tab) {
      this.headerTabs.forEach((item) => {
        item.isActive = false
      })
      tab.isActive = true
    },

    setDay(value) {
      this.changeCronExpression('day', value)
    },

    setMonth(value) {
      this.changeCronExpression('month', value)
    },

    setWeek(value) {
      this.changeCronExpression('week', value)
    },

    setYear(value) {
      this.changeCronExpression('year', value)
    },

    changeCronExpression(param, value) {
      let cronExpressionArray = this.cronCopy.split(' ')
      switch(param) {
        case 'day':
          cronExpressionArray[3] = value
          this.days = cronExpressionArray[3]
          break
        case 'month':
          cronExpressionArray[4] = value
          this.months = cronExpressionArray[4]
          break
        case 'week':
          cronExpressionArray[5] = value
          this.weeks = cronExpressionArray[5]
          break
        case 'year':
          cronExpressionArray[6] = value
          this.years = cronExpressionArray[6]
          break
      }
      this.cronCopy = cronExpressionArray.join(' ')
      this.$emit('cron', this.cronCopy, this.id)
    }
  }
};
</script>

<style lang="scss" scoped>
  .cronContainer {
    position: relative;
    height: 100%;

    .cronExpression {
      &__input {
        height: 28px;
        line-height: 28px;
        border-radius: 4px;
        border: 1px solid #DCDFE6;
        color: #606266;
        padding: 0 15px;
      }

      &__modal {
        z-index: 10;
        position: absolute;
        background: #FFF;
        border: 1px solid #DCDFE6;
        top: 40px;
        left: 0;
        width: 600px;

        .modal__header {
          height: 40px;
          background-color: #F5F7FA;
          border-bottom: 1px solid #E4E7ED;

          ul {
            display: flex;
            height: 100%;
            justify-content: space-around;

            li {
              width: 100%;
              display: flex;
              align-items: center;
              justify-content: center;
              color: #909399;
              cursor: pointer;
              transition: all .2s ease;

              &:hover {
                color: #409EFF;
              }
            }

            .active {
              color: #409EFF;
              background: #FFF;
            }
          }
        }
      }
    }
  }
</style>
