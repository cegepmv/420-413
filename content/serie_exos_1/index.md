---
title: "Série d'exercices 1"
course_code: 420-413
session: Hiver 2026
author: Samuel Fostiné
weight: 8
---

# Première partie

###  1 : Calculatrice Simple
**Concepts pratiqués :** Variables, opérateurs arithmétiques, types numériques

**Description :**
Créez un programme qui demande à l'utilisateur deux nombres et une opération (+, -, *, /), puis affiche le résultat.

**Fonctionnalités requises :**
- Demander deux nombres décimaux à l'utilisateur
- Demander l'opération souhaitée
- Effectuer le calcul approprié
- Afficher le résultat avec 2 décimales
- Gérer la division par zéro

**Exemple d'exécution :**
```
Entrez le premier nombre : 15.5
Entrez le deuxième nombre : 3.2
Choisissez l'opération (+, -, *, /) : *
Résultat : 15.5 * 3.2 = 49.60
```

---

###  2 : Convertisseur de Température
**Concepts pratiqués :** Variables, opérateurs, formules mathématiques, casting

**Description :**
Développez un convertisseur qui transforme une température de Celsius vers Fahrenheit et Kelvin.

**Fonctionnalités requises :**
- Demander une température en Celsius
- Calculer l'équivalent en Fahrenheit : F = (C × 9/5) + 32
- Calculer l'équivalent en Kelvin : K = C + 273.15
- Afficher les trois températures avec 2 décimales

**Exemple d'exécution :**
```
Entrez la température en Celsius : 25
25.00°C = 77.00°F = 298.15K
```

---

###  3 : Vérificateur de Nombre Pair ou Impair
**Concepts pratiqués :** Variables, opérateur modulo, structure if/else

**Description :**
Créez un programme qui détermine si un nombre entier est pair ou impair.

**Fonctionnalités requises :**
- Demander un nombre entier à l'utilisateur
- Utiliser l'opérateur modulo (%) pour vérifier la parité
- Afficher si le nombre est pair ou impair
- Bonus : indiquer si le nombre est positif, négatif ou zéro

**Exemple d'exécution :**
```
Entrez un nombre entier : 42
Le nombre 42 est pair et positif.
```

---

###  4 : Calculateur d'IMC (Indice de Masse Corporelle)
**Concepts pratiqués :** Variables, opérateurs, structures de contrôle if/else if

**Description :**
Développez un calculateur d'IMC qui catégorise le résultat selon les normes de santé.

**Fonctionnalités requises :**
- Demander le poids en kg et la taille en mètres
- Calculer l'IMC : IMC = poids / (taille × taille)
- Catégoriser le résultat :
  - Moins de 18.5 : Insuffisance pondérale
  - 18.5 à 24.9 : Poids normal
  - 25 à 29.9 : Surpoids
  - 30 ou plus : Obésité
- Afficher l'IMC et la catégorie

**Exemple d'exécution :**
```
Entrez votre poids (kg) : 70
Entrez votre taille (m) : 1.75
Votre IMC est de 22.86 - Poids normal
```

---

###  5 : Jeu du Plus ou Moins
**Concepts pratiqués :** Variables, Random, boucles while, structures if/else

**Description :**
Créez un jeu où l'ordinateur choisit un nombre aléatoire entre 1 et 100, et l'utilisateur doit le deviner.

**Fonctionnalités requises :**
- Générer un nombre aléatoire entre 1 et 100
- Utiliser une boucle pour permettre plusieurs tentatives
- Indiquer si la proposition est trop haute ou trop basse
- Compter le nombre de tentatives
- Afficher un message de victoire avec le nombre de coups

**Exemple d'exécution :**
```
J'ai choisi un nombre entre 1 et 100. Devinez !
Votre proposition : 50
Trop bas !
Votre proposition : 75
Trop haut !
Votre proposition : 63
Bravo ! Vous avez trouvé en 3 coups !
```

---

###  6 : Calculateur de Facture avec Pourboire
**Concepts pratiqués :** Variables, opérateurs, types décimaux, formatage

**Description :**
Développez un programme qui calcule le montant total d'une facture incluant taxes et pourboire.

**Fonctionnalités requises :**
- Demander le montant de base de la facture
- Demander le pourcentage de taxes (ex: 15%)
- Demander le pourcentage de pourboire (ex: 18%)
- Calculer le montant des taxes
- Calculer le montant du pourboire (sur le sous-total avec taxes)
- Afficher le détail complet de la facture

**Exemple d'exécution :**
```
Montant de base : 50.00$
Pourcentage de taxes : 15
Pourcentage de pourboire : 18

--- Facture détaillée ---
Montant de base : 50.00$
Taxes (15%) : 7.50$
Sous-total : 57.50$
Pourboire (18%) : 10.35$
TOTAL À PAYER : 67.85$
```

---

###  7 : Validateur de Mot de Passe
**Concepts pratiqués :** Variables string, opérateurs logiques, structures if/else, méthodes de string

**Description :**
Créez un validateur qui vérifie si un mot de passe respecte certains critères de sécurité.

**Fonctionnalités requises :**
- Demander un mot de passe à l'utilisateur
- Vérifier que le mot de passe :
  - Contient au moins 8 caractères
  - Contient au moins une majuscule
  - Contient au moins une minuscule
  - Contient au moins un chiffre
- Afficher si le mot de passe est valide ou non
- Lister les critères non respectés

**Exemple d'exécution :**
```
Entrez un mot de passe : Hello123
✓ Au moins 8 caractères
✓ Contient une majuscule
✓ Contient une minuscule
✓ Contient un chiffre
Mot de passe valide !
```

---

###  8 : Chronométreur de Temps de Réaction
**Concepts pratiqués :** Variables, Random, DateTime, boucles, opérateurs de comparaison

**Description :**
Développez un mini-jeu qui teste le temps de réaction de l'utilisateur.

**Fonctionnalités requises :**
- Afficher "Préparez-vous..."
- Attendre un délai aléatoire (2 à 5 secondes)
- Afficher "MAINTENANT !" et enregistrer l'heure
- Attendre que l'utilisateur appuie sur Entrée
- Calculer et afficher le temps de réaction en millisecondes
- Proposer de rejouer

**Exemple d'exécution :**
```
Préparez-vous...
MAINTENANT !
[utilisateur appuie sur Entrée]
Votre temps de réaction : 342 ms
Excellent !
Voulez-vous rejouer ? (o/n)
```

---

###  9 : Convertisseur de Devises
**Concepts pratiqués :** Variables, opérateurs, switch/case, types décimaux

**Description :**
Créez un convertisseur qui transforme un montant en dollars canadiens vers différentes devises.

**Fonctionnalités requises :**
- Demander un montant en CAD
- Proposer plusieurs devises (USD, EUR, GBP, JPY)
- Utiliser un switch pour sélectionner le taux de change approprié
- Calculer et afficher la conversion
- Utiliser des taux de change réalistes

**Exemple d'exécution :**
```
Montant en CAD : 100
Choisissez la devise :
1. USD (Dollar américain)
2. EUR (Euro)
3. GBP (Livre sterling)
4. JPY (Yen japonais)
Votre choix : 1
100.00 CAD = 72.50 USD
```

