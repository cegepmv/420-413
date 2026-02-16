---
title: "Pratique - Exam 1"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 13
---

# 📝 EXERCICE DE PRÉPARATION À L'EXAMEN
## Système de Gestion d'une Bibliothèque Municipale
### POO Avancée + LINQ

---

## 🎯 CONTEXTE

Vous travaillez pour la Ville de Montréal et devez créer un système de gestion pour la bibliothèque municipale. Le système doit gérer différents types de documents (livres, magazines, DVD), les membres, les emprunts et générer des statistiques.

**Technologies:** Application Console C# (.NET), POO avancée (héritage, interfaces, polymorphisme), LINQ obligatoire pour toutes les requêtes

---

## 📋 PARTIE 1: MODÈLE DE DONNÉES

### 1.1 Énumérations à créer

**Enum `GenreLivre`**
- Roman
- ScienceFiction
- Policier
- Biographie
- Histoire
- Science
- Jeunesse
- BD

**Enum `StatutEmprunt`**
- EnCours
- Retourne
- EnRetard
- Perdu

**Enum `TypeMembre`**
- Regulier
- Etudiant
- Senior

---

### 1.2 Interface IEmpruntable

Créez une interface qui définit le comportement des éléments empruntables.

**Interface `IEmpruntable`**

**Propriétés:**
- `bool EstDisponible { get; }` - Lecture seule, indique si l'item est disponible
- `string Titre { get; }` - Lecture seule, titre de l'item

**Méthodes:**
- `bool PeutEtreEmprunte()` - Retourne true si l'item peut être emprunté
- `bool Emprunter()` - Tente d'emprunter, retourne true si succès
- `void Retourner()` - Retourne l'item
- `int ObtenirDureeEmprunt(TypeMembre typeMembre)` - Retourne la durée d'emprunt selon le type de membre

---

### 1.3 Classe abstraite Document

Créez une classe abstraite qui servira de base pour tous les types de documents.

**Classe abstraite `Document`**

**Champ statique:**
- `private static int prochainId = 1` - Compteur pour générer des ID uniques

**Propriétés:**
- `int Id { get; protected set; }` - Identifiant unique auto-généré
- `string Titre { get; set; }` - Titre du document
- `int AnneePublication { get; set; }` - Année de publication
- `bool EstDisponible { get; protected set; }` - Disponibilité

**Propriétés calculées:**
- `int Age` - Retourne l'âge du document (année actuelle - année publication)
- `bool EstRecent` - Retourne true si publié dans les 5 dernières années

**Propriétés abstraites à implémenter dans les classes dérivées:**
- `abstract string TypeDocument { get; }` - Retourne le type de document ("Livre", "Magazine", "DVD")

**Constructeur:**
```csharp
protected Document()
{
    Id = prochainId++;
    EstDisponible = true;
}
```

**Méthodes virtuelles:**
- `virtual string ObtenirDescription()` - Retourne "{Titre} ({AnneePublication})"
- `virtual void AfficherInfos()` - Affiche les informations de base du document

**Méthodes abstraites:**
- `abstract int ObtenirDureeEmpruntDefaut()` - Durée d'emprunt par défaut selon le type de document

**Override obligatoire:**
- `override string ToString()` - Format: "TypeDocument: Titre"

---

### 1.4 Classe Livre (hérite de Document et implémente IEmpruntable)

**Classe `Livre : Document, IEmpruntable`**

**Propriétés spécifiques:**
- `string Auteur { get; set; }` - Nom de l'auteur
- `string ISBN { get; set; }` - Numéro ISBN
- `GenreLivre Genre { get; set; }` - Genre du livre
- `int NombrePages { get; set; }` - Nombre de pages
- `int NombreExemplaires { get; set; }` - Nombre total d'exemplaires
- `int NombreDisponibles { get; set; }` - Nombre d'exemplaires disponibles

**Implémentation propriété abstraite:**
- `override string TypeDocument` - Retourne "Livre"

**Override méthodes virtuelles:**
- `override string ObtenirDescription()` - Retourne "{Titre} par {Auteur} ({AnneePublication})"
- `override void AfficherInfos()` - Affiche toutes les infos du livre incluant auteur, genre, pages

