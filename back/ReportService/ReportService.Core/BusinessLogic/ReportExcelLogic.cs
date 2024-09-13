using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GreenCity.CommonUtils.CommonExtensions;
using GreenCity.Http;
using GreenCity.Http.Helpers;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ReportService.Contracts.DataContracts;
using ReportService.Contracts.DataContracts.DoneTasksList;
using ReportService.Core.Helpers;

namespace ReportService.Core.BusinessLogic
{
    public class ReportExcelLogic
    {
        private readonly FolderSetting _folderSetting;

        public ReportExcelLogic(IOptions<FolderSetting> options)
        {
            _folderSetting = options.Value;
        }

        public async Task<CommonHttpResponse<ResponseFileContract>> GenerateShiftTaskReportAsync(
            CreateShiftTaskReportContract contract)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    int column = 1;
                    int row = 1;

                    //добавляем лист
                    var ws = package.Workbook.Worksheets.Add("Лист 1");

                    ws.Cells[row, column].Value = "Сменное задание";

                    var shiftTaskProps = ReplaceHelper.GetPropertiesInfo(contract);

                    //вставляем названия и значения свойств сменного задания
                    foreach (var property in shiftTaskProps)
                    {
                        row++;
                        ws.Cells[row, column].Value = property.Description;
                        ws.Cells[row, column + 1].Value = property.Value;
                    }

                    DrawBorders(ws.Cells[2, 1, shiftTaskProps.Count + 1, 2]);

                    row += 2;

                    int startRow, endRow = 0;
                    IList<PropertyData> standsProps = null, containersProps = null;

                    foreach (var route in contract.ShiftTaskRoutes)
                    {
                        ws.Cells[row, column].Value = $"Рейс {route.RouteNumber}";

                        row++;
                        startRow = row;

                        var stands = route.ShiftTaskStands;
                        var firstStand = stands.FirstOrDefault();

                        //вставляем шапку таблицы из свойств объекта
                        standsProps = ReplaceHelper.GetPropertiesInfo(firstStand);
                        foreach (var property in standsProps)
                        {
                            ws.Cells[row, column].Value = property.Description;
                            column++;
                        }

                        var containers = firstStand!.ShiftTaskContainers;

                        containersProps = ReplaceHelper.GetPropertiesInfo(containers.FirstOrDefault());
                        foreach (var property in containersProps)
                        {
                            ws.Cells[row, column].Value = property.Description;
                            column++;
                        }

                        ws.Cells[row, 1, row, column].Style
                            .HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        row++;
                        column = 1;

                        //проходимся по каждой КП и вставляем значения в ячейки
                        foreach (var stand in stands)
                        {
                            DecorateCells(ws.Cells[row, column, row, column + 1], ExcelFillStyle.Solid,
                                Color.FromArgb(235, 241, 222));
                            ws.Cells[row, column].Value = $"{stand.StandAdress}";
                            ws.Cells[row, column + 1].Value = $"{stand.StandContacts}";
                            int firstContainerRow = row;
                            foreach (var container in stand.ShiftTaskContainers)
                            {
                                if (StringComparer.CurrentCultureIgnoreCase.Equals(container.StandType, $"Обычный"))
                                {
                                    DecorateCells(ws.Cells[row, column + 2, row, column + 6], ExcelFillStyle.Solid,
                                        Color.Yellow);
                                }

                                ws.Cells[row, column + 2].Value = $"{container.Contragents}";
                                ws.Cells[row, column + 3].Value = $"{container.ContainerName}";
                                ws.Cells[row, column + 4].Value = $"{container.TrashType}";
                                ws.Cells[row, column + 5].Value = $"{container.StandType}";
                                ws.Cells[row, column + 6].Value = $"{container.Comment}";
                                row++;
                            }

                            if (stand.ShiftTaskContainers.Count > 1)
                            {
                                FormatCells(ws.Cells[firstContainerRow, column, row - 1, column], true, true);
                                FormatCells(ws.Cells[firstContainerRow, column + 1, row - 1, column + 1], true, true);
                            }
                        }

                        endRow = startRow + route.ShiftTaskStands.Sum(x => x.ShiftTaskContainers.Count);
                        DrawBorders(ws.Cells[startRow, column, endRow, standsProps.Count + containersProps.Count]);

                        row++;
                    }

                    ws.Cells[1, 1, endRow, standsProps!.Count + containersProps!.Count].AutoFitColumns();

                    package.Workbook.Calculate();

                    //генерим рандомное название файла
                    //(при скачивании может оказаться 2 одинаковых файла, как бы пофиг, но все же)
                    int random = new Random().Next();
                    string shiftTaskDocName = $"shiftTaskReport-{random}.xlsx";

                    //если надо затестить сохраненный локально файл - то надо раскоментить
                    //package.SaveAs(new FileInfo(Path.Combine(_folderSetting.Path, shiftTaskDocName)));

                    //отправляем резульат как массив байтов
                    var result = new ResponseFileContract
                    {
                        Buffer = package.GetAsByteArray(),
                        Extension = $"application/xlsx",
                        Filename = shiftTaskDocName,
                    };

