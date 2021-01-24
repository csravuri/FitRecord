using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitRecord.Database;
using FitRecord.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitRecord.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonCreatePage : ContentPage
    {
        PersonModel person;
        private DbConnection dbConnection = DbConnection.GetDbConnection();
        public PersonCreatePage()
        {
            InitializeComponent();
            BindingContext = this.person = new PersonModel();
            Title = "Create Person";
        }

        public PersonCreatePage(PersonModel person)
        {
            InitializeComponent();
            BindingContext = this.person = person;
            Title = "Update Person";
        }

        private async void OnSave(object sender, EventArgs e)
        {
            if(person.PersonID == 0)
            {
                await dbConnection.InsertRecord(person);
                await DisplayAlert("Success", $"{person.Name} created.", "OK");
            }
            else
            {
                await dbConnection.UpdateRecord(person);
                await DisplayAlert("Success", $"{person.Name} updated.", "OK");
                await Navigation.PopAsync();
            }
        }

        private async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}