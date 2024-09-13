import Swal from "sweetalert2";

export function showError(err = "Проверьте данные") {
    Swal.fire("Произошла ошибка", err, "error");
}
export function showSuccess() {
    Swal.fire("Успех!", "Запрос выполнен успешно", "success");
}
export function showErrorMessage(text, title = "Ошибка") {
    Swal.fire({
        icon: "error",
        title: title,
        text: text,
    });
}