                    return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GenerateShiftTaskReportAsync)}");
            }
        }

        public async Task<CommonHttpResponse<ResponseFileContract>> GenerateDoneTaskListReportAsync(
            DoneTaskListContract contract)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    int column = 1;
                    int row = 1;

                    //добавляем лист
                    var ws = package.Workbook.Worksheets.Add("Лист 1");

                    int startRow = 0, endRow = 0;
                    IList<PropertyData> routeProps = null;

                    foreach (var route in contract.Routes)
                    {
                        routeProps = ReplaceHelper.GetPropertiesInfo(route);

                        //вставляем заголовок (название и дата рейса)
                        ws.Cells[row, column].Value =
                            $"{routeProps[0].Description} {routeProps[0].Value} {routeProps[1].Value}";

                        //объединяем ячейки заголовока
                        FormatCells(ws.Cells[row, column, row, routeProps.Count + 1], true);

                        row++;

                        ws.Cells[row, column].Value = $"№ п/п";

                        //устанавливаем фиксированную высоту для строки шапки
                        ws.Row(row).Height = 72;
                        //применяем необходимый формат к строке шапки
                        FormatCells(ws.Cells[row, 1, row, routeProps.Count + 1], false, true);

                        //вставляем названия и значения свойств рейсов и кп
                        //(начинаем с третьего свойства, тк 1 и 2 - заголовок табл.)
                        for (var i = 2; i < routeProps.Count; i++)
                        {
                            column++;
                            var property = routeProps[i];
                            //вставка названия свойства рейса
                            ws.Cells[row, column].Value = property.Description;

                            //вставка значения свойства рейса
                            if (!property.IsEndListValue)
                                ws.Cells[row + 1, column].Value = property.Value;

                            //объединяем необходимые ячейки
                            if (property.IsMerge)
                            {
                                ws.Cells[row, column, row, column + 1].Merge = true;
                                ws.Cells[row + 1, column].Value = $"Куб.м.";
                                ws.Cells[row + 1, ++column].Value = $"Т.";
                            }
                        }

                        //устанавливаем строку значений рейса по центру
                        FormatCells(ws.Cells[row + 1, 1, row + 1, routeProps.Count + 1]);

                        row += 2;
                        column = 1;

                        //фиксириуем номер строки, на которой начинается список кп
                        startRow = row;

                        //ищем индексы столбцов, связанных с кп
                        int columnStandAddress =
                            routeProps.IndexOf(routeProps.FirstOrDefault(x => x.Name == "StandAddress"));
                        int columnTotalVolumeByStand =
                            routeProps.IndexOf(routeProps.FirstOrDefault(x => x.Name == "TotalVolumeByStand"));

                        for (var i = 0; i < route.StandsInfo.Count; i++)
                        {
                            //нумеруем кп
                            ws.Cells[row, column].Value = i + 1;

                            //вставляем значения кп
                            var stand = route.StandsInfo[i];
                            ws.Cells[row, columnStandAddress].Value = $"{stand.StandAddress}";
                            ws.Cells[row, columnTotalVolumeByStand].Value = $"{stand.TotalVolumeByStand}";

                            row++;
                        }

                        //фиксириуем номер строки, на которой заканчивается список кп
                        endRow = startRow + route.StandsInfo.Count;

                        //выравниваем посередине столбцы номеров кп и объемов загруж.тко
                        FormatCells(ws.Cells[startRow, 1, endRow, 1]);
                        FormatCells(ws.Cells[startRow, columnTotalVolumeByStand, endRow, columnTotalVolumeByStand]);

                        //ищем свойства рейса, значения которых должны быть расставлены после списка кп
                        var propsInfos = routeProps.Where(x => x.IsEndListValue).ToList();
                        foreach (var propInfo in propsInfos)
                        {
                            var prop = routeProps.IndexOf(propInfo) + 1;
                            //расставляем значения в необходиых столбцах
                            ws.Cells[endRow, prop].Value = propInfo.Value;
                            //выравниваем посередине
                            FormatCells(ws.Cells[endRow, prop, endRow, prop]);
                        }

                        row++;
                    }

                    //устанавливаем необходимый шрифт
                    var allCells = ws.Cells[1, 1, ws.Dimension.End.Row, ws.Dimension.End.Column];
                    var cellFont = allCells.Style.Font;
                    cellFont.SetFromFont(new Font("Times New Roman", 14));
                    //рисуем границы
                    DrawBorders(ws.Cells[1, 1, endRow, routeProps.Count + 1]);
                    //делаем автоподбор по содержимому (шапки таблиц не учитываем)
                    ws.Cells[startRow - 1, 1, endRow, ws.Dimension.End.Column].AutoFitColumns();

                    package.Workbook.Calculate();

                    //генерим рандомное название файла
                    //(при скачивании может оказаться 2 одинаковых файла, как бы пофиг, но все же)
                    int random = new Random().Next();
                    string shiftTaskDocName = $"doneTaskListReport-{random}.xlsx";

                    //если надо затестить сохраненный локально файл - то надо раскоментить
                    //package.SaveAs(new FileInfo(Path.Combine(_folderSetting.Path, shiftTaskDocName)));

                    //отправляем резульат как массив байтов
                    var result = new ResponseFileContract
                    {
                        Buffer = package.GetAsByteArray(),
                        Extension = $"application/xlsx",
                        Filename = shiftTaskDocName,
                    };

                    return CommonHttpHelper.BuildSuccessResponse(result, HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return CommonHttpHelper.BuildErrorResponse<ResponseFileContract>(
                    HttpStatusCode.InternalServerError,
                    ex.ToExceptionDetails(),
                    $"Ошибка выполнения метода {nameof(GenerateDoneTaskListReportAsync)}");
            }
        }


        //тестовая настройка стилей ячеек
        private void DecorateCells(ExcelRange range, ExcelFillStyle fillstyle, Color color)
        {
            range.Style.Fill.PatternType = fillstyle;
            range.Style.Fill.BackgroundColor.SetColor(color);
        }

        private void DrawBorders(ExcelRange range)
        {
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        private void FormatCells(ExcelRange range, bool isMerge = false, bool isWrap = false)
        {
            range.Merge = isMerge;
            range.Style.WrapText = isWrap;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }
    }
}