using AutoMapper;
using DataGenerationService.Contracts.DataContracts.Sd;
using DataGenerationService.Core.Helpers;
using OfficeOpenXml;
using SDContracts.DataContracts.ContractClient;
using SDService.Database.Interfaces;

namespace DataGenerationService.Core.BusinessLogic
{
    public class GeneratingContractClientLogic
    {
        private readonly IContractClientStorage _contractClientStorage;
        private readonly IMapper _mapper;

        public GeneratingContractClientLogic(
            IContractClientStorage contractClientStorage,
            IMapper mapper
        )
        {
            _contractClientStorage = contractClientStorage;
            _mapper = mapper;
        }

        public async Task GenerateContractClientsAsync()
        {
            var contractClientContracts = new List<ReadContractClientContract>();

            var file = new FileInfo(Consts.Folder + Consts.ContractClientsFilename);

            List<CreateContractClientContract> createContractClientContracts;

            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;
                for (int row = 2; row <= rowCount; row++)
                {
                    var clientName = worksheet.Cells[row, 2].Value.ToString();
                    var email = worksheet.Cells[row, 4].Value.ToString();
                    var inn = worksheet.Cells[row, 5].Value.ToString();
                    var kpp = worksheet.Cells[row, 6].Value.ToString();
                    var ogrn = worksheet.Cells[row, 7].Value.ToString();
                    var rschet = worksheet.Cells[row, 8].Value.ToString();
                    var bank = worksheet.Cells[row, 9].Value.ToString();
                    var bik = worksheet.Cells[row, 10].Value.ToString();
                    var kschet = worksheet.Cells[row, 11].Value.ToString();
                    var legalAddress = worksheet.Cells[row, 12].Value.ToString();
                    var postalAddress = worksheet.Cells[row, 13].Value.ToString();
                    var supervizorName = worksheet.Cells[row, 14].Value.ToString();
                    var supervizorPosition = worksheet.Cells[row, 15].Value.ToString();
                    var responsibleTkoPersonName = worksheet.Cells[row, 16].Value.ToString();
                    var responsibleTkoPersonPhone = worksheet.Cells[row, 17].Value.ToString();
                    var contractClient = new ReadContractClientContract
                    {
                        Number =
                            int.TryParse(worksheet.Cells[row, 1].Value.ToString(), out var number) ? number : default,
                        ClientName = !clientName.Equals("*") ? clientName : null,
                        ClientContact = worksheet.Cells[row, 3].Value.ToString(),
                        Email = !email.Equals("*") ? email : null,
                        Inn = !inn.Equals("*") ? inn : null,
                        Kpp = !kpp.Equals("*") ? kpp : null,
                        Ogrn = !ogrn.Equals("*") ? ogrn : null,
                        Rschet = !rschet.Equals("*") ? rschet : null,
                        Bank = !bank.Equals("*") ? bank : null,
                        Bik = !bik.Equals("*") ? bik : null,
                        Kschet = !kschet.Equals("*") ? kschet : null,
                        LegalAddress = !legalAddress.Equals("*") ? legalAddress : null,
                        PostalAddress = !postalAddress.Equals("*") ? postalAddress : null,
                        SupervizorName = !supervizorName.Equals("*") ? supervizorName : null,
                        SupervizorPosition = !supervizorPosition.Equals("*") ? supervizorPosition : null,
                        ResponsibleTkoPersonName =
                            !responsibleTkoPersonName.Equals("*") ? responsibleTkoPersonName : null,
                        ResponsibleTkoPersonPhone =
                            !responsibleTkoPersonPhone.Equals("*") ? responsibleTkoPersonPhone : null,
                        CreatedUserNumber =
                            int.TryParse(worksheet.Cells[row, 18].Value.ToString(), out var createdUserNumber)
                                ? createdUserNumber
                                : default,
                    };
                    contractClientContracts.Add(contractClient);
                }
            }

            createContractClientContracts =
                contractClientContracts.Select(_mapper.Map<CreateContractClientContract>).ToList();

            foreach (var contractClient in createContractClientContracts)
            {
                try
                {
                    var id = await _contractClientStorage.CreateContractClientAsync(contractClient);
                    Console.WriteLine($"Добавлен контрагент с id {id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}