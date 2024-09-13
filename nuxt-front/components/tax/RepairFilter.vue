<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтр
        </v-card-title>
        <v-divider class="mx-4"></v-divider>
        <v-card-text class="pb-2">
            <v-select multiple item-text="name" item-value="id" v-model="value.transportIds" label="Транспорт"
                :items="transports" rounded filled class="mt-4 mr-2" clearable>
                <template v-slot:selection="{ item }">
                    {{ item.name + " " + item.regNumber }}
                </template>
                <template v-slot:item="{ item }">
                    {{ item.name + " " + item.regNumber }}
                </template>
            </v-select>

            <v-select v-model="value.organizationIds" :items="organizations" item-text="name" clearable multiple
                item-value="id" label="Огранизации" rounded filled class="mt-4 mr-2">
            </v-select>

            <v-select v-model="value.events" :items="events" item-text="name" clearable multiple
                item-value="value" label="Событие" rounded filled class="mt-4 mr-2">
            </v-select>

            <v-select v-model="value.workShift" :items="workShifts" item-text="name" clearable multiple
                item-value="value" label="Смены" rounded filled class="mt-4 mr-2">
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
        transports: {},
        events: {},
        organizations: {},
        workShifts: {},
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