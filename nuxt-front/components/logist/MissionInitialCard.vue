<template>
    <v-card>
        <v-card-title>
            {{ mission.name }}
        </v-card-title>
        <v-card-actions>
            <v-btn
                icon
                color="primary"
                @click="move"
                >
                <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>
export default {
    props: {
        mission: {
            type: Object,
            default: {}
        }
    },
    methods: {
        move() {
            this.$emit("onMove", this.mission)
        }
    }
}
</script>
<style>
    
</style>