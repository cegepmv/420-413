---
title: "Exercices - POO"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 11
---

# Mini-Projets : Programmation Orientée Objet en C#

## Progression recommandée

| Niveau | Projets |
|--------|---------|
| **Débutant** | Projet 8 (Formes), Projet 5 (Tâches) |
| **Intermédiaire** | Projet 1 (Banque), Projet 2 (Bibliothèque), Projet 6 (Zoo) |
| **Avancé** | Projet 3 (RPG), Projet 4 (Restaurant), Projet 7 (Hôtel) |

## Table des matières
1. [Projet 1 : Système de Gestion Bancaire](#projet-1--système-de-gestion-bancaire)
2. [Projet 2 : Gestion d'une Bibliothèque](#projet-2--gestion-dune-bibliothèque)
3. [Projet 3 : Jeu de Combat RPG](#projet-3--jeu-de-combat-rpg)
4. [Projet 4 : Système de Commandes Restaurant](#projet-4--système-de-commandes-restaurant)
5. [Projet 5 : Gestionnaire de Tâches](#projet-5--gestionnaire-de-tâches)
6. [Projet 6 : Simulateur de Zoo](#projet-6--simulateur-de-zoo)
7. [Projet 7 : Système de Réservation Hôtel](#projet-7--système-de-réservation-hôtel)
8. [Projet 8 : Calculatrice de Formes Géométriques](#projet-8--calculatrice-de-formes-géométriques)

---

## Projet 1 : Système de Gestion Bancaire

### 📋 Objectif
Créer un système de gestion de comptes bancaires avec différents types de comptes et opérations.

### 🎯 Concepts utilisés
- Héritage
- Encapsulation
- Polymorphisme
- Classes abstraites

### 📝 Spécifications

**Créer :**
1. Une classe abstraite `CompteBancaire` avec :
   - Propriétés : NumeroCompte, Titulaire, Solde, DateOuverture
   - Méthodes abstraites : CalculerInterets()
   - Méthodes concrètes : Deposer(), Retirer(), AfficherReleve()

2. Classe `CompteEpargne` héritant de `CompteBancaire` :
   - Propriété : TauxInteret (ex: 2.5%)
   - Minimum de retrait : 10$
   - Implémente CalculerInterets()

3. Classe `CompteCourant` héritant de `CompteBancaire` :
   - Propriété : DecouvertAutorise (ex: -500$)
   - Frais mensuels : 5$
   - Peut retirer jusqu'à atteindre le découvert

4. Classe `CompteJeune` héritant de `CompteEpargne` :
   - Pour les moins de 18 ans
   - Bonus annuel de 50$ si solde > 500$
   - Limite de retrait : 200$ par transaction

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;

namespace GestionBancaire
{
    // Classe abstraite de base
    public abstract class CompteBancaire
    {
        // TODO: Ajouter les propriétés
        
        // TODO: Ajouter le constructeur
        
        public virtual bool Deposer(decimal montant)
        {
            // TODO: Implémenter
            return false;
        }
        
        public abstract bool Retirer(decimal montant);
        
        public abstract decimal CalculerInterets();
        
        public virtual void AfficherReleve()
        {
            // TODO: Implémenter
        }
    }
    
    // TODO: Créer la classe CompteEpargne
    
    // TODO: Créer la classe CompteCourant
    
    // TODO: Créer la classe CompteJeune
    
    // Classe de gestion
    public class Banque
    {
        private List<CompteBancaire> _comptes = new List<CompteBancaire>();
        
        public void AjouterCompte(CompteBancaire compte)
        {
            // TODO: Implémenter
        }
        
        public void AppliquerIntérets()
        {
            // TODO: Parcourir tous les comptes et appliquer les intérêts
        }
        
        public void AfficherTousLesComptes()
        {
            // TODO: Implémenter
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Créer des comptes et tester
            
            Console.WriteLine("=== Système Bancaire ===");
            
            // Créer une banque
            Banque banque = new Banque();
            
            // Créer différents types de comptes
            // ...
            
            // Effectuer des opérations
            // ...
            
            // Appliquer les intérêts
            // ...
            
            // Afficher tous les comptes
            // ...
        }
    }
}
```

### ✅ Critères de réussite
- [ ] Impossible de retirer plus que le solde (sauf compte courant avec découvert)
- [ ] Les intérêts sont correctement calculés
- [ ] Le polymorphisme fonctionne (même méthode, comportements différents)
- [ ] Les encapsulations protègent les données sensibles

---

## Projet 2 : Gestion d'une Bibliothèque

### 📋 Objectif
Créer un système de gestion de bibliothèque avec emprunts et réservations.

### 🎯 Concepts utilisés
- Interfaces
- Héritage
- Collections
- Énumérations

### 📝 Spécifications

**Créer :**

1. Interface `IEmpruntable` :
   - Méthodes : Emprunter(string emprunteur), Retourner()
   - Propriété : EstDisponible

2. Interface `IReservable` :
   - Méthodes : Reserver(string utilisateur), AnnulerReservation()

3. Classe abstraite `Document` :
   - Propriétés : Titre, Auteur, AnneePublication, Cote
   - Méthode abstraite : AfficherDetails()

4. Classe `Livre` héritant de `Document` et implémentant `IEmpruntable`, `IReservable` :
   - Propriétés supplémentaires : ISBN, NombrePages, Genre
   - Durée d'emprunt : 21 jours

5. Classe `DVD` héritant de `Document` et implémentant `IEmpruntable` :
   - Propriétés supplémentaires : Duree (en minutes), Genre
   - Durée d'emprunt : 7 jours

6. Classe `Magazine` héritant de `Document` et implémentant `IEmpruntable` :
   - Propriétés supplémentaires : NumeroEdition, Mois
   - Durée d'emprunt : 14 jours
   - Ne peut pas être réservé

7. Énumération `GenreLivre` : Fiction, NonFiction, ScienceFiction, Romance, Thriller, etc.

8. Classe `Bibliotheque` :
   - Collection de documents
   - Méthodes : AjouterDocument(), RechercherParTitre(), RechercherParAuteur(), ListerDocumentsDisponibles()

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionBibliotheque
{
    public interface IEmpruntable
    {
        // TODO: Définir l'interface
    }
    
    public interface IReservable
    {
        // TODO: Définir l'interface
    }
    
    public enum GenreLivre
    {
        Fiction,
        NonFiction,
        ScienceFiction,
        Romance,
        Thriller,
        Biographie,
        Histoire
    }
    
    public abstract class Document
    {
        // TODO: Propriétés communes
        
        public abstract void AfficherDetails();
    }
    
    public class Livre : Document, IEmpruntable, IReservable
    {
        // TODO: Implémenter
    }
    
    public class DVD : Document, IEmpruntable
    {
        // TODO: Implémenter
    }
    
    public class Magazine : Document, IEmpruntable
    {
        // TODO: Implémenter
    }
    
    public class Bibliotheque
    {
        private List<Document> _documents = new List<Document>();
        
        public void AjouterDocument(Document document)
        {
            // TODO: Implémenter
        }
        
        public List<Document> RechercherParTitre(string titre)
        {
            // TODO: Implémenter
            return null;
        }
        
        public void ListerDocumentsDisponibles()
        {
            // TODO: Implémenter
        }
        
        public void ListerEmprunts()
        {
            // TODO: Afficher tous les documents empruntés
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Bibliotheque biblio = new Bibliotheque();
            
            // TODO: Ajouter des documents
            
            // TODO: Tester les emprunts
            
            // TODO: Tester les réservations
            
            // TODO: Tester les recherches
        }
    }
}
```

### ✅ Critères de réussite
- [ ] Impossible d'emprunter un document déjà emprunté
- [ ] Les réservations fonctionnent uniquement pour les livres
- [ ] La recherche fonctionne correctement
- [ ] L'affichage des détails est polymorphe

---

## Projet 3 : Jeu de Combat RPG

### 📋 Objectif
Créer un mini-jeu de combat avec différentes classes de personnages.

### 🎯 Concepts utilisés
- Héritage
- Polymorphisme
- Classes abstraites
- Interfaces

### 📝 Spécifications

**Créer :**

1. Classe abstraite `Personnage` :
   - Propriétés : Nom, PointsDeVie, PointsDeVieMax, Force, Defense, Niveau
   - Méthode abstraite : AttaqueSpeciale()
   - Méthodes concrètes : Attaquer(Personnage cible), RecevoirDegats(int degats), EstVivant(), Guerir(int points)

2. Classe `Guerrier` héritant de `Personnage` :
   - Compétence : Coup Puissant (2x la force, mais perd 10 PV)
   - Bonus : +5 défense

3. Classe `Mage` héritant de `Personnage` :
   - Propriété supplémentaire : Mana
   - Compétence : Boule de Feu (3x la force, coûte 30 mana)
   - Peut se régénérer (récupère 20 mana par tour)

4. Classe `Archer` héritant de `Personnage` :
   - Propriété supplémentaire : Precision (%)
   - Compétence : Tir Critique (chance de critique basée sur précision)
   - Attaque à distance (peut éviter les contre-attaques)

5. Classe `Paladin` héritant de `Personnage` :
   - Peut se soigner (50% de la force en soins)
   - Compétence : Bouclier Sacré (augmente défense de 50% pour 3 tours)
   - Bonus : Régénération passive (5 PV par tour)

6. Interface `IInventaire` :
   - Méthodes : AjouterObjet(), UtiliserObjet(), AfficherInventaire()

7. Classe `Objet` :
   - Types : Potion (restore PV), PotionMana, ElixirForce (augmente force temporairement)

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;

namespace JeuCombatRPG
{
    public abstract class Personnage
    {
        public string Nom { get; set; }
        public int PointsDeVie { get; protected set; }
        public int PointsDeVieMax { get; protected set; }
        public int Force { get; protected set; }
        public int Defense { get; protected set; }
        public int Niveau { get; protected set; }
        
        protected Personnage(string nom, int pv, int force, int defense)
        {
            // TODO: Initialiser
        }
        
        public virtual int Attaquer(Personnage cible)
        {
            // TODO: Calculer les dégâts (Force - Defense de la cible)
            // Minimum 1 dégât
            return 0;
        }
        
        public void RecevoirDegats(int degats)
        {
            // TODO: Réduire les PV
        }
        
        public abstract void AttaqueSpeciale(Personnage cible);
        
        public bool EstVivant()
        {
            return PointsDeVie > 0;
        }
        
        public void AfficherStats()
        {
            // TODO: Afficher nom, PV, Force, Défense
        }
    }
    
    public class Guerrier : Personnage
    {
        public Guerrier(string nom) : base(nom, 150, 25, 15)
        {
        }
        
        public override void AttaqueSpeciale(Personnage cible)
        {
            // TODO: Coup Puissant
        }
    }
    
    // TODO: Créer les autres classes (Mage, Archer, Paladin)
    
    public class Combat
    {
        public void Duel(Personnage p1, Personnage p2)
        {
            Console.WriteLine($"=== COMBAT: {p1.Nom} VS {p2.Nom} ===\n");
            
            int tour = 1;
            while (p1.EstVivant() && p2.EstVivant())
            {
                Console.WriteLine($"--- Tour {tour} ---");
                
                // TODO: p1 attaque p2
                
                if (!p2.EstVivant())
                {
                    Console.WriteLine($"\n🏆 {p1.Nom} remporte le combat!");
                    break;
                }
                
                // TODO: p2 attaque p1
                
                if (!p1.EstVivant())
                {
                    Console.WriteLine($"\n🏆 {p2.Nom} remporte le combat!");
                    break;
                }
                
                tour++;
                Console.WriteLine();
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Créer des personnages
            
            // TODO: Lancer des combats
            
            // TODO: Tester les attaques spéciales
        }
    }
}
```

### ✅ Critères de réussite
- [ ] Chaque classe a une attaque spéciale unique
- [ ] Le système de combat est équilibré
- [ ] Les statistiques sont correctement affichées
- [ ] Le polymorphisme permet des combats entre différents types

### 🎮 Extensions possibles
- Ajouter un système d'expérience et de montée de niveau
- Implémenter un inventaire d'objets
- Créer un mode tournoi avec plusieurs combattants
- Ajouter des effets de statut (poison, paralysie, etc.)

---

## Projet 4 : Système de Commandes Restaurant

### 📋 Objectif
Créer un système de gestion de commandes pour un restaurant.

### 🎯 Concepts utilisés
- Héritage
- Interfaces
- Collections
- Énumérations

### 📝 Spécifications

**Créer :**

1. Énumération `CategoriePlat` : Entree, PlatPrincipal, Dessert, Boisson

2. Classe abstraite `Article` :
   - Propriétés : Nom, Prix, Description, Categorie
   - Méthode abstraite : CalculerPrix() (pour gérer les options/suppléments)

3. Classe `Plat` héritant de `Article` :
   - Propriétés : Ingredients (liste), TempsPreparation, EstVegetarien
   - Peut avoir des suppléments (fromage +2$, bacon +3$)

4. Classe `Boisson` héritant de `Article` :
   - Propriétés : Taille (Petit, Moyen, Grand), EstGazeuse
   - Prix varie selon la taille

5. Interface `IPersonnalisable` :
   - Méthodes : AjouterOption(string option, decimal prix), RetirerIngredient(string ingredient)

6. Classe `Menu` :
   - Contient une entrée, un plat principal, un dessert et une boisson
   - Prix réduit de 15% par rapport aux articles séparés

7. Classe `Commande` :
   - Propriétés : NumeroCommande, Client, Articles, DateHeure, Statut
   - Méthodes : AjouterArticle(), RetirerArticle(), CalculerTotal(), CalculerTaxes(), AfficherFacture()

8. Énumération `StatutCommande` : EnAttente, EnPreparation, Prete, Livree, Annulee

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemeRestaurant
{
    public enum CategoriePlat
    {
        Entree,
        PlatPrincipal,
        Dessert,
        Boisson
    }
    
    public enum StatutCommande
    {
        EnAttente,
        EnPreparation,
        Prete,
        Livree,
        Annulee
    }
    
    public enum TailleBoisson
    {
        Petit,
        Moyen,
        Grand
    }
    
    public interface IPersonnalisable
    {
        void AjouterOption(string option, decimal prix);
        void RetirerIngredient(string ingredient);
    }
    
    public abstract class Article
    {
        public string Nom { get; set; }
        public decimal PrixBase { get; set; }
        public string Description { get; set; }
        public CategoriePlat Categorie { get; set; }
        
        public abstract decimal CalculerPrix();
        
        public virtual void AfficherDetails()
        {
            Console.WriteLine($"{Nom} - {PrixBase:C}");
            Console.WriteLine($"  {Description}");
        }
    }
    
    public class Plat : Article, IPersonnalisable
    {
        public List<string> Ingredients { get; set; }
        public int TempsPreparation { get; set; } // en minutes
        public bool EstVegetarien { get; set; }
        private Dictionary<string, decimal> _options = new Dictionary<string, decimal>();
        
        public Plat()
        {
            Ingredients = new List<string>();
        }
        
        public void AjouterOption(string option, decimal prix)
        {
            // TODO: Implémenter
        }
        
        public void RetirerIngredient(string ingredient)
        {
            // TODO: Implémenter
        }
        
        public override decimal CalculerPrix()
        {
            // TODO: Prix de base + options
            return 0;
        }
        
        public override void AfficherDetails()
        {
            base.AfficherDetails();
            Console.WriteLine($"  Temps de préparation: {TempsPreparation} min");
            Console.WriteLine($"  Végétarien: {(EstVegetarien ? "Oui" : "Non")}");
            Console.WriteLine($"  Ingrédients: {string.Join(", ", Ingredients)}");
        }
    }
    
    public class Boisson : Article
    {
        public TailleBoisson Taille { get; set; }
        public bool EstGazeuse { get; set; }
        
        public override decimal CalculerPrix()
        {
            // TODO: Ajuster le prix selon la taille
            // Petit: 100%, Moyen: 130%, Grand: 160%
            return 0;
        }
    }
    
    public class Menu
    {
        public Plat Entree { get; set; }
        public Plat PlatPrincipal { get; set; }
        public Article Dessert { get; set; }
        public Boisson Boisson { get; set; }
        
        public decimal CalculerPrix()
        {
            // TODO: Calculer avec réduction de 15%
            return 0;
        }
        
        public void AfficherMenu()
        {
            // TODO: Afficher tous les éléments du menu
        }
    }
    
    public class Commande
    {
        private static int _compteurCommandes = 0;
        
        public int NumeroCommande { get; private set; }
        public string Client { get; set; }
        public List<Article> Articles { get; private set; }
        public DateTime DateHeure { get; private set; }
        public StatutCommande Statut { get; set; }
        
        public Commande(string client)
        {
            NumeroCommande = ++_compteurCommandes;
            Client = client;
            Articles = new List<Article>();
            DateHeure = DateTime.Now;
            Statut = StatutCommande.EnAttente;
        }
        
        public void AjouterArticle(Article article)
        {
            // TODO: Implémenter
        }
        
        public decimal CalculerSousTotal()
        {
            // TODO: Sommer tous les articles
            return 0;
        }
        
        public decimal CalculerTaxes()
        {
            // TODO: Calculer TPS (5%) + TVQ (9.975%)
            return 0;
        }
        
        public decimal CalculerTotal()
        {
            return CalculerSousTotal() + CalculerTaxes();
        }
        
        public void AfficherFacture()
        {
            // TODO: Afficher facture détaillée
        }
    }
    
    public class Restaurant
    {
        public string Nom { get; set; }
        private List<Article> _carte = new List<Article>();
        private List<Commande> _commandes = new List<Commande>();
        
        public void AjouterAuMenu(Article article)
        {
            _carte.Add(article);
        }
        
        public void AfficherCarte()
        {
            // TODO: Afficher par catégorie
        }
        
        public Commande CreerCommande(string client)
        {
            var commande = new Commande(client);
            _commandes.Add(commande);
            return commande;
        }
        
        public void AfficherCommandesEnCours()
        {
            // TODO: Afficher commandes non terminées
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Restaurant resto = new Restaurant { Nom = "Chez Claude" };
            
            // TODO: Créer des plats et les ajouter au menu
            
            // TODO: Créer une commande
            
            // TODO: Personnaliser des plats
            
            // TODO: Afficher la facture
            
            Console.WriteLine("\n=== BIENVENUE CHEZ CLAUDE ===\n");
            
            // Exemple de création de plats
            var poutine = new Plat
            {
                Nom = "Poutine Classique",
                PrixBase = 12.99m,
                Description = "Frites, sauce brune et fromage en grains",
                Categorie = CategoriePlat.PlatPrincipal,
                Ingredients = new List<string> { "Frites", "Sauce brune", "Fromage en grains" },
                TempsPreparation = 15,
                EstVegetarien = true
            };
            
            // ... créer d'autres plats
        }
    }
}
```

### ✅ Critères de réussite
- [ ] Les prix sont calculés correctement avec les options
- [ ] Les taxes sont appliquées correctement
- [ ] La facture s'affiche proprement
- [ ] Les menus offrent une réduction

---

## Projet 5 : Gestionnaire de Tâches

### 📋 Objectif
Créer une application de gestion de tâches avec différents types de tâches et priorités.

### 🎯 Concepts utilisés
- Héritage
- Interfaces
- Délégués et événements
- Propriétés

### 📝 Spécifications

**Créer :**

1. Énumération `Priorite` : Basse, Normale, Haute, Critique

2. Énumération `StatutTache` : AFaire, EnCours, Terminee, Annulee

3. Interface `INotifiable` :
   - Événement : TacheModifiee
   - Méthode : NotifierChangement()

4. Classe abstraite `Tache` :
   - Propriétés : Id, Titre, Description, DateCreation, DateEcheance, Priorite, Statut
   - Méthodes abstraites : Executer(), EstEnRetard()

5. Classe `TacheSimple` héritant de `Tache`

6. Classe `TacheRecurrente` héritant de `Tache` :
   - Propriété : Frequence (Quotidien, Hebdomadaire, Mensuel)
   - Méthode : CreerProchaineTache()

7. Classe `TacheAvecSousTaches` héritant de `Tache` :
   - Liste de sous-tâches
   - Calcul de progression (%)

8. Classe `ProjetTaches` :
   - Collection de tâches
   - Méthodes : AjouterTache(), SupprimerTache(), ObtenirTachesParPriorite(), ObtenirTachesEnRetard()

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionnaireTaches
{
    public enum Priorite { Basse, Normale, Haute, Critique }
    public enum StatutTache { AFaire, EnCours, Terminee, Annulee }
    public enum FrequenceRecurrence { Quotidien, Hebdomadaire, Mensuel }
    
    public interface INotifiable
    {
        event EventHandler<string> TacheModifiee;
        void NotifierChangement(string message);
    }
    
    public abstract class Tache : INotifiable
    {
        private static int _compteur = 0;
        
        public int Id { get; private set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; private set; }
        public DateTime? DateEcheance { get; set; }
        public Priorite Priorite { get; set; }
        public StatutTache Statut { get; set; }
        
        public event EventHandler<string> TacheModifiee;
        
        protected Tache(string titre)
        {
            Id = ++_compteur;
            Titre = titre;
            DateCreation = DateTime.Now;
            Statut = StatutTache.AFaire;
            Priorite = Priorite.Normale;
        }
        
        public abstract void Executer();
        
        public virtual bool EstEnRetard()
        {
            // TODO: Vérifier si la date d'échéance est dépassée
            return false;
        }
        
        public void NotifierChangement(string message)
        {
            TacheModifiee?.Invoke(this, message);
        }
        
        public virtual void AfficherDetails()
        {
            // TODO: Afficher toutes les infos
        }
    }
    
    public class TacheSimple : Tache
    {
        public TacheSimple(string titre) : base(titre)
        {
        }
        
        public override void Executer()
        {
            // TODO: Marquer comme terminée
        }
    }
    
    public class TacheRecurrente : Tache
    {
        public FrequenceRecurrence Frequence { get; set; }
        
        public TacheRecurrente(string titre, FrequenceRecurrence frequence) 
            : base(titre)
        {
            Frequence = frequence;
        }
        
        public override void Executer()
        {
            // TODO: Marquer comme terminée et créer la prochaine occurrence
        }
        
        public TacheRecurrente CreerProchaineTache()
        {
            // TODO: Créer une nouvelle tâche avec date d'échéance ajustée
            return null;
        }
    }
    
    public class TacheAvecSousTaches : Tache
    {
        public List<Tache> SousTaches { get; private set; }
        
        public TacheAvecSousTaches(string titre) : base(titre)
        {
            SousTaches = new List<Tache>();
        }
        
        public void AjouterSousTache(Tache tache)
        {
            // TODO: Implémenter
        }
        
        public double CalculerProgression()
        {
            // TODO: Calculer % de sous-tâches terminées
            return 0;
        }
        
        public override void Executer()
        {
            // TODO: Marquer toutes les sous-tâches comme terminées
        }
        
        public override void AfficherDetails()
        {
            base.AfficherDetails();
            Console.WriteLine($"Progression: {CalculerProgression():P0}");
            Console.WriteLine("Sous-tâches:");
            // TODO: Afficher les sous-tâches
        }
    }
    
    public class ProjetTaches
    {
        public string Nom { get; set; }
        private List<Tache> _taches = new List<Tache>();
        
        public ProjetTaches(string nom)
        {
            Nom = nom;
        }
        
        public void AjouterTache(Tache tache)
        {
            _taches.Add(tache);
            tache.TacheModifiee += OnTacheModifiee;
        }
        
        private void OnTacheModifiee(object sender, string message)
        {
            Console.WriteLine($"[NOTIFICATION] {message}");
        }
        
        public List<Tache> ObtenirTachesParPriorite(Priorite priorite)
        {
            // TODO: Filtrer par priorité
            return null;
        }
        
        public List<Tache> ObtenirTachesEnRetard()
        {
            // TODO: Retourner les tâches en retard
            return null;
        }
        
        public void AfficherResume()
        {
            // TODO: Afficher statistiques (nombre total, terminées, en retard, etc.)
        }
        
        public void AfficherTachesParStatut()
        {
            // TODO: Grouper et afficher par statut
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            ProjetTaches projet = new ProjetTaches("Développement Application");
            
            // TODO: Créer différents types de tâches
            
            // TODO: Afficher les tâches
            
            // TODO: Marquer des tâches comme terminées
            
            // TODO: Afficher les statistiques
        }
    }
}
```

### ✅ Critères de réussite
- [ ] Les événements notifient correctement les changements
- [ ] Les tâches récurrentes créent de nouvelles instances
- [ ] La progression des tâches avec sous-tâches est correcte
- [ ] Les filtres fonctionnent correctement

---

## Projet 6 : Simulateur de Zoo

### 📋 Objectif
Créer un simulateur de zoo avec différents types d'animaux et comportements.

### 🎯 Concepts utilisés
- Héritage multiple (interfaces)
- Polymorphisme
- Classes abstraites
- Collections

### 📝 Spécifications

**Créer :**

1. Interfaces de comportements :
   - `IVolant` : Voler(), AltitudeMaximale
   - `INageant` : Nager(), ProfondeurMaximale
   - `IGrimpant` : Grimper(), Agilite
   - `ICarnivore` : Chasser(Animal proie)
   - `IHerbivore` : Brouter()

2. Classe abstraite `Animal` :
   - Propriétés : Nom, Espece, Age, Poids, Sante, Faim
   - Méthodes : Manger(), Dormir(), SeReproduire(), AfficherInfos()

3. Classes d'animaux implémentant les bonnes interfaces :
   - `Lion` : ICarnivore
   - `Aigle` : IVolant, ICarnivore
   - `Dauphin` : INageant, ICarnivore
   - `Singe` : IGrimpant, IHerbivore
   - `Elephant` : IHerbivore
   - `Pingouin` : INageant (ne vole pas!)
   - `Canard` : IVolant, INageant

4. Classe `Enclos` :
   - Type (Terrestre, Aquatique, Aerien, Mixte)
   - Capacite maximale
   - Liste d'animaux
   - Méthode : AjouterAnimal(), RetirerAnimal(), NourririAnimaux()

5. Classe `Zoo` :
   - Nom du zoo
   - Collection d'enclos
   - Méthodes : AjouterEnclos(), FaireVisiter(), RapportJournalier(), NourrirTousLesAnimaux()

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimulateurZoo
{
    public interface IVolant
    {
        void Voler();
        double AltitudeMaximale { get; }
    }
    
    public interface INageant
    {
        void Nager();
        double ProfondeurMaximale { get; }
    }
    
    public interface IGrimpant
    {
        void Grimper();
        int Agilite { get; } // Sur 10
    }
    
    public interface ICarnivore
    {
        void Chasser(Animal proie);
        string RegimeAlimentaire { get; }
    }
    
    public interface IHerbivore
    {
        void Brouter();
        string VegetationPreferee { get; }
    }
    
    public enum TypeEnclos
    {
        Terrestre,
        Aquatique,
        Aerien,
        Mixte
    }
    
    public abstract class Animal
    {
        public string Nom { get; set; }
        public string Espece { get; protected set; }
        public int Age { get; set; }
        public double Poids { get; set; }
        public int Sante { get; protected set; } // 0-100
        public int Faim { get; protected set; } // 0-100
        
        protected Animal(string nom, int age, double poids)
        {
            Nom = nom;
            Age = age;
            Poids = poids;
            Sante = 100;
            Faim = 50;
        }
        
        public virtual void Manger()
        {
            Faim = Math.Max(0, Faim - 30);
            Console.WriteLine($"{Nom} mange. Faim: {Faim}%");
        }
        
        public void Dormir()
        {
            Sante = Math.Min(100, Sante + 10);
            Console.WriteLine($"{Nom} dort. Santé: {Sante}%");
        }
        
        public abstract void EmettreSon();
        
        public virtual void AfficherInfos()
        {
            Console.WriteLine($"=== {Nom} ===");
            Console.WriteLine($"Espèce: {Espece}");
            Console.WriteLine($"Âge: {Age} ans");
            Console.WriteLine($"Poids: {Poids} kg");
            Console.WriteLine($"Santé: {Sante}%");
            Console.WriteLine($"Faim: {Faim}%");
        }
    }
    
    // TODO: Créer la classe Lion
    public class Lion : Animal, ICarnivore
    {
        public string RegimeAlimentaire { get; } = "Carnivore strict";
        
        public Lion(string nom, int age, double poids) : base(nom, age, poids)
        {
            Espece = "Lion";
        }
        
        public void Chasser(Animal proie)
        {
            // TODO: Implémenter
        }
        
        public override void EmettreSon()
        {
            Console.WriteLine($"{Nom} rugit: ROARRR!");
        }
    }
    
    // TODO: Créer les autres classes d'animaux
    
    public class Enclos
    {
        private static int _compteur = 0;
        
        public int Numero { get; private set; }
        public string Nom { get; set; }
        public TypeEnclos Type { get; set; }
        public int CapaciteMax { get; set; }
        public List<Animal> Animaux { get; private set; }
        
        public Enclos(string nom, TypeEnclos type, int capacite)
        {
            Numero = ++_compteur;
            Nom = nom;
            Type = type;
            CapaciteMax = capacite;
            Animaux = new List<Animal>();
        }
        
        public bool AjouterAnimal(Animal animal)
        {
            // TODO: Vérifier capacité et compatibilité
            return false;
        }
        
        public void NourrirAnimaux()
        {
            // TODO: Nourrir tous les animaux
        }
        
        public void AfficherContenu()
        {
            // TODO: Afficher infos de l'enclos
        }
    }
    
    public class Zoo
    {
        public string Nom { get; set; }
        public string Ville { get; set; }
        private List<Enclos> _enclos = new List<Enclos>();
        
        public Zoo(string nom, string ville)
        {
            Nom = nom;
            Ville = ville;
        }
        
        public void AjouterEnclos(Enclos enclos)
        {
            _enclos.Add(enclos);
        }
        
        public void NourrirTousLesAnimaux()
        {
            // TODO: Parcourir tous les enclos
        }
        
        public void FaireVisiter()
        {
            // TODO: Afficher tous les enclos
        }
        
        public void RapportJournalier()
        {
            // TODO: Statistiques (nombre animaux, par type, santé moyenne, etc.)
        }
        
        public List<Animal> RechercherAnimauxParCapacite(Type interfaceType)
        {
            // TODO: Trouver tous les animaux qui implémentent une interface donnée
            // Ex: tous les IVolant
            return null;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo("Zoo de Montréal", "Montréal");
            
            // TODO: Créer des enclos
            
            // TODO: Créer des animaux
            
            // TODO: Placer les animaux dans les enclos
            
            // TODO: Faire une visite
            
            // TODO: Nourrir les animaux
            
            // TODO: Générer un rapport
        }
    }
}
```

### ✅ Critères de réussite
- [ ] Les animaux ont les bonnes capacités (nager, voler, etc.)
- [ ] Les enclos acceptent seulement les animaux compatibles
- [ ] Le polymorphisme fonctionne pour les comportements
- [ ] Les statistiques sont correctes

---

## Projet 7 : Système de Réservation Hôtel

### 📋 Objectif
Créer un système de réservation pour un hôtel avec différents types de chambres.

### 🎯 Concepts utilisés
- Héritage
- Interfaces
- Propriétés calculées
- Énumérations

### 📝 Spécifications

**Créer :**

1. Énumérations :
   - `TypeChambre` : Simple, Double, Suite, Penthouse
   - `StatutReservation` : Confirmee, EnAttente, Annulee, Terminee

2. Interface `IAnnulable` :
   - Méthode : Annuler(), ObtenirFraisAnnulation()

3. Classe abstraite `Chambre` :
   - Propriétés : Numero, Type, PrixParNuit, NombrePersonnesMax, Superficie, EstDisponible
   - Méthode abstraite : CalculerPrix(int nuits)

4. Classes de chambres :
   - `ChambreSimple` : 1 personne, prix de base
   - `ChambreDouble` : 2 personnes, prix + 30%
   - `Suite` : 4 personnes, prix + 80%, inclut petit-déjeuner
   - `Penthouse` : 6 personnes, prix + 150%, tous services inclus

5. Classe `Service` :
   - Types : PetitDejeuner, Spa, Parking, RoomService
   - Prix par service

6. Classe `Reservation` implémentant `IAnnulable` :
   - Propriétés : NumeroReservation, Client, Chambre, DateArrivee, DateDepart, Services, Statut
   - Méthodes : AjouterService(), CalculerCoutTotal(), CalculerDuree()

7. Classe `Hotel` :
   - Gestion des chambres et réservations
   - Méthodes : RechercherChambresDisponibles(), CreerReservation(), AfficherOccupation()

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemeHotel
{
    public enum TypeChambre { Simple, Double, Suite, Penthouse }
    public enum StatutReservation { Confirmee, EnAttente, Annulee, Terminee }
    public enum TypeService { PetitDejeuner, Spa, Parking, RoomService, Wifi }
    
    public interface IAnnulable
    {
        bool Annuler();
        decimal ObtenirFraisAnnulation();
    }
    
    public abstract class Chambre
    {
        public int Numero { get; set; }
        public TypeChambre Type { get; protected set; }
        public decimal PrixParNuit { get; set; }
        public int NombrePersonnesMax { get; protected set; }
        public double Superficie { get; set; }
        public bool EstDisponible { get; set; }
        public List<string> Equipements { get; protected set; }
        
        protected Chambre(int numero, decimal prixParNuit)
        {
            Numero = numero;
            PrixParNuit = prixParNuit;
            EstDisponible = true;
            Equipements = new List<string>();
        }
        
        public abstract decimal CalculerPrix(int nuits);
        
        public virtual void AfficherDetails()
        {
            Console.WriteLine($"Chambre #{Numero} - {Type}");
            Console.WriteLine($"Prix par nuit: {PrixParNuit:C}");
            Console.WriteLine($"Capacité: {NombrePersonnesMax} personne(s)");
            Console.WriteLine($"Superficie: {Superficie}m²");
            Console.WriteLine($"Disponible: {(EstDisponible ? "Oui" : "Non")}");
        }
    }
    
    // TODO: Créer ChambreSimple
    
    // TODO: Créer ChambreDouble
    
    // TODO: Créer Suite
    
    // TODO: Créer Penthouse
    
    public class Service
    {
        public TypeService Type { get; set; }
        public string Description { get; set; }
        public decimal Prix { get; set; }
        
        public Service(TypeService type, string description, decimal prix)
        {
            Type = type;
            Description = description;
            Prix = prix;
        }
    }
    
    public class Reservation : IAnnulable
    {
        private static int _compteur = 0;
        
        public int NumeroReservation { get; private set; }
        public string NomClient { get; set; }
        public string EmailClient { get; set; }
        public Chambre Chambre { get; set; }
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }
        public List<Service> Services { get; private set; }
        public StatutReservation Statut { get; set; }
        public DateTime DateReservation { get; private set; }
        
        public Reservation(string nomClient, string email, Chambre chambre, 
            DateTime arrivee, DateTime depart)
        {
            NumeroReservation = ++_compteur;
            NomClient = nomClient;
            EmailClient = email;
            Chambre = chambre;
            DateArrivee = arrivee;
            DateDepart = depart;
            Services = new List<Service>();
            Statut = StatutReservation.Confirmee;
            DateReservation = DateTime.Now;
        }
        
        public int CalculerDuree()
        {
            // TODO: Calculer nombre de nuits
            return 0;
        }
        
        public void AjouterService(Service service)
        {
            // TODO: Implémenter
        }
        
        public decimal CalculerCoutTotal()
        {
            // TODO: Chambre + services
            return 0;
        }
        
        public bool Annuler()
        {
            // TODO: Vérifier si annulation possible et appliquer frais
            return false;
        }
        
        public decimal ObtenirFraisAnnulation()
        {
            // TODO: Calculer frais selon date d'annulation
            // Moins de 48h avant: 100%
            // Moins d'une semaine: 50%
            // Plus d'une semaine: 25%
            return 0;
        }
        
        public void AfficherDetails()
        {
            // TODO: Afficher tous les détails
        }
    }
    
    public class Hotel
    {
        public string Nom { get; set; }
        public string Adresse { get; set; }
        private List<Chambre> _chambres = new List<Chambre>();
        private List<Reservation> _reservations = new List<Reservation>();
        
        public Hotel(string nom, string adresse)
        {
            Nom = nom;
            Adresse = adresse;
        }
        
        public void AjouterChambre(Chambre chambre)
        {
            _chambres.Add(chambre);
        }
        
        public List<Chambre> RechercherChambresDisponibles(DateTime arrivee, DateTime depart)
        {
            // TODO: Filtrer les chambres disponibles pour ces dates
            return null;
        }
        
        public List<Chambre> RechercherParType(TypeChambre type)
        {
            // TODO: Filtrer par type
            return null;
        }
        
        public Reservation CreerReservation(string client, string email, 
            int numeroChambre, DateTime arrivee, DateTime depart)
        {
            // TODO: Créer et ajouter la réservation
            return null;
        }
        
        public void AfficherOccupation()
        {
            // TODO: Statistiques d'occupation
        }
        
        public decimal CalculerRevenuTotal()
        {
            // TODO: Sommer toutes les réservations confirmées
            return 0;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Hotel hotel = new Hotel("Grand Hôtel", "123 Rue Principale, Montréal");
            
            // TODO: Ajouter des chambres
            
            // TODO: Créer des réservations
            
            // TODO: Ajouter des services
            
            // TODO: Afficher l'occupation
            
            // TODO: Tester les annulations
        }
    }
}
```

### ✅ Critères de réussite
- [ ] Les prix varient selon le type de chambre et les services
- [ ] Les frais d'annulation sont calculés correctement
- [ ] Impossible de réserver une chambre déjà réservée
- [ ] Les statistiques sont exactes

---

## Projet 8 : Calculatrice de Formes Géométriques

### 📋 Objectif
Créer un système pour calculer l'aire, le périmètre et d'autres propriétés de formes géométriques.

### 🎯 Concepts utilisés
- Classes abstraites
- Polymorphisme
- Méthodes virtuelles
- Interfaces

### 📝 Spécifications

**Créer :**

1. Interface `IDessinable` :
   - Méthode : Dessiner()

2. Classe abstraite `Forme` :
   - Propriétés : Nom, Couleur
   - Méthodes abstraites : CalculerAire(), CalculerPerimetre()
   - Méthode virtuelle : AfficherInfos()

3. Classes de formes 2D :
   - `Cercle` : rayon
   - `Rectangle` : longueur, largeur
   - `Carre` : côté
   - `Triangle` : base, hauteur
   - `Polygone` : nombre de côtés, longueur des côtés

4. Classe abstraite `Forme3D` héritant de `Forme` :
   - Méthode abstraite supplémentaire : CalculerVolume()

5. Classes de formes 3D :
   - `Sphere` : rayon
   - `Cube` : côté
   - `Cylindre` : rayon, hauteur
   - `Cone` : rayon base, hauteur

6. Classe `Calculateur` :
   - Méthodes statiques pour comparer des formes, trier par aire, etc.

### 💡 Code de démarrage

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatriceFormes
{
    public interface IDessinable
    {
        void Dessiner();
    }
    
    public abstract class Forme
    {
        public string Nom { get; set; }
        public string Couleur { get; set; }
        
        public abstract double CalculerAire();
        public abstract double CalculerPerimetre();
        
        public virtual void AfficherInfos()
        {
            Console.WriteLine($"=== {Nom} ===");
            Console.WriteLine($"Couleur: {Couleur}");
            Console.WriteLine($"Aire: {CalculerAire():F2}");
            Console.WriteLine($"Périmètre: {CalculerPerimetre():F2}");
        }
    }
    
    public class Cercle : Forme, IDessinable
    {
        public double Rayon { get; set; }
        
        public Cercle(double rayon, string couleur = "Noir")
        {
            Rayon = rayon;
            Couleur = couleur;
            Nom = "Cercle";
        }
        
        public override double CalculerAire()
        {
            // TODO: π × r²
            return 0;
        }
        
        public override double CalculerPerimetre()
        {
            // TODO: 2 × π × r
            return 0;
        }
        
        public void Dessiner()
        {
            Console.WriteLine($"Dessin d'un cercle de rayon {Rayon} en {Couleur}");
            Console.WriteLine("    ***    ");
            Console.WriteLine("  *     *  ");
            Console.WriteLine(" *       * ");
            Console.WriteLine("  *     *  ");
            Console.WriteLine("    ***    ");
        }
    }
    
    // TODO: Créer Rectangle
    
    // TODO: Créer Carre
    
    // TODO: Créer Triangle
    
    public abstract class Forme3D : Forme
    {
        public abstract double CalculerVolume();
        
        public override void AfficherInfos()
        {
            base.AfficherInfos();
            Console.WriteLine($"Volume: {CalculerVolume():F2}");
        }
    }
    
    // TODO: Créer Sphere
    
    // TODO: Créer Cube
    
    // TODO: Créer Cylindre
    
    public static class Calculateur
    {
        public static Forme TrouverPlusGrandeAire(List<Forme> formes)
        {
            // TODO: Retourner la forme avec la plus grande aire
            return null;
        }
        
        public static double CalculerAireTotale(List<Forme> formes)
        {
            // TODO: Sommer toutes les aires
            return 0;
        }
        
        public static List<Forme> TrierParAire(List<Forme> formes)
        {
            // TODO: Trier par aire croissante
            return null;
        }
        
        public static void ComparerFormes(Forme f1, Forme f2)
        {
            // TODO: Comparer aires et périmètres
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            List<Forme> formes = new List<Forme>();
            
            // TODO: Créer différentes formes
            
            // TODO: Afficher les infos
            
            // TODO: Utiliser le calculateur
            
            // TODO: Dessiner les formes qui sont IDessinable
        }
    }
}
```

## Conseils pour réussir les projets

### 🎯 Méthodologie

1. **Lire attentivement** les spécifications
2. **Planifier** la structure avant de coder
3. **Tester** fréquemment chaque nouvelle fonctionnalité
4. **Refactoriser** le code pour améliorer la qualité
5. **Documenter** avec des commentaires clairs

### 🔍 Points de contrôle

Pour chaque projet, vérifiez :
- ✅ Respect de l'encapsulation (propriétés privées/publiques appropriées)
- ✅ Utilisation correcte de l'héritage
- ✅ Polymorphisme fonctionnel
- ✅ Interfaces bien implémentées
- ✅ Gestion des cas d'erreur
- ✅ Code lisible et bien organisé
