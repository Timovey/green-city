<v-alert text prominent class="mb-4" type="error" icon="mdi-clock-fast">
    {{error}}
</v-alert>

<script>
export default {
    props:{
        error: {
            type: String,
            required: false
        }
    }
}
</script>