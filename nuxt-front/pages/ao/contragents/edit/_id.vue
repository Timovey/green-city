<template>
    <div>
        <GlobalMenu @onShowFilters="showFilters = true" :backButton="true" :edit="true" title="Контрагенты"
            @onPlus="onPlus()" v-model="drawer" :menu="menu" @onBack="onBack"/>
        <v-row>
            <v-col class="col col-2" />
            <v-col class="col col-8">
                <v-card class="mt-5">
                    <v-form ref="form" v-model="isValid">
                        <v-card-title class="text--secondary">
                            Реквизиты
                        </v-card-title>
                        <v-divider class="mx-4"></v-divider>
                        <v-card-text class="pb-2">
                            <v-text-field :rules="requiredRules" v-model="data.clientName" filled label="Наименование"
                                outlined clearable />
                            <v-text-field v-model="data.inn" filled label="ИНН" class="mr-2"
                                type="number" outlined clearable />
                            <v-text-field v-model="data.kpp" filled label="КПП" type="number"
                                outlined clearable />
                            <v-text-field v-model="data.ogrn" filled label="ОГРН" type="number"
                                outlined clearable />
                            
                           
                            <div class="d-flex">
                                <v-text-field v-model="data.rschet" filled label="Р/СЧ"
                                    class="mr-2" outlined clearable type="number" />
                                <v-text-field v-model="data.bank" filled class="ml-2"
                                    label="Банк" outlined clearable />
                            </div>
                           
                            <v-text-field v-model="data.bik" filled label="БИК" class="mr-2"
                                type="number" outlined clearable />
                            <v-text-field v-model="data.kschet" filled label="К/СЧ" class="mr-2"
                                type="number" outlined clearable />
                        </v-card-text>
                        <v-card-title class="pt-0 text--secondary">
                            Адрес
                        </v-card-title>
                        <v-divider class="mx-4"></v-divider>
                        <v-card-text>
                            <v-text-field  v-model="data.legalAddress" filled label="Юридический адрес" class="mr-2"
                                outlined clearable>
                            </v-text-field>
                            <v-text-field v-model="data.postalAddress" filled label="Почтовый адрес" class="mr-2"
                                outlined clearable>
                            </v-text-field>
                            <v-text-field v-model="data.email" filled label="Электронная почта" class="mr-2" outlined
                                clearable></v-text-field>
                        </v-card-text>
                        <v-card-title class="pt-0 text--secondary">
                            Информация о руководителе
                        </v-card-title>
                        <v-divider class="mx-4"></v-divider>
                        <v-card-text>
                            <v-text-field v-model="data.supervizorName" filled label="Руководитель" class="mr-2"
                                outlined clearable>
                            </v-text-field>
                            <v-text-field v-model="data.supervizorPosition" filled label="Должность руководителя"
                                class="mr-2" outlined clearable>
                            </v-text-field>
                        </v-card-text>
                        <v-card-title class="pt-0 text--secondary">
                            Ответственное лицо по ТКО
                        </v-card-title>
                        <v-divider class="mx-4"></v-divider>
                        <v-card-text>
                            <v-text-field v-model="data.responsibleTkoPersonName" filled type="phone"
                                label="Ответственный по ТКО" class="mr-2" outlined clearable></v-text-field>
                            <v-text-field v-model="data.responsibleTkoPersonPhone" filled label="ФИО" class="mr-2"
                                outlined clearable></v-text-field>
                            <v-text-field  v-model="data.clientContact" filled type="phone" label="Контактный телефон"
                                class="mr-2" outlined clearable>
                            </v-text-field>
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
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
            <v-col class="col col-2" />
        </v-row>
    </div>
</template>

<script>
import { aoMenu } from '@/helpers/menus'
import GlobalMenu from "@/components/UI/GlobalMenu"
import { mapState } from "vuex"
export default {
    data() {
        return {
            editStatus: false,
            id: false,
            drawer: false,
            isValid: false,
            menu: [],
            data: {
                isValidated: false,
                clientName: '',
                inn: '',
                kpp: '',
                ogrn: '',
                rschet: "",
                bank: '',
                bik: '',
                kschet: '',
                legalAddress: '',
                postalAddress: '',
                email: '',
                supervizorName: '',
                supervizorPosition: '',
                responsibleTkoPersonName: '',
                responsibleTkoPersonPhone: '',
                clientContact: '',
            },
            requiredRules: [
                v => !!v || 'Поле обязательно к заполнению',
            ],
            emailRules: [
                v => !!v || 'Поле обязательно к заполнению',
                v => /.+@.+\..+/.test(v) || 'Некорректный адрес',
            ],
            phoneRules: [
                v => !!v || 'Введите номер',
                v => /(?:\+|\d)[\d\-\(\) ]{9,}\d/g.test(v) || 'Некорректный номер телефона',
            ],
        }
    },
    components: {
        GlobalMenu
    },
    created() {
        this.menu = aoMenu
        this.id = this.$route.params.id
        if (this.id == "new") {
            this.editStatus = false
        }
        else {
            this.editStatus = true
            this.$store.dispatch("ao.contragents/getContragent", { contractClientId: this.id })
                .then(res => this.data = res)
        }

    },
    methods: {
        onBack() {
            this.$router.push("/ao")
        },
        onSubmit() {
            this.$refs.form.validate()
            if (!this.isValid) return
            let data = {
                ...this.data,
                user: localStorage.getItem('user')
            }
            if (this.editStatus) {
                this.$store.dispatch("ao.contragents/updateContragent", data).then(res => this.$router.push("/ao/"))
            }
            else {
                this.$store.dispatch("ao.contragents/addContragent", data).then(res => this.$router.push("/ao/"))
            }
        }
    },
    computed: {
        ...mapState["auth", ["user", "token"]],
    }
}
</script>
