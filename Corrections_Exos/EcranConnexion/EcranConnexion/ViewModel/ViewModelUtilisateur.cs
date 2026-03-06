using EcranConnexion.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcranConnexion.ViewModel
{
    internal class ViewModelUtilisateur : INotifyPropertyChanged
    {
        Utilisateur utilisateur;

        public ViewModelUtilisateur()
        {
            utilisateur = new Utilisateur();
        }

        public string Nom
        {
            get => utilisateur.Nom;
            set
            {
                utilisateur.Nom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nom"));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
