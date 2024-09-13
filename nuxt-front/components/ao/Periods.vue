<template>
    <v-timeline>
        <v-timeline-item class="d-flex" :key="index" :class="{ 'text-right': index % 2, 'text-left': !(index % 2) }"
            v-for="(period, index) in periods">
            {{ $correctDate(period.scheduleDateFrom) }} -
            {{ $correctDate(period.scheduleDateTo) }}
            <v-btn @click="$emit('edit', index)" small icon>
                <v-icon>mdi-pencil</v-icon>
            </v-btn>
            <v-btn @click="$emit('remove', index)" small icon>
                <v-icon>mdi-delete</v-icon>
            </v-btn>
        </v-timeline-item>
    </v-timeline>
</template>

<script>
export default {
    data() {
        return {}
    },
    props: {
        periods: []
    }
}
</script>