<template>
    <v-card>
        <v-card-title class="text--secondary">
            Фильтр
        </v-card-title>
        <v-divider class="mx-4"></v-divider>
        <v-card-text class="pb-2">
            <v-text-field v-model="value.tableNumber" filled label="Таб. №" class="mr-2" outlined clearable>
            </v-text-field>

            <v-text-field v-model="value.driverSurname" filled label="Фамилия водителя" class="mr-2" outlined clearable>
            </v-text-field>

            <v-text-field v-model="value.driverName" filled label="Имя водителя" class="mr-2" outlined
                clearable></v-text-field>

            <v-text-field v-model="value.birthDateFrom" type="date" label="Дата рождения с" class="mr-2" outlined
                clearable filled></v-text-field>

            <v-text-field v-model="value.birthDateTo" type="date" label="Дата рождения по" class="mr-2" outlined
                clearable filled></v-text-field>

            <v-text-field v-model="value.birthPlace" class="mr-2" label="Место рождения" outlined clearable filled>
            </v-text-field>

            <v-text-field v-model="value.regAddress" class="mr-2" label="Рег. Адрес" outlined clearable filled></v-text-field>

            <v-text-field v-model="value.factAddress" class="mr-2" label="Фактический адрес" outlined clearable filled></v-text-field>

            <v-text-field v-model="value.passportSerie" label="Серия паспорта" class="mr-2" outlined clearable filled>
            </v-text-field>
            <v-text-field v-model="value.passportNumber" class="mr-2" label="Номер паспорта" outlined clearable filled>
            </v-text-field>

            <v-text-field v-model="value.issueDateFrom" type="date" class="mr-2" label="Дата выдачи с" outlined
                clearable filled></v-text-field>

            <v-text-field v-model="value.issueDateTo" type="date" class="mr-2" label="Дата выдачи по" outlined clearable
                filled></v-text-field>

            <v-text-field v-model="value.issuer" class="mr-2" label="Кем выдан" outlined clearable filled>
            </v-text-field>

            <v-text-field v-model="value.acceptWorkDateFrom" type="date" class="mr-2" label="Дата принятия на работу с"
                outlined clearable filled></v-text-field>

            <v-text-field v-model="value.acceptWorkDateTo" type="date" class="mr-2" label="Дата принятия на работу по"
                outlined clearable filled></v-text-field>

            <v-text-field v-model="value.firedDateFrom" type="date" class="mr-2" label="Уволен с" outlined clearable
                filled></v-text-field>

            <v-text-field v-model="value.firedDateTo" type="date" class="mr-2" label="Уволен по" outlined clearable
                filled></v-text-field>

            <v-text-field v-model="value.skillClass" label="Квалиф. разряд" class="mr-2" filled outlined clearable>
            </v-text-field>

            <v-select v-model="value.isFired" :items="fired" item-text="name" clearable item-value="value"
                label="Флаг увольнения" rounded filled class="mt-4 mr-2">
            </v-select>

            <v-select v-model="value.organizationIds" :items="organizations" item-text="name" clearable multiple
                item-value="id" label="Огранизации" rounded filled class="mt-4 mr-2">
            </v-select>

            <v-select v-model="value.drivingSkills" :items="drivingSkills" item-text="name" clearable multiple
                item-value="value" label="Чем может управлять" rounded filled class="mt-4 mr-2">
            </v-select>

            <v-select v-model="value.driverPositionTypes" :items="driverPositions" item-text="name" clearable multiple
                item-value="value" label="Должность" rounded filled class="mt-4 mr-2">
            </v-select>

            <v-text-field v-model="value.contactNumber" class="mr-2" label="Телефон" outlined clearable filled>
            </v-text-field>

            <v-text-field v-model="value.driversLicense" class="mr-2" label="Водительское удостоверение" outlined
                clearable filled></v-text-field>
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
            value: {},
            fired: [
                {
                    "name": "Уволен",
                    "value": true
                },
                {
                    "name": "Работает",
                    "value": false
                },
            ]
        }
    },
    props: {
        organizations: [],
        drivingSkills: {},
        driverPositions: {}
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
                if (newValue.drivingSkills) {
                    if (newValue.drivingSkills.length == 0) {
                        newValue.drivingSkillsJson = null
                    }
                    else {
                        newValue.drivingSkillsJson = JSON.stringify(newValue.drivingSkills)

                    }
                }
                this.$emit("input", newValue)
            },
            deep: true
        }
    }
}
</script>