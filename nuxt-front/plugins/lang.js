import Vue from 'vue'
import VueI18n from 'vue-i18n'
import elementEnLocale from 'element-ui/lib/locale/lang/en'
import elementZhLocale from 'element-ui/lib/locale/lang/zh-CN'
import cronEnLocale from './ru'
import cronZhLocale from 'vue-cron-generator/src/locale/zh'
import enLocale from './en'
import zhLocale from './zh'

Vue.use(VueI18n)

const messages = {
    en: {
      ...enLocale,
      ...elementEnLocale,
      ...cronEnLocale
    },
    zh: {
      ...zhLocale,
      ...elementZhLocale,
      ...cronZhLocale
    }
  },

  i18n = new VueI18n({
    locale: 'en',
    messages
  })

export default i18n