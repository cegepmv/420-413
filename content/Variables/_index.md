---
title: "Variables"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 4
---


## 1. Déclaration de variables et types de données

C# est un langage fortement typé, ce qui signifie que chaque variable doit être déclarée avec un type spécifique. Voici quelques types de base :

```csharp
int age = 25;          // Entier
double price = 19.99;  // Nombre à virgule flottante
char grade = 'A';      // Caractère unique
string name = "John";  // Chaîne de caractères
bool isActive = true;  // Booléen (vrai ou faux)

```
* Une variable peut être déclarée et initialisée avec la même instruction.<br>
    `string salutation = "Bonjour tout le monde!";`

* Il est également possible de déclarer et d’initier plusieurs variables en une seule instruction, à la condition qu’elles soient du même type. Dans ce cas, les variables sont séparées par une virgule. <br>
    `bool joyeux = true, content = true;`

* Une variable peut également être marquée par le mot-clé `const` qui spécifie que la valeur de la variable ne peut pas être modifiée pendant l’exécution. C’est une variable en lecture seule.
    `const double pi = 3.14;`

* L'inférence de type (var) : Le mot-clé var permet au compilateur de deviner le type de la variable lors de l'initialisation. Attention : le type reste fixe après l'assignation.

    `var total = 15.5; // Le compilateur déduit 'double'`


#### **💡 Voici quelques conventions de codage applicables aux variables :**

1. La règle d'or : **Camel Case** 
<br>Pour les variables locales (celles définies à l'intérieur d'une méthode), la convention universelle en C# est le camelCase. <br>
    `Par exemple : string thisIsCamelCase;.` <br>

    * Le premier mot commence par une lettre minuscule.

    * Chaque mot suivant commence par une lettre majuscule.

    * On ne doit pas utiliser de chiffres au début du nom.

    * Le trait de soulignement (_) est proscrit pour le moment.

    ```csharp
    int nombreEtudiants; // Correct	

    string codePermanent; // Correct

    double soldeBanque;	 // Correct

    int NombreEtudiants; // (PascalCase) Incorrect (ou non conventionnel)

    string code_permanent; // (snake_case) non conventionnel)

    double soldebanque; // (tout en minuscule) Incorrect (ou non conventionnel)

    ```

2. Sémantique (Donner du sens)

    Une variable doit décrire son contenu sans ambiguïté. En programmation professionnelle, on évite les noms d'une seule lettre.

    * **Évitez** : `string s = "Informatique";`

    * **Privilégiez** : `string nomProgramme = "Informatique";`


#### Tableau récapitulatif des conventions de nommage C#

| Élément | Convention | Exemple |
| :--- | :--- | :--- |
| **Variable locale** | camelCase | `uniteDisponible` |
| **Paramètre de méthode** | camelCase | `(int quantiteItems)` |
| **Constante** | PascalCase | `TauxTaxeFederale` |
| **Classe** | PascalCase | `GestionnaireEtudiant` |
| **Méthode** | PascalCase | `CalculerSalaire()` |

## 2. Les types de base

* Les types de données permettent de stocker des valeurs dans l’application. 
* Les langages .NET étant fortement typés, il n’est pas toujours possible de convertir un type de données à un autre.
* Les conversions permettent de convertir les types de données.
* Cela est possible, car tous les types du Framework .NET dérivent du type Object qui est le type de case de tous les autres types

### Les types numériques

Les types numériques sont décomposés en deux parties: Les entiers et les décimaux. 
Chacun dispose d’un ensemble de types pour représenter les données de la manière la plus judicieuse en fonction des besoins.

#### Les entiers

![Les types entiers](/420-413/images/entiers.png)

* Une valeur peut être assignée à un entier avec une notation décimale:
    ```csharp
    int nombre = 10; //Notation décimale
    ```

* La notation hexadécimale peut être utilisée et elle doit être précédée du préfixe 0x:
    ```csharp
    int nombre = 0x4B; // Notation hexadécimale équivalente à 75
    ````

* La notation binaire peut être utilisée et elle doit être précédée du préfixe 0b:
    ```csharp
    int nombre = 0b1101; // Notation binaire équivalente à 13
    ```


#### Les décimaux

![Les décimaux](/420-413/images/decimaux.png)


#### Les booléens
Un booléen est un type qui permet de représenter une valeur qui est soit true, soit false. Le type .NET correspondant est System.Boolean et son nom C# est bool.
Il est possible d’assigner à un booléen le résultat d’une comparaison:

```csharp
int nombre = 8;
bool estPair = nombre % 2 == 0;
```

### Les chaînes de caractères
* Le type `System.String` (string) est un type de référence qui représente une série de types `System.Char` (char)<br>Une variable de type `char` est assignée avec un caractère placé entre guillemets simples:
    ```csharp
    char premiereLettre = 'a';
    ```

* Une variable de type string est assignée avec une chaîne de caractère placée entre des guillemets doubles
    ```csharp
    string salutation = "Bonjour tout le monde!";
    ```
* La propriété `Length` permet de savoir quelle est la longueur d’un string
    ```csharp
    Console.WriteLine("Hello".Length); // Résultat: 5
    ```


#### Déclaration des chaines de caractères

```csharp
// Déclarer sans initialiser.
string message1;

// Initialiser à null.
string message2 = null;

// Initialise comme une chaîne vide (empty string).
// Utilise la constante Empty au lieu de "".
string message3 = System.String.Empty;