**Implémentation méthode abstraite:**
- `override int ObtenirDureeEmpruntDefaut()` - Retourne 14 jours

**Implémentation interface IEmpruntable:**
- `bool PeutEtreEmprunte()` - Retourne true si NombreDisponibles > 0
- `bool Emprunter()` - Diminue NombreDisponibles si possible, met à jour EstDisponible
- `void Retourner()` - Augmente NombreDisponibles, met à jour EstDisponible
- `int ObtenirDureeEmprunt(TypeMembre typeMembre)` - Retourne durée selon type:
  - Regulier: 14 jours
  - Etudiant: 21 jours
  - Senior: 21 jours

**Constructeur:**
```csharp
public Livre() : base()
{
    NombreExemplaires = 1;
    NombreDisponibles = 1;
}
```

---

### 1.5 Classe Magazine (hérite de Document et implémente IEmpruntable)

**Classe `Magazine : Document, IEmpruntable`**

**Propriétés spécifiques:**
- `int NumeroEdition { get; set; }` - Numéro de l'édition
- `string Editeur { get; set; }` - Nom de l'éditeur
- `int Periodicite { get; set; }` - Périodicité en jours (7 pour hebdo, 30 pour mensuel)

**Implémentation propriété abstraite:**
- `override string TypeDocument` - Retourne "Magazine"

**Override méthodes virtuelles:**
- `override string ObtenirDescription()` - Retourne "{Titre} #{NumeroEdition} - {Editeur}"
- `override void AfficherInfos()` - Affiche infos du magazine incluant éditeur, numéro

**Implémentation méthode abstraite:**
- `override int ObtenirDureeEmpruntDefaut()` - Retourne 7 jours (magazines = emprunts courts)

**Implémentation interface IEmpruntable:**
- `bool PeutEtreEmprunte()` - Retourne EstDisponible
- `bool Emprunter()` - Met EstDisponible à false
- `void Retourner()` - Met EstDisponible à true
- `int ObtenirDureeEmprunt(TypeMembre typeMembre)` - Retourne toujours 7 jours (même durée pour tous)

---

### 1.6 Classe Membre

**Propriétés:**
- `int Id { get; private set; }` - Identifiant unique auto-généré
- `string Nom { get; set; }` - Nom complet
- `string NumeroMembre { get; set; }` - Format: MEM-XXXXX
- `string Courriel { get; set; }` - Adresse courriel
- `TypeMembre Type { get; set; }` - Type de membre
- `DateTime DateInscription { get; set; }` - Date d'inscription
- `List<Emprunt> Emprunts { get; set; }` - Liste de tous les emprunts

**Champ statique:**
```csharp
private static int prochainId = 1;
```

**Propriétés calculées:**
- `int NombreEmpruntsActuels` - Compte les emprunts avec Statut == EnCours
- `int NombreEmpruntsTotal` - Total d'emprunts dans l'historique
- `bool PeutEmprunter` - true si NombreEmpruntsActuels < LimiteEmprunts
- `int LimiteEmprunts` - Selon le type:
  - Regulier: 5
  - Etudiant: 10
  - Senior: 8
- `int JoursMembre` - Jours depuis l'inscription

**Constructeur:**
```csharp
public Membre()
{
    Id = prochainId++;
    Emprunts = new List<Emprunt>();
    DateInscription = DateTime.Now;
}
```

**Méthodes:**
- `void AjouterEmprunt(Emprunt emprunt)` - Ajoute un emprunt
- `override string ToString()` - Format: "NumeroMembre - Nom (Type)"

---

### 1.7 Classe Emprunt

**Propriétés:**
- `int Id { get; private set; }` - ID unique auto-généré
- `IEmpruntable Document { get; set; }` - Le document emprunté (interface!)
- `Membre Membre { get; set; }` - Le membre qui emprunte
- `DateTime DateEmprunt { get; set; }` - Date de l'emprunt
- `DateTime DateRetourPrevue { get; set; }` - Date de retour prévue
- `DateTime? DateRetourReelle { get; set; }` - Date de retour effective (nullable)
- `StatutEmprunt Statut { get; set; }` - Statut actuel

