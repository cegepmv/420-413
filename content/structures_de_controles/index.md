---
title: "Structures de contrôles"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 5
---

## 1. Structures Conditionnelles

### 1.1 If-Else

**Similitudes avec Java :** La syntaxe de base est identique.

```csharp
// Calcul de rabais selon la quantité et le montant
int quantite = 15;
double montantTotal = 250.50;
double rabais = 0.0;

if (quantite >= 20 && montantTotal >= 500)
{
    rabais = 0.20; // 20% pour commandes importantes
}
else if (quantite >= 10 || montantTotal >= 200)
{
    rabais = 0.10; // 10% pour commandes moyennes
}
else
{
    rabais = 0.0; // Pas de rabais
}

double montantFinal = montantTotal * (1 - rabais);
Console.WriteLine($"Montant final: {montantFinal} $");
```

**✅ Bonne pratique - Accolades obligatoires**
```csharp
int stock = 5;
int quantite = 10;

// ✅ BIEN - Toujours utiliser des accolades
if (quantite > stock)
{
    Console.WriteLine("Stock insuffisant");
}

// ❌ ÉVITER - Même si syntaxiquement valide
if (quantite > stock)
    Console.WriteLine("Stock insuffisant");
```

### 1.2 Opérateur Ternaire

Identique à Java, mais C# encourage son utilisation judicieuse.

```csharp
// Calcul de frais d'expédition
// Calcul des frais d'expédition
double montantCommande = 45.00;
double fraisExpedition = montantCommande >= 50 ? 0.0 : 5.99;

Console.WriteLine($"Frais d'expédition : {fraisExpedition:C}");

// Détermination du statut de paiement
int joursRetard = 15;
string statutPaiement = joursRetard > 0 ? "En retard" : "À jour";

Console.WriteLine($"Statut : {statutPaiement}");
```

**✅ Bonne pratique - Ternaires imbriqués**
```csharp
int noteExamen = 85;

// ✅ Acceptable pour des cas simples (Logique linéaire)
string mention = noteExamen >= 90 ? "A" :
                 noteExamen >= 80 ? "B" :
                 noteExamen >= 70 ? "C" :
                 noteExamen >= 60 ? "D" : "F";

Console.WriteLine($"Résultat : {mention}");

// ❌ À ÉVITER - Trop complexe, rend le débogage difficile
int heuresTravaillees = 45;
int heuresSupplementaires = 5;
bool estWeekend = true;

// Cette structure imbriquée est une "dette technique" immédiate
double tauxHoraire = heuresTravaillees > 40 ? (estWeekend ? 30.0 : 25.0) : 
                     (heuresSupplementaires > 0 ? 22.0 : 20.0);

// Difficile à lire et à maintenir !
```

### 1.3 Switch Statement

**Différence majeure avec Java :** Pas de "fall-through" implicite en C#.

```csharp
int jour = 3;

switch (jour)
{
    case 1:
        Console.WriteLine("Lundi");
        break; // break obligatoire
    case 2:
        Console.WriteLine("Mardi");
        break;
    case 3:
        Console.WriteLine("Mercredi");
        break;
    case 4:
        Console.WriteLine("Jeudi");
        break;
    case 5:
        Console.WriteLine("Vendredi");
        break;
    default:
        Console.WriteLine("Weekend ou invalide");
        break;
}
```

**Fall-through explicite en C#**
```csharp
int jour = 6;

switch (jour)
{
    case 6:
    case 7:
        Console.WriteLine("Weekend");
        break; // Un seul break pour les deux cas
    case 1:
    case 2:
    case 3:
    case 4:
    case 5:
        Console.WriteLine("Jour de semaine");
        break;
    default:
        Console.WriteLine("Jour invalide");
        break;
}
```

**✅ Bonne pratique - Switch avec char et string**
```csharp
// Switch avec char
char noteLettre = 'B';
switch (noteLettre)
{
    case 'A':
        Console.WriteLine("Excellent");
        break;
    case 'B':
        Console.WriteLine("Très bien");
        break;
    case 'C':
        Console.WriteLine("Bien");
        break;
    default:
        Console.WriteLine("À améliorer");
        break;
}

// Switch avec string
string commande = "start";
switch (commande)
{
    case "start":
        Console.WriteLine("Démarrage...");
        break;
    case "stop":
        Console.WriteLine("Arrêt...");
        break;
    case "pause":
        Console.WriteLine("Pause...");
        break;
    default:
        Console.WriteLine("Commande inconnue");
        break;
}
```


### 1.4 Switch Expression (C# 8.0+)

**Nouveauté C#**

