---
title: "Tableaux et collections"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 6
---

En C#, on distingue deux grandes familles pour stocker des groupes de données : les **Tableaux** (taille fixe) et les **Collections** (taille dynamique).


## Les tableaux (Array)
Un tableau est une structure de données de taille fixe stockée de manière contiguë en mémoire.

### 1. Déclaration et initialisation

#### Syntaxe de base
```csharp
// Déclaration d'un tableau
int[] nombres;

// Déclaration avec initialisation de la taille
int[] nombres = new int[5];

// Déclaration avec initialisation des valeurs
int[] nombres = new int[] { 10, 20, 30, 40, 50 };

// Syntaxe courte (recommandée)
int[] nombres = { 10, 20, 30, 40, 50 };
```
> *Le piège de l'immuabilité : Si vous créez int[] tab = new int[3], vous ne pourrez jamais y mettre un 4e élément. Pour "agrandir" un tableau, il faut en créer un nouveau, copier les données, puis ajouter le nouvel élément. C'est pourquoi nous utilisons les Collections pour les données dynamiques.*


### Points importants à retenir
- Les tableaux ont une taille fixe définie à la création
- Les collections (comme `List<T>`) peuvent changer de taille dynamiquement
- Les propriétés en C# (comme `.Length`, `.Count`) n'ont pas de parenthèses
- C# offre LINQ (Language Integrated Query) pour manipuler les collections facilement
- Les collections sont dans le namespace `System.Collections.Generic`

---

### 2. Exemples pratiques

#### Exemple 1 : Gestion des notes d'étudiants
```csharp
using System;

class GestionNotes
{
    static void Main()
    {
        // Déclaration d'un tableau de notes
        double[] notesEtudiants = { 85.5, 92.0, 78.5, 88.0, 95.5 };
        
        // Afficher toutes les notes
        Console.WriteLine("=== Notes des étudiants ===");
        for (int i = 0; i < notesEtudiants.Length; i++)
        {
            Console.WriteLine($"Étudiant {i + 1}: {notesEtudiants[i]}%");
        }
        
        // Calculer la moyenne
        double somme = 0;
        foreach (double note in notesEtudiants)
        {
            somme += note;
        }
        double moyenne = somme / notesEtudiants.Length;
        Console.WriteLine($"\nMoyenne de la classe: {moyenne:F2}%");
    }
}
```

**Sortie :**
```
=== Notes des étudiants ===
Étudiant 1: 85.5%
Étudiant 2: 92%
Étudiant 3: 78.5%
Étudiant 4: 88%
Étudiant 5: 95.5%

Moyenne de la classe: 87.90%
```

#### Exemple 2 : Tableau de chaînes de caractères
```csharp
using System;

class GestionPrenoms
{
    static void Main()
    {
        // Tableau de prénoms
        string[] prenoms = { "Alice", "Bernard", "Catherine", "David", "Émilie" };
        
        // Afficher les prénoms avec leur longueur
        Console.WriteLine("=== Liste des prénoms ===");
        foreach (string prenom in prenoms)
        {
            Console.WriteLine($"{prenom} - {prenom.Length} lettres");
        }
        
        // Trouver le prénom le plus long
        string prenomPlusLong = prenoms[0];
        foreach (string prenom in prenoms)
        {
            if (prenom.Length > prenomPlusLong.Length)
            {
                prenomPlusLong = prenom;
            }
        }
        Console.WriteLine($"\nPrénom le plus long: {prenomPlusLong}");
    }
}
```

### 3. Tableaux multidimensionnels

#### Tableau à deux dimensions (matrice)
```csharp
using System;

class Matrice
{
    static void Main()
    {
        // Déclaration d'une matrice 3x3
        int[,] matrice = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        
        // Affichage de la matrice
        Console.WriteLine("=== Matrice 3x3 ===");
        for (int ligne = 0; ligne < 3; ligne++)
        {
            for (int colonne = 0; colonne < 3; colonne++)
            {
                Console.Write($"{matrice[ligne, colonne]}\t");
            }
            Console.WriteLine();
        }
        
        // Calculer la somme de chaque ligne
        Console.WriteLine("\n=== Somme par ligne ===");
        for (int ligne = 0; ligne < 3; ligne++)
        {
            int somme = 0;
            for (int colonne = 0; colonne < 3; colonne++)
            {
                somme += matrice[ligne, colonne];
            }
            Console.WriteLine($"Ligne {ligne + 1}: {somme}");
        }
    }
}
```

