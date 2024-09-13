using AutoMapper;
using DataGenerationService.Contracts.DataContracts.Sd;
using DataGenerationService.Core.Helpers;
using OfficeOpenXml;
using SDContracts.DataContracts.Stand;
using SDService.Database.Interfaces;

namespace DataGenerationService.Core.BusinessLogic
{
    public class GeneratingStandLogic
    {
        private readonly IStandStorage _standStorage;
        private readonly IMapper _mapper;

        public GeneratingStandLogic(
            IStandStorage standStorage,
            IMapper mapper
        )
        {
            _standStorage = standStorage;
            _mapper = mapper;
        }

        public async Task GenerateStandsAsync()
        {
            var standContracts = new List<ReadStandContract>();

            var file = new FileInfo(Consts.Folder + Consts.StandsFilename);

            List<CreateStandContract> createStandContracts;

            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    var attachedContainerIdsStr = worksheet.Cells[row, 14].Value.ToString();
                    var buff = attachedContainerIdsStr.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var attachedContainerIds = buff.Select(int.Parse).ToList();
                    var address = worksheet.Cells[row, 3].Value.ToString();
                    var driveWay = worksheet.Cells[row, 7].Value.ToString();
                    var contacts = worksheet.Cells[row, 8].Value.ToString();
                    var stand = new ReadStandContract
                    {
                        Number =
                            int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var number) ? number : default,
                        IsHighSecurityObject =
                            StringComparer.CurrentCultureIgnoreCase.Equals(worksheet.Cells[row, 2].Value.ToString(),
                                "да"),
                        Address = !address.Equals("*") ? address : null,
                        Latitude =
                            decimal.TryParse(worksheet.Cells[row, 4].Value.ToString().Replace('.', ','),
                                out var latitude)
                                ? latitude
                                : default,
                        Longitude =
                            decimal.TryParse(worksheet.Cells[row, 5].Value.ToString().Replace('.', ','),
                                out var longitude)
                                ? longitude
                                : default,
                        CoordinatesComment = worksheet.Cells[row, 6].Value.ToString(),
                        DriveWay = !driveWay.Equals("*") ? driveWay : null,
                        Contacts = !contacts.Equals("*") ? contacts : null,
                        StandType =
                            byte.TryParse(worksheet.Cells[row, 9].Value.ToString(), out var standType)
                                ? standType
                                : default,
                        HaveBase =
                            StringComparer.CurrentCultureIgnoreCase.Equals(worksheet.Cells[row, 10].Value.ToString(),
                                "да"),
                        HaveRoof =
                            StringComparer.CurrentCultureIgnoreCase.Equals(worksheet.Cells[row, 11].Value.ToString(),
                                "да"),
                        HaveFence =
                            StringComparer.CurrentCultureIgnoreCase.Equals(worksheet.Cells[row, 12].Value.ToString(),
                                "да"),
                        CreatedUserNumber =
                            int.TryParse(worksheet.Cells[row, 13].Value.ToString(), out var createdUserNumber)
                                ? createdUserNumber
                                : default,
                        AttachedContainerIds = attachedContainerIds
                    };
                    standContracts.Add(stand);
                }
            }

            createStandContracts = standContracts.Select(_mapper.Map<CreateStandContract>).ToList();

            foreach (var stand in createStandContracts)
            {
                try
                {
                    var id = await _standStorage.CreateStandAsync(stand);
                    Console.WriteLine($"Добавлена кп с id {id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}