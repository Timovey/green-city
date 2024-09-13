import { saveAs } from "file-saver";
import { getItem } from "@/helpers/persistanceStorage";

async function throwErrorMessage(response) {
  const res = await response.json();
  if (Array.isArray(res)) {
    console.log("Ошибка при печати", res);
    return res
      .map((el) => {
        if (el.errors) {
          return el.errors.map((er) => er.errorMessage ?? "").join(", ");
        } else {
          return "";
        }
      })
      .join(", ");
  }
  return "Ошибка при формировании документа";
}
export default (context) => ({
  async printShiftTask(data) {
    const baseUrl = context.$axios.defaults.baseURL;
    const headers = {
      "Content-Type": "application/json;charset=utf-8",
      Authorization: String(token),
    };
    let token = getItem("Token");

    const url = `${baseUrl}api/Report/GetShiftTaskFile`;
    const response = await fetch(url, {
      method: "POST",
      headers,
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(await throwErrorMessage(response));
    }

    const blob = await response.blob();
    saveAs(blob, `Сменное_Задание-${data.id}.xlsx`);
  },
  async printTravelSheet(data) {
    const baseUrl = context.$axios.defaults.baseURL;
    const headers = {
      "Content-Type": "application/json;charset=utf-8",
      Authorization: String(token),
    };
    let token = getItem("Token");

    const url = `${baseUrl}api/Report/GetTravelSheetFile`;
    const response = await fetch(url, {
      method: "POST",
      headers,
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(await throwErrorMessage(response));
    }

    const blob = await response.blob();
    saveAs(blob, `Путевой_Лист-${data.id}.docx`);
  },
  async printDoneTaskList(data) {
    const baseUrl = context.$axios.defaults.baseURL;
    const headers = {
      "Content-Type": "application/json;charset=utf-8",
      Authorization: String(token),
    };
    let token = getItem("Token");

    const url = `${baseUrl}api/Report/GetDoneTaskListFile`;
    const response = await fetch(url, {
      method: "POST",
      headers,
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(await throwErrorMessage(response));
    }

    const blob = await response.blob();
    saveAs(
      blob,
      `Движение_транспортных_средств_${data.dateFrom}-${data.dateTo}.xlsx`
    );
  },
});
