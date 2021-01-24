using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitRecord.Common;
using FitRecord.Database;
using FitRecord.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitRecord.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParameterViewPage : ContentPage
    {
        public ObservableCollection<ParameterModel> Parameters { get; set; }
        private DbConnection dbConnection = DbConnection.GetDbConnection();
        public ParameterViewPage()
        {
            InitializeComponent();
            Parameters = new ObservableCollection<ParameterModel>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadParametersFromDB();
        }

        private async Task LoadParametersFromDB()
        {
            try
            {
                Parameters.Clear();
                IEnumerable<ParameterModel> parameterModels = await dbConnection.GetParameters();
                Parameters.AddRange(parameterModels);
            }
            catch (SQLiteException ee)
            {
                await DisplayAlert("Try restarting App", $"Somthing went wrong. {ee.Message}", "OK");
            }
        }

        private async void OnNewUnit(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ParameterCreatePage());
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView view = sender as ListView;
            view.SelectedItem = null;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ParameterCreatePage(e.Item as ParameterModel));
        }

        private async void OnEdit(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            await Navigation.PushAsync(new ParameterCreatePage(menuItem.CommandParameter as ParameterModel));
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ParameterModel item = menuItem.CommandParameter as ParameterModel;
            Parameters.Remove(item);
            await dbConnection.DeleteRecord(item);
        }
    }
}