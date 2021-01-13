using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinProject.Helpers;
using XamarinProject.Models;
using XamarinProject.Service;
using XamarinProject.Views;

namespace XamarinProject.ViewModels
{
    class StoreViewModel : INotifyPropertyChanged
    {
        #region Singleton

        public static StoreViewModel instance = null;

        public StoreViewModel()
        {
            InitCommands();
            InitClass();
        }

        public static StoreViewModel getInstance()
        {
            if (instance == null)
            {
                instance = new StoreViewModel();
            }
            return instance;
        }

        public static void deleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        #endregion


        #region Variables

        #region Store Items
        private ObservableCollection<StoreItemModel> lstStoreItems = new ObservableCollection<StoreItemModel>();

        public ObservableCollection<StoreItemModel> LstStoreItems
        {
            get { return lstStoreItems; }

            set
            {
                lstStoreItems = value;
                OnPropertyChanged("LstStoreItems");
            }
        }


        #endregion

        #region Selected Item

        public StoreItemModel selectedItem = new StoreItemModel();

        public StoreItemModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                this.selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }


        #endregion

        #region Quantity

        public int cantidad = 1;

        public int Cantidad
        {
            get
            {
                return this.cantidad;
            }
            set
            {
                this.cantidad = value;
                OnPropertyChanged("Cantidad");
            }
                
        }

        #endregion

        #region Cart

        public CartModel carrito = new CartModel();

        public CartModel Carrito
        {
            get
            {
                return this.carrito;
            }
            set
            {
                carrito = value;
                OnPropertyChanged("Carrito");
            }
        }


        #endregion

        #region Cart Items
        private ObservableCollection<CartItemModel> lstItemsCarrito = new ObservableCollection<CartItemModel>();

        public ObservableCollection<CartItemModel> LstItemsCarrito
        {
            get { return this.lstItemsCarrito; }

            set
            {
                this.lstItemsCarrito = value;
                OnPropertyChanged("LstItemsCarrito");
            }
        }

        #endregion

        #region Order Item
        private ObservableCollection<OrderListItemModel> lstOrders = new ObservableCollection<OrderListItemModel>();

        public ObservableCollection<OrderListItemModel> LstOrders
        {
            get { return this.lstOrders; }

            set
            {
                this.lstOrders = value;
                OnPropertyChanged("LstOrders");
            }
        }

        #endregion

        #endregion

        #region Inits
        public void InitClass()
        {
             this.FillStore();
            this.setUserCart();
        }

        private void InitCommands()
        {
            EnterItemDetailCommand = new Command<string>(this.EnterItemDetail);
            EnterCartItemCommand = new Command<string>(this.EnterCartItem);
            AddItemCommand = new Command(this.AddItem);
            DeleteItemCommand = new Command(this.DeleteItem);
            AddOrderCommand = new Command(this.AddOrder);

        }


        #endregion

        #region Commands and Funtions

        public ICommand EnterItemDetailCommand { get; set; }

        public ICommand EnterCartItemCommand { get; set; }

        public ICommand AddItemCommand { get; set; }

        public ICommand AddOrderCommand { get; set; }

        public ICommand DeleteItemCommand { get; set; }


