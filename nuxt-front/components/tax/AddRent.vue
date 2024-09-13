<template>
    <v-card>
        <v-card-title>
            Добавить аренду
        </v-card-title>
        <v-card-text>
            <v-form v-model="valid" ref="formRent">
                <v-select :rules="requiredRules" required v-model="data.transportId" item-value="id" item-text="name"
                    :items="transports" label="Транспорт" class="ml-2" filled rounded clearable>
                    <template v-slot:selection="{ item }">
                        {{ item.name + " " + item.regNumber }}
                    </template>
                    <template v-slot:item="{ item }">
                        {{ item.name + " " + item.regNumber }}
                    </template>    
                </v-select>
                <v-select v-model="data.organizationId" item-value="id" :rules="requiredRules" 
                    item-text="name" :items="organizations" label="Арендатор" class="ml-2" filled rounded clearable/>
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
            },
            requiredRules: [
                v => String(v).length > 0 || 'Необходимо заполнить поле',
                v => !!v || 'Необходимо заполнить поле',
            ],
        }

    },
    props: {
        organizations: [],
        transports: [],
        addingErrors: false
    },
    methods: {
        onSave() {
            this.$refs.formRent.validate()
            if (!this.valid) {
                showErrorMessage("Невалидная форма")
                return
            }
            this.data.reset = this.$refs.formRent.reset
            this.$emit("onSave", this.data)
        }
    }
}
</script>