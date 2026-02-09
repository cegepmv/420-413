---
title: "LINQ"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 19
---

# 📘 LINQ en C#
## Language Integrated Query 

---

## 📋 Table des matières

1. [Introduction à LINQ](#1-introduction-à-linq)
2. [Pourquoi utiliser LINQ ?](#2-pourquoi-utiliser-linq)
3. [Les Bases de LINQ](#3-les-bases-de-linq)
4. [Les Deux Syntaxes LINQ](#4-les-deux-syntaxes-linq)
5. [Opérations de Filtrage](#5-opérations-de-filtrage)
6. [Opérations de Projection](#6-opérations-de-projection)
7. [Opérations de Tri](#7-opérations-de-tri)
8. [Opérations d'Agrégation](#8-opérations-dagrégation)
9. [Opérations de Regroupement](#9-opérations-de-regroupement)
10. [Opérations de Jointure](#10-opérations-de-jointure)
11. [Opérations d'Ensemble](#11-opérations-densemble)
12. [Opérations de Quantification](#12-opérations-de-quantification)
13. [Opérations de Partition](#13-opérations-de-partition)
14. [LINQ avec Objets Complexes](#14-linq-avec-objets-complexes)
15. [Cas Pratiques Réels](#15-cas-pratiques-réels)
16. [Bonnes Pratiques et Pièges](#16-bonnes-pratiques-et-pièges)

---

## 1. Introduction à LINQ

### 1.1 Qu'est-ce que LINQ ?

**LINQ** (Language Integrated Query) est une fonctionnalité de C# qui permet d'**interroger des collections de données** directement dans le code, avec une syntaxe élégante et lisible.

**Analogie :**
Imaginez que vous avez une bibliothèque remplie de livres. Plutôt que de parcourir manuellement chaque étagère pour trouver "tous les romans de science-fiction publiés après 2020", LINQ vous permet de poser cette question directement et d'obtenir la réponse instantanément.

### 1.2 Les sources de données LINQ

LINQ peut interroger différents types de données :

```csharp
// LINQ to Objects - Collections en mémoire
List<int> nombres = new List<int> { 1, 2, 3, 4, 5 };
var pairs = from n in nombres where n % 2 == 0 select n;

// LINQ to XML - Documents XML
XDocument doc = XDocument.Load("data.xml");
var elements = from e in doc.Descendants("item") select e;

// LINQ to SQL / Entity Framework - Bases de données
var clients = from c in dbContext.Clients where c.Ville == "Montréal" select c;
```

**Dans ce cours**, nous nous concentrerons sur **LINQ to Objects** (collections en mémoire).

### 1.3 Mise en contexte : Problème sans LINQ

```csharp
// ==========================================
// SCÉNARIO : Trouver tous les étudiants avec une moyenne >= 75
// ==========================================

public class Etudiant
{
    public string Nom { get; set; }
    public int Age { get; set; }
    public double Moyenne { get; set; }
    public string Programme { get; set; }
}

// Données
List<Etudiant> etudiants = new List<Etudiant>
{
    new Etudiant { Nom = "Alice", Age = 20, Moyenne = 85.5, Programme = "Informatique" },
    new Etudiant { Nom = "Bob", Age = 22, Moyenne = 72.0, Programme = "Génie" },
    new Etudiant { Nom = "Charlie", Age = 21, Moyenne = 90.0, Programme = "Informatique" },
    new Etudiant { Nom = "Diana", Age = 19, Moyenne = 68.5, Programme = "Math" },
    new Etudiant { Nom = "Eve", Age = 23, Moyenne = 78.0, Programme = "Informatique" }
};

// ==========================================
// SANS LINQ - Approche traditionnelle (boucles)
// ==========================================
List<Etudiant> etudiantsAvecBonneMoyenne = new List<Etudiant>();

foreach (var etudiant in etudiants)
{
    if (etudiant.Moyenne >= 75)
    {
        etudiantsAvecBonneMoyenne.Add(etudiant);
    }
}

Console.WriteLine("Étudiants avec moyenne >= 75:");
foreach (var etudiant in etudiantsAvecBonneMoyenne)
{
    Console.WriteLine($"{etudiant.Nom}: {etudiant.Moyenne}");
}

// Affiche:
// Alice: 85.5
// Charlie: 90
// Eve: 78
```

**Problèmes avec cette approche :**
- Code verbeux (beaucoup de lignes pour une tâche simple)
- Lisibilité réduite
- Variables temporaires nécessaires
- Difficile à combiner plusieurs opérations

---

## 2. Pourquoi utiliser LINQ ?

### 2.1 Avantages de LINQ

```csharp
// ==========================================
// AVEC LINQ - Élégant et concis
// ==========================================
var etudiantsAvecBonneMoyenne = etudiants.Where(e => e.Moyenne >= 75);

foreach (var etudiant in etudiantsAvecBonneMoyenne)
{
    Console.WriteLine($"{etudiant.Nom}: {etudiant.Moyenne}");
}

// Même résultat, mais en UNE SEULE LIGNE ! 🎯
```

**Avantages :**

1. **Concision** : Moins de code pour le même résultat
2. **Lisibilité** : Le code se lit comme une phrase en anglais
3. **Composabilité** : Facile d'enchaîner plusieurs opérations
4. **Type-safe** : Erreurs détectées à la compilation
5. **IntelliSense** : Auto-complétion dans Visual Studio
6. **Réutilisabilité** : Requêtes réutilisables

### 2.2 Comparaison avant/après LINQ

```csharp
// ==========================================
// SCÉNARIO : Trouver les noms des étudiants en informatique, 
// triés par moyenne décroissante, avec moyenne >= 80
// ==========================================

// SANS LINQ - 15+ lignes
List<Etudiant> etudiantsInfo = new List<Etudiant>();
foreach (var e in etudiants)
{
    if (e.Programme == "Informatique" && e.Moyenne >= 80)
    {
        etudiantsInfo.Add(e);
    }
}

// Tri manuel
etudiantsInfo.Sort((a, b) => b.Moyenne.CompareTo(a.Moyenne));

List<string> noms = new List<string>();
foreach (var e in etudiantsInfo)
{
    noms.Add(e.Nom);
}

// AVEC LINQ - 3 lignes (ou même 1 ligne!)
var nomsInfo = etudiants
    .Where(e => e.Programme == "Informatique" && e.Moyenne >= 80)
    .OrderByDescending(e => e.Moyenne)
    .Select(e => e.Nom);

// Affichage
foreach (var nom in nomsInfo)
{
    Console.WriteLine(nom);
}

// Affiche:
// Charlie
// Alice
```

---

## 3. Les bases de LINQ

### 3.1 Importer le namespace

```csharp
using System.Linq; // ⚠️ OBLIGATOIRE pour utiliser LINQ
using System.Collections.Generic;
```

### 3.2 Les collections et IEnumerable

LINQ fonctionne avec tout ce qui implémente `IEnumerable<T>` :

```csharp
// Listes
List<int> nombres = new List<int> { 1, 2, 3, 4, 5 };

// Tableaux
int[] tableauNombres = { 1, 2, 3, 4, 5 };

// Dictionnaires
Dictionary<string, int> ages = new Dictionary<string, int>
{
    { "Alice", 25 },
    { "Bob", 30 }
};

// Tous peuvent être interrogés avec LINQ !
var resultats1 = nombres.Where(n => n > 3);
var resultats2 = tableauNombres.Where(n => n > 3);
var resultats3 = ages.Where(kvp => kvp.Value > 25);
```

### 3.3 L'exécution différée (deferred execution)

**Concept important** : LINQ utilise l'**exécution différée** - la requête n'est exécutée que lorsqu'on **énumère** les résultats.

```csharp
List<int> nombres = new List<int> { 1, 2, 3, 4, 5 };

// ⚠️ La requête est DÉFINIE mais PAS EXÉCUTÉE
var nombresPairs = nombres.Where(n => n % 2 == 0);

Console.WriteLine("Requête définie");

// Ajout d'un nouvel élément APRÈS la définition de la requête
nombres.Add(6);

// ✅ La requête est EXÉCUTÉE maintenant (lors du foreach)
foreach (var n in nombresPairs)
{
    Console.WriteLine(n); // Affiche: 2, 4, 6 (6 est inclus!)
}
```

**Forcer l'exécution immédiate :**

```csharp
// Exécution différée (par défaut)
var requete = nombres.Where(n => n % 2 == 0); // IEnumerable<int>

// Exécution immédiate avec ToList()
var liste = nombres.Where(n => n % 2 == 0).ToList(); // List<int>

// Exécution immédiate avec ToArray()
var tableau = nombres.Where(n => n % 2 == 0).ToArray(); // int[]

// Exécution immédiate avec Count()
int compte = nombres.Where(n => n % 2 == 0).Count(); // int
```

---

## 4. Les deux syntaxes LINQ

LINQ offre **deux syntaxes** pour écrire les requêtes :

### 4.1 Syntaxe de méthode (method syntax)

Utilise des méthodes d'extension avec des expressions lambda.

```csharp
var resultat = etudiants
    .Where(e => e.Moyenne >= 75)
    .OrderBy(e => e.Nom)
    .Select(e => e.Nom);
```

**Avantages :**
- Plus concise
- Plus flexible (accès à toutes les méthodes LINQ)
- Préférée par la plupart des développeurs
- **Recommandée** pour ce cours

### 4.2 Syntaxe de requête (query syntax)

Ressemble au SQL, utilise des mots-clés comme `from`, `where`, `select`.

```csharp
var resultat = from e in etudiants
               where e.Moyenne >= 75
               orderby e.Nom
               select e.Nom;
```

**Avantages :**
- Familière pour ceux qui connaissent SQL
- Parfois plus lisible pour des requêtes complexes

### 4.3 Comparaison des deux syntaxes

```csharp
// ==========================================
// SCÉNARIO : Étudiants en informatique avec moyenne >= 80
// ==========================================

// SYNTAXE DE MÉTHODE (recommandée)
var resultat1 = etudiants
    .Where(e => e.Programme == "Informatique")
    .Where(e => e.Moyenne >= 80)
    .Select(e => e.Nom);

// SYNTAXE DE REQUÊTE
var resultat2 = from e in etudiants
                where e.Programme == "Informatique"
                where e.Moyenne >= 80
                select e.Nom;

// Les DEUX produisent le même résultat !
```

**💡 Dans ce cours**, nous utiliserons principalement la **syntaxe de méthode** car elle est :
- Plus courante dans l'industrie
- Plus flexible
- Mieux supportée par IntelliSense

---

## 5. Opérations de filtrage

### 5.1 Where - Filtrer selon une condition

`Where()` retourne tous les éléments qui satisfont une condition.

```csharp
// ==========================================
// MISE EN CONTEXTE : Gestion d'un magasin
// ==========================================

public class Produit
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public string Categorie { get; set; }
    public int Stock { get; set; }
    public bool EstEnPromotion { get; set; }
}

List<Produit> produits = new List<Produit>
{
    new Produit { Id = 1, Nom = "Laptop", Prix = 1200, Categorie = "Électronique", Stock = 5, EstEnPromotion = true },
    new Produit { Id = 2, Nom = "Souris", Prix = 25, Categorie = "Électronique", Stock = 50, EstEnPromotion = false },
    new Produit { Id = 3, Nom = "Clavier", Prix = 80, Categorie = "Électronique", Stock = 30, EstEnPromotion = true },
    new Produit { Id = 4, Nom = "Chaise", Prix = 150, Categorie = "Mobilier", Stock = 10, EstEnPromotion = false },
    new Produit { Id = 5, Nom = "Bureau", Prix = 400, Categorie = "Mobilier", Stock = 3, EstEnPromotion = true },
    new Produit { Id = 6, Nom = "Lampe", Prix = 45, Categorie = "Mobilier", Stock = 20, EstEnPromotion = false }
};

// ==========================================
// EXEMPLE 1 : Produits en promotion
// ==========================================
var produitsEnPromotion = produits.Where(p => p.EstEnPromotion);

Console.WriteLine("Produits en promotion:");
foreach (var p in produitsEnPromotion)
{
    Console.WriteLine($"- {p.Nom} ({p.Prix:C})");
}
// Affiche:
// - Laptop (1 200,00 $)
// - Clavier (80,00 $)
// - Bureau (400,00 $)

// ==========================================
// EXEMPLE 2 : Produits chers (> 100$)
// ==========================================
var produitsChe rs = produits.Where(p => p.Prix > 100);

// ==========================================
// EXEMPLE 3 : Électronique PAS en promotion
// ==========================================
var electroniqueNormale = produits.Where(p => 
    p.Categorie == "Électronique" && !p.EstEnPromotion
);

// ==========================================
// EXEMPLE 4 : Stock faible (< 10 unités)
// ==========================================
var stockFaible = produits.Where(p => p.Stock < 10);

Console.WriteLine("\n⚠️ ALERTE Stock faible:");
foreach (var p in stockFaible)
{
    Console.WriteLine($"- {p.Nom}: seulement {p.Stock} unités");
}
// Affiche:
// - Laptop: seulement 5 unités
// - Bureau: seulement 3 unités

// ==========================================
// EXEMPLE 5 : Conditions multiples
// ==========================================
var produitsSpeciaux = produits.Where(p => 
    p.Prix >= 50 && 
    p.Prix <= 200 && 
    p.Stock > 5 &&
    p.Categorie == "Électronique"
);
```

**💡 Astuce :** Vous pouvez chaîner plusieurs `Where()` :

```csharp
// Ces deux requêtes sont équivalentes:

// Version 1 : Une seule condition complexe
var resultat1 = produits.Where(p => p.Prix > 50 && p.Stock > 10);

// Version 2 : Deux Where() séparés, mais moins efficace que la première version
var resultat2 = produits
    .Where(p => p.Prix > 50)
    .Where(p => p.Stock > 10);
```

### 5.2 OfType - Filtrer par type

Utile quand vous avez une collection d'objets de types différents.

```csharp
// ==========================================
// MISE EN CONTEXTE : Gestion de formes géométriques
// ==========================================

public abstract class Forme
{
    public string Couleur { get; set; }
}

public class Cercle : Forme
{
    public double Rayon { get; set; }
}

public class Rectangle : Forme
{
    public double Longueur { get; set; }
    public double Largeur { get; set; }
}

public class Triangle : Forme
{
    public double Base { get; set; }
    public double Hauteur { get; set; }
}

// Collection mixte
List<Forme> formes = new List<Forme>
{
    new Cercle { Couleur = "Rouge", Rayon = 5 },
    new Rectangle { Couleur = "Bleu", Longueur = 10, Largeur = 5 },
    new Cercle { Couleur = "Vert", Rayon = 3 },
    new Triangle { Couleur = "Jaune", Base = 4, Hauteur = 6 },
    new Rectangle { Couleur = "Orange", Longueur = 8, Largeur = 4 }
};

// ==========================================
// Obtenir seulement les cercles
// ==========================================
var cercles = formes.OfType<Cercle>();

Console.WriteLine("Cercles:");
foreach (var c in cercles)
{
    Console.WriteLine($"- Cercle {c.Couleur}, rayon = {c.Rayon}");
}
// Affiche:
// - Cercle Rouge, rayon = 5
// - Cercle Vert, rayon = 3

// ==========================================
// Obtenir seulement les rectangles
// ==========================================
var rectangles = formes.OfType<Rectangle>();

// ==========================================
// Calcul d'aire pour tous les cercles
// ==========================================
var airesDesCercles = formes
    .OfType<Cercle>()
    .Select(c => Math.PI * c.Rayon * c.Rayon);
```

---

## 6. Opérations de projection

La projection transforme les éléments d'une collection en un autre format.

### 6.1 Select - Transformer chaque élément

```csharp
// ==========================================
// MISE EN CONTEXTE : Système de gestion d'employés
// ==========================================

public class Employe
{
    public int Id { get; set; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public string Departement { get; set; }
    public decimal Salaire { get; set; }
    public DateTime DateEmbauche { get; set; }
}

List<Employe> employes = new List<Employe>
{
    new Employe { Id = 1, Prenom = "Alice", Nom = "Tremblay", Departement = "IT", Salaire = 75000, DateEmbauche = new DateTime(2020, 5, 15) },
    new Employe { Id = 2, Prenom = "Bob", Nom = "Gagnon", Departement = "RH", Salaire = 65000, DateEmbauche = new DateTime(2019, 3, 10) },
    new Employe { Id = 3, Prenom = "Charlie", Nom = "Roy", Departement = "IT", Salaire = 85000, DateEmbauche = new DateTime(2018, 1, 20) },
    new Employe { Id = 4, Prenom = "Diana", Nom = "Martin", Departement = "Ventes", Salaire = 70000, DateEmbauche = new DateTime(2021, 7, 5) }
};

// ==========================================
// EXEMPLE 1 : Extraire seulement les noms complets
// ==========================================
var nomsComplets = employes.Select(e => $"{e.Prenom} {e.Nom}");

Console.WriteLine("Liste des employés:");
foreach (var nom in nomsComplets)
{
    Console.WriteLine($"- {nom}");
}
// Affiche:
// - Alice Tremblay
// - Bob Gagnon
// - Charlie Roy
// - Diana Martin

// ==========================================
// EXEMPLE 2 : Extraire seulement les salaires
// ==========================================
var salaires = employes.Select(e => e.Salaire);

// ==========================================
// EXEMPLE 3 : Augmenter tous les salaires de 10%
// ==========================================
var salairesAugmentes = employes.Select(e => e.Salaire * 1.10m);

Console.WriteLine("\nSalaires après augmentation de 10%:");
foreach (var salaire in salairesAugmentes)
{
    Console.WriteLine($"{salaire:C}");
}

// ==========================================
// EXEMPLE 4 : Projection vers un type anonyme
// ==========================================
var resumeEmployes = employes.Select(e => new
{
    NomComplet = $"{e.Prenom} {e.Nom}",
    Dept = e.Departement,
    SalaireAnnuel = e.Salaire,
    SalaireMensuel = e.Salaire / 12,
    Anciennete = DateTime.Now.Year - e.DateEmbauche.Year
});

Console.WriteLine("\nRésumé des employés:");
foreach (var e in resumeEmployes)
{
    Console.WriteLine($"{e.NomComplet} ({e.Dept}) - {e.SalaireAnnuel:C}/an - {e.Anciennete} ans d'ancienneté");
}

// ==========================================
// EXEMPLE 5 : Projection vers une nouvelle classe
// ==========================================
public class EmployeDTO  // DTO = Data Transfer Object
{
    public string NomComplet { get; set; }
    public string Departement { get; set; }
    public string InformationSalaire { get; set; }
}

var employesDTO = employes.Select(e => new EmployeDTO
{
    NomComplet = $"{e.Prenom} {e.Nom}",
    Departement = e.Departement,
    InformationSalaire = $"{e.Salaire:C} par année"
});
```

### 6.2 SelectMany - Aplatir des collections imbriquées

Utilisé pour "aplatir" des collections de collections en une seule collection.

```csharp
// ==========================================
// MISE EN CONTEXTE : Université avec étudiants et cours
// ==========================================

public class Cours
{
    public string Code { get; set; }
    public string Nom { get; set; }
    public int Credits { get; set; }
}

public class EtudiantAvecCours
{
    public string Nom { get; set; }
    public List<Cours> CoursInscrits { get; set; }
}

List<EtudiantAvecCours> etudiants = new List<EtudiantAvecCours>
{
    new EtudiantAvecCours
    {
        Nom = "Alice",
        CoursInscrits = new List<Cours>
        {
            new Cours { Code = "INF101", Nom = "Programmation I", Credits = 3 },
            new Cours { Code = "MAT201", Nom = "Calcul I", Credits = 4 }
        }
    },
    new EtudiantAvecCours
    {
        Nom = "Bob",
        CoursInscrits = new List<Cours>
        {
            new Cours { Code = "INF101", Nom = "Programmation I", Credits = 3 },
            new Cours { Code = "INF202", Nom = "Structures de données", Credits = 3 }
        }
    },
    new EtudiantAvecCours
    {
        Nom = "Charlie",
        CoursInscrits = new List<Cours>
        {
            new Cours { Code = "MAT201", Nom = "Calcul I", Credits = 4 },
            new Cours { Code = "PHY101", Nom = "Physique I", Credits = 4 }
        }
    }
};

// ==========================================
// PROBLÈME : Obtenir TOUS les cours (de tous les étudiants)
// ==========================================

// ❌ MAUVAISE approche avec Select (ne fonctionne pas comme voulu)
var tentative = etudiants.Select(e => e.CoursInscrits);
// Retourne : IEnumerable<List<Cours>> - Une liste de listes !

// ✅ BONNE approche avec SelectMany
var tousLesCours = etudiants.SelectMany(e => e.CoursInscrits);
// Retourne : IEnumerable<Cours> - Une liste plate !

Console.WriteLine("Tous les cours (avec doublons):");
foreach (var cours in tousLesCours)
{
    Console.WriteLine($"- {cours.Code}: {cours.Nom}");
}
// Affiche:
// - INF101: Programmation I
// - MAT201: Calcul I
// - INF101: Programmation I (doublon)
// - INF202: Structures de données
// - MAT201: Calcul I (doublon)
// - PHY101: Physique I

// ==========================================
// Obtenir les cours uniques
// ==========================================
var coursUniques = etudiants
    .SelectMany(e => e.CoursInscrits)
    .Distinct() // Élimine les doublons
    .Select(c => c.Code);

Console.WriteLine("\nCours uniques:");
foreach (var code in coursUniques)
{
    Console.WriteLine($"- {code}");
}

// ==========================================
// Compter le nombre total de crédits de tous les étudiants
// ==========================================
int totalCredits = etudiants
    .SelectMany(e => e.CoursInscrits)
    .Sum(c => c.Credits);

Console.WriteLine($"\nTotal de crédits inscrits: {totalCredits}");
```

**Visualisation Select vs SelectMany :**

```
Données:
Étudiant 1 → [Cours A, Cours B]
Étudiant 2 → [Cours C, Cours D]
Étudiant 3 → [Cours E]

Select() :
[[Cours A, Cours B], [Cours C, Cours D], [Cours E]]
↑ Liste de listes

SelectMany() :
[Cours A, Cours B, Cours C, Cours D, Cours E]
↑ Liste plate
```

---

## 7. Opérations de tri

### 7.1 OrderBy et OrderByDescending

```csharp
// ==========================================
// MISE EN CONTEXTE : Classement d'athlètes
// ==========================================

public class Athlete
{
    public string Nom { get; set; }
    public string Pays { get; set; }
    public int Age { get; set; }
    public double TempsCourse { get; set; } // En secondes
    public int MedaillesOr { get; set; }
}

List<Athlete> athletes = new List<Athlete>
{
    new Athlete { Nom = "Usain Bolt", Pays = "Jamaïque", Age = 36, TempsCourse = 9.58, MedaillesOr = 8 },
    new Athlete { Nom = "Carl Lewis", Pays = "USA", Age = 61, TempsCourse = 9.86, MedaillesOr = 9 },
    new Athlete { Nom = "Andre De Grasse", Pays = "Canada", Age = 28, TempsCourse = 9.89, MedaillesOr = 1 },
    new Athlete { Nom = "Yohan Blake", Pays = "Jamaïque", Age = 33, TempsCourse = 9.69, MedaillesOr = 1 },
    new Athlete { Nom = "Justin Gatlin", Pays = "USA", Age = 41, TempsCourse = 9.74, MedaillesOr = 1 }
};

// ==========================================
// EXEMPLE 1 : Trier par temps (du plus rapide au plus lent)
// ==========================================
var classementParTemps = athletes.OrderBy(a => a.TempsCourse);

Console.WriteLine("Classement par temps de course:");
int position = 1;
foreach (var athlete in classementParTemps)
{
    Console.WriteLine($"{position}. {athlete.Nom} - {athlete.TempsCourse}s");
    position++;
}
// Affiche:
// 1. Usain Bolt - 9.58s
// 2. Yohan Blake - 9.69s
// 3. Justin Gatlin - 9.74s
// 4. Carl Lewis - 9.86s
// 5. Andre De Grasse - 9.89s

// ==========================================
// EXEMPLE 2 : Trier par médailles (du plus au moins)
// ==========================================
var classementParMedailles = athletes.OrderByDescending(a => a.MedaillesOr);

Console.WriteLine("\nClassement par médailles d'or:");
foreach (var athlete in classementParMedailles)
{
    Console.WriteLine($"{athlete.Nom}: {athlete.MedaillesOr} médaille(s)");
}

// ==========================================
// EXEMPLE 3 : Trier par nom (ordre alphabétique)
// ==========================================
var classementAlphabetique = athletes.OrderBy(a => a.Nom);

// ==========================================
// EXEMPLE 4 : Trier par âge (du plus jeune au plus vieux)
// ==========================================
var classementParAge = athletes.OrderBy(a => a.Age);
```

### 7.2 ThenBy et ThenByDescending - Tris secondaires

Utilisés pour trier selon plusieurs critères.

```csharp
// ==========================================
// EXEMPLE : Trier par pays, puis par temps
// ==========================================
var classementPaysEtTemps = athletes
    .OrderBy(a => a.Pays)           // D'abord par pays
    .ThenBy(a => a.TempsCourse);    // Ensuite par temps

Console.WriteLine("Classement par pays puis temps:");
foreach (var athlete in classementPaysEtTemps)
{
    Console.WriteLine($"{athlete.Pays} - {athlete.Nom} ({athlete.TempsCourse}s)");
}
// Affiche:
// Canada - Andre De Grasse (9.89s)
// Jamaïque - Usain Bolt (9.58s)
// Jamaïque - Yohan Blake (9.69s)
// USA - Justin Gatlin (9.74s)
// USA - Carl Lewis (9.86s)

// ==========================================
// EXEMPLE : Trier par médailles (desc), puis par temps (asc)
// ==========================================
var classementComplet = athletes
    .OrderByDescending(a => a.MedaillesOr)  // Plus de médailles en premier
    .ThenBy(a => a.TempsCourse);            // Si égalité, plus rapide en premier

Console.WriteLine("\nClassement final:");
foreach (var athlete in classementComplet)
{
    Console.WriteLine($"{athlete.Nom}: {athlete.MedaillesOr} médaille(s), {athlete.TempsCourse}s");
}
```

### 7.3 Reverse - Inverser l'ordre

```csharp
// Liste de nombres
List<int> nombres = new List<int> { 1, 2, 3, 4, 5 };

// Inverser l'ordre
var nombresInverses = nombres.AsEnumerable().Reverse();

foreach (var n in nombresInverses)
{
    Console.Write($"{n} "); // Affiche: 5 4 3 2 1
}
```

---

## 8. Opérations d'Agrégation

Les opérations d'agrégation calculent une **valeur unique** à partir d'une collection.

### 8.1 Count - Compter les éléments

```csharp
// ==========================================
// MISE EN CONTEXTE : Système de gestion de bibliothèque
// ==========================================

public class Livre
{
    public string Titre { get; set; }
    public string Auteur { get; set; }
    public int AnneePublication { get; set; }
    public string Genre { get; set; }
    public int NombrePages { get; set; }
    public bool EstDisponible { get; set; }
    public double Note { get; set; } // Sur 5
}

List<Livre> bibliotheque = new List<Livre>
{
    new Livre { Titre = "1984", Auteur = "George Orwell", AnneePublication = 1949, Genre = "Science-Fiction", NombrePages = 328, EstDisponible = true, Note = 4.7 },
    new Livre { Titre = "Le Petit Prince", Auteur = "Antoine de Saint-Exupéry", AnneePublication = 1943, Genre = "Fiction", NombrePages = 96, EstDisponible = false, Note = 4.8 },
    new Livre { Titre = "Harry Potter à l'école des sorciers", Auteur = "J.K. Rowling", AnneePublication = 1997, Genre = "Fantasy", NombrePages = 309, EstDisponible = true, Note = 4.9 },
    new Livre { Titre = "Le Seigneur des Anneaux", Auteur = "J.R.R. Tolkien", AnneePublication = 1954, Genre = "Fantasy", NombrePages = 1178, EstDisponible = true, Note = 4.9 },
    new Livre { Titre = "Dune", Auteur = "Frank Herbert", AnneePublication = 1965, Genre = "Science-Fiction", NombrePages = 688, EstDisponible = false, Note = 4.6 },
    new Livre { Titre = "Les Misérables", Auteur = "Victor Hugo", AnneePublication = 1862, Genre = "Classique", NombrePages = 1463, EstDisponible = true, Note = 4.5 }
};

// ==========================================
// EXEMPLE 1 : Compter tous les livres
// ==========================================
int totalLivres = bibliotheque.Count();
Console.WriteLine($"Total de livres: {totalLivres}"); // 6

// ==========================================
// EXEMPLE 2 : Compter les livres disponibles
// ==========================================
int livresDisponibles = bibliotheque.Count(l => l.EstDisponible);
Console.WriteLine($"Livres disponibles: {livresDisponibles}"); // 4

// ==========================================
// EXEMPLE 3 : Compter par genre
// ==========================================
int livresSciFi = bibliotheque.Count(l => l.Genre == "Science-Fiction");
int livresFantasy = bibliotheque.Count(l => l.Genre == "Fantasy");

Console.WriteLine($"Science-Fiction: {livresSciFi}");  // 2
Console.WriteLine($"Fantasy: {livresFantasy}");        // 2

// ==========================================
// EXEMPLE 4 : Compter les livres récents (après 1990)
// ==========================================
int livresRecents = bibliotheque.Count(l => l.AnneePublication >= 1990);
Console.WriteLine($"Livres publiés après 1990: {livresRecents}"); // 1
```

### 8.2 Sum - Calculer la somme

```csharp
// ==========================================
// EXEMPLE 1 : Nombre total de pages dans la bibliothèque
// ==========================================
int totalPages = bibliotheque.Sum(l => l.NombrePages);
Console.WriteLine($"Total de pages: {totalPages:N0}"); // 4 062

// ==========================================
// EXEMPLE 2 : Pages des livres disponibles
// ==========================================
int pagesDisponibles = bibliotheque
    .Where(l => l.EstDisponible)
    .Sum(l => l.NombrePages);

Console.WriteLine($"Pages disponibles: {pagesDisponibles:N0}"); // 3 178

// ==========================================
// EXEMPLE 3 : Somme simple (liste de nombres)
// ==========================================
List<int> ventes = new List<int> { 100, 250, 175, 320, 95 };
int totalVentes = ventes.Sum();
Console.WriteLine($"Total des ventes: {totalVentes}"); // 940
```

### 8.3 Average - Calculer la moyenne

```csharp
// ==========================================
// EXEMPLE 1 : Note moyenne de tous les livres
// ==========================================
double noteMoyenne = bibliotheque.Average(l => l.Note);
Console.WriteLine($"Note moyenne: {noteMoyenne:F2}/5"); // 4.73/5

// ==========================================
// EXEMPLE 2 : Nombre moyen de pages
// ==========================================
double moyennePages = bibliotheque.Average(l => l.NombrePages);
Console.WriteLine($"Moyenne de pages: {moyennePages:F0}"); // 677

// ==========================================
// EXEMPLE 3 : Note moyenne des livres Fantasy
// ==========================================
double noteMoyenneFantasy = bibliotheque
    .Where(l => l.Genre == "Fantasy")
    .Average(l => l.Note);

Console.WriteLine($"Note moyenne Fantasy: {noteMoyenneFantasy:F2}/5"); // 4.90/5
```

### 8.4 Min et Max - Trouver minimum et maximum

```csharp
// ==========================================
// EXEMPLE 1 : Plus vieux et plus récent livre
// ==========================================
int anneeMin = bibliotheque.Min(l => l.AnneePublication);
int anneeMax = bibliotheque.Max(l => l.AnneePublication);

Console.WriteLine($"Plus vieux livre: {anneeMin}");   // 1862
Console.WriteLine($"Plus récent livre: {anneeMax}");  // 1997

// ==========================================
// EXEMPLE 2 : Livre le plus court et le plus long
// ==========================================
int pagesMin = bibliotheque.Min(l => l.NombrePages);
int pagesMax = bibliotheque.Max(l => l.NombrePages);

Console.WriteLine($"Livre le plus court: {pagesMin} pages");  // 96
Console.WriteLine($"Livre le plus long: {pagesMax} pages");   // 1463

// ==========================================
// EXEMPLE 3 : Meilleure et pire note
// ==========================================
double meilleureNote = bibliotheque.Max(l => l.Note);
double pireNote = bibliotheque.Min(l => l.Note);

Console.WriteLine($"Meilleure note: {meilleureNote}/5");  // 4.9/5
Console.WriteLine($"Pire note: {pireNote}/5");            // 4.5/5

// ==========================================
// EXEMPLE 4 : Obtenir LE livre avec la meilleure note (pas juste la note)
// ==========================================
var meilleurLivre = bibliotheque.OrderByDescending(l => l.Note).First();
Console.WriteLine($"Meilleur livre: {meilleurLivre.Titre} ({meilleurLivre.Note}/5)");
// Harry Potter à l'école des sorciers (4.9/5)
```

### 8.5 Aggregate - Agrégation personnalisée

Pour des calculs plus complexes.

```csharp
// ==========================================
// EXEMPLE 1 : Concaténer tous les titres
// ==========================================
string tousTitres = bibliotheque
    .Select(l => l.Titre)
    .Aggregate((titre1, titre2) => titre1 + ", " + titre2);

Console.WriteLine($"Tous les titres: {tousTitres}");

// ==========================================
// EXEMPLE 2 : Calculer un produit (multiplication)
// ==========================================
List<int> nombres = new List<int> { 2, 3, 4, 5 };
int produit = nombres.Aggregate((a, b) => a * b);
Console.WriteLine($"Produit: {produit}"); // 2 × 3 × 4 × 5 = 120

// ==========================================
// EXEMPLE 3 : Calculer un total avec valeur initiale
// ==========================================
int sommeAvecInitial = nombres.Aggregate(100, (total, nombre) => total + nombre);
Console.WriteLine($"Somme avec initial: {sommeAvecInitial}"); // 100 + 2 + 3 + 4 + 5 = 114
```

---

## 9. Opérations de Regroupement

### 9.1 GroupBy - Regrouper par clé

```csharp
// ==========================================
// MISE EN CONTEXTE : Analyse des ventes d'un magasin
// ==========================================

public class Vente
{
    public int Id { get; set; }
    public string Produit { get; set; }
    public string Categorie { get; set; }
    public decimal Montant { get; set; }
    public DateTime Date { get; set; }
    public string Vendeur { get; set; }
}

List<Vente> ventes = new List<Vente>
{
    new Vente { Id = 1, Produit = "Laptop", Categorie = "Électronique", Montant = 1200, Date = new DateTime(2024, 1, 15), Vendeur = "Alice" },
    new Vente { Id = 2, Produit = "Souris", Categorie = "Électronique", Montant = 25, Date = new DateTime(2024, 1, 16), Vendeur = "Bob" },
    new Vente { Id = 3, Produit = "Bureau", Categorie = "Mobilier", Montant = 400, Date = new DateTime(2024, 1, 17), Vendeur = "Alice" },
    new Vente { Id = 4, Produit = "Chaise", Categorie = "Mobilier", Montant = 150, Date = new DateTime(2024, 1, 18), Vendeur = "Charlie" },
    new Vente { Id = 5, Produit = "Clavier", Categorie = "Électronique", Montant = 80, Date = new DateTime(2024, 1, 19), Vendeur = "Bob" },
    new Vente { Id = 6, Produit = "Lampe", Categorie = "Mobilier", Montant = 45, Date = new DateTime(2024, 1, 20), Vendeur = "Alice" },
    new Vente { Id = 7, Produit = "Écran", Categorie = "Électronique", Montant = 350, Date = new DateTime(2024, 1, 21), Vendeur = "Charlie" }
};

// ==========================================
// EXEMPLE 1 : Regrouper par catégorie
// ==========================================
var ventesParCategorie = ventes.GroupBy(v => v.Categorie);

Console.WriteLine("Ventes par catégorie:");
foreach (var groupe in ventesParCategorie)
{
    Console.WriteLine($"\n{groupe.Key}:"); // Key = la catégorie
    foreach (var vente in groupe)
    {
        Console.WriteLine($"  - {vente.Produit}: {vente.Montant:C}");
    }
}
// Affiche:
// Électronique:
//   - Laptop: 1 200,00 $
//   - Souris: 25,00 $
//   - Clavier: 80,00 $
//   - Écran: 350,00 $
// Mobilier:
//   - Bureau: 400,00 $
//   - Chaise: 150,00 $
//   - Lampe: 45,00 $

// ==========================================
// EXEMPLE 2 : Calculer le total par catégorie
// ==========================================
var totauxParCategorie = ventes
    .GroupBy(v => v.Categorie)
    .Select(groupe => new
    {
        Categorie = groupe.Key,
        NombreVentes = groupe.Count(),
        MontantTotal = groupe.Sum(v => v.Montant),
        MontantMoyen = groupe.Average(v => v.Montant)
    });

Console.WriteLine("\n📊 Statistiques par catégorie:");
foreach (var stat in totauxParCategorie)
{
    Console.WriteLine($"{stat.Categorie}:");
    Console.WriteLine($"  Nombre de ventes: {stat.NombreVentes}");
    Console.WriteLine($"  Total: {stat.MontantTotal:C}");
    Console.WriteLine($"  Moyenne: {stat.MontantMoyen:C}");
}
// Affiche:
// Électronique:
//   Nombre de ventes: 4
//   Total: 1 655,00 $
//   Moyenne: 413,75 $
// Mobilier:
//   Nombre de ventes: 3
//   Total: 595,00 $
//   Moyenne: 198,33 $

// ==========================================
// EXEMPLE 3 : Regrouper par vendeur
// ==========================================
var ventesParVendeur = ventes
    .GroupBy(v => v.Vendeur)
    .Select(groupe => new
    {
        Vendeur = groupe.Key,
        NombreVentes = groupe.Count(),
        TotalVentes = groupe.Sum(v => v.Montant),
        MeilleureVente = groupe.Max(v => v.Montant)
    })
    .OrderByDescending(x => x.TotalVentes);

Console.WriteLine("\n🏆 Performance des vendeurs:");
foreach (var vendeur in ventesParVendeur)
{
    Console.WriteLine($"{vendeur.Vendeur}:");
    Console.WriteLine($"  {vendeur.NombreVentes} vente(s)");
    Console.WriteLine($"  Total: {vendeur.TotalVentes:C}");
    Console.WriteLine($"  Meilleure vente: {vendeur.MeilleureVente:C}");
}

// ==========================================
// EXEMPLE 4 : Regrouper par mois
// ==========================================
var ventesParMois = ventes
    .GroupBy(v => v.Date.Month)
    .Select(groupe => new
    {
        Mois = groupe.Key,
        Total = groupe.Sum(v => v.Montant)
    });
```

---

## 10. Opérations de Jointure

Les jointures combinent des données de deux collections basées sur une clé commune.

### 10.1 Join - Jointure interne

```csharp
// ==========================================
// MISE EN CONTEXTE : Système de commandes en ligne
// ==========================================

public class Client
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string Ville { get; set; }
}

public class Commande
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime DateCommande { get; set; }
    public decimal Montant { get; set; }
    public string Statut { get; set; }
}

List<Client> clients = new List<Client>
{
    new Client { Id = 1, Nom = "Alice Tremblay", Email = "alice@email.com", Ville = "Montréal" },
    new Client { Id = 2, Nom = "Bob Gagnon", Email = "bob@email.com", Ville = "Québec" },
    new Client { Id = 3, Nom = "Charlie Roy", Email = "charlie@email.com", Ville = "Montréal" },
    new Client { Id = 4, Nom = "Diana Martin", Email = "diana@email.com", Ville = "Laval" }
};

List<Commande> commandes = new List<Commande>
{
    new Commande { Id = 101, ClientId = 1, DateCommande = new DateTime(2024, 1, 15), Montant = 150.00m, Statut = "Livrée" },
    new Commande { Id = 102, ClientId = 1, DateCommande = new DateTime(2024, 1, 20), Montant = 75.50m, Statut = "En cours" },
    new Commande { Id = 103, ClientId = 2, DateCommande = new DateTime(2024, 1, 18), Montant = 200.00m, Statut = "Livrée" },
    new Commande { Id = 104, ClientId = 3, DateCommande = new DateTime(2024, 1, 22), Montant = 95.00m, Statut = "Livrée" },
    new Commande { Id = 105, ClientId = 1, DateCommande = new DateTime(2024, 1, 25), Montant = 120.00m, Statut = "En cours" }
    // Note: Aucune commande pour Diana (Id = 4)
};

// ==========================================
// EXEMPLE 1 : Joindre clients et commandes
// ==========================================
var commandesAvecClient = commandes.Join(
    clients,                          // Collection à joindre
    commande => commande.ClientId,    // Clé de la première collection
    client => client.Id,              // Clé de la deuxième collection
    (commande, client) => new         // Résultat de la jointure
    {
        NumeroCommande = commande.Id,
        NomClient = client.Nom,
        VilleClient = client.Ville,
        Montant = commande.Montant,
        Date = commande.DateCommande,
        Statut = commande.Statut
    }
);

Console.WriteLine("📦 Commandes avec informations clients:");
foreach (var cmd in commandesAvecClient)
{
    Console.WriteLine($"Commande #{cmd.NumeroCommande} - {cmd.NomClient} ({cmd.VilleClient}) - {cmd.Montant:C} - {cmd.Statut}");
}
// Affiche:
// Commande #101 - Alice Tremblay (Montréal) - 150,00 $ - Livrée
// Commande #102 - Alice Tremblay (Montréal) - 75,50 $ - En cours
// Commande #103 - Bob Gagnon (Québec) - 200,00 $ - Livrée
// Commande #104 - Charlie Roy (Montréal) - 95,00 $ - Livrée
// Commande #105 - Alice Tremblay (Montréal) - 120,00 $ - En cours

// ⚠️ Note: Diana n'apparaît pas (elle n'a aucune commande)

// ==========================================
// EXEMPLE 2 : Total des commandes par client
// ==========================================
var totauxParClient = commandes
    .Join(clients,
          cmd => cmd.ClientId,
          cli => cli.Id,
          (cmd, cli) => new { Client = cli.Nom, Montant = cmd.Montant })
    .GroupBy(x => x.Client)
    .Select(groupe => new
    {
        Client = groupe.Key,
        NombreCommandes = groupe.Count(),
        TotalAchats = groupe.Sum(x => x.Montant)
    })
    .OrderByDescending(x => x.TotalAchats);

Console.WriteLine("\n💰 Total des achats par client:");
foreach (var total in totauxParClient)
{
    Console.WriteLine($"{total.Client}: {total.NombreCommandes} commande(s) - Total: {total.TotalAchats:C}");
}
// Affiche:
// Alice Tremblay: 3 commande(s) - Total: 345,50 $
// Bob Gagnon: 1 commande(s) - Total: 200,00 $
// Charlie Roy: 1 commande(s) - Total: 95,00 $
```

### 10.2 GroupJoin - Jointure de groupe

Similaire à un LEFT JOIN en SQL - inclut tous les éléments de la première collection même s'ils n'ont pas de correspondance.

```csharp
// ==========================================
// EXEMPLE : Tous les clients avec leurs commandes (même ceux sans commande)
// ==========================================
var clientsAvecCommandes = clients.GroupJoin(
    commandes,
    client => client.Id,
    commande => commande.ClientId,
    (client, commandesClient) => new
    {
        NomClient = client.Nom,
        Ville = client.Ville,
        NombreCommandes = commandesClient.Count(),
        TotalAchats = commandesClient.Sum(c => (decimal?)c.Montant) ?? 0,
        Commandes = commandesClient.ToList()
    }
);

Console.WriteLine("\n👥 Tous les clients:");
foreach (var client in clientsAvecCommandes)
{
    Console.WriteLine($"\n{client.NomClient} ({client.Ville}):");
    Console.WriteLine($"  Nombre de commandes: {client.NombreCommandes}");
    Console.WriteLine($"  Total des achats: {client.TotalAchats:C}");
    
    if (client.Commandes.Any())
    {
        Console.WriteLine("  Commandes:");
        foreach (var cmd in client.Commandes)
        {
            Console.WriteLine($"    - #{cmd.Id}: {cmd.Montant:C} ({cmd.Statut})");
        }
    }
    else
    {
        Console.WriteLine("  ⚠️ Aucune commande");
    }
}
// Affiche maintenant TOUS les clients, y compris Diana qui n'a aucune commande!
```

---

## 11. Opérations d'Ensemble

### 11.1 Distinct - Éliminer les doublons

```csharp
// ==========================================
// MISE EN CONTEXTE : Tags d'articles de blog
// ==========================================

List<string> tagsArticle1 = new List<string> { "C#", "LINQ", "Programming", "Tutorial" };
List<string> tagsArticle2 = new List<string> { "C#", "ASP.NET", "Web", "Programming" };
List<string> tagsArticle3 = new List<string> { "Python", "Programming", "Data Science" };

// Combiner tous les tags
List<string> tousTags = new List<string>();
tousTags.AddRange(tagsArticle1);
tousTags.AddRange(tagsArticle2);
tousTags.AddRange(tagsArticle3);

Console.WriteLine("Tous les tags (avec doublons):");
Console.WriteLine(string.Join(", ", tousTags));
// C#, LINQ, Programming, Tutorial, C#, ASP.NET, Web, Programming, Python, Programming, Data Science

// Obtenir les tags uniques
var tagsUniques = tousTags.Distinct();

Console.WriteLine("\nTags uniques:");
Console.WriteLine(string.Join(", ", tagsUniques));
// C#, LINQ, Programming, Tutorial, ASP.NET, Web, Python, Data Science

// ==========================================
// Distinct avec objets personnalisés
// ==========================================
public class Ville
{
    public string Nom { get; set; }
    public string Province { get; set; }
}

List<Ville> villes = new List<Ville>
{
    new Ville { Nom = "Montréal", Province = "Québec" },
    new Ville { Nom = "Québec", Province = "Québec" },
    new Ville { Nom = "Montréal", Province = "Québec" }, // Doublon!
    new Ville { Nom = "Toronto", Province = "Ontario" }
};

// Pour Distinct sur objets, il faut implémenter IEquatable ou utiliser DistinctBy
var villesUniques = villes.DistinctBy(v => v.Nom);

Console.WriteLine("\nVilles uniques:");
foreach (var ville in villesUniques)
{
    Console.WriteLine($"{ville.Nom}, {ville.Province}");
}
```

### 11.2 Union - Union de deux collections

Combine deux collections et élimine les doublons.

```csharp
List<int> liste1 = new List<int> { 1, 2, 3, 4, 5 };
List<int> liste2 = new List<int> { 4, 5, 6, 7, 8 };

var union = liste1.Union(liste2);

Console.WriteLine("Union:");
Console.WriteLine(string.Join(", ", union)); // 1, 2, 3, 4, 5, 6, 7, 8
```

### 11.3 Intersect - Intersection

Retourne uniquement les éléments présents dans LES DEUX collections.

```csharp
var intersection = liste1.Intersect(liste2);

Console.WriteLine("Intersection:");
Console.WriteLine(string.Join(", ", intersection)); // 4, 5
```

### 11.4 Except - Différence

Retourne les éléments de la première collection qui ne sont PAS dans la deuxième.

```csharp
var difference = liste1.Except(liste2);

Console.WriteLine("Différence (liste1 - liste2):");
Console.WriteLine(string.Join(", ", difference)); // 1, 2, 3
```

### 11.5 Exemple Pratique : Gestion d'abonnements

```csharp
// ==========================================
// MISE EN CONTEXTE : Abonnés à des newsletters
// ==========================================

List<string> abonnesNewsletter = new List<string>
{
    "alice@email.com",
    "bob@email.com",
    "charlie@email.com",
    "diana@email.com"
};

List<string> abonnesBlog = new List<string>
{
    "bob@email.com",
    "charlie@email.com",
    "eve@email.com",
    "frank@email.com"
};

// Qui est abonné aux DEUX?
var abonnesDeuxServices = abonnesNewsletter.Intersect(abonnesBlog);
Console.WriteLine("📧 Abonnés aux deux services:");
foreach (var email in abonnesDeuxServices)
{
    Console.WriteLine($"  - {email}");
}
// bob@email.com
// charlie@email.com

// Qui est abonné à la newsletter mais PAS au blog?
var uniquementNewsletter = abonnesNewsletter.Except(abonnesBlog);
Console.WriteLine("\n📰 Uniquement à la newsletter:");
foreach (var email in uniquementNewsletter)
{
    Console.WriteLine($"  - {email}");
}
// alice@email.com
// diana@email.com

// Tous les abonnés (peu importe le service)
var tousLesAbonnes = abonnesNewsletter.Union(abonnesBlog);
Console.WriteLine($"\n👥 Total d'abonnés uniques: {tousLesAbonnes.Count()}");
// 6
```

---

## 12. Opérations de Quantification

Ces opérations retournent un **booléen** (true/false).

### 12.1 Any - Au moins un élément satisfait la condition

```csharp
// ==========================================
// MISE EN CONTEXTE : Validation de stock
// ==========================================

List<Produit> produits = new List<Produit>
{
    new Produit { Nom = "Laptop", Stock = 5 },
    new Produit { Nom = "Souris", Stock = 50 },
    new Produit { Nom = "Clavier", Stock = 0 },  // Rupture de stock!
    new Produit { Nom = "Écran", Stock = 10 }
};

// Y a-t-il au moins un produit en rupture de stock?
bool ruptureStock = produits.Any(p => p.Stock == 0);
Console.WriteLine($"Rupture de stock: {ruptureStock}"); // True

// Y a-t-il au moins un produit avec beaucoup de stock?
bool stockAbondant = produits.Any(p => p.Stock > 30);
Console.WriteLine($"Stock abondant: {stockAbondant}"); // True

// Y a-t-il des produits?
bool existeProduits = produits.Any();
Console.WriteLine($"Existe des produits: {existeProduits}"); // True

// ==========================================
// EXEMPLE PRATIQUE : Validation de formulaire
// ==========================================
public class Formulaire
{
    public string Nom { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
}

bool FormulaireEstValide(Formulaire form)
{
    // Au moins un champ doit être vide pour que le formulaire soit invalide
    bool champVide = new[] { form.Nom, form.Email, form.Telephone }
        .Any(champ => string.IsNullOrWhiteSpace(champ));
    
    return !champVide;
}
```

### 12.2 All - Tous les éléments satisfont la condition

```csharp
// Tous les produits ont-ils un stock positif?
bool tousEnStock = produits.All(p => p.Stock > 0);
Console.WriteLine($"Tous en stock: {tousEnStock}"); // False (à cause du Clavier)

// Tous les produits ont-ils un stock >= 0?
bool stockValide = produits.All(p => p.Stock >= 0);
Console.WriteLine($"Stock valide: {stockValide}"); // True

// ==========================================
// EXEMPLE : Validation d'âge pour un groupe
// ==========================================
List<Etudiant> groupe = new List<Etudiant>
{
    new Etudiant { Nom = "Alice", Age = 20 },
    new Etudiant { Nom = "Bob", Age = 22 },
    new Etudiant { Nom = "Charlie", Age = 21 }
};

// Tous les étudiants sont-ils majeurs?
bool tousMajeurs = groupe.All(e => e.Age >= 18);
Console.WriteLine($"Tous majeurs: {tousMajeurs}"); // True

// Tous les étudiants ont-ils plus de 21 ans?
bool tousPlus21 = groupe.All(e => e.Age > 21);
Console.WriteLine($"Tous plus de 21 ans: {tousPlus21}"); // False
```

### 12.3 Contains - Vérifie si un élément existe

```csharp
List<string> fruits = new List<string> { "Pomme", "Banane", "Orange", "Kiwi" };

bool aPomme = fruits.Contains("Pomme");
Console.WriteLine($"Contient Pomme: {aPomme}"); // True

bool aRaisin = fruits.Contains("Raisin");
Console.WriteLine($"Contient Raisin: {aRaisin}"); // False

// ==========================================
// EXEMPLE : Vérifier si un utilisateur existe
// ==========================================
List<string> utilisateursAutorises = new List<string>
{
    "admin@site.com",
    "moderateur@site.com",
    "editeur@site.com"
};

string emailUtilisateur = "admin@site.com";
bool estAutorise = utilisateursAutorises.Contains(emailUtilisateur);

if (estAutorise)
{
    Console.WriteLine("✅ Accès autorisé");
}
else
{
    Console.WriteLine("❌ Accès refusé");
}
```

---

## 13. Opérations de Partition

### 13.1 Take - Prendre les N premiers éléments

```csharp
// ==========================================
// MISE EN CONTEXTE : Affichage de résultats paginés
// ==========================================

List<string> resultatsRecherche = new List<string>
{
    "Résultat 1", "Résultat 2", "Résultat 3", "Résultat 4", "Résultat 5",
    "Résultat 6", "Résultat 7", "Résultat 8", "Résultat 9", "Résultat 10"
};

// Afficher les 3 premiers résultats
var top3 = resultatsRecherche.Take(3);

Console.WriteLine("Top 3 résultats:");
foreach (var resultat in top3)
{
    Console.WriteLine($"  - {resultat}");
}
// Résultat 1
// Résultat 2
// Résultat 3

// ==========================================
// EXEMPLE : Top 5 des meilleurs étudiants
// ==========================================
var top5Etudiants = etudiants
    .OrderByDescending(e => e.Moyenne)
    .Take(5)
    .Select(e => new { e.Nom, e.Moyenne });

Console.WriteLine("\n🏆 Top 5 des étudiants:");
int rang = 1;
foreach (var etudiant in top5Etudiants)
{
    Console.WriteLine($"{rang}. {etudiant.Nom}: {etudiant.Moyenne}");
    rang++;
}
```

### 13.2 Skip - Ignorer les N premiers éléments

```csharp
// Ignorer les 3 premiers résultats
var apres3 = resultatsRecherche.Skip(3);

Console.WriteLine("Après les 3 premiers:");
foreach (var resultat in apres3)
{
    Console.WriteLine($"  - {resultat}");
}
// Résultat 4
// Résultat 5
// ...
// Résultat 10
```

### 13.3 Pagination avec Take et Skip

```csharp
// ==========================================
// EXEMPLE PRATIQUE : Pagination
// ==========================================

int pageSize = 3;  // 3 résultats par page
int pageNumber = 2; // Page 2

var resultatsPagines = resultatsRecherche
    .Skip((pageNumber - 1) * pageSize)  // Ignorer les pages précédentes
    .Take(pageSize);                     // Prendre uniquement cette page

Console.WriteLine($"Page {pageNumber}:");
foreach (var resultat in resultatsPagines)
{
    Console.WriteLine($"  - {resultat}");
}
// Page 2:
//   - Résultat 4
//   - Résultat 5
//   - Résultat 6

// ==========================================
// Fonction de pagination réutilisable
// ==========================================
public static IEnumerable<T> ObtenirPage<T>(IEnumerable<T> source, int numeroPage, int taillePage)
{
    return source
        .Skip((numeroPage - 1) * taillePage)
        .Take(taillePage);
}

// Utilisation
var page1 = ObtenirPage(resultatsRecherche, 1, 3);
var page2 = ObtenirPage(resultatsRecherche, 2, 3);
var page3 = ObtenirPage(resultatsRecherche, 3, 3);
```

### 13.4 TakeWhile et SkipWhile - Selon une condition

```csharp
List<int> nombres = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Prendre tant que la condition est vraie
var inferieursA6 = nombres.TakeWhile(n => n < 6);
Console.WriteLine("TakeWhile (< 6):");
Console.WriteLine(string.Join(", ", inferieursA6)); // 1, 2, 3, 4, 5

// Ignorer tant que la condition est vraie
var depuis6 = nombres.SkipWhile(n => n < 6);
Console.WriteLine("SkipWhile (< 6):");
Console.WriteLine(string.Join(", ", depuis6)); // 6, 7, 8, 9, 10
```

---

## 14. LINQ avec Objets Complexes

### 14.1 Exemple Complet : Système Universitaire

```csharp
public class Universite
{
    public string Nom { get; set; }
    public List<Departement> Departements { get; set; }
}

public class Departement
{
    public string Nom { get; set; }
    public List<Professeur> Professeurs { get; set; }
    public List<Cours> Cours { get; set; }
}

public class Professeur
{
    public string Nom { get; set; }
    public string Specialite { get; set; }
    public int AnneesExperience { get; set; }
}

public class Cours
{
    public string Code { get; set; }
    public string Titre { get; set; }
    public int Credits { get; set; }
    public string ProfesseurNom { get; set; }
    public List<Etudiant> EtudiantsInscrits { get; set; }
}

// Données
var universite = new Universite
{
    Nom = "Université du Québec",
    Departements = new List<Departement>
    {
        new Departement
        {
            Nom = "Informatique",
            Professeurs = new List<Professeur>
            {
                new Professeur { Nom = "Dr. Smith", Specialite = "Intelligence Artificielle", AnneesExperience = 15 },
                new Professeur { Nom = "Dr. Johnson", Specialite = "Bases de données", AnneesExperience = 10 }
            },
            Cours = new List<Cours>
            {
                new Cours
                {
                    Code = "INF101",
                    Titre = "Programmation I",
                    Credits = 3,
                    ProfesseurNom = "Dr. Smith",
                    EtudiantsInscrits = new List<Etudiant>
                    {
                        new Etudiant { Nom = "Alice", Moyenne = 85 },
                        new Etudiant { Nom = "Bob", Moyenne = 78 }
                    }
                },
                new Cours
                {
                    Code = "INF202",
                    Titre = "Structures de données",
                    Credits = 4,
                    ProfesseurNom = "Dr. Johnson",
                    EtudiantsInscrits = new List<Etudiant>
                    {
                        new Etudiant { Nom = "Charlie", Moyenne = 90 },
                        new Etudiant { Nom = "Diana", Moyenne = 82 }
                    }
                }
            }
        },
        new Departement
        {
            Nom = "Mathématiques",
            Professeurs = new List<Professeur>
            {
                new Professeur { Nom = "Dr. Williams", Specialite = "Algèbre", AnneesExperience = 20 }
            },
            Cours = new List<Cours>
            {
                new Cours
                {
                    Code = "MAT101",
                    Titre = "Calcul I",
                    Credits = 4,
                    ProfesseurNom = "Dr. Williams",
                    EtudiantsInscrits = new List<Etudiant>
                    {
                        new Etudiant { Nom = "Eve", Moyenne = 88 }
                    }
                }
            }
        }
    }
};

// ==========================================
// REQUÊTE 1 : Tous les professeurs de l'université
// ==========================================
var tousProfesseurs = universite.Departements
    .SelectMany(d => d.Professeurs)
    .Select(p => new { p.Nom, p.Specialite, p.AnneesExperience });

Console.WriteLine("👨‍🏫 Tous les professeurs:");
foreach (var prof in tousProfesseurs)
{
    Console.WriteLine($"  - {prof.Nom} ({prof.Specialite}) - {prof.AnneesExperience} ans");
}

// ==========================================
// REQUÊTE 2 : Tous les cours avec plus de 3 crédits
// ==========================================
var coursAvances = universite.Departements
    .SelectMany(d => d.Cours)
    .Where(c => c.Credits > 3)
    .Select(c => new { c.Code, c.Titre, c.Credits });

Console.WriteLine("\n📚 Cours avancés (>3 crédits):");
foreach (var cours in coursAvances)
{
    Console.WriteLine($"  - {cours.Code}: {cours.Titre} ({cours.Credits} crédits)");
}

// ==========================================
// REQUÊTE 3 : Nombre total d'étudiants dans l'université
// ==========================================
int totalEtudiants = universite.Departements
    .SelectMany(d => d.Cours)
    .SelectMany(c => c.EtudiantsInscrits)
    .Distinct() // Au cas où un étudiant est dans plusieurs cours
    .Count();

Console.WriteLine($"\n👥 Total d'étudiants: {totalEtudiants}");

// ==========================================
// REQUÊTE 4 : Moyenne générale de l'université
// ==========================================
double moyenneGenerale = universite.Departements
    .SelectMany(d => d.Cours)
    .SelectMany(c => c.EtudiantsInscrits)
    .Average(e => e.Moyenne);

Console.WriteLine($"📊 Moyenne générale: {moyenneGenerale:F2}");

// ==========================================
// REQUÊTE 5 : Statistiques par département
// ==========================================
var statsParDepartement = universite.Departements
    .Select(d => new
    {
        Departement = d.Nom,
        NombreProfesseurs = d.Professeurs.Count,
        NombreCours = d.Cours.Count,
        TotalEtudiants = d.Cours.SelectMany(c => c.EtudiantsInscrits).Count(),
        MoyenneDepartement = d.Cours.SelectMany(c => c.EtudiantsInscrits).Average(e => e.Moyenne)
    });

Console.WriteLine("\n📈 Statistiques par département:");
foreach (var stat in statsParDepartement)
{
    Console.WriteLine($"{stat.Departement}:");
    Console.WriteLine($"  Professeurs: {stat.NombreProfesseurs}");
    Console.WriteLine($"  Cours: {stat.NombreCours}");
    Console.WriteLine($"  Étudiants: {stat.TotalEtudiants}");
    Console.WriteLine($"  Moyenne: {stat.MoyenneDepartement:F2}");
}

// ==========================================
// REQUÊTE 6 : Cours les plus populaires
// ==========================================
var coursPopulaires = universite.Departements
    .SelectMany(d => d.Cours)
    .OrderByDescending(c => c.EtudiantsInscrits.Count)
    .Take(3)
    .Select(c => new
    {
        c.Code,
        c.Titre,
        NombreEtudiants = c.EtudiantsInscrits.Count
    });

Console.WriteLine("\n🔥 Cours les plus populaires:");
foreach (var cours in coursPopulaires)
{
    Console.WriteLine($"  - {cours.Code}: {cours.Titre} ({cours.NombreEtudiants} étudiants)");
}
```

---

## 15. Cas Pratiques Réels

### 15.1 Analyse de Logs d'Application

```csharp
public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string Level { get; set; } // INFO, WARNING, ERROR
    public string Message { get; set; }
    public string Module { get; set; }
}

List<LogEntry> logs = new List<LogEntry>
{
    new LogEntry { Timestamp = DateTime.Now.AddHours(-5), Level = "INFO", Message = "Application started", Module = "Startup" },
    new LogEntry { Timestamp = DateTime.Now.AddHours(-4), Level = "ERROR", Message = "Database connection failed", Module = "Database" },
    new LogEntry { Timestamp = DateTime.Now.AddHours(-3), Level = "WARNING", Message = "High memory usage", Module = "Performance" },
    new LogEntry { Timestamp = DateTime.Now.AddHours(-2), Level = "ERROR", Message = "Null reference exception", Module = "UserService" },
    new LogEntry { Timestamp = DateTime.Now.AddHours(-1), Level = "INFO", Message = "User logged in", Module = "Auth" },
    new LogEntry { Timestamp = DateTime.Now, Level = "ERROR", Message = "Payment processing failed", Module = "Payment" }
};

// ==========================================
// Rapport d'erreurs
// ==========================================
var erreurs = logs
    .Where(log => log.Level == "ERROR")
    .OrderByDescending(log => log.Timestamp)
    .Select(log => new
    {
        Heure = log.Timestamp.ToString("HH:mm"),
        log.Module,
        log.Message
    });

Console.WriteLine("🔴 Erreurs récentes:");
foreach (var erreur in erreurs)
{
    Console.WriteLine($"[{erreur.Heure}] {erreur.Module}: {erreur.Message}");
}

// ==========================================
// Statistiques par module
// ==========================================
var statsParModule = logs
    .GroupBy(log => log.Module)
    .Select(groupe => new
    {
        Module = groupe.Key,
        Total = groupe.Count(),
        Erreurs = groupe.Count(log => log.Level == "ERROR"),
        Warnings = groupe.Count(log => log.Level == "WARNING")
    })
    .OrderByDescending(stat => stat.Erreurs);

Console.WriteLine("\n📊 Statistiques par module:");
foreach (var stat in statsParModule)
{
    Console.WriteLine($"{stat.Module}: {stat.Total} logs (dont {stat.Erreurs} erreurs)");
}

// ==========================================
// Alertes critiques (plusieurs erreurs dans un module)
// ==========================================
var modulesCritiques = logs
    .Where(log => log.Level == "ERROR")
    .GroupBy(log => log.Module)
    .Where(groupe => groupe.Count() >= 2)
    .Select(groupe => groupe.Key);

if (modulesCritiques.Any())
{
    Console.WriteLine("\n⚠️ ALERTE: Modules avec erreurs multiples:");
    foreach (var module in modulesCritiques)
    {
        Console.WriteLine($"  - {module}");
    }
}
```

### 15.2 Analyse de Données de Ventes

```csharp
public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Produit { get; set; }
    public decimal Prix { get; set; }
    public int Quantite { get; set; }
    public string Categorie { get; set; }
    public string Region { get; set; }
}

List<Transaction> transactions = new List<Transaction>
{
    new Transaction { Id = 1, Date = new DateTime(2024, 1, 15), Produit = "Laptop", Prix = 1200, Quantite = 2, Categorie = "Électronique", Region = "Québec" },
    new Transaction { Id = 2, Date = new DateTime(2024, 1, 16), Produit = "Souris", Prix = 25, Quantite = 10, Categorie = "Électronique", Region = "Montréal" },
    new Transaction { Id = 3, Date = new DateTime(2024, 1, 17), Produit = "Bureau", Prix = 400, Quantite = 1, Categorie = "Mobilier", Region = "Québec" },
    new Transaction { Id = 4, Date = new DateTime(2024, 1, 18), Produit = "Chaise", Prix = 150, Quantite = 4, Categorie = "Mobilier", Region = "Laval" },
    new Transaction { Id = 5, Date = new DateTime(2024, 1, 19), Produit = "Clavier", Prix = 80, Quantite = 5, Categorie = "Électronique", Region = "Montréal" }
};

// ==========================================
// Chiffre d'affaires total
// ==========================================
decimal chiffreAffaires = transactions.Sum(t => t.Prix * t.Quantite);
Console.WriteLine($"💰 Chiffre d'affaires total: {chiffreAffaires:C}");

// ==========================================
// Top 3 des produits les plus vendus
// ==========================================
var top3Produits = transactions
    .GroupBy(t => t.Produit)
    .Select(groupe => new
    {
        Produit = groupe.Key,
        QuantiteTotale = groupe.Sum(t => t.Quantite),
        Revenu = groupe.Sum(t => t.Prix * t.Quantite)
    })
    .OrderByDescending(x => x.Revenu)
    .Take(3);

Console.WriteLine("\n🏆 Top 3 produits par revenu:");
foreach (var produit in top3Produits)
{
    Console.WriteLine($"  {produit.Produit}: {produit.QuantiteTotale} unités - {produit.Revenu:C}");
}

// ==========================================
// Performance par région
// ==========================================
var performanceRegions = transactions
    .GroupBy(t => t.Region)
    .Select(groupe => new
    {
        Region = groupe.Key,
        NombreTransactions = groupe.Count(),
        RevenuTotal = groupe.Sum(t => t.Prix * t.Quantite),
        PanierMoyen = groupe.Average(t => t.Prix * t.Quantite)
    })
    .OrderByDescending(x => x.RevenuTotal);

Console.WriteLine("\n📍 Performance par région:");
foreach (var region in performanceRegions)
{
    Console.WriteLine($"{region.Region}:");
    Console.WriteLine($"  Transactions: {region.NombreTransactions}");
    Console.WriteLine($"  Revenu: {region.RevenuTotal:C}");
    Console.WriteLine($"  Panier moyen: {region.PanierMoyen:C}");
}

// ==========================================
// Tendance quotidienne
// ==========================================
var ventesParJour = transactions
    .GroupBy(t => t.Date.Date)
    .Select(groupe => new
    {
        Date = groupe.Key.ToString("dd/MM/yyyy"),
        Revenu = groupe.Sum(t => t.Prix * t.Quantite)
    })
    .OrderBy(x => x.Date);

Console.WriteLine("\n📈 Ventes quotidiennes:");
foreach (var jour in ventesParJour)
{
    Console.WriteLine($"  {jour.Date}: {jour.Revenu:C}");
}
```

---

## 16. Bonnes Pratiques et Pièges

### 16.1 Éviter l'Exécution Multiple

```csharp
// ❌ MAUVAIS - La requête est exécutée 3 fois!
var requete = produits.Where(p => p.Prix > 100);

int count = requete.Count();        // Exécution 1
decimal total = requete.Sum(p => p.Prix);  // Exécution 2
var liste = requete.ToList();       // Exécution 3

// ✅ BON - Exécuter une seule fois avec ToList()
var resultats = produits.Where(p => p.Prix > 100).ToList();

int count = resultats.Count;         // Pas d'exécution (déjà en mémoire)
decimal total = resultats.Sum(p => p.Prix);  // Pas d'exécution
```

### 16.2 Attention aux Null References

```csharp
// ❌ RISQUE - NullReferenceException si un produit est null
var produitsChers = produits.Where(p => p.Prix > 100);

// ✅ MIEUX - Filtrer les nulls d'abord
var produitsChers = produits
    .Where(p => p != null)
    .Where(p => p.Prix > 100);

// ✅ OU utiliser l'opérateur ?.
var prixMax = produits
    .Where(p => p != null)
    .Max(p => p.Prix);
```

### 16.3 Performance : Where avant Select

```csharp
// ❌ MOINS EFFICACE - Transforme tout puis filtre
var resultats = produits
    .Select(p => new { p.Nom, p.Prix })
    .Where(x => x.Prix > 100);

// ✅ PLUS EFFICACE - Filtre d'abord, puis transforme
var resultats = produits
    .Where(p => p.Prix > 100)
    .Select(p => new { p.Nom, p.Prix });
```

### 16.4 Utiliser Any() plutôt que Count()

```csharp
// ❌ MOINS EFFICACE - Compte tous les éléments
if (produits.Count() > 0)
{
    // ...
}

// ✅ PLUS EFFICACE - S'arrête dès qu'un élément est trouvé
if (produits.Any())
{
    // ...
}

// ❌ MOINS EFFICACE
if (produits.Where(p => p.Prix > 1000).Count() > 0)
{
    // ...
}

// ✅ PLUS EFFICACE
if (produits.Any(p => p.Prix > 1000))
{
    // ...
}
```

### 16.5 Éviter les Requêtes Complexes dans les Boucles

```csharp
// ❌ TRÈS MAUVAIS - Requête LINQ dans une boucle!
foreach (var categorie in categories)
{
    var count = produits.Count(p => p.Categorie == categorie);
    Console.WriteLine($"{categorie}: {count}");
}

// ✅ BON - Une seule requête avec GroupBy
var comptesParCategorie = produits
    .GroupBy(p => p.Categorie)
    .Select(g => new { Categorie = g.Key, Count = g.Count() });

foreach (var groupe in comptesParCategorie)
{
    Console.WriteLine($"{groupe.Categorie}: {groupe.Count}");
}
```

### 16.6 Préférer FirstOrDefault() à First()

```csharp
// ❌ RISQUE - Lève une exception si aucun élément trouvé
var produit = produits.First(p => p.Id == 999);

// ✅ SÉCURITAIRE - Retourne null si aucun élément trouvé
var produit = produits.FirstOrDefault(p => p.Id == 999);

if (produit != null)
{
    Console.WriteLine(produit.Nom);
}
else
{
    Console.WriteLine("Produit introuvable");
}
```

### 16.7 Ordre des Opérations

```csharp
// L'ordre compte pour la performance!

// ✅ BON - Filtre puis trie (trie moins d'éléments)
var resultats = produits
    .Where(p => p.Prix > 100)     // Réduit la collection
    .OrderBy(p => p.Nom);         // Trie une petite collection

// ❌ MOINS BON - Trie puis filtre (trie inutilement tout)
var resultats = produits
    .OrderBy(p => p.Nom)          // Trie TOUT
    .Where(p => p.Prix > 100);    // Puis filtre
```

---

## 📚 Résumé des Opérations LINQ

| Catégorie | Opérations | Description |
|-----------|-----------|-------------|
| **Filtrage** | `Where`, `OfType` | Sélectionner des éléments selon une condition |
| **Projection** | `Select`, `SelectMany` | Transformer les éléments |
| **Tri** | `OrderBy`, `OrderByDescending`, `ThenBy`, `Reverse` | Trier les éléments |
| **Agrégation** | `Count`, `Sum`, `Average`, `Min`, `Max`, `Aggregate` | Calculer des valeurs |
| **Regroupement** | `GroupBy` | Regrouper par clé |
| **Jointure** | `Join`, `GroupJoin` | Combiner deux collections |
| **Ensemble** | `Distinct`, `Union`, `Intersect`, `Except` | Opérations d'ensemble |
| **Quantification** | `Any`, `All`, `Contains` | Tests booléens |
| **Partition** | `Take`, `Skip`, `TakeWhile`, `SkipWhile` | Pagination et découpage |
| **Élément** | `First`, `FirstOrDefault`, `Last`, `Single` | Obtenir un élément |
| **Conversion** | `ToList`, `ToArray`, `ToDictionary` | Convertir en collection |

---

## 🎯 Checklist de Maîtrise LINQ

- [ ] Je comprends la différence entre exécution différée et immédiate
- [ ] Je sais utiliser `Where()` pour filtrer
- [ ] Je sais utiliser `Select()` pour projeter/transformer
- [ ] Je maîtrise `OrderBy()` et `ThenBy()` pour trier
- [ ] Je sais utiliser `GroupBy()` pour regrouper
- [ ] Je comprends la différence entre `First()` et `FirstOrDefault()`
- [ ] Je sais utiliser `Any()` et `All()` pour tester
- [ ] Je maîtrise `Sum()`, `Average()`, `Min()`, `Max()`
- [ ] Je sais utiliser `Take()` et `Skip()` pour paginer
- [ ] Je comprends `Join()` et `GroupJoin()`
- [ ] Je sais éviter les pièges de performance
- [ ] Je peux écrire des requêtes LINQ complexes

---

## 🔗 Ressources Supplémentaires

- Documentation Microsoft LINQ : https://docs.microsoft.com/fr-fr/dotnet/csharp/linq/
- LINQ samples : https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b