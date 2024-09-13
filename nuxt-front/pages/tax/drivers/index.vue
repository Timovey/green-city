<template>
	<div>
		<GlobalMenu @onShowFilters="showFilters = true" title="Водители" @onPlus="onPlus()" v-model="drawer"
			:menu="menu" />
		<v-dialog scrollable width="80%" v-model="showFilters">
			<DriverFilter @onSave="applyFilters()" v-model="filters" :organizations="organizations"
				:drivingSkills="getDrivingSkills" :driverPositions="getDriverPositions" />
		</v-dialog>
		<DeleteItems v-model="showDeleteModal" @onDelete="onDelete()"
			:message="`Водитель ${selectedItem.driverFio}`" />
		<PageLayout>
			<v-data-table multi-sort item-key="driverId" show-select single-select :headers="headers" :items="drivers"
				:items-per-page="10" @item-selected="onItemSelect" ref="driverTable">
				<template v-slot:item.isFired="{ item }">
					<div class="d-flex justify-center" dark>
						<v-icon :color="getColor(item.isFired)" v-if="item.isFired">
							mdi-checkbox-marked-circle
						</v-icon>
						<v-icon :color="getColor(item.isFired)" v-else>
							mdi-close-circle
						</v-icon>
					</div>
				</template>
				<template v-slot:item.organizationId="{ item }">
					{{ getNameOrganization(item.organizationId) }}
				</template>
				<template v-slot:item.drivingSkills="{ item }">
					{{ getNameDrivingSkills(item.drivingSkills) }}
				</template>
				<template v-slot:item.driverPositionType="{ item }">
					{{ getDriverPositionName(item.driverPositionType) }}
				</template>
			</v-data-table>
			<TableActions :selectedItem="selectedItem" @onShowModal="showDeleteModal = true" @onEdit="onEdit" />
		</PageLayout>
	</div>
</template>

<script>
import GlobalMenu from "@/components/UI/GlobalMenu"
import { taxerMenu } from "@/helpers/menus"
import DeleteItems from "@/components/UI/DeleteItems"
import PageLayout from "@/components/UI/PageLayout"
import TableActions from "@/components/UI/TableActions"
import DriverFilter from "@/components/tax/DriverFilter"
import { mapState } from "vuex"
import { showSuccess, showErrorMessage } from "@/helpers/messages"
import { toArray } from "@/helpers/objectHelper"
export default {
	head: {
		title: 'Водители',
	},
	name: 'Drivers',
	components: {
		DriverFilter,
		GlobalMenu,
		DeleteItems,
		PageLayout,
		TableActions
	},
	async created() {
		this.menu = taxerMenu
		this.$store.dispatch("tax.drivers/fetchDrivers").catch(err => showErrorMessage(err))
		this.$store.dispatch('organizations/fetchOrganizations').catch(err => showErrorMessage(err))
		this.$store.dispatch('tax.enums/fetchEnums').catch(err => showErrorMessage(err))
	},
	data() {
		return {
			menu: [],
			baseUrlPage: "/tax/drivers",
			drawer: false,
			selectedItem: false,
			showDeleteModal: false,
			showFilters: false,
			filters: {},
			headers: [
				{
					text: 'Таб. №',
					align: 'start',
					sortable: false,
					value: 'tableNumber',
				},
				{ text: 'ФИО', value: 'driverFio' },
				{ text: 'Должность', value: 'driverPositionType' },
				{ text: 'Адрес регистр.', value: 'regAddress' },
				{ text: 'Факт. адрес', value: 'factAddress' },
				{ text: 'Дата принятия на работу', value: 'acceptWorkDate' },
				{ text: 'Дата рождения', value: 'birthDate' },
				{ text: 'Место рождения', value: 'birthPlace' },
				{ text: 'Телефон', value: 'contactNumber' },
				{ text: 'Разряд', value: 'skillClass' },
				{ text: 'Чем может управлять', value: 'drivingSkills' },
				{ text: 'Лицензия', value: 'driversLicense' },
				{ text: 'Серия паспорта', value: 'passportSerie' },
				{ text: 'Номер паспорта', value: 'passportNumber' },
				{ text: 'Дата выдачи', value: 'issueDate' },
				{ text: 'Кем выдан', value: 'issuer' },
				{ text: 'Код подразделения', value: 'devisionCode' },
				{ text: 'Уволен', value: 'isFired' },
				{ text: 'Дата увольнения', value: 'firedDate' },
				{ text: 'Организация', value: 'organizationId' },
			],
		}
	},
	methods: {
		getColor(item) {
			if (item) return 'red'
			else return 'warning'
		},
		getNameDrivingSkills(drivingSkills) {
			if (this.enums.DrivingSkills) {
				return drivingSkills.map(el => this.enums.DrivingSkills[el]).join(' ')
			}
			return 'Неизвестно'
		},
		getNameOrganization(orgsId) {
			if(!orgsId) {
				return null
			}
			const res = this.organizations.find(el => el.id == orgsId)
			if(res) {
				return res.name
			}
			return null
		},
		getDriverPositionName(position) {
			if (this.enums.DriverPosition && this.enums.DriverPosition[position]) {
				return this.enums.DriverPosition[position]
			}
			return 'Неизвестно'
		},
		onPlus() {
			this.$router.push(`${this.baseUrlPage}/edit/new`)
		},
		onEdit() {
			this.$router.push(`${this.baseUrlPage}/edit/${this.selectedItem.driverId}`)
		},
		onDelete() {
			let data = { driverIds: [this.selectedItem.driverId] }
			this.$store.dispatch('tax.drivers/deleteDriver', data)
				.then(res => {
					console.log('res', res);
					showSuccess()
				}).catch(err => {
					console.log('err', err);
					showErrorMessage(err)
				})
			this.showDeleteModal = false
			this.selectedItem = false
		},
		onItemSelect(item) {
			if (item.value)
				this.selectedItem = item.item
			else this.selectedItem = false
		},
		applyFilters() {
			let data = {
				isFilter: true,
				isFilterByCreatedUser: true,
				...this.filters,
				sorting: {}
			}
			this.$store.dispatch("tax.drivers/fetchDrivers", data).catch(err => {
				showErrorMessage(err)
			})
			this.showFilters = false
			//убираем выделение элемента, т.к. его может и не быть
			this.selectedItem = false
			this.$refs.driverTable.selection = []
		}
	},
	computed: {
		...mapState("tax.drivers", ["drivers"]),
		...mapState("organizations", ["organizations"]),
		...mapState("tax.enums", ["enums"]),
		getDrivingSkills() {
			return toArray(this.enums.DrivingSkills);
		},
		getDriverPositions() {
			return toArray(this.enums.DriverPosition);
		},
	}
}
</script>
<style lang="scss">

</style>