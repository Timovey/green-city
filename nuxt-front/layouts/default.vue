<template>
  <v-app class="mt-0">
    <v-main class="pt-0 mt-0">
      <Nuxt />
    </v-main>
  </v-app>
</template>

<script>
import Menu from '@/components/UI/Menu.vue'
import { getItem } from "@/helpers/persistanceStorage"
export default {
  data(){ 
    return {
      show: false
    }
  },
  created(){
    let user = getItem("user")
    // console.log(user);
    if (!user){
      this.$router.push("/login")
    }
    this.show = true
  },
  name: 'DefaultLayout',
  components: {
    Menu
  }
}
</script>
