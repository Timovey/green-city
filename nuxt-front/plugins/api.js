import api from "@/api/index.js";
export default (context, inject) => {
  // Initialize API factories
  const factories = Object.create(null);

  Object.entries(api).forEach(([name, func]) => {
    factories[name] = func(context);
  });
  inject("api", factories);
};
