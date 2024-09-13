<template>
  <div class="cronModalContainer">
    <div class="cronExpression__modal">
      <div class="cross-container">
        <div>{{containerYard.id}}-{{containerYard.address}} / {{container.id}}-{{container.address}}</div>
        <div @click="closeModal" class="cross">X</div>
      </div>
      <div class="modal__header">
        <ul>
          <li v-for="(tab, index) in headerTabs" :key="index" :class="{active: tab.isActive}" @click="changeActive(tab)">
            {{tab.name}}
          </li>
        </ul>
      </div>
      <div class="modal__content">
        <crons v-if="headerTabs[0].isActive" :crons="cronProp.crons" @cron="setCron" />
        <cronsExclude v-else-if="headerTabs[1].isActive" :crons="cronProp.excludeCrons" @cron="setCronExclude" />
        <cronStep v-else-if="headerTabs[2].isActive" :stepInitialDateSchedule="cronProp.stepInitialDateSchedule" @cronStep="setStep" />
        <cronDates v-else-if="headerTabs[3].isActive" :scheduleDates="cronProp.scheduleDates" @cronDates="setDates" />
        <cronDatesExclude v-else-if="headerTabs[4].isActive" :scheduleDates="cronProp.excludeDates" @cronDates="setExcludeDates" />
        <cronPreview v-else-if="headerTabs[5].isActive" />
        <cronSettings v-else-if="headerTabs[6].isActive" :contractScheduleSettings="cronProp.contractScheduleSettings" @cronSettings="setSettings" />
      </div>
    </div>
  </div>
</template>

<script>
import crons from "@/components/cron/crons.vue";
import cronsExclude from "@/components/cron/cronsExclude.vue";
import cronDatesExclude from "@/components/cron/cronDatesExclude.vue";
import cronPreview from "@/components/cron/cronPreview.vue";
import cronStep from "@/components/cron/cronStep.vue";
import cronDates from "@/components/cron/cronDates.vue";
import cronSettings from "@/components/cron/cronSettings.vue";

export default {
  name: "cronModal",

  components: {
    crons,
    cronsExclude,
    cronDatesExclude,
    cronPreview,
    cronStep,
    cronDates,
    cronSettings
  },

  props: {
    cronProp: {
      type: Object,
      default() {
        return {
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
      scheduleDateFrom: null,
      scheduleDateTo: null,
      lang: {
        formatLocale: {
          firstDayOfWeek: 1,
        },
        monthBeforeYear: false,
      },
      format: 'YYYY-MM-DD',
      headerTabs: [
        {
          name: 'CRON',
          isActive: true
        },
        {
          name: 'Исключающий CRON',
          isActive: false
        },
        {
          name: 'Шаговый',
          isActive: false
        },
        {
          name: 'Даты',
          isActive: false
        },
        {
          name: 'Исключающие даты',
          isActive: false
        },
        {
          name: 'Предпросмотр',
          isActive: false
        },
        {
          name: 'Настройки',
          isActive: false
        }
      ],
    }
  },

  created() {
    this.$root.$refs.CronModal_component = this
    console.log(this.cronProp)
    console.log(this.containerYard, this.container)
  },

  methods: {
    closeModal() {
      if (this.common) {
        this.$emit('closeCommon', this.cronProp)
      }
      else {
        this.$emit('close', this.cronProp, this.container.id)
      }
    },

    changeActive(tab) {
      this.headerTabs.forEach((item) => {
        item.isActive = false
      })
      tab.isActive = true
    },

    setCron(value) {
      this.cronProp.crons = value
      console.log(this.cronProp.crons)
    },

    setCronExclude(value) {
      this.cronProp.excludeCrons = value
      console.log(this.cronProp.excludeCrons)
    },

    setStep(value) {
      this.cronProp.stepInitialDateSchedule = value
      console.log(this.cronProp.stepInitialDateSchedule)
    },

    setDates(value) {
      this.cronProp.scheduleDates = value
      console.log(this.cronProp.scheduleDates)
    },

    setExcludeDates(value) {
      this.cronProp.excludeDates = value
      console.log(this.cronProp.excludeDates)
    },

    setSettings(value) {
      this.cronProp.contractScheduleSettings = value
      console.log(this.cronProp.contractScheduleSettings)
    }
  }
}
</script>

<style lang="scss" scoped>
  .cronModalContainer {
    height: 100%;
    width: 100%;
    position: absolute;
    top: 0;
    left: 0;

    .cross-container {
      display: flex;
      justify-content: space-between;
      align-items: center;

      .cross {
        cursor: pointer;
      }
    }

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
        background: #FFF;
        border: 1px solid #DCDFE6;
        border-bottom: none;
        height: 100%;

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
