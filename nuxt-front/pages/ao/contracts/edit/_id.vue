<template>
	<div>
		<GlobalMenu @onShowFilters="showFilters = true" @onBack="onBack" :backButton="true" title="Договоры"
			@onPlus="onPlus()" :edit="true" v-model="drawer" :menu="menu" />
		<NewByKP ref="newByKP" @save="saveByKP" v-model="showAddByKP" />
		<v-dialog v-model="showAddContainerContract" fullscreen hide-overlay transition="dialog-bottom-transition">
			<AddContainerContract @onAddressSearch="onAddressSearch" :current="currentContainer"
				@onClose="showAddContainerContract = false" :containers="searchedContainers"
				@onContainerSave="saveContainer($event)" @onContainerAdd="addContainer($event)" />
		</v-dialog>
		<v-row>
			<v-col class="col col-1" />
			<v-col class="col col-10">
				<v-card class="mt-5">
					<v-card-title class="text--secondary">
						<div v-if="editStatus">Редактирование договора</div>
						<div v-else>Добавление договора</div>
					</v-card-title>
					<v-form ref="form" v-model="isValid">
						<v-card-text class="pb-2">
							<v-autocomplete :rules="requiredRules" required v-model="contract.contractClientId"
								:items="contragents" item-value="contractClientId" item-text="clientName" filled
								label="Контрагент" outlined clearable />
							<div class="d-flex">
								<v-text-field :rules="requiredRules" required v-model="contract.contractNumber" class="mr-2"
									filled type="text" label="Номер договора" outlined clearable />
								<v-text-field v-model="contract.sendDocsType" class="ml-2" filled type="text"
									label="Способ отправки документов" outlined clearable />
							</div>
							<div class="d-flex justify-space-around">
								<v-checkbox v-model="contract.isAutorenewSchedules" label="Автопродление договора"
									color="primary" />
								<v-checkbox v-model="contract.contractStatus" label="Статус подписания договора "
									color="primary" />
							</div>
							<v-textarea required v-model="contract.comment" filled type="text" label="Комментарий" outlined
								clearable />
							<v-card outlined class="ma-2">
								<v-card-title>
									Контейнеры
								</v-card-title>
								<v-card-text>
									<v-expansion-panels :key="keys" accordion multiple>
										<v-expansion-panel v-for="(stand, key) in containersToShow" :key="key">
											<v-expansion-panel-header>
												{{ key }}
											</v-expansion-panel-header>
											<v-expansion-panel-content :value="key">
												<ContainerItem :key="container.containerId"
													v-for="(container, index) in stand" class="w-50"
													@remove="deleteContainer(container)" @edit="editContainer(container)"
													:container="container" :containers="containers" />
											</v-expansion-panel-content>
										</v-expansion-panel>
									</v-expansion-panels>

									<!-- <v-alert v-else border="bottom" colored-border type="warning" elevation="2">
										Необходимо добавить контейнеры</v-alert> -->
								</v-card-text>
								<v-card-actions>
									<v-spacer />
									<v-btn @click="addNew()" class="primary">Добавить 1 контейнер</v-btn>
									<v-btn @click="addNewByKP()" class="primary">Добавить из КП</v-btn>
									<v-spacer />
								</v-card-actions>
							</v-card>
							<div class="d-flex pt-2 justify-space-between">
								<PickDate ref="pickDate" :rules="requiredRules" v-model="startValid"
									:date.sync="contract.startDate" label="Дата начала" />
								<v-spacer></v-spacer>
								<PickDate v-model="endValid" :date.sync="contract.endDate" label="Дата конца"
									:clearable="true" />
							</div>

						</v-card-text>
						<v-spacer />
						<v-card-actions>
							<v-spacer />
							<v-btn @click="onBack" color="error lighten-2">
								Отмена
							</v-btn>
							<v-btn @click="onSubmit" color="primary">
								<div v-if="editStatus">Сохранить</div>
								<div v-else>Добавить</div>
							</v-btn>
						</v-card-actions>
					</v-form>
				</v-card>
			</v-col>
			<v-col class="col col-1" />
		</v-row>
	</div>
</template>

