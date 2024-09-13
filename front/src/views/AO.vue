<template>
  <div class="aoView">
    <div class="aoView__navigation">
      <navigation @view="changeCurrentView" :title="title" :categories="categories"/>
    </div>
    <div class="aoView__container">
      <clients v-if="currentView === 'clients'"/>   
      <containers v-else-if="currentView === 'containers'"/>   
      <containerYards v-else-if="currentView === 'containerYards'"/>   
      <contracts :editView="contractsEditView" :clientId="contractsClientId" v-else-if="currentView === 'contracts'"/>   
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import navigation from "@/components/navigation/navigation.vue";
import clients from "@/components/pages/ao/clients.vue";
import containers from "@/components/pages/ao/containers.vue"
import containerYards from "@/components/pages/ao/containerYards.vue"
import contracts from "@/components/pages/ao/contracts.vue";

export default {
  name: "AO",

  components: {
    navigation,
    clients,
    containers,
    containerYards,
    contracts,
  },

  data() {
    return {
      title: 'АО',
      currentView: '',
      contractsEditView: false,
      contractsClientId: null,
      categories: [
        {
          name: 'Контрагенты',
          link: 'clients'
        },
        {
          name: 'Емкости',
          link: 'containers'
        },
        {
          name: 'Контейнерные площадки',
          link: 'containerYards'
        },
        {
          name: 'Договоры',
          link: 'contracts'
        },
      ]
    }
  },

  methods: {
    changeCurrentView(data, bl = false, id = null) {
      if (bl) {
        this.contractsEditView = true
      }
      this.contractsClientId = id
      this.currentView = data.link
    },
  },

  created() {
    this.$root.$refs.AO__component = this
  }
};
</script>

<style lang="scss" scoped>
.aoView {
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
