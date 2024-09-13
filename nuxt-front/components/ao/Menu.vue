<template>
    <v-navigation-drawer
        v-model="value"
        absolute
        temporary
        floating
        width="350"
    >
      <div class="pa-4 text-h4">АО</div>
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
                        text: "Контрагенты",
                        ico: "mdi-account",
                        to: "/ao/"
                    },
                    {
                        text: "Ёмкость",
                        ico: "mdi-apps",
                        to: "/ao/capacity"
                    },
                    {
                        text: "Контейнерные площадки",
                        ico: "mdi-truck-cargo-container",
                        to: "/ao/squares"
                    },
                    {
                        text: "Договоры",
                        ico: "mdi-file-document",
                        to: "/ao/contracts"
                    }
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