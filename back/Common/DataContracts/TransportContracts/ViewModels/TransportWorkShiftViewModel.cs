using System;
using System.Collections.Generic;
using TransportContracts.DataContracts.Enums;
using TransportContracts.DataContracts.TransportWorkShift;

namespace TransportContracts.ViewModels
{
    public class TransportWorkShiftViewModel : IEquatable<TransportWorkShiftViewModel>, IEquatable<CreateTransportWorkShiftContract>
    {
        /// <summary>
        /// ИД обновляемой сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID транспорта
        /// </summary>
        public int TransportId { get; set; }

        /// <summary>
        /// Название транспортного средства
        /// </summary>
        public string TransportName { get; set; }

        /// <summary> ИД Организации транспорта</summary>
        public int? TransportOrganizationId { get; set; }

        /// <summary> Тип транспорта</summary>
        public TransportType TransportType { get; set; }

        /// <summary> Регистрационный номер </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Тип бака (тип тары из Абонентского отдела)
        /// </summary>
        public IList<int> TankTypes { get; set; }

        /// <summary>
        /// Дата выхода
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Список организаций, которым доступна рабочая смена
        /// </summary>
        public IList<int> OrganizationIds { get; set; }


        /// <summary>
        /// Id водителя
        /// </summary>
        public int DriverId { get; set; }

        public string DriverFio { get; set; }

        /// <summary>
        /// Смена водителя
        /// </summary>
        public DriverWorkDateType DriverWorkDateType { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TransportWorkShiftViewModel);
        }

        public bool Equals(TransportWorkShiftViewModel other)
        {
            return other != null && 
                TransportId == other.TransportId && 
                DateTime == other.DateTime && 
                DriverId == other.DriverId && 
                DriverWorkDateType == other.DriverWorkDateType;
        }

        public bool Equals(CreateTransportWorkShiftContract other)
        {
            return other != null &&
               TransportId == other.TransportId &&
               DateTime == other.DateTime &&
               DriverId == other.DriverId &&
               DriverWorkDateType == other.DriverWorkDateType;
        }
    }
}