---

###  10 : Calculateur de Note Finale
**Concepts pratiqués :** Variables, opérateurs, structures if/else, moyenne pondérée

**Description :**
Développez un programme qui calcule la note finale d'un étudiant selon différents critères pondérés.

**Fonctionnalités requises :**
- Demander les notes pour :
  - Examens (40% de la note finale)
  - Devoirs (30% de la note finale)
  - Participation (10% de la note finale)
  -  final (20% de la note finale)
- Calculer la note finale pondérée
- Déterminer la cote (A+, A, B+, B, C+, C, D, E)
- Indiquer si l'étudiant a réussi (60% et plus)

**Exemple d'exécution :**
```
Note des examens (sur 100) : 85
Note des devoirs (sur 100) : 78
Note de participation (sur 100) : 92
Note du  final (sur 100) : 88

--- Résultat final ---
Note finale : 84.2 / 100
Cote : A
Statut : Réussite
```

---

###  11 : Simulateur de Dés
**Concepts pratiqués :** Variables, Random, boucles for, opérateurs

**Description :**
Créez un simulateur qui lance plusieurs dés et calcule des statistiques.

**Fonctionnalités requises :**
- Demander le nombre de dés à lancer (1-10)
- Demander le nombre de faces par dé (4, 6, 8, 12, 20)
- Lancer tous les dés
- Afficher le résultat de chaque dé
- Calculer et afficher la somme totale
- Afficher le résultat minimum et maximum obtenu

**Exemple d'exécution :**
```
Nombre de dés : 3
Nombre de faces : 6

Lancer des dés...
Dé 1 : 4
Dé 2 : 6
Dé 3 : 2

Somme totale : 12
Minimum : 2
Maximum : 6
```

---

###  12 : Compteur de Voyelles et Consonnes
**Concepts pratiqués :** Variables string, boucles foreach, switch/case, opérateurs

**Description :**
Développez un programme qui analyse une phrase et compte les voyelles et consonnes.

**Fonctionnalités requises :**
- Demander une phrase à l'utilisateur
- Parcourir chaque caractère de la phrase
- Compter les voyelles (a, e, i, o, u, y - majuscules et minuscules)
- Compter les consonnes
- Compter les espaces et caractères spéciaux
- Afficher les statistiques complètes

**Exemple d'exécution :**
```
Entrez une phrase : Bonjour le monde!

--- Analyse ---
Voyelles : 6
Consonnes : 8
Espaces : 2
Autres caractères : 1
Total de caractères : 17
```

---

###  13 : Générateur de Tables de Multiplication
**Concepts pratiqués :** Variables, boucles for imbriquées, opérateurs, formatage

**Description :**
Créez un programme qui génère et affiche des tables de multiplication.

**Fonctionnalités requises :**
- Demander quel nombre de table l'utilisateur veut voir (1-12)
- Demander jusqu'à quel multiplicateur (généralement 10 ou 12)
- Utiliser une boucle pour générer la table
- Afficher la table de manière formatée et lisible
- Bonus : afficher plusieurs tables côte à côte

**Exemple d'exécution :**
```
Table de multiplication de : 7
Jusqu'à : 10

7 x 1 = 7
7 x 2 = 14
7 x 3 = 21
7 x 4 = 28
7 x 5 = 35
7 x 6 = 42
7 x 7 = 49
7 x 8 = 56
7 x 9 = 63
7 x 10 = 70
```

---

###  14 : Calculateur d'Âge Précis
**Concepts pratiqués :** Variables, DateTime, opérateurs, structures if/else

**Description :**
Développez un programme qui calcule l'âge précis d'une personne en années, mois et jours.

**Fonctionnalités requises :**
- Demander la date de naissance (jour, mois, année)
- Utiliser DateTime pour les calculs
- Calculer l'âge en années, mois et jours
- Calculer le nombre total de jours vécus
- Calculer le jour de la semaine de la naissance
- Afficher le prochain anniversaire

**Exemple d'exécution :**
```
Date de naissance
Jour : 15
Mois : 3
Année : 2005

Vous avez 20 ans, 10 mois et 15 jours
Total de jours vécus : 7625 jours
Vous êtes né(e) un mardi
Prochain anniversaire : dans 136 jours
```

---

###  15 : Détecteur de Nombre Premier
**Concepts pratiqués :** Variables, boucles for, opérateurs, structures if/else

**Description :**
Créez un programme qui détermine si un nombre est premier et trouve tous les nombres premiers dans une plage.

**Fonctionnalités requises :**
- Demander un nombre à l'utilisateur
- Vérifier s'il est premier (divisible seulement par 1 et lui-même)
- Afficher tous les diviseurs du nombre
- Option : afficher tous les nombres premiers jusqu'à ce nombre
- Optimiser l'algorithme (vérifier jusqu'à la racine carrée)

**Exemple d'exécution :**
```
Entrez un nombre : 17

Le nombre 17 est PREMIER
Diviseurs : 1, 17

Voulez-vous voir tous les nombres premiers jusqu'à 17 ? (o/n) : o
2, 3, 5, 7, 11, 13, 17
Total : 7 nombres premiers
```

---

###  16 : Simulateur de Distributeur Bancaire
**Concepts pratiqués :** Variables, switch/case, boucles while, opérateurs

**Description :**
Développez un simulateur de guichet automatique avec un menu et des opérations bancaires de base.

**Fonctionnalités requises :**
- Définir un solde initial (ex: 1000$)
- Créer un menu avec options :
  1. Consulter le solde
  2. Déposer de l'argent
  3. Retirer de l'argent
  4. Quitter
- Vérifier que les retraits n'excèdent pas le solde
- Utiliser une boucle pour répéter le menu
- Afficher l'historique des transactions

**Exemple d'exécution :**
```
=== Guichet Automatique ===
1. Consulter le solde
2. Déposer
3. Retirer
4. Quitter
Votre choix : 3

Montant à retirer : 50
Retrait effectué. Nouveau solde : 950.00$
```

---

###  17 : Convertisseur Binaire/Décimal
**Concepts pratiqués :** Variables, boucles while, opérateurs, types numériques

**Description :**
Créez un convertisseur bidirectionnel entre nombres décimaux et binaires.

**Fonctionnalités requises :**
- Proposer deux options :
  1. Décimal vers binaire
  2. Binaire vers décimal
- Pour décimal vers binaire : utiliser la division successive par 2
- Pour binaire vers décimal : utiliser les puissances de 2
- Afficher les étapes de conversion
- Valider que l'entrée binaire contient seulement 0 et 1

**Exemple d'exécution :**
```
1. Décimal vers Binaire
2. Binaire vers Décimal
Votre choix : 1

Entrez un nombre décimal : 42

Conversion de 42 en binaire :
42 ÷ 2 = 21 reste 0
21 ÷ 2 = 10 reste 1
10 ÷ 2 = 5 reste 0
5 ÷ 2 = 2 reste 1
2 ÷ 2 = 1 reste 0
1 ÷ 2 = 0 reste 1

Résultat : 42 (décimal) = 101010 (binaire)
```

---

