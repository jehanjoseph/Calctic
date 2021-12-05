using System;
using System.Collections.Generic;
using Xamarin.Forms;

using Calctic.View.Calculators;
using Calctic.View.Converters;
using Calctic.View.Finance;
using Calctic.View.Health;

namespace Calctic.View
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CalculatorMenuScreenPage), typeof(CalculatorMenuScreenPage));
            Routing.RegisterRoute(nameof(ConverterMenuScreenPage), typeof(ConverterMenuScreenPage));
            Routing.RegisterRoute(nameof(FinanceMenuScreenPage), typeof(FinanceMenuScreenPage));
            Routing.RegisterRoute(nameof(HealthMenuScreenPage), typeof(HealthMenuScreenPage));

            #region Calculators

            Routing.RegisterRoute(nameof(BasicCalculatorPage), typeof(BasicCalculatorPage));
            Routing.RegisterRoute(nameof(BasicCalculatorHistoryLogsPage), typeof(BasicCalculatorHistoryLogsPage));
            Routing.RegisterRoute(nameof(DateTimeCalculatorPage), typeof(DateTimeCalculatorPage));

            #endregion

            #region Converters

            Routing.RegisterRoute(nameof(LengthConverterPage), typeof(LengthConverterPage));
            Routing.RegisterRoute(nameof(VolumeConverterPage), typeof(VolumeConverterPage));
            Routing.RegisterRoute(nameof(WeightConverterPage), typeof(WeightConverterPage));
            Routing.RegisterRoute(nameof(TimeConverterPage), typeof(TimeConverterPage));
            Routing.RegisterRoute(nameof(TemperatureConverterPage), typeof(TemperatureConverterPage));
            Routing.RegisterRoute(nameof(SpeedConverterPage), typeof(SpeedConverterPage));
            Routing.RegisterRoute(nameof(CurrencyConverterPage), typeof(CurrencyConverterPage));

            #endregion

            #region Finance

            Routing.RegisterRoute(nameof(InterestFinancePage), typeof(InterestFinancePage));
            Routing.RegisterRoute(nameof(DiscountFinancePage), typeof(DiscountFinancePage));
            Routing.RegisterRoute(nameof(PercentageFinancePage), typeof(PercentageFinancePage));
            Routing.RegisterRoute(nameof(SplitBillFinancePage), typeof(SplitBillFinancePage));
            Routing.RegisterRoute(nameof(LoanFinancePage), typeof(LoanFinancePage));

            #endregion

            #region Health

            Routing.RegisterRoute(nameof(AgeHealthPage), typeof(AgeHealthPage));
            Routing.RegisterRoute(nameof(BmiHealthPage), typeof(BmiHealthPage));

            #endregion
        }
    }
}
