using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcranConnexion.Model
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set;}
        public string Prenom { get; set; }
        public string MotDePasse { get; set; }

    }
}
