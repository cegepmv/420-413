---
title: "Les méthodes en c#"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 7
---

## Table des matières
1. [Introduction](#introduction)
2. [Syntaxe de base](#syntaxe-de-base)
3. [Paramètres de méthodes](#paramètres-de-méthodes)
4. [Paramètres par référence (ref)](#paramètres-par-référence-ref)
5. [Paramètres de sortie (out)](#paramètres-de-sortie-out)
6. [Paramètres optionnels](#paramètres-optionnels)
7. [Surcharge de méthodes](#surcharge-de-méthodes)
8. [Portée des variables](#portée-des-variables)
9. [Exercices](#exercices)

---

## Introduction

Les **méthodes** (aussi appelées **fonctions**) sont des blocs de code réutilisables qui effectuent une tâche spécifique. Elles permettent de :
- Organiser le code de manière logique
- Éviter la répétition de code
- Faciliter la maintenance et la lecture du programme
- Diviser un problème complexe en sous-problèmes plus simples

### Avantages des méthodes
- **Réutilisabilité** : Écrire une fois, utiliser plusieurs fois
- **Lisibilité** : Code plus clair et organisé
- **Maintenance** : Plus facile de corriger ou modifier le code
- **Tests** : Plus facile de tester des petites unités de code

---

## Syntaxe de base

### Structure d'une méthode

```csharp
modificateurAcces [static] typeRetour NomMethode(typeParam1 param1, typeParam2 param2)
{
    // Corps de la méthode
    // Instructions à exécuter
    return valeur; // Si la méthode retourne une valeur
}
```

**Composantes :**
- **modificateurAcces** : Contrôle la visibilité de la méthode (`public`, `private`, `protected`, `internal`)
- **static** : Indique que la méthode appartient à la classe, pas à une instance (obligatoire pour l'instant)
- **typeRetour** : Le type de données que la méthode retourne (`int`, `string`, `double`, `void`, etc.)
- **NomMethode** : Le nom de la méthode (convention : commence par une majuscule en C#)
- **Paramètres** : Les données d'entrée (optionnels)
- **Corps** : Le code qui s'exécute quand la méthode est appelée
- **return** : Retourne une valeur (sauf si `void`)

### Les modificateurs d'accès

| Modificateur | Description | Utilisation |
|--------------|-------------|-------------|
| `public` | Accessible partout | Pour les méthodes utilisées par d'autres classes |
| `private` | Accessible seulement dans la classe actuelle | Pour les méthodes internes (par défaut) |
| `protected` | Accessible dans la classe et ses classes dérivées | Pour l'héritage (POO) |
| `internal` | Accessible dans le même assembly | Pour les méthodes partagées dans un projet |

**Note :** Pour l'instant, nous utilisons principalement `static` avec nos méthodes car nous ne travaillons pas encore avec la programmation orientée objet. Les méthodes `static` peuvent être appelées directement sans créer d'objet.

### Exemples de déclarations de méthodes

```csharp
// Méthode publique statique qui retourne un entier
public static int CalculerSomme(int a, int b)
{
    return a + b;
}

// Méthode privée statique qui ne retourne rien (void)
private static void AfficherMessage()
{
    Console.WriteLine("Message privé");
}

// Méthode publique statique avec plusieurs paramètres
public static double CalculerMoyenne(double note1, double note2, double note3)
{
    return (note1 + note2 + note3) / 3;
}

// Méthode publique statique sans paramètres
public static void AfficherBienvenue()
{
    Console.WriteLine("Bienvenue!");
}
```

### Exemple simple : Méthode sans paramètres

```csharp
using System;

class Programme
{
    public static void AfficherMessage()
    {
        Console.WriteLine("Bonjour du CÉGEP!");
        Console.WriteLine("Bienvenue dans le cours de C#");
    }
    
    static void Main()
    {
        AfficherMessage(); // Appel de la méthode
        AfficherMessage(); // On peut l'appeler plusieurs fois
    }
}
```

**Sortie :**
```
Bonjour du CÉGEP!
Bienvenue dans le cours de C#
Bonjour du CÉGEP!
Bienvenue dans le cours de C#
```

### Exemple : Méthode avec retour

```csharp
using System;

class Programme
{
    public static int ObtenirAnneeActuelle()
    {
        return 2024;
    }
    
    static void Main()
    {
        int annee = ObtenirAnneeActuelle();
        Console.WriteLine($"Nous sommes en {annee}");
    }
}
```

---

## Paramètres de méthodes

Les paramètres permettent de passer des données à une méthode.

### Passage par valeur (comportement par défaut)

Quand vous passez une variable à une méthode, C# crée une **copie** de la valeur. Les modifications dans la méthode n'affectent pas la variable originale.

```csharp
using System;

class Programme
{
    public static void AugmenterNombre(int nombre)
    {
        nombre = nombre + 10;
        Console.WriteLine($"Dans la méthode: {nombre}");
    }
    
    static void Main()
    {
        int valeur = 5;
        Console.WriteLine($"Avant l'appel: {valeur}");
        
        AugmenterNombre(valeur);
        
        Console.WriteLine($"Après l'appel: {valeur}");
    }
}
```

**Sortie :**
```
Avant l'appel: 5
Dans la méthode: 15
Après l'appel: 5
```

**Explication :** La variable `valeur` n'a pas changé car la méthode a travaillé avec une copie.

### Exemple : Calculer l'aire d'un rectangle

```csharp
using System;

class Programme
{
    public static double CalculerAireRectangle(double longueur, double largeur)
    {
        double aire = longueur * largeur;
        return aire;
    }
    
    static void Main()
    {
        double resultat = CalculerAireRectangle(5.5, 3.2);
        Console.WriteLine($"L'aire du rectangle est: {resultat:F2} m²");
        
        // Appel avec d'autres valeurs
        resultat = CalculerAireRectangle(10.0, 7.5);
        Console.WriteLine($"L'aire du rectangle est: {resultat:F2} m²");
    }
}
```

### Exemple : Méthode avec plusieurs paramètres

```csharp
using System;

class Programme
{
    public static void AfficherInfoEtudiant(string nom, string prenom, int age, double moyenne)
    {
        Console.WriteLine("=== FICHE ÉTUDIANT ===");
        Console.WriteLine($"Nom: {nom}");
        Console.WriteLine($"Prénom: {prenom}");
        Console.WriteLine($"Âge: {age} ans");
        Console.WriteLine($"Moyenne: {moyenne:F1}%");
        Console.WriteLine("=====================");
    }
    
    static void Main()
    {
        AfficherInfoEtudiant("Tremblay", "Alice", 19, 87.5);
        AfficherInfoEtudiant("Lavoie", "Bernard", 20, 82.3);
    }
}
```

---

## Paramètres par référence (ref)

Le mot-clé **`ref`** permet de passer une variable **par référence** plutôt que par valeur. Cela signifie que la méthode travaille directement avec la variable originale, pas avec une copie.

### Caractéristiques de `ref`
- La variable **DOIT** être initialisée avant l'appel
- Les modifications dans la méthode affectent la variable originale
- Le mot-clé `ref` doit apparaître à la **définition** ET à l'**appel** de la méthode

### Syntaxe

```csharp
public static void NomMethode(ref int parametre)
{
    parametre = parametre * 2;
}

// Appel
int nombre = 10;
NomMethode(ref nombre); // Utiliser 'ref' lors de l'appel
```

### Exemple : Échanger deux valeurs

```csharp
using System;

class Programme
{
    public static void EchangerValeurs(ref int a, ref int b)
    {
        int temporaire = a;
        a = b;
        b = temporaire;
        
        Console.WriteLine($"Dans la méthode - a: {a}, b: {b}");
    }
    
    static void Main()
    {
        int x = 10;
        int y = 20;
        
        Console.WriteLine($"Avant l'échange - x: {x}, y: {y}");
        
        EchangerValeurs(ref x, ref y);
        
        Console.WriteLine($"Après l'échange - x: {x}, y: {y}");
    }
}
```

**Sortie :**
```
Avant l'échange - x: 10, y: 20
Dans la méthode - a: 20, b: 10
Après l'échange - x: 20, y: 10
```

### Exemple : Doubler une valeur

```csharp
using System;

class Programme
{
    public static void DoublerValeur(ref int nombre)
    {
        nombre = nombre * 2;
    }
    
    static void Main()
    {
        int valeur = 15;
        Console.WriteLine($"Valeur initiale: {valeur}");
        
        DoublerValeur(ref valeur);
        
        Console.WriteLine($"Valeur après doublement: {valeur}");
    }
}
```

**Sortie :**
```
Valeur initiale: 15
Valeur après doublement: 30
```

### Exemple : Modifier plusieurs valeurs

```csharp
using System;

class Programme
{
    public static void AppliquerTaxes(ref double prix, double tauxTPS, double tauxTVQ)
    {
        double tps = prix * tauxTPS;
        double tvq = prix * tauxTVQ;
        prix = prix + tps + tvq;
    }
    
    static void Main()
    {
        double prixProduit = 100.00;
        Console.WriteLine($"Prix avant taxes: {prixProduit:C}");
        
        AppliquerTaxes(ref prixProduit, 0.05, 0.09975);
        
        Console.WriteLine($"Prix après taxes: {prixProduit:C}");
    }
}
```

---

## Paramètres de sortie (out)

Le mot-clé **`out`** est similaire à `ref`, mais avec des différences importantes :

### Différences entre `ref` et `out`

| Caractéristique | `ref` | `out` |
|----------------|-------|-------|
| Initialisation avant appel | **REQUISE** | Pas nécessaire |
| Assignation dans la méthode | Optionnelle | **OBLIGATOIRE** |
| Utilisation principale | Modifier une valeur existante | Retourner plusieurs valeurs |

### Caractéristiques de `out`
- La variable **N'A PAS BESOIN** d'être initialisée avant l'appel
- La méthode **DOIT** assigner une valeur au paramètre `out`
- Utilisé principalement pour retourner plusieurs valeurs d'une méthode
- Le mot-clé `out` doit apparaître à la **définition** ET à l'**appel**

### Syntaxe

```csharp
public static void CalculerStatistiques(int[] nombres, out double moyenne, out int maximum)
{
    // La méthode DOIT assigner des valeurs à moyenne et maximum
    moyenne = ...;
    maximum = ...;
}

// Appel
double moy;
int max;
CalculerStatistiques(tableau, out moy, out max);
```

### Exemple : Retourner plusieurs valeurs

```csharp
using System;

class Programme
{
    public static void CalculerRectangle(double longueur, double largeur, 
                                  out double aire, out double perimetre)
    {
        aire = longueur * largeur;
        perimetre = 2 * (longueur + largeur);
    }
    
    static void Main()
    {
        double surfaceResultat;
        double perimetreResultat;
        
        CalculerRectangle(5.0, 3.0, out surfaceResultat, out perimetreResultat);
        
        Console.WriteLine($"Aire: {surfaceResultat} m²");
        Console.WriteLine($"Périmètre: {perimetreResultat} m");
    }
}
```

**Sortie :**
```
Aire: 15 m²
Périmètre: 16 m
```

### Déclaration inline (C# 7.0+)

Depuis C# 7.0, vous pouvez déclarer les variables `out` directement dans l'appel :

```csharp
using System;

class Programme
{
    public static void DiviserAvecReste(int dividende, int diviseur, 
                                 out int quotient, out int reste)
    {
        quotient = dividende / diviseur;
        reste = dividende % diviseur;
    }
    
    static void Main()
    {
        // Déclaration inline - plus concis!
        DiviserAvecReste(17, 5, out int q, out int r);
        
        Console.WriteLine($"17 ÷ 5 = {q} reste {r}");
    }
}
```

### Exemple : Statistiques d'un tableau

```csharp
using System;

class Programme
{
    public static void CalculerStatistiques(int[] nombres, out double moyenne, 
                                     out int minimum, out int maximum)
    {
        // Calcul de la somme et moyenne
        int somme = 0;
        foreach (int nombre in nombres)
        {
            somme += nombre;
        }
        moyenne = (double)somme / nombres.Length;
        
        // Trouver min et max
        minimum = nombres[0];
        maximum = nombres[0];
        
        foreach (int nombre in nombres)
        {
            if (nombre < minimum)
                minimum = nombre;
            if (nombre > maximum)
                maximum = nombre;
        }
    }
    
    static void Main()
    {
        int[] notes = { 85, 92, 78, 95, 88, 76, 90 };
        
        CalculerStatistiques(notes, out double moy, out int min, out int max);
        
        Console.WriteLine("=== STATISTIQUES DES NOTES ===");
        Console.WriteLine($"Moyenne: {moy:F2}%");
        Console.WriteLine($"Note minimale: {min}%");
        Console.WriteLine($"Note maximale: {max}%");
    }
}
```

### Exemple : Validation d'entrée avec TryParse

La méthode `TryParse` utilise `out` pour retourner le résultat de la conversion :

```csharp
using System;

class Programme
{
    static void Main()
    {
        Console.Write("Entrez votre âge: ");
        string entree = Console.ReadLine();
        
        // TryParse retourne true si la conversion réussit
        // et met le résultat dans la variable 'age'
        if (int.TryParse(entree, out int age))
        {
            Console.WriteLine($"Votre âge est: {age} ans");
            
            if (age >= 18)
                Console.WriteLine("Vous êtes majeur");
            else
                Console.WriteLine("Vous êtes mineur");
        }
        else
        {
            Console.WriteLine("Entrée invalide!");
        }
    }
}
```

### Exemple : Analyse de chaîne

```csharp
using System;

class Programme
{
    public static bool ExtraireNomPrenom(string nomComplet, out string nom, out string prenom)
    {
        string[] parties = nomComplet.Split(' ');
        
        if (parties.Length >= 2)
        {
            prenom = parties[0];
            nom = parties[1];
            return true;
        }
        else
        {
            nom = "";
            prenom = "";
            return false;
        }
    }
    
    static void Main()
    {
        string nomComplet = "Alice Tremblay";
        
        if (ExtraireNomPrenom(nomComplet, out string n, out string p))
        {
            Console.WriteLine($"Prénom: {p}");
            Console.WriteLine($"Nom: {n}");
        }
        else
        {
            Console.WriteLine("Format invalide");
        }
    }
}
```

---

## Paramètres optionnels

Les **paramètres optionnels** ont des valeurs par défaut et peuvent être omis lors de l'appel.

### Règles
- Les paramètres optionnels doivent être à la **fin** de la liste de paramètres
- Vous devez fournir une valeur par défaut
- Si omis lors de l'appel, la valeur par défaut est utilisée

### Syntaxe

```csharp
public static void NomMethode(int obligatoire, int optionnel = 10)
{
    // Corps de la méthode
}

// Appels possibles
NomMethode(5);        // optionnel = 10 (valeur par défaut)
NomMethode(5, 20);    // optionnel = 20
```

### Exemple : Afficher un message personnalisé

```csharp
using System;

class Programme
{
    public static void AfficherBienvenue(string nom, string titre = "Étudiant")
    {
        Console.WriteLine($"Bonjour {titre} {nom}!");
    }
    
    static void Main()
    {
        AfficherBienvenue("Alice");              // Utilise "Étudiant" par défaut
        AfficherBienvenue("Bernard", "Professeur"); // Utilise "Professeur"
        AfficherBienvenue("Catherine", "Directrice");
    }
}
```

**Sortie :**
```
Bonjour Étudiant Alice!
Bonjour Professeur Bernard!
Bonjour Directrice Catherine!
```

### Exemple : Calcul avec taux par défaut

```csharp
using System;

class Programme
{
    public static double CalculerPrixTotal(double prixBase, double tauxTaxe = 0.15)
    {
        return prixBase * (1 + tauxTaxe);
    }
    
    static void Main()
    {
        // Utilise le taux par défaut (15%)
        double total1 = CalculerPrixTotal(100.00);
        Console.WriteLine($"Prix avec taxe par défaut: {total1:C}");
        
        // Spécifie un taux différent
        double total2 = CalculerPrixTotal(100.00, 0.20);
        Console.WriteLine($"Prix avec 20% de taxe: {total2:C}");
    }
}
```

### Exemple : Affichage formaté

```csharp
using System;

class Programme
{
    public static void AfficherLigne(string texte, char caractere = '-', int longueur = 40)
    {
        Console.WriteLine(texte);
        Console.WriteLine(new string(caractere, longueur));
    }
    
    static void Main()
    {
        AfficherLigne("Titre 1");                    // - et 40 par défaut
        AfficherLigne("Titre 2", '=');               // = et 40
        AfficherLigne("Titre 3", '*', 30);           // * et 30
    }
}
```

**Sortie :**
```
Titre 1
----------------------------------------
Titre 2
========================================
Titre 3
******************************
```

---

## Surcharge de méthodes

La **surcharge** (overloading) permet d'avoir plusieurs méthodes avec le **même nom** mais des **signatures différentes**.

### Qu'est-ce qu'une signature?
La signature d'une méthode inclut :
- Le nom de la méthode
- Le nombre de paramètres
- Le type des paramètres
- L'ordre des paramètres

**Note :** Le type de retour ne fait PAS partie de la signature.

### Exemple : Calculer l'aire de différentes formes

```csharp
using System;

class Programme
{
    // Aire d'un carré
    public static double CalculerAire(double cote)
    {
        return cote * cote;
    }
    
    // Aire d'un rectangle
    public static double CalculerAire(double longueur, double largeur)
    {
        return longueur * largeur;
    }
    
    // Aire d'un cercle
    public static double CalculerAire(double rayon, bool estCercle)
    {
        return Math.PI * rayon * rayon;
    }
    
    static void Main()
    {
        double aireCarre = CalculerAire(5.0);
        Console.WriteLine($"Aire du carré: {aireCarre:F2} m²");
        
        double aireRectangle = CalculerAire(5.0, 3.0);
        Console.WriteLine($"Aire du rectangle: {aireRectangle:F2} m²");
        
        double aireCercle = CalculerAire(4.0, true);
        Console.WriteLine($"Aire du cercle: {aireCercle:F2} m²");
    }
}
```

### Exemple : Afficher différents types

```csharp
using System;

class Programme
{
    public static void Afficher(int nombre)
    {
        Console.WriteLine($"Nombre entier: {nombre}");
    }
    
    public static void Afficher(double nombre)
    {
        Console.WriteLine($"Nombre décimal: {nombre:F2}");
    }
    
    public static void Afficher(string texte)
    {
        Console.WriteLine($"Texte: {texte}");
    }
    
    public static void Afficher(int[] tableau)
    {
        Console.Write("Tableau: ");
        foreach (int n in tableau)
        {
            Console.Write($"{n} ");
        }
        Console.WriteLine();
    }
    
    static void Main()
    {
        Afficher(42);
        Afficher(3.14159);
        Afficher("Bonjour!");
        Afficher(new int[] { 1, 2, 3, 4, 5 });
    }
}
```

### Exemple : Créer un message de bienvenue

```csharp
using System;

class Programme
{
    // Version simple
    public static string CreerBienvenue(string nom)
    {
        return $"Bienvenue {nom}!";
    }
    
    // Version avec prénom et nom
    public static string CreerBienvenue(string prenom, string nom)
    {
        return $"Bienvenue {prenom} {nom}!";
    }
    
    // Version avec titre
    public static string CreerBienvenue(string prenom, string nom, string titre)
    {
        return $"Bienvenue {titre} {prenom} {nom}!";
    }
    
    static void Main()
    {
        Console.WriteLine(CreerBienvenue("Alice"));
        Console.WriteLine(CreerBienvenue("Alice", "Tremblay"));
        Console.WriteLine(CreerBienvenue("Alice", "Tremblay", "Dr."));
    }
}
```

---

## Portée des variables

La **portée** (scope) d'une variable détermine où elle peut être utilisée dans le code.

### Variables locales

Les variables déclarées **à l'intérieur** d'une méthode sont **locales** à cette méthode.

```csharp
using System;

class Programme
{
    public static void Methode1()
    {
        int x = 10; // Variable locale à Methode1
        Console.WriteLine($"Dans Methode1: x = {x}");
    }
    
    public static void Methode2()
    {
        int x = 20; // Variable différente, locale à Methode2
        Console.WriteLine($"Dans Methode2: x = {x}");
    }
    
    static void Main()
    {
        Methode1();
        Methode2();
        // Console.WriteLine(x); // ERREUR: x n'existe pas ici
    }
}
```

### Portée de bloc

Les variables déclarées dans un bloc `{ }` ne sont visibles que dans ce bloc.

```csharp
using System;

class Programme
{
    static void Main()
    {
        int nombre = 10;
        
        if (nombre > 5)
        {
            int resultat = nombre * 2; // Variable locale au bloc if
            Console.WriteLine($"Résultat: {resultat}");
        }
        
        // Console.WriteLine(resultat); // ERREUR: resultat n'existe plus
        
        for (int i = 0; i < 3; i++) // i existe seulement dans le for
        {
            Console.WriteLine($"i = {i}");
        }
        
        // Console.WriteLine(i); // ERREUR: i n'existe plus
    }
}
```

### Exemple : Portée et méthodes

```csharp
using System;

class Programme
{
    public static int CalculerCarre(int nombre)
    {
        int resultat = nombre * nombre; // Variable locale
        return resultat;
    }
    
    static void Main()
    {
        int valeur = 5;
        int carre = CalculerCarre(valeur);
        
        Console.WriteLine($"Le carré de {valeur} est {carre}");
        
        // Console.WriteLine(resultat); // ERREUR: resultat est local à CalculerCarre
    }
}
```

---

## Bonnes pratiques

### 1. Nommage des méthodes
- Utilisez des verbes qui décrivent l'action
- Commencez par une majuscule (convention C#)
- Soyez descriptif

```csharp
// Bon
public static void CalculerMoyenne()
public static bool VerifierAge()
public static string ObtenirNomComplet()

// À éviter
public static void calc()
public static bool check()
public static string get()
```

### 2. Une méthode = une tâche
Chaque méthode devrait faire **une seule chose** et la faire bien.

```csharp
// Bon - une méthode par tâche
public static double CalculerMoyenne(int[] notes)
{
    // Calcule seulement la moyenne
}

public static void AfficherResultats(double moyenne)
{
    // Affiche seulement les résultats
}

// Moins bon - fait trop de choses
public static void CalculerEtAfficher(int[] notes)
{
    // Calcule ET affiche
}
```

### 3. Limiter le nombre de paramètres
Si une méthode a trop de paramètres (plus de 4-5), considérez regrouper les données.

```csharp
// Peut devenir difficile à gérer
public static void CreerEtudiant(string nom, string prenom, int age, 
                         string adresse, string telephone, string courriel)
{
    // Beaucoup de paramètres!
}
```

### 4. Utiliser des noms de paramètres significatifs

```csharp
// Bon
public static double CalculerPrixTotal(double prixBase, double tauxTaxe)
{
    return prixBase * (1 + tauxTaxe);
}

// Moins clair
public static double Calculer(double p, double t)
{
    return p * (1 + t);
}
```

---

## Récapitulatif : ref vs out

### Utilisez `ref` quand :
- Vous voulez **modifier** une valeur existante
- La variable doit être **initialisée** avant l'appel
- Vous voulez que les changements affectent la variable originale

### Utilisez `out` quand :
- Vous voulez **retourner plusieurs valeurs** d'une méthode
- La variable **n'a pas besoin** d'être initialisée avant l'appel
- La méthode va créer/calculer une nouvelle valeur

### Tableau comparatif

| Aspect | Passage par valeur | `ref` | `out` |
|--------|-------------------|-------|-------|
| Initialisation requise | Non | Oui | Non |
| Modifie l'original | Non | Oui | Oui |
| Doit assigner dans méthode | Non | Non | Oui |
| Usage principal | Donner des données | Modifier des données | Retourner plusieurs valeurs |

---

## Exercices

### Exercice 1 : Méthode simple
**Difficulté : Facile**

Créez une méthode `AfficherTableMultiplication` qui prend un nombre en paramètre et affiche sa table de multiplication de 1 à 10.

**Exemple de sortie pour le nombre 5 :**
```
5 x 1 = 5
5 x 2 = 10
5 x 3 = 15
...
5 x 10 = 50
```

---

### Exercice 2 : Méthode avec retour
**Difficulté : Facile**

Créez une méthode `EstPair` qui prend un nombre entier en paramètre et retourne `true` s'il est pair, `false` sinon.

**Indice :** Un nombre est pair si `nombre % 2 == 0`

---

### Exercice 3 : Méthode avec plusieurs paramètres
**Difficulté : Facile**

Créez une méthode `CalculerMoyenne` qui prend trois notes (double) en paramètres et retourne leur moyenne.

---

### Exercice 4 : Utilisation de ref
**Difficulté : Moyenne**

Créez une méthode `AugmenterDe10Pourcent` qui prend un prix en paramètre (`ref double`) et l'augmente de 10%.

Testez avec un prix initial de 100.00$ et vérifiez que la variable originale est bien modifiée.

---

### Exercice 5 : Utilisation de out - Division
**Difficulté : Moyenne**

Créez une méthode `DiviserEntiers` qui prend deux entiers (dividende et diviseur) et retourne le quotient ET le reste en utilisant des paramètres `out`.

**Exemple :** 17 divisé par 5 donne quotient = 3 et reste = 2

**Indice :** Utilisez les opérateurs `/` pour le quotient et `%` pour le reste.

---

### Exercice 6 : Utilisation de out - Statistiques
**Difficulté : Moyenne**

Créez une méthode `AnalyserTableau` qui prend un tableau d'entiers et retourne (via `out`) :
- La somme de tous les éléments
- La moyenne
- Le nombre d'éléments

Testez avec le tableau : `{10, 20, 30, 40, 50}`

---

### Exercice 7 : Paramètres optionnels
**Difficulté : Moyenne**

Créez une méthode `AfficherFacture` qui prend :
- Le montant (obligatoire)
- Le taux de TPS (optionnel, défaut = 0.05)
- Le taux de TVQ (optionnel, défaut = 0.09975)

La méthode calcule et affiche le montant avant taxes, les taxes, et le total.

---

### Exercice 8 : Surcharge de méthodes
**Difficulté : Moyenne**

Créez trois versions surchargées d'une méthode `CalculerVolume` :
1. Volume d'un cube : `CalculerVolume(double cote)`
2. Volume d'un parallélépipède : `CalculerVolume(double longueur, double largeur, double hauteur)`
3. Volume d'un cylindre : `CalculerVolume(double rayon, double hauteur, bool estCylindre)`

**Formules :**
- Cube : côté³
- Parallélépipède : longueur × largeur × hauteur
- Cylindre : π × rayon² × hauteur

---

### Exercice 9 : Validation avec TryParse
**Difficulté : Moyenne**

Créez un programme qui demande à l'utilisateur d'entrer un nombre. Utilisez `int.TryParse` avec un paramètre `out` pour valider l'entrée. Si l'entrée est valide, affichez le carré du nombre. Sinon, affichez un message d'erreur.

---

### Exercice 10 : Calculatrice simple
**Difficulté : Difficile**

Créez les méthodes suivantes pour une calculatrice :
1. `Additionner(double a, double b)` - retourne a + b
2. `Soustraire(double a, double b)` - retourne a - b
3. `Multiplier(double a, double b)` - retourne a × b
4. `Diviser(double a, double b, out bool succes)` - retourne a ÷ b et indique si la division a réussi (pas de division par zéro)

Créez un menu qui permet à l'utilisateur de choisir une opération et d'entrer deux nombres.

---

### Exercice 11 : Analyse de texte
**Difficulté : Difficile**

Créez une méthode `AnalyserTexte` qui prend une chaîne de caractères et retourne (via `out`) :
- Le nombre de caractères
- Le nombre de mots
- Le nombre de voyelles

**Indice :** Utilisez `.Split(' ')` pour séparer les mots, et une boucle pour compter les voyelles (a, e, i, o, u).

---

### Exercice 12 : Conversion de température
**Difficulté : Difficile**

Créez les méthodes suivantes avec surcharge :
1. `ConvertirTemperature(double celsius)` - convertit Celsius → Fahrenheit
2. `ConvertirTemperature(double fahrenheit, bool versCelsius)` - convertit Fahrenheit → Celsius

Puis créez une méthode `ConvertirAvecValidation` qui utilise un paramètre `out bool` pour indiquer si la température est physiquement possible (au-dessus du zéro absolu : -273.15°C ou -459.67°F).

**Formules :**
- C → F : (C × 9/5) + 32
- F → C : (F - 32) × 5/9

---

## Conclusion

Les méthodes sont un outil fondamental en programmation. Vous avez appris :

✅ **Syntaxe de base** : Comment créer et appeler des méthodes  
✅ **Paramètres** : Passage par valeur vs passage par référence  
✅ **`ref`** : Modifier des variables existantes  
✅ **`out`** : Retourner plusieurs valeurs  
✅ **Paramètres optionnels** : Valeurs par défaut  
✅ **Surcharge** : Plusieurs méthodes avec le même nom  
✅ **Portée** : Où les variables sont accessibles  

### Concepts clés à retenir

1. **Une méthode = une tâche** : Gardez vos méthodes simples et ciblées
2. **Nommage clair** : Le nom doit décrire ce que fait la méthode
3. **`ref` pour modifier, `out` pour retourner** : Choisissez selon votre besoin
4. **Réutilisabilité** : Écrivez une fois, utilisez plusieurs fois