using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinProject.Helpers;
using XamarinProject.Models;
using XamarinProject.Service;

namespace XamarinProject.ViewModels
{
    class UserProfileViewModel: INotifyPropertyChanged
    {

        #region Singleton

       public static UserProfileViewModel instace = null;


        UserProfileViewModel()
        {
            InitCommands();
            InitClass();
        }


        public static UserProfileViewModel getInstance()
        {
            if (instace == null)
            {
                instace = new UserProfileViewModel();
            }
            return instace;
        }

        public static void deleteInstance()
        {
            if (instace != null)
            {
                instace = null;
            }
         
        }

        #endregion

        #region Varibles

        /* Funcionalidades */

        public UsersInfo userInfo = new UsersInfo();

        #region Primera Pagina

        public bool editingaccount = true;
        public bool EditingAccount
        {
            get
            {
                return this.editingaccount;
            }
            set
            {
                this.editingaccount = value;
                OnPropertyChanged("Identifier");
            }
        }


        public string accountBtnTxt = "Editar Informacion";
        public string AccountBtnTxt
        {
            get
            {
                return this.accountBtnTxt;
            }
            set
            {
                this.accountBtnTxt = value;
                OnPropertyChanged("Identifier");
            }
        }


        public string accountBtnClr = "#16c79a";
        public string AccountBtnClr
        {
            get
            {
                return accountBtnClr;
            }
            set
            {
                this.accountBtnClr = value;
                OnPropertyChanged("Identifier");
            }
        }

        public string accountBtnIcon = "IconClass.Pen";

        public string AccountBtnIcon
        {
            get
            {
                return accountBtnIcon;
            }
            set
            {
                this.accountBtnIcon = value;
                OnPropertyChanged("Identifier");
            }
        }

        #endregion


        /* Informacion de Cuenta */

        #region UID

        private string uid = "";
        public string UID
        {
            get
            {
                return this.uid;
            }
            set
            {
                this.uid = value;
                OnPropertyChanged("UID");
            }
        }

        #endregion

        #region Email

