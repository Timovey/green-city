export const toArray = (obj) => {
    return Object.entries({ ...obj  })
    .filter(([key, value]) => {
        return key != 0
    })
    .map(([key, value]) => {
        return {
            "name": value,
            "value": +key
        }
    })
}