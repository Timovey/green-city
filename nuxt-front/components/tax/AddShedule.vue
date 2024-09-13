<template>
    <v-card>
        <v-card-title>
            Добавить расписание
        </v-card-title>
        <v-card-text>
            <v-form v-model="valid" ref="formShedule">
                <div class="d-flex">
                    <v-select :rules="requiredRules" required v-model="data.transportId" item-value="id"
                        item-text="name" :items="transports" label="Транспорт" class="ml-2" filled rounded clearable>
                        <template v-slot:selection="{ item }">
                            {{ item.name + " " + item.regNumber }}
                        </template>
                        <template v-slot:item="{ item }">
                            {{ item.name + " " + item.regNumber }}
                        </template>
                    </v-select>
                    <v-select :rules="requiredRules" required v-model="data.workSheduleType" item-value="value"
                        item-text="name" :items="sheduleTypes" label="Тип расписания" class="ml-2" filled rounded clearable/>
                </div>
                <DateRange :dateRange.sync="data.dateRange"/>
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
            dateValid: false,
            data: {
                event: "",
                dateRange: {
                    createDate: null,
                    startDate: null,
                    endDate: null,
                    factEndDate: null
                },
                workSheduleType: "",
                transportId: "",
            },
            requiredRules: [
                v => String(v).length > 0 || 'Необходимо заполнить поле',
                v => !!v || 'Необходимо заполнить поле',
            ],
        }

    },
    props: {
        sheduleTypes: [],
        transports: [],
        addingErrors: false
    },
    methods: {
        onSave() {
            this.$refs.formShedule.validate()
            if (!this.valid) {
                showErrorMessage("Невалидная форма")
                return
            }
            //отправляем в данных ссылку на сброс формы
            this.data.reset = this.$refs.formShedule.reset
            this.$emit("onSave", this.data)
        }
    }
}
</script>