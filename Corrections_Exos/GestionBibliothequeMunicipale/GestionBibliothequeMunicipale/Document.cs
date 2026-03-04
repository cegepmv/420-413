namespace GestionBibliothequeMunicipale
{
    internal abstract class Document
    {
        private static int _prochainId = 1;
        public int AnneePublication { get; set; }

        public string Titre { get; set; }
        public int Id { get; private set; }

        public bool EstDisponible { get; set; }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - AnneePublication;
            }
        }

        public bool EstRecent
        {
            get
            {
                // Si l'age du document est inférieur à 5 ans, on le considère comme récent
                return Age <= 5;
            }
        }

        public abstract string TypeDocument { get; }


        protected Document()
        {
            Id = _prochainId++;
            EstDisponible = true;
        }

        public abstract int ObtenirDureeEmpruntDefaut();

        public virtual string ObtenirDescription()
        {
            return $"{Titre} ({AnneePublication})";
        }

        public virtual void AfficherInfos()
        {
            Console.WriteLine($"[{TypeDocument}] {Titre} — Publié: {AnneePublication} — Disponible: {(EstDisponible ? "Oui" : "Non")}");
        }

        public override string ToString()
        {
            return ObtenirDescription();
        }
    }
}