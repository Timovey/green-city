<template>
  <div class="logistView">
    <div class="logistView__navigation">
      <navigation @view="changeCurrentView" :title="title" :categories="categories"/>
    </div>
    <div class="logistView__container">
      <waybill v-if="currentView === 'waybill'"/>   
      <contracts v-if="currentView === 'contracts'"/>  
      <baseTrips v-if="currentView === 'baseTrips'"/>  
      <task v-if="currentView === 'task'"/>      
      <test v-if="currentView === 'test'"/>  
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import navigation from "@/components/navigation/navigation.vue";
import waybill from "@/components/pages/logist/waybill.vue";
import contracts from "@/components/pages/logist/contracts.vue";
import baseTrips from "@/components/pages/logist/baseTrips.vue";
import task from "@/components/pages/logist/task.vue";
import test from "@/components/pages/logist/test.vue";

export default {
  name: "Logist",

  components: {
    navigation,
    waybill,
    contracts,
    baseTrips,
    task,
    test
  },

  data() {
    return {
      title: 'Логист',
      currentView: '',
      categories: [
        {
          name: 'Путевой лист',
          link: 'waybill'
        },
        {
          name: 'Сменное задание',
          link: 'task'
        },
        {
          name: 'Базовые рейсы',
          link: 'baseTrips'
        },
        {
          name: 'Реестр договоров',
          link: 'contracts'
        },
        {
          name: 'test',
          link: 'test'
        },
      ]
    }
  },

  methods: {
    changeCurrentView(data) {
      this.currentView = data.link
    },
  },

  created() {
    this.$root.$refs.Logist__component = this
  }
};
</script>

<style lang="scss" scoped>
.logistView {
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
