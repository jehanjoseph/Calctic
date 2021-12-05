using System;
using Calctic.Interfaces;
using Calctic.Services;
using Calctic.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calctic
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