**Champ statique:**
```csharp
private static int prochainId = 1;
```

**Propriétés calculées:**
- `int DureeEmprunt` - Obtenue via Document.ObtenirDureeEmprunt(Membre.Type)
- `int JoursEmprunt` - Jours depuis DateEmprunt
- `bool EstEnRetard` - true si maintenant > DateRetourPrevue ET Statut == EnCours
- `int JoursRetard` - Jours de retard (0 si pas en retard)
- `decimal Penalite` - JoursRetard × 0.50$

**Constructeur:**
```csharp
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
```

**Méthodes:**
- `void Retourner()` - Met DateRetourReelle, change Statut
- `override string ToString()` - Format: "Document.Titre - Membre.Nom - Statut"

---

## 📋 PARTIE 2: CLASSE GESTIONNAIRE AVEC POLYMORPHISME

### Classe `GestionnaireBibliotheque`

**Propriétés:**
- `List<Document> Documents { get; set; }` - Tous les documents (polymorphisme!)
- `List<Membre> Membres { get; set; }` - Tous les membres
- `List<Emprunt> Emprunts { get; set; }` - Tous les emprunts

**Constructeur:**
- Initialise les 3 listes vides

---

### MÉTHODES À IMPLÉMENTER AVEC LINQ

#### 2.1 Gestion de base avec polymorphisme

**`void AjouterDocument(Document document)`**
- Ajoute un document (peut être Livre ou Magazine)
- Valide que l'ID n'existe pas déjà (LINQ)

**`void AjouterMembre(Membre membre)`**
- Ajoute un membre
- Valide que le courriel n'existe pas déjà (LINQ)

**`bool CreerEmprunt(int documentId, int membreId)`**
- Trouve le document avec LINQ (FirstOrDefault)
- **Cast vers IEmpruntable** si le document implémente l'interface
- Trouve le membre avec LINQ
- Vérifie PeutEmprunter du membre
- Vérifie PeutEtreEmprunte() du document
- Crée l'Emprunt et l'ajoute aux listes
- Appelle document.Emprunter()
- Retourne true si succès

**`bool RetournerDocument(int empruntId)`**
- Trouve l'emprunt avec LINQ
- Appelle Emprunt.Retourner()
- Appelle Document.Retourner()
- Retourne true si succès

---

#### 2.2 Recherches LINQ avec polymorphisme

**`List<Document> ObtenirDocumentsDisponibles()`**
- Retourne tous les documents disponibles
- Triés par Titre

**`List<Livre> ObtenirLivresParGenre(GenreLivre genre)`**
- Filtre les documents qui sont des Livre (utilisez `is` ou `OfType<Livre>()`)
- Filtre par genre
- Triés par AnneePublication décroissant

**`List<Magazine> ObtenirMagazinesRecents()`**
- Filtre les documents qui sont des Magazine
- Où EstRecent == true
- Triés par NumeroEdition décroissant

**`List<Document> ObtenirDocumentsParAuteur(string auteur)`**
- Filtre les documents qui sont des Livre
- Dont l'auteur contient la chaîne (ignore casse)
- Retourne comme List<Document> (polymorphisme!)
- Triés par Titre

**`List<Emprunt> ObtenirEmpruntsEnRetard()`**
- Tous les emprunts où EstEnRetard == true
- Triés par JoursRetard décroissant

---

#### 2.3 Statistiques LINQ

**`int CompterDocumentsParType(string typeDocument)`**
- Compte les documents où TypeDocument == paramètre
- Exemple: CompterDocumentsParType("Livre")

**`int CompterLivresParGenre(GenreLivre genre)`**
- Filtre les Livre du genre donné
- Compte le total

**`decimal CalculerPenalitesTotales()`**
- Somme toutes les pénalités

**`double CalculerMoyenneEmpruntsParMembre()`**
- Moyenne d'emprunts par membre

**`Document ObtenirDocumentLePlusEmprunte()`**
- GroupBy sur Document
- OrderByDescending par Count
- Retourne le premier

