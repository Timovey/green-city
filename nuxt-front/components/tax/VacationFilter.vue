<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтр
        </v-card-title>
        <v-divider class="mx-4"></v-divider>
        <v-card-text class="pb-2">
            <v-select v-model="value.driverIds" :items="drivers" label="Водители" clearable multiple rounded filled
                class="mt-4 mr-2" item-value="driverId">
                <template v-slot:selection="{ item }">
                    {{ item.driverFio }}
                </template>
                <template v-slot:item="{ item }">
                    {{ item.driverFio }}
                </template>
            </v-select>

            <v-select v-model="value.replacementDriverIds" :items="drivers" label="Сменные водители" clearable multiple rounded filled
                class="mt-4 mr-2" item-value="driverId">
                <template v-slot:selection="{ item }">
                    {{ item.driverFio }}
                </template>
                <template v-slot:item="{ item }">
                    {{ item.driverFio }}
                </template>
            </v-select>

            <v-select v-model="value.vacationTypes" :items="vacationTypes" item-text="name" clearable multiple
                item-value="value" label="Событие" rounded filled class="mt-4 mr-2">
            </v-select>

            <DateRangeFilter v-model="value"/>
        </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="onReset" color="warning">
                Сбросить фильтры
            </v-btn>
            <v-btn @click="onSave" color="primary">
                Сохранить
            </v-btn>
        </v-card-actions>
    </v-card>
</template>


<script>
import DateRangeFilter from '@/components/tax/DateRangeFilter';
export default {
    components: {
        DateRangeFilter
    },
    data() {
        return {
            value: {}
        }
    },
    props: {
        drivers: {},
        vacationTypes: {}
    },
    methods: {
        onSave() {
            this.$emit("onSave")
        },
        onReset() {
            this.value = {}
        }
    },
    watch: {
        value: {
            handler: function (newValue) {
                this.$emit("input", newValue)
            },
            deep: true
        }
    }
}
</script>