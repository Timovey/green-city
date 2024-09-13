<template>
    <div>
        <GlobalMenu @onShowFilters="showFilters = true" title="Привязки водителей" @onPlus="onPlus()" v-model="drawer"
            :menu="menu" />
        <v-dialog width="70%" v-model="addModal">
            <AddFixing @onSave="onAdd" :addingErrors="serverErrors" :transports="transports" :drivers="drivers" :workShifts="getWorkShifts"/>
        </v-dialog>
        <v-dialog scrollable width="80%" v-model="showFilters">
            <FixingFilter v-model="filters" :drivers="drivers" :transports="transports" @onSave="applyFilters()" />
        </v-dialog>
        <DeleteItems v-model="showDeleteModal" @onDelete="onDelete()"
            :message="`Привязку водителя ${selectedItem.driverName} ${selectedItem.driverInitials} на транспорт ${selectedItem.transportName + ' ' + selectedItem.regNumber}`" />
        <PageLayout>
            <v-data-table show-expand :expanded.sync="expanded" multi-sort item-key="transportId" :headers="headers"
                :items="reformedFixes" :items-per-page="10" @item-selected="onItemSelect">
                <template v-slot:expanded-item="{ headers, item }">
                    <td :colspan="headers.length">
                        <div class="text-h6 align-center text--secondary d-flex pa-2" v-for="driver in item.drivers">
                            <div class="mr-4">
                                Водитель:
                            </div>
                            <div class="mr-4">
                                {{ driver.driverFio }}
                            </div>
                            <div class="mr-4">
                                Выход:
                            </div>
                            <div class="mr-4">
                                {{ driver.workShift }}
                            </div>
                            <v-spacer />
                            <div class="mr-4">
                                Дата создания:
                            </div>
                            <v-chip class="mr-4">
                                {{ driver.dateRange.createDate }}
                            </v-chip>
                            <div class="mr-4">
                                Закрепление
                            </div>
                            <v-chip>
                                {{ driver.dateRange.startDate }}
                            </v-chip>
                            <div class="pa-2">
                                —
                            </div>
                            <v-chip>
                                {{ driver.dateRange.endDate }}
                            </v-chip>
                            <v-btn @click="onDeleteModal(driver)" color="error" class="ml-2" fab x-small>
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
import AddFixing from "@/components/tax/AddFixing"
import FixingFilter from "@/components/tax/FixingFilter"
import { taxerMenu } from "@/helpers/menus"
import DeleteItems from "@/components/UI/DeleteItems"
import GlobalAppBar from "@/components/UI/GlobalAppBar"
import PageLayout from "@/components/UI/PageLayout"
import TableActions from "@/components/UI/TableActions"
import { mapState } from "vuex"
import { showSuccess, showErrorMessage } from "@/helpers/messages"
import { deepCopy } from "@/helpers/deepCopyHelper";
import { toArray } from "@/helpers/objectHelper"

export default {
    head: {
		title: 'Привязки',
	},
    components: {
        GlobalMenu,
        GlobalAppBar,
        DeleteItems,
        PageLayout,
        FixingFilter,
        TableActions,
        AddFixing
    },
    created() {
        this.menu = taxerMenu
        this.$store.dispatch("tax.drivers/fetchDrivers").catch(err => showErrorMessage(err))
        this.$store.dispatch('tax.transports/fetchTransports').catch(err => showErrorMessage(err))
        this.$store.dispatch('tax.drivers.fixing/fetchFixing').catch(err => showErrorMessage(err))
        this.$store.dispatch('organizations/fetchOrganizations').catch(err => showErrorMessage(err))
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
            this.$store.dispatch("tax.drivers.fixing/deleteFix", { driver2TransportIds: [this.selectedItem.driver2TransportId] })
                .then(() => {
                    showSuccess()
                }).catch(err => showErrorMessage(err))
            this.showDeleteModal = false
        },
        onEdit() {
            this.$router.push(`/tax/transports/edit/${this.selectedItem.id}`)
        },
        applyFilters() {
            let data = {
                isFilter: true,
                sorting: {},
                ...this.filters
            }
            this.$store.dispatch('tax.drivers.fixing/fetchFixing', data)
                .then(() => this.showFilters = false).catch(err => showErrorMessage(err))
        },
        onAdd(data) {
            this.$store.dispatch("tax.drivers.fixing/addFix", data)
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
        getDriverData(item) {
            let driver = { ...deepCopy(item) }

            if (this.enums.DriverPosition && this.enums.DriverPosition[item.driverPositionType]) {
                driver.driverPosition = this.enums.DriverPosition[item.driverPositionType]
            }
            if (this.enums.WorkShift && this.enums.WorkShift[item.workShift]) {
                driver.workShift = this.enums.WorkShift[item.workShift]
            }
            let org = this.organizations.find(el => el.id == item.driverOrganizationId)

            driver.driverOrganization = org == null ? '-' : org.name
            return driver
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
        ...mapState("tax.drivers.fixing", ["fixes"]),
        ...mapState("organizations", ["organizations"]),
        ...mapState("tax.enums", ["enums"]),
        ...mapState("tax.drivers", ["drivers"]),
        ...mapState("tax.transports", ["transports"]),
        getWorkShifts() {
            return toArray(this.enums.WorkShift);
        },
        reformedFixes() {
            let result = deepCopy(this.fixes)
            let data = {}
            for (let item of result) {
                if (!data[item.transportId]) {
                    data[item.transportId] = {}
                    this.fillTransportData(data, item)

                    data[item.transportId].drivers = [{
                        ...this.getDriverData(item)
                    }]

                } else {
                    data[item.transportId].drivers.push({
                        ...this.getDriverData(item)
                    })
                }
            }
            let resultData = []
            for (let item of Object.keys(data)) {
                resultData.push(data[item])
            }
            return resultData
        }
    }
}
</script>