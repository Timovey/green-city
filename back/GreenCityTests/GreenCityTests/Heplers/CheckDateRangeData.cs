using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportContracts.DataContracts.Enums;
using TransportContracts.ViewModels;

namespace GreenCityTests.Heplers
{
    public static class CheckDateRangeData
    {
        public static List<Dictionary<int, DriverWorkDateType>> GetFirstTrasnportDateRangeOrg1__DriverShedules(List<int> driverIds)
        {
            return new List<Dictionary<int, DriverWorkDateType>> {
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[0], DriverWorkDateType.WorkThreeThree }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[0], DriverWorkDateType.Unknown }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[0], DriverWorkDateType.Unknown }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[0], DriverWorkDateType.Unknown },
                        { driverIds[1], DriverWorkDateType.Unknown }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[0], DriverWorkDateType.NotWork },
                        { driverIds[1], DriverWorkDateType.ToRepair },
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[1], DriverWorkDateType.WorkThreeThree },
                        { driverIds[0], DriverWorkDateType.NotWork }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[1], DriverWorkDateType.NotWork },
                        { driverIds[0], DriverWorkDateType.WorkThreeThree }
                },
            };
        }

        public static List<TransportWorkDateType> GetFirstTrasnportDateRangeOrg1__TransportShedules()
        {
            return new List<TransportWorkDateType> {
                TransportWorkDateType.WorkThreeThree,
                TransportWorkDateType.Rent,
                TransportWorkDateType.Rent,
                TransportWorkDateType.Rent,
                TransportWorkDateType.RepairAll,
                TransportWorkDateType.WorkThreeThree,
                TransportWorkDateType.WorkThreeThree,
            };
        }

        public static List<Dictionary<int, DriverWorkDateType>> GetFirstTrasnportDateRangeOrg2__DriverShedules(List<int> driverIds)
        {
            return new List<Dictionary<int, DriverWorkDateType>> {
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[0], DriverWorkDateType.Unknown }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[0], DriverWorkDateType.WorkThreeThree }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[0], DriverWorkDateType.WorkThreeThree }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[0], DriverWorkDateType.NotWork },
                        { driverIds[1], DriverWorkDateType.WorkThreeThree }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[0], DriverWorkDateType.Unknown },
                        { driverIds[1], DriverWorkDateType.Unknown },
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[1], DriverWorkDateType.Unknown },
                        { driverIds[0], DriverWorkDateType.Unknown }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                        { driverIds[1], DriverWorkDateType.Unknown },
                        { driverIds[0], DriverWorkDateType.Unknown }
                },
            };
        }

        public static List<TransportWorkDateType> GetFirstTrasnportDateRangeOrg2__TransportShedules()
        {
            return new List<TransportWorkDateType> {
                TransportWorkDateType.Unknown,
                TransportWorkDateType.WorkThreeThree,
                TransportWorkDateType.WorkThreeThree,
                TransportWorkDateType.WorkThreeThree,
                TransportWorkDateType.Unknown,
                TransportWorkDateType.Unknown,
                TransportWorkDateType.Unknown,
            };
        }

        public static List<Dictionary<int, DriverWorkDateType>> GetSecondTrasnportDateRange__DriverShedules(List<int> driverIds)
        {
            return new List<Dictionary<int, DriverWorkDateType>> {
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[2], DriverWorkDateType.WorkFiveTwo }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[2], DriverWorkDateType.WorkFiveTwo }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[2], DriverWorkDateType.Unknown }
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[2], DriverWorkDateType.Unknown },
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[2], DriverWorkDateType.Unknown },
                },
                new Dictionary<int, DriverWorkDateType>
                {
                    { driverIds[2], DriverWorkDateType.Unknown },
                },
                new Dictionary<int, DriverWorkDateType>
                {
                     { driverIds[2], DriverWorkDateType.WorkFiveTwo },
                },
            };
        }

        public static List<TransportWorkDateType> GetSecondTrasnportDateRange__TransportShedules()
        {
            return new List<TransportWorkDateType> {
                TransportWorkDateType.DutyShift,
                TransportWorkDateType.WorkFiveTwo,
                TransportWorkDateType.Rent,
                TransportWorkDateType.Rent,
                TransportWorkDateType.Rent,
                TransportWorkDateType.Rent,
                TransportWorkDateType.WorkFiveTwo,
            };
        }

        public static List<DateTime> GetWorkshiftsDates()
        {
            return new List<DateTime> {

                DateTime.Parse("2023-03-01"),
                DateTime.Parse("2023-03-02"),
                DateTime.Parse("2023-03-03"),
                DateTime.Parse("2023-03-04"),
                DateTime.Parse("2023-03-05"),
                DateTime.Parse("2023-03-06"),
                DateTime.Parse("2023-03-07")
            };
        }

        public static Dictionary<DateTime, IList<TransportWorkShiftViewModel>> GetFirstTrasnportAllWorkshifts(List<int> transportIds)
        {
            var threeThreeList = new List<TransportWorkShiftViewModel>{
                        new TransportWorkShiftViewModel
                        {
                            TransportId = transportIds[0],
                            DriverWorkDateType = DriverWorkDateType.WorkThreeThree
                        }
                    };

            var emptyList = new List<TransportWorkShiftViewModel>();
            return new Dictionary<DateTime, IList<TransportWorkShiftViewModel>>
            {
                {
                    DateTime.Parse("2023-03-01"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-02"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-03"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-04"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-05"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-06"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-07"),
                    threeThreeList
                },
            };
        }

        public static Dictionary<DateTime, IList<TransportWorkShiftViewModel>> GetFirstTrasnportWorkshiftsOrg1(List<int> transportIds)
        {
            var threeThreeList = new List<TransportWorkShiftViewModel>{
                        new TransportWorkShiftViewModel
                        {
                            TransportId = transportIds[0],
                            DriverWorkDateType = DriverWorkDateType.WorkThreeThree
                        }
                    };
            var emptyList = new List<TransportWorkShiftViewModel>();
            return new Dictionary<DateTime, IList<TransportWorkShiftViewModel>>
            {
                {
                    DateTime.Parse("2023-03-01"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-02"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-03"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-04"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-05"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-06"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-07"),
                    threeThreeList
                },
            };
        }

        public static Dictionary<DateTime, IList<TransportWorkShiftViewModel>> GetFirstTrasnportWorkshiftsOrg2(List<int> transportIds)
        {
            var threeThreeList = new List<TransportWorkShiftViewModel>{
                        new TransportWorkShiftViewModel
                        {
                            TransportId = transportIds[0],
                            DriverWorkDateType = DriverWorkDateType.WorkThreeThree
                        }
                    };
            var emptyList = new List<TransportWorkShiftViewModel>();
            return new Dictionary<DateTime, IList<TransportWorkShiftViewModel>>
            {
                {
                    DateTime.Parse("2023-03-01"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-02"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-03"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-04"),
                    threeThreeList
                },
                {
                    DateTime.Parse("2023-03-05"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-06"),
                    emptyList
                },
                {
                    DateTime.Parse("2023-03-07"),
                    emptyList
                },
            };
        }

    }
}
