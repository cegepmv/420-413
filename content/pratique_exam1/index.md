---
title: "Pratique - Exam 1"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 13
---

# 📝 EXERCICE DE PRÉPARATION À L'EXAMEN
## Système de Gestion d'une Bibliothèque Municipale
### POO + LINQ

---

## 🎯 CONTEXTE

Vous travaillez pour la Ville de Montréal et devez créer un système de gestion pour la bibliothèque municipale. Le système doit gérer les livres, les membres, les emprunts et générer des statistiques.

**Technologies:** Application Console C# (.NET), POO avancée, LINQ obligatoire pour toutes les requêtes

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

### 1.2 Classes à créer

#### Classe `Livre`

**Propriétés:**
- `int Id` - Identifiant unique
- `string Titre` - Titre du livre
- `string Auteur` - Nom de l'auteur
- `string ISBN` - Numéro ISBN (format: XXX-X-XXXX-XXXX-X)
- `int AnneePublication` - Année de publication
- `GenreLivre Genre` - Genre du livre
- `int NombrePages` - Nombre de pages
- `bool EstDisponible` - Disponibilité actuelle
- `int NombreExemplaires` - Nombre total d'exemplaires
- `int NombreDisponibles` - Nombre d'exemplaires disponibles

**Propriétés calculées à implémenter:**
- `int Age` - Retourne l'âge du livre (année actuelle - année publication)
- `bool EstRecent` - Retourne true si publié dans les 5 dernières années
- `string Description` - Retourne "{Titre} par {Auteur} ({AnneePublication})"

**Méthodes à implémenter:**
- `bool Emprunter()` - Diminue NombreDisponibles de 1 si possible, retourne true si succès
- `void Retourner()` - Augmente NombreDisponibles de 1
- `override string ToString()` - Format: "Titre - Auteur (Genre)"

---

#### Classe `Membre`

**Propriétés:**
- `int Id` - Identifiant unique - auto-généré (pas de set)
- `string Nom` - Nom complet
- `string NumeroMembre` - Format: MEM-XXXXX (ex: MEM-00123)
- `string Courriel` - Adresse courriel
- `TypeMembre Type` - Type de membre
- `DateTime DateInscription` - Date d'inscription
- `List<Emprunt> Emprunts` - Liste de tous les emprunts (historique)

**Propriétés calculées à implémenter:**
- `int NombreEmpruntsActuels` - Nombre d'emprunts en cours (StatutEmprunt.EnCours)
- `int NombreEmpruntsTotal` - Nombre total d'emprunts dans l'historique
- `bool PeutEmprunter` - true si NombreEmpruntsActuels < LimiteEmprunts
- `int LimiteEmprunts` - Retourne la limite selon le type:
  - Regulier: 5 livres
  - Etudiant: 10 livres
  - Senior: 8 livres
- `int JoursMembre` - Nombre de jours depuis l'inscription

**Méthodes à implémenter:**
- `void AjouterEmprunt(Emprunt emprunt)` - Ajoute un emprunt à la liste
- `override string ToString()` - Format: "NumeroMembre - Nom (Type)"

---

#### Classe `Emprunt`

**Propriétés:**
- `int Id` - Identifiant unique
- `Livre Livre` - Le livre emprunté
- `Membre Membre` - Le membre qui emprunte
- `DateTime DateEmprunt` - Date de l'emprunt
- `DateTime DateRetourPrevue` - Date de retour prévue (DateEmprunt + durée)
- `DateTime? DateRetourReelle` - Date de retour effective (nullable)
- `StatutEmprunt Statut` - Statut actuel de l'emprunt

**Propriétés calculées à implémenter:**
- `int DureeEmprunt` - Durée selon le type de membre:
  - Regulier: 14 jours
  - Etudiant: 21 jours
  - Senior: 21 jours
- `int JoursEmprunt` - Nombre de jours depuis l'emprunt
- `bool EstEnRetard` - true si date actuelle > DateRetourPrevue ET Statut == EnCours
- `int JoursRetard` - Nombre de jours de retard (0 si pas en retard)
- `decimal Penalite` - Calcul: JoursRetard × 0.50$ (0 si pas de retard)

