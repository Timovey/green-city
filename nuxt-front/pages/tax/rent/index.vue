<template>
    <div>
        <GlobalMenu @onShowFilters="showFilters = true" title="Аренда транспорта" @onPlus="onPlus()" v-model="drawer"
            :menu="menu" />
        <v-dialog width="70%" v-model="addModal">
            <AddRent @onSave="onAdd" :addingErrors="serverErrors"
                :organizations="organizations" :transports="transports" />
        </v-dialog>
        <v-dialog scrollable width="80%" v-model="showFilters">
            <RentFilter  :organizations="organizations"
                :transports="transports" @onSave="applyFilters()"
                v-model="filters" />
        </v-dialog>
        <DeleteItems v-if="selectedItem" v-model="showDeleteModal" @onDelete="onDelete()"
            :message="`Аренда машины ${selectedItem.transportName} ${selectedItem.regNumber}, диапазон ${selectedItem.dateRange.startDate} - ${selectedItem.dateRange.endDate}`" />
        <PageLayout>
            <v-data-table show-expand :expanded.sync="expanded" multi-sort item-key="transportId" :headers="headers"
                :items="reformedRents" :items-per-page="10">
                <template v-slot:expanded-item="{ headers, item }">
                    <td :colspan="headers.length">
                        <div class="text-h6 align-center text--secondary d-flex pa-2" v-for="rent in item.rents">
                            <div class="mr-4">
                                Организация:
                            </div>
                            <div class="mr-4">
                                {{ rent.organization }}
                            </div>
                            <v-spacer />
                            <div class="mr-4">
                                Дата создания:
                            </div>
                            <v-chip class="mr-4">
                                {{ rent.dateRange.createDate }}
                            </v-chip>
                            <div class="mr-4">
                                Диапазон
                            </div>
                            <v-chip>
                                {{ rent.dateRange.startDate }}
                            </v-chip>
                            <div class="pa-2">
                                —
                            </div>
                            <v-chip>
                                {{ rent.dateRange.endDate }}
                            </v-chip>
                            <v-btn @click="onDeleteModal(rent)" color="error" class="ml-2" fab x-small>
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
import AddRent from "@/components/tax/AddRent"
import RentFilter from "@/components/tax/RentFilter"
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
		title: 'Аренда',
	},
    components: {
        GlobalMenu,
        DeleteItems,
        PageLayout,
        RentFilter,
        TableActions,
        AddRent
    },
    mounted() {
        this.menu = taxerMenu
        this.$store.dispatch('tax.transports.rent/fetchRents').catch(err => showErrorMessage(err))
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
            this.$store.dispatch("tax.transports.rent/deleteRent", { rentIds: [this.selectedItem.rentId] })
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
            this.$store.dispatch('tax.transports.rent/fetchRents', data)
                .then(() => this.showFilters = false).catch(err => showErrorMessage(err))
        },
        onAdd(data) {
            this.$store.dispatch("tax.transports.rent/addRent", data)
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
        getRentData(item) {
            let rent = { ...deepCopy(item) }

            let org = this.organizations.find(el => el.id == item.organizationId)
            rent.organization = org == null ? '-' : org.name
            return rent
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
        ...mapState("tax.transports.rent", ["rents"]),
        ...mapState("organizations", ["organizations"]),
        ...mapState("tax.enums", ["enums"]),
        reformedRents: {
            cache: false,
            get() {
                if (!this.rents) return
                let result = deepCopy(this.rents)
                let data = {}
                for (let item of result) {
                    if (!data[item.transportId]) {
                        data[item.transportId] = {}
                        this.fillTransportData(data, item)

                        data[item.transportId].rents = [{
                            ...this.getRentData(item)
                        }]

                    } else {
                        data[item.transportId].rents.push({
                            ...this.getRentData(item)
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