using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinProject.Helpers;
using XamarinProject.Models;
using XamarinProject.Service;
using XamarinProject.Views;

namespace XamarinProject.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {

        #region Singleton

        public static LoginViewModel instance = null;

        private LoginViewModel()
        {
            InitCommands();
            InitClass();
        }

        public static LoginViewModel GetInstance()
        {
            if (instance == null)
                instance = new LoginViewModel();

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
            EnterRegisterCommand = new Command(EnterRegister);
            LoginCommand = new Command(this.LoginAsync);
        }
        #endregion

        #region Variables

        #region Identifier

        private string identfier = "";
        public string Identifier
        {
            get
            {
                return this.identfier;
            }
            set
            {
                this.identfier = value;
                OnPropertyChanged("Identifier");
            }
        }

        #endregion

        #region Password
        private string password = "";
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        #endregion

        #region Functions and Commands

        public ICommand EnterRegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }


        public void EnterRegister()
        {
            Application.Current.MainPage.Navigation.PushAsync(new RegisterView());
        }

        public async void LoginAsync()
        {

            try
            {
                FirebaseService service = FirebaseService.GetInstance();

                UsersTable user = await service.findUser(this.identfier);

                if (user != null)
                {

                    if (user.Password == this.password)
                    {
                        /*Loggear usuario*/
                        Settings.UID = user.UserID;
                        Settings.Username = user.Username;
                        Settings.Email = user.Email;
                        Settings.Password = user.Password;

                        Console.WriteLine("Configuracion guardada para: "+Settings.Email+", UID: "+Settings.UID);

                        NavigationPage navigation = new NavigationPage(new LandingPageView());
                        App.Current.MainPage = new MasterDetailPage
                        {
                            Master = new UserMenuView(),
                            Detail = navigation
                        };

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Password incorrecta!", "Ok");
                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Usuario no encontrado!", "Ok");
                }

            }
            catch(Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error en el login", "Ok");
                Console.WriteLine("Error login: "+e);
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
