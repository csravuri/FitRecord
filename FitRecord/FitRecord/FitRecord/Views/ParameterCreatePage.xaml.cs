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
    public partial class ParameterCreatePage : ContentPage
    {
        ParameterModel parameter;
        private DbConnection dbConnection = DbConnection.GetDbConnection();
        public ParameterCreatePage()
        {
            InitializeComponent();
            BindingContext = this.parameter = new ParameterModel() { IsActive = true};
            Title = "Create Parameter";
        }

        public ParameterCreatePage(ParameterModel parameter)
        {
            InitializeComponent();
            BindingContext = this.parameter = parameter;
            Title = "Update Parameter";
        }

        private async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnSave(object sender, EventArgs e)
        {
            if (IsValid())
            {
                if (parameter.ParameterID == 0)
                {
                    await dbConnection.InsertRecord(parameter);
                    await DisplayAlert("Success", $"{parameter.Name} created.", "OK");
                }
                else
                {
                    await dbConnection.UpdateRecord(parameter);
                    await DisplayAlert("Success", $"{parameter.Name} updated.", "OK");
                    await Navigation.PopAsync();
                }
            }

            parameter = new ParameterModel() { IsActive = true};
            
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(parameter.Name))
            {

            }

            return true;
        }

        private void Entry_MeasureInvalidated(object sender, EventArgs e)
        {

        }
    }
}