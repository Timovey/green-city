<template>
  <RouteView :menu="menu" :baseUrlPage="baseUrlPage" />
</template>

<script>
import RouteView from "@/components/common/RouteView.vue";
import { logistMenu } from "@/helpers/menus";
export default {
  head: {
    title: "Рейсы",
  },
  name: "Routes",
  components: {
    RouteView,
  },
  created() {
    this.menu = logistMenu;
  },
  data() {
    return {
      menu: [],
      baseUrlPage: "/logist/route",
    };
  },
};
</script>

<style lang="scss" scoped></style>
