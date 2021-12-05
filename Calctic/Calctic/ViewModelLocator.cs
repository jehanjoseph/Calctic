using System;
using System.Collections.Generic;
using System.Text;
using Calctic.Dependency;
using Calctic.Interfaces;
using Calctic.ViewModel;
using Calctic.ViewModel.Calculators;
using Calctic.ViewModel.Calculators.Popups;
using Calctic.ViewModel.Converters;
using Calctic.ViewModel.Converters.Popups;
using Calctic.ViewModel.Finance;
using Calctic.ViewModel.Health;

namespace Calctic
{
    public static class ViewModelLocator
    {
        private static ILocator Locator = Bootstrap.GetLocator();

        static ViewModelLocator()
        {
            Locator.Register<MainMenuScreenPageViewModel>();
            Locator.Register<CalculatorMenuScreenPageViewModel>();
            Locator.Register<ConverterMenuScreenPageViewModel>();
            Locator.Register<FinanceMenuScreenPageViewModel>();
            Locator.Register<HealthMenuScreenPageViewModel>();

            #region Register Calculators

            Locator.RegisterAsSingleton<BasicCalculatorPageViewModel>();
            Locator.RegisterAsSingleton<BasicCalculatorHistoryLogsPageViewModel>();
            Locator.RegisterAsSingleton<DateTimeCalculatorPageViewModel>();

            #endregion

            #region Register Converters

            Locator.RegisterAsSingleton<BaseConverterPageViewModel>();
            Locator.RegisterAsSingleton<LengthConverterPageViewModel>();
            Locator.RegisterAsSingleton<VolumeConverterPageViewModel>();
            Locator.RegisterAsSingleton<WeightConverterPageViewModel>();
            Locator.RegisterAsSingleton<TimeConverterPageViewModel>();
            Locator.RegisterAsSingleton<TemperatureConverterPageViewModel>();
            Locator.RegisterAsSingleton<SpeedConverterPageViewModel>();
            Locator.RegisterAsSingleton<CurrencyConverterPageViewModel>();

            #endregion

            #region Register Finance

            Locator.RegisterAsSingleton<InterestFinancePageViewModel>();
            Locator.RegisterAsSingleton<DiscountFinancePageViewModel>();
            Locator.RegisterAsSingleton<PercentageFinancePageViewModel>();
            Locator.RegisterAsSingleton<SplitBillFinancePageViewModel>();
            Locator.RegisterAsSingleton<LoanFinancePageViewModel>();

            #endregion

            #region Register Health

            Locator.RegisterAsSingleton<AgeHealthPageViewModel>();
            Locator.RegisterAsSingleton<BmiHealthPageViewModel>();

            #endregion

            #region Register Popups

            Locator.Register<PopupCurrencyListPageViewModel>();
            Locator.Register<PopupDeleteHistoryPageViewModel>();

            #endregion
        }

        public static MainMenuScreenPageViewModel MainMenuScreenPageViewModel => Locator.GetInstance<MainMenuScreenPageViewModel>();
        public static CalculatorMenuScreenPageViewModel CalculatorMenuScreenPageViewModel => Locator.GetInstance<CalculatorMenuScreenPageViewModel>();
        public static ConverterMenuScreenPageViewModel ConverterMenuScreenPageViewModel => Locator.GetInstance<ConverterMenuScreenPageViewModel>();
        public static FinanceMenuScreenPageViewModel FinanceMenuScreenPageViewModel => Locator.GetInstance<FinanceMenuScreenPageViewModel>();
        public static HealthMenuScreenPageViewModel HealthMenuScreenPageViewModel => Locator.GetInstance<HealthMenuScreenPageViewModel>();

        #region Calculator Locators

        public static BasicCalculatorPageViewModel BasicCalculatorPageViewModel => Locator.GetInstance<BasicCalculatorPageViewModel>();
        public static BasicCalculatorHistoryLogsPageViewModel BasicCalculatorHistoryLogsPageViewModel => Locator.GetInstance<BasicCalculatorHistoryLogsPageViewModel>();

        public static DateTimeCalculatorPageViewModel DateTimeCalculatorPageViewModel => Locator.GetInstance<DateTimeCalculatorPageViewModel>();

        #endregion

        #region Converter Locators

        public static BaseConverterPageViewModel BaseConverterPageViewModel => Locator.GetInstance<BaseConverterPageViewModel>();
        public static LengthConverterPageViewModel LengthConverterPageViewModel => Locator.GetInstance<LengthConverterPageViewModel>();
        public static VolumeConverterPageViewModel VolumeConverterPageViewModel => Locator.GetInstance<VolumeConverterPageViewModel>();
        public static WeightConverterPageViewModel WeightConverterPageViewModel => Locator.GetInstance<WeightConverterPageViewModel>();
        public static TimeConverterPageViewModel TimeConverterPageViewModel => Locator.GetInstance<TimeConverterPageViewModel>();
        public static TemperatureConverterPageViewModel TemperatureConverterPageViewModel => Locator.GetInstance<TemperatureConverterPageViewModel>();
        public static SpeedConverterPageViewModel SpeedConverterPageViewModel => Locator.GetInstance<SpeedConverterPageViewModel>();
        public static CurrencyConverterPageViewModel CurrencyConverterPageViewModel => Locator.GetInstance<CurrencyConverterPageViewModel>();

        #endregion

        #region Finance Locators

        public static InterestFinancePageViewModel InterestFinancePageViewModel => Locator.GetInstance<InterestFinancePageViewModel>();
        public static DiscountFinancePageViewModel DiscountFinancePageViewModel => Locator.GetInstance<DiscountFinancePageViewModel>();
        public static PercentageFinancePageViewModel PercentageFinancePageViewModel => Locator.GetInstance<PercentageFinancePageViewModel>();
        public static SplitBillFinancePageViewModel SplitBillFinancePageViewModel => Locator.GetInstance<SplitBillFinancePageViewModel>();
        public static LoanFinancePageViewModel LoanFinancePageViewModel => Locator.GetInstance<LoanFinancePageViewModel>();

        #endregion

        #region Health Locators

        public static AgeHealthPageViewModel AgeHealthPageViewModel => Locator.GetInstance<AgeHealthPageViewModel>();
        public static BmiHealthPageViewModel BmiHealthPageViewModel => Locator.GetInstance<BmiHealthPageViewModel>();

        #endregion

        #region Popup Locators

        public static PopupCurrencyListPageViewModel PopupCurrencyListPageViewModel => Locator.GetInstance<PopupCurrencyListPageViewModel>();
        public static PopupDeleteHistoryPageViewModel PopupDeleteHistoryPageViewModel => Locator.GetInstance<PopupDeleteHistoryPageViewModel>();

        #endregion
    }
}