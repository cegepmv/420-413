using GestionBibliothequeMunicipale.Enums;

namespace GestionBibliothequeMunicipale
{
    internal class Livre : Document, IEmpruntable
    {
        private const int dureeMaximaleEmpruntLivreDefaut = 14;
        public string Auteur { get; set; }
        public string ISBN { get; set; }
        public GenreLivre Genre { get; set; }
        public int NombrePages { get; set; }
        public int NombreExemplaires { get; set; }
        public int NombreDisponibles { get; set; }

        public override string TypeDocument => "Livre";

        public Livre() : base()
        {
            NombreExemplaires = 1;
            NombreDisponibles = 1;
        }

        public bool Emprunter()
        {
            if (!PeutEtreEmprunte())
            {
                return false;
            }

            NombreDisponibles--;

            EstDisponible = NombreDisponibles > 0;

            return true;
        }

        public int ObtenirDureeEmprunt(TypeMembre typeMembre)
        {
            return typeMembre switch
            {
                TypeMembre.Regulier => dureeMaximaleEmpruntLivreDefaut,
                TypeMembre.Etudiant or TypeMembre.Senior => 21, // 21 est le nombre de jour maximal d'emprunt pour un étudiant ou un sénior 
                _ => dureeMaximaleEmpruntLivreDefaut
            };
        }

        public override int ObtenirDureeEmpruntDefaut()
        {
            return dureeMaximaleEmpruntLivreDefaut;
        }

        public bool PeutEtreEmprunte()
        {
            return NombreDisponibles > 0;
        }

        public void Retourner()
        {
            NombreDisponibles++;
            EstDisponible = true;
        }

        public override string ObtenirDescription()
        {
            return $"{Titre} par {Auteur} ({AnneePublication})";
        }

        public override void AfficherInfos()
        {
            Console.WriteLine($"[Livre] {Titre} par {Auteur} | Genre: {Genre} | Pages: {NombrePages} | " +
                          $"Exemplaires: {NombreDisponibles}/{NombreExemplaires} | ISBN: {ISBN}");
        }
    }
}
