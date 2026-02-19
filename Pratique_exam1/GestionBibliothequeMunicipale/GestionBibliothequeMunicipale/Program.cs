using GestionBibliothequeMunicipale.Enums;

namespace GestionBibliothequeMunicipale
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestionnaireBibliotheque gestionnaire = new GestionnaireBibliotheque();

            // ── Données de test : Livres ──────────────────────────────────────────
            var livres = new[]
            {
                new Livre { Titre="1984",                Auteur="George Orwell",            ISBN="978-0-452-28423-4", Genre=GenreLivre.ScienceFiction, NombrePages=328, AnneePublication=1949, NombreExemplaires=3, NombreDisponibles=3 },
                new Livre { Titre="Le Petit Prince",     Auteur="Antoine de Saint-Exupéry", ISBN="978-2-07-040850-4", Genre=GenreLivre.Jeunesse,       NombrePages=96,  AnneePublication=1943, NombreExemplaires=2, NombreDisponibles=2 },
                new Livre { Titre="Dune",                Auteur="Frank Herbert",             ISBN="978-0-441-17271-9", Genre=GenreLivre.ScienceFiction, NombrePages=688, AnneePublication=1965, NombreExemplaires=2, NombreDisponibles=2 },
                new Livre { Titre="L'Étranger",          Auteur="Albert Camus",              ISBN="978-2-07-036024-7", Genre=GenreLivre.Roman,          NombrePages=186, AnneePublication=1942, NombreExemplaires=1, NombreDisponibles=1 },
                new Livre { Titre="Sapiens",             Auteur="Yuval Noah Harari",         ISBN="978-2-226-25576-1", Genre=GenreLivre.Histoire,       NombrePages=512, AnneePublication=2011, NombreExemplaires=2, NombreDisponibles=2 },
                new Livre { Titre="Sherlock Holmes",     Auteur="Arthur Conan Doyle",        ISBN="978-2-07-040751-4", Genre=GenreLivre.Policier,       NombrePages=307, AnneePublication=1892, NombreExemplaires=1, NombreDisponibles=1 },
                new Livre { Titre="Une brève histoire",  Auteur="Stephen Hawking",           ISBN="978-2-07-033071-4", Genre=GenreLivre.Science,        NombrePages=229, AnneePublication=1988, NombreExemplaires=1, NombreDisponibles=1 },
                new Livre { Titre="Astérix le Gaulois",  Auteur="René Goscinny",             ISBN="978-2-01-210001-7", Genre=GenreLivre.BD,             NombrePages=48,  AnneePublication=1961, NombreExemplaires=2, NombreDisponibles=2 },
            };

            foreach (var l in livres)
            {
                gestionnaire.AjouterDocument(l);
            }

            // ── Données de test : Magazines ───────────────────────────────────────
            var magazines = new[]
            {
                new Magazine { Titre="Science et Vie",       NumeroEdition=125, Editeur="Mondadori",  Periodicite=30, AnneePublication=2024 },
                new Magazine { Titre="Le Devoir",            NumeroEdition=210, Editeur="Le Devoir",  Periodicite=7,  AnneePublication=2025 },
                new Magazine { Titre="National Geographic",  NumeroEdition=88,  Editeur="NGS",        Periodicite=30, AnneePublication=2023 },
                new Magazine { Titre="Québec Science",       NumeroEdition=52,  Editeur="Québec Sc.", Periodicite=30, AnneePublication=2025 },
            };

            foreach (var m in magazines)
            {
                gestionnaire.AjouterDocument(m);
            }

            // ── Données de test : Membres ─────────────────────────────────────────
            var membres = new[]
            {
                new Membre { Nom="Alice Tremblay", NumeroMembre="MEM-00001", Courriel="alice@email.com",   Type=TypeMembre.Etudiant },
                new Membre { Nom="Bob Gagnon",     NumeroMembre="MEM-00002", Courriel="bob@email.com",     Type=TypeMembre.Regulier },
                new Membre { Nom="Charlie Roy",    NumeroMembre="MEM-00003", Courriel="charlie@email.com", Type=TypeMembre.Senior   },
                new Membre { Nom="Diana Côté",     NumeroMembre="MEM-00004", Courriel="diana@email.com",   Type=TypeMembre.Etudiant },
                new Membre { Nom="Eric Bouchard",  NumeroMembre="MEM-00005", Courriel="eric@email.com",    Type=TypeMembre.Regulier },
            };
            foreach (var m in membres)
            {
                gestionnaire.AjouterMembre(m);
            }

            // ── Emprunts normaux ──────────────────────────────────────────────────
            gestionnaire.CreerEmprunt(livres[0].Id, membres[0].Id);   // 1984 — Alice
            gestionnaire.CreerEmprunt(livres[2].Id, membres[1].Id);   // Dune — Bob
            gestionnaire.CreerEmprunt(magazines[0].Id, membres[2].Id); // Science et Vie — Charlie
            gestionnaire.CreerEmprunt(livres[4].Id, membres[3].Id);   // Sapiens — Diana
            gestionnaire.CreerEmprunt(magazines[1].Id, membres[4].Id); // Le Devoir — Eric

            // ── Emprunts en retard (simulés) ──────────────────────────────────────
            Emprunt empruntRetard1 = new Emprunt(livres[1], membres[0]);
            empruntRetard1.DateEmprunt = DateTime.Now.AddDays(-30);
            empruntRetard1.DateRetourPrevue = DateTime.Now.AddDays(-16);
            gestionnaire.Emprunts.Add(empruntRetard1);
            membres[0].AjouterEmprunt(empruntRetard1);

            var EmpruntRetard2 = new Emprunt(magazines[2], membres[1]);
            EmpruntRetard2.DateEmprunt = DateTime.Now.AddDays(-15);
            EmpruntRetard2.DateRetourPrevue = DateTime.Now.AddDays(-8);
            gestionnaire.Emprunts.Add(EmpruntRetard2);
            membres[1].AjouterEmprunt(EmpruntRetard2);

            // ── Emprunts retournés (pour l'historique) ────────────────────────────
            gestionnaire.CreerEmprunt(livres[5].Id, membres[2].Id);
            gestionnaire.RetournerDocument(gestionnaire.Emprunts.Last().Id);

            gestionnaire.CreerEmprunt(livres[6].Id, membres[0].Id);
            gestionnaire.RetournerDocument(gestionnaire.Emprunts.Last().Id);

            gestionnaire.CreerEmprunt(livres[7].Id, membres[3].Id);
            gestionnaire.RetournerDocument(gestionnaire.Emprunts.Last().Id);

            // =============================================================================
            // AFFICHAGE
            // =============================================================================

            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("       SYSTÈME DE GESTION - BIBLIOTHÈQUE MUNICIPALE    ");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // Tous les documents — polymorphisme
            Console.WriteLine("📖 TOUS LES DOCUMENTS (affichage polymorphe):");
            gestionnaire.AfficherTousLesDocuments();
            Console.WriteLine();

            // Statistiques par type
            Console.WriteLine("📊 STATISTIQUES PAR TYPE DE DOCUMENT:");
            foreach (dynamic s in gestionnaire.ObtenirStatistiquesCompletes())
                Console.WriteLine($"   • {s.TypeDocument}: {s.Nombre} documents ({s.NombreDisponibles} disponibles) - {s.TauxDisponibilite}%");
            Console.WriteLine();

            // Documents disponibles
            Console.WriteLine("📚 DOCUMENTS DISPONIBLES:");
            foreach (var d in gestionnaire.ObtenirDocumentsDisponibles())
                Console.WriteLine($"   • {d.ObtenirDescription()}");
            Console.WriteLine();

            // Emprunts en retard
            Console.WriteLine("⏰ EMPRUNTS EN RETARD:");
            var listEmpruntEnRetard = gestionnaire.ObtenirEmpruntsEnRetard();
            if (!listEmpruntEnRetard.Any())
                Console.WriteLine("   Aucun emprunt en retard.");
            foreach (var empruntEnRetard in listEmpruntEnRetard)
            {
                string titre = empruntEnRetard.Document is Document docE ? docE.Titre : "Inconnu";
                Console.WriteLine($"   • {titre} — {empruntEnRetard.Membre.Nom} — {empruntEnRetard.JoursRetard} jours — {empruntEnRetard.Penalite:C}");
            }
            Console.WriteLine();

            // Pénalités totales
            Console.WriteLine($"💰 PÉNALITÉS TOTALES: {gestionnaire.CalculerPenalitesTotales():C}");
            Console.WriteLine();

            // Top 5 membres actifs
            Console.WriteLine("🏆 TOP 5 MEMBRES ACTIFS:");
            int rang = 1;
            foreach (dynamic m in gestionnaire.ObtenirTop5MembresActifs())
                Console.WriteLine($"   {rang++}. {m.Nom} — {m.NombreEmprunts} emprunt(s)");
            Console.WriteLine();

            // Descriptions polymorphes
            Console.WriteLine("📝 DESCRIPTIONS (polymorphisme via ObtenirDescription):");
            foreach (var desc in gestionnaire.ObtenirDescriptionsTousDocuments())
                Console.WriteLine($"   • {desc}");
            Console.WriteLine();

            // Livres de science-fiction
            Console.WriteLine("🚀 LIVRES DE SCIENCE-FICTION:");
            foreach (var l in gestionnaire.ObtenirLivresParGenre(GenreLivre.ScienceFiction))
                Console.WriteLine($"   • {l.ObtenirDescription()}");
            Console.WriteLine();

            // Magazines récents
            Console.WriteLine("📰 MAGAZINES RÉCENTS (≤ 5 ans):");
            foreach (var m in gestionnaire.ObtenirMagazinesRecents())
                Console.WriteLine($"   • {m.ObtenirDescription()}");
            Console.WriteLine();

            // Durée moyenne par type
            Console.WriteLine("⏱  DURÉE MOYENNE D'EMPRUNT PAR TYPE (membre Régulier):");
            foreach (var kv in gestionnaire.CalculerDureeMoyenneParType())
                Console.WriteLine($"   • {kv.Key}: {kv.Value} jours");

            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("                     FIN DU PROGRAMME                  ");
            Console.WriteLine("═══════════════════════════════════════════════════════");
        }
    }
}
