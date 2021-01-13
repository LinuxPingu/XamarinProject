using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinProject.Service
{
    public interface IFirebaseAuth
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);

        Task<string> SingUpWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();

    }
}
