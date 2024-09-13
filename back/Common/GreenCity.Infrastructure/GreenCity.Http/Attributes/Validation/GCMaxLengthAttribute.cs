using System.ComponentModel.DataAnnotations;

namespace GreenCity.Http.Attributes.Validation
{
    public class GCMaxLengthAttribute : MaxLengthAttribute
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="length"> Максимальная длина </param>
        /// <param name="fieldName"> Пользовательское название поля </param>
        /// <param name="errorMessage"> Текст ошибки (полностью перепишет стандартный текст ошибки) </param>
        public GCMaxLengthAttribute(int length, string fieldName = "Неуказанное поле", string errorMessage = default)
            : base(length)
        {
            ErrorMessage = errorMessage == default ?
                $"Предельная длина значения поля *{fieldName}* - {length}" :
                errorMessage;
        }
    }
}
