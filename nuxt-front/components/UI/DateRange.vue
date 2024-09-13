<template>
    <div class="d-flex">
        <PickDateV2 label="Дата начала" :date.sync="dateRange.startDate" :rules="dateRules"/>
        <PickDateV2 label="Дата конца" :date.sync="dateRange.endDate"/>
    </div>
</template>

<script>
import PickDateV2 from '@/components/UI/PickDateV2';
export default {
    components: {
        PickDateV2
    },
    data() {
        return {
            dateRules: [
                v => !!v || 'Необходимо заполнить поле',
                v => String(v).length > 0 || 'Необходимо заполнить поле',
            ],
        }
    },
    props: {
        dateRange: {}
    },
    computed: {
        dates: {
            get() {
                return this.dataRange
            },
            set() {
                this.$emit("update:dateRange")
            }
        }
    }
}
</script>