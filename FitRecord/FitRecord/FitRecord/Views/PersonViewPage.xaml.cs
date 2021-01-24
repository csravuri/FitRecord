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
    public partial class PersonViewPage : ContentPage
    {
        public ObservableCollection<PersonModel> Persons { get; set; }
        private DbConnection dbConnection = DbConnection.GetDbConnection();
        public PersonViewPage()
        {
            InitializeComponent();
            Persons = new ObservableCollection<PersonModel>();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadPersonsFromDBAsync();
        }

        private async Task LoadPersonsFromDBAsync()
        {
            try
            {
                Persons.Clear();
                IEnumerable<PersonModel> personModels = await dbConnection.GetPersons();
                Persons.AddRange(personModels);
            }
            catch (SQLiteException ee)
            {
                await DisplayAlert("Try restarting App", $"Somthing went wrong. {ee.Message}", "OK");
            }            
        }

        private async void AddNewPerson(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonCreatePage());
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView view = sender as ListView;
            view.SelectedItem = null;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new RecordsViewPage(e.Item as PersonModel));
        }

        private async void OnEdit(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            await Navigation.PushAsync(new PersonCreatePage(menuItem.CommandParameter as PersonModel));
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            PersonModel item = menuItem.CommandParameter as PersonModel;
            Persons.Remove(item);
            await dbConnection.DeleteRecord(item);
        }

        private async void ViewPerameters(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ParameterViewPage());
        }
    }
}