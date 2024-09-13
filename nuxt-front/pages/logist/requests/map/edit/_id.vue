<template>
    <div>
        <GlobalMenu @onShowFilters="showFilters = true" :backButton="true" :edit="true" title="Заявки по картам"
            @onPlus="onPlus()" v-model="drawer" :menu="menu" @onBack="onBack" />
        <v-row>
            <v-col class="col col-2" />
            <v-col class="col col-8">
                <v-card class="mt-5">
                    <CreateRequest :data="initialData" :readonly="editStatus" :map="true" :edit="editStatus"
                        @submit="onSubmit">
                    </CreateRequest>
                </v-card>
            </v-col>
            <v-col class="col col-2" />
        </v-row>
    </div>
</template>

<script>
import { logistMenu } from '@/helpers/menus'
import CreateRequest from '@/components/common/CreateRequest.vue'
import GlobalMenu from "@/components/UI/GlobalMenu"
import { mapState } from "vuex"
import { showSuccess, showErrorMessage } from "@/helpers/messages";
export default {
    data() {
        return {
            editStatus: false,
            id: false,
            drawer: false,
            isValid: false,
            menu: logistMenu,
            data: {

            },
            initialData: {},
            requiredRules: [
                v => !!v || 'Поле обязательно к заполнению',
            ]
        }
    },
    components: {
        GlobalMenu,
        CreateRequest
    },
    created() {
        this.id = this.$route.params.id
        if (this.id == "new") {
            this.editStatus = false
        }
        else {
            this.editStatus = true
            this.$api.requestMapLogistWorker.get({ id: this.id })
                .then((val) => this.initialData = val)
                .catch((err) => {
                    showErrorMessage(err);
                });
        }

    },
    methods: {
        onBack() {
            this.$router.push("/logist/requests/map")
        },
        onSubmit(request) {
            let data = {
                ...request,
                user: this.user
            }
            this.$api.requestMapLogistWorker.add(data)
                .then(() => {
                    showSuccess("Успешно добавлено!")
                    this.$router.push("/logist/requests/map")
                })
                .catch((err) => {
                    showErrorMessage(err);
                });
        }
    },
    computed: {
        ...mapState["auth", ["user", "token"]],
    }
}
</script>
