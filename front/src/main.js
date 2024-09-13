import Vue from "vue";
import router from "./router";
import store from "./store";

import Element from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';

import 'vue-cron-generator/src/styles/global.css';

import App from "./App";

const VueInputMask = require('vue-inputmask').default

import i18n from './lang'

Vue.use(VueInputMask)

Vue.use(Element, {
  size: localStorage.getItem('size') || 'small', // set element-ui default size
  i18n: (key, value) => i18n.t(key, value)
})

import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
Vue.component('font-awesome-icon', FontAwesomeIcon);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  i18n,
  render: (h) => h(App),
}).$mount("#app");
