<template>
    <v-dialog scrollable v-model="show" @click:outside="clearData">
        <v-snackbar v-model="showAlert">
            {{ alert }}
        </v-snackbar>
        <v-card elevation="0">
            <v-card-text class="pa-0">
                <v-stepper v-model="step">
                    <v-stepper-header>
                        <v-stepper-step :complete="(step > 1)" step="1">
                            Выбор КП и контейнеров
                        </v-stepper-step>
                        <v-divider></v-divider>
                        <v-stepper-step :complete="(step > 2)" step="2">
                            Задайте расписание по умолчанию
                        </v-stepper-step>
                    </v-stepper-header>
                    <v-stepper-items>
                        <v-stepper-content step="1">
                            <v-card-text>
                                <v-autocomplete class="mt-4" v-model="kp" :items="squares" item-value="standId"
                                    item-text="address" filled label="Контейнерная площадка" outlined clearable />
                                <v-card class="pa-2">
                                    <template v-if="containers.length">
                                        <div :key="item.name" v-for="item in containers">
                                            <v-checkbox class="pa-2" :label="item.name" :value="item"
                                                v-model="selectedContainers" />
                                        </div>
                                    </template>
                                    <v-alert v-else outlined dense border="left" type="warning">
                                        Для данной площадки не добавлено контейнеров
                                    </v-alert>
                                </v-card>
                            </v-card-text>
                        </v-stepper-content>
                        <v-stepper-content step="2">
                            <AddContainerContract @onValidate="onValidate" @onDataChange="changeDefault" :byKP="true"
                                class="pa-0" :current="false" :containers="selectedContainers" @onContainerAdd="" />
                        </v-stepper-content>
                    </v-stepper-items>
                </v-stepper>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn v-if="(step < 2)" @click="goNext" color="primary">
                    Далее
                </v-btn>
                <v-btn v-else @click="(step = 1)" color="primary">
                    Назад
                </v-btn>
                <!-- захардкодил так, потому что лень -->
                <v-btn @click="save" :disabled="step < 2" text>
                    Сохранить
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
import { mapState } from "vuex"
import AddContainerContract from "@/components/ao/AddContainerContract"
export default {
    components: {
        AddContainerContract,
    },
    data() {
        return {
            step: 1,
            kp: undefined,
            selectedContainers: [],
            isValid: false,
            showAlert: false,
            alert: "",
            containers: []
        }
    },
    props: {
        value: {
            type: Boolean,
            default: false
        }
    },
    mounted() {
        this.$store.dispatch("ao.squares/fetchSquares").then((res) => {})
    },
    methods: {
        getContainers(id) {
            let data = {
                isFilter: true,
                containerStandIds: [
                    id
                ],
                isFilterByCreatedUser: true,
                sorting: {}
            }
            this.$store.dispatch("ao.capacities/fetchContainers", data)
                .then((res) => {
                    this.containers = res
                }).catch(err => console.log(err))
        },
        goNext() {
            if (!this.selectedContainers.length) {
                this.showAlert = true
                this.alert = "Выберите хотя бы один контейнер"
            }
            else {
                this.step++
                this.showAlert = false
                this.alert = ""
            }
        },
        changeDefault(val) {
            this.default = val
        },
        onValidate(valid) {
            this.isValid = valid
        },
        save() {
            // if (!this.isValid) {
            //     this.alert = "Проверьте расписание"
            //     this.showAlert = true
            // }
            // else {
            let containersArray = []
            for (let item of this.selectedContainers) {
                containersArray.push({
                    ...this.default,
                    name: item.name,
                    containerId: item.containerId,
                    containerStandAddress: item.containerStandAddress
                })
            }
            this.$emit("save", containersArray)
            // }
        },
        clearData(){
            this.step = 1,
            this.kp = undefined,
            this.selectedContainers = [],
            this.isValid = false,
            this.showAlert = false,
            this.alert = "",
            this.containers = []
        }
    },
    computed: {
        show: {
            get() {
                return this.value
            },
            set(val) {
                this.$emit('input', val)
            }
        },
        ...mapState("ao.squares", ["squares"]),
    },
    watch: {
        kp: {
            deep: true,
            handler(val) {
                if(!!val) {
                    this.getContainers(val)
                    this.selectedContainers = []
                }
            }
        }
    }
}
</script>