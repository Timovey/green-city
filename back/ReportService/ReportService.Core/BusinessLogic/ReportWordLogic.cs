using ReportService.Core.Helpers;
using Microsoft.Extensions.Options;
using ReportService.Contracts.DataContracts;
using GreenCity.Http;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http.Helpers;
using System.Net;
using TemplateEngine.Docx;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;

namespace ReportService.Core.BusinessLogic
{
    public class ReportWordLogic
    {
        private readonly FolderSetting _folderSetting;
        public ReportWordLogic(IOptions<FolderSetting> options)
        {
            _folderSetting = options.Value;
        }

        //закоментить finally, если хочется посмотреть сам файл
        public async Task<CommonHttpResponse<ResponseFileContract>> GenerateTravelSheetsReportAsync(CreateTravelSheetsReportContract contract)
        {
            var wordOperationsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _folderSetting.Path);

            try
            {
                var props = ReplaceHelper.GetClassProps(contract);

                List<FieldContent> content = new List<FieldContent>();

                foreach (var property in props)
                {
                    content.Add(new FieldContent(property.Item1, property.Item2));
                }
                var valuesToFill = new Content(content.ToArray());

                int random = new Random().Next();
                string travelSheetDocName = $"travelSheetsReport-{random}.docx";

                string pathSheetDocName = Path.Combine(wordOperationsDir, travelSheetDocName);
                File.Copy(Path.Combine(wordOperationsDir, _folderSetting.TravelSheetsFilename), pathSheetDocName);

                using (var outputDocument = new TemplateProcessor(pathSheetDocName)
                .SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(valuesToFill);
                    outputDocument.SaveChanges();
                }

                return await ResponseFileHelper.GetFileResponseAsync(wordOperationsDir, travelSheetDocName);
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GenerateTravelSheetsReportAsync)}");
            }
            finally
            {
                var files = Directory.GetFiles(wordOperationsDir);
                foreach (var file in files)
                {
                    if (file.Contains("travelSheetsReport-"))
                    {
                        File.Delete(file);
                    }
                }
            }
        }
    }
}
