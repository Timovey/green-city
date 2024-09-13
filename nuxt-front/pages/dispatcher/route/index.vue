<template>
  <RouteView :menu="menu" :baseUrlPage="baseUrlPage" :isCanAdd="true" />
</template>

<script>
import RouteView from "@/components/common/RouteView.vue";
import { dispatcherMenu } from "@/helpers/menus";
export default {
  head: {
    title: "Рейсы",
  },
  name: "Routes",
  components: {
    RouteView,
  },
  created() {
    this.menu = dispatcherMenu;
  },
  data() {
    return {
      menu: [],
      baseUrlPage: "/dispatcher/route",
    };
  },
};
</script>

<style lang="scss" scoped></style>
