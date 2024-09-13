<template>
  <div class="cronSettingsContainer">
    <div class="row">
      <input type="checkbox" name="stepRadio" :checked="contractScheduleSettingsCopy.enableWeekendWorkDates" id="stepRadio" class="hidden" @click="changeEnableWorkDay">
      <label for="stepRadio">Учитывать рабочие выходные дни</label>
    </div>
    <div class="row">
      <div>
        В случае выпадения на праздники:
        <div class="option" v-for="(reaction, index) in reactions" :key="index">
          <input type="radio" name="cronMonthRadio" :checked="contractScheduleSettingsCopy.redDateReactionType == index+1" :id="'cronPerMonthRadio_'+index" class="hidden" @click="chooseCronSetting(reaction.id)">
          <label :for="'cronPerMonthRadio_'+index">{{reaction.name}}</label>
        </div>
      </div>
      <div class="ml-80">
        В случае выпадения на выходные:
        <div class="option" v-for="(reaction, index) in reactions" :key="index">
          <input type="radio" name="cronMonthWeekendRadio" :checked="contractScheduleSettingsCopy.weekendReactionType == index+1" :id="'cronMonthWeekendRadio_'+index" class="hidden" @click="chooseCronSettingWeekend(reaction.id)">
          <label :for="'cronMonthWeekendRadio_'+index">{{reaction.name}}</label>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "cronSettings",

  components: {
  },

  props: {
    contractScheduleSettings: {
      type: Object,
      default() {
        return {
          enableWeekendWorkDates: true,
          redDateReactionType: 4,
          weekendReactionType: 4
        }
      }
    }
  },

  data() {
    return {
      contractScheduleSettingsCopy: {
        enableWeekendWorkDates: true,
        redDateReactionType: 4,
        weekendReactionType: 4
      },
      reactions: [
        {
          id: 1,
          name: 'Рабочий день вперед'
        },
        {
          id: 2,
          name: 'Рабочий день назад'
        },
        {
          id: 3,
          name: 'Ближайший рабочий день'
        },
        {
          id: 4,
          name: 'Работать в такие дни'
        },
        {
          id: 5,
          name: 'Игнорировать такие дни'
        },
      ]
    }
  },

  mounted() {
    this.contractScheduleSettingsCopy.enableWeekendWorkDates = this.contractScheduleSettings.enableWeekendWorkDates
    this.contractScheduleSettingsCopy.redDateReactionType = this.contractScheduleSettings.redDateReactionType
    this.contractScheduleSettingsCopy.weekendReactionType = this.contractScheduleSettings.weekendReactionType
  },

  methods: {
    changeEnableWorkDay() {
      this.contractScheduleSettingsCopy.enableWeekendWorkDates = !this.contractScheduleSettingsCopy.enableWeekendWorkDates
      this.chooseCronSetting()
    },

    chooseCronSetting(id = null) {
      if (id) {
        this.contractScheduleSettingsCopy.redDateReactionType = id
      }
      this.$emit('cronSettings', this.contractScheduleSettingsCopy)
    },

    chooseCronSettingWeekend(id = null) {
      if (id) {
        this.contractScheduleSettingsCopy.weekendReactionType = id
      }
      this.$emit('cronSettings', this.contractScheduleSettingsCopy)
    }
  }
};
</script>

<style lang="scss">
  .cronSettingsContainer {
    padding: 15px;

    .row {
      margin: 10px 0;
      display: flex;
      align-items: center;
      justify-content: flex-start;

      .ml-80 {
        margin-left: 80px;
      }

      input, label {
        cursor: pointer;
      }

      label {
        margin-left: 10px;
        display: flex;
        width: 100%;
      }

      .option {
        display: flex;
        align-items: center;
        justify-content: flex-start;
        margin-top: 10px;
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
  }
</style>
