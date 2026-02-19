using GestionBibliothequeMunicipale.Enums;

namespace GestionBibliothequeMunicipale
{
    internal class Membre
    {
        private static int prochainId = 1;

        public int Id { get; private set; }
        public string Nom { get; set; }
        public string NumeroMembre { get; set; }
        public string Courriel { get; set; }
        public TypeMembre Type { get; set; }
        public DateTime DateInscription { get; set; }
        public List<Emprunt> Emprunts { get; set; }

        public int NombreEmpruntsActuels
        {
            get
            {
                return Emprunts.Count(e => e.Statut == StatutEmprunt.EnCours);
            }
        }
        
        //autre façon d'écrire le getter
        public int NombreEmpruntsTotal => Emprunts.Count;

        public int LimiteEmprunts => Type switch
        {
            TypeMembre.Regulier => 5,
            TypeMembre.Etudiant => 10,
            TypeMembre.Senior => 8,
            _ => 5
        };

        public bool PeutEmprunter => NombreEmpruntsActuels < LimiteEmprunts;

        public int JoursMembre => (int)(DateTime.Now - DateInscription).TotalDays;

        public Membre()
        {
            Id = prochainId++;
            Emprunts = new List<Emprunt>();
            DateInscription = DateTime.Now;
        }

        public void AjouterEmprunt(Emprunt emprunt) => Emprunts.Add(emprunt);

        public override string ToString() => $"{NumeroMembre} - {Nom} ({Type})";
    }

}
