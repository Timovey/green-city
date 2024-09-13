<template>
  <div class="statusModal" :class="{error: isError}">
    <vTitle v-if="!isError" class="tableCellTitle">Данные сохранены</vTitle>
    <vTitle v-else class="tableCellTitle">Произошла ошибка</vTitle>
  </div>
</template>

<script>

import vTitle from "@/components/ui-kit/vTitle";

export default {
  name: "createdStatusModal",

  components: {
    vTitle,
  },

  props: {
    isError: {
      type: Boolean,
      default: false
    }
  }
}
</script>

<style lang="scss" scoped>
.statusModal {
  position: fixed;
  bottom: 40px;
  right: -150px;
  z-index: 100;
  box-shadow: 0px 20px 30px rgba(0, 0, 0, 0.04), 0px 6px 10px rgba(0, 0, 0, 0.02);
  border-radius: 10px;
  padding: 10px 15px;
  border: 1px solid #adbfa2;
  background: #d7f4e1;
  animation-name: slide;
  animation-duration: 3s;
  transform: scale(1.4);

  @keyframes slide {
    0% {
      right: -150px;
    }

    50% {
      right: 50px;
    }

    100% {
      right: -150px;
    }
  }
}

.error {
  border: 1px solid #d66262;
  background: #f7d9d9;
}
</style>
