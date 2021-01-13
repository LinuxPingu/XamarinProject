using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinProject.Service;
using Xamarin.Forms;
using Firebase.Auth;
using Firebase;
using Firebase.Database;

namespace XamarinProject.Droid
{
    public class AuthAndroid : IFirebaseAuth
    {

       public static FirebaseClient firebase = new FirebaseClient("https://burrosingles.firebaseio.com/");

        public bool IsSignIn()
        {
          
            throw new NotImplementedException();

        }

        public Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool SignOut()
        {
            throw new NotImplementedException();
        }

        public Task<string> SingUpWithEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}