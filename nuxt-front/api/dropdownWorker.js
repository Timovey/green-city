export default ({ $post }) => ({
  fetchContainers(filter = {}) {
    return new Promise((resolve, reject) => {
      $post("/api/Sd/GetDropDownContainers", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchContragents(filter = {}) {
    return new Promise((resolve, reject) => {
      $post("/api/Sd/GetDropDownContractClientV2", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchContracts(filter = {}) {
    return new Promise((resolve, reject) => {
      $post("/api/Sd/GetDropDownContracts", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchWorkhifts(filter = {}) {
    filter.isFilterByUser = false;
    return new Promise((resolve, reject) => {
      $post("/api/Transport/GetWorkShiftsDropdown", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchBms(filter = {}) {
    filter.isFilterByCreatedUser = false;
    return new Promise((resolve, reject) => {
      $post("/api/Logist/GetDropdownBaseRoutes", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
  fetchStands(filter = {}) {
    filter.isFilterByCreatedUser = false;
    return new Promise((resolve, reject) => {
      $post("/api/Sd/GetDropDownStands", filter)
        .then((res) => resolve(res.data.data))
        .catch((err) => reject(err));
    });
  },
});