---

#### 2.4 Requêtes avancées avec polymorphisme

**`Dictionary<string, int> ObtenirStatistiquesParTypeDocument()`**
- GroupBy sur TypeDocument
- Retourne: "Livre" → 25, "Magazine" → 15
- Trié par nombre décroissant

**`var ObtenirStatistiquesCompletes()`**
- Pour chaque type de document, retourne (type anonyme):
```csharp
new {
    TypeDocument = "Livre",
    Nombre = ...,
    NombreDisponibles = ...,
    TauxDisponibilite = ...
}
```

**`List<IEmpruntable> ObtenirDocumentsEmpruntables()`**
- Retourne tous les documents qui sont IEmpruntable
- Où PeutEtreEmprunte() == true
- **Important:** retour polymorphe comme IEmpruntable

**`Dictionary<TypeMembre, List<Membre>> GrouperMembresParType()`**
- GroupBy par type
- Retourne dictionnaire

**`var ObtenirTop5MembresActifs()`**
- Top 5 par nombre d'emprunts totaux
- Type anonyme avec Nom et NombreEmprunts

**`List<Emprunt> ObtenirHistoriqueMembreParType(int membreId, string typeDocument)`**
- Emprunts d'un membre
- Filtre par TypeDocument du Document
- Triés par DateEmprunt décroissant

---

#### 2.5 Méthodes utilisant le polymorphisme (IMPORTANT!)

**`void AfficherTousLesDocuments()`**
- Parcourt la liste Documents
- Pour chaque document, appelle **document.AfficherInfos()** (polymorphisme!)
- La bonne méthode est appelée selon le type réel

**`List<string> ObtenirDescriptionsTousDocuments()`**
- Select sur Documents
- Appelle **document.ObtenirDescription()** pour chacun (polymorphisme!)
- Retourne la liste des descriptions

**`Dictionary<string, int> CalculerDureeMoyenneParType()`**
- GroupBy par TypeDocument
- Pour chaque groupe, calcule la durée moyenne d'emprunt
- Retourne: "Livre" → 18, "Magazine" → 7

---

## 📋 PARTIE 3: PROGRAMME PRINCIPAL

Dans `Program.cs`:

1. **Créez un GestionnaireBibliotheque**

2. **Ajoutez des données de test:**
   - Au moins 8 Livres (différents genres)
   - Au moins 4 Magazines
   - Au moins 5 Membres (différents types)
   - Au moins 10 Emprunts (certains en retard, certains de livres, certains de magazines)

3. **Démontrez le polymorphisme:**
   ```csharp
   // Ajout polymorphe
   Document doc1 = new Livre { Titre = "1984", Auteur = "Orwell" };
   Document doc2 = new Magazine { Titre = "Science et Vie", NumeroEdition = 125 };
   
   gestionnaire.AjouterDocument(doc1);
   gestionnaire.AjouterDocument(doc2);
   
   // Affichage polymorphe
   gestionnaire.AfficherTousLesDocuments();
   ```

4. **Testez et affichez:**
   - Statistiques par type de document
   - Documents disponibles (livres ET magazines)
   - Emprunts en retard avec pénalités
   - Top 5 membres actifs
   - Descriptions de tous les documents (polymorphisme!)

**Format d'affichage suggéré:**
```
═══════════════════════════════════════════════
    SYSTÈME DE GESTION - BIBLIOTHÈQUE
═══════════════════════════════════════════════

📊 STATISTIQUES PAR TYPE:
   • Livre: 8 documents (5 disponibles) - 62.5%
   • Magazine: 4 documents (3 disponibles) - 75.0%

📚 TOUS LES DOCUMENTS DISPONIBLES:
   • Livre: 1984 par George Orwell (1949)
   • Magazine: Science et Vie #125 - Editeur XYZ
   • Livre: Le Petit Prince par Saint-Exupéry (1943)

⏰ EMPRUNTS EN RETARD:
   • 1984 - Alice Tremblay - 5 jours - 2.50$
   • Science et Vie #120 - Bob Gagnon - 2 jours - 1.00$

💰 PÉNALITÉS TOTALES: 3.50$

🏆 TOP 5 MEMBRES ACTIFS:
   1. Alice Tremblay - 8 emprunts
   2. Bob Gagnon - 6 emprunts
   3. Charlie Roy - 4 emprunts
```

