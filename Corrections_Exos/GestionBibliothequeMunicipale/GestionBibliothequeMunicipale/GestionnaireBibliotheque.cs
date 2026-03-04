using GestionBibliothequeMunicipale.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBibliothequeMunicipale
{
    internal class GestionnaireBibliotheque
    {
        public List<Document> Documents { get; set; }
        public List<Membre> Membres { get; set; }
        public List<Emprunt> Emprunts { get; set; }

        public GestionnaireBibliotheque()
        {
            Documents = new List<Document>();
            Membres = new List<Membre>();
            Emprunts = new List<Emprunt>();
        }

        // --- 2.1 Gestion de base ---

        public void AjouterDocument(Document document)
        {
            if (Documents.Any(d => d.Id == document.Id))
            {
                throw new InvalidOperationException($"Un document avec l'ID {document.Id} existe déjà.");
            }

            Documents.Add(document);
        }

        public void AjouterMembre(Membre membre)
        {
            if (Membres.Any(m => m.Courriel.Equals(membre.Courriel, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Un membre avec le courriel {membre.Courriel} existe déjà.");
            }

            Membres.Add(membre);
        }

        public bool CreerEmprunt(int documentId, int membreId)
        {
            Document? document = Documents.FirstOrDefault(d => d.Id == documentId);
            if (document == null) return false;

            var empruntable = document as IEmpruntable;
            if (empruntable == null)
            {
                return false;
            }

            Membre? membre = Membres.FirstOrDefault(m => m.Id == membreId);
            if (membre == null)
            {
                return false;
            }

            if (!membre.PeutEmprunter)
            {
                return false;
            }

            if (!empruntable.PeutEtreEmprunte())
            {
                return false;
            }

            Emprunt emprunt = new Emprunt(empruntable, membre);
            Emprunts.Add(emprunt);
            membre.AjouterEmprunt(emprunt);
            empruntable.Emprunter();
            
            return true;
        }

        public bool RetournerDocument(int empruntId)
        {
            Emprunt? emprunt = Emprunts.FirstOrDefault(e => e.Id == empruntId);
            if (emprunt == null)
            {
                return false;
            }

            emprunt.Retourner();
            emprunt.Document.Retourner();

            return true;
        }

        // --- 2.2 Recherches LINQ ---

        public List<Document> ObtenirDocumentsDisponibles()
        { 
            return Documents.Where(d => d.EstDisponible)
                        .OrderBy(d => d.Titre)
                        .ToList();
        }

        public List<Livre> ObtenirLivresParGenre(GenreLivre genre)
        {
            return Documents.OfType<Livre>()
                            .Where(l => l.Genre == genre)
                            .OrderByDescending(l => l.AnneePublication)
                            .ToList();
        }

        public List<Magazine> ObtenirMagazinesRecents()
        {
            return Documents.OfType<Magazine>()
                        .Where(m => m.EstRecent)
                        .OrderByDescending(m => m.NumeroEdition)
                        .ToList();
        }

        public List<Document> ObtenirDocumentsParAuteur(string auteur)
        {
            return Documents.OfType<Livre>()
                        .Where(l => l.Auteur.Contains(auteur, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(l => l.Titre)
                        .Cast<Document>()
                        .ToList();
        }

        public List<Emprunt> ObtenirEmpruntsEnRetard()
        {
            return Emprunts.Where(e => e.EstEnRetard)
                       .OrderByDescending(e => e.JoursRetard)
                       .ToList();
        }

        // --- 2.3 Statistiques LINQ ---

        public int CompterDocumentsParType(string typeDocument)
        {
            return Documents.Count(d => d.TypeDocument == typeDocument);
        }

        public int CompterLivresParGenre(GenreLivre genre)
        {
            return Documents.OfType<Livre>().Count(l => l.Genre == genre);
        }

        public decimal CalculerPenalitesTotales()
            => Emprunts.Sum(e => e.Penalite);

        public double CalculerMoyenneEmpruntsParMembre()
            => Membres.Any() ? Membres.Average(m => m.NombreEmpruntsTotal) : 0;


        // --- 2.4 Requêtes avancées ---

        public Dictionary<string, int> ObtenirStatistiquesParTypeDocument()
        {
            return Documents.GroupBy(d => d.TypeDocument)
                        .OrderByDescending(g => g.Count())
                        .ToDictionary(g => g.Key, g => g.Count());
        }

        public IEnumerable<object> ObtenirStatistiquesCompletes()
        {
            return Documents.GroupBy(d => d.TypeDocument)
                        .Select(g => (object)new
                        {
                            TypeDocument = g.Key,
                            Nombre = g.Count(),
                            NombreDisponibles = g.Count(d => d.EstDisponible),
                            TauxDisponibilite = g.Count() > 0
                                ? Math.Round((double)g.Count(d => d.EstDisponible) / g.Count() * 100, 1)
                                : 0.0
                        })
                        .ToList();
        }

        public List<IEmpruntable> ObtenirDocumentsEmpruntables()
        {
            return Documents.OfType<IEmpruntable>()
                        .Where(e => e.PeutEtreEmprunte())
                        .ToList();
        }

        public Dictionary<TypeMembre, List<Membre>> GrouperMembresParType()
        {
            return Membres.GroupBy(m => m.Type)
                      .ToDictionary(g => g.Key, g => g.ToList());
        }

        public IEnumerable<object> ObtenirTop5MembresActifs()
        {
            return Membres.OrderByDescending(m => m.NombreEmpruntsTotal)
                      .Take(5)
                      .Select(m => (object)new { Nom = m.Nom, NombreEmprunts = m.NombreEmpruntsTotal })
                      .ToList();
        }

        public List<Emprunt> ObtenirHistoriqueMembreParType(int membreId, string typeDocument)
        {
            return Emprunts.Where(e => e.Membre.Id == membreId &&
                                   e.Document is Document doc &&
                                   doc.TypeDocument == typeDocument)
                       .OrderByDescending(e => e.DateEmprunt)
                       .ToList();
        }

        // --- 2.5 Polymorphisme ---

        public void AfficherTousLesDocuments()
        {
            foreach (var document in Documents)
            {
                document.AfficherInfos();   // appel polymorphe
            }
        }

        public List<string> ObtenirDescriptionsTousDocuments()
        {
            return Documents.Select(d => d.ObtenirDescription()).ToList();  // appel polymorphe
        }

        public Dictionary<string, int> CalculerDureeMoyenneParType()
        {
            return Documents.GroupBy(d => d.TypeDocument)
                        .ToDictionary(
                            g => g.Key,
                            g => (int)g.OfType<IEmpruntable>()
                                       .Average(e => e.ObtenirDureeEmprunt(TypeMembre.Regulier))
                        );
        }
    }
}
