<template>
    <v-app-bar>
        <v-app-bar-nav-icon @click="$emit('input', !value)"></v-app-bar-nav-icon>
        <div class="mr-4 text--secondary text-h5 px-2">Транспортные средства</div>
        <v-btn @click="$emit('onPlus')" color="primary" small fab>
            <v-icon>
                mdi-plus
            </v-icon>
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn color=primary @click="$emit('onShowFilters')" class="mr-2">Фильтр поиска</v-btn>
    </v-app-bar>
</template>


<script>

    export default{
        props: {
            title: String,
            value: false
        }
    }
</script>