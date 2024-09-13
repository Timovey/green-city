<template>
  <div class="dropDownMenu__container">
    <div class="category" v-if="!category.link" @click="showSubcategory = !showSubcategory">
      <vTitle class="dropDownMenuTitle">{{ category.name }}</vTitle>
      <img :class="{ rotate: showSubcategory }" src="@/assets/icons/dropDownMenuArrow.svg" alt="" />
    </div>
    <div class="category" v-else @click="changeCurrentView(category.link)">
      <vTitle class="dropDownMenuTitle">{{ category.name }}</vTitle>
    </div>
    <div class="subcategory" v-if="category.subcategories" v-show="showSubcategory">
      <div class="subcategory__container" v-for="(subcategory, index) in category.subcategories" :key="index" @click="changeCurrentView(subcategory.link)">
        <vTitle class="dropDownMenuTitle">{{ subcategory.name }}</vTitle>
      </div>
    </div>
  </div>
</template>

<script>
import vTitle from "@/components/ui-kit/vTitle";

export default {
  name: "dropDownMenu",

  components: {
    vTitle,
  },

  props: {
    category: {
      type: Object,
      default() {
        return {}
      }
    },
  },

  data() {
    return {
      showSubcategory: false,
    }
  },

  methods: {
    changeCurrentView(link) {
      this.$emit('view', {link: link})
    }
  }
}
</script>

<style lang="scss" scoped>
.dropDownMenu__container {
  width: 100%;
  margin: 0 auto;
  display: block;

  .category {
    background: rgba(255, 255, 255, 0.2);
    width: 100%; 
    padding: 5px 10px;
    border-radius: 5px;
    cursor: pointer;
    display: flex;
    justify-content: space-between;
    align-items: center;

    .rotate {
      transform: rotate(180deg);
    }
  }

  .subcategory {
    margin: 3px 0 0 0;
    background: rgba(164, 210, 143, 0.2);
    width: 100%; 
    padding: 5px;
    border-radius: 5px;

    &__container {
      padding: 5px;
      cursor: pointer;
      transition: all .2s ease;
      border-radius: 5px;

      &:hover {
        background: rgba(164, 210, 143, 1);
      }
    }
  }
}
</style>
