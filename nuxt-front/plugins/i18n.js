import "element-ui/lib/theme-chalk/index.css";
import elementEnLocale from "element-ui/lib/locale/lang/en";

import VueI18n from "vue-i18n";
import { getLocale } from "vue-cron-generator/src/util/tools";
import "vue-cron-generator/src/styles/global.css";
import cronEnLocale from "@/lang/ru";
import enLocale from "@/lang/en";

const messages = {
        ru: {
            ...enLocale,
            ...elementEnLocale,
            ...cronEnLocale,
        },
        en: {
            ...enLocale,
            ...elementEnLocale,
            ...cronEnLocale,
        }
    },
    i18n = new VueI18n({
        // set locale
        // options: 'en' | 'zh'
        locale: "ru",
        // set locale messages
        messages,
    });

export default ({ app }) => {
    app.i18n = i18n;
};