#### Tableau dentelé (jagged array)
```csharp
using System;

class TableauDentele
{
    static void Main()
    {
        // Tableau où chaque ligne peut avoir une longueur différente
        int[][] notesParCours = new int[3][];
        
        notesParCours[0] = new int[] { 85, 90, 78 };           // 3 notes
        notesParCours[1] = new int[] { 92, 88 };               // 2 notes
        notesParCours[2] = new int[] { 95, 87, 91, 89 };       // 4 notes
        
        // Affichage
        Console.WriteLine("=== Notes par cours ===");
        for (int cours = 0; cours < notesParCours.Length; cours++)
        {
            Console.Write($"Cours {cours + 1}: ");
            foreach (int note in notesParCours[cours])
            {
                Console.Write($"{note} ");
            }
            Console.WriteLine();
        }
    }
}
```

### 4. Méthodes utiles pour les tableaux

```csharp
using System;

class MethodesTableaux
{
    static void Main()
    {
        int[] nombres = { 45, 12, 78, 34, 90, 23, 67 };
        
        // Longueur du tableau
        Console.WriteLine($"Nombre d'éléments: {nombres.Length}");
        
        // Trier le tableau
        Array.Sort(nombres);
        Console.WriteLine("\nTableau trié:");
        AfficherTableau(nombres);
        
        // Inverser l'ordre
        Array.Reverse(nombres);
        Console.WriteLine("\nTableau inversé:");
        AfficherTableau(nombres);
        
        // Rechercher un élément
        int valeurRecherchee = 78;
        int index = Array.IndexOf(nombres, valeurRecherchee);
        Console.WriteLine($"\nIndex de {valeurRecherchee}: {index}");
        
        // Copier un tableau
        int[] copie = new int[nombres.Length];
        Array.Copy(nombres, copie, nombres.Length);
        Console.WriteLine("\nCopie du tableau:");
        AfficherTableau(copie);
    }
    
    static void AfficherTableau(int[] tableau)
    {
        foreach (int nombre in tableau)
        {
            Console.Write($"{nombre} ");
        }
        Console.WriteLine();
    }
}
```

---

## Les Collections

Les collections en C# offrent plus de flexibilité que les tableaux. Elles peuvent changer de taille dynamiquement et offrent des méthodes pratiques pour manipuler les données.

### 1. List<T> - La collection la plus utilisée

#### Syntaxe de base
```csharp
using System;
using System.Collections.Generic;

class ExempleList
{
    static void Main()
    {
        // Création d'une liste vide
        List fruits = new List();
        
        // Création avec valeurs initiales
        List nombres = new List { 1, 2, 3, 4, 5 };
        
        // Ajout d'éléments
        fruits.Add("Pomme");
        fruits.Add("Banane");
        fruits.Add("Orange");
        
        // Affichage
        Console.WriteLine("=== Liste de fruits ===");
        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }
        
        // Nombre d'éléments
        Console.WriteLine($"\nNombre de fruits: {fruits.Count}");
    }
}
```

#### Exemple 2 : Gestion d'un inventaire simple
```csharp
using System;
using System.Collections.Generic;

class GestionInventaire
{
    static void Main()
    {
        // Création de listes pour stocker les informations
        List nomsProduits = new List();
        List prixProduits = new List();
        List quantitesProduits = new List();
        
        // Ajout de produits
        nomsProduits.Add("Ordinateur portable");
        prixProduits.Add(1299.99);
        quantitesProduits.Add(15);
        
        nomsProduits.Add("Souris sans fil");
        prixProduits.Add(29.99);
        quantitesProduits.Add(50);
        
        nomsProduits.Add("Clavier mécanique");
        prixProduits.Add(149.99);
        quantitesProduits.Add(25);
        
        // Affichage de l'inventaire
        Console.WriteLine("=== INVENTAIRE ===");
        for (int i = 0; i < nomsProduits.Count; i++)
        {
            Console.WriteLine($"{nomsProduits[i]} - {prixProduits[i]:C} - Qté: {quantitesProduits[i]}");
        }
        
        // Calculer la valeur totale de l'inventaire
        double valeurTotale = 0;
        for (int i = 0; i < nomsProduits.Count; i++)
        {
            valeurTotale += prixProduits[i] * quantitesProduits[i];
        }
        Console.WriteLine($"\nValeur totale de l'inventaire: {valeurTotale:C}");
    }
}
```

