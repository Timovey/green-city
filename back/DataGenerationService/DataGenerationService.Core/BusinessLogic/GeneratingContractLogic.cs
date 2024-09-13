using AutoMapper;
using DataGenerationService.Contracts.DataContracts.Sd;
using DataGenerationService.Core.Helpers;
using OfficeOpenXml;
using SDContracts.DataContracts.Contract;
using SDService.Database.Interfaces;

namespace DataGenerationService.Core.BusinessLogic
{
    public class GeneratingContractLogic
    {
        private readonly IContractStorage _contractStorage;
        private readonly IMapper _mapper;

        public GeneratingContractLogic(
            IContractStorage contractStorage,
            IMapper mapper
        )
        {
            _contractStorage = contractStorage;
            _mapper = mapper;
        }

        public async Task GenerateContractsAsync()
        {
            var contractContracts = new List<ReadContractContract>();

            var file = new FileInfo(Consts.Folder + Consts.ContractsFilename);

            List<CreateContractContract> createContractContracts;

            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    var comment = worksheet.Cells[row, 8].Value.ToString();
                    var contract = new ReadContractContract
                    {
                        Number =
                            int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var number) ? number : default,
                        ContractClientNumber =
                            int.TryParse(worksheet.Cells[row, 2].Value.ToString(), out var contractClientNumber)
                                ? contractClientNumber
                                : null,
                        ContractStatus =
                            StringComparer.CurrentCultureIgnoreCase.Equals(worksheet.Cells[row, 3].Value.ToString(),
                                "да"),
                        CreateDate =
                            DateTime.TryParse(worksheet.Cells[row, 4].Value.ToString(), out var createDate)
                                ? createDate
                                : default,
                        StartDate =
                            DateTime.TryParse(worksheet.Cells[row, 5].Value.ToString(), out var startDate)
                                ? startDate
                                : default,
                        EndDate =
                            DateTime.TryParse(worksheet.Cells[row, 6].Value.ToString(), out var endDate)
                                ? endDate
                                : null,
                        SendDocsType = worksheet.Cells[row, 7].Value.ToString(),
                        Comment = !comment.Equals("*") ? comment : null,
                        IsAutorenewSchedules =
                            StringComparer.CurrentCultureIgnoreCase.Equals(worksheet.Cells[row, 9].Value.ToString(),
                                "да"),
                        CreatedUserNumber =
                            int.TryParse(worksheet.Cells[row, 10].Value.ToString(), out var createdUserNumber)
                                ? createdUserNumber
                                : default
                    };
                    contractContracts.Add(contract);
                }
            }

            createContractContracts = contractContracts.Select(_mapper.Map<CreateContractContract>).ToList();

            foreach (var contract in createContractContracts)
            {
                try
                {
                    var id = await _contractStorage.CreateContractAsync(contract);
                    Console.WriteLine($"Добавлен договор с id {id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}