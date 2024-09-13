<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтры
        </v-card-title>
        <v-card-text class="pb-5">
            <v-text-field
                v-model="value.clientName"
                filled
                label="Наименование"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.kpp"
                filled
                label="КПП"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.ogrn"
                filled
                label="ОГРН"
                outlined
                clearable
            ></v-text-field>
            <div class="d-flex">
                <v-text-field
                    v-model="value.rschet"
                    filled
                    label="Р/СЧ"
                    class="mr-2"
                    outlined
                    clearable
                ></v-text-field>
                <v-text-field
                    v-model="value.bank"
                    filled
                    class="ml-2"
                    label="Банк"
                    outlined
                    clearable
                ></v-text-field>
            </div>
            <v-text-field
                v-model="value.bik"
                filled
                label="БИК"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.kschet"
                filled
                label="К/СЧ"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.legalAddress"
                filled
                label="Юридический адрес"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.postalAddress"
                filled
                label="Почтовый адрес"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.responsibleTkoPersonPhone"
                filled
                label="Телефон"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.email"
                filled
                label="Электронная почта"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
        
            <v-text-field
                v-model="value.supervizorName"
                filled
                label="Руководитель"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.supervizorPosition"
                filled
                label="Должность руководителя"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
       
            <v-text-field
                v-model="value.responsibleTkoPersonName"
                filled
                label="ФИО"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
            <v-text-field
                v-model="value.clientContact"
                filled
                label="Контактный телефон"
                class="mr-2"
                outlined
                clearable
            ></v-text-field>
        </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
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