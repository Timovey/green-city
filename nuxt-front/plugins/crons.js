import Vue from "vue";
import VueI18n from "vue-i18n";
import elementEnLocale from "element-ui/lib/locale/lang/en";
import elementZhLocale from "element-ui/lib/locale/lang/zh-CN";
import cronEnLocale from "vue-cron-generator/src/locale/en";
import cronZhLocale from "vue-cron-generator/src/locale/zh";
import { getLocale } from "vue-cron-generator/src/util/tools";

import Element from "element-ui";
import "element-ui/lib/theme-chalk/index.css";

import "vue-cron-generator/src/styles/global.css";
Vue.use(VueI18n);

const messages = {
    en: {
        ...elementEnLocale,
    },
    zh: {
        ...elementZhLocale,
    },
};
let i18n = new VueI18n({
    // set locale
    // options: 'en' | 'zh'
    locale: getLocale(),
    // set locale messages
    messages,
});

Vue.use(Element, {
    size: localStorage.getItem("size") || "small", // set element-ui default size
    i18n: (key, value) => i18n.t(key, value),
});
