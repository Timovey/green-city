<template>
    <v-menu :style='{ "width": "100%" }' ref="menu" v-model="menu" :close-on-content-click="false"
        :return-value.sync="inputTime" transition="scale-transition" offset-y min-width="auto">
        <template v-slot:activator="{ on, attrs }">
            <v-form class="mr-2" :style='{ "width": "100%" }' v-model="isValid" ref="form">
                <v-text-field outlined filled rounded v-model="inputTime" :label="label" prepend-icon="mdi-clock-time-four-outline"
                    :rules="rules" readonly v-bind="attrs" v-on="on" :disabled="disabled" :clearable="clearable"/>
            </v-form>
        </template>
        <v-time-picker
          v-if="menu"
          v-model="inputTime"
          @click:minute="$refs.menu.save(inputTime)"
        ></v-time-picker>
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
        time: null,
        value: false,
        rules: [],
        disabled: false,
        clearable: false
    },
   
    computed: {
        isValid: {
            get(val) {
                return !!this.value
            },
            set(newValue) {
                this.$emit("input", newValue)
            }
        },
        inputTime: {
            get() {
                return this.time
            },
            set(newValue) {
                console.log(newValue)
                this.$emit("update:time", newValue)
            }
        }
    },
}
</script>