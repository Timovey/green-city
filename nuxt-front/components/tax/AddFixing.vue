<template>
    <v-card>
        <v-card-title>
            Добавить привязку
        </v-card-title>
        <v-card-text>
            <v-form v-model="valid" ref="formFixing">
                <div class="d-flex">
                    <v-select :rules="requiredRules" required v-model="data.driverId" item-value="driverId"
                        item-text="driverName" :items="drivers" label="Водитель" class="mr-2" filled rounded clearable>
                        <template v-slot:selection="{ item }">
                            {{ item.driverFio }}
                        </template>
                        <template v-slot:item="{ item }">
                            {{ item.driverFio }}
                        </template>
                    </v-select>
                    <v-select :rules="requiredRules" required v-model="data.transportId" item-value="id"
                        item-text="name" :items="transports" label="Транспорт" class="ml-2" filled rounded clearable>
                        <template v-slot:selection="{ item }">
                            {{ item.name + " " + item.regNumber }}
                        </template>
                        <template v-slot:item="{ item }">
                            {{ item.name + " " + item.regNumber }}
                        </template>
                    </v-select>
                    <v-select :rules="requiredRules" required v-model="data.workShift" item-value="value"
                        item-text="name" :items="workShifts" label="Выход на работу с" class="ml-2" filled rounded clearable/>
                </div>
                <DateRange :dateRange.sync="data.dateRange" />
            </v-form>
        </v-card-text>
        <v-card-actions>
            <v-spacer />
            <v-btn @click="onSave()" color="primary">
                Сохранить
            </v-btn>
        </v-card-actions>
    </v-card>
</template>


<script>
import DateRange from '../UI/DateRange';
import { showErrorMessage } from '@/helpers/messages';
export default {
    components: { DateRange },
    data() {
        return {
            valid: false,
            data: {
                dateRange: {
                    createDate: null,
                    startDate: null,
                    endDate: null,
                    factEndDate: null
                },
                driverId: null,
                transportId: "",
            },
            requiredRules: [
                v => String(v).length > 0 || 'Необходимо заполнить поле',
                v => !!v || 'Необходимо заполнить поле',
            ],
        }

    },
    props: {
        drivers: [],
        transports: [],
        workShifts: [],
        addingErrors: false
    },
    methods: {
        onSave() {
            this.$refs.formFixing.validate()
            if (!this.valid) {
                showErrorMessage("Невалидная форма")
                return
            }
            this.data.reset = this.$refs.formFixing.reset
            this.$emit("onSave", this.data)
        }
    }
}
</script>