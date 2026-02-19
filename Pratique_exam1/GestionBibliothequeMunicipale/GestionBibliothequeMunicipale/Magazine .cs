using GestionBibliothequeMunicipale.Enums;

namespace GestionBibliothequeMunicipale
{
    internal class Magazine: Document, IEmpruntable
{
    public int NumeroEdition { get; set; }
    public string Editeur { get; set; }
    public int Periodicite { get; set; }

    //cette syntaxe est équivalent à get {return "Magazine"; }
    public override string TypeDocument => "Magazine";

    public override string ObtenirDescription()
    {
        return $"{Titre} #{NumeroEdition} - {Editeur}";
    }

    public override void AfficherInfos()
    {
        Console.WriteLine($"[Magazine] {Titre} #{NumeroEdition} — Éditeur: {Editeur} — Périodicité: {Periodicite}j — Disponible: {(EstDisponible ? "Oui" : "Non")}");
    }

    public override int ObtenirDureeEmpruntDefaut() => 7;

        // IEmpruntable
        //Syntaxe équivalent à public bool PeutEtreEmprunte(){ return EstDisponible;}
        public bool PeutEtreEmprunte() => EstDisponible;

    public bool Emprunter()
    {
        if (!PeutEtreEmprunte())
        {
            return false;
        }

        EstDisponible = false;

        return true;
    }

    public void Retourner()
    {
        EstDisponible = true;
    }

    public int ObtenirDureeEmprunt(TypeMembre typeMembre) => 7;
}
}
