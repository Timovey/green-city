<template>
    <v-sheet>
        <v-calendar :key="calendarKey" ref="calendar" v-model="selectDay" :weekdays="[1, 2, 3, 4, 5, 6, 0]" type="month"
            :events="transportEvents" event-overlap-mode="stack" :event-overlap-threshold="30"
            :event-color="getEventColor" @change="getTransportEvents" locale="ru-Ru" color="primary"></v-calendar>
    </v-sheet>
</template>
<script>
import {  transportEventsColor, errorEventsColor, formateDate } from '@/helpers/workShiftCalendar'
export default {
    data() {
        return {
            calendarKey: 1,
            transportEvents: [],
            selectDay: '',
        }
    },
    props: {
        workShift: {
            // datesTransport: {},
            // responseErrors: {},
            // datesDrivers: {},
        },
        listCount: 0,
        enums: {}
    },
    methods: {
        prev() {
            this.$refs.calendar.prev()
        },
        next() {
            this.$refs.calendar.next()
        },
        getTransportEvents({ start, end }) {
            if (!this.workShift || !this.enums) return

            const events = []
            //сначала проходимся по расписанию транспорта
            let dates = []
            for (let [key, value] of Object.entries(this.workShift.datesTransport)) {
                key = formateDate(this,key)
                dates.push({ key, value })
            }
            if (dates.length == 0) return

            let prev = dates[0]
            for (let i = 1; i < dates.length; i++) {
                if (dates[i].value == prev.value) {

                }
                else {
                    // console.log(`${prev.key} - ${dates[i].key}`);
                    events.push({
                        // name: getTransportShedule(dates[i - 1].value),
                        name: this.enums.TransportWorkDate[dates[i - 1].value],
                        start: prev.key,
                        end: dates[i - 1].key,
                        color: transportEventsColor[dates[i - 1].value],
                        timed: false,
                    })
                    prev = dates[i]
                }

            }
            events.push({
                // name: getTransportShedule(prev.value),
                name: this.enums.TransportWorkDate[prev.value],
                start: prev.key,
                end: dates[dates.length - 1].key,
                color: transportEventsColor[prev.value],
                timed: false,
            })

            // затем по ошибкам
            let errors = []

            for (let [key, value] of Object.entries(this.workShift.responseErrors)) {
                key = formateDate(this,key)
                errors.push({ key, value })
            }
            if (errors.length != 0) {
                for (let i = 0; i < errors.length; i++) {

                    let nameErr =  this.enums.DateWorkedDriver[errors[i].value]
                    if (nameErr) {
                        events.push({
                            name: nameErr,
                            start: errors[i].key,
                            end: errors[i].key,
                            color: errorEventsColor[errors[i].value],
                            timed: false,
                        })
                    }
                }
            }


            this.transportEvents = events
        },
        getEventColor(event) {
            return event.color
        },
    },
    watch: {
        workShift: {
            handler: function () {
                this.calendarKey++
            },
            deep: true
        },
        listCount: function (newCount, oldCount) {
            newCount > oldCount ? this.next() : this.prev()
        }
    },
}
</script>
<style lang="">
    
</style>