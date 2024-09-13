import { addFio } from "@/helpers/driverHelper";

export default ({ $post }) => ({
  fetch(filter) {
    filter.isFilterByCreatedUser = false;
    filter.isFilter = false;
    filter.sorting = {};
    return new Promise((resolve, reject) => {
      $post("/api/Transport/GetFilteredDriver", filter)
        .then((res) => resolve(res.data.data.map((el) => addFio(el))))
        .catch((err) => reject(err));
    });
  },
});
