<template>
    <v-card>
        <v-card-title>Фильтры</v-card-title>
        <v-card-text>
            <v-select multiple item-text="driverName" item-value="driverId" v-model="value.driverIds" label="Водитель"
                :items="drivers" rounded filled clearable>
                <template v-slot:selection="{ item }">
                    {{ item.driverFio }}
                </template>
                <template v-slot:item="{ item }">
                    {{ item.driverFio }}
                </template>
            </v-select>
            <v-select multiple item-text="name" item-value="id" v-model="value.transportIds" label="Транспорт"
                :items="transports" rounded filled clearable>
                <template v-slot:selection="{ item }">
                    {{ item.name + " " + item.regNumber }}
                </template>
                <template v-slot:item="{ item }">
                    {{ item.name + " " + item.regNumber }}
                </template>
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
    components: { DateRangeFilter },
    data() {
        return {
            value: {}
        }
    },
    props: {
        drivers: [],
        transports: []
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