using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinProject.Models;


namespace XamarinProject.Service
{
    public class FirebaseService
    {

        #region Singleton

        public static FirebaseService instance = null;
        public static string WebApiKey = "AIzaSyA1MaIiIySg3au3dkTDMkPkavHPQ6Q7O9c";
        public static FirebaseClient firebase = new FirebaseClient("https://burrosingles.firebaseio.com/");

        public static FirebaseService GetInstance()
        {
            if (instance == null)
                instance = new FirebaseService();

            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
                instance = null;
        }




        #endregion

        public FirebaseService()
        {

        }



        #region GetAllUsers




        #endregion

        #region Register User

        public async Task<bool> RegisterUser(UsersTable user)
        {
            bool chk = false;
            try
            {
                await firebase.Child("Users").PostAsync(new UsersTable() { UserID = user.UserID, Email = user.Email, Username = user.Username, Password = user.Password });
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);

            }

            return chk;
        }
        #endregion

        #region Get All Users

        public async Task<List<UsersTable>> getAllUsers()
        {
            var list = (await firebase.Child("Users").OnceAsync<UsersTable>()).Select(item => new UsersTable
            {
                UserID = item.Object.UserID,
                Email = item.Object.Email,
                Username = item.Object.Username,
                Password = item.Object.Password
            }).ToList();
            return list;
        }

        #endregion

        #region Get User by ID

        public async Task<UsersTable> getUserByID(string id)
        {
            var allUsers = await this.getAllUsers();
            await firebase.Child("Users").OnceAsync<UsersTable>();
            return allUsers.Where(a => a.UserID == id).FirstOrDefault();
        }
        #endregion

        #region Edit Account

        public async Task<bool> editAccount(UsersTable user)
        {
            bool chk = false;
            try
            {
                var toUpdate = (await firebase.Child("Users").OnceAsync<UsersTable>()).Where(a => a.Object.UserID == user.UserID).FirstOrDefault();
                await firebase.Child("Users").Child(toUpdate.Key).PutAsync(new UsersTable { UserID = user.UserID, Email = user.Email, Username = user.Username, Password = user.Password });
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Actualizando: " + e);
            }

            return chk;
        }
        #endregion

        #region Get All Personal Info
        public async Task<List<UsersInfo>> allPersonalInfoExs()
        {

            var list = (await firebase.Child("UsersInfo").OnceAsync<UsersInfo>()).Select(item => new UsersInfo
            {
                UID = item.Object.UID,
                Nombre = item.Object.Nombre,
                Apellidos = item.Object.Apellidos,
                FechaNac = item.Object.FechaNac,
                telefono = item.Object.telefono
            }).ToList();
            return list;

        }

        #endregion

        #region Info Exs

        public async Task<UsersInfo> getPersonalInfoByID(string id)
        {
            var allUsers = await this.allPersonalInfoExs();
            await firebase.Child("UsersInfo").OnceAsync<UsersInfo>();
            return allUsers.Where(a => a.UID == id).FirstOrDefault();
        }

        #endregion

        #region Save Personal Info

        public async Task<bool> SaveUserInfo(UsersInfo user)
        {
            bool chk = false;
            try
            {
                await firebase.Child("UsersInfo").PostAsync(new UsersInfo() { UID = user.UID, Nombre = user.Nombre, Apellidos = user.Apellidos, FechaNac = user.FechaNac, telefono = user.telefono });
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);

            }

