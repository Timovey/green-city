using System.Collections.Generic;
using TransportContracts.DataContracts.Transport;

namespace TransportService.Core.Extensions
{
    public static class TransportExtensions
    {
        public static List<string> Validate(this CreateTransportContract contract)
        {
            var errors = new List<string>();

            if (contract == null)
            {
                errors.Add("Пустой запрос");
                return errors;
            }

            //--1
            if (string.IsNullOrEmpty(contract.Name))
            {
                errors.Add($"Поле {nameof(contract.Name)} не заполнено");
            }
            else
            {
                if (contract.Name.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.Name)} слишком длинное максимум 100");
                }
            }

            //--2
            if (contract.OrganizationId == 0)
            {
                errors.Add($"Поле {nameof(contract.OrganizationId)} не заполнено");
            }

            //--3
            if (string.IsNullOrEmpty(contract.RegNumber))
            {
                errors.Add($"Поле {nameof(contract.RegNumber)} не заполнено");
            }
            else
            {
                if (contract.RegNumber.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.RegNumber)} слишком длинное максимум 32");
                }
            }

            //--4
            if (string.IsNullOrEmpty(contract.Sts))
            {
                errors.Add($"Поле {nameof(contract.Sts)} не заполнено");
            }
            else
            {
                if (contract.RegNumber.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.Sts)} слишком длинное максимум 32");
                }
            }

            //--5
            if (string.IsNullOrEmpty(contract.TransportType))
            {
                errors.Add($"Поле {nameof(contract.TransportType)} не заполнено");
            }
            else
            {
                if (contract.TransportType.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.TransportType)} слишком длинное максимум 100");
                }
            }

            //--6
            if (string.IsNullOrEmpty(contract.BodyType))
            {
                errors.Add($"Поле {nameof(contract.BodyType)} не заполнено");
            }
            else
            {
                if (contract.BodyType.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.BodyType)} слишком длинное максимум 100");
                }
            }

            //--7
            if (string.IsNullOrEmpty(contract.BodyColor))
            {
                errors.Add($"Поле {nameof(contract.BodyColor)} не заполнено");
            }
            else
            {
                if (contract.BodyColor.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.BodyColor)} слишком длинное максимум 100");
                }
            }

            //--8
            if (string.IsNullOrEmpty(contract.TankFence))
            {
                errors.Add($"Поле {nameof(contract.TankFence)} не заполнено");
            }
            else
            {
                if (contract.TankFence.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.TankFence)} слишком длинное максимум 100");
                }
            }

            //--9
            if (contract.TankType == TransportContracts.DataContracts.Enums.TankType.Unknown)
            {
                errors.Add($"Неизвестный тип бака");
            }

            //--10
            if (string.IsNullOrEmpty(contract.LoadCapacity))
            {
                errors.Add($"Поле {nameof(contract.LoadCapacity)} не заполнено");
            }
            else
            {
                if (contract.LoadCapacity.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.LoadCapacity)} слишком длинное максимум 32");
                }
            }

            //--11
            if (contract.CargoVolume == 0)
            {
                errors.Add($"Поле {nameof(contract.CargoVolume)} не может быть 0");
            }

            //--12
            if (string.IsNullOrEmpty(contract.CargoVolumeCoef))
            {
                errors.Add($"Поле {nameof(contract.CargoVolumeCoef)} не заполнено");
            }
            else
            {
                if (contract.CargoVolumeCoef.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.CargoVolumeCoef)} слишком длинное максимум 32");
                }
            }

            //--13
            if (contract.ProductionYear == 0)
            {
                errors.Add($"Поле {nameof(contract.ProductionYear)} не заполнено");
            }

            //--14
            if (string.IsNullOrEmpty(contract.Se))
            {
                errors.Add($"Поле {nameof(contract.Se)} не заполнено");
            }
            else
            {
                if (contract.Se.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.Se)} слишком длинное максимум 32");
                }
            }

            return errors;
        }

        public static List<string> Validate(this UpdateTransportContract contract)
        {
            var errors = new List<string>();

            if (contract == null)
            {
                errors.Add("Пустой запрос");
                return errors;
            }

            //--1
            if (string.IsNullOrEmpty(contract.Name))
            {
                errors.Add($"Поле {nameof(contract.Name)} не заполнено");
            }
            else
            {
                if (contract.Name.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.Name)} слишком длинное максимум 100");
                }
            }

            //--2
            if (contract.OrganizationId == 0)
            {
                errors.Add($"Поле {nameof(contract.OrganizationId)} не заполнено");
            }

            //--3
            if (string.IsNullOrEmpty(contract.RegNumber))
            {
                errors.Add($"Поле {nameof(contract.RegNumber)} не заполнено");
            }
            else
            {
                if (contract.RegNumber.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.RegNumber)} слишком длинное максимум 32");
                }
            }

            //--4
            if (string.IsNullOrEmpty(contract.Sts))
            {
                errors.Add($"Поле {nameof(contract.Sts)} не заполнено");
            }
            else
            {
                if (contract.Sts.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.Sts)} слишком длинное максимум 32");
                }
            }

            //--5
            if (string.IsNullOrEmpty(contract.TransportType))
            {
                errors.Add($"Поле {nameof(contract.TransportType)} не заполнено");
            }
            else
            {
                if (contract.TransportType.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.TransportType)} слишком длинное максимум 100");
                }
            }

            //--6
            if (string.IsNullOrEmpty(contract.BodyType))
            {
                errors.Add($"Поле {nameof(contract.BodyType)} не заполнено");
            }
            else
            {
                if (contract.BodyType.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.BodyType)} слишком длинное максимум 100");
                }
            }

            //--7
            if (string.IsNullOrEmpty(contract.BodyColor))
            {
                errors.Add($"Поле {nameof(contract.BodyColor)} не заполнено");
            }
            else
            {
                if (contract.BodyColor.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.BodyColor)} слишком длинное максимум 100");
                }
            }

            //--8
            if (string.IsNullOrEmpty(contract.TankFence))
            {
                errors.Add($"Поле {nameof(contract.TankFence)} не заполнено");
            }
            else
            {
                if (contract.TankFence.Length > 100)
                {
                    errors.Add($"Поле {nameof(contract.TankFence)} слишком длинное максимум 100");
                }
            }

            //--9
            if (contract.TankType == TransportContracts.DataContracts.Enums.TankType.Unknown)
            {
                errors.Add($"Неизвестный тип бака");
            }

            //--10
            if (string.IsNullOrEmpty(contract.LoadCapacity))
            {
                errors.Add($"Поле {nameof(contract.LoadCapacity)} не заполнено");
            }
            else
            {
                if (contract.LoadCapacity.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.LoadCapacity)} слишком длинное максимум 32");
                }
            }

            //--11
            if (contract.CargoVolume == 0)
            {
                errors.Add($"Поле {nameof(contract.CargoVolume)} не может быть 0");
            }

            //--12
            if (string.IsNullOrEmpty(contract.CargoVolumeCoef))
            {
                errors.Add($"Поле {nameof(contract.CargoVolumeCoef)} не заполнено");
            }
            else
            {
                if (contract.CargoVolumeCoef.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.CargoVolumeCoef)} слишком длинное максимум 32");
                }
            }

            //--13
            if (contract.ProductionYear == 0)
            {
                errors.Add($"Поле {nameof(contract.ProductionYear)} не заполнено");
            }

            //--14
            if (string.IsNullOrEmpty(contract.Se))
            {
                errors.Add($"Поле {nameof(contract.Se)} не заполнено");
            }
            else
            {
                if (contract.Se.Length > 32)
                {
                    errors.Add($"Поле {nameof(contract.Se)} слишком длинное максимум 32");
                }
            }

            return errors;
        }
    }
}
