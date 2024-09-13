<template>
    <v-menu :style='{ "width": "100%" }' ref="menu" v-model="menu" :close-on-content-click="false"
        :return-value.sync="inputDate" transition="scale-transition" offset-y min-width="auto">
        <template v-slot:activator="{ on, attrs }">
            <v-text-field outlined filled rounded v-model="inputDate" :label="label" prepend-icon="mdi-calendar"
                :rules="rules" readonly v-bind="attrs" v-on="on" :disabled="disabled" :clearable=clearable>
            </v-text-field>
        </template>
        <v-date-picker v-model="inputDate" no-title scrollable :weekday-format="weekDay" :first-day-of-week="1"
            locale="ru-RU">
            <v-spacer></v-spacer>
            <v-btn text color="primary" @click="menu = false">
                Отмена
            </v-btn>
            <v-btn text color="primary" @click="$refs.menu.save(inputDate)">
                OK
            </v-btn>
        </v-date-picker>
    </v-menu>
</template>

<script>
export default {
    data() {
        return {
            menu: false,
        }
    },
    props: {
        date: null,
        label: "",
        rules: [],
        disabled: false,
        clearable: false
    },
    methods: {
        weekDay(date) {
            return new Date(date).toLocaleString('ru-RU', { weekday: 'short' });
        },
    },
    computed: {
        inputDate: {
            get() {
                return this.date
            },
            set(newValue) {
                this.$emit("update:date", newValue)
            }
        },
    },
}
</script>