###  18 : Jeu de Pierre-Papier-Ciseaux
**Concepts pratiqués :** Variables, Random, switch/case, structures de contrôle, boucles

**Description :**
Développez le jeu classique Pierre-Papier-Ciseaux contre l'ordinateur avec score.

**Fonctionnalités requises :**
- Créer un menu avec les 3 choix possibles
- Générer un choix aléatoire pour l'ordinateur
- Déterminer le gagnant selon les règles :
  - Pierre bat Ciseaux
  - Ciseaux bat Papier
  - Papier bat Pierre
- Tenir un score (victoires, défaites, égalités)
- Permettre de jouer plusieurs parties
- Afficher le score final

**Exemple d'exécution :**
```
=== Pierre-Papier-Ciseaux ===
1. Pierre
2. Papier
3. Ciseaux
Votre choix : 1

Vous : Pierre
Ordinateur : Ciseaux
Vous gagnez !

Score - Vous: 1 | Ordi: 0 | Égalités: 0
Rejouer ? (o/n)
```

---

###  19 : Calculateur de Moyenne Mobile
**Concepts pratiqués :** Variables, boucles for, opérateurs, types numériques

**Description :**
Créez un programme qui calcule la moyenne d'une série de nombres entrés par l'utilisateur.

**Fonctionnalités requises :**
- Demander combien de nombres l'utilisateur veut entrer
- Utiliser une boucle pour saisir chaque nombre
- Calculer la moyenne, le minimum et le maximum
- Calculer l'écart-type (bonus)
- Afficher tous les nombres au-dessus et en-dessous de la moyenne

**Exemple d'exécution :**
```
Combien de nombres : 5
Nombre 1 : 12
Nombre 2 : 15
Nombre 3 : 8
Nombre 4 : 20
Nombre 5 : 10

--- Statistiques ---
Moyenne : 13.00
Minimum : 8
Maximum : 20
Nombres au-dessus de la moyenne : 15, 20
Nombres en-dessous de la moyenne : 12, 8, 10
```

---

###  20 : Générateur de Calendrier Mensuel
**Concepts pratiqués :** Variables, DateTime, boucles, structures if/else, formatage

**Description :**
Développez un programme qui affiche le calendrier d'un mois donné.

**Fonctionnalités requises :**
- Demander le mois et l'année
- Déterminer le premier jour du mois (lundi, mardi, etc.)
- Calculer le nombre de jours dans le mois
- Afficher le calendrier formaté avec les jours de la semaine
- Mettre en évidence le jour actuel si c'est le mois en cours
- Bonus : marquer les fins de semaine différemment

**Exemple d'exécution :**
```
Mois (1-12) : 2
Année : 2026

      Février 2026
Dim Lun Mar Mer Jeu Ven Sam
  1   2   3   4   5   6   7
  8   9  10  11  12  13  14
 15  16  17  18  19  20  21
 22  23  24  25  26  27  28
```

---

## s Avancés

Ces s intègrent **tableaux**, **collections** (List, Dictionary), **méthodes** et tous les concepts précédents.

---

###  21 : Gestionnaire de Liste de Courses
**Concepts pratiqués :** List<string>, méthodes, boucles, switch/case

**Description :**
Créez une application complète pour gérer une liste de courses avec ajout, suppression et affichage.

**Fonctionnalités requises :**
- Créer une List<string> pour stocker les articles
- Menu avec options :
  1. Ajouter un article
  2. Supprimer un article
  3. Afficher la liste
  4. Rechercher un article
  5. Vider la liste
  6. Compter les articles
  7. Quitter
- Créer des méthodes pour chaque fonctionnalité
- Éviter les doublons
- Trier la liste alphabétiquement (option)

**Structure suggérée :**
```csharp
static List<string> listeDesCourses = new List<string>();

static void AjouterArticle()
static void SupprimerArticle()
static void AfficherListe()
static void RechercherArticle()
```

**Exemple d'exécution :**
```
=== Liste de Courses ===
1. Ajouter un article
2. Supprimer un article
3. Afficher la liste
4. Rechercher un article
5. Vider la liste
6. Quitter
Choix : 1

Article à ajouter : Lait
Article ajouté ! Total : 1 article(s)
```

---

###  22 : Carnet d'Adresses
**Concepts pratiqués :** Dictionary, méthodes, structures (ou classes), collections

**Description :**
Développez un carnet d'adresses qui stocke nom, téléphone et courriel de plusieurs contacts.

**Fonctionnalités requises :**
- Utiliser un Dictionary<string, Contact> (nom comme clé)
- Créer une structure Contact avec propriétés :
  - Nom
  - Téléphone
  - Courriel
- Méthodes pour :
  - Ajouter un contact
  - Modifier un contact
  - Supprimer un contact
  - Rechercher un contact
  - Afficher tous les contacts
  - Afficher les contacts par ordre alphabétique

**Structure suggérée :**
```csharp
struct Contact
{
    public string Nom;
    public string Telephone;
    public string Courriel;
}

static Dictionary<string, Contact> carnet = new Dictionary<string, Contact>();

static void AjouterContact()
static Contact RechercherContact(string nom)
```

**Exemple d'exécution :**
```
=== Carnet d'Adresses ===
1. Ajouter un contact
2. Rechercher
3. Afficher tous
4. Quitter
Choix : 1

Nom : Jean Tremblay
Téléphone : 514-555-1234
Courriel : jean@email.com
Contact ajouté !
```

---

###  23 : Analyseur de Texte Avancé
**Concepts pratiqués :** Tableaux, string methods, Dictionary, méthodes, LINQ

**Description :**
Créez un analyseur qui effectue des statistiques détaillées sur un texte.

**Fonctionnalités requises :**
- Demander un texte à l'utilisateur (plusieurs phrases)
- Créer des méthodes pour :
  - Compter les mots
  - Compter les phrases
  - Calculer la longueur moyenne des mots
  - Trouver le mot le plus long
  - Compter la fréquence de chaque mot (Dictionary)
  - Afficher les 5 mots les plus fréquents
- Ignorer la ponctuation et la casse pour le comptage

**Méthodes suggérées :**
```csharp
static int CompterMots(string texte)
static int CompterPhrases(string texte)
static double LongueurMoyenneMots(string texte)
static string TrouverMotLePlusLong(string texte)
static Dictionary<string, int> CompterFrequenceMots(string texte)
static void AfficherTopMots(Dictionary<string, int> frequences, int top)
```

**Exemple d'exécution :**
```
Entrez votre texte :
Le chat est sur le tapis. Le chat dort.

--- Analyse ---
Nombre de mots : 9
Nombre de phrases : 2
Longueur moyenne des mots : 3.33 lettres
Mot le plus long : tapis (5 lettres)

Mots les plus fréquents :
1. le (3 fois)
2. chat (2 fois)
3. est (1 fois)
4. sur (1 fois)
5. tapis (1 fois)
```

---

###  24 : Jeu du Pendu
**Concepts pratiqués :** Tableaux char, List, méthodes, string manipulation, Random

**Description :**
Développez le jeu classique du pendu avec une banque de mots et gestion des vies.

