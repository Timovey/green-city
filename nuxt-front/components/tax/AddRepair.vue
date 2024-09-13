<template>
    <v-card>
        <v-card-title>
            Добавить Ремонт/То
        </v-card-title>
        <v-card-text>
            <v-form v-model="valid" ref="formRepair">
                <div class="d-flex">
                    <v-select :rules="requiredRules" required v-model="data.transportId" item-value="id" item-text="name"
                        :items="transports" label="Транспорт" class="ml-2" filled rounded clearable>
                        <template v-slot:selection="{ item }">
                            {{ item.name + " " + item.regNumber }}
                        </template>
                        <template v-slot:item="{ item }">
                            {{ item.name + " " + item.regNumber }}
                        </template>    
                    </v-select>
                    <v-select :rules="requiredRules" required v-model="data.event" item-value="value" item-text="name"
                        :items="events" label="Тип работ" class="ml-2" filled rounded clearable/>
                </div>
                <div class="d-flex">
                    <v-select v-model="data.organizationId" item-value="id"
                        item-text="name" :items="organizations" label="Организация" class="ml-2" filled rounded clearable/>
                    <v-select :rules="requiredRules" required v-model="data.workShift" item-value="value"
                        item-text="name" :items="workShifts" label="Смена" class="ml-2" filled rounded clearable/>
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
                transportId: "",
                organizationId: "",
                workShift: "",
            },
            requiredRules: [
                v => String(v).length > 0 || 'Необходимо заполнить поле',
                v => !!v || 'Необходимо заполнить поле',
            ],
        }

    },
    props: {
        events: [],
        organizations: [],
        workShifts: [],
        transports: [],
        addingErrors: false
    },
    methods: {
        onSave() {
            this.$refs.formRepair.validate()
            if (!this.valid) {
                showErrorMessage("Невалидная форма")
                return
            }
            this.data.reset = this.$refs.formRepair.reset
            this.$emit("onSave", this.data)
        }
    }
}
</script>