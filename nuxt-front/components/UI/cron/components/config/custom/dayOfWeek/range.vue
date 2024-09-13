<template>
  <div class="cell-div">
    <el-radio v-model="type_" :label="label" @change="change">
      <span class="cell-symbol">{{ tag_ }}</span>
      {{ $t('common.between') }}
      <el-select v-model="lower" :size="size" :placeholder="$t('common.placeholder')" :disabled="type_ !== label"
        style="width: 100px;" filterable>
        <div v-if="nums">
          <el-option v-for="item in nums" :key="item.value" :label="item.label" :value="item.value"
            />
        </div>
        <div v-else>
          <el-option v-for="item in weekNums" :key="item.label" :label="item.label" :value="item.value"
            />
        </div>

      </el-select>
      {{ $t('common.and') }}
      <el-select v-model="upper" :size="size" :placeholder="$t('common.placeholder')" :disabled="type_ !== label"
        style="width: 100px;" filterable>

        <div v-if="nums">
          <el-option v-for="item in nums" :key="item.value" :label="item.label" :value="item.value"
             />
        </div>
        <div v-else>
          <el-option v-for="item in weekNums" :key="item.label" :label="item.label" :value="item.value"
            />
        </div>

      </el-select>
      {{ $t('common.end') }}{{ $t('common.every') }}{{ timeUnit }}
    </el-radio>
  </div>
</template>

<script>
import { RANGE } from '../../../../constant/filed'
import watchValue from '../../../../mixins/watchValue'
import { isNumber } from '../../../../util/tools'

export default {
  mixins: [watchValue],
  props: {
    nums: {
      type: Array,
      default: null
    },
    size: {
      type: String,
      default: 'mini'
    },
    timeUnit: {
      type: String,
      default: null
    },
    type: {
      type: String,
      default: RANGE
    },
    tag: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      label: RANGE,
      type_: this.type,
      lower: 1,
      upper: 1,
      weekNums: [{
        value: 1,
        label: "Понедельник"
      },
      {
        value: 2,
        label: "Вторник"
      },
      {
        value: 3,
        label: "Среда"
      },
      {
        value: 4,
        label: "Четверг"
      },
      {
        value: 5,
        label: "Пятница"
      },
      {
        value: 6,
        label: "Суббота"
      },
      {
        value: 7,
        label: "Воскресенье"
      }
      ]
    }
  },
  computed: {
    tag_: {
      get() {
        return this.lower + RANGE + this.upper
      },
      set(newValue) {
        if (this.type_ !== RANGE) {
          return
        }
        const arr = newValue.split(RANGE)
        if (!arr[0] || !this.nums) {
          return
        }
        if (arr.length !== 2) {
          this.$message.error(this.$t('common.tagError') + ':' + newValue)
          return
        }
        if (!isNumber(arr[0]) || parseInt(arr[0]) < this.nums[0].value || parseInt(arr[0]) > this.nums[this.nums.length - 1].value) {
          this.$message.error(this.$t('range.lowerError') + ':' + arr[0])
          return
        }
        if (!isNumber(arr[1]) || parseInt(arr[1]) < this.nums[0].value || parseInt(arr[1]) > this.nums[this.nums.length - 1].value) {
          this.$message.error(this.$t('range.upperError') + ':' + arr[1])
          return
        }
        if (parseInt(arr[0]) > parseInt(arr[1])) {
          this.$message.error(this.$t('range.lowerBiggerThanUpperError') + ':' + arr[0] + '>' + arr[1])
          return
        }
        this.lower = parseInt(arr[0])
        this.upper = parseInt(arr[1])
      }
    }
  },
  methods: {
    change() {
      this.$emit('type-changed', this.type_)
      this.$emit('tag-changed', this.tag_)
    }
  }
}
</script>
