using AutoMapper;
using DataGenerationService.Contracts.DataContracts.Sd;
using DataGenerationService.Core.Helpers;
using OfficeOpenXml;
using SDContracts.DataContracts.Container;
using SDService.Database.Interfaces;

namespace DataGenerationService.Core.BusinessLogic
{
    public class GeneratingContainerLogic
    {
        private readonly IContainerStorage _containerStorage;
        private readonly IMapper _mapper;

        public GeneratingContainerLogic(
            IContainerStorage containerStorage,
            IMapper mapper
        )
        {
            _containerStorage = containerStorage;
            _mapper = mapper;
        }

        public async Task GenerateContainersAsync()
        {
            var containerContracts = new List<ReadContainerContract>();

            var file = new FileInfo(Consts.Folder + Consts.ContainersFilename);

            List<CreateContainerContract> createContainerContracts;

            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    var name = worksheet.Cells[row, 2].Value.ToString();
                    var container = new ReadContainerContract
                    {
                        Number =
                            int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var number) ? number : default,
                        Name = !name.Equals("*") ? name : null,
                        ContainerTypeNumber =
                            int.TryParse(worksheet.Cells[row, 3].Value.ToString(), out var containerTypeNumber)
                                ? containerTypeNumber
                                : default,
                        TrashType =
                            byte.TryParse(worksheet.Cells[row, 4].Value.ToString(), out var trashType)
                                ? trashType
                                : default,
                        ContainerStandNumber =
                            int.TryParse(worksheet.Cells[row, 5].Value.ToString(), out var containerStandNumber)
                                ? containerStandNumber
                                : null,
                        CreatedUserNumber =
                            int.TryParse(worksheet.Cells[row, 6].Value.ToString(), out var createdUserNumber)
                                ? createdUserNumber
                                : default
                    };
                    containerContracts.Add(container);
                }
            }

            createContainerContracts = containerContracts.Select(_mapper.Map<CreateContainerContract>).ToList();

            foreach (var container in createContainerContracts)
            {
                try
                {
                    var id = await _containerStorage.CreateContainerAsync(container);
                    Console.WriteLine($"Добавлен контейнер с id {id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}