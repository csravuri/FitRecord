using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitRecord.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitRecord.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsViewPage : ContentPage, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;
        private PersonModel _Person;
        public PersonModel Person {
            get
            {
                return _Person;
            }
            set
            {
                _Person = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Person)));
            }
        }
        public RecordsViewPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public RecordsViewPage(PersonModel person) : this()
        {
            this.Person = person;
        }

        private async void OnViewPerson(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PersonCreatePage(Person));
        }

     
    }
}