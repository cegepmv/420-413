---
title: "Série d'exercices 1"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 8
---

## 1. Le Distributeur de Breuvages Intelligent
* **Scénario** : Vous gérez une machine qui vend du Café (2.50$), du Thé (2.00$) et du Chocolat (3.00$).
* **Exigences** : 
    * Créer une méthode `AfficherMenu()` qui montre les choix et les prix.
    * L'utilisateur entre un montant. Si le montant est insuffisant, redemander de l'argent ou annuler.
    * Calculer la monnaie à rendre en utilisant le moins de pièces possible (pièces de 2$, 1$, 0.25$, 0.10$).
* **Notions** : `while`, `switch`, opérateurs modulo `%`.

## 2. Analyseur de Données Météo (Statistiques)
* **Scénario** : Un centre météo a besoin d'analyser les températures d'une semaine.
* **Exigences** : 
    * Stocker 7 valeurs `double` dans un tableau.
    * Méthode `CalculerMoyenne(double[] temp)` : retourne la moyenne.
    * Méthode `TrouverExtremes(double[] temp)` : affiche la plus haute et la plus basse sans `Max()` ou `Min()`.
    * Afficher un histogramme simple dans la console (ex: 22°C = `**********`).
* **Notions** : Boucles `for`, algorithme de recherche de minimum/maximum.

## 3. Gestionnaire de Contacts avec Recherche Floue
* **Scénario** : Une liste de noms simple mais interactive.
* **Exigences** : 
    * Utiliser une `List<string>` pour stocker les noms.
    * Menu : 1. Ajouter, 2. Supprimer par nom, 3. Rechercher, 4. Quitter.
    * La recherche doit afficher tous les noms qui *contiennent* la lettre ou la syllabe saisie (ex: "an" trouve "André" et "Chantal").
* **Notions** : `List<T>`, méthode `string.Contains()`, `foreach`.

## 4. Système de Facturation de Magasin
* **Scénario** : Calculer le total d'un panier d'achat avec taxes.
* **Exigences** : 
    * L'utilisateur saisit des prix jusqu'à ce qu'il entre `-1`.
    * Gérer un tableau de "rabais" : si le prix > 100$, appliquer 10% de réduction avant taxes.
    * Méthode `CalculerTaxes(double total)` : retourne le montant des taxes (TPS 5%, TVQ 9.975%).
* **Notions** : Accumulateurs, constantes, méthodes de calcul.

## 5. Jeu du Pendu : Le Défi des Caractères
* **Scénario** : Deviner un mot caché lettre par lettre.
* **Exigences** : 
    * Le mot secret est un `char[]`. Créer un second `char[]` rempli de `_`.
    * L'étudiant doit comparer la lettre saisie avec chaque caractère du mot secret.
    * Gérer un maximum de 6 erreurs.
* **Notions** : Tableaux de caractères, manipulation d'index.

## 6. Bureau de Scrutin Virtuel
* **Scénario** : Compter les votes pour une élection à 3 candidats.
* **Exigences** : 
    * Utiliser un `Dictionary<string, int>` où la clé est le nom du candidat.
    * Boucle de vote : l'utilisateur tape le nom ou le numéro du candidat.
    * Méthode `AfficherGagnant()` : parcourt le dictionnaire pour trouver la valeur la plus élevée.
* **Notions** : Dictionnaires, itération sur paires Clé/Valeur.