#### Méthodes importantes de List<T>
```csharp
using System;
using System.Collections.Generic;

class MethodesList
{
    static void Main()
    {
        List etudiants = new List 
        { "Alice", "Bernard", "Catherine", "David" };
        
        // Add - Ajouter un élément à la fin
        etudiants.Add("Émilie");
        
        // Insert - Insérer à une position spécifique
        etudiants.Insert(1, "François");
        
        // Remove - Retirer un élément spécifique
        etudiants.Remove("Bernard");
        
        // RemoveAt - Retirer à un index spécifique
        etudiants.RemoveAt(0);
        
        // Contains - Vérifier si un élément existe
        bool existe = etudiants.Contains("Catherine");
        Console.WriteLine($"Catherine est dans la liste: {existe}");
        
        // IndexOf - Trouver l'index d'un élément
        int index = etudiants.IndexOf("David");
        Console.WriteLine($"Index de David: {index}");
        
        // Clear - Vider la liste
        // etudiants.Clear();
        
        // Sort - Trier la liste
        etudiants.Sort();
        
        // Reverse - Inverser l'ordre
        etudiants.Reverse();
        
        // Count - Nombre d'éléments
        Console.WriteLine($"Nombre d'étudiants: {etudiants.Count}");
        
        // Affichage final
        Console.WriteLine("\n=== Liste finale ===");
        foreach (string etudiant in etudiants)
        {
            Console.WriteLine(etudiant);
        }
    }
}
```

### 2. Dictionary<TKey, TValue> - Paires clé-valeur

```csharp
using System;
using System.Collections.Generic;

class ExempleDictionnaire
{
    static void Main()
    {
        // Création d'un dictionnaire pour stocker des codes postaux
        Dictionary codesPostaux = new Dictionary();
        
        // Ajout d'éléments
        codesPostaux.Add("Montréal", "H1A");
        codesPostaux.Add("Québec", "G1A");
        codesPostaux.Add("Laval", "H7A");
        codesPostaux.Add("Gatineau", "J8T");
        
        // Accès à une valeur
        Console.WriteLine($"Code postal de Montréal: {codesPostaux["Montréal"]}");
        
        // Vérifier si une clé existe
        if (codesPostaux.ContainsKey("Québec"))
        {
            Console.WriteLine($"Code postal de Québec: {codesPostaux["Québec"]}");
        }
        
        // Parcourir le dictionnaire
        Console.WriteLine("\n=== Tous les codes postaux ===");
        foreach (KeyValuePair paire in codesPostaux)
        {
            Console.WriteLine($"{paire.Key}: {paire.Value}");
        }
        
        // Modifier une valeur
        codesPostaux["Montréal"] = "H2X";
        
        // Retirer un élément
        codesPostaux.Remove("Gatineau");
        
        Console.WriteLine($"\nNombre de villes: {codesPostaux.Count}");
    }
}
```

#### Exemple pratique : Système de notes
```csharp
using System;
using System.Collections.Generic;

class SystemeNotes
{
    static void Main()
    {
        // Dictionnaire: nom de l'étudiant -> liste de notes
        Dictionary> notesEtudiants = new Dictionary>();
        
        // Ajout des étudiants et leurs notes
        notesEtudiants.Add("Alice", new List { 85.5, 92.0, 88.5 });
        notesEtudiants.Add("Bernard", new List { 78.0, 82.5, 80.0 });
        notesEtudiants.Add("Catherine", new List { 95.0, 93.5, 97.0 });
        
        // Afficher les notes et moyennes
        Console.WriteLine("=== NOTES ET MOYENNES ===\n");
        foreach (KeyValuePair> etudiant in notesEtudiants)
        {
            string nom = etudiant.Key;
            List notes = etudiant.Value;
            
            Console.WriteLine($"Étudiant: {nom}");
            Console.Write("Notes: ");
            foreach (double note in notes)
            {
                Console.Write($"{note}% ");
            }
            
            // Calculer la moyenne
            double somme = 0;
            foreach (double note in notes)
            {
                somme += note;
            }
            double moyenne = somme / notes.Count;
            Console.WriteLine($"\nMoyenne: {moyenne:F2}%\n");
        }
    }
}
```

