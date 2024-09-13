using GreenCity.Db.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenCity.Db.Services.History.Interface
{
    public interface IFillHistoryService
    {
        /// <summary>
        /// Заполнение данных исторической таблицы
        /// </summary>
        /// <typeparam name="TDbModel"> Исходная таблица </typeparam>
        /// <typeparam name="THistoryModel"> Историческая таблица </typeparam>
        /// <param name="models"> Измененные данные исходной таблицы </param>
        /// <param name="dbContext"> Используемый контекст БД </param>
        /// <param name="changeUserId"> ИД пользователя изменившего запись </param>
        /// <param name="changeUserName"> Имя пользователя изменившего запись </param>
        /// <param name="execContextSaveChanges"> Вызвать ли внутри метод контекста SaveChangesAsync </param>
        /// <returns></returns>
        Task FillHistoryAsync<TDbModel, THistoryModel>(
            IList<TDbModel> models,
            DbContext dbContext,
            int changeUserId = 0,
            string changeUserName = "no name",
            bool execContextSaveChanges = true
        ) 
            where TDbModel : BaseDbModel
            where THistoryModel : BaseHistoryTableModel;

        [Obsolete("Для старой архитектуры БД, кандидат на удаление")]
        Task FillBindingTableHistoryAsync<TDbModel, THistoryModel>(
            IList<TDbModel> models,
            DbContext dbContext,
            int changeUserId = 0,
            string changeUserName = "no name"
        )
            where TDbModel : BaseBindingTableDbModel
            where THistoryModel : BaseHistoryTableModel;
    }
}
