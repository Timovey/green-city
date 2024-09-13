using AutoMapper;
using DataGenerationService.Contracts.DataContracts.Sd;
using DataGenerationService.Core.Helpers;
using SDService.Database.Models.DbModels;

namespace DataGenerationService.Core.BusinessLogic
{
    public class GeneratingContainerTypeLogic
    {
        private readonly IMapper _mapper;

        public GeneratingContainerTypeLogic(
            IMapper mapper
        )
        {
            _mapper = mapper;
        }

        public async Task<IList<ContainerType>> GenerateContainerTypesAsync()
        {
            var containerTypeContracts = new List<ReadContainerTypeContract>();

            var file = new FileInfo(Consts.Folder + Consts.ContainerTypesFilename);

            var containerTypes = new List<ContainerType>
            {
                new()
                {
                    Name = "ПМК 0.75",
                    Volume = (decimal)0.75
                },
                new()
                {
                    Name = "ЕК 0.36",
                    Volume = (decimal)0.36
                },
                new()
                {
                    Name = "ЕК 0.24",
                    Volume = (decimal)0.24
                },
                new()
                {
                    Name = "Бункер 4м3",
                    Volume = (decimal)4.0
                },
                new()
                {
                    Name = "Бункер 8м3",
                    Volume = (decimal)8.0
                },
                new()
                {
                    Name = "ПЛАСТИК ЕК 0.77",
                    Volume = (decimal)0.77
                },
                new()
                {
                    Name = "ПЛАСТИК ЕК 1.1",
                    Volume = (decimal)1.1
                },
                new()
                {
                    Name = "ПЛАСТИК ЕК 1.2",
                    Volume = (decimal)1.2
                },
                new()
                {
                    Name = "ПЛАСТИК ЕК 0.66",
                    Volume = (decimal)0.66
                },
                new()
                {
                    Name = "ЦИНК 0.77",
                    Volume = (decimal)0.77
                },
                new()
                {
                    Name = "ЦИНК 1.1",
                    Volume = (decimal)1.1
                }
            };

            // using (var package = new ExcelPackage(file))
            // {
            //     var worksheet = package.Workbook.Worksheets[0];
            //     int rowCount = worksheet.Dimension.End.Row;
            //     for (int row = 2; row <= rowCount; row++)
            //     {
            //         var containerType = new ReadContainerTypeContract
            //         {
            //             Number =
            //                 int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var number) ? number : default,
            //             Name = worksheet.Cells[row, 2].Value.ToString(),
            //             Volume =
            //                 decimal.TryParse(worksheet.Cells[row, 3].Value.ToString(), out var volume)
            //                     ? volume
            //                     : default
            //         };
            //         containerTypeContracts.Add(containerType);
            //     }
            // }
            //
            // containerTypes = containerTypeContracts.Select(_mapper.Map<ContainerType>).ToList();


            return containerTypes;
        }
    }
}