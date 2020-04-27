using Aufgabe11.Framework;
using Aufgabe11.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aufgabe11.ViewModels
{
    class WindowAddViewModel : ViewModelBase
    {
        public Employee Model { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Array Genders
        {
            get
            {
                return Enum.GetValues(typeof(Gender));
            }
        }

        public string Firstname
        {
            get => Model.Firstname;
            set
            {
                if (Model.Firstname == value) return;
                Model.Firstname = value;
                OnPropertyChanged();
            }
        }
        public string Lastname
        {
            get => Model.Surname;
            set
            {
                if (Model.Surname == value) return;
                Model.Surname = value;
                OnPropertyChanged();
            }
        }
        public Gender Gender
        {
            get => Model.Gender;
            set
            {
                if (Model.Gender == value) return;
                Model.Gender = value;
                OnPropertyChanged();
            }
        }
        public string Street
        {
            get => Model.Address.Street;
            set
            {
                if (Model.Address.Street == value) return;
                Model.Address.Street = value;
                OnPropertyChanged();
            }
        }
        public string StreetNumber
        {
            get => Model.Address.StreetNumber;
            set
            {
                if (Model.Address.StreetNumber == value) return;
                Model.Address.StreetNumber = value;
                OnPropertyChanged();
            }
        }
        public int PostCode
        {
            get => Model.Address.PostCode;
            set
            {
                if (Model.Address.PostCode == value) return;
                Model.Address.PostCode = value;
                OnPropertyChanged();
            }
        }
        public string City
        {
            get => Model.Address.City;
            set
            {
                if (Model.Address.City == value) return;
                Model.Address.City = value;
                OnPropertyChanged();
            }
        }

    }
}
