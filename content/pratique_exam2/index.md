---
title: "Pratique - Exam 2"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 22
---

# Exercices de révision - Examen 2

---

## Table des matières

1. [POO — Partie 1 : Classes, encapsulation, héritage](#1-poo--partie-1--classes-encapsulation-héritage)
2. [POO — Partie 2 : virtual, abstract, interfaces, sealed](#2-poo--partie-2--virtual-abstract-interfaces-sealed)
3. [LINQ](#3-linq)
4. [Patron Observateur](#4-patron-observateur)
5. [Entity Framework Core](#5-entity-framework-core)
6. [Patron Repository avec EF Core](#6-patron-repository-avec-ef-core)
7. [Injection de dépendances](#7-injection-de-dépendances)
8. [Async / Await](#8-async--await)
9. [Exercice intégrateur](#9-exercice-intégrateur)

---

## 1. POO — Partie 1 : Classes, encapsulation, héritage

### Questions théoriques

**Q1.** Quelle est la différence entre une **classe** et un **objet** ?

**Q2.** Expliquez le concept d'**encapsulation** avec un exemple concret de la vie courante (pas bancaire, pas voiture).

**Q3.** À quoi servent les **modificateurs d'accès** `public`, `private` et `protected` ? Donnez une règle générale pour chaque.

**Q4.** Quelle est la différence entre un **champ** (`private int _age`) et une **propriété** (`public int Age { get; set; }`) ?

**Q5.** Qu'est-ce que le mot-clé `this` dans un constructeur ? Donnez un exemple de chaînage de constructeurs avec `this`.

**Q6.** Qu'est-ce que la relation **« est-un »** (is-a) en héritage ? Donnez un exemple.

**Q7.** Pourquoi C# n'autorise-t-il pas l'**héritage multiple** de classes ?

**Q8.** Vrai ou faux : Une classe dérivée hérite des constructeurs de sa classe de base. Expliquez.

---

### Exercices pratiques

#### Exercice 1.1 — Système de véhicules

Créez une hiérarchie de classes en console représentant des véhicules.

**Exigences :**

- Classe `Vehicule` avec : `Marque` (string), `Modele` (string), `Annee` (int), `Vitesse` (int, private avec getter)
- Méthodes : `Accelerer(int km)`, `Freiner(int km)` (la vitesse ne peut pas être négative), `AfficherInfos()`
- Constructeur avec paramètres obligatoires + constructeur par défaut qui chaîne avec `this`
- Classe `Voiture` hérite de `Vehicule` : ajoute `NombrePortes` (int), surcharge `AfficherInfos()`
- Classe `Camion` hérite de `Vehicule` : ajoute `CapaciteChargeKg` (decimal), surcharge `AfficherInfos()`
- Classe `Moto` hérite de `Vehicule` : ajoute `AUnSidecar` (bool), surcharge `AfficherInfos()`

**Dans `Program.cs` :**

```csharp
// Créer une liste de Vehicule contenant des Voiture, Camion et Moto
// Appeler AfficherInfos() sur chaque élément (polymorphisme)
// Filtrer et afficher seulement les Voiture de la liste
// Afficher le véhicule ayant la plus grande capacité de charge
```

---

#### Exercice 1.2 — Compte bancaire encapsulé

Créez une classe `CompteBancaire` respectant strictement l'encapsulation.

**Exigences :**

- Champs privés : `_solde` (decimal), `_numeroCompte` (string), `_historique` (List\<string\>)
- Propriétés publiques en lecture seule : `Solde`, `NumeroCompte`, `NombreTransactions`
- Constructeur : génère un numéro de compte automatique au format `CPT-YYYYMMDD-XXXX`
- Méthodes : `Deposer(decimal montant)`, `Retirer(decimal montant)` → retourne bool (échec si solde insuffisant), `AfficherHistorique()`
- Chaque opération réussie est ajoutée à `_historique` avec la date et le montant
- Propriété calculée `EstActif` → true si au moins une transaction

**Programme de test :**

```csharp
var compte = new CompteBancaire("Tremblay", "Alexia");
compte.Deposer(500);
compte.Deposer(200);
bool succes = compte.Retirer(800);   // doit réussir
bool echec  = compte.Retirer(1000);  // doit échouer
compte.AfficherHistorique();
Console.WriteLine($"Actif : {compte.EstActif}");
```

---

## 2. POO — Partie 2 : virtual, abstract, interfaces, sealed

### Questions théoriques

**Q1.** Quelle est la différence entre `virtual` et `abstract` ? Dans quel cas utilise-t-on l'un plutôt que l'autre ?

**Q2.** Peut-on instancier une classe abstraite directement ? Justifiez.

**Q3.** Quelle est la différence entre une **classe abstraite** et une **interface** ? Donnez un cas d'usage pour chacune.

**Q4.** Une classe peut-elle implémenter plusieurs interfaces ? Peut-elle hériter de plusieurs classes abstraites ?

**Q5.** À quoi sert le mot-clé `sealed` ? Donnez un exemple où il est pertinent.

**Q6.** Qu'est-ce que l'**implémentation explicite** d'interface ? Pourquoi l'utilise-t-on ?

**Q7.** Qu'est-ce qu'un **membre statique** ? Quelle est la différence avec un membre d'instance ?

**Q8.** Identifiez l'erreur dans ce code et expliquez pourquoi :

```csharp
public abstract class Forme
{
    public abstract double CalculerAire();
}

public class Carre : Forme
{
    public double Cote { get; set; }
    // Classe compilée sans override de CalculerAire()
}
```

---

### Exercices pratiques

#### Exercice 2.1 — Système de paiement (classe abstraite)

Créez un système de paiement en console.

**Exigences :**

- Classe abstraite `MethodePaiement` avec :
  - Propriétés : `Titulaire` (string), `DeviseUtilisee` (string, défaut "CAD")
  - Méthode abstraite : `Payer(decimal montant)` → retourne bool
  - Méthode abstraite : `ObtenirDescription()` → retourne string
  - Méthode `virtual` : `AfficherRecapitulatif(decimal montant)` → affiche titulaire + description + montant (peut être surchargée)
- Classe `CarteCreditVisa` hérite de `MethodePaiement` : ajoute `NumeroMasque` (string), `LimiteCredit` (decimal). `Payer` échoue si montant > limiteCredit.
- Classe `PayPal` hérite de `MethodePaiement` : ajoute `EmailCompte`. `Payer` échoue si email invalide (pas de `@`).
- Classe `CryptoMonnaie` hérite de `MethodePaiement` : ajoute `TypeCrypto` (Bitcoin, Ethereum, etc.), `TauxConversion` (decimal). Surcharger `AfficherRecapitulatif` pour afficher aussi l'équivalent en crypto.

**Programme :**

```csharp
var paiements = new List<MethodePaiement>
{
    new CarteCreditVisa("Alexia Tremblay", "4532-XXXX-XXXX-1234", 5000),
    new PayPal("samuel.gagnon@email.com"),
    new CryptoMonnaie("Félix Bouchard", "Bitcoin", 45000m)
};

foreach (var p in paiements)
    p.AfficherRecapitulatif(150.00m);
```

---

#### Exercice 2.2 — Interfaces multiples

Créez des interfaces et une classe qui en implémente plusieurs.

**Exigences :**

- Interface `IExportable` : `string ExporterEnCSV()`, `string ExporterEnJSON()`
- Interface `IValidable` : `bool EstValide()`, `List<string> ObtenirErreurs()`
- Interface `IComparable<T>` (déjà en C#) : permet le tri

- Classe `Produit` implémente `IExportable` et `IValidable` :
  - Propriétés : `Id` (int), `Nom` (string), `Prix` (decimal), `Stock` (int)
  - `EstValide()` : vrai si Nom non vide ET Prix > 0 ET Stock >= 0
  - `ObtenirErreurs()` : retourne la liste des règles violées
  - `ExporterEnCSV()` : retourne `"Id,Nom,Prix,Stock\n1,Café,3.50,100"`
  - `ExporterEnJSON()` : retourne le JSON manuellement formaté

**Programme :**

```csharp
var produits = new List<Produit>
{
    new Produit(1, "Café",    3.50m, 100),
    new Produit(2, "",        -5m,   50),   // invalide
    new Produit(3, "Thé vert", 4.25m, 0),
};

foreach (var p in produits)
{
    if (!p.EstValide())
        Console.WriteLine($"Produit invalide : {string.Join(", ", p.ObtenirErreurs())}");
    else
        Console.WriteLine(p.ExporterEnCSV());
}
```

---

## 3. LINQ

### Questions théoriques

**Q1.** Qu'est-ce que LINQ ? Quelle est l'interface de base que LINQ utilise ?

**Q2.** Expliquez l'**exécution différée** (deferred execution). Quand la requête LINQ est-elle réellement exécutée ?

**Q3.** Quelle est la différence entre `First()` et `FirstOrDefault()` ? Quand faut-il préférer l'un à l'autre ?

**Q4.** Quelles sont les deux syntaxes LINQ ? Laquelle est recommandée pour les requêtes complexes ?

**Q5.** Pourquoi `Any()` est-il préférable à `Count() > 0` pour vérifier qu'une liste est non vide ?

**Q6.** Expliquez `GroupBy`. Que contient chaque groupe retourné ?

**Q7.** Quelle est la différence entre `Select` et `SelectMany` ?

---

### Exercices pratiques

Utilisez cette liste de données pour tous les exercices LINQ :

```csharp
public record Etudiant(int Id, string Nom, string Programme, int Age, double MoyenneGenerale);

var etudiants = new List<Etudiant>
{
    new(1,  "Tremblay Alexia",   "Informatique",  20, 88.5),
    new(2,  "Gagnon Samuel",     "Gestion",       22, 72.0),
    new(3,  "Bouchard Émilie",   "Informatique",  19, 91.2),
    new(4,  "Roy Mathieu",       "Sciences",      23, 65.4),
    new(5,  "Côté Sophie",       "Informatique",  21, 78.9),
    new(6,  "Lavoie Antoine",    "Gestion",       20, 83.1),
    new(7,  "Fortin Camille",    "Sciences",      22, 70.5),
    new(8,  "Gauthier Félix",    "Informatique",  19, 94.0),
    new(9,  "Morin Jade",        "Gestion",       21, 68.3),
    new(10, "Landry William",    "Sciences",      20, 85.7),
};
```

---

**Ex 3.1** — Étudiants en informatique avec une moyenne ≥ 80, triés par moyenne décroissante.

**Ex 3.2** — Nombres d'étudiants par programme (utiliser `GroupBy`).

**Ex 3.3** — Moyenne générale de tous les étudiants en Informatique.

**Ex 3.4** — L'étudiant avec la plus haute moyenne (utiliser `MaxBy` ou `OrderByDescending + First`).

**Ex 3.5** — Noms de tous les étudiants de 20 ans ou moins, en majuscules, triés alphabétiquement.

**Ex 3.6** — Y a-t-il au moins un étudiant en Sciences avec une moyenne > 80 ? (`Any`)

**Ex 3.7** — Tous les étudiants en Gestion ont-ils une moyenne ≥ 60 ? (`All`)

**Ex 3.8** — Les 3 premiers étudiants par ordre alphabétique de nom. (`Take`)

**Ex 3.9** — Créer un dictionnaire `<Programme, List<Etudiant>>` groupant les étudiants par programme.

**Ex 3.10 (difficile)** — Pour chaque programme, afficher : le nom du programme, le nombre d'étudiants, la moyenne du groupe, et le nom du meilleur étudiant.

Format attendu :
```
Informatique : 4 étudiants | Moyenne : 88.15 | Meilleur : Gauthier Félix (94.0)
Gestion      : 3 étudiants | Moyenne : 74.47 | Meilleur : Lavoie Antoine (83.1)
Sciences     : 3 étudiants | Moyenne : 73.87 | Meilleur : Landry William (85.7)
```

---

## 4. Patron Observateur

### Questions théoriques

**Q1.** Quel problème le patron Observateur résout-il ? Donnez une analogie de la vie courante.

**Q2.** Expliquez les deux rôles principaux du patron Observateur : **Sujet** et **Observateur**.

**Q3.** Quelle est la différence entre un **délégué** et un **événement** en C# ? Pourquoi utiliser `event` plutôt qu'un simple délégué public ?

**Q4.** Qu'est-ce qu'une **expression lambda** ? Donnez un exemple concret.

**Q5.** Quelle est la convention de nommage standard pour les événements en C# ? (ex : signature de l'EventHandler)

**Q6.** En quoi le patron Observateur réduit-il le **couplage** entre les classes ?

---

### Exercices pratiques

#### Exercice 4.1 — Station météo (implémentation directe)

Créez un système de station météo en console sans WPF.

**Exigences :**

- Interface `IObservateur` : `void Actualiser(double temperature, double humidite)`
- Classe `StationMeteo` (sujet) : maintient une liste d'observateurs, méthodes `Attacher`, `Detacher`, `Notifier`. Propriété `Temperature` et `Humidite` — quand elles changent via `MettreAJour(double temp, double hum)`, notifier tous les observateurs.
- Classe `AffichageConsole` implémente `IObservateur` : affiche les nouvelles valeurs avec timestamp
- Classe `SystemeAlerte` implémente `IObservateur` : alerte si température > 35 ou humidité > 90
- Classe `JournalFichier` implémente `IObservateur` : accumule les mesures dans une `List<string>` et expose `AfficherJournal()`

**Programme :**

```csharp
var station = new StationMeteo();
var affichage = new AffichageConsole();
var alerte    = new SystemeAlerte();
var journal   = new JournalFichier();

station.Attacher(affichage);
station.Attacher(alerte);
station.Attacher(journal);

station.MettreAJour(25.0, 60.0);
station.MettreAJour(38.5, 95.0); // déclenche les alertes
station.Detacher(alerte);
station.MettreAJour(20.0, 45.0); // alerte ne reçoit plus rien

journal.AfficherJournal();
```

---

#### Exercice 4.2 — Événements C# avec EventHandler

Recréez un système de notification de stock en utilisant des **événements C#** (pas l'interface manuelle).

**Exigences :**

- Classe `ProduitEnStock` avec propriété `Quantite`. Quand `Quantite` passe sous 10, déclencher l'événement `StockBas`. Quand `Quantite` atteint 0, déclencher `RuptureStock`.
- Classe `EventArgs` personnalisée : `StockEventArgs` contenant `NomProduit` et `Quantite`
- Deux gestionnaires d'événements (méthodes séparées) : un qui affiche une alerte console, un qui simule l'envoi d'un email (simple `Console.WriteLine`)

```csharp
var produit = new ProduitEnStock("Café Premium", 15);
produit.StockBas     += AlerteConsole;
produit.RuptureStock += EnvoyerEmailAchat;

produit.Quantite = 8;   // déclenche StockBas
produit.Quantite = 3;   // déclenche StockBas
produit.Quantite = 0;   // déclenche RuptureStock
```

---

## 5. Entity Framework Core

### Questions théoriques

**Q1.** Qu'est-ce qu'un ORM ? Quel problème résout EF Core ?

**Q2.** Qu'est-ce qu'une **entité** dans EF Core ? Quelles règles doit-elle respecter ?

**Q3.** Qu'est-ce qu'un `DbContext` ? Qu'est-ce qu'un `DbSet<T>` ?

**Q4.** Expliquez la différence entre **Code First** et **Database First**.

**Q5.** Qu'est-ce qu'une **migration** ? Quelles sont les deux commandes essentielles ?

**Q6.** Quelle annotation force un champ à être obligatoire ? Laquelle limite la longueur ?

**Q7.** Quelle méthode EF Core persiste les changements en base de données ?

**Q8.** Quelle est la différence entre `Find(id)` et `FirstOrDefault(e => e.Id == id)` ?

---

### Exercices pratiques

#### Exercice 5.1 — Catalogue de jeux vidéo

Créez une application console avec EF Core et SQLite pour gérer un catalogue de jeux.

**Entités :**

```csharp
public class Developpeur
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Nom { get; set; }
    public string Pays { get; set; }
    public List<Jeu> Jeux { get; set; } = new();
}

public class Jeu
{
    public int Id { get; set; }
    [Required, MaxLength(200)]
    public string Titre { get; set; }
    public int AnneeParution { get; set; }
    public decimal Prix { get; set; }
    public Genre GenreJeu { get; set; }
    public int DeveloppeParId { get; set; }
    public Developpeur? DeveloppeePar { get; set; }
}

public enum Genre { Action, RPG, Simulation, Sport, Aventure, Strategie }
```

**Exigences :**

1. Créer `JeuDbContext` avec `DbSet<Jeu>` et `DbSet<Developpeur>`
2. Configurer la relation 1-N dans `OnModelCreating`
3. Créer et appliquer la migration
4. Insérer des données (3 développeurs, 8 jeux au minimum)
5. Requêtes LINQ à implémenter :
   - Tous les jeux avec leur développeur, triés par prix
   - Jeux du genre RPG
   - Développeur qui a le plus de jeux
   - Prix moyen par genre
   - Jeux sortis après 2020 avec prix < 50$

---

## 6. Patron Repository avec EF Core

### Questions théoriques

**Q1.** Quel problème le patron Repository résout-il ? Pourquoi ne pas accéder directement au `DbContext` dans le ViewModel ?

**Q2.** Expliquez les 4 composants du patron Repository : Interface, Repository concret, DbContext, Modèle.

**Q3.** Pourquoi définit-on une **interface** pour le Repository plutôt qu'utiliser directement la classe concrète ?

**Q4.** Expliquez l'avantage du Repository pour les **tests unitaires**.

**Q5.** Qu'est-ce que `Include()` dans EF Core ? Quand est-il nécessaire ?

---

### Exercices pratiques

#### Exercice 6.1 — Repository pour le catalogue de jeux

En reprenant l'exercice 5.1, ajoutez la couche Repository.

**Exigences :**

- Interface `IJeuRepository` :

```csharp
public interface IJeuRepository
{
    List<Jeu>  ObtenirTous();
    Jeu?       ObtenirParId(int id);
    List<Jeu>  ObtenirParGenre(Genre genre);
    List<Jeu>  ObtenirAvecDeveloppeur();
    void       Ajouter(Jeu jeu);
    void       Modifier(Jeu jeu);
    void       Supprimer(int id);
    int        Compter();
}
```

- Implémenter `JeuRepository : IJeuRepository` avec EF Core
- Interface `IDeveloppeurRepository` + implémentation
- Classe `CatalogueService` qui reçoit `IJeuRepository` et `IDeveloppeurRepository` par constructeur et expose des méthodes métier :
  - `AjouterJeu(Jeu jeu)` — valide que le titre n'est pas vide
  - `ObtenirTop3PlusChers()` — retourne les 3 jeux les plus chers
  - `ObtenirStatistiquesParDeveloppeur()` — retourne `List<(string NomDev, int NbJeux, decimal PrixMoyen)>`

---

## 7. Injection de dépendances

### Questions théoriques

**Q1.** Qu'est-ce que le principe **DIP** (Dependency Inversion Principle) ?

**Q2.** Expliquez les 3 types d'injection de dépendances. Lequel est recommandé ?

**Q3.** Quelle est la différence entre `AddTransient`, `AddScoped` et `AddSingleton` ? Donnez un exemple d'usage pour chacun.

**Q4.** Un service `Singleton` peut-il dépendre d'un service `Scoped` ? Expliquez pourquoi.

**Q5.** Quel est le rôle de `ServiceCollection` et `BuildServiceProvider()` ?

**Q6.** Quelle est la différence entre `GetService<T>()` et `GetRequiredService<T>()` ?

---

### Exercices pratiques

#### Exercice 7.1 — Conteneur IoC sans WPF

Créez une application console qui utilise `Microsoft.Extensions.DependencyInjection`.

**Exigences :**

- Interface `ILogService` : `void Log(string message)`
- `ConsoleLogService` : affiche avec timestamp dans la console
- `FichierLogService` : accumule dans une `List<string>` (simule un fichier)
- Interface `INotificationService` : `Task EnvoyerAsync(string destinataire, string message)`
- `FakeNotificationService` : affiche dans la console (pas de vrai SMS)
- Classe `GestionnaireCommande` reçoit par constructeur : `ILogService`, `INotificationService`
- Méthode `async Task TraiterCommandeAsync(string numeroCommande, string emailClient)` :
  - Log le début du traitement
  - Simule un délai de 500ms (`Task.Delay`)
  - Envoie une notification à l'email client
  - Log la fin du traitement

**Programme :**

```csharp
var services = new ServiceCollection();

services.AddSingleton<ILogService, ConsoleLogService>();
services.AddScoped<INotificationService, FakeNotificationService>();
services.AddScoped<GestionnaireCommande>();

var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();
var gestionnaire = scope.ServiceProvider.GetRequiredService<GestionnaireCommande>();

await gestionnaire.TraiterCommandeAsync("CMD-001", "client@email.com");
await gestionnaire.TraiterCommandeAsync("CMD-002", "autre@email.com");
```

---

#### Exercice 7.2 — Remplacement d'implémentation

En reprenant l'exercice 7.1, montrez la puissance de l'IoC en changeant une seule ligne :

1. Remplacez `ConsoleLogService` par `FichierLogService` sans modifier `GestionnaireCommande`
2. Ajoutez une troisième implémentation `ILogService` → `MultiLogService` qui délègue aux deux (console ET fichier) en même temps
3. Enregistrez `MultiLogService` comme Singleton et vérifiez que les deux destinations reçoivent les logs

---

## 8. Async / Await

### Questions théoriques

**Q1.** Pourquoi la programmation asynchrone est-elle nécessaire ? Donnez un exemple de code synchrone problématique.

**Q2.** Quelle est la différence entre `Task` et `Task<T>` ?

**Q3.** Complétez le tableau des types de retour :

| Méthode synchrone | Équivalent asynchrone |
|---|---|
| `void Faire()` | ? |
| `string Lire()` | ? |
| `List<Produit> Obtenir()` | ? |
| `bool Valider()` | ? |

**Q4.** Quelle est la différence entre ces deux approches ? Laquelle est plus rapide et pourquoi ?

```csharp
// Approche A
string a = await TacheAAsync();
string b = await TacheBAsync();

// Approche B
Task<string> tA = TacheAAsync();
Task<string> tB = TacheBAsync();
string[] resultats = await Task.WhenAll(tA, tB);
```

**Q5.** Pourquoi `async void` est-il dangereux (sauf exception) ? Quelle exception accepte-t-on ?

**Q6.** Qu'est-ce qu'un `CancellationToken` ? Dans quel scénario l'utilise-t-on ?

**Q7.** Identifiez le piège dans ce code :

```csharp
public async Task<List<Produit>> ObtenirProduitsAsync()
{
    return _context.Produits.ToList(); // manque quelque chose
}
```

---

### Exercices pratiques

#### Exercice 8.1 — Traitement de fichiers async

Créez une application console qui simule le traitement de plusieurs rapports.

**Exigences :**

- Méthode `async Task<string> LireRapportAsync(string nom, int delaiMs)` — simule une lecture de fichier avec `Task.Delay`
- Méthode `async Task<bool> SauvegarderRapportAsync(string nom, string contenu)` — simule une écriture
- Méthode `async Task TraiterRapportAsync(string nom)` — lit, transforme (mettre en majuscules), sauvegarde, affiche le résultat
- Méthode `async Task TraiterTousEnParalleleAsync(List<string> noms)` — utilise `Task.WhenAll` pour traiter tous les rapports en même temps

**Programme :**

```csharp
var rapports = new List<string> { "RapportVentes", "RapportStock", "RapportRH", "RapportFinancier" };

Console.WriteLine("=== Traitement séquentiel ===");
var debut = DateTime.Now;
foreach (var nom in rapports)
    await TraiterRapportAsync(nom);
Console.WriteLine($"Durée : {(DateTime.Now - debut).TotalSeconds:F1}s");

Console.WriteLine("\n=== Traitement parallèle ===");
debut = DateTime.Now;
await TraiterTousEnParalleleAsync(rapports);
Console.WriteLine($"Durée : {(DateTime.Now - debut).TotalSeconds:F1}s");
// Le traitement parallèle doit être significativement plus rapide
```

---

#### Exercice 8.2 — Repository async

Reprenez `IJeuRepository` de l'exercice 6.1 et convertissez-le en version async.

**Exigences :**

```csharp
public interface IJeuRepositoryAsync
{
    Task<List<Jeu>>  ObtenirTousAsync();
    Task<Jeu?>       ObtenirParIdAsync(int id);
    Task             AjouterAsync(Jeu jeu);
    Task             ModifierAsync(Jeu jeu);
    Task             SupprimerAsync(int id);
    Task<int>        CompterAsync();
}
```

- Implémenter avec EF Core (`ToListAsync`, `FindAsync`, `SaveChangesAsync`)
- Créer `CatalogueServiceAsync` qui utilise le repository async
- Méthode `async Task ChargerCatalogueCompletAsync()` qui récupère jeux ET développeurs en parallèle avec `Task.WhenAll`
- Tester avec `CancellationTokenSource` et annuler après 100ms

---

## 9. Exercice intégrateur

### Système de gestion d'une école en console

Cet exercice combine toutes les notions vues dans le cours.

**Contexte :** Créez un système de gestion scolaire en application console (pas WPF) qui intègre POO, LINQ, EF Core, Repository, IoC et Async.

---

### Modèles (POO + EF Core)

```csharp
// Classe abstraite
public abstract class Personne
{
    public int Id { get; set; }
    [Required] public string Nom { get; set; }
    [Required] public string Prenom { get; set; }
    public string Email { get; set; }

    public abstract string ObtenirRole();
    public virtual string AfficherInfos() => $"[{ObtenirRole()}] {Prenom} {Nom}";
}

public class Etudiant : Personne
{
    public string NumeroEtudiant { get; set; }
    public string Programme { get; set; }
    public List<Inscription> Inscriptions { get; set; } = new();
    public override string ObtenirRole() => "Étudiant";
}

public class Enseignant : Personne
{
    public string Departement { get; set; }
    public List<Cours> CoursEnseignes { get; set; } = new();
    public override string ObtenirRole() => "Enseignant";
}

public class Cours
{
    public int Id { get; set; }
    public string Code { get; set; }       // ex: 420-413
    public string Titre { get; set; }
    public int NombreHeures { get; set; }
    public int EnseignantId { get; set; }
    public Enseignant? Enseignant { get; set; }
    public List<Inscription> Inscriptions { get; set; } = new();
}

public class Inscription
{
    public int Id { get; set; }
    public int EtudiantId { get; set; }
    public Etudiant? Etudiant { get; set; }
    public int CoursId { get; set; }
    public Cours? Cours { get; set; }
    public DateTime DateInscription { get; set; }
    public double? Note { get; set; }      // null si pas encore notée
}
```

---

### Interfaces (Repository + IoC)

```csharp
public interface IEtudiantRepository
{
    Task<List<Etudiant>> ObtenirTousAsync();
    Task<Etudiant?>      ObtenirParIdAsync(int id);
    Task<Etudiant?>      ObtenirAvecInscriptionsAsync(int id);
    Task                 AjouterAsync(Etudiant etudiant);
    Task                 ModifierAsync(Etudiant etudiant);
}

public interface ICoursRepository
{
    Task<List<Cours>>    ObtenirTousAsync();
    Task<Cours?>         ObtenirAvecInscritsAsync(int id);
    Task                 AjouterAsync(Cours cours);
}

public interface IInscriptionRepository
{
    Task                 InscrireAsync(int etudiantId, int coursId);
    Task                 NoterAsync(int inscriptionId, double note);
    Task<List<Inscription>> ObtenirParEtudiantAsync(int etudiantId);
    Task<bool>           EtudiantEstInscritAsync(int etudiantId, int coursId);
}

public interface INotificationService
{
    Task EnvoyerAsync(string destinataire, string message);
}
```

---

### Service métier (IoC + Async + LINQ)

Créez `EcoleService` qui reçoit tous les repositories et `INotificationService` par constructeur.

**Méthodes à implémenter :**

```csharp
// Inscription avec validation et notification
Task<(bool Succes, string Message)> InscrireEtudiantAsync(int etudiantId, int coursId);

// LINQ : bulletin de l'étudiant
Task<BulletinEtudiant> GenererBulletinAsync(int etudiantId);

// LINQ + GroupBy : statistiques de l'école
Task<StatistiquesEcole> ObtenirStatistiquesAsync();
```

**Classe `BulletinEtudiant` :**

```csharp
public class BulletinEtudiant
{
    public string NomEtudiant    { get; set; }
    public string Programme      { get; set; }
    public List<(string Cours, double? Note, string Statut)> Resultats { get; set; }
    public double? MoyenneGenerale { get; set; }  // null si aucune note
    public bool    EstAdmis        { get; set; }  // true si moyenne >= 60
}
```

**Classe `StatistiquesEcole` :**

```csharp
public class StatistiquesEcole
{
    public int    NombreEtudiants        { get; set; }
    public int    NombreCours            { get; set; }
    public int    NombreInscriptions     { get; set; }
    public double MoyenneGeneraleEcole   { get; set; }
    public List<(string Programme, int NbEtudiants, double MoyenneProgramme)> ParProgramme { get; set; }
    public List<(string Cours, int NbInscrits)> CoursLesPlusPopulaires { get; set; }
}
```

---

### Patron Observateur (bonus)

Ajoutez un système de notifications via événements C# :

- `EcoleService` déclare un événement `event EventHandler<InscriptionEventArgs> NouvelleInscription`
- `InscriptionEventArgs` contient `NomEtudiant`, `TitreCours`, `DateInscription`
- Dans `Program.cs`, abonnez un gestionnaire qui affiche l'inscription dans la console

---

### `Program.cs` attendu

```csharp
// 1. Configurer IoC
var services = new ServiceCollection();
services.AddDbContext<EcoleDbContext>(...);
services.AddScoped<IEtudiantRepository, EtudiantRepository>();
services.AddScoped<ICoursRepository, CoursRepository>();
services.AddScoped<IInscriptionRepository, InscriptionRepository>();
services.AddScoped<INotificationService, ConsoleNotificationService>();
services.AddScoped<EcoleService>();
var provider = services.BuildServiceProvider();

// 2. Seeder les données
// 3. Inscrire des étudiants et générer leurs bulletins
// 4. Afficher les statistiques de l'école
// 5. Démontrer Task.WhenAll en générant plusieurs bulletins en parallèle
```

---