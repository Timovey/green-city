export const getDriverInfo = (item) => {
    let patr = item.driverPatronymic ? item.driverPatronymic[0] + '.' : ''
    let fio = item.driverSurname + ' ' + item.driverName[0] + '. ' + patr
    return fio
}
export const addFio = (item) => {
    let patr = item.driverPatronymic ? item.driverPatronymic[0] + '.' : ''
    item.driverFio = item.driverSurname + ' ' + item.driverName[0] + '. ' + patr
    return item
}
export const addReplacementFio = (item) => {
    if(item.replacementDriverId) {
        let patr = item.replacementDriverPatronymic ? item.replacementDriverPatronymic[0] + '.' : ''
        item.replacementDriverFio = item.replacementDriverSurname + ' ' + item.replacementDriverName[0] + '. ' + patr
    }
    else {
        item.replacementDriverFio = '-'
    }
    return item
}