        private string email = "";
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged("Email");
            }
        }

        #endregion

        #region Username

        private string username = "";
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                OnPropertyChanged("Username");
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

        /* Informacion Personal */

        #region User info Exists

        public bool userInfoExists = false;

        #endregion

        #region Nombre

        public string nombre = "";
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        #endregion

        #region Apelliddos

        private string apellidos = "";
        public string Apellidos
        {
            get
            {
                return this.apellidos;
            }
            set
            {
                this.apellidos = value;
                OnPropertyChanged("Apellidos");
            }
        }

        #endregion

        #region Fecha Nacimiento


        public DateTime date { get; set; }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                this.date = value;
                OnPropertyChanged("Date");
            }
        }

        private string fechaNac = "";
        public string FechaNac
        {
            get
            {
                return this.fechaNac;
            }
            set
            {
                this.fechaNac = value;
                OnPropertyChanged("FechaNac");
            }
        }

        #endregion

        #region Telefono

        private string telefono = "";
        public string Telefono
        {
            get
            {
                return this.telefono;
            }
            set
            {
                this.telefono = value;
                OnPropertyChanged("Telefono");
            }
        }

        #endregion


        /* Mapa */

        #region Latitude
        private double lat = 0.0;
        public double Latitude
        {
            get
            {
                return this.lat ;
            }
            set
            {
                this.lat = value;
                OnPropertyChanged("Identifier");
            }
        }
        #endregion

        #region Longitude
        private double longt = 0.0;
        public double Longitude
        {
            get
            {
                return this.longt;
            }
            set
            {
                this.longt = value;
                OnPropertyChanged("Identifier");
            }
        }


        #endregion

        #region Altitude

        private double alt = 0.0;
        public double Altitude
        {
            get
            {
                return this.alt;
            }
            set
            {
                this.alt = value;
                OnPropertyChanged("Identifier");
            }
        }

        #endregion

        #region Mapa
        private ObservableCollection<UsersLocation> _lstLocations = new ObservableCollection<UsersLocation>();

        public ObservableCollection<UsersLocation> lstLocations
        {
            get { return _lstLocations; }

            set
            {
                _lstLocations = value;
                OnPropertyChanged("lstLocations");
            }
        }
        #endregion


        public double newLat = 0.0;
        public double newLong = 0.0;

        #endregion

        #region Commands and Functions
        public void InitCommands()
        {
            EditAccountCommand = new Command(this.editAccount);
            this.EditPersonalInfoCommand = new Command(this.editPersonalInfo);
            this.EditUbicationCommand = new Command(this.editUbication);
        }

        public void InitClass()
        {
          
            setAccountInfo();

            Console.WriteLine("Out Ubication: " + this.newLat + "," + this.newLong);
            
            lstLocations.Add(new UsersLocation { Latitude = 9.84350333333333, Longitude = -83.9727966666667, Description = "Tu ubicacion" });
            

        }


        public ICommand EditAccountCommand { get; set; }
        public ICommand EditPersonalInfoCommand { get; set; }
        public ICommand EditUbicationCommand { get; set; }

        public async void editAccount()
        {

            UsersTable user = new UsersTable();
            user.UserID = Settings.UID;
            user.Email = this.email;
            user.Username = this.username;
            user.Password = this.password;


            if ( user.Email != Settings.Email || user.Username != Settings.Username || user.Password != Settings.Password)
            {
                FirebaseService service = FirebaseService.GetInstance();
                try
                {
                    bool chk = await service.editAccount(user);

                    if (chk)
                    {
                        await App.Current.MainPage.DisplayAlert("Success", " Cuenta Editada!", "Ok");
                        Settings.Email = user.Email;
                        Settings.Username = user.Username;
                        Settings.Password = user.Password;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Error editando cuenta", "Ok");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error en edicion: " + e);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No se han detectado cambios", "Ok");
            }
        }
        public async void editPersonalInfo()
        {

            if (this.userInfoExists)
            {

                /* Campos llenos */
                if (this.nombre != null && this.apellidos != null && this.fechaNac != null && this.telefono != null)
                {
                    /* Campos iguales */
                    if(this.nombre == userInfo.Nombre && this.apellidos == userInfo.Apellidos && this.fechaNac == userInfo.FechaNac && this.telefono == userInfo.telefono)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "No hay cambios detectados", "Ok");
                    }
                    else
                    {
                        try
                        {
                            UsersInfo newInfo = new UsersInfo();
                            newInfo.UID = Settings.UID;
                            newInfo.Nombre = this.nombre;
                            newInfo.Apellidos = this.apellidos;
                            newInfo.FechaNac = this.fechaNac;
                            newInfo.telefono = this.telefono;
                            FirebaseService service = FirebaseService.GetInstance();
                            bool chk = await service.editPersonalInfo(newInfo);

                            if (chk)
                            {
                                await App.Current.MainPage.DisplayAlert("Success", "Informacion personal editada!", "Ok");
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error", "Error editando la informacion", "Ok");
                            }


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error editando la indormacion: "+e);
                            await App.Current.MainPage.DisplayAlert("Error", "Error editando la informacion", "Ok");
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Revise que los campos esten llenos", "Ok");
                }


            }
            else
            {

                if (this.nombre != null && this.apellidos != null && this.fechaNac != null && this.telefono != null)
                {

                    try
                    {
                        UsersInfo newInfo = new UsersInfo();
                        newInfo.UID = Settings.UID;
                        newInfo.Nombre = this.nombre;
                        newInfo.Apellidos = this.apellidos;
                        newInfo.FechaNac = this.fechaNac;
                        newInfo.telefono = this.telefono;

                        FirebaseService service = FirebaseService.GetInstance();

                        bool chk = await service.SaveUserInfo(newInfo);

                        if (chk)
                        {
                            await App.Current.MainPage.DisplayAlert("Success", "Informacion de usuario agregada!", "Ok");
                            this.userInfoExists = true;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "Error creando la informacion", "Ok");
                        }


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error generando la informacion de usuario: "+e);
                        await App.Current.MainPage.DisplayAlert("Error", "Error creando la informacion", "Ok");
                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Revise que los campos esten llenos", "Ok");
                }

            }

        }

        public async void editUbication()
        {

            FirebaseService service = FirebaseService.GetInstance();

            try
            {
                UsersLocation newLocation = new UsersLocation();
                newLocation.UID = Settings.UID;
                newLocation.Latitude = this.newLat;
                newLocation.Longitude = this.newLong;

                bool chk = await service.saveUbication(newLocation);

                if (chk)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Datos Actualizados", "Ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Error actualizando los datos", "Ok");
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error actualizando la ubicacion");
            }

        }

        public void dateSelect()
        {

        }

        public async void setAccountInfo()
        {
            this.lstLocations.Clear();

            this.uid = Settings.UID;
            this.email = Settings.Email;
            this.username = Settings.Username;
            this.password = Settings.Password;

            try
            {
                await GetCurrentLocation();
                Console.WriteLine("Ubicacion obtenida: "+this.lat+","+this.longt);
                lstLocations.Add(new UsersLocation { Latitude = lat, Longitude = longt, Description = "Tu ubicacion" });
                this.newLat = lat;
                this.newLong = longt;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error obteniendo ubicacion: "+e);
            }
           

            

            try
            {
                FirebaseService service = FirebaseService.GetInstance();

                UsersInfo info = await service.getPersonalInfoByID(Settings.UID);

                if (info != null)
                {
                    this.userInfoExists = true;
                    this.Nombre = info.Nombre;
                    this.Apellidos = info.Apellidos;
                    this.FechaNac = info.FechaNac;
                    this.Telefono = info.telefono;
        
                    userInfo.UID = Settings.UID;
                    userInfo.Nombre = this.nombre;
                    userInfo.Apellidos = this.apellidos;
                    userInfo.FechaNac = this.fechaNac;
                    userInfo.telefono = this.telefono;

                    this.Nombre = userInfo.Nombre;
                    this.Apellidos = userInfo.Apellidos;
                    this.FechaNac = userInfo.FechaNac;
                    this.Telefono = userInfo.telefono;

                }
                else
                {
                    this.userInfoExists = false;
                    this.nombre = "";
                    this.apellidos = "";
                    this.telefono = "";
                    this.date = DateTime.Now;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error al encontrar informacion personal: "+e);
            }

        }



        CancellationTokenSource cts;

        public async Task GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {


                    this.Latitude= location.Latitude;
                    this.Longitude = location.Longitude;

                   

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine("Error Obteniendo ubicacion: " +fnsEx);
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Console.WriteLine("Error Obteniendo ubicacion: " + fneEx);
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine("Error Obteniendo ubicacion: " + pEx);
                // Handle permission exception
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error Obteniendo ubicacion: " + ex);
                // Unable to get location
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
