<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтры
        </v-card-title>
        <v-card-text class="pb-2">
            <v-text-field
                v-model="value.contractNumber"
                filled
                label="Номер контракта"
                outlined
                clearable
            />
            <v-text-field
                v-model="value.sendDocsType"
                filled
                label="Способ отправки документа"
                outlined
                clearable
            />
            <v-autocomplete v-model="value.contractClientIds" :items="contragents" clearable item-value="contractClientId"
                label="Контрагент" item-text="clientName" multiple rounded filled class="mt-4 mr-2">
            </v-autocomplete>
            <!-- <v-autocomplete :rules="requiredRules" required v-model="contract.contractClientId"
								:items="contragents" item-value="contractClientId" item-text="clientName" filled
								label="Контрагент" outlined clearable /> -->
            <v-select v-model="value.contractStatus" :items="boolElems" item-text="name" clearable item-value="value"
                label="Статус подписания" rounded filled class="mt-4 mr-2">
            </v-select>
        </v-card-text>
        <v-card-actions>
            <v-spacer/>
            <v-btn @click="onReset" color="warning">
                Сбросить фильтры
            </v-btn>
            <v-btn @click="onSave" color="primary">
                Сохранить
            </v-btn>
        </v-card-actions>
    </v-card>
</template>



<script>
    export default {
        props: {
            containers: [],
            contragents: []
        },
        data() {
            return {
                value: {},
                boolElems: [
                {
                    "name": "Подписан",
                    "value": true
                },
                {
                    "name": "Еще не подписан",
                    "value": false
                },
            ]
            }
        },
        watch:{
            value: {
                handler: function(newValue) {
                    this.$emit("input", newValue)
                },
                deep: true
            }
        },
        methods: {
            onSave(){
                this.$emit("onSave")
            },
            onReset() {
                this.value = {}
            }
        }
    }

    
</script>