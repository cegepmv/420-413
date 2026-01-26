---
title: "Opérateurs"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 5
---


## Les opérateurs


### Opérateurs Arithmétiques en C#

| Opérateur | Nom | Description | Exemple | Résultat |
| :--- | :--- | :--- | :--- | :--- |
| `+` | **Addition** | Somme de deux valeurs | `int x = 10 + 5;` | `15` |
| `-` | **Soustraction** | Différence entre deux valeurs | `int x = 20 - 8;` | `12` |
| `*` | **Multiplication** | Produit de deux valeurs | `double x = 5.5 * 2;` | `11.0` |
| `/` | **Division** | Quotient de la division | `int x = 10 / 3;` | `3` (Entier) |
| `%` | **Modulo** | Reste de la division entière | `int x = 10 % 3;` | `1` |
---

### Opérateurs d'incrémentation et de décrémentation

| Opérateur | Nom | Position | Description | Exemple | Résultat (x) |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `++x` | **Pré-incrémentation** | Avant | Incrémente, puis retourne la valeur | `int y = ++x;` | Augmenté de 1 |
| `x++` | **Post-incrémentation** | Après | Retourne la valeur, puis incrémente | `int y = x++;` | Augmenté de 1 |
| `--x` | **Pré-décrémentation** | Avant | Décrémente, puis retourne la valeur | `int y = --x;` | Diminué de 1 |
| `x--` | **Post-décrémentation** | Après | Retourne la valeur, puis décrémente | `int y = x--;` | Diminué de 1 |



#### Pourquoi la position est-elle importante ?

La différence réside dans la **valeur retournée** par l'expression au moment de l'exécution :

* **Pré (`++x`) :** C'est le mode "Mise à jour d'abord". On change la valeur et on utilise le nouveau résultat tout de suite.
* **Post (`x++`) :** C'est le mode "Utilisation d'abord". On utilise la valeur actuelle dans le calcul, et l'ajout de 1 se fait juste après.


#### Exemple de comparaison :
```csharp
int a = 10;
int b = 10;

int resultatA = ++a; // a devient 11, puis resultatA reçoit 11.
int resultatB = b++; // resultatB reçoit 10, puis b devient 11.

// À la fin :
// a est 11, resultatA est 11
// b est 11, resultatB est 10
```
---

### Opérateurs Logiques en C#

Les opérateurs logiques permettent de tester plusieurs conditions à la fois et retournent toujours une valeur booléenne (`true` ou `false`).

| Opérateur | Nom | Description | Exemple |
| :--- | :--- | :--- | :--- |
| `&&` | **ET (AND)** | Retourne `true` si **toutes** les conditions sont vraies. | `(age >= 18 && aPermis)` |
| `\|\|` | **OU (OR)** | Retourne `true` si **au moins une** des conditions est vraie. | `(estSamedi \|\| estDimanche)` |
| `!` | **NON (NOT)** | Inverse l'état logique (vrai devient faux et inversement). | `!estConnecte` |
| `^` | **OU exclusif (XOR)** | Retourne `true` si **une seule** des deux conditions est vraie. | `(estOptionA ^ estOptionB)` |



#### Tables de vérité (Résumé)

| A | B | A && B | A \|\| B | A ^ B | !A |
| :---: | :---: | :---: | :---: | :---: | :---: |
| `true` | `true` | `true` | `true` | `false` | `false` |
| `true` | `false` | `false` | `true` | `true` | `false` |
| `false` | `true` | `false` | `true` | `true` | `true` |
| `false` | `false` | `false` | `false` | `false` | `true` |


#### Le concept de "Court-circuit" (Short-circuit)

Les opérateurs `&&` et `||` sont dits "intelligents" en C# :

1. **Avec `&&` :** Si la première condition est **fausse**, C# n'évalue même pas la deuxième (car le résultat sera forcément faux).
2. **Avec `||` :** Si la première condition est **vraie**, C# s'arrête là (car le résultat sera forcément vrai).

> **Astuce :** Placez toujours la condition la plus "lourde" ou risquée en deuxième position pour profiter du court-circuit.


#### Opérateurs logiques : Court-circuit vs Évaluation complète

Il existe deux variantes pour les opérateurs **ET** et **OU**. La différence réside dans la gestion de la deuxième condition.

| Type | ET | OU | Comportement |
| :--- | :---: | :---: | :--- |
| **Court-circuit** | `&&` | `\|\|` | Évalue la 2e condition **uniquement si nécessaire**. |
| **Évaluation complète**| `&` | `\|` | Évalue **toujours** les deux conditions, sans exception. |


#### Pourquoi utiliser l'évaluation complète (`&` et `|`) ?

L'utilisation de `&` et `|` sur des booléens est plus rare, mais elle est nécessaire si la deuxième condition contient un **effet de bord** (une action qui doit absolument se produire, comme une incrémentation ou l'appel d'une méthode).

#### Exemple de différence :

```csharp
int compteur = 0;
bool conditionFausse = false;

// Cas 1 : Court-circuit (&&)
if (conditionFausse && ++compteur > 0) { /* ... */ }
Console.WriteLine(compteur); // Affiche 0 (le ++ n'a jamais été exécuté)

// Cas 2 : Évaluation complète (&)
if (conditionFausse & ++compteur > 0) { /* ... */ }
Console.WriteLine(compteur); // Affiche 1 (le ++ a été exécuté malgré le faux)
```
---

### Opérateurs de comparaison en C#

Les opérateurs de comparaison permettent de vérifier la relation entre deux expressions. Le résultat est toujours une valeur booléenne (`true` ou `false`).

| Opérateur | Nom | Description | Exemple | Résultat |
| :--- | :--- | :--- | :--- | :--- |
| `==` | **Égalité** | `true` si les valeurs sont identiques | `5 == 5` | `true` |
| `!=` | **Inégalité** | `true` si les valeurs sont différentes | `5 != 3` | `true` |
| `>` | **Plus grand que** | `true` si la gauche est strictement supérieure | `10 > 5` | `true` |
| `<` | **Plus petit que** | `true` si la gauche est strictement inférieure | `2 < 1` | `false` |
| `>=` | **Plus grand ou égal** | `true` si la gauche est supérieure ou égale | `5 >= 5` | `true` |
| `<=` | **Plus petit ou égal** | `true` si la gauche est inférieure ou égale | `4 <= 3` | `false` |


##### Confusion entre `=` et `==`
C'est l'erreur la plus fréquente chez les débutants :
* `=` est l'opérateur d'**assignation** (on donne une valeur à une variable).
* `==` est l'opérateur de **comparaison** (on vérifie si deux valeurs sont égales).

##### Comparaison de chaînes de caractères (`string`)
En C#, l'opérateur `==` fonctionne pour comparer le contenu des chaînes de caractères. Attention : la comparaison est **sensible à la casse**.
```csharp
string nomUn = "Bob";
string nomDeux = "bob";

bool sontEgaux = (nomUn == nomDeux); // Résultat: false
```