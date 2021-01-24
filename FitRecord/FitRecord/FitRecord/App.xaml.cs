using System;
using FitRecord.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitRecord
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new ParameterViewPage());
            MainPage = new NavigationPage(new PersonViewPage());
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
