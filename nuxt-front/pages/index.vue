<template>
  <div class="d-flex align-center flex-column justify-center" style="height: 100%">
    <div class="d-flex flex-column" v-for="role in filteredRoles" :key="role.link">
      <v-btn class="ma-2" color="primary" :to="role.to">
        {{ role.name }}
      </v-btn>
    </div>
  </div>
    
</template>

<script>
import PageLayout from "@/components/UI/PageLayout"
import { getItem} from "@/helpers/persistanceStorage"
import { deepCopy } from "@/helpers/deepCopyHelper"
import {routeRoles} from "@/helpers/routeRoles"
export default {
  head: {
		title: 'Главная',
	},
  components: {
    PageLayout,
  },
  async created() {
    let user = getItem("user")
    if(user && user.roles) {
      this.roles = user.roles
    } 
    else {
      this.$router.push("/login")
    }
    // this.roles = ["ao", "tax"]
  },
  name: 'IndexPage',
  data() {
      return {
        roles: [],
      }
  },
  computed: {
      filteredRoles() {
        let filteredRouteRoles = deepCopy(routeRoles)
        
        if(this.roles.includes("admin")) {
          return filteredRouteRoles
        }
        else {
          return filteredRouteRoles.filter(el => this.roles.includes(el.link))
        } 
      },
    }
}
</script>
