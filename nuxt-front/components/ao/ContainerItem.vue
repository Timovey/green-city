<template>
    <v-card shaped>
        <v-card-title>
            {{ container.name }}
        </v-card-title>
        <v-card-text>
            <v-chip>
                {{container.workTimeFrom}}
            </v-chip>
            -
            <v-chip>
                {{container.workTimeTo}}
            </v-chip>
        </v-card-text>
        <v-card-actions>
            <v-spacer />
            <v-btn @click='$emit("edit")' color="warning" small fab outlined>
                <v-icon>
                    mdi-pencil
                </v-icon>
            </v-btn>
            <v-btn @click='$emit("remove")' color="error" small fab outlined>
                <v-icon>
                    mdi-delete
                </v-icon>
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
export default {
    props: {
        container: {},
        containers: []
    },
    mounted(){
    },
    computed: {
        containerInfo() {
            return this.containers.find(item => item.containerId == this.container.containerId)
        }
    }
}
</script>