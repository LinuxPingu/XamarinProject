using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using XamarinProject.Service;

namespace XamarinProject.iOS
{
    public class AuthIOs : IFirebaseAuth
    {
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