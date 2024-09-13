<template>
    <v-app>
        <Menu/>
        <v-main>
            <Nuxt/>
        </v-main>
   </v-app>
 </template>
 
 <script>
 import Menu from "@/components/ao/Menu"
 export default {
   name: 'AoLayout',
   components: {
        Menu
   }
 }
 </script>
 