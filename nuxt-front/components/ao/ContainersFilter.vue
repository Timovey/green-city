<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтры
        </v-card-title>
        <v-card-text class="pb-2">
            <v-text-field
                v-model="value.name"
                filled
                label="Наименование"
                outlined
                clearable
            />
            <v-select
                v-model="value.trashTypes"
                :items="garbageTypes"
                item-value="value"
                item-text="name"
                label="Тип мусора"
                multiple
                required
                outlined
                filled
            />
            </v-card-text>
            <v-card-title class="pt-0 text--secondary">
                Тара
            </v-card-title>
            <v-card-text>
                <v-select
                    v-model="value.containerTypeIds"
                    :items="containerTypes"
                    label="Вид тары"
                    required
                    outlined
                    multiple
                    filled
                    item-value="id"
                    item-text="name"
                />
                <v-select
                    v-model="value.containerStandIds"
                    label="Контейнерная площадка"
                    required
                    multiple
                    outlined
                    filled
                />
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
            containerTypes: [],
            garbageTypes: []
        },
        data() {
            return {
                value: {}
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