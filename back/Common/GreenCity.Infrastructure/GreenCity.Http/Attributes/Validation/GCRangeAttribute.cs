using System.ComponentModel.DataAnnotations;

namespace GreenCity.Http.Attributes.Validation
{
    public class GCRangeAttribute : RangeAttribute
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="from"> Значение От диапазона </param>
        /// <param name="to"> Значение До диапазона </param>
        /// <param name="fieldName"> Пользовательское название поля </param>
        /// <param name="errorMessage"> Текст ошибки (полностью перепишет стандартный текст ошибки) </param>
        public GCRangeAttribute(int from, int to, string fieldName = "Неуказанное поле", string errorMessage = default)
            : base(from, to)
        {
            ErrorMessage = errorMessage == default ?
                $"Значение поля *{fieldName}* должно находится в диапазоне от {from} до {to}" :
                errorMessage;
        }
    }
}
