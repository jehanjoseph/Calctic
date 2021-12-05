using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

using Calctic.Model.UnitConverter;
using Calctic.Services;
using Calctic.Resources.JsonFiles.Converters;

namespace Calctic.ViewModel.Converters.Popups
{
    public class PopupUnitListPageViewModel : ViewModelBase
    {
        public UnitConverterService UnitConverterService { get; set; }

        public ObservableCollection<UnitName> InputUnitList { get; set; } = new ObservableCollection<UnitName>();

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand UnitSelectedCommand { get; set; }

        private UnitName _selectedUnit;

        public UnitName SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                SetProperty(ref _selectedUnit, value);
            }
        }

        private UnitSelection _unitSetting;   

        public UnitSelection UnitSetting
        {
            get => _unitSetting;
            set
            {
                SetProperty(ref _unitSetting, value);
            }
        }

        public PopupUnitListPageViewModel()
        {
            ConfirmCommand = new Command(ExecuteConfirm);
            CancelCommand = new Command(ExecuteCancel);
        }

        public PopupUnitListPageViewModel(UnitName inputSelectedUnit)
        {
            SelectedUnit = inputSelectedUnit;

            Title = inputSelectedUnit.Name;

            ConfirmCommand = new Command(ExecuteConfirm);
            CancelCommand = new Command(ExecuteCancel);
        }

        private void ExecuteConfirm()
        {
            UnitSelectedCommand?.Execute(SelectedUnit);
            PopupNavigation.Instance.PopAsync();
        }

        private void ExecuteCancel()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
