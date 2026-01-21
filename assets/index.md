+++
date = '2025-08-20T19:03:11-04:00'
title = 'Série 12'
weight = 12
+++


---

# 📥 **Exercices – Entrées utilisateur en Java**

---

### 🧪 **Exercice 1 – Lire un prénom**

Demande à l’utilisateur de saisir son prénom (chaine de caractères) et affiche :
`Bonjour, [prénom] !`


---

### 🧪 **Exercice 3 – Lire deux entiers et afficher leur somme**

Demande à l’utilisateur de saisir deux entiers, puis affiche la somme.

---

### 🧪 **Exercice 4 – Lire un nombre à virgule**

Demande à l’utilisateur de saisir un nombre à virgule (double) et affiche ce nombre arrondi à 2 décimales.

---

### 🧪 **Exercice 6 – Lire une phrase complète**

Demande à l’utilisateur de saisir une phrase complète (avec espaces), puis affiche-la.

---

### 🧪 **Exercice 7 – Calculer la moyenne de 3 notes**

Demande à l’utilisateur de saisir 3 notes (double) séparées par un espace sur la même ligne, calcule la moyenne et affiche-la arrondie à 2 décimales.


---

### Exo_55

Niveau : 3 ⭐

**Exercice** :

Vérification de l'année bissextile : Écrivez un programme qui demande à
l'utilisateur de saisir une année, puis affiche "Année bissextile" si l'année est
divisible par 4 et non divisible par 100, ou si elle est divisible par 400. Sinon,
affiche "Année non bissextile".

```
Entrez une année : 2025
Année non bissextile
```

---


### 🧪 **Exercice 1 – Menu simple de salutation**

Affiche ce menu :

```
1. Dire bonjour  
2. Dire au revoir  
3. Quitter  
```

Lis le choix de l’utilisateur (entier).
Affiche :

* `"Bonjour !"` si choix = 1
* `"Au revoir !"` si choix = 2
* `"Fin du programme."` si choix = 3

---

### 🧪 **Exercice 2 – Menu calculatrice simple**

Affiche ce menu :

```
1. Addition  
2. Soustraction  
3. Multiplication  
4. Division  
5. Quitter  
```

Lis le choix, puis demande deux nombres (entiers).
Effectue l’opération choisie et affiche le résultat.
Si division par zéro, affiche un message d’erreur.

---


### 🧪 **Exercice 4 – Menu gestion de tableau**

Déclare un tableau d’entiers vide de taille 5.
Propose ce menu en boucle jusqu’à ce que l’utilisateur choisisse de quitter :

```
1. Ajouter un nombre au tableau  
2. Afficher le tableau  
3. Quitter  
```

* Si 1 : demande un entier et ajoute-le dans la première case libre du tableau (si possible).
* Si 2 : affiche le contenu du tableau.
* Si 3 : termine le programme.

---


# Exo_71

Niveau : 4 ⭐

Créer un menu pour gérer un reçu avec des options pour ajouter ou retirer des items.

**Exercice** :
- Créez un tableau de `String` pour les noms des plats : `["Crevette", "Salade", "Frite", "Hamburger", "Gâteau"]`.
- Créez un tableau de `float` pour les prix des plats correspondants : `[8.99, 5.60, 6.40, 10.99, 7.99]`.
- Implémentez un menu interactif avec les options suivantes :
  1. Ajouter un item.
  2. Retirer un item.
  3. Afficher le reçu (afficher les plats ajoutés et le total).
  4. Terminer la transaction (Afficher le reçu, écrire un message d'adieu et arrête le programme).
- À chaque ajout d'item, le prix du plat sera ajouté au total de la facture.
- À chaque retrait d'item, le prix du plat sera soustrait du total de la facture.

**Exemple de sortie attendue** :

```
Menu :
1. Ajouter un item
2. Retirer un item
3. Afficher le reçu
4. Terminer la transaction
Total de la facture actuelle : 0.00$

Entrez votre choix : 1
Choisissez un item à ajouter :
1. Crevette           8.99$
2. Salade             5.60$
3. Frite              6.40$
4. Hamburger         10.99$
5. Gâteau             7.99$


Entrez le numéro de l'item : 1
Plat ajouté : Crevette
Total de la facture actuelle : 8.99$

Menu :
1. Ajouter un item
2. Retirer un item
3. Afficher le reçu
4. Terminer la transaction
Total de la facture actuelle : 8.99$

Entrez votre choix : 3
Reçu :

Crevette              8.99$
---------------------------
Total                 8.99$


Menu :
1. Ajouter un item
2. Retirer un item
3. Afficher le reçu
4. Terminer la transaction
Total de la facture actuelle : 8.99$

Entrez votre choix : 4

Crevette              8.99$
---------------------------
Total                 8.99$

Merci pour votre visite!

```

---





<a href="https://github.com/cegepmv/420-111/tree/main/solutions/serie12">Solutions</a>
