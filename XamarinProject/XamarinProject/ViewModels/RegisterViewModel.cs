using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinProject.Models;
using XamarinProject.Service;
using XamarinProject.Views;

namespace XamarinProject.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {

        #region Singleton
        public static RegisterViewModel instance = null;

        private RegisterViewModel()
        {
            InitCommands();
            InitClass();
        }

        public static RegisterViewModel GetInstance()
        {
            if (instance == null)
                instance = new RegisterViewModel();

            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
                instance = null;
        }
        #endregion

        #region Inits
        public void InitClass()
        {

        }

        private void InitCommands()
        {
            RegisterCommand = new Command(RegisterCMD);
        }
        #endregion

        #region Variables

        #region UserTable

        private UsersTable user = new UsersTable();

        public UsersTable User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }


        #endregion

        #region PasswordConfirm
        private String confirmationPass = "";
        public String Confirmation
        {
            get
            {
                return this.confirmationPass;
            }
            set
            {
                this.confirmationPass = value;
                OnPropertyChanged("Confirmation");
            }
        }

        #endregion

        #endregion

        #region Functions and Commands

        public ICommand RegisterCommand { get; set; }

        public async void RegisterCMD()
        {

            if (user.Email != "" && user.Username != "" && user.Password != "" && this.confirmationPass != "")
            {
                if (user.Password == confirmationPass)
                {
                    user.UserID = Guid.NewGuid().ToString();
                    FirebaseService service = FirebaseService.GetInstance();
                    bool chk = await service.RegisterUser(user);

                    if (chk)
                    {
                        await App.Current.MainPage.DisplayAlert("Success", "Cuenta creada!", "Ok");
                        Application.Current.MainPage = new NavigationPage(new LoginView());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Error al crear la cuenta", "Ok");
                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Las password no coinciden", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Revise que los campos esten llenos", "Ok");
            }

        }
        #endregion

        #region INotifyPropertyChanged Implentation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
