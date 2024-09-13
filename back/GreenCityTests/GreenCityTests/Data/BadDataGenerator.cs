using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenCityTests.Data
{
    public class BadDataGenerator
    {
        public static string GetVeryBigString(int stringLen)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < stringLen; i++)
            {
                stringBuilder.AppendLine("X");
            }

            return stringBuilder.ToString();
        }
    }
}