### 3. Queue<T> - File d'attente (FIFO)

```csharp
using System;
using System.Collections.Generic;

class ExempleQueue
{
    static void Main()
    {
        // Création d'une file d'attente
        Queue fileAttente = new Queue();
        
        // Enqueue - Ajouter à la fin de la file
        fileAttente.Enqueue("Client 1");
        fileAttente.Enqueue("Client 2");
        fileAttente.Enqueue("Client 3");
        fileAttente.Enqueue("Client 4");
        
        Console.WriteLine($"Nombre de clients: {fileAttente.Count}");
        
        // Peek - Voir le premier élément sans le retirer
        string premier = fileAttente.Peek();
        Console.WriteLine($"Prochain client à servir: {premier}");
        
        // Dequeue - Retirer et retourner le premier élément
        Console.WriteLine("\n=== Service des clients ===");
        while (fileAttente.Count > 0)
        {
            string client = fileAttente.Dequeue();
            Console.WriteLine($"Servir: {client}");
        }
        
        Console.WriteLine($"\nClients restants: {fileAttente.Count}");
    }
}
```

### 4. Stack<T> - Pile (LIFO)

```csharp
using System;
using System.Collections.Generic;

class ExempleStack
{
    static void Main()
    {
        // Création d'une pile
        Stack historiqueNavigation = new Stack();
        
        // Push - Ajouter au sommet de la pile
        historiqueNavigation.Push("Page d'accueil");
        historiqueNavigation.Push("Page de recherche");
        historiqueNavigation.Push("Page de résultats");
        historiqueNavigation.Push("Page de détails");
        
        Console.WriteLine($"Nombre de pages visitées: {historiqueNavigation.Count}");
        
        // Peek - Voir le sommet sans retirer
        string pageActuelle = historiqueNavigation.Peek();
        Console.WriteLine($"Page actuelle: {pageActuelle}");
        
        // Pop - Retirer et retourner l'élément du sommet
        Console.WriteLine("\n=== Navigation arrière ===");
        while (historiqueNavigation.Count > 0)
        {
            string page = historiqueNavigation.Pop();
            Console.WriteLine($"Retour à: {page}");
        }
    }
}
```

### 5. HashSet<T> - Ensemble (pas de doublons)

```csharp
using System;
using System.Collections.Generic;

class ExempleHashSet
{
    static void Main()
    {
        // Création d'un ensemble
        HashSet tagsUniques = new HashSet();
        
        // Add - Ajouter un élément (ignore les doublons)
        tagsUniques.Add("programmation");
        tagsUniques.Add("csharp");
        tagsUniques.Add("collections");
        tagsUniques.Add("csharp"); // Sera ignoré (doublon)
        tagsUniques.Add("dotnet");
        
        Console.WriteLine($"Nombre de tags uniques: {tagsUniques.Count}");
        
        // Affichage
        Console.WriteLine("\n=== Tags ===");
        foreach (string tag in tagsUniques)
        {
            Console.WriteLine($"- {tag}");
        }
        
        // Contains - Vérifier l'existence
        bool existe = tagsUniques.Contains("csharp");
        Console.WriteLine($"\nLe tag 'csharp' existe: {existe}");
        
        // Opérations sur les ensembles
        HashSet autresTags = new HashSet { "csharp", "java", "python" };
        
        // Union
        tagsUniques.UnionWith(autresTags);
        Console.WriteLine($"\nAprès union: {tagsUniques.Count} tags");
        
        // Intersection
        HashSet langages = new HashSet { "csharp", "java", "python" };
        HashSet appris = new HashSet { "csharp", "javascript" };
        langages.IntersectWith(appris);
        Console.WriteLine("\n=== Langages en commun ===");
        foreach (string langage in langages)
        {
            Console.WriteLine(langage);
        }
    }
}
```

---
