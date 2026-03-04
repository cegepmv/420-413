using GestionBibliothequeMunicipale.Enums;

namespace GestionBibliothequeMunicipale
{
    internal class Emprunt
    {
        private static int prochainId = 1;
        public int Id { get; private set; }
        public IEmpruntable Document { get; set; }
        public Membre Membre { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRetourPrevue { get; set; }
        public DateTime? DateRetourReelle { get; set; }
        public StatutEmprunt Statut { get; set; }

        public int DureeEmprunt => Document.ObtenirDureeEmprunt(Membre.Type);
        public int JoursEmprunt => (int)(DateTime.Now - DateEmprunt).TotalDays;
        public bool EstEnRetard => DateTime.Now > DateRetourPrevue && Statut == StatutEmprunt.EnCours;
        public int JoursRetard => EstEnRetard ? (int)(DateTime.Now - DateRetourPrevue).TotalDays : 0;
        public decimal Penalite => JoursRetard * 0.50m;

        public Emprunt(IEmpruntable document, Membre membre)
        {
            Id = prochainId++;
            Document = document;
            Membre = membre;
            DateEmprunt = DateTime.Now;
            int duree = document.ObtenirDureeEmprunt(membre.Type);
            DateRetourPrevue = DateEmprunt.AddDays(duree);
            Statut = StatutEmprunt.EnCours;
        }

        public void Retourner()
        {
            DateRetourReelle = DateTime.Now;
            Statut = EstEnRetard ? StatutEmprunt.EnRetard : StatutEmprunt.Retourne;
        }

        public override string ToString()
        {
            string titre = Document is Document doc ? doc.Titre : "Inconnu";
            return $"{titre} - {Membre.Nom} - {Statut}";
        }
    }
}