**Fonctionnalités requises :**
- Créer un tableau de mots à deviner
- Choisir un mot aléatoirement
- Utiliser un tableau de char pour le mot masqué
- Gérer une List<char> pour les lettres déjà proposées
- Compter les erreurs (maximum 6)
- Créer des méthodes pour :
  - Afficher le mot masqué
  - Vérifier si une lettre est dans le mot
  - Afficher le dessin du pendu selon les erreurs
  - Vérifier si le jeu est gagné/perdu

**Méthodes suggérées :**
```csharp
static string[] banqueDeMots = { "programmation", "ordinateur", "clavier" };
static List<char> lettresProposees = new List<char>();

static string ChoisirMotAleatoire()
static void AfficherMotMasque(string mot)
static bool VerifierLettre(string mot, char lettre)
static void DessinerPendu(int erreurs)
static bool EstMotTrouve(string mot)
```

**Exemple d'exécution :**
```
=== Jeu du Pendu ===
Mot à deviner : _ _ _ _ _ _ _ _ _ _ _  (11 lettres)
Erreurs : 0/6

Proposez une lettre : e
Bien joué !
Mot : _ _ _ _ _ _ _ _ _ _ e
```

---

###  25 : Gestionnaire de Notes d'Étudiants
**Concepts pratiqués :** Dictionary, List, méthodes, structures/classes, calculs statistiques

**Description :**
Créez un système complet pour gérer les notes de plusieurs étudiants dans plusieurs matières.

**Fonctionnalités requises :**
- Utiliser un Dictionary<string, Etudiant>
- Créer une classe/struct Etudiant avec :
  - Nom
  - Dictionary<string, double> pour les notes par matière
- Méthodes pour :
  - Ajouter un étudiant
  - Ajouter une note pour une matière
  - Calculer la moyenne d'un étudiant
  - Calculer la moyenne de classe pour une matière
  - Afficher le bulletin d'un étudiant
  - Trouver le meilleur étudiant
  - Afficher les statistiques générales

**Structure suggérée :**
```csharp
class Etudiant
{
    public string Nom { get; set; }
    public Dictionary<string, double> Notes { get; set; }
    
    public double CalculerMoyenne()
    public void AfficherBulletin()
}

static Dictionary<string, Etudiant> etudiants = new Dictionary<string, Etudiant>();

static void AjouterEtudiant()
static void AjouterNote()
static double CalculerMoyenneClasse(string matiere)
```

**Exemple d'exécution :**
```
=== Gestionnaire de Notes ===
1. Ajouter étudiant
2. Ajouter note
3. Bulletin étudiant
4. Statistiques classe
5. Quitter
Choix : 3

Nom de l'étudiant : Marie Dubois

--- Bulletin de Marie Dubois ---
Mathématiques : 85
Français : 92
Sciences : 78
Moyenne générale : 85.0
```

---

###  26 : Système de Playlist Musicale
**Concepts pratiqués :** List<string>, méthodes, Random, manipulation de listes

**Description :**
Développez un gestionnaire de playlist musicale avec lecture aléatoire et répétition.

