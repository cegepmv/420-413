---
title: "Architecture .NET"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 2
---

## **Introduction**

**C#** : Langage de programmation développé par Microsoft, principalement utilisé pour le développement dʼapplications sous
Windows, mobiles, web, jeux, etc.

**.NET** : Un écosystème pour le développement dʼapplications qui prend en charge plusieurs langages, dont C#, F#, et
VB.NET. Il fournit des outils, des bibliothèques, et un environnement dʼexécution pour faciliter le développement
d'applications.


## **La plateforme .NET**
La plateforme .NET repose sur une architecture en couches qui permet l'exécution de code écrit dans différents langages (C#, F#, VB.NET) de manière unifiée et performante.

## 1. Les composants fondamentaux

L'architecture s'appuie principalement sur deux piliers :

* **CLR (Common Language Runtime) :** C'est le **moteur/coeur** d'exécution. Il gère l'exécution des programmes, la mémoire (Garbage Collector), la sécurité et la compilation à la volée (JIT).
* **Moteur d'exécution** : Le CLR est responsable de charger et exécuter des programmes .NET et assure la gestion de la
mémoire, la sécurité, et la gestion des exceptions.
* **Conversion IL** -> Code natif : Le code C# est d'abord compilé en Intermediate Language (IL), puis converti en code
machine natif par le CLR.
* **Gestion de la mémoire** : Le CLR gère automatiquement la mémoire via la garbage collection (collecte des objets non
utilisés).
* **Sécurité** : Le CLR applique des règles de sécurité pour exécuter les programmes de manière sécurisée.
* **BCL (Base Class Library) :** Une immense bibliothèque de classes réutilisables qui fournit les fonctionnalités de base (gestion des fichiers, réseau, dates, collections, etc.).

## 2. Le Mécanisme de Compilation du Code Source C#
* **Code source C#** : Lorsqu'un programme C# est écrit, le code source est d'abord compilé en IL (Intermediate Language). Le compilateur C# s'appelle **Roslyn**
* **Le CIL (Common Intermediate Language)** : Le compilateur ne crée pas tout de suite un programme fini. Il traduit votre C# en un langage intermédiaire (autrefois appelé MSIL). 
> *Pourquoi ? Parce que ce langage est universel au sein de l'écosystème .NET. Que vous écriviez en C#, F# ou VB.NET, tout finit en CIL. Cela permet à différents langages de fonctionner ensemble.*

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;En plus du code IL, le compilateur génère des informations cruciales :

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Métadonnées : Une description de chaque classe, méthode et variable définie dans votre code.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Manifeste : La "carte d'identité" du programme (nom, version, bibliothèques externes nécessaires).
* **Assembly** : Le code et les ressources sont stockés dans un fichier assembly (généralement avec l'extension .dll ou .exe).
Un assembly contient un manifeste qui décrit ses types, sa version, et ses métadonnées.
* **Compilation Just-In-Time (JIT)** : Lorsque le programme est exécuté, le CLR charge lʼassembly, et le code IL est compilé en
code natif via la compilation JIT.
> *Particularité : Le JIT ne compile que les morceaux de code au fur et à mesure qu'ils sont appelés ("Juste à temps"). Si une fonction n'est jamais utilisée pendant une session, elle n'est jamais compilée en code machine, ce qui économise des ressources.*


![Mécanisme de compilation](/420-413/images/compilation.png)