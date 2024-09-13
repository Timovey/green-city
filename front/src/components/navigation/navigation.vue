<template>
  <div class="navigation">
    <div class="navigation__title">
      <vTitle class="navigationTitle">{{ title }}</vTitle>
    </div>
    <div class="navigation__container" v-for="(category, index) in categories" :key="index">
      <dropDownMenu @view="changeCurrentView" :category="category" />
    </div>
  </div>
</template>

<script>
import dropDownMenu from "@/components/navigation/dropDownMenu";
import vTitle from "@/components/ui-kit/vTitle";

export default {
  name: "navigation",

  components: {
    dropDownMenu,
    vTitle,
  },

  props: {
    title: {
      type: String,
      default: ''
    },

    categories: {
      type: Array,
      default() {
        return []
      }
    }
  },

  methods: {
    changeCurrentView(data) {
      this.$emit('view', {link: data.link})
    }
  }
}
</script>

<style lang="scss" scoped>
.navigation {
  width: 100%;
  height: 100%;
  background: rgba(37, 72, 15, 0.75);
  padding: 18px 24px;

  &__container {
    margin: 10px 0;
  }
}
</style>
