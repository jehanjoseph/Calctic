using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calctic.Model.BasicCalculator;
using MvvmHelpers;

namespace Calctic.Interfaces
{
    public interface IHistoryLogService
    {
        ObservableRangeCollection<Result> HistoryLogs { get; }

        void ClearHistoryLogs();

        void AddHistoryLogs(Result historyLog);

        void AddHistoryLogs(List<Result> historyLogs);
    }
}
