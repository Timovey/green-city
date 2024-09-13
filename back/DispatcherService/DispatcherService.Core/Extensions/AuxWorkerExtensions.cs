using GreenCity.CommonUtils.CommonExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherService.Core.Extensions
{
    internal static class AuxWorkerExtensions
    {
        internal static string GetFio(this (string Surname, string Name, string Patronymic) fio)
        {
            if (fio == default || fio.Surname.IsNullOrEmpty())
                return string.Empty;

            var sbFio = new StringBuilder(fio.Surname.Trim());

            if (!fio.Name.IsNullOrEmpty())
            {
                sbFio.Append($" {fio.Name.FirstOrDefault()}.");
            }

            if (!fio.Patronymic.IsNullOrEmpty())
            {
                sbFio.Append($" {fio.Patronymic.FirstOrDefault()}.");
            }

            return sbFio.ToString();
        }
    }
}
