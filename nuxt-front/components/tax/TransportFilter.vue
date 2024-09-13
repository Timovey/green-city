<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтр
        </v-card-title>
        <v-card-text>
            <v-text-field filled v-model="value.name" label="Название" outlined clearable />
            <v-text-field filled v-model="value.regNumber" label="Регистрационный номер" outlined clearable />
            <v-select v-model="value.rent" :items="rent" item-text="name" clearable item-value="value"
                label="Аренда" rounded filled class="mt-4 mr-2">
            </v-select>
            <v-select v-model="value.organizationIds" :items="organizations" item-text="name" clearable multiple
                item-value="id" label="Огранизации" rounded filled class="mt-4 mr-2">
            </v-select>
            <v-select v-model="value.transportTypes" :items="transportTypes" item-text="name" clearable multiple
                item-value="value" label="Тип транспорта" rounded filled class="mt-4 mr-2">
            </v-select>
            <!-- <v-text-field filled v-model="value.sts" label="СТС" outlined clearable /> -->
            <!-- <v-text-field filled v-model="value.bodyType" label="Тип кузова" outlined clearable /> -->
            <v-text-field filled v-model="value.tankFence" label="Забор бака" outlined clearable />
            <v-text-field filled v-model="value.bodyColor" label="Цвет кузова" outlined clearable />
            <v-text-field filled v-model="value.cargoVolume" label="Объем кузова" type="number" hide-spin-buttons
                outlined clearable />
            <!-- <v-text-field filled v-model="value.cargoVolumeCoef" label="Объем кузова с коэф." outlined clearable />
            <v-text-field filled v-model="value.mileage" label="Пробег" type="number" hide-spin-buttons outlined
                clearable />
            <v-text-field filled v-model="value.productionYear" label="Год выпуска" type="number" hide-spin-buttons
                outlined clearable />
            <v-text-field filled v-model="value.se" label="Предполагаемый СЭ" outlined clearable />

            <v-text-field filled v-model="value.spendingLimit" label="Лимит затрат" outlined clearable />
            <v-text-field filled v-model="value.gsmNumber" label="Номер расхода ГСМ" outlined clearable />
            <v-text-field filled v-model="value.lastTO" label="Последнее ТО" type="date" outlined clearable />
            <v-text-field filled v-model="value.repairsTO" label="ТО и ремонты" outlined clearable /> -->
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
export default {
    data() {
        return {
            value: {},
            rent: [
                {
                    "name": "В аренде",
                    "value": true
                },
                {
                    "name": "Не в аренде",
                    "value": false
                },
            ]
        }
    },
    props: {
        organizations: [],
        tankTypes: [],
        transportTypes: [],
    },
    methods: {
        onSave() {
            this.$emit("onSave")
        },
        onReset() {
            this.value = {}
        }
    },
    watch: {
        value: {
            handler: function (newValue) {
                // console.log(newValue)
                this.$emit("input", newValue)
            },
            deep: true
        }
    },
}
</script>