**Fonctionnalités requises :**
- Créer une List<string> pour stocker les chansons
- Méthodes pour :
  - Ajouter une chanson
  - Supprimer une chanson
  - Afficher toute la playlist
  - Lire la playlist en ordre
  - Lire en mode aléatoire (shuffle)
  - Rechercher une chanson
  - Déplacer une chanson (changer l'ordre)
  - Afficher le nombre total de chansons
  - Vider la playlist
- Simuler la lecture avec un compteur de chansons jouées

**Méthodes suggérées :**
```csharp
static List<string> playlist = new List<string>();
static Random rand = new Random();

static void AjouterChanson()
static void SupprimerChanson()
static void AfficherPlaylist()
static void LireEnOrdre()
static void LireAleatoire()
static void DeplacerChanson(int indexDepart, int indexArrivee)
```

**Exemple d'exécution :**
```
=== Gestionnaire de Playlist ===
1. Ajouter chanson
2. Supprimer chanson
3. Afficher playlist
4. Lire en ordre
5. Lire aléatoire
6. Quitter
Choix : 3

--- Ma Playlist (4 chansons) ---
1. Bohemian Rhapsody - Queen
2. Imagine - John Lennon
3. Hotel California - Eagles
4. Stairway to Heaven - Led Zeppelin

Choix : 5
Mode lecture aléatoire activé...
♪ Lecture : Hotel California - Eagles
♪ Lecture : Bohemian Rhapsody - Queen
♪ Lecture : Stairway to Heaven - Led Zeppelin
♪ Lecture : Imagine - John Lennon
Playlist terminée !
```

---

###  27 : Générateur et Analyseur de Statistiques
**Concepts pratiqués :** Tableaux, méthodes mathématiques, Random, tri, calculs statistiques

**Description :**
Créez un programme qui génère des données aléatoires et effectue des analyses statistiques complètes.

**Fonctionnalités requises :**
- Générer un tableau de N nombres aléatoires (10-100)
- Créer des méthodes pour calculer :
  - Moyenne
  - Médiane (valeur centrale après tri)
  - Mode (valeur la plus fréquente)
  - Écart-type
  - Minimum et maximum
  - Premier et troisième quartile
- Afficher un histogramme simple en console
- Créer une méthode pour trier le tableau

**Méthodes suggérées :**
```csharp
static double[] GenererDonnees(int taille, int min, int max)
static double CalculerMoyenne(double[] donnees)
static double CalculerMediane(double[] donnees)
static double CalculerMode(double[] donnees)
static double CalculerEcartType(double[] donnees)
static void AfficherHistogramme(double[] donnees)
static double[] TrierTableau(double[] donnees)
```

**Exemple d'exécution :**
```
Génération de 20 nombres entre 1 et 100...

Données générées : 45, 23, 78, 45, 12, ...

--- Statistiques ---
Moyenne : 48.5
Médiane : 46.0
Mode : 45 (apparaît 3 fois)
Écart-type : 24.3
Min : 12
Max : 98
Premier quartile : 28.5
Troisième quartile : 71.0

--- Histogramme ---
 0-20: *** (3)
21-40: ***** (5)
41-60: ******* (7)
61-80: **** (4)
81-100: * (1)
```

---

###  28 : Simulateur de Loto et Statistiques
**Concepts pratiqués :** Tableaux, List, Random, méthodes, comparaison

**Description :**
Créez un simulateur de loterie qui génère des numéros et vérifie les gains.

**Fonctionnalités requises :**
- Demander à l'utilisateur de choisir 6 numéros entre 1 et 49
- Générer un tirage aléatoire de 6 numéros (sans répétition)
- Comparer les numéros du joueur avec le tirage
- Calculer le gain selon le nombre de numéros correspondants :
  - 6 numéros : Jackpot (1 000 000$)
  - 5 numéros : 10 000$
  - 4 numéros : 500$
  - 3 numéros : 20$
  - 2 numéros : 5$
- Méthodes pour :
  - Générer des numéros aléatoires uniques
  - Valider les choix de l'utilisateur (pas de doublons, dans la plage)
  - Comparer deux listes de numéros
  - Calculer le gain
  - Afficher les statistiques après plusieurs parties

**Méthodes suggérées :**
```csharp
static int[] numeros_joueur = new int[6];
static int[] numeros_gagnants = new int[6];

static void ChoisirNumeros()
static int[] GenererTirage()
static int CompterNumerosCorrespondants()
static double CalculerGain(int correspondants)
static bool ContiendreNumero(int[] tableau, int numero)
static void TrierTableau(int[] tableau)
```

**Exemple d'exécution :**
```
=== Simulateur de Loto 6/49 ===

Choisissez 6 numéros entre 1 et 49 :
Numéro 1 : 7
Numéro 2 : 14
Numéro 3 : 21
Numéro 4 : 28
Numéro 5 : 35
Numéro 6 : 42

Vos numéros : 7, 14, 21, 28, 35, 42

Tirage en cours...

Numéros gagnants : 12, 14, 23, 28, 35, 41

Numéros correspondants : 14, 28, 35
Total : 3 numéros

Vous gagnez : 20.00$ !

Rejouer ? (o/n)
```

---

###  29 : Gestion d'Inventaire de Magasin
**Concepts pratiqués :** Tableaux parallèles, méthodes, recherche, tri

**Description :**
Créez un système de gestion d'inventaire avec produits, quantités et prix en utilisant des tableaux parallèles.

**Fonctionnalités requises :**
- Utiliser 4 tableaux parallèles pour stocker :
  - string[] codes (codes produits)
  - string[] noms (noms des produits)
  - int[] quantites (quantités en stock)
  - double[] prix (prix unitaires)
- Méthodes pour :
  - Ajouter un produit
  - Modifier un produit
  - Supprimer un produit
  - Rechercher par code ou nom
  - Afficher tous les produits
  - Afficher les produits en rupture de stock (quantité = 0)
  - Calculer la valeur totale de l'inventaire
  - Vendre un produit (diminuer quantité)
  - Trier les produits par prix

**Méthodes suggérées :**
```csharp
static string[] codes = new string[100];
static string[] noms = new string[100];
static int[] quantites = new int[100];
static double[] prix = new double[100];
static int nombreProduits = 0;

static void AjouterProduit()
static int RechercherParCode(string code)
static void AfficherInventaire()
static void ProduitsEnRupture()
static double ValeurTotaleInventaire()
static void VendreProduit(string code, int quantite)
```

**Exemple d'exécution :**
```
=== Gestion d'Inventaire ===
1. Ajouter produit
2. Vendre
3. Rechercher
4. Afficher inventaire
5. Ruptures de stock
6. Valeur totale
7. Quitter
Choix : 4

--- Inventaire (3 produits) ---
Code    Nom              Qté    Prix    Valeur
P001    Clavier USB      25     29.99   749.75
P002    Souris sans fil  12     19.99   239.88
P003    Écran 24"        0      299.99  0.00

Total produits : 3
Valeur totale : 989.63$
```

---

###  30 : Convertisseur d'Unités Universel
**Concepts pratiqués :** Dictionary, méthodes, conversions, switch/case

**Description :**
Développez un convertisseur complet pour différentes catégories d'unités.

**Fonctionnalités requises :**
- Catégories de conversion :
  - Longueur (m, km, cm, mm, miles, yards, pieds, pouces)
  - Poids (kg, g, mg, livres, onces)
  - Température (Celsius, Fahrenheit, Kelvin)
  - Volume (L, mL, gallons, pintes)
- Utiliser des Dictionary pour les facteurs de conversion
- Méthodes pour chaque catégorie de conversion
- Menu pour sélectionner la catégorie
- Afficher toutes les conversions possibles depuis l'unité saisie

**Méthodes suggérées :**
```csharp
static Dictionary<string, double> facteursLongueur = new Dictionary<string, double>
{
    { "m", 1 },
    { "km", 1000 },
    { "cm", 0.01 },
    // etc.
};

static double ConvertirLongueur(double valeur, string deUnite, string versUnite)
static double ConvertirTemperature(double valeur, string deUnite, string versUnite)
static void AfficherToutesConversions(double valeur, string unite, string categorie)
```

**Exemple d'exécution :**
```
=== Convertisseur d'Unités ===
1. Longueur
2. Poids
3. Température
4. Volume
5. Quitter
Choix : 1

Valeur : 5
Unité de départ : m

5 mètres =
- 5000 millimètres
- 500 centimètres
- 0.005 kilomètres
- 5.468 yards
- 16.404 pieds
- 196.850 pouces
- 0.003 miles
```

---

###  31 : Générateur de Mots de Passe Sécurisés
**Concepts pratiqués :** Tableaux, Random, méthodes, string manipulation, validation

**Description :**
Créez un générateur de mots de passe avec options de personnalisation et vérification de force.

**Fonctionnalités requises :**
- Options configurables :
  - Longueur du mot de passe (8-50 caractères)
  - Inclure majuscules (A-Z)
  - Inclure minuscules (a-z)
  - Inclure chiffres (0-9)
  - Inclure symboles (!@#$%^&*)
- Générer plusieurs mots de passe à la fois
- Créer une méthode pour évaluer la force du mot de passe (faible/moyen/fort)
- Éviter les caractères ambigus (0/O, 1/l/I)
- Permettre de sauvegarder les mots de passe générés dans une List

**Méthodes suggérées :**
```csharp
static char[] majuscules = "ABCDEFGHJKLMNPQRSTUVWXYZ".ToCharArray();
static char[] minuscules = "abcdefghijkmnopqrstuvwxyz".ToCharArray();
static char[] chiffres = "23456789".ToCharArray();
static char[] symboles = "!@#$%^&*".ToCharArray();

static string GenererMotDePasse(int longueur, bool maj, bool min, bool chif, bool sym)
static string EvaluerForce(string motDePasse)
static List<char> CreerPoolDeCaracteres(bool maj, bool min, bool chif, bool sym)
```

**Exemple d'exécution :**
```
=== Générateur de Mots de Passe ===

Longueur (8-50) : 16
Inclure majuscules ? (o/n) : o
Inclure minuscules ? (o/n) : o
Inclure chiffres ? (o/n) : o
Inclure symboles ? (o/n) : o

Mots de passe générés :
1. kT8#mPq2nL9@wXz5 (Force: Fort)
2. Vy3$bNr7hK4&qWm2 (Force: Fort)
3. Jp6%dGt8sM3!xZn9 (Force: Fort)

Générer d'autres mots de passe ? (o/n)
```

---

###  32 : Calculatrice de Prêt Hypothécaire
**Concepts pratiqués :** Méthodes, calculs financiers, tableaux, formatage, boucles

**Description :**
Développez une calculatrice qui calcule les paiements mensuels et génère un tableau d'amortissement.

**Fonctionnalités requises :**
- Demander :
  - Montant du prêt
  - Taux d'intérêt annuel
  - Durée en années
- Calculer le paiement mensuel avec la formule :
  - M = P[r(1+r)^n]/[(1+r)^n-1]
  - M = paiement mensuel
  - P = montant principal
  - r = taux mensuel
  - n = nombre de paiements
- Utiliser des tableaux pour stocker :
  - double[] paiements
  - double[] interets
  - double[] capital
  - double[] soldes
- Afficher les premiers et derniers mois
- Calculer le total des intérêts payés
- Option : comparer différents scénarios

**Méthodes suggérées :**
```csharp
static double CalculerPaiementMensuel(double montant, double tauxAnnuel, int annees)
static void GenererTableauAmortissement(double montant, double tauxAnnuel, int annees,
    out double[] paiements, out double[] interets, out double[] capital, out double[] soldes)
static void AfficherLigneTableau(int mois, double paiement, double interet, double cap, double solde)
static double CalculerTotalInterets(double[] interets)
```

**Exemple d'exécution :**
```
=== Calculatrice Hypothécaire ===

Montant du prêt : 300000
Taux d'intérêt annuel (%) : 3.5
Durée (années) : 25

Paiement mensuel : 1498.88$
Total des intérêts : 149664.00$
Total à rembourser : 449664.00$

--- Tableau d'amortissement (premiers mois) ---
Mois  Paiement  Intérêts  Capital   Solde
1     1498.88   875.00    623.88    299376.12
2     1498.88   873.18    625.70    298750.42
3     1498.88   871.36    627.52    298122.90
4     1498.88   869.53    629.35    297493.55
5     1498.88   867.69    631.19    296862.36
...

Afficher tous les mois ? (o/n)
```

---

###  33 : Système de Quiz avec Banque de Questions
**Concepts pratiqués :** Tableaux parallèles, méthodes, Random, score, validation

**Description :**
Créez un système de quiz interactif avec différentes catégories et suivi des résultats.

**Fonctionnalités requises :**
- Utiliser des tableaux parallèles pour stocker les questions :
  - string[] questions
  - string[] choixA, choixB, choixC, choixD
  - int[] bonnesReponses (1-4)
  - string[] categories
- Créer une banque d'au moins 15 questions
- Méthodes pour :
  - Sélectionner des questions aléatoires
  - Afficher une question avec ses choix
  - Valider la réponse de l'utilisateur
  - Calculer le score
  - Afficher les statistiques finales
  - Filtrer par catégorie
- Mélanger l'ordre des questions
- Empêcher les doublons dans un même quiz

**Méthodes suggérées :**
```csharp
static string[] questions = new string[50];
static string[] choixA = new string[50];
static string[] choixB = new string[50];
static string[] choixC = new string[50];
static string[] choixD = new string[50];
static int[] bonnesReponses = new int[50];
static string[] categories = new string[50];
static int nombreQuestions = 0;

static void InitialiserQuestions()
static int[] SelectionnerQuestionsAleatoires(int nombre)
static void AfficherQuestion(int index)
static bool VerifierReponse(int index, int reponseUtilisateur)
static void AfficherResultats(int bonnes, int totales)
```

**Exemple d'exécution :**
```
=== Quiz C# ===

Catégories disponibles :
1. Programmation C#
2. Mathématiques
3. Culture générale
4. Toutes catégories
Choix : 1

Nombre de questions : 5

--- Question 1/5 ---
Catégorie : Programmation C#

Quel est le résultat de 5 % 2 en C# ?

1. 2.5
2. 1
3. 2
4. 0

Votre réponse : 2
✓ Correct !

Score actuel : 1/1

--- Question 2/5 ---
...

=== Résultats finaux ===
Bonnes réponses : 4/5 (80%)
Mauvaises réponses : 1/5 (20%)
Note : B+
```

---

###  34 : Simulateur de Compte Bancaire avec Historique
**Concepts pratiqués :** List, méthodes, DateTime, formatage, validation

**Description :**
Développez un simulateur de compte bancaire complet avec transactions et historique détaillé.

**Fonctionnalités requises :**
- Variables globales pour le compte :
  - string numeroCompte
  - string titulaire
  - double solde
- Utiliser des List pour l'historique :
  - List<string> typesTransactions (Depot, Retrait, etc.)
  - List<double> montants
  - List<DateTime> dates
  - List<double> soldesApres
  - List<string> descriptions
- Méthodes pour :
  - Déposer (ajouter à l'historique)
  - Retirer (avec vérification solde, ajouter à l'historique)
  - Afficher l'historique complet
  - Afficher les dépôts seulement
  - Afficher les retraits seulement
  - Calculer solde moyen sur une période
  - Afficher les transactions d'un mois spécifique
- Calculer des intérêts mensuels (bonus)

**Méthodes suggérées :**
```csharp
static string numeroCompte = "12345-67890";
static string titulaire = "Jean Tremblay";
static double solde = 1000.00;

static List<string> typesTransactions = new List<string>();
static List<double> montants = new List<double>();
static List<DateTime> dates = new List<DateTime>();
static List<double> soldesApres = new List<double>();
static List<string> descriptions = new List<string>();

static bool Deposer(double montant, string description)
static bool Retirer(double montant, string description)
static void AfficherHistorique()
static void FiltrerParType(string type)
static void FiltrerParMois(int mois, int annee)
static double CalculerSoldeMoyen()
```

**Exemple d'exécution :**
```
=== Compte Bancaire - Jean Tremblay ===
Numéro : 12345-67890
Solde actuel : 1500.00$

1. Déposer
2. Retirer
3. Historique complet
4. Voir dépôts uniquement
5. Voir retraits uniquement
6. Statistiques
7. Quitter
Choix : 3

--- Historique des transactions ---
Date                Type        Montant    Solde      Description
2026-01-15 10:30   Dépôt       +500.00    1500.00    Paie janvier
2026-01-16 14:20   Retrait     -50.00     1450.00    Épicerie
2026-01-18 09:15   Dépôt       +200.00    1650.00    Remboursement
2026-01-20 16:45   Retrait     -150.00    1500.00    Facture téléphone

Total transactions : 4
Solde moyen : 1537.50$
```

---

###  35 : Simulateur de Machine à Sous (Slot Machine)
**Concepts pratiqués :** Random, tableaux, méthodes, boucles, calculs

**Description :**
Créez un jeu de machine à sous avec 3 rouleaux et différentes combinaisons gagnantes.

**Fonctionnalités requises :**
- Définir les symboles possibles : 🍒 (Cerise), 🍋 (Citron), 🍊 (Orange), 🍇 (Raisin), 💎 (Diamant), 7️⃣ (Sept)
- Utiliser un tableau de string pour les symboles
- Variables pour :
  - Solde du joueur
  - Mise actuelle
- Méthodes pour :
  - Tourner les 3 rouleaux (générer 3 symboles aléatoires)
  - Afficher les rouleaux
  - Vérifier les combinaisons gagnantes :
    - 3 identiques : Jackpot (mise × 50)
    - 3 Sept : Super Jackpot (mise × 100)
    - 2 identiques : Petit gain (mise × 5)
    - 3 Diamants : Gros gain (mise × 25)
  - Calculer les gains
  - Gérer le solde du joueur
- Historique des gains et pertes

**Méthodes suggérées :**
```csharp
static string[] symboles = { "🍒", "🍋", "🍊", "🍇", "💎", "7️⃣" };
static Random rand = new Random();
static double solde = 100.00;

static string[] TournerRouleaux()
static void AfficherRouleaux(string[] rouleaux)
static double CalculerGain(string[] rouleaux, double mise)
static bool VerifierTroisIdentiques(string[] rouleaux)
static bool VerifierDeuxIdentiques(string[] rouleaux)
```

**Exemple d'exécution :**
```
=== Machine à Sous 🎰 ===
Solde : 100.00$

Symboles :
🍒 Cerise  🍋 Citron  🍊 Orange
🍇 Raisin  💎 Diamant  7️⃣ Sept

Entrez votre mise (min 1$, max 10$) : 5

 ┌─────┬─────┬─────┐
 │  🍒 │  🍒 │  🍋 │
 └─────┴─────┴─────┘

Deux cerises ! Vous gagnez 25.00$ !
Solde : 120.00$

Rejouer ? (o/n) : o

Entrez votre mise : 10

 ┌─────┬─────┬─────┐
 │  💎 │  💎 │  💎 │
 └─────┴─────┴─────┘

JACKPOT ! Trois diamants !
Vous gagnez 250.00$ !
Solde : 360.00$
```

---

###  36 : Planificateur de Tâches avec Priorités
**Concepts pratiqués :** List, tableaux parallèles, méthodes, tri, DateTime

**Description :**
Développez une application de gestion de tâches avec catégories, priorités et échéances.

**Fonctionnalités requises :**
- Utiliser des List pour stocker les tâches :
  - List<string> titres
  - List<string> descriptions
  - List<int> priorites (1=Basse, 2=Moyenne, 3=Haute, 4=Urgente)
  - List<string> categories
  - List<DateTime> datesEcheance
  - List<int> statuts (1=À faire, 2=En cours, 3=Terminée)
- Méthodes pour :
  - Ajouter une tâche
  - Modifier une tâche
  - Marquer comme terminée
  - Supprimer une tâche
  - Afficher toutes les tâches
  - Filtrer par priorité, catégorie ou statut
  - Trier par date d'échéance ou priorité
  - Afficher les tâches en retard
  - Statistiques (nombre par statut, taux de complétion)

**Méthodes suggérées :**
```csharp
static List<string> titres = new List<string>();
static List<string> descriptions = new List<string>();
static List<int> priorites = new List<int>();
static List<string> categories = new List<string>();
static List<DateTime> datesEcheance = new List<DateTime>();
static List<int> statuts = new List<int>();

static void AjouterTache()
static void AfficherTaches()
static void FiltrerParPriorite(int priorite)
static void AfficherTachesEnRetard()
static void AfficherStatistiques()
static string ObtenirNomPriorite(int priorite)
static string ObtenirNomStatut(int statut)
static int CalculerJoursRestants(DateTime dateEcheance)
```

**Exemple d'exécution :**
```
=== Planificateur de Tâches ===
1. Ajouter tâche
2. Voir toutes les tâches
3. Filtrer
4. Marquer terminée
5. Tâches en retard
6. Statistiques
7. Quitter
Choix : 2

--- Toutes les tâches ---
[1] [URGENTE] Terminer  C# - Catégorie: École
    Échéance: 2026-02-05 (6 jours) - Statut: En cours
    Description: Compléter les 40 mini-s
    
[2] [HAUTE] Réviser pour examen - Catégorie: École
    Échéance: 2026-02-10 (11 jours) - Statut: À faire
    Description: Réviser chapitres 1 à 5
    
[3] [MOYENNE] Faire l'épicerie - Catégorie: Personnel
    Échéance: 2026-02-01 (2 jours) - Statut: À faire
    Description: Acheter fruits et légumes
```

---

###  37 : Système de Sondage et Analyse
**Concepts pratiqués :** List, Dictionary, méthodes, statistiques, pourcentages

**Description :**
Créez un système pour créer des sondages, collecter des réponses et analyser les résultats.

**Fonctionnalités requises :**
- Utiliser des List pour stocker les questions :
  - List<string> questions
  - List<string> option1, option2, option3, option4
- Utiliser Dictionary<string, int> pour compter les réponses
  - Clé : "Question1_Option1", "Question1_Option2", etc.
- Méthodes pour :
  - Créer un sondage (ajouter questions et options)
  - Répondre au sondage (un participant à la fois)
  - Afficher les résultats :
    - Calculer les pourcentages pour chaque option
    - Afficher un graphique en mode texte (barres ASCII)
    - Trouver l'option la plus populaire
  - Calculer le nombre total de participants
  - Réinitialiser les résultats

**Méthodes suggérées :**
```csharp
static List<string> questions = new List<string>();
static List<string> option1 = new List<string>();
static List<string> option2 = new List<string>();
static List<string> option3 = new List<string>();
static List<string> option4 = new List<string>();
static Dictionary<string, int> reponses = new Dictionary<string, int>();
static int nombreParticipants = 0;

static void CreerSondage()
static void RemplirSondage()
static void AnalyserResultats()
static void AfficherGraphique(int questionIndex)
static double CalculerPourcentage(int nbReponses, int total)
static string GenererBarre(double pourcentage)
```

**Exemple d'exécution :**
```
=== Système de Sondage ===
1. Créer sondage
2. Répondre à un sondage
3. Voir résultats
4. Quitter
Choix : 3

Sondage : Satisfaction des étudiants
Participants : 20

Question 1: Comment évaluez-vous la qualité du cours ?
Excellent  : ████████████████ 45% (9 réponses)
Bon        : ██████████ 30% (6 réponses)
Moyen      : ████ 15% (3 réponses)
Faible     : ██ 10% (2 réponses)

Question 2: Recommanderiez-vous ce cours ?
Oui, certainement      : ██████████████████ 60% (12 réponses)
Probablement oui       : ████████ 25% (5 réponses)
Probablement non       : ████ 10% (2 réponses)
Non, certainement pas  : ██ 5% (1 réponse)

Option la plus populaire : "Oui, certainement"
```

---

###  38 : Jeu de Mémoire avec Cartes
**Concepts pratiqués :** Tableaux, List, méthodes, Random, logique de jeu, temps

**Description :**
Développez un jeu de mémoire simplifié où il faut retrouver les paires de nombres identiques.

**Fonctionnalités requises :**
- Créer un tableau de 16 cartes (8 paires de nombres de 1 à 8)
- Utiliser des tableaux pour gérer l'état :
  - int[] valeurs (contient les nombres 1-8 en double)
  - bool[] estVisible (true si carte retournée)
  - bool[] estTrouvee (true si paire trouvée)
- Mélanger les cartes au début
- Méthodes pour :
  - Initialiser et mélanger les cartes
  - Afficher toutes les cartes (cachées sauf visibles et trouvées)
  - Retourner une carte (par sa position 0-15)
  - Vérifier si deux cartes forment une paire
  - Compter le nombre de coups
  - Vérifier si toutes les paires sont trouvées
- Empêcher de retourner plus de 2 cartes à la fois
- Afficher le temps écoulé

**Méthodes suggérées :**
```csharp
static int[] valeurs = new int[16];
static bool[] estVisible = new bool[16];
static bool[] estTrouvee = new bool[16];
static Random rand = new Random();
static int nombreCoups = 0;

static void InitialiserJeu()
static void MelangerCartes()
static void AfficherCartes()
static void RetournerCarte(int position)
static bool VerifierPaire(int pos1, int pos2)
static bool JeuTermine()
```

**Exemple d'exécution :**
```
=== Jeu de Mémoire 🎴 ===

Positions : 0 à 15

 0   1   2   3   4   5   6   7
[?] [?] [?] [?] [?] [?] [?] [?]

 8   9  10  11  12  13  14  15
[?] [?] [?] [?] [?] [?] [?] [?]

Paires trouvées : 0/8
Coups : 0

Première carte (0-15) : 3
 0   1   2   3   4   5   6   7
[?] [?] [?] [5] [?] [?] [?] [?]

Deuxième carte (0-15) : 11
 0   1   2   3   4   5   6   7
[?] [?] [?] [5] [?] [?] [?] [?]

 8   9  10  11  12  13  14  15
[?] [?] [?] [3] [?] [?] [?] [?]

Pas de paire ! Cartes cachées...

Paires trouvées : 0/8
Coups : 1
```

---

###  39 : Calculatrice de Budget Personnel
**Concepts pratiqués :** List, Dictionary, méthodes, calculs financiers, DateTime

**Description :**
Créez une application complète pour gérer un budget personnel avec revenus, dépenses et analyse.

**Fonctionnalités requises :**
- Utiliser des List pour stocker les transactions :
  - List<string> types ("Revenu" ou "Depense")
  - List<double> montants
  - List<string> categories (Salaire, Épicerie, Transport, Loisirs, etc.)
  - List<DateTime> dates
  - List<string> descriptions
- Utiliser un Dictionary<string, double> pour les budgets prévus par catégorie
- Méthodes pour :
  - Ajouter revenu/dépense
  - Afficher le résumé mensuel
  - Calculer solde actuel
  - Afficher dépenses par catégorie (avec pourcentages)
  - Comparer budget prévu vs réel
  - Afficher graphique en barres des dépenses
  - Identifier les plus grosses dépenses
  - Filtrer par mois

**Méthodes suggérées :**
```csharp
static List<string> types = new List<string>();
static List<double> montants = new List<double>();
static List<string> categories = new List<string>();
static List<DateTime> dates = new List<DateTime>();
static List<string> descriptions = new List<string>();
static Dictionary<string, double> budgetsPrevus = new Dictionary<string, double>();

static void AjouterTransaction()
static double CalculerRevenusMois(int mois, int annee)
static double CalculerDepensesMois(int mois, int annee)
static Dictionary<string, double> DepensesParCategorie(int mois, int annee)
static void AfficherResumeMensuel(int mois, int annee)
static void AfficherGraphiqueDepenses(Dictionary<string, double> depenses, double total)
static string GenererBarre(double pourcentage)
```

**Exemple d'exécution :**
```
=== Budget Personnel - Janvier 2026 ===

Revenus totaux : 3200.00$
Dépenses totales : 2450.00$
Solde : +750.00$

--- Dépenses par catégorie ---
Loyer         : 1200.00$ (49%) ████████████████████
Épicerie      : 450.00$  (18%) ████████
Transport     : 200.00$  (8%)  ███
Loisirs       : 300.00$  (12%) █████
Téléphone     : 80.00$   (3%)  █
Autres        : 220.00$  (9%)  ████

--- Budget prévu vs réel ---
Épicerie : 450$ / 500$ (sous budget de 50$)
Transport : 200$ / 150$ (dépassement de 50$)
Loisirs : 300$ / 250$ (dépassement de 50$)

Plus grosses dépenses du mois :
1. Loyer - 1200.00$ (2026-01-01)
2. Épicerie Costco - 180.00$ (2026-01-15)
3. Restaurant - 120.00$ (2026-01-20)
```

---

###  40 : Simulateur de Tournoi Sportif (Round-Robin)
**Concepts pratiqués :** List, tableaux parallèles, méthodes, algorithmes, tri

**Description :**
Développez un système pour gérer un tournoi sportif où chaque équipe affronte toutes les autres.

**Fonctionnalités requises :**
- Utiliser des tableaux/listes parallèles pour stocker les équipes :
  - List<string> nomsEquipes
  - List<int> victoires
  - List<int> defaites
  - List<int> nulles
  - List<int> pointsMarques
  - List<int> pointsEncaisses
- Créer des List pour les matches :
  - List<string> equipe1
  - List<string> equipe2
  - List<int> score1
  - List<int> score2
  - List<int> rondes
- Méthodes pour :
  - Ajouter des équipes
  - Générer le calendrier complet (round-robin)
  - Simuler un match (aléatoire ou saisie manuelle)
  - Enregistrer les résultats
  - Mettre à jour les classements
  - Afficher le classement actuel
  - Afficher les matches à venir
  - Calculer les statistiques (différentiel de points, taux de victoire)

**Méthodes suggérées :**
```csharp
static List<string> nomsEquipes = new List<string>();
static List<int> victoires = new List<int>();
static List<int> defaites = new List<int>();
static List<int> nulles = new List<int>();
static List<int> pointsMarques = new List<int>();
static List<int> pointsEncaisses = new List<int>();

static List<string> matchEquipe1 = new List<string>();
static List<string> matchEquipe2 = new List<string>();
static List<int> matchScore1 = new List<int>();
static List<int> matchScore2 = new List<int>();

static void AjouterEquipe(string nom)
static void GenererCalendrier()
static void SimulerMatch(int indexMatch)
static void EnregistrerResultat(string equipe1, string equipe2, int score1, int score2)
static void AfficherClassement()
static int TrouverIndexEquipe(string nom)
static double CalculerTauxVictoire(int index)
static int CalculerPoints(int index) // Victoire = 3pts, Nulle = 1pt
```

**Exemple d'exécution :**
```
=== Tournoi de Hockey Round-Robin ===

Participants : 6 équipes
Nombre total de matches : 15

--- Classement après ronde 3 ---
Pos Équipe             Pts  V  N  D  PM  PE  Diff
1   Canadiens MTL      9    3  0  0  15  8   +7
2   Maple Leafs TOR    6    2  0  1  12  10  +2
3   Bruins BOS         6    2  0  1  10  9   +1
4   Senators OTT       3    1  0  2  9   11  -2
5   Sabres BUF         3    1  0  2  7   10  -3
6   Red Wings DET      0    0  0  3  5   10  -5

--- Prochaine ronde (Ronde 4) ---
Canadiens MTL vs Senators OTT
Maple Leafs TOR vs Red Wings DET
Bruins BOS vs Sabres BUF

1. Simuler ronde suivante
2. Entrer résultats manuellement
3. Afficher statistiques
4. Quitter
```

---

### Bonnes Pratiques
1. **Nommage** : Utilisez des noms descriptifs (camelCase pour variables, PascalCase pour méthodes)
2. **Commentaires** : Expliquez les parties complexes de votre code
3. **Validation** : Vérifiez toujours les entrées utilisateur
4. **Gestion d'erreurs** : Utilisez try-catch pour les opérations risquées
5. **Modularité** : Divisez votre code en méthodes réutilisables
6. **Tests** : Testez tous les scénarios possibles


---

# Deuxième partie

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