## 7. Validateur de Complexité de Mot de Passe
* **Scénario** : Sécuriser la création de compte.
* **Exigences** : 
    * Créer une méthode `VerifierForce(string mdp)` qui retourne un score de 1 à 5.
    * Critères : +1 si > 8 car., +1 si majuscule, +1 si chiffre, +1 si symbole (#, !, $), +1 si > 12 car.
    * Utiliser des boucles pour inspecter chaque caractère.
* **Notions** : `char.IsUpper`, `char.IsDigit`, `char.IsPunctuation`.

## 8. Convertisseur de Devises avec Historique
* **Scénario** : Changer de l'argent et garder une trace des transactions.
* **Exigences** : 
    * Utiliser un tableau `double[]` fixe pour les taux (USD, EUR, GBP).
    * Chaque conversion effectuée est ajoutée sous forme de chaîne (ex: "10 CAD -> 7 USD") dans une `List<string>`.
    * Option pour afficher l'historique complet à la fin.
* **Notions** : Tableaux, listes, formatage de texte.

## 9. Bataille Navale : Tactique 1D
* **Scénario** : Détruire des navires cachés dans une ligne de 10 cases.
* **Exigences** : 
    * Un tableau `bool[10]` où 3 cases aléatoires sont `true`.
    * L'utilisateur choisit un index. Afficher "Touché" ou "À l'eau".
    * Le jeu s'arrête quand les 3 bateaux sont coulés.
* **Notions** : `Random`, `do-while`, tableaux de booléens.

## 10. Calculateur de Moyennes Multidimensionnel
* **Scénario** : Gérer les notes d'une classe de 3 étudiants ayant chacun 4 examens.
* **Exigences** : 
    * Déclarer un `double[3, 4]`.
    * Remplir le tableau via des saisies utilisateur.
    * Méthode `MoyenneEtudiant(int index)` : calcule la moyenne d'une ligne.
    * Méthode `MoyenneExamen(int index)` : calcule la moyenne d'une colonne.
* **Notions** : Tableaux 2D, boucles imbriquées.

## 11. Simulateur de File d'Attente (Banque)
* **Scénario** : Gérer l'ordre de passage des clients.
* **Exigences** : 
    * Une `List<string>` simulant une file d'attente.
    * Options : "Nouveau client", "Servir prochain", "Afficher file".
    * "Servir prochain" doit afficher le nom et retirer le premier élément (index 0).
* **Notions** : `List.Add()`, `List.RemoveAt()`.

## 12. Créateur de Deck de Cartes et Mélangeur
* **Scénario** : Générer et mélanger un jeu de 52 cartes.
* **Exigences** : 
    * Deux tableaux : `couleurs` (Pique, Coeur...) et `valeurs` (As, 2, 3...).
    * Générer les 52 combinaisons dans une `List<string>`.
    * Algorithme de mélange : échanger chaque carte avec une autre à un index aléatoire.
* **Notions** : Boucles imbriquées, algorithme de permutation (Swap).

## 13. Détecteur de Palindromes et de "Mirroring"
* **Scénario** : Analyser si un mot est identique à l'envers.
* **Exigences** : 
    * Saisir un mot. Créer une méthode `EstPalindrome(string mot)`.
    * Inverser la chaîne manuellement dans un tableau de caractères pour comparer.
    * Ne pas utiliser `Array.Reverse()`.
* **Notions** : Boucle `for` décroissante, manipulation de chaînes.

## 14. Inventaire de Magasin (Tableaux Dentelés)
* **Scénario** : Gérer des rayons de différentes tailles.
* **Exigences** : 
    * Un tableau dentelé `string[][] rayons = new string[3][]`.
    * Rayon 1 : 2 produits, Rayon 2 : 5 produits, Rayon 3 : 3 produits.
    * L'utilisateur peut modifier un produit en spécifiant [rayon, index].
* **Notions** : `Jagged Arrays`, gestion des limites de tableaux.

## 15. Le Juste Prix (Multi-joueurs)
* **Scénario** : Deviner un prix secret généré aléatoirement.
* **Exigences** : 
    * Le programme génère un nombre entre 1 et 1000.
    * Plusieurs joueurs entrent leur nom. Chacun joue à tour de rôle.
    * Le programme indique "C'est plus !" ou "C'est moins !".
* **Notions** : `Random`, listes de noms, boucle de jeu.

## 16. Système de Login avec Blocage
* **Scénario** : Sécuriser l'accès à une console.
* **Exigences** : 
    * Stocker les utilisateurs/mots de passe dans deux tableaux parallèles ou un dictionnaire.
    * L'utilisateur a 3 tentatives. Après 3 erreurs, le programme se verrouille (utilise `Thread.Sleep` pour simuler une attente).
* **Notions** : Compteurs, conditions logiques, sécurité de base.

## 17. Analyseur de Texte : Compteur de Mots
* **Scénario** : Analyser la structure d'un paragraphe.
* **Exigences** : 
    * L'utilisateur entre un long texte.
    * Compter le nombre de mots (délimités par des espaces).
    * Compter l'occurrence d'une lettre spécifique demandée à l'utilisateur.
* **Notions** : `string.Split()`, `foreach`, compteurs.

## 18. Calculateur d'IMC Professionnel
* **Scénario** : Santé et nutrition.
* **Exigences** : 
    * Saisir le nom, le poids et la taille de plusieurs patients.
    * Calculer l'IMC ($poids / taille^2$).
    * Méthode `InterpreterIMC(double imc)` : retourne une chaîne (Maigreur, Normal, Obèse).
* **Notions** : Méthodes de retour, formules mathématiques.

## 19. Gestionnaire de Tâches avec Priorité
* **Scénario** : Une To-Do List intelligente.
* **Exigences** : 
    * Stocker les tâches dans une liste.
    * L'utilisateur peut ajouter une tâche avec un niveau d'importance (Haute, Moyenne, Basse).
    * Afficher les tâches filtrées manuellement : d'abord toutes les "Haute", ensuite les autres.
* **Notions** : Filtrage par boucles, comparaisons de chaînes.

## 20. Le Carré Magique (Validation de Grille)
* **Scénario** : Vérifier si une grille 3x3 est un carré magique.
* **Exigences** : 
    * L'utilisateur entre 9 nombres dans un tableau `int[3, 3]`.
    * Le programme doit calculer la somme de chaque ligne, chaque colonne et des deux diagonales.
    * Si toutes les sommes sont égales, c'est un carré magique !
* **Notions** : Algorithmique avancée sur tableaux 2D.