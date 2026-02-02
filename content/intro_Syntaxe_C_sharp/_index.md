---
title: "Intro et syntaxe du C#"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 3
---


## Introduction à C#


**C#** (prononcé "C Sharp") est un langage de programmation moderne, orienté objet et fortement typé. C# est développé par Microsoft en 2000 et est principalement utilisé pour le développement dʼapplications sous la plateforme .NET. La syntaxe de C# est influencée par C, C++ et Java, mais elle présente certaines spécificités qui lui sont propres.

C# est un langage sensible à la casse.

**L'extension des fichiers C# est `.cs`**

## Caractéristiques principales:

* **Orienté objet**: tout est basé sur des classes et des objets
* **Typé statiquement**: les types de variables doivent être déclarés
* **Géré**: la mémoire est gérée automatiquement par le garbage collector
* **Polyvalent**: applications desktop, web, mobile, jeux vidéo (Unity)
* **Moderne**: syntaxe claire et évolutive

## Domaines d'utilisation: 
Avec C#, on peut créer :

* 🖥️ des applications console 

* 🖥️ Applications Windows (WPF, WinForms)

* 🌐 des sites Web et des API

* 📱 des applications mobiles

* 🎮 des jeux (Unity utilise C#)

👉 Bref : un langage, plusieurs carrières possibles.


## Structure de base d’un programme C#

Un programme C# est composé de plusieurs éléments clés : l’espace de noms (namespace), les classes, et la méthode Main, qui est le point d’entrée de l’application.

```csharp
using System;  // Importation des bibliothèques

namespace MyProgram   // Définition de l'espace de noms
{
    class Program      // Définition de la classe
    {
        static void Main(string[] args)   // Méthode Main, point d'entrée du programme
        {
            Console.WriteLine("Hello, World!");  // Affichage d'un message et aller à la prochaine ligne
        }
    }
}
```
* **using** : Utilisé pour importer des bibliothèques externes (par exemple, `System` contient des classes utiles comme `Console`).
* **namespace** : Un conteneur logique pour les classes et autres types. En d'autres termes, c'est une façon d’organiser le code. Pense à un namespace comme : un dossier une section ou un casier pour ranger des classes. <br>Sans namespaces, ce serait le chaos total 😱. 
<br>Imagine des milliers de classes, toutes avec des noms simples comme Console, List, Button. 
<br>Sans namespace :
    * 💥 conflits de noms
    * 💥 code impossible à lire
    * 💥 développeurs en dépression
* **class** : Un modèle pour créer des objets. Tout programme C# doit contenir au moins une classe.
* **Main** : La méthode Main est le point de départ d'une application C#. C'est ici que l'exécution du programme commence.


## Les identifiants ou les identificateurs
* Ce sont les noms donnés aux classes et à leurs membres.
* Un identifiant doit être composé d’un seul mot commençant par une lettre ou un caractère underscore (_). Mais, il peut contenir aussi un chiffre qui ne doit pas se placer au début de l’identifiant.
* Ils peuvent être composés de lettres majuscules et minuscules, mais le langage C# étant sensible à la casse, les majuscules et minuscules doivent être respectées pour faire référence au bon identifiant
* Par exemple: les identifiants suivants ne sont pas les mêmes `monIdentifiant` et `MonIdentifiant`.

## Les mots-clés
Les mots clés sont des noms réservés par le langage C#, qui ont des significations spécifiques pour le compilateur. Ils ne peuvent pas être utilisés comme identifiants dans votre programme, sauf s’ils incluent `@` comme préfixe. Par exemple, `@if` est un identifiant valide, mais pas *if*, car *if* est un mot clé.
* Par défaut, les mots-clés sont colorés en bleu dans l’éditeur de Visual Studio.
* Le caractère `@` peut également préfixer des identifiants qui n’ont aucun conflit avec les mots-clés. Ainsi `@monIdentifiant` et `monIdentifiant` seront interprétés de la même manière (la même variable).

![Mots clés en C#](/420-413/images/mots_cles.png)

## Les commentaires
C# prend en charge deux formes différentes de commentaires:
* Les commentaires sur une seule ligne commencent par // et se terminent à la fin de cette ligne de code. 
* Les commentaires multilignes commencent par /* et se terminent par */

Le commentaire sur plusieurs lignes peut également être utilisé pour insérer du texte dans une ligne de code. Étant donné que ces commentaires ont un caractère de fermeture explicite, vous pouvez inclure plus de code exécutable après le commentaire:

```csharp
public static int additioner(int nombre1, int nombre2) 
{ 
    return nombre1 /* première opérande */ + nombre2 /* deuxième opérande*/;
}
```

Le commentaire sur une seule ligne peut apparaître après le code exécutable sur la même ligne. Le commentaire se termine à la fin de la ligne de texte :

```csharp
float temperature = 34.4f; // Si nous n'ajoutons pas le f ou le F après la valeur, nous obtiendrons une erreur

```

