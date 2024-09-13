using AutoMapper;
using DataGenerationService.Contracts.DataContracts.Sd;
using DataGenerationService.Core.Helpers;
using OfficeOpenXml;

namespace DataGenerationService.Core.BusinessLogic
{
    public class GeneratingContract2StandLogic
    {
        private readonly IMapper _mapper;

        public GeneratingContract2StandLogic(
            IMapper mapper
        )
        {
            _mapper = mapper;
        }

        public async Task GenerateContract2StandsAsync()
        {
            var contract2StandContracts = new List<ReadContract2StandContract>();

            var file = new FileInfo(Consts.Folder + Consts.Contract2StandsFilename);

            //List<Contract2Stand> contract2Stands;

            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    var contract2Stand = new ReadContract2StandContract
                    {
                        ContractNumber =
                            int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var contractNumber)
                                ? contractNumber
                                : default,
                        StandNumber =
                            int.TryParse(worksheet.Cells[row, 2].Value.ToString(), out var standNumber)
                                ? standNumber
                                : default,
                        StartDate =
                            DateTime.TryParse(worksheet.Cells[row, 3].Value.ToString(), out var startDate)
                                ? startDate
                                : default,
                        EndDate =
                            DateTime.TryParse(worksheet.Cells[row, 4].Value.ToString(), out var endDate)
                                ? endDate
                                : default
                    };
                    contract2StandContracts.Add(contract2Stand);
                }
            }
            //contract2Stands = contract2StandContracts.Select(_mapper.Map<Contract2Stand>).ToList();
        }
    }
}