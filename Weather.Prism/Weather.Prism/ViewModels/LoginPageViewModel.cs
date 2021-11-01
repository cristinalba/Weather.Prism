using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Weather.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
//atributos
        private string _password; 
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

//construtor
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
        }
//comandos
        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

   

//propiedades
        public string Email { get; set; }

        public string Password  //to read, write and clear
        {
            get => _password;
            set => SetProperty(ref _password, value); 
        }

        public bool IsRunning  //to read, write and clear
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public bool IsEnabled  //to read, write and clear
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
//metodos
        private async void Login()
        {
            IsRunning = true;

            if(string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a password.", "Accept");
                return;
            }

            IsRunning = false;

            await App.Current.MainPage.DisplayAlert("Ok", "Welcome to the App", "Accept");

        }
    }
}
