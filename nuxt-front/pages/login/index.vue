<template>
    <div class="auth_container">
        <AuthForm :errors="errors" @submit="submit" />
    </div>
</template>

<script>
import AuthForm from '@/components/login/AuthForm.vue'
export default {
    components: {
        AuthForm
    },
    data() {
        return {
            errors: null
        }
    },
    methods: {
        submit(data) {
            this.$store.dispatch('auth/login', data).then(res=>{
                if(res){
                    this.errors = null
                    this.$router.push("/")
                }
            }).catch(res=>this.errors=res)
        },
    },
}
</script>