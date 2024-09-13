<template>
    <v-card>
        <v-card-title>
            Добавить аренду
        </v-card-title>
        <v-card-text>
            <v-form v-model="valid" ref="formDutyshift">
                <v-select :rules="requiredRules" required v-model="data.transportId" item-value="id" item-text="name"
                    :items="transports" label="Транспорт" class="ml-2" filled rounded clearable>
                    <template v-slot:selection="{ item }">
                        {{ item.name + " " + item.regNumber }}
                    </template>
                    <template v-slot:item="{ item }">
                        {{ item.name + " " + item.regNumber }}
                    </template>    
                </v-select>
                <PickDateV2 label="Дата" :date.sync="data.date" :rules="requiredRules"/>
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
import PickDateV2 from '../UI/PickDateV2';
import { showErrorMessage } from '@/helpers/messages';
export default {
    components: { PickDateV2 },
    data() {
        return {
            valid: false,
            dateValid: false,
            data: {
                transportId: "",
                date: "",
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
            this.$refs.formDutyshift.validate()
            if (!this.valid) {
                showErrorMessage("Невалидная форма")
                return
            }
            this.data.reset = this.$refs.formDutyshift.reset
            this.$emit("onSave", this.data)
        }
    }
}
</script>