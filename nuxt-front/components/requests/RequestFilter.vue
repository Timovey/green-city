<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтр
        </v-card-title>
        <v-card-text>
            <v-checkbox label="Фильтровать по создателю" v-model="value.isFilterByCreatedUser" outlined filled />
            <v-text-field type="date" filled v-model="value.appDate" label="Дата" outlined clearable />
            <v-autocomplete :items="contragents" label="Контрагенты" item-value="contractClientId" outlined filled
                item-text="clientName" multiple v-model="value.contractClientIds"></v-autocomplete>
            <v-autocomplete :items="contracts" label="Контракты" item-value="contractId" outlined filled
                item-text="contractNumber" multiple v-model="value.contractIds"></v-autocomplete>
        </v-card-text>

        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="onReset" color="warning">
                Сбросить фильтры
            </v-btn>
            <v-btn @click.stop='$emit("onSave")' color="primary">
                <div>Применить</div>
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
import { mapState } from "vuex"
export default {
    created() {
        this.$store.dispatch('ao.contragents/fetchContragents')
        this.$store.dispatch('ao.contracts/fetchContracts')
    },
    methods: {
        onSave() {
            this.$emit("onSave")
        },
        onReset() {
            this.value = {}
        }
    },
    props: {
        value: {}
    },
    watch: {
        value: {
            handler: function (newValue) {
                this.$emit("input", newValue)
            },
            deep: true
        }
    },
    computed: {
        ...mapState("ao.contragents", ['contragents']),
        ...mapState("ao.contracts", ['contracts'])
    }
}
</script>