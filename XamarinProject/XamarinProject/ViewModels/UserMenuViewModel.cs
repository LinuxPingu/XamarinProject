using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinProject.Helpers;
using XamarinProject.Models;
using XamarinProject.Views;

namespace XamarinProject.ViewModels
{
    class UserMenuViewModel: INotifyPropertyChanged
    {

        #region Singleton

        public static UserMenuViewModel instance = null;


        public static UserMenuViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new UserMenuViewModel();
            }

            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }

        }

        private UserMenuViewModel()
        {
            InitCommands();
            InitClass();
        }


        #endregion

        #region Variables

        #region ListaMenu
        private ObservableCollection<UserMenuModel> lstMenu = new ObservableCollection<UserMenuModel>();
        public ObservableCollection<UserMenuModel> LstMenu
        {
            get
            {
                return lstMenu;
            }
            set
            {
                lstMenu = value;
                OnPropertyChanged("LstMenu");
            }
        }
        #endregion

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

        #endregion

        #region Inits
        public void InitClass()
        {
            InitCommands();
            /*Setteo del Username*/
            identfier = Settings.Username;
            lstMenu = new ObservableCollection<UserMenuModel>();
            lstMenu.Add(new UserMenuModel { Id = 1, Name = "Inicio", Icon = "https://png.pngtree.com/png-vector/20190411/ourlarge/pngtree-vector-newspaper-icon-png-image_927091.jpg" });
            lstMenu.Add(new UserMenuModel { Id = 2, Name = "Mi Perfil", Icon = "https://www.pngitem.com/pimgs/m/150-1503941_user-windows-10-user-icon-png-transparent-png.png" });
            lstMenu.Add(new UserMenuModel { Id = 3, Name = "Tienda", Icon = "https://pngimg.com/uploads/shopping_cart/shopping_cart_PNG38.png" });
        }

        private void InitCommands()
        {
            EnterMenuOptionCommand = new Command<int>(EnterMenuOption);
            LogOutCommand = new Command(this.LogOut);
        }
        #endregion

        #region Commands
        public ICommand EnterMenuOptionCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public async void EnterMenuOption(int opc)
        {
            switch (opc)
            {
                case 1:
                    await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new LandingPageView());
                    break;
                case 2:
                    await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new UserProfileView());
                    break;

                case 3:

                    await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new StoreView());

                    break;

                default:
                    break;
            }
        }
        #endregion

        public void LogOut()
        {
            Settings.UID = "";
            Settings.Username = "";
            Settings.Email = "";
            Settings.Password = "";

            Application.Current.MainPage = new NavigationPage(new LoginView());
        }


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