// Initialiser avec une chaîne de caractères normale.
string vieuxChemin = "c:\\Program Files\\Microsoft Visual Studio 8.0";

// Initialiser avec un littéral de chaîne verbatim (pratique pour les chemins).
string nouveauChemin = @"c:\Program Files\Microsoft Visual Studio 9.0";

// Utilisez System.String si vous préférez.
System.String salutation = "Hello World!";

// Dans les variables locales (c'est-à-dire dans le corps d'une méthode),
// vous pouvez utiliser le typage implicite.
var temporaire = "I'm still a strongly-typed System.String!";

// Utilisez une chaîne const pour empêcher la variable de stocker une autre valeur.
const string messagePermanent = "Tu ne peux pas te débarrasser de moi!";

// Utilisez le constructeur String uniquement lors de la création
// d'une chaîne à partir d'un char*, char[] ou sbyte*.
char[] lettres = { 'A', 'B', 'C' };
string alphabet = new string(lettres);
```

#### Quelques propriétés et méthodes de la classe string

Soit la variable suivante : `string salutation = "Bonjour tout le monde!";`

* La nombre de caractère:

    ```csharp
    int nombreCaractere = salutation.Length; // 22

    ```   

* Convertir tous les caractères de la chaîne en majuscule:
    ```csharp
    string salutationMajuscule = salutation.ToUpper(); // BONJOUR TOUT LE MONDE!
    ```   

* Convertir tous les caractères de la chaîne en lettre minuscule
    ```csharp
    string salutationMinuscule = salutation.ToLower(); // bonjour tout le monde!
    ```

* Vérifier si le string contient une sous-chaîne de caractère ou un mot
    ```csharp
    bool contientBonjour = salutation.Contains("bonjour");
    contientBonjour = salutation.Contains("Bonjour"); //true
    ```
* Remplacer toutes les occurrences d'un caractère dans la chaîne par un autre
    ```csharp
    string salut = salutation.Replace("Bonjour", "Salut") // Salut tout le monde!
    ```
* Retourne une partie de la chaine, le 0 est l'index du début, et le 7 est le nombre de caractère à considérer
    ```csharp
    string bonjour = salutation.Substring(0, 7); // Bonjour
    ```
* Admettons que j'ajoute un autre string, pour Concaténer les strings
    ```csharp
    string question = "Comment allez-vous?";
    String nouvelleSalutation = string.Concat(salutation, " ", question); //Bonjour tout le monde! Comment allez-vous?
    ```
* Formatter la chaine de caractère pour remplacer les expressions de type {0}, {1}, {2}, {3}, etc. Présentes dans la chaîne par les valeurs passées en paramètres lors de l'appel de la fonction.
    ```csharp
    string firstName = "Sara";
    int count = 25;                      
    float temperature = 34.4f;

    Console.WriteLine("Bonjour, {0}! Tu as {1} messages dans ta boîte. La température est {2} celsius.", prenom, count, temperature);
    ```


#### L'interpolation de string

* Soit les variables suivantes:
    ```csharp
    string nom = "Samuel";
    int age = 28;
    ```


* On aimerait créer le string: Je m'appelle Marc, j'ai 20 ans. Si on utilise l'addition des chaînes de caractères, on aurait:
    ```csharp
    string presentation = "Je m'appelle " + nom + ", j'ai " +  age + " ans.";
    ```

* En utilisant la méthode string.format:
    ```csharp
    string presentation = string.Format("Je m'appelle {0}, j'ai {1} ans. ",  nom, age);
    ```

* En utilisant l'interpolation:
    ```csharp
    string presentation = $"Je m'appelle {nom}, j'ai {age} ans. ";
    ```



#### Convertir une chaîne de caractères aux autres types

* Utiliser la méthode Parse pour convertir un string vers un autre type
    ```csharp
    int age = int.Parse("35"); 
    bool vrai = bool.Parse("true");
    ```

* Il se peut qu'on essaie de convertir une valeur incorrecte. Par exemple, si on essaie de convertir la chaîne de caractère "Samuel" en int. On aura une erreur lors de l'exécution.
![Erreur conversion de types](/420-413/images/erreur_parse.png)


* Pour éviter d'avoir une erreur, on utilise TryParse pour vérifier si la chaîne de caractère peut se convertir au type désiré
![Utilisation de TryParse](/420-413/images/tryParse.png)


### Type implicite et explicite
* Jusqu'à maintenant, on a vu différents types explicites qui existent, par exemple : `int, double, bool, string, etc.`
Depuis C# 3, C# permet d'utiliser un type implicite. De ce fait, le programmeur demande à C# de trouver le type associé à la variable. 

* On utilise le mot-clé **var** pour définir une variable implicite
    ```csharp
    var nombre = 6; // C# sait que c'est un int
    var content = true; // C# sait que c'est un booléen
    var salutation = "Bonjour"; // sait que c'est un string
    ```

* Par contre, si on déclare une variable implicite sans l'initialiser, C# donnera une erreur de compilation.
    ```csharp
    // ❌ Erreur de compilation : "Implicitly-typed variables must be initialized"
    var message; 

    // ✅ Correct : Le compilateur voit "Bonjour" et déduit que 'message' est de type string
    var message = "Bonjour";
    ```
    > * Avec `string message;`, vous dites explicitement au compilateur : "Réserve une boîte pour du texte". Avec `var message;`, vous ne lui donnez aucun indice. Le compilateur refuse de deviner ou de laisser la variable "sans type" jusqu'à plus tard.*








