using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
     public class UsersInfo
    {
        public string UID { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string FechaNac { get; set; }

        public string telefono { get; set; }

        public UsersInfo()
        {
        }

        public UsersInfo(string uID, string nombre, string apellidos, string fechaNac, string telefono)
        {
            UID = uID;
            Nombre = nombre;
            Apellidos = apellidos;
            FechaNac = fechaNac;
            this.telefono = telefono;
        }



    }
}
