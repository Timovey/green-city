<template>
    <div>
        <GlobalMenu @onShowFilters="showFilters = true" title="Ремонт/ТО транспорта" @onPlus="onPlus()" v-model="drawer"
            :menu="menu" />
        <v-dialog width="70%" v-model="addModal">
            <AddRepair :workShifts="getWorkShifts" @onSave="onAdd" :addingErrors="serverErrors"
                :events="getEvents" :organizations="organizations" :transports="transports" />
        </v-dialog>
        <v-dialog scrollable width="80%" v-model="showFilters">
            <RepairFilter :events="getEvents" :organizations="organizations"
                :workShifts="getWorkShifts" :transports="transports" @onSave="applyFilters()"
                v-model="filters" />
        </v-dialog>
        <DeleteItems v-if="selectedItem" v-model="showDeleteModal" @onDelete="onDelete()"
            :message="`${selectedItem.event} у машины ${selectedItem.transportName} ${selectedItem.regNumber}, диапазон ${selectedItem.dateRange.startDate} - ${selectedItem.dateRange.endDate}`" />
        <PageLayout>
            <v-data-table show-expand :expanded.sync="expanded" multi-sort item-key="transportId" :headers="headers"
                :items="reformedRepairs" :items-per-page="10">
                <template v-slot:expanded-item="{ headers, item }">
                    <td :colspan="headers.length">
                        <div class="text-h6 align-center text--secondary d-flex pa-2" v-for="repair in item.repairs">
                            <div class="mr-4">
                                Событие:
                            </div>
                            <div class="mr-4">
                                {{ repair.event }}
                            </div>
                            <div class="mr-4">
                                Длительность:
                            </div>
                            <div class="mr-4">
                                {{ repair.workShift }}
                            </div>
                            <v-spacer />
                            <div class="mr-4">
                                Дата создания:
                            </div>
                            <v-chip class="mr-4">
                                {{ repair.dateRange.createDate }}
                            </v-chip>
                            <div class="mr-4">
                                Диапазон
                            </div>
                            <v-chip>
                                {{ repair.dateRange.startDate }}
                            </v-chip>
                            <div class="pa-2">
                                —
                            </div>
                            <v-chip>
                                {{ repair.dateRange.endDate }}
                            </v-chip>
                            <v-btn @click="onDeleteModal(repair)" color="error" class="ml-2" fab x-small>
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
import AddRepair from "@/components/tax/AddRepair"
import RepairFilter from "@/components/tax/RepairFilter"
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
		title: 'Ремонт/ТО',
	},
    components: {
        GlobalMenu,
        DeleteItems,
        PageLayout,
        RepairFilter,
        TableActions,
        AddRepair
    },
    mounted() {
        this.menu = taxerMenu
        this.$store.dispatch('tax.transports.repair/fetchRepairs').catch(err => showErrorMessage(err))
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
            this.$store.dispatch("tax.transports.repair/deleteRepair", { repairCardIds: [this.selectedItem.repairCardId] })
                .then(() => {
                    showSuccess()
                }).catch((err) => {
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
            this.$store.dispatch('tax.transports.repair/fetchRepairs', data)
                .then(() => this.showFilters = false).catch(err => showErrorMessage(err))
        },
        onAdd(data) {
            this.$store.dispatch("tax.transports.repair/addRepair", data)
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
        getRepairData(item) {
            let repair = { ...deepCopy(item) }

            if (this.enums.WorkShift && this.enums.WorkShift[item.workShift]) {
                repair.workShift = this.enums.WorkShift[item.workShift]
            }
            if (this.enums.Repair && this.enums.Repair[item.event]) {
                repair.event = this.enums.Repair[item.event]
            }
            return repair
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
        ...mapState("tax.transports.repair", ["repairs"]),
        ...mapState("organizations", ["organizations"]),
        ...mapState("tax.enums", ["enums"]),
        reformedRepairs: {
            cache: false,
            get() {
                if (!this.repairs) return
                let result = deepCopy(this.repairs)
                let data = {}
                for (let item of result) {
                    if (!data[item.transportId]) {
                        data[item.transportId] = {}
                        this.fillTransportData(data, item)

                        data[item.transportId].repairs = [{
                            ...this.getRepairData(item)
                        }]

                    } else {
                        data[item.transportId].repairs.push({
                            ...this.getRepairData(item)
                        })
                    }
                }
                let resultData = []
                for (let item of Object.keys(data)) {
                    resultData.push(data[item])
                }
                return resultData
            }
        },
        getWorkShifts() {
            return toArray(this.enums.WorkShift);
        },
        getEvents() {
            return toArray(this.enums.Repair);
        }
    }
}
</script>