---

## ✅ CRITÈRES D'ÉVALUATION

### POO Avancée (40%)
- [ ] Interface IEmpruntable correctement définie et implémentée
- [ ] Classe abstraite Document avec propriétés/méthodes abstraites et virtuelles
- [ ] Héritage correct: Livre et Magazine héritent de Document
- [ ] Implémentation correcte de l'interface dans les deux classes
- [ ] Utilisation de `override` pour les méthodes virtuelles/abstraites
- [ ] Utilisation de `base` dans les constructeurs
- [ ] Polymorphisme démontré (Document peut référer Livre ou Magazine)
- [ ] Propriété protégée (protected) utilisée correctement

### LINQ (40%)
- [ ] Utilisation de `OfType<T>()` ou `is` pour filtrer par type
- [ ] Toutes les méthodes utilisent LINQ (pas de boucles)
- [ ] Where, OrderBy, Select correctement utilisés
- [ ] GroupBy, Count, Sum, Average correctement utilisés
- [ ] FirstOrDefault avec gestion du null
- [ ] Types anonymes utilisés

### Qualité du code (20%)
- [ ] Code compile sans erreurs
- [ ] Noms significatifs
- [ ] Gestion des cas null
- [ ] Polymorphisme bien exploité
- [ ] Programme principal démontre bien les concepts

---

## 🎓 CONCEPTS POO À MAÎTRISER

### Classe abstraite vs Interface

**Classe abstraite** (Document):
- Peut avoir des propriétés concrètes ET abstraites
- Peut avoir des méthodes implémentées ET abstraites
- Peut avoir un constructeur
- Héritage simple uniquement (une classe ne peut hériter que d'une classe abstraite)

**Interface** (IEmpruntable):
- Définit uniquement un contrat (signatures)
- Pas d'implémentation
- Pas de constructeur
- Une classe peut implémenter plusieurs interfaces

### Mots-clés importants

**`abstract`** - Classe ou membre qui DOIT être implémenté
```csharp
public abstract string TypeDocument { get; }
public abstract int ObtenirDureeEmpruntDefaut();
```

**`virtual`** - Membre qui PEUT être surchargé
```csharp
public virtual string ObtenirDescription() { ... }
```

**`override`** - Surcharge un membre virtual ou abstract
```csharp
public override string ObtenirDescription() { ... }
```

**`protected`** - Accessible dans la classe et les classes dérivées
```csharp
protected set { ... }
```

**`base`** - Appelle le constructeur/méthode de la classe parent
```csharp
public Livre() : base() { }
```

### Pattern matching avec `is` et `as`

```csharp
// Vérifier le type
if (document is Livre livre)
{
    Console.WriteLine(livre.Auteur);
}

// Cast sécuritaire
IEmpruntable empruntable = document as IEmpruntable;
if (empruntable != null)
{
    empruntable.Emprunter();
}

// Avec LINQ
var livres = Documents.OfType<Livre>();
```

---

## 📚 RAPPELS LINQ SPÉCIFIQUES AU POLYMORPHISME

```csharp
// Filtrer par type avec OfType
var livres = Documents.OfType<Livre>();

// Filtrer par type avec Where et is
var magazines = Documents.Where(d => d is Magazine);

// Cast après filtrage
var livresSciFi = Documents
    .OfType<Livre>()
    .Where(l => l.Genre == GenreLivre.ScienceFiction);

// GroupBy sur propriété polymorphe
var stats = Documents
    .GroupBy(d => d.TypeDocument)
    .Select(g => new { Type = g.Key, Count = g.Count() });

// Utiliser l'interface
var disponibles = Documents
    .OfType<IEmpruntable>()
    .Where(e => e.PeutEtreEmprunte());
```

---


*Focus: Héritage, Interfaces, Polymorphisme et LINQ!*