**Méthodes à implémenter:**
- `void Retourner()` - Met DateRetourReelle à aujourd'hui, change Statut à Retourne ou EnRetard
- `override string ToString()` - Format: "Livre.Titre - Membre.Nom - Statut"

---

## 📋 PARTIE 2: CLASSE GESTIONNAIRE

### Classe `GestionnaireBibliotheque`

Cette classe contient toutes les méthodes de gestion et requêtes LINQ.

**Propriétés:**
- `List<Livre> Livres` - Collection de tous les livres
- `List<Membre> Membres` - Collection de tous les membres
- `List<Emprunt> Emprunts` - Collection de tous les emprunts

**Constructeur:**
- Initialise les 3 listes vides

---

### MÉTHODES À IMPLÉMENTER AVEC LINQ (OBLIGATOIRE!)

#### 2.1 Gestion de base

**`void AjouterLivre(Livre livre)`**
- Ajoute un livre à la liste
- Valide que l'ISBN n'existe pas déjà

**`void AjouterMembre(Membre membre)`**
- Ajoute un membre à la liste
- Valide que le courriel n'existe pas déjà

**`bool CreerEmprunt(int livreId, int membreId)`**
- Trouve le livre et le membre avec LINQ (FirstOrDefault)
- Vérifie que le membre peut emprunter (PeutEmprunter)
- Vérifie que le livre est disponible (EstDisponible)
- Crée l'emprunt et l'ajoute aux deux listes (Emprunts et Membre.Emprunts)
- Appelle Livre.Emprunter()
- Retourne true si succès, false sinon

**`bool RetournerLivre(int empruntId)`**
- Trouve l'emprunt avec LINQ
- Appelle Emprunt.Retourner()
- Appelle Livre.Retourner()
- Retourne true si succès, false sinon

---

#### 2.2 Recherches LINQ (utilisez Where, OrderBy, Select)

**`List<Livre> ObtenirLivresDisponibles()`**
- Retourne tous les livres où EstDisponible == true
- Triés par Titre (ordre alphabétique)

