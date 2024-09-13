using AutoMapper;
using GreenCity.Db.BaseModels;
using GreenCity.Db.Services.History.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenCity.Db.Services
{
    public class FillHistoryService : IFillHistoryService
    {
        private readonly IMapper _mapper;

        public FillHistoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task FillHistoryAsync<TDbModel, THistoryModel>(
            IList<TDbModel> models,
            DbContext dbContext,
            int changeUserId = 0,
            string changeUserName = "no name",
            bool execContextSaveChanges = true
        )
            where TDbModel : BaseDbModel
            where THistoryModel : BaseHistoryTableModel
        {
            if (models == null || models.Count < 1)
                return;

            List<THistoryModel> containerHistories = new List<THistoryModel>();
            foreach (var model in models)
            {
                var history = _mapper.Map<THistoryModel>(model);
                history.ChangeDt = DateTime.UtcNow;
                history.DataId = model.Id;
                history.ChangeUserId = changeUserId;
                history.ChangeUserName = changeUserName;

                containerHistories.Add(history);
            }

            await dbContext.Set<THistoryModel>().AddRangeAsync(containerHistories);

            if (execContextSaveChanges)
                await dbContext.SaveChangesAsync();
        }

        public async Task FillBindingTableHistoryAsync<TDbModel, THistoryModel>(
            IList<TDbModel> models,
            DbContext dbContext,
            int changeUserId = 0,
            string changeUserName = "no name"
        )
            where TDbModel : BaseBindingTableDbModel
            where THistoryModel : BaseHistoryTableModel
        {
            if (models == null || models.Count < 1)
                return;

            List<THistoryModel> containerHistories = new List<THistoryModel>();
            foreach (var model in models)
            {
                var history = _mapper.Map<THistoryModel>(model);
                history.ChangeDt = DateTime.UtcNow;
                history.DataId = model.DataId;
                history.ChangeUserId = changeUserId;
                history.ChangeUserName = changeUserName;

                containerHistories.Add(history);
            }

            await dbContext.Set<THistoryModel>().AddRangeAsync(containerHistories);
            await dbContext.SaveChangesAsync();
        }
    }
}