            return chk;
        }

        #endregion

        #region Update Personal Info

        public async Task<bool> editPersonalInfo(UsersInfo user)
        {
            bool chk = false;
            try
            {
                var toUpdate = (await firebase.Child("UsersInfo").OnceAsync<UsersInfo>()).Where(a => a.Object.UID == user.UID).FirstOrDefault();
                await firebase.Child("UsersInfo").Child(toUpdate.Key).PutAsync(new UsersInfo { UID = user.UID, Nombre = user.Nombre, Apellidos = user.Apellidos, FechaNac = user.FechaNac, telefono = user.telefono });
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Actualizando: " + e);
            }

            return chk;
        }


        #endregion

        #region Get All Ubications
        public async Task<List<UsersLocation>> getAllUbications()
        {
            var list = (await firebase.Child("UsersUbi").OnceAsync<UsersLocation>()).Select(item => new UsersLocation
            {
                UID = item.Object.UID,
                Latitude = item.Object.Latitude,
                Longitude = item.Object.Longitude

            }).ToList();
            return list;
        }

        #endregion

        #region Find Ubication By ID
        public async Task<UsersLocation> findUserUbication(string UID)
        {
            UsersLocation location = null;

            List<UsersLocation> dummyList = await this.getAllUbications();
            try
            {
                location = dummyList.Where(a => a.UID == UID).FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrando por username: " + e);

            }

            return location;

        }

        #endregion

        #region Save Ubication

        public async Task<bool> saveUbication(UsersLocation inLocation)
        {
            bool chk = false;

            UsersLocation found = await this.findUserUbication(inLocation.UID);

            if (found != null)
            {
                /* Update*/

                try
                {
                    var toUpdate = (await firebase.Child("UsersUbi").OnceAsync<UsersLocation>()).Where(a => a.Object.UID == inLocation.UID).FirstOrDefault();
                    await firebase.Child("UsersUbi").Child(toUpdate.Key).PutAsync(new UsersLocation { UID = inLocation.UID, Latitude = inLocation.Latitude, Longitude = inLocation.Longitude });
                    chk = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error Actualizando: " + e);
                }

            }
            else
            {
                /*Save*/


                try
                {
                    await firebase.Child("UsersUbi").PostAsync(new UsersLocation() { UID = inLocation.UID, Latitude = inLocation.Latitude, Longitude = inLocation.Longitude });
                    chk = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);

                }



            }

            return chk;
        }

        #endregion

        #region Find User By ID

        public async Task<UsersTable> findUser(string identifier)
        {
            UsersTable user = null;

            List<UsersTable> dummyList = await this.getAllUsers();
            try
            {
                user = dummyList.Where(a => a.Username == identifier || a.Email == identifier).FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrando por username: " + e);

            }


            return user;
        }
        #endregion

        #region Get All Store Items

        public async Task<List<StoreItemModel>> getStoreItems()
        {
            var list = (await firebase.Child("StoreItems").OnceAsync<StoreItemModel>()).Select(item => new StoreItemModel
            {
                ItemID = item.Object.ItemID,
                Title = item.Object.Title,
                Image = item.Object.Image,
                Description = item.Object.Description,
                Price = item.Object.Price
            }).ToList();
            return list;
        }

        #endregion

        #region Get Item By Name or ID

        public async Task<StoreItemModel> findItem(string identifier)
        {
            StoreItemModel item = null;

            List<StoreItemModel> dummyList = await this.getStoreItems();
            try
            {
                item = dummyList.Where(a => a.ItemID == identifier || a.Title == identifier).FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrando por username: " + e);

            }


            return item;
        }



        #endregion

        #region Get All Carts

        public async Task<List<CartModel>> getAllCarts()
        {
            var list = (await firebase.Child("UsersCarts").OnceAsync<CartModel>()).Select(item => new CartModel
            {
                UserID = item.Object.UserID,
                CartID = item.Object.CartID,
                Total = item.Object.Total,
                isBought = item.Object.isBought,
                isRevision = item.Object.isRevision

            }).ToList();
            return list;
        }


        #endregion

        #region Get Cart By UID
        public async Task<CartModel> getCartByID(string identifier)
        {
            CartModel cart = null;

            List<CartModel> dummyList = await this.getAllCarts();
            try
            {
                cart = dummyList.Where(a => a.UserID == identifier).FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error encontrando por username: " + e);

            }

            return cart;
        }

        #endregion

        #region Crear Carrito
        public async Task<bool> CreateCart(CartModel carrito)
        {
            bool chk = false;

            try
            {

                await firebase.Child("UsersCarts").PostAsync(new CartModel()
                {
                    UserID = carrito.UserID,
                    CartID = carrito.CartID,
                    Total = carrito.Total,
                    isRevision = true,
                    isBought = false
                });

                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creando carrito: " + e);
            }
            return chk;
        }
        #endregion

        #region Salvar Item

        public async Task<bool> saveItem(CartModel carrito,CartItemModel item)
        {
            bool chk = false;

            CartItemModel itemFound = await this.ifItemExists(carrito,item);

            if (itemFound != null)
            {
                chk = await this.editItem(carrito,item);
            }
            else
            {
                try
                {

                    await firebase.Child("UsersCartItems").PostAsync(new CartItemModel()
                    {
                        
                        ItemID = item.ItemID,
                        CartID = item.CartID,
                        Title = item.Title,
                        Image = item.Image,
                        Quantity = item.Quantity,
                        Subtotal = item.Subtotal
                    });

                    chk = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error agregando item al carrito: " + e);
                }

            }



            return chk;
        }

        #endregion

        #region Editar Item

        public async Task<bool> editItem(CartModel cart, CartItemModel item)
        {
            bool chk = false;
            try
            {
                var toUpdate = (await firebase.Child("UsersCartItems").OnceAsync<CartItemModel>()).Where(a => a.Object.CartID == cart.CartID && a.Object.ItemID == item.ItemID).FirstOrDefault();
                await firebase.Child("UsersCartItems").Child(toUpdate.Key).PutAsync(new CartItemModel
                {
                     
                    CartID = item.CartID,
                    Image = item.Image,
                    Title = item.Title,
                    ItemID = item.ItemID,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal
                });
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Actualizando: " + e);
            }

            return chk;
        }

        #endregion

        #region Get Items by Cart


        public async Task<List<CartItemModel>> getItemsByCart(CartModel cart)
        {
            var list = (await firebase.Child("UsersCartItems").OnceAsync<CartItemModel>()).Select(item => new CartItemModel
            {
                CartID = item.Object.CartID,
                Title = item.Object.Title,
                Image = item.Object.Image,
                ItemID = item.Object.ItemID,
                Quantity = item.Object.Quantity,
                Subtotal = item.Object.Subtotal
            }).Where(a => a.CartID == cart.CartID).ToList();
            return list;
        }

        #endregion

        #region UpdateCarrtio

        public async Task<bool> editCart(CartModel cart)
        {

            bool chk = false;
            try
            {
                var toUpdate = (await firebase.Child("UsersCarts").OnceAsync<CartModel>()).Where(a => a.Object.UserID == cart.UserID).FirstOrDefault();
                await firebase.Child("UsersCarts").Child(toUpdate.Key).PutAsync(new CartModel { UserID = cart.UserID, 
                                                                                                CartID = cart.CartID, 
                                                                                                Total= cart.Total, 
                                                                                                isRevision= cart.isRevision,
                                                                                                isBought=cart.isBought});
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Actualizando: " + e);
            }

            return chk;

        }

        #endregion

        #region Borrar Carrito

        public async Task<bool> deleteCart(CartModel cart)
        {
            bool chk = false;
            try
            {
                var toDelete = (await firebase.Child("UsersCartItems").OnceAsync<CartItemModel>()).Where(a => a.Object.CartID == cart.CartID).FirstOrDefault();
                await firebase.Child("UsersCartItems").Child(toDelete.Key).DeleteAsync();
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Actualizando: " + e);
            }

            return chk;

        }


        #endregion

        #region Place Order

        public async Task<bool> placeOrder(CartModel cart,OrderModel order)
        {
            bool chk = false;

            try
            {

                await this.deleteCart(cart);

                await firebase.Child("UsersOrders").PostAsync(new OrderModel()
                {
                    UserID = order.UserID,
                    OrderID = order.OrderID,
                    Total = order.Total,
                    isRevision = true,
                    isBought=false
                });

                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creando orden "+e);
            }


            return chk;
        }

        #endregion

        #region Borrar Carrito

        public async Task<bool> deleteOrder(OrderModel order)
        {
            bool chk = false;
            try
            {
                var toDelete = (await firebase.Child("UsersOrders").OnceAsync<OrderModel>()).Where(a => a.Object.OrderID == order.OrderID).FirstOrDefault();
                await firebase.Child("UsersOrders").Child(toDelete.Key).DeleteAsync();
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Actualizando: " + e);
            }

            return chk;

        }


        #endregion

        #region if Item Ext

        public async Task<CartItemModel> ifItemExists(CartModel carrito, CartItemModel item)
        {

            CartItemModel itemFound = null;
            List<CartItemModel> dummyList = await this.getItemsByCart(carrito);
            try
            {
                itemFound = dummyList.Where(a => a.ItemID == item.ItemID).FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error Buscando item : " + e);

            }



            return itemFound;

        }


        #endregion

        #region Delete item


        public async Task<bool> deleteItem(CartModel carrito, CartItemModel item)
        {
            bool chk = false;
            try
            {
                var toDelete = (await firebase.Child("UsersCartItems").OnceAsync<CartItemModel>()).Where(a => a.Object.CartID == carrito.CartID && a.Object.ItemID == item.ItemID).FirstOrDefault();
                await firebase.Child("UsersCartItems").Child(toDelete.Key).DeleteAsync();
                chk = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Actualizando: " + e);
            }

            return chk;

        }

        #endregion


        #region get cart Item


        public async Task<CartItemModel> getCartItem(CartModel carrito, StoreItemModel item)
        {

            CartItemModel itemFound = null;
            List<CartItemModel> dummyList = await this.getItemsByCart(carrito);
            try
            {
                itemFound = dummyList.Where(a => a.ItemID == item.ItemID).FirstOrDefault();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error Buscando item : " + e);

            }

            return itemFound;

        }

        #endregion

        #region Get Orders By User


        public async Task<List<OrderModel>> getOrdersByUser(string UID)
        {
            var list = (await firebase.Child("UsersOrders").OnceAsync<OrderModel>()).Select(item => new OrderModel
            {
                OrderID = item.Object.OrderID,
                UserID = item.Object.UserID,
                Total = item.Object.Total,
                isBought = item.Object.isBought,
                isRevision = item.Object.isRevision
                 
            }).Where(a => a.UserID == UID).ToList();
            return list;
        }

        #endregion

    }
}
