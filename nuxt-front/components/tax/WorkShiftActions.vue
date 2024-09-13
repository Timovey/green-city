<template >
    <v-sheet tile class="d-flex">
        <v-btn icon class="ma-2" @click="$emit('onPrev')">
            <v-icon>mdi-chevron-left</v-icon>
        </v-btn>
        <v-spacer />
        <v-btn icon class="ma-2" @click="$emit('onNext')">
            <v-icon>mdi-chevron-right</v-icon>
        </v-btn>
    </v-sheet>
</template>

