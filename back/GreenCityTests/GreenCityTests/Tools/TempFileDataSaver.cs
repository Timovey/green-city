using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreenCityTests.Tools
{
    public class TempFileDataSaver
    {
        public static async Task<T> ReadJsonFromFileAsync<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName + ".json");
                return default(T);
            }

            fileName = fileName + ".json";

            using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);

                return JsonSerializer.Deserialize<T>(textFromFile);
            }
        }

        public static async Task WriteJsonToFileAsync<T>(string fileName, T data)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName + ".json");
            }
            else
            {
                File.Delete(fileName);
            }

            using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var json = JsonSerializer.Serialize(data);
                // преобразуем строку в байты
                byte[] buffer = Encoding.Default.GetBytes(json);
                // запись массива байтов в файл
                await fstream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
