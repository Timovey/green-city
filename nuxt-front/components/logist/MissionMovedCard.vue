<template>
    <v-card>
        <v-card-title>
            {{ mission.name }}
        </v-card-title>
        <v-card-actions>
            <v-btn
                icon
                color="red"
                @click="remove"
                >
                <v-icon>mdi-minus</v-icon>
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
        remove() {
            this.$emit("onRemove", this.mission)
        }
    }
}
</script>
<style>
    
</style>