```csharp
// Syntaxe concise et fonctionnelle
int numeroDuJour = 3;
string nomJour = numeroDuJour switch
{
    1 => "Lundi",
    2 => "Mardi",
    3 => "Mercredi",
    4 => "Jeudi",
    5 => "Vendredi",
    6 => "Weekend",
    7 => "Weekend",
    _ => "Invalide"
};
Console.WriteLine(nomJour);
```

**✅ Bonne pratique - Switch expression pour calculer des valeurs**
```csharp
// Utilisation de valeurs discrètes (cas précis)
int numeroJour = 3;

string typeJour = numeroJour switch
{
    1 or 2 => "Début de semaine", 
    3 or 4 => "Milieu de semaine",
    5      => "Fin de semaine",
    6 or 7 => "Weekend",
    _      => "Invalide"
};

Console.WriteLine(typeJour);

// Pour les comparaisons de ranges, utiliser if-else
iint age = 25;
double prixBillet;

if (age < 5)
    prixBillet = 0.0;
else if (age < 18)
    prixBillet = 8.50;
else if (age < 65)
    prixBillet = 12.00;
else
    prixBillet = 9.00;

Console.WriteLine($"Prix du billet : {prixBillet} $");
```

**✅ Bonne pratique - Switch expression avec valeurs multiples**
```csharp
char lettre = 'e';

// ✅ Version optimisée (C# 9+) : Plus lisible et moins de répétitions
bool estUneVoyelle = lettre switch
{
    'a' or 'e' or 'i' or 'o' or 'u' or
    'A' or 'E' or 'I' or 'O' or 'U' => true,
    _                               => false
};

Console.WriteLine($"Est une voyelle : {estUneVoyelle}");
```

**✅ Bonne pratique - Quand utiliser switch vs if-else**
```csharp
int note = 85;

// La switch expression agit comme une "table de correspondance"
string mention = note switch
{
    >= 90 => "A",
    >= 80 => "B",
    >= 70 => "C",
    >= 60 => "D",
    _    => "F" // Le discard (_) est obligatoire pour couvrir tous les cas
};

Console.WriteLine($"Mention obtenue : {mention}");

double solde = 1500.50;
int nombreTransactions = 25;
bool aUnDecouvert = false;
double fraisMensuels = 0.0;

// Logique décisionnelle basée sur plusieurs critères
if (solde >= 5000 && nombreTransactions > 50)
{
    fraisMensuels = 0.0; // Compte premium gratuit
}
else if (solde >= 1000 || nombreTransactions <= 10)
{
    fraisMensuels = 5.0; // Frais réduits (fidélité ou faible usage)
}
else if (aUnDecouvert)
{
    fraisMensuels = 25.0; // Pénalité pour découvert
}
else
{
    fraisMensuels = 12.0; // Frais standards
}

// Utilisation du format monétaire :C (très pratique en .NET 8)
Console.WriteLine($"Frais mensuels : {fraisMensuels:C}");
```


## 2. Structures Itératives

### 2.1 Boucle While

Identique à Java.

```csharp
int i = 0;
while (i < 5)
{
    Console.WriteLine(i);
    i++;
}
```

**✅ Bonne pratique - While avec condition claire**
```csharp
int somme = 0;
int nombre = 1;

while (somme < 100)
{
    somme += number;
    nombre++;
}

Console.WriteLine($"Somme: {somme}, Dernier nombre: {nombre}");
```

**✅ Bonne pratique - Éviter les boucles infinies**
```csharp
// ❌ DANGEREUX
// while (true)
// {
//     // Sans condition de sortie claire
// }

// ✅ MIEUX - Avec limite de sécurité
int maxTentatives = 10;
int tentatives = 0;
bool succes = false;

// ✅ Utilisation d'une boucle While avec deux conditions de sortie
while (tentatives < maxTentatives && !succes)
{
    Console.Write("Entrez un nombre entre 1 et 10 : ");
    string saisie = Console.ReadLine();
    
    // ✅ int.TryParse évite que le programme plante si l'utilisateur tape du texte
    // Le "out int nombre" déclare la variable uniquement si la conversion réussit
    if (int.TryParse(saisie, out int nombre) && nombre >= 1 && nombre <= 10)
    {
        succes = true;
        Console.WriteLine("Valide !");
    }
    else
    {
        tentatives++;
        Console.WriteLine($"Invalide. Tentatives restantes : {maxTentatives - tentatives}");
    }
}
```


### 2.2 Boucle Do-While

Identique à Java.

```csharp
int i = 0;
do
{
    Console.WriteLine(i);
    i++;
} while (i < 5);
```

**✅ Bonne pratique - Utiliser do-while pour validation**
Utilisation pour la validation de saisie : Cette structure garantit que l'utilisateur verra le message au moins une fois.
```csharp
int nombre;
string saisie;

do
{
    Console.Write("Entrez un nombre positif : ");
    saisie = Console.ReadLine();
    
    // On boucle tant que la saisie n'est pas un entier OU que le nombre est <= 0
} while (!int.TryParse(saisie, out nombre) || nombre <= 0);

Console.WriteLine($"Vous avez entré : {nombre}");
```

