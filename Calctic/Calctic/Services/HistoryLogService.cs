using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calctic.Interfaces;
using Calctic.Model.BasicCalculator;
using MvvmHelpers;

namespace Calctic.Services
{
    public class HistoryLogService : IHistoryLogService
    {
        public ObservableRangeCollection<Result> HistoryLogs { get; } = new ObservableRangeCollection<Result>();

        public void ClearHistoryLogs()
        {
            HistoryLogs.Clear();
        }

        public void AddHistoryLogs(Result historyLog)
        {
            HistoryLogs.Add(historyLog);
        }

        public void AddHistoryLogs(List<Result> historyLogs)
        {
            HistoryLogs.AddRange(historyLogs);
        }
    }
}
