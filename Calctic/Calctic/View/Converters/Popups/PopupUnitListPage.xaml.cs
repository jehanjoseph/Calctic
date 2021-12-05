using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calctic.Model.UnitConverter;
using Calctic.Resources.JsonFiles.Converters;
using Calctic.ViewModel.Converters.Popups;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Calctic.View.Converters.Popups
{
    public partial class PopupUnitListPage : PopupPage
    {
        public PopupUnitListPage()
        {
            InitializeComponent();
        }

        public PopupUnitListPage(UnitSelection inputSelectedUnit, ObservableCollection<UnitName> inputUnitList)
        {
            InitializeComponent();

            var popupContext = new PopupUnitListPageViewModel
            {
                Title = $"Select Unit of {inputSelectedUnit}"
            };

            BindingContext = popupContext;

            UnitListView.ItemsSource = inputUnitList;
        }
    }
}
