<template>
    <v-card>
        <v-card-title>
            id: {{ container.id }} / {{ container.name }}
        </v-card-title>
        <v-card-subtitle class="text-left" v-if="enums.MissionStatus">
            {{
                enums.MissionStatus[container.missionStatus]
            }}
        </v-card-subtitle>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn v-if="edit" @click="$emit('remove')" color="error">Удалить</v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
export default {
    props: {
        container: {},
        edit: true,
        enums: []
    }
}
</script>