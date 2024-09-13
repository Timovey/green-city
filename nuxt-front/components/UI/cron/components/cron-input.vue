<template>
  <div>
    <el-popover v-model="visible">
      <cron class="w-100" v-model="cron" :size="size" @change="change" />
      <v-text-field append-icon="mdi-refresh" @click:append="reset" required :rules="rules" filled outlined
        slot="reference" v-model="cron" :placeholder="$t('common.inputPlaceholder')" :size="size" />
    </el-popover>
  </div>

</template>

<script>
import Cron from './cron'
import { DEFAULT_CRON_EXPRESSION } from '../constant/filed'
export default {
  name: 'CronInput',
  components: {
    Cron
  },
  props: {
    value: {
      type: String,
      default: DEFAULT_CRON_EXPRESSION
    },
    size: {
      type: String,
      default: 'mini'
    }
  },
  data() {
    return {
      visible: false,
      errored: false,
      rules: [v => {
        let error = ""
        let signs = v.trim().split(" ")
        let valid = true
        if (signs[3] != "?" && signs[5] != "?") {
          this.errored = true
          error = "Нельзя одновременно указать дни и дни недели, отметьте в одной из этих вкладок ? не указано"
          valid = false
        }
        if (signs[3] == "?" && signs[5] == "?") {
          this.errored = true
          error = "Укажите дни, либо дни недели"
          valid = false
        }
        console.log(valid)
        if(!valid) this.$message.error(error)
        return valid || error
      }]
    }
  },

  created() {
    this.setCron(this.value)
  },
  methods: {
    setCron(newValue) {
      if (!newValue || newValue.trim().length < 11) {
        this.$message.error(this.$t('common.wordNumError'))
        return
      }
      this.cron = newValue
    },
    change(cron) {
      this.cron = cron
    },
    reset() {
      this.$emit('input', DEFAULT_CRON_EXPRESSION)
    }
  },
  computed: {
    cron: {
      get() {
        return this.value
      },
      set(newValue) {
        if (!newValue || newValue.trim().length < 11) {
          this.$message.error(this.$t('common.wordNumError'))
          return
        }
        this.$emit('input', newValue)
      }
    }
  }
}
</script>

<style scoped>
.red {
  border: 1px solid red;
}
</style>