        public async void EnterItemDetail(string item)
        {
            FirebaseService service = FirebaseService.GetInstance();
            Console.WriteLine("Entra item: "+item);
            this.Cantidad = 1;
            selectedItem = await service.findItem(item);
            if (selectedItem != null)
            {
                Console.WriteLine("Encontrado: " + selectedItem.Title);
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new ItemPreviewView());
            }
          

        }


        public async void EnterCartItem(string item)
        {
            FirebaseService service = FirebaseService.GetInstance();
            Console.WriteLine("Entra item: " + item);

            StoreItemModel foundItem = await service.findItem(item);

            SelectedItem = foundItem;

            if (selectedItem != null)
            {
                Console.WriteLine("Encontrado: " + selectedItem.Title);
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new CartItemView());
            }
        }

        public async void AddItem()
        {
            CartItemModel newItem = new CartItemModel();
            newItem.CartID = Carrito.CartID;
            newItem.ItemID = SelectedItem.ItemID;
            newItem.Image = SelectedItem.Image;
            newItem.Title = SelectedItem.Title;
            newItem.Quantity = this.Cantidad;
            newItem.Subtotal = (SelectedItem.Price * this.Cantidad);

            FirebaseService service = FirebaseService.GetInstance();

            try
            {
                bool chk = await service.saveItem(Carrito, newItem);

                if (chk)
                {
                    this.refreshCart();
                    await App.Current.MainPage.DisplayAlert("Success", "Informacion agregada", "Ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Error al agregar item", "Ok");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error agregando item: "+e);
            }


        }

        public async void DeleteItem()
        {
            FirebaseService service = FirebaseService.GetInstance();

            try
            {
                CartItemModel newItem = new CartItemModel();
                newItem.CartID = Carrito.CartID;
                newItem.ItemID = SelectedItem.ItemID;
                newItem.Image = SelectedItem.Image;
                newItem.Title = SelectedItem.Title;
                newItem.Quantity = this.Cantidad;
                newItem.Subtotal = (SelectedItem.Price * this.Cantidad);

                
                bool chk = await service.deleteItem(Carrito,newItem);

                if (chk)
                {
                    this.refreshCart();
                    await App.Current.MainPage.DisplayAlert("Success", "Item Eliminado!", "Ok");
                }
                else
                {
             
                    await App.Current.MainPage.DisplayAlert("Error", "Error eliminando el item", "Ok");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error eliminadn item: "+e);
            }
        }

        public async void AddOrder()
        {
            try
            {
                FirebaseService service = FirebaseService.GetInstance();
                OrderModel newOrden = new OrderModel();
                newOrden.UserID = Settings.UID;
                newOrden.OrderID = Carrito.CartID;
                newOrden.Total = Carrito.Total;
                newOrden.isRevision = true;
                newOrden.isBought = false;


                bool chk = await service.placeOrder(Carrito,newOrden);


                if (chk)
                {
                    this.setUserCart();
                    this.refreshCart();
                    resfreshOrders();
                    await App.Current.MainPage.DisplayAlert("Success", "Orden Levantada!", "Ok");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Levantando orden", "Ok");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error generando orden: "+e);
            }
        }

        public async void FillStore()
        {
            try
            {
                FirebaseService service = FirebaseService.GetInstance();
                List<StoreItemModel> items = await service.getStoreItems();

                foreach (StoreItemModel i in items)
                {
                    this.LstStoreItems.Add(i);
                }

                Console.WriteLine("Tienda Cargada! ");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error llenando la tienda: "+e);
            }
        }

        public async void setUserCart()
        {
            try
            {
                FirebaseService service = FirebaseService.GetInstance();

                CartModel dummy = await service.getCartByID(Settings.UID);

                bool chk = false;

                if (dummy != null)
                {

                    try
                    {
                        Carrito = dummy;
                        this.LstItemsCarrito.Clear();
                        List<CartItemModel> itemCache = await service.getItemsByCart(Carrito);

                        if (itemCache.Count > 0)
                        {
                            foreach (CartItemModel i in itemCache)
                            {
                                this.LstItemsCarrito.Add(i);                     
                            }
                        }
                        else
                        {
                            Console.WriteLine("Carrito se encontro Vacio!");
                        }

                        resfreshOrders();
                        Console.WriteLine("Carrito: "+Carrito.CartID+", Setteado!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error trayendo info de carrito: "+e);
                    }
                    

                }
                else
                {
                    try
                    {
                        CartModel newCart = new CartModel();
                        newCart.UserID = Settings.UID;
                        newCart.CartID = Guid.NewGuid().ToString();
                        newCart.Total = 0.00;
                        newCart.isRevision = false;
                        newCart.isBought = false;

                        chk = await service.CreateCart(newCart);

                        if (chk)
                        {
                            this.Carrito = newCart;
                            resfreshOrders();
                            Console.WriteLine("Carrito: "+newCart.CartID+", Creado!");
                        }
                        else
                        {
                            Console.WriteLine("Error creando nuevo Carrito! ");
                        }



                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error creando nuevo Carrito: "+e);
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error generando carrito: "+e);
            }
        }

        public async void refreshCart()
        {
            FirebaseService service = FirebaseService.GetInstance();

            this.LstItemsCarrito.Clear();

            List<CartItemModel> itemCache = await service.getItemsByCart(Carrito);

            if (itemCache.Count > 0)
            {
                double suma = 0.00;
                
                foreach (CartItemModel i in itemCache)
                {
                    suma += i.Subtotal;
                    this.LstItemsCarrito.Add(i);
                }

                Carrito.Total = suma;
            }
            else
            {
                Console.WriteLine("Carrito se encontro Vacio!");
            }
        }

        public async void resfreshOrders()
        {
            try
            {
                this.LstOrders.Clear();
                FirebaseService service = FirebaseService.GetInstance();
                List<OrderModel> dummyList = await service.getOrdersByUser(Settings.UID);

                if (dummyList != null)
                {

                    foreach (OrderModel i in dummyList)
                    {
                        OrderListItemModel item = new OrderListItemModel();
                        item.OrderID = i.OrderID;
                        item.Detail = "Orden: "+ i.OrderID+" - Total: "+i.Total;
                        if (i.isBought)
                        {
                            item.Status = "Status: Finalizada";
                        }
                        else
                        {
                            item.Status = "Status: En revision";
                        }

                        this.LstOrders.Add(item);
                    }

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Error refrescando Ordenes: "+e);
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
