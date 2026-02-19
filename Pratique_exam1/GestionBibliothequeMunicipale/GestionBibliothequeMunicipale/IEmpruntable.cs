using GestionBibliothequeMunicipale.Enums;

namespace GestionBibliothequeMunicipale
{
    internal interface IEmpruntable
    {
        public bool PeutEtreEmprunte();
        bool Emprunter();
        void Retourner();
        int ObtenirDureeEmprunt(TypeMembre typeMembre);
    }
}
