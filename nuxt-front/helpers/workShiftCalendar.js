const dateDriverFormat = 'DD-MM-YYYY'
import { correctDate } from './dateHelper'
export const formateDriverDate = (context, value) => {
    return correctDate(context, value, dateDriverFormat)
}
export const formateDate = (context, value) => {
    return correctDate(context, value)
}
export const getCountDay = (minDate, maxDate) => {
    return Math.abs(maxDate.getTime() - minDate.getTime()) / (1000 * 3600 * 24)
}
export const transportEventsColor = {
    0: "red darken-1",
    1: "light-green lighten-1",
    2: "light-green lighten-1",
    3: "light-green lighten-1",
    4: "light-blue lighten-1",
    5: "light-blue lighten-1",
    6: "light-blue lighten-1",
    7: "light-blue lighten-1",
    8: "blue-grey lighten-1",
    9: "brown darken-1",
    10: "purple accent-1",
    11: "orange lighten-2",
    12: "lime lighten-3",
}
//фактически не отображается 1 - прозрачный цвет, если просто все ок
export const errorEventsColor = {
    2: "yellow darken-3",
    3: "red darken-2",
    4: "yellow darken-3",
    0: "red darken-2",
}