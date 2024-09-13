<template>
    <v-data-table :key="value" item-key="driverId" :headers="headers" :items="driversTable" :disable-sort="true"
        :disable-pagination="true" :hide-default-footer="true" @change="setDrivers">
        <!-- <template v-slot:header="{ item }">
                {{ getHolidaysColor(item) }}
            </template> -->
    </v-data-table>
</template>
<script>
import { formateDriverDate} from '@/helpers/workShiftCalendar'
import { deepCopy } from "@/helpers/deepCopyHelper";
export default {
    created() {
        this.setDrivers()
    },
    data() {
        return {
            headersInit: [
                { text: 'ФИО', value: 'driverFio' },
            ],
            headers: [],
            driversTable: []
        }
    },
    props: {
        value: {},
        workShift: {
            datesDrivers: null,
        },
        enums: {},
        drivers: [],
        listCount: 0,
    },
    watch: {
        value() {
            this.workShiftKey++
            this.setDrivers()
            this.workShiftKey++
        },
        workShift: {
            handler: function () {
                this.workShiftKey++
                this.setDrivers()
            },
            deep: true
        },
        listCount: function (newCount, oldCount) {
            this.setDrivers()
            this.workShiftKey++
        },
    },
    methods: {
        getLastDayOfMonth(year, month) {
            let date = new Date(year, month + 1, 0);
            return date
        },
        addDays(date, days) {
            var result = new Date(date);
            result.setDate(result.getDate() + days);
            return result;
        },
        setDrivers() {
            if (!this.workShift) return

            if (!this.workShift.datesDrivers) return

            let drivers = [...deepCopy(this.drivers)]
            let headers = [...this.headersInit]

            let cuurentMonth = new Date().getMonth() + this.listCount

            let transportDrivers = new Set()
            for (let [key, value] of Object.entries(this.workShift.datesDrivers)) {
                let dateKey = formateDriverDate(this, key)

                for (let [keyDriv, valueDriv] of Object.entries(value)) {
                    transportDrivers.add(+keyDriv)
                    let driver = drivers.find(el => el.driverId == keyDriv)
                    driver[dateKey] = this.enums.DriverWorkDate[valueDriv]
                }
                if (new Date(key).getMonth() == cuurentMonth) {
                    headers.push({ text: dateKey, value: dateKey })
                }
            }
            drivers = drivers.filter(el => transportDrivers.has(el.driverId))
       
            this.headers = headers
            this.driversTable = drivers

        }
    }
}
</script>
<style lang="scss">

</style>