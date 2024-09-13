using System.ComponentModel.DataAnnotations;

namespace GreenCity.Http.Attributes.Validation
{
    public class GCRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fieldName"> Пользовательское название поля </param>
        /// <param name="errorMessage"> Текст ошибки (полностью перепишет стандартный текст ошибки) </param>
        public GCRequiredAttribute(string fieldName = "Неуказанное поле", string errorMessage = default)
        {
            ErrorMessage = errorMessage == default ?
                $"Поле *{fieldName}* обязательно к заполнению" :
                errorMessage;
        }
    }
}
