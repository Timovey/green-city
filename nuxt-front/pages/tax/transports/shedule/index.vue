<template>
    <div>
        <GlobalMenu @onShowFilters="showFilters = true" title="Расписание транспорта" @onPlus="onPlus()"
            v-model="drawer" :menu="menu" />
        <v-dialog width="70%" v-model="addModal">
            <AddShedule :sheduleTypes="getSheduleTypes" @onSave="onAdd" :addingErrors="serverErrors"
                :transports="transports" />
        </v-dialog>
        <v-dialog scrollable width="80%" v-model="showFilters">
            <SheduleFilter :transports="transports" :sheduleTypes="getSheduleTypes" @onSave="applyFilters()"
                v-model="filters" />
        </v-dialog>
        <DeleteItems v-if="selectedItem" v-model="showDeleteModal" @onDelete="onDelete()"
            :message="`Расписание ${selectedItem.workSheduleType} у машины ${selectedItem.transportName} ${selectedItem.regNumber}, график ${(selectedItem.dateRange.startDate)} - ${selectedItem.dateRange.endDate}`" />
        <PageLayout>
            <v-data-table show-expand :expanded.sync="expanded" multi-sort item-key="transportId" :headers="headers"
                :items="reformedShedules" :items-per-page="10">
                <template v-slot:expanded-item="{ headers, item }">
                    <td :colspan="headers.length">
                        <div class="text-h6 align-center text--secondary d-flex pa-2" v-for="shedule in item.shedules">
                            <div class="mr-4">
                                График:
                            </div>
                            <div class="mr-4">
                                {{ shedule.workSheduleType }}
                            </div>
                            <v-spacer />
                            <div class="mr-4">
                                Дата создания:
                            </div>
                            <v-chip class="mr-4">
                                {{ shedule.dateRange.createDate }}
                            </v-chip>
                            <div class="mr-4">
                                Диапазон
                            </div>
                            <v-chip>
                                {{ shedule.dateRange.startDate }}
                            </v-chip>
                            <div class="pa-2">
                                —
                            </div>
                            <v-chip>
                                {{ shedule.dateRange.endDate }}
                            </v-chip>
                            <v-btn @click="onDeleteModal(shedule)" color="error" class="ml-2" fab x-small>
                                <v-icon>
                                    mdi-delete
                                </v-icon>
                            </v-btn>
                        </div>
                    </td>
                </template>
            </v-data-table>
        </PageLayout>
    </div>
</template>
<script>
import GlobalMenu from "@/components/UI/GlobalMenu"
import AddShedule from "@/components/tax/AddShedule"
import SheduleFilter from "@/components/tax/SheduleFilter"
import { taxerMenu } from "@/helpers/menus"
import DeleteItems from "@/components/UI/DeleteItems"
import PageLayout from "@/components/UI/PageLayout"
import TableActions from "@/components/UI/TableActions"
import { mapState } from "vuex"
import { showSuccess, showErrorMessage } from "@/helpers/messages"
import { toArray } from "@/helpers/objectHelper"
import { deepCopy } from "@/helpers/deepCopyHelper";

export default {
    head: {
		title: 'Расписание ТС',
	},
    components: {
        GlobalMenu,
        DeleteItems,
        PageLayout,
        SheduleFilter,
        TableActions,
        AddShedule
    },
    created() {
        this.menu = taxerMenu
        this.$store.dispatch('tax.transports.shedule/fetchShedules').catch(err => showErrorMessage(err))
        this.$store.dispatch('organizations/fetchOrganizations').catch(err => showErrorMessage(err))
        this.$store.dispatch('tax.transports/fetchTransports').catch(err => showErrorMessage(err))
        this.$store.dispatch('tax.enums/fetchEnums').catch(err => showErrorMessage(err))
    },
    data() {
        return {
            expanded: [],
            serverErrors: false,
            addModal: false,
            menu: [],
            showFilters: false,
            selectedItem: false,
            pageCount: 0,
            filters: {},
            drawer: false,
            showDeleteModal: false,
            itemsPerPage: 5,
            headers: [
                {
                    text: '№',
                    align: 'start',
                    value: 'transportId',
                },
                { text: 'Модель транспорта', value: 'transportName' },
                { text: 'Тип транспорта', value: 'transportType' },
                { text: 'Номер', value: 'regNumber' },
                { text: 'Организация', value: 'transportOrganization' },
            ],
        }
    },
    methods: {
        onPlus() {
            this.addModal = true
        },
        onItemSelect(item) {
            if (item.value)
                this.selectedItem = item.item
            else this.selectedItem = false
        },
        onDelete() {
            console.log(this.selectedItem);
            this.$store.dispatch("tax.transports.shedule/deleteShedule", { transportSheduleIds: [this.selectedItem.transportSheduleId] })
                .then(() => {
                    console.log('suc');
                    showSuccess()
                }).catch((err) => {
                    console.log('err', err)
                    showErrorMessage(err)
                })
            this.showDeleteModal = false
        },
        applyFilters() {
            let data = {
                isFilter: true,
                sorting: {},
                ...this.filters
            }
            this.$store.dispatch('tax.transports.shedule/fetchShedules', data)
                .then(() => this.showFilters = false).catch(err => showErrorMessage(err))
        },
        onAdd(data) {
            this.$store.dispatch("tax.transports.shedule/addShedule", data)
                .then(res => {
                    showSuccess()
                    this.serverErrors = false
                    this.addModal = false
                    data.reset()
                })
                .catch(err => {
                    this.serverErrors = err
                    showErrorMessage(err)
                })
        },
        onDeleteModal(item) {
            this.selectedItem = item
            this.showDeleteModal = true
        },
        getSheduleData(item) {
            let shedule = { ...deepCopy(item) }

            if (this.enums.WorkSchedule && this.enums.WorkSchedule[item.workSheduleType]) {
                shedule.workSheduleType = this.enums.WorkSchedule[item.workSheduleType]
            }
            return shedule
        },
        fillTransportData(data, item) {
            data[item.transportId].transportId = item.transportId
            data[item.transportId].transportName = item.transportName

            if (this.enums.Transport && this.enums.Transport[item.transportType]) {
                data[item.transportId].transportType = this.enums.Transport[item.transportType]
            }
            data[item.transportId].regNumber = item.regNumber

            let org = this.organizations.find(el => el.id == item.transportOrganizationId)

            data[item.transportId].transportOrganization = org == null ? '-' : org.name

        }
    },
    computed: {
        ...mapState("tax.transports", ["transports"]),
        ...mapState("organizations", ["organizations"]),
        ...mapState("tax.enums", ["enums"]),
        ...mapState("tax.transports.shedule", ["shedules"]),
        reformedShedules() {
            let result = deepCopy(this.shedules)
            let data = {}
            for (let item of result) {
                if (!data[item.transportId]) {
                    data[item.transportId] = {}
                    this.fillTransportData(data, item)

                    data[item.transportId].shedules = [{
                        ...this.getSheduleData(item)
                    }]

                } else {
                    data[item.transportId].shedules.push({
                        ...this.getSheduleData(item)
                    })
                }
            }
            let resultData = []
            for (let item of Object.keys(data)) {
                resultData.push(data[item])
            }
            return resultData
        },
        getSheduleTypes() {
            return toArray(this.enums.WorkSchedule);
        }
    }
}
</script>