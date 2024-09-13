<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтры
        </v-card-title>
        <v-card-text class="pb-2">
            <v-text-field
                v-model="value.address"
                filled
                label="Адрес"
                outlined
                clearable
            />
            <div class="d-flex flex-column justify-space-between">
                <v-select v-model="value.isHighSecurityObject" :items="boolElems" item-text="name" clearable item-value="value"
                    label="Режимный объект" rounded filled class="mt-4 mr-2">
                </v-select>
                <v-select v-model="value.haveRoof" :items="boolElems" item-text="name" clearable item-value="value"
                    label="Наличие крыши" rounded filled class="mt-4 mr-2">
                </v-select>
                <v-select v-model="value.haveBase" :items="boolElems" item-text="name" clearable item-value="value"
                    label="Наличие основания" rounded filled class="mt-4 mr-2">
                </v-select>
                <v-select v-model="value.haveFence" :items="boolElems" item-text="name" clearable item-value="value"
                    label="Наличие ограждения" rounded filled class="mt-4 mr-2">
                </v-select>
            </div>
            <v-textarea v-model="value.driveWay" filled label="Путь проезда" outlined clearable></v-textarea>
            <v-text-field v-model="value.contacts" filled label="Контакты" outlined clearable></v-text-field>
            <v-select v-model="value.standTypes" :items="squaresTypes" item-value="value" item-text="name"
                label="Тип площадки" multiple required outlined filled></v-select>
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
            boolElems: [
                {
                    "name": "Да",
                    "value": true
                },
                {
                    "name": "Нет",
                    "value": false
                },
            ]
        }
    },
    props: {
        value: {},
        squaresTypes: [],
    },
    watch: {
        value: {
            handler: function (newValue) {
                this.$emit("input", newValue)
            },
            deep: true
        }
    },
    methods: {
        onSave() {
            this.$emit("onSave")
        },
        onReset() {
            this.value = {}
        }
    }
}


</script>