**`List<Livre> ObtenirLivresParGenre(GenreLivre genre)`**
- Retourne tous les livres d'un genre spécifique
- Triés par AnneePublication décroissant (plus récents d'abord)

**`List<Livre> ObtenirLivresParAuteur(string auteur)`**
- Retourne les livres dont l'auteur contient la chaîne donnée (ignore la casse)
- Triés par Titre

**`List<Membre> ObtenirMembresParType(TypeMembre type)`**
- Retourne tous les membres d'un type donné
- Triés par Nom

**`List<Emprunt> ObtenirEmpruntsEnRetard()`**
- Retourne tous les emprunts où EstEnRetard == true
- Triés par JoursRetard décroissant (plus en retard d'abord)

---

#### 2.3 Statistiques LINQ (utilisez Count, Sum, Average, Max, Min)

**`int CompterLivresParGenre(GenreLivre genre)`**
- Compte le nombre de livres d'un genre donné

**`int CompterEmpruntsActifsParMembre(int membreId)`**
- Compte les emprunts en cours d'un membre spécifique

**`decimal CalculerPenalitesTotales()`**
- Somme toutes les pénalités de tous les emprunts

**`double CalculerMoyenneEmpruntsParMembre()`**
- Calcule le nombre moyen d'emprunts (total) par membre

**`Livre ObtenirLivreLePlusEmprunte()`**
- Retourne le livre qui apparaît le plus souvent dans les emprunts
- Indice: GroupBy sur Livre, OrderByDescending sur Count, puis First

---

#### 2.4 Requêtes avancées LINQ (utilisez GroupBy, Join, SelectMany)

**`Dictionary<GenreLivre, int> ObtenirStatistiquesGenres()`**
- Groupe les livres par genre
- Retourne un dictionnaire: Genre → Nombre de livres
- Trié par nombre de livres décroissant

**`Dictionary<TypeMembre, List<Membre>> GrouperMembresParType()`**
- Groupe les membres par type
- Retourne un dictionnaire: Type → Liste de membres

**`List<Livre> ObtenirLivresNonEmpruntes()`**
- Retourne les livres qui n'apparaissent dans AUCUN emprunt
- Indice: Utiliser Where avec !Emprunts.Any(...)

**`var ObtenirTop5MembresActifs()`**
- Retourne les 5 membres avec le plus d'emprunts totaux
- Format de retour (type anonyme):
  ```csharp
  new { 
      Nom = membre.Nom, 
      NombreEmprunts = membre.Emprunts.Count 
  }
  ```
- Triés par NombreEmprunts décroissant

**`List<Emprunt> ObtenirHistoriqueMembreParGenre(int membreId, GenreLivre genre)`**
- Retourne tous les emprunts d'un membre pour un genre spécifique
- Triés par DateEmprunt décroissant (plus récents d'abord)

---

## 📋 PARTIE 3: PROGRAMME PRINCIPAL

Dans `Program.cs`, créez une méthode `Main` qui:

1. Crée une instance de `GestionnaireBibliotheque`

2. Ajoute des données de test:
   - Au moins 10 livres de différents genres
   - Au moins 5 membres de différents types
   - Au moins 8 emprunts (dont certains en retard)

3. Teste les méthodes suivantes et affiche les résultats:
   - `ObtenirLivresDisponibles()` - Afficher le nombre
   - `ObtenirEmpruntsEnRetard()` - Afficher chaque emprunt en retard avec la pénalité
   - `CalculerPenalitesTotales()` - Afficher le montant total
   - `ObtenirStatistiquesGenres()` - Afficher chaque genre avec son compte
   - `ObtenirTop5MembresActifs()` - Afficher le classement

**Format d'affichage suggéré:**
```
═══════════════════════════════════════════════
    SYSTÈME DE GESTION - BIBLIOTHÈQUE
═══════════════════════════════════════════════

📚 LIVRES DISPONIBLES: 7

⏰ EMPRUNTS EN RETARD:
   • 1984 - Alice Tremblay - 5 jours - Pénalité: 2.50$
   • Le Petit Prince - Bob Gagnon - 3 jours - Pénalité: 1.50$

💰 PÉNALITÉS TOTALES: 4.00$

📊 STATISTIQUES PAR GENRE:
   • Roman: 4 livres
   • ScienceFiction: 3 livres
   • Policier: 2 livres

🏆 TOP 5 MEMBRES ACTIFS:
   1. Alice Tremblay - 12 emprunts
   2. Bob Gagnon - 8 emprunts
   3. Charlie Roy - 6 emprunts
```

---

## ✅ CRITÈRES D'ÉVALUATION

### Modèle de données (30%)
- [ ] Toutes les classes créées avec les propriétés demandées
- [ ] Propriétés calculées fonctionnelles
- [ ] Méthodes de base implémentées
- [ ] Utilisation correcte des enums

### Requêtes LINQ (50%)
- [ ] Toutes les méthodes utilisent LINQ (pas de boucles for/foreach)
- [ ] Utilisation correcte de Where, OrderBy, Select
- [ ] Utilisation correcte de Count, Sum, Average
- [ ] Utilisation correcte de GroupBy et types anonymes
- [ ] Requêtes fonctionnelles et retournent les bons résultats

### Qualité du code (20%)
- [ ] Noms de variables significatifs
- [ ] Code lisible et bien organisé
- [ ] Gestion des cas null (FirstOrDefault, etc.)
- [ ] Programme compile sans erreurs


---

## 📚 RAPPELS LINQ UTILES

```csharp
// Filtrer
var resultat = liste.Where(x => x.Propriete > 10);

// Trier
var resultat = liste.OrderBy(x => x.Nom).ThenBy(x => x.Age);

// Trier décroissant
var resultat = liste.OrderByDescending(x => x.Date);

// Compter
int nombre = liste.Count(x => x.EstActif);

// Somme
decimal total = liste.Sum(x => x.Montant);

// Moyenne
double moyenne = liste.Average(x => x.Note);

// Premier élément (ou null)
var element = liste.FirstOrDefault(x => x.Id == 5);

// Grouper
var groupes = liste.GroupBy(x => x.Categorie);

// Grouper et compter
var stats = liste
    .GroupBy(x => x.Categorie)
    .ToDictionary(g => g.Key, g => g.Count());

// Type anonyme
var resultat = liste.Select(x => new { 
    x.Nom, 
    x.Age 
});

// Vérifier existence
bool existe = liste.Any(x => x.Nom == "Alice");
```

---

*N'oubliez pas: LINQ partout, pas de boucles for/foreach dans les requêtes!*