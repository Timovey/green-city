<template>
    <div>
        <v-text-field v-model="value.createDateFrom" type="date" class="mr-2" label="Дата создания с" outlined clearable
            filled>
        </v-text-field>

        <v-text-field v-model="value.createDateTo" type="date" class="mr-2" label="Дата создания по" outlined clearable
            filled>
        </v-text-field>

        <v-text-field v-model="value.startDateFrom" type="date" class="mr-2" label="Дата начала с" outlined clearable
            filled>
        </v-text-field>

        <v-text-field v-model="value.startDateTo" type="date" class="mr-2" label="Дата начала по" outlined clearable
            filled>
        </v-text-field>

        <v-text-field v-model="value.endDateFrom" type="date" class="mr-2" label="Дата окончания с" outlined clearable
            filled>
        </v-text-field>

        <v-text-field v-model="value.endDateTo" type="date" class="mr-2" label="Дата окончания по" outlined clearable
            filled>
        </v-text-field>
    </div>
</template>
<script>
export default {
    props: {
        value: false,
    },
}
</script>