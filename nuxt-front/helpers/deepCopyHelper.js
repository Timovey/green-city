export const deepCopy = (item) => {
    return JSON.parse(JSON.stringify(item))
}