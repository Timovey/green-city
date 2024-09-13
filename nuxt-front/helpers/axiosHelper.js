import { getItem } from "@/helpers/persistanceStorage";
export const post = (context, url, data) => {
  return new Promise((resolve, reject) => {
    let user = getItem("user");
    if (!user) {
      // reject("Пройдите авторизацию")
      context.$router.push("/login");
    }
    let userName = user.userName;
    let userId = user.userId;
    let token = getItem("Token");
    if (!token || !userName || !userId) {
      // reject("Пройдите авторизацию")
      context.$router.push("/login");
    }
    data = { ...data, userId, userName };
    // console.log('data',data);
    context.$axios
      .post(url, data, {
        headers: {
          Authorization: token,
        },
      })
      .then((res) => {
        if (!res.data.errors) {
          if (res.status === 200) {
            //приходит массив, последний элемент null
            if (Array.isArray(res.data.data)) {
              res.data.data = res.data.data.filter((el) => {
                return el != null;
              });
            }
            return resolve(res);
          } else if (res.status === 401) {
            // reject("Пройдите авторизацию")
            context.$router.push("/login");
          } else if (res.status === 404) {
            reject("Не найдено");
          } else {
            return reject("Непредвиденная ошибка");
          }
        } else {
          if (res.data.statusCode === 500) {
            if (res.data.errors[1]) {
              return reject(res.data.errors[1]);
            }
            return reject("Ошибка сервера");
          } else if (res.data.statusCode === 404) {
            if (res.data.errors[1]) {
              return reject(res.data.errors[1]);
            }
            reject("Не найдено");
          }
          return reject(res.data.errors[0]);
        }
      })
      .catch((err) => {
        if (err?.response?.status === 401) {
          context.$router.push("/login");
          // return reject("Пройдите авторизацию");
        } else if (err?.response?.status === 404) {
          return reject("Не найдено");
        } else {
          return reject("Непредвиденная ошибка");
        }
      });
  });
};
