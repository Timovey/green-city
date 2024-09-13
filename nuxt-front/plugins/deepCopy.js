export default ({ app }, inject) => {
  inject("deepCopy", function (item) {
    return JSON.parse(JSON.stringify(item));
  });
};