**Exemple - Menu simple**
```csharp
int choix;
do
{
    Console.WriteLine("\n--- MENU ---");
    Console.WriteLine("1. Addition");
    Console.WriteLine("2. Soustraction");
    Console.WriteLine("3. Quitter");
    Console.Write("Votre choix : ");
    
    //si l'utilisateur ne tape pas un chiffre et que la conversion ne fonction pas, choix va avoir la valeur par défaut 0
    int.TryParse(Console.ReadLine(), out choix);
    
    switch (choix)
    {
        case 1:
            Console.WriteLine("Addition sélectionnée");
            break;
        case 2:
            Console.WriteLine("Soustraction sélectionnée");
            break;
        case 3:
            Console.WriteLine("Au revoir !");
            break;
        default:
            Console.WriteLine("Choix invalide, veuillez recommencer.");
            break;
    }
    
    // La boucle continue tant que l'utilisateur n'a pas choisi de quitter (3)
} while (choix != 3);
```


### 2.3 Boucle For

Identique à Java.

```csharp
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

**✅ Bonne pratique - Déclarer la variable dans la boucle**
```csharp
// ✅ BIEN - Portée limitée
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
// i n'existe plus ici

// ❌ ÉVITER - Portée trop large
int j;
for (j = 0; j < 10; j++)
{
    Console.WriteLine(j);
}
// j existe encore ici
```

**Itérer sur un tableau**

La boucle for est privilégiée lorsque l'indexation est nécessaire ou pour manipuler des structures multidimensionnelles.

```csharp
int[] nombres = { 10, 20, 30, 40, 50 };

// Calcul de la somme
int somme = 0;
for (int i = 0; i < nombres.Length; i++)
{
    somme += nombres[i];
}
Console.WriteLine($"Somme : {somme}");

// Recherche d'un élément
int valeurRecherchee = 30;
int indexTrouve = -1;

for (int i = 0; i < nombres.Length; i++)
{
    if (nombres[i] == valeurRecherchee)
    {
        indexTrouve = i;
        break; // On quitte la boucle dès qu'on a trouvé
    }
}

if (indexTrouve != -1)
{
    Console.WriteLine($"Trouvé à l'index {indexTrouve}");
}
```

**✅ Bonne pratique - Éviter de modifier le compteur dans la boucle**
```csharp
int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// ❌ MAUVAIS - Difficile à comprendre
for (int i = 0; i < values.Length; i++)
{
    Console.WriteLine(values[i]);
    if (values[i] % 2 == 0)
        i++; // Modifie le flux de contrôle - confus!
}

// ✅ MIEUX - Intention claire
for (int i = 0; i < values.Length; i++)
{
    Console.WriteLine(values[i]);
    
    if (values[i] % 2 == 0 && i + 1 < values.Length)
    {
        i++; // Saute le prochain si pair
    }
}
```

**Boucles imbriquées - Tableaux 2D**
```csharp
int[,] matrice = {
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 }
};

// Affichage de la matrice ligne par ligne
for (int ligne = 0; ligne < 3; ligne++)
{
    for (int col = 0; col < 3; col++)
    {
        Console.Write($"{matrice[ligne, col]} ");
    }
    Console.WriteLine(); // Saut de ligne après chaque rangée
}
```


### 2.4 Boucle Foreach

**Différence avec Java :** Mot-clé `foreach` au lieu de `for`.
C'est l'outil le plus sûr car il élimine les erreurs de dépassement d'index (le fameux IndexOutOfRangeException).

```csharp
// Java: for (String name : names)
// C#:
string[] noms = { "Alice", "Bob", "Charlie" };
foreach (string nom in noms)
{
    Console.WriteLine(nom);
}
```

**✅ Bonne pratique - Préférer foreach quand approprié**
```csharp
iint[] nombres = { 1, 2, 3, 4, 5 };

// ✅ BIEN - Plus lisible, idéal pour le calcul ou l'affichage
int somme = 0;
foreach (int n in nombres)
{
    somme += n;
}
Console.WriteLine($"Somme : {somme}");

// ❌ À ÉVITER - Sauf si vous avez besoin de l'index 'i' (ex: modification)
int sommeAlternative = 0;
for (int i = 0; i < nombres.Length; i++)
{
    sommeAlternative += nombres[i];
}
```

**Exemples pratiques avec foreach**
```csharp
// Affichage de prix formatés (Symbole $ au lieu de :C)
double[] prixUnitaires = { 19.99, 29.99, 39.99, 49.99 };
foreach (double p in prixUnitaires)
{
    Console.WriteLine($"Prix : {p} $");
}

