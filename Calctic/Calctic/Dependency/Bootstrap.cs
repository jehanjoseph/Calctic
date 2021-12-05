using System;
using System.Collections.Generic;
using System.Text;
using Calctic.Interfaces;
using Calctic.Model.BasicCalculator;
using Calctic.Model.Health.BMI;
using Calctic.Resources.JsonFiles.Currency;
using Calctic.Services;
using TinyIoC;

namespace Calctic.Dependency
{
    public static class Bootstrap
    {
        internal static TinyIoCContainer GetContainer()
        {
            var container = new TinyIoCContainer();
            container.Register<IBasicCalculator, BasicCalculator>();
            container.Register<IBmiCalculator, BmiCalculator>();
            container.Register<IBasicCalculatorService, BasicCalculatorService>();
            container.Register<IHistoryLogService, HistoryLogService>().AsSingleton();
            container.Register<IMessagingService, MessagingService>().AsSingleton();
            container.Register<ICurrencyService, CurrencyService>();

            return container;
        }

        public static ILocator GetLocator()
        {
            return new Locator(GetContainer());
        }
    }
}