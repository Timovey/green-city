<template>
    <v-navigation-drawer
        v-model="value"
        absolute
        temporary
        floating
        width="350"
    >
      <div class="pa-4 text-h4">Таксировщик</div>
      <v-divider></v-divider>

      <v-list>
        <v-list-item-group  active-class="green--text text--accent-4">
            <v-list-item
                exact
                v-for="item in menu"
                :key="item.text"
                link
                :to="item.to"
            >
                <v-list-item-icon>
                    <v-icon>{{ item.ico }}</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>{{ item.text }}</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list-item-group>
        
      </v-list>
    </v-navigation-drawer>
</template>

<script>
    export default {
        data(){
            return {
                menu: [
                    {
                        text: "Транспорт",
                        ico: "mdi-truck",
                        to: "/taks/transports"
                    },
                    {
                        text: "Водители",
                        ico: "mdi-steering",
                        to: "/taks/drivers"
                    },
                ]
            }
        },
        props: {
            value: {
                type: Boolean,
                default: true
            }
        },
        watch: {
            value(val){
                this.$emit("input", val)
            }
        }
    }
</script>