// Comptage avec condition (Filtrage simple)
int[] scores = { 45, 78, 92, 65, 88, 54, 91 };
int reussites = 0;

foreach (int s in scores)
{
    if (s >= 60)
    {
        reussites++;
    }
}
Console.WriteLine($"{reussites} étudiants ont réussi.");

// Recherche du maximum
int[] valeurs = { 23, 67, 12, 89, 45, 34 };
int maximum = valeurs[0];

foreach (int v in valeurs)
{
    if (v > maximum)
    {
        maximum = v;
    }
}
Console.WriteLine($"Valeur maximale : {maximum}");
```

**⚠️ Important - Foreach est read-only**

Il est impossible de modifier directement l'élément de la collection à l'intérieur d'un foreach. Le compilateur .NET 8 bloquera le code.

```csharp
int[] chiffres = { 1, 2, 3 };

// ❌ ERREUR DE COMPILATION - On ne peut pas modifier 'c'
/*
foreach (int c in chiffres)
{
    c = c * 2; 
}
*/

// ✅ SOLUTION - Utiliser 'for' pour modifier le contenu du tableau
for (int i = 0; i < chiffres.Length; i++)
{
    chiffres[i] = chiffres[i] * 2;
}

// Vérification (Lecture simple via foreach)
foreach (int c in chiffres)
{
    Console.WriteLine(c); // Affiche : 2, 4, 6
}
```

**Foreach avec tableaux multidimensionnels**
```csharp
int[,] matrice = {
    { 1, 2, 3 },
    { 4, 5, 6 }
};

// Foreach parcourt automatiquement toutes les lignes et colonnes
foreach (int valeur in matrice)
{
    Console.Write($"{valeur} ");
}
// Résultat : 1 2 3 4 5 6
```


## 3. Instructions de Contrôle de Flux

### 3.1 Break

Identique à Java - Sort de la boucle ou du switch.

```csharp
// Recherche d'un élément dans un tableau
int[] nombres = { 5, 12, 8, 3, 19, 7 };
int cible = 19;
int position = -1;

for (int i = 0; i < nombres.Length; i++)
{
    if (nombres[i] == cible)
    {
        position = i;
        break; // ✅ Succès : On arrête de chercher pour économiser des ressources
    }
}

if (position != -1)
{
    Console.WriteLine($"Trouvé à la position {position}");
}
else
{
    Console.WriteLine("L'élément n'a pas été trouvé.");
}
```

**Break dans switch**
```csharp
int operation = 2;
int nombre1 = 10;
int nombre2 = 5;
int resultat = 0;

switch (operation)
{
    case 1:
        resultat = nombre1 + nombre2;
        break; // Sort du switch après l'addition
    case 2:
        resultat = nombre1 - nombre2;
        break; // Sort du switch après la soustraction
    case 3:
        resultat = nombre1 * nombre2;
        break; // Sort du switch après la multiplication
    default:
        Console.WriteLine("Opération invalide");
        break;
}

Console.WriteLine($"Résultat : {resultat}");
```


### 3.2 Continue

Identique à Java - Passe à l'itération suivante.

```csharp
// Afficher seulement les nombres impairs
for (int i = 0; i < 10; i++)
{
    if (i % 2 == 0)
        continue; // Saute les nombres pairs
    
    Console.WriteLine(i);
}
// Affiche: 1, 3, 5, 7, 9
```

**Continue pour filtrage simple**
```csharp
int[] notes = { 45, 78, 0, 92, -1, 65, 88 };

// ✅ BIEN - Utilisation de continue pour filtrer (Style "Guard Clause")
foreach (int note in notes)
{
    // Si la note est hors limite, on l'ignore immédiatement
    if (note < 0 || note > 100)
        continue; 
    
    // Le code principal reste ici, sans être caché dans un bloc 'if'
    Console.WriteLine($"Note valide : {note}");
}
```

**Exemple - Somme conditionnelle**
```csharp
int[] nombres = { 5, -3, 12, 0, 8, -7, 15 };
int sommePositifs = 0;

foreach (int n in nombres)
{
    // Si le nombre est négatif ou nul, on passe directement au suivant
    if (n <= 0)
        continue; 
    
    // On ne traite que les valeurs qui nous intéressent
    sommePositifs += n;
}

Console.WriteLine($"Somme des nombres positifs : {sommePositifs}");

```



## 4. Gestion des Exceptions

### 4.1 Try-Catch-Finally

Très similaire à Java.

```csharp
try
{
    int result = 10 / 0; // Génère une exception
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"Erreur: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erreur générale: {ex.Message}");
}
finally
{
    Console.WriteLine("Toujours exécuté");
}
```