<script>
import { mapState } from "vuex"
import { showError, showSuccess, showErrorMessage } from "@/helpers/messages"
import AddContainerContract from "@/components/ao/AddContainerContract"
import ContainerItem from "@/components/ao/ContainerItem.vue"
import GlobalMenu from "@/components/UI/GlobalMenu"
import PickDate from "@/components/UI/PickDate"
import NewByKP from "@/components/ao/NewByKP.vue"
import { aoMenu } from '@/helpers/menus.js'
export default {
	components: {
		GlobalMenu,
		PickDate,
		NewByKP,
		AddContainerContract,
		ContainerItem
	},
	created() {
		this.menu = aoMenu
		this.$store.dispatch("ao.contragents/fetchContragents")
		this.id = this.$route.params.id
		this.$store.dispatch("ao.capacities/fetchContainers")
			.then(res => this.containers = res)

		if (this.id == "new") {
			this.editStatus = false
		}
		else {
			this.editStatus = true
			this.$store.dispatch("ao.contracts/getContract", { contractId: this.id })
				.then(res => {
					this.contract = res
				})
		}
	},
	data() {
		return {
			showAddByKP: false,
			show: false,
			containersToShow: [],
			showAddContainerContract: false,
			id: 0,
			keys: 0,
			editStatus: false,
			drawer: false,
			menu: [],
			startValid: false,
			endValid: true,
			currentContainer: false,
			contract: {
				contractClientId: "",
				contractNumber: "",
				sendDocsType: "",
				contractStatus: false,
				isAutorenewSchedules: false,
				attachedContainers: [],
				startDate: "",
				endDate: null,
			},
			requiredRules: [v => String(v).length > 0 || 'Поле не долно быть пустым'],
			isValid: false,
			containers: [],
			addressQuery: ""
		}
	},

	methods: {
		onBack() {
			this.$router.push("/ao/contracts")
		},
		onSubmit() {
			console.log()
			this.$refs.form.validate()
			this.$refs.pickDate.$refs.form.validate()
			if (this.isValid && this.dateValid) {
				if (!this.editStatus) {
					this.$store.dispatch("ao.contracts/addContract", this.contract)
						.then(res => showSuccess())
						.catch(err => {
							console.log(err);
							showError(err.toString())
						})
				} else {
					this.$store.dispatch("ao.contracts/updateContract", this.contract)
						.then(res => showSuccess())
						.catch(err => showErrorMessage(err))
				}
				this.$router.push("/ao/contracts")
			}
			else {
				showError()
			}
		},
		addContainer(container) {
			this.contract.attachedContainers.push(JSON.parse(JSON.stringify(container)))
			this.showAddContainerContract = false
		},
		deleteContainer(container) {
			const idToDelete = this.contract.attachedContainers.findIndex(el => el.containerId === container.containerId)
			return this.contract.attachedContainers.splice(idToDelete, 1)
		},
		editContainer(container) {
			const cntnr = { ...this.contract.attachedContainers.find(el => el.containerId === container.containerId) }

			this.currentContainer =
			{
				...cntnr,
				inRegistryFromDate: this.$correctDate(cntnr.inRegistryFromDate)
			}
			this.showAddContainerContract = true
		},
		saveContainer(container) {
			const idToSave = this.contract.attachedContainers.findIndex(el => el.containerId === container.containerId)
			this.contract.attachedContainers[idToSave] = container
			this.currentContainer = false
			this.showAddContainerContract = false
			this.containersToShow = this.reformContainers()
		},
		addNew() {
			this.currentContainer = false
			this.showAddContainerContract = true
		},
		addNewByKP() {
			this.showAddByKP = true
		},
		saveByKP(arr) {
			let errors = []
			let tempContainers = []
			for (let container of arr) {
				if (this.currentContainersIds.indexOf(container.containerId) != -1) {
					errors.push(container.name)
				} else {
					this.contract.attachedContainers.push(container)
					tempContainers.push(container)
				}
			}
			if (errors.length) {
				showError(`Данные контейнеры: ${errors.join(', ')} уже были добавлены`)
				//удаляем контейнеры, которые добавили, но у нас вылезла ошибка
				for (let container of tempContainers) {
					this.contract.attachedContainers.pop(container)
				}
			}
			else {
				this.refreshNewByKP()
			}
		},
		refreshNewByKP() {
			this.showAddByKP = false
			this.$refs.newByKP.clearData()
		},
		onAddressSearch(val) {
			this.addressQuery = val
		},
		reformContainers() {
			let containers = {}
			for (let item of this.contract.attachedContainers) {
				if (containers[item.containerStandAddress]) {
					containers[item.containerStandAddress].push(item)
				}
				else {
					containers[item.containerStandAddress] = [item]
				}
			}
			return containers
		}
	},
	computed: {
		currentContainersIds(val) {
			return this.contract.attachedContainers.map(item => item.containerId);
		},
		...mapState("ao.contragents", ["contragents"]),
		dateValid(val) {
			// console.log(val)
			return this.startValid
		},
		searchedContainers() {
			if (!this.addressQuery) return this.containers
			console.log(this.containers, "comp", this.addressQuery)
			return JSON.parse(JSON.stringify(this.containers))
				.filter(container => container.containerStandAddress == this.addressQuery)
		}
	},
	watch: {
		contract: {
			immediate: true,
			deep: true,
			handler: function (newValue) {
				this.containersToShow = this.reformContainers()
			}
		}
	}
}
</script>

<style>
.v-expansion-panel-content__wrap {
	display: flex;
	justify-content: space-between;
	flex-wrap: wrap;
}

.w-50 {
	width: 49%;
	margin-top: 10px;
}
</style>
