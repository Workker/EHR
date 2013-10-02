using AutoMapper;
using EHR.Domain.Entities;
using System.Collections.Generic;

namespace EHR.UI.Models.Mappers
{
    public class HistoryRecordMapper
    {
        public static List<HistoryRecordMoldel> MapHistoryRecordModelsFrom(IEnumerable<HistoryRecord> historyRecords)
        {
            var historyRecordMoldel = new List<HistoryRecordMoldel>();

            foreach (var item in historyRecords)
            {
                historyRecordMoldel.Add(MapHistoryRecordModelFrom(item));
            }

            return historyRecordMoldel;
        }

        public static HistoryRecordMoldel MapHistoryRecordModelFrom(HistoryRecord historyRecord)
        {
            Mapper.CreateMap<HistoryRecord, HistoryRecordMoldel>().ForMember(ac => ac.Account, source => source.Ignore())
                .ForMember(at => at.Action, source => source.Ignore());

            var historyRecordModel = Mapper.Map<HistoryRecord, HistoryRecordMoldel>(historyRecord);

            historyRecordModel.Account = AccountMapper.MapAccountModelFrom(historyRecord.Account);
            historyRecordModel.Action = historyRecord.Action.Description;

            return historyRecordModel;
        }
    }
}