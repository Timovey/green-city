<template>
  <div>
  <GlobalMenu title="БМ" :menu="menu" v-model="drawer"/>
  <v-dialog scrollable width="80%" v-model="showFilters">
    
  </v-dialog>
  <PageLayout>
    <v-row >
      <v-col class="" >
        <v-expansion-panels
          multiple
          accordion
        >
          <v-expansion-panel
            v-for="(item, tripKey) in arr"
            :key="tripKey"
          >
          <v-expansion-panel-header>
            {{ item.name }}

          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <div class="d-flex flex-row justify-space-between">
              <v-btn
                icon
                color="red"
                @click="removeTrip(item)"	
              >
                <v-icon>mdi-minus</v-icon>
              </v-btn>
              <v-btn
                icon
                color="grey"
                @click="upTrip(item)"	
              >
                <v-icon>mdi-arrow-up</v-icon>
              </v-btn>
              <v-btn
                icon
                color="grey"
                @click="downTrip(item)"	
              >
                <v-icon>mdi-arrow-down</v-icon>
              </v-btn>
            </div>
            
            <MissionMovedCard
              class="ma-2" 
              v-for="(mission, missionKey) in item.missions" 
              :key="missionKey" 
              :mission="mission"
              @onRemove="removeMission"
            />
          </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-col>
      <v-spacer/>
      <v-col class="" >

        <v-expansion-panels
          multiple
          accordion
        >
          <v-expansion-panel
            v-for="(item, tripKey) in serverTrips"
            :key="tripKey"
          >
          <v-expansion-panel-header>
            {{ item.name }}

          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <v-btn
              icon
              color="primary"
              @click="moveTrip(item)"	
            >
              <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <MissionInitialCard
              class="ma-2" 
              v-for="(mission, missionKey) in item.missions" 
              :key="missionKey" 
              :mission="mission"
              @onMove="moveMission"
            />
          </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
        
      </v-col>
    </v-row>
  </PageLayout>
</div>
</template>
<script>
import GlobalMenu from "@/components/UI/GlobalMenu"
import PageLayout from "@/components/UI/PageLayout"
import { logistMenu } from "@/helpers/menus"
import MissionInitialCard from '@/components/logist/MissionInitialCard';
import MissionMovedCard from '@/components/logist/MissionMovedCard';
import { mapState } from "vuex"
import { showSuccess, showErrorMessage } from "@/helpers/messages"
import { toArray } from "@/helpers/objectHelper"
export default {
components: {
  GlobalMenu,
  PageLayout,
  MissionInitialCard,
  MissionMovedCard
},
created() {
      this.menu = logistMenu
  },
  data(){
  return {
    serverTrips: [
      {
        id: 1,
        "name": "первый",
        "missions": [
          {
            id: 1,
            name: "первая миссия",
            tripId: 1,
          },
          {
            id: 2,
            name: "вторая миссия",
            tripId: 1,
          },
          {
            id: 4,
            name: "четвертая миссия",
            tripId: 1,
          },
        ]	
      },
      {
        id: 2,
        "name": "второй",
        "missions": [
          {
            id: 3,
            name: "третья миссия",
            tripId: 2,
          },
          {
            id: 5,
            name: "пыятая миссия",
            tripId: 2,
          },
        ]	
      },
      {
        id: 3,
        "name": "третий",
        "missions": [
          {
            id: 6,
            name: "шестая",
            tripId: 3,
          },
          {
            id: 7,
            name: "шестая",
            tripId: 3,
          },
        ]	
      },
    ],
    arr: [],
    menu: [],
    showFilters: false,
    drawer: false
  }
},
methods: {
  moveMission(mission) {
    // console.log(mission);
    let movedTrip = this.arr.find(el => el.id == mission.tripId);
    let initialTrip = this.serverTrips.find(el => el.id == mission.tripId);

    if(movedTrip) {
      movedTrip.missions.push({...mission})	
    }
    else {
      let copyTrip = {...initialTrip}
      copyTrip.missions = [{...mission}]
      this.arr.push(copyTrip)
    }

    if(initialTrip) {
      let missionArrIndex = initialTrip.missions.findIndex(el => el.id == mission.id);
      if(~missionArrIndex) {
        initialTrip.missions.splice(missionArrIndex, 1);
      }
      if(initialTrip.missions.length == 0) {
        let initialTripIndex = this.serverTrips.findIndex(el => el.id == mission.tripId);
        this.serverTrips.splice(initialTripIndex,1)
      }
    }
  },
  moveTrip(trip) {
    let movedTripIndex = this.arr.findIndex(el => el.id == trip.id);
    let initialTripIndex = this.serverTrips.findIndex(el => el.id == trip.id);
    if(~movedTripIndex) {
      this.arr[movedTripIndex].missions = [...this.arr[movedTripIndex].missions, ...trip.missions]
    }
    else {
      this.arr.push({...trip})
    }
    this.serverTrips.splice(initialTripIndex,1)
  },
  removeMission(mission) {
    let initialTrip = this.serverTrips.find(el => el.id == mission.tripId);
    let movedTrip = this.arr.find(el => el.id == mission.tripId);
    
    if(initialTrip) {
      initialTrip.missions.push({...mission})
    }
    else {
      let trip = {...movedTrip}
      trip.missions = [{...mission}]
      this.serverTrips.push(trip)
    }

    if(movedTrip) {
      let missionArrIndex = movedTrip.missions.findIndex(el => el.id == mission.id);
      if(~missionArrIndex) {
        movedTrip.missions.splice(missionArrIndex, 1);
      }
      if(movedTrip.missions.length == 0) {
        let movedTripIndex = this.arr.findIndex(el => el.id == mission.tripId);
        this.arr.splice(movedTripIndex,1)
      }
    }
  },
  removeTrip(trip) {
    let movedTripIndex = this.arr.findIndex(el => el.id == trip.id);
    let initialTripIndex = this.serverTrips.findIndex(el => el.id == trip.id);

    if(~initialTripIndex) {
      this.serverTrips[initialTripIndex].missions = [...this.serverTrips[initialTripIndex].missions, ...trip.missions]
    }
    else {
      this.serverTrips.push({...trip})
    }
    this.arr.splice(movedTripIndex,1)
  },
  upTrip(trip) {
    let movedTripIndex = this.arr.findIndex(el => el.id == trip.id);
    if(movedTripIndex > 0) {
      let t = {...this.arr[movedTripIndex - 1]}
      this.arr.splice(movedTripIndex - 1, 1 , {...this.arr[movedTripIndex]})
      this.arr.splice(movedTripIndex, 1 , t)
    }	
  },
  downTrip(trip) {
    let movedTripIndex = this.arr.findIndex(el => el.id == trip.id);
    if(movedTripIndex < this.arr.length - 1) {
      let t = {...this.arr[movedTripIndex + 1]}
      this.arr.splice(movedTripIndex + 1, 1 , {...this.arr[movedTripIndex]})
      this.arr.splice(movedTripIndex, 1 , t)
    }	
  },
},
}
</script>
<style>
  
</style>