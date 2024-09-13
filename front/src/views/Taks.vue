<template>
  <div class="mainView">
    <div class="mainView__navigation">
      <navigation @view="changeCurrentView" :title="title" :categories="categories"/>
    </div>
    <div class="mainView__container">
      <vehicles v-if="currentView === 'vehicles'"/>
      <vehiclesRepair v-else-if="currentView === 'vehiclesRepair'"/>
      <operatingSchedule v-else-if="currentView === 'operatingSchedule'" />
      <employees v-else-if="currentView === 'employees'" />
      <vehiclesSchedule v-else-if="currentView === 'vehiclesSchedule'" />
      <waybill v-else-if="currentView === 'waybill'" />
      <workPlan v-else-if="currentView === 'workPlan'" />
      <driversSchedule v-else-if="currentView === 'driversSchedule'" />
      <dutyShifts v-else-if="currentView === 'dutyShifts'" />
      <fixingDrivers v-else-if="currentView === 'fixingDrivers'" />
      <test v-else-if="currentView === 'test'" />
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import navigation from "@/components/navigation/navigation.vue";
import vehicles from "@/components/pages/taks/vehicles.vue";
import vehiclesRepair from "@/components/pages/taks/vehiclesRepair.vue";
import operatingSchedule from "@/components/pages/taks/operatingSchedule.vue";
import employees from "@/components/pages/taks/employees.vue";
import vehiclesSchedule from "@/components/pages/taks/vehiclesSchedule.vue"
import waybill from "@/components/pages/taks/waybill.vue"
import workPlan from "@/components/pages/taks/workPlan.vue"
import driversSchedule from "@/components/pages/taks/driversSchedule.vue"
import dutyShifts from "@/components/pages/taks/dutyShifts.vue"
import fixingDrivers from "@/components/pages/taks/fixingDrivers.vue"
import test from "@/components/pages/taks/test.vue"

export default {
  name: "Taks",

  components: {
    navigation,
    vehicles,
    vehiclesRepair,
    operatingSchedule,
    employees,
    vehiclesSchedule,
    waybill,
    workPlan,
    driversSchedule,
    dutyShifts,
    fixingDrivers,
    test,
  },

  data() {
    return {
      title: 'Таксировщик',
      currentView: '',
      categories: [
        {
          name: 'ТС',
          subcategories: [
            {
              name: 'Транспортные средства',
              link: 'vehicles',
            },
            {
              name: 'График работы ТС',
              link: 'vehiclesSchedule',
            },
            {
              name: 'Ремонт и ТО',
              link: 'vehiclesRepair',
            },
          ]
        },
        {
          name: 'Организации',
          link: 'test'
        },
        {
          name: 'Сотрудники',
          subcategories: [
            {
              name: 'График работы водителей',
              link: 'test',
            },
            {
              name: 'Реестр водителей',
              link: 'test',
            },
            {
              name: 'Отпуск/Больничный',
              link: 'test',
            },
            {
              name: 'Закрепление водителей',
              link: 'test',
            },
          ]
        },
        {
          name: 'Дежурные смены',
          link: 'test'
        },
        {
          name: 'Путевой лист',
          link: 'test'
        },
        {
          name: 'План рабочих смен',
          link: 'test'
        },
      ]
    }
  },

  methods: {
    changeCurrentView(data) {
      this.currentView = data.link
    }
  },

  created() {
    this.$root.$refs.Main__component = this
  }
};
</script>

<style lang="scss" scoped>
.mainView {
  display: flex;

  &__navigation {
    width: 16%;
    min-height: 100vh;
  }

  &__container {
    width: 84%;
    min-height: 100vh;
    padding: 25px 15px 25px 20px;
  }
}
</style>
