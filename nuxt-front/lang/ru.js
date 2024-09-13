import { CUR_YEAR, UPPER_LIMIT_YEAR } from 'vue-cron-generator/src/constant/filed'

export default {
  common: {
    from: 'С',
    fromThe: 'Начиная с',
    start: ' Начиная ',
    every: 'Каждый ',
    between: 'Между',
    and: ' и ',
    end: ' заканчивая ',
    specified: 'Указанные',
    symbolTip: 'Символ ',
    valTip: 'Значение ',
    nearest: ' Ближайший',
    current: 'Текущий ',
    nth: ' ',
    index: 'th ',
    placeholder: 'Выберите',
    placeholderMulti: '(Выбрать) несколько',
    help: 'Помощь',
    wordNumError: 'Допустимо 6 или 7 слов',
    reverse: 'Развернуть',
    reset: 'Сбросить',
    tagError: 'Tag Error ',
    numError: 'Number Error ',
    use: 'Использовать',
    inputPlaceholder: 'График'
  },
  custom: {
    unspecified: 'Не указанные',
    workDay: ' Рабочий день',
    lastTh: ' Последние',
    lastOne: ' Последний',
    latestWorkday: ' Последний рабочий день',
    empty: 'Пусто'
  },
  second: {
    title: 'Секунду',
    val: '0 1 2...59'
  },
  minute: {
    title: 'Минуту',
    val: '0 1 2...59'
  },
  hour: {
    title: 'Час',
    val: '0 1 2...23'
  },
  dayOfMonth: {
    timeUnit: 'Day',
    title: 'День',
    val: '1 2...31'
  },
  month: {
    title: 'Месяц',
    val: '1 2...12 or Янв ... Дек'
  },
  dayOfWeek: {
    timeUnit: 'День',
    title: 'День недели',
    val: '1 2...7 or ПН ... ВС',
    SUN: 'Суббота',
    MON: 'Понедельник',
    TUE: 'Вторник',
    WED: 'Среда',
    THU: 'Четверг',
    FRI: 'Пятница',
    SAT: 'Воскресенье'
  },
  per:{
     title: "в"
  },
  year: {
    title: 'Год',
    val: CUR_YEAR + ' ... ' + UPPER_LIMIT_YEAR
  },
  period: {
    startError: 'Start is Error',
    cycleError: 'Cycle is Error'
  },
  range: {
    lowerError: 'Lower is Error',
    upperError: 'Upper is Error',
    lowerBiggerThanUpperError: 'Lower should smaller than Upper'
  },
  weekDay: {
    weekDayNumError: 'The Week Number is Error',
    nthError: 'The Day in Week is Error'
  },
  app: {
    title: 'Cron Generator Implemented by Vue.js and Element-ui',
    next10FireTimes: 'The 10 fired times recently'
  }
}
