<template>
    <div>
        <GlobalMenu @onShowFilters="showFilters = true" title="Дежурные смены" @onPlus="onPlus()" v-model="drawer"
            :menu="menu" />
        <v-dialog width="70%" v-model="addModal">
            <AddDutyshift @onSave="onAdd" :addingErrors="serverErrors"
                :organizations="organizations" :transports="transports" />
        </v-dialog>
        <v-dialog scrollable width="80%" v-model="showFilters">
            <DutyshiftFilter  :organizations="organizations"
                :transports="transports" @onSave="applyFilters()"
                v-model="filters" />
        </v-dialog>
        <DeleteItems v-if="selectedItem" v-model="showDeleteModal" @onDelete="onDelete()"
            :message="`Дежурная смена машины ${selectedItem.transportName} ${selectedItem.regNumber}, дата ${selectedItem.date}`" />
        <PageLayout>
            <v-data-table show-expand :expanded.sync="expanded" multi-sort item-key="transportId" :headers="headers"
                :items="reformedDutyshifts" :items-per-page="10">
                <template v-slot:expanded-item="{ headers, item }">
                    <td :colspan="headers.length">
                        <div class="text-h6 align-center text--secondary d-flex pa-2" v-for="dutyshift in item.dutyshifts">
                            <div class="mr-4">
                                Дата:
                            </div>
                            <v-chip class="mr-4">
                                {{ dutyshift.date}}
                            </v-chip>
                            <v-btn @click="onDeleteModal(dutyshift)" color="error" class="ml-2" fab x-small>
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
import AddDutyshift from "@/components/tax/AddDutyshift"
import DutyshiftFilter from "@/components/tax/DutyshiftFilter"
import { taxerMenu } from "@/helpers/menus"
import DeleteItems from "@/components/UI/DeleteItems"
import PageLayout from "@/components/UI/PageLayout"
import TableActions from "@/components/UI/TableActions"
import { mapState } from "vuex"
import { showSuccess, showErrorMessage } from "@/helpers/messages"
import { deepCopy } from "@/helpers/deepCopyHelper";

export default {
    head: {
		title: 'Дежурные смены',
	},
    components: {
        GlobalMenu,
        DeleteItems,
        PageLayout,
        DutyshiftFilter,
        TableActions,
        AddDutyshift
    },
    mounted() {
        this.menu = taxerMenu
        this.$store.dispatch('tax.transports.dutyshift/fetchDutyshifts').catch(err => showErrorMessage(err))
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
                { text: 'Организация транспорта', value: 'transportOrganization' },
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
            this.$store.dispatch("tax.transports.dutyshift/deleteDutyshift", { transportDutyShiftIds: [this.selectedItem.transportDutyShiftId] })
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
            this.$store.dispatch('tax.transports.dutyshift/fetchDutyshifts', data)
                .then(() => this.showFilters = false).catch(err => showErrorMessage(err))
        },
        onAdd(data) {
            this.$store.dispatch("tax.transports.dutyshift/addDutyshift", data)
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
        getDutyshiftData(item) {
            let dutyshift = deepCopy(item)
            return dutyshift
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
        ...mapState("tax.transports.dutyshift", ["dutyshifts"]),
        ...mapState("organizations", ["organizations"]),
        ...mapState("tax.enums", ["enums"]),
        reformedDutyshifts: {
            cache: false,
            get() {
                if (!this.dutyshifts) return
                let result = deepCopy(this.dutyshifts)
                let data = {}
                for (let item of result) {
                    if (!data[item.transportId]) {
                        data[item.transportId] = {}
                        this.fillTransportData(data, item)

                        data[item.transportId].dutyshifts = [{
                            ...this.getDutyshiftData(item)
                        }]

                    } else {
                        data[item.transportId].dutyshifts.push({
                            ...this.getDutyshiftData(item)
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
    }
}
</script>