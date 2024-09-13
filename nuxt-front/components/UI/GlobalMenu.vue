<template>
  <div>
    <v-app-bar>
      <v-app-bar-nav-icon
        @click="$emit('input', { value: true })"
      ></v-app-bar-nav-icon>
      <div class="mr-4 text--secondary text-h5 px-2">{{ title }}</div>
      <v-btn v-if="!edit" @click="$emit('onPlus')" color="primary" small fab>
        <v-icon> mdi-plus </v-icon>
      </v-btn>
      <v-spacer />
      <v-btn class="mr-2 teal lighten-3" @click="$router.push('/')"
        >На главную</v-btn
      >
      <v-btn
        v-if="!edit"
        color="primary"
        @click="$emit('onShowFilters')"
        class="mr-2"
        >Фильтр поиска</v-btn
      >
      <v-btn
        v-if="edit && backButton"
        color="warning lighten-2 text--secondary"
        @click="$emit('onBack')"
        class="mr-2"
        >Назад</v-btn
      >
      <v-btn color="indigo lighten-1" @click="logout" class="mr-2">Выйти</v-btn>
    </v-app-bar>
    <v-navigation-drawer
      v-model="show.value"
      absolute
      temporary
      floating
      width="350"
    >
      <div class="pa-4 text-h4">{{ title }}</div>
      <v-divider></v-divider>
      <v-list>
        <v-list-item-group
          v-for="item in menu"
          :key="item.text"
          active-class="green--text d-flex align-left text--accent-4"
        >
          <v-list-item v-if="item.to" exact link :to="item.to">
            <v-list-item-icon>
              <v-icon>{{ item.ico }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title class="text-left">{{
                item.text
              }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
          <v-list-group v-else :prepend-icon="item.ico">
            <template v-slot:activator>
              <v-list-item-title class="text-left">{{
                item.text
              }}</v-list-item-title>
            </template>
            <v-list-item
              exact
              v-for="subitem in item.sublist"
              :key="subitem.text"
              nuxt
              active-class="activeLink"
              :to="subitem.to"
              class="ml-6"
            >
              <v-list-item-icon>
                <v-icon small>{{ subitem.ico }}</v-icon>
              </v-list-item-icon>
              <v-list-item-title class="text-left">{{
                subitem.text
              }}</v-list-item-title>
            </v-list-item>
          </v-list-group>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    value: {
      type: Object | String,
      default: {},
    },
    menu: [],
    edit: false,
    backButton: true,
  },
  methods: {
    logout() {
      this.$store.dispatch("auth/logout", {});
      this.$router.push("/login");
    },
  },
  computed: {
    show: {
      get() {
        return { value: this.value };
      },
      set() {
        this.$emit("input", { value: this.value });
      },
    },
